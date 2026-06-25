using Servicios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class PerfilDAL : AbstractDAL<Familia>
    {

        public List<PermisoSimple> ObtenerPermisos()
        {
            var lista = new List<PermisoSimple>();
            _sqlcommand.CommandText = "SELECT Nombre FROM PermisoSimple";

            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();

                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new PermisoSimple { Nombre = reader["Nombre"].ToString() });
                    }
                }
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
            }
            return lista;
        }

        public List<Familia> ObtenerFamiliasYPerfiles()
        {
            var listaUnificada = new List<Familia>();
            var nombresFamilias = new List<string>();
            var nombresPerfiles = new List<string>();

            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();

                _sqlcommand.CommandText = "SELECT Nombre FROM Familia";
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read()) nombresFamilias.Add(reader["Nombre"].ToString());
                }

                _sqlcommand.CommandText = "SELECT Nombre FROM Perfil";
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read()) nombresPerfiles.Add(reader["Nombre"].ToString());
                }

                foreach (var nombre in nombresFamilias)
                {
                    var fam = new Familia { Nombre = nombre, EsRol = false };
                    CargarHijosComponente(fam);
                    listaUnificada.Add(fam);
                }

                foreach (var nombre in nombresPerfiles)
                {
                    var perf = new Familia { Nombre = nombre, EsRol = true };
                    CargarHijosComponente(perf);
                    listaUnificada.Add(perf);
                }
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
            }
            return listaUnificada;
        }

        private void CargarHijosComponente(Familia padre)
        {
            string tablaPermisos = padre.EsRol ? "Perfil_PermisoSimple" : "Familia_PermisoSimple";
            string tablaFamilias = padre.EsRol ? "Perfil_Familia" : "Familia_Familia";
            string colPadre = padre.EsRol ? "NombrePerfil" : "NombreFamilia";
            string colPadreHijo = padre.EsRol ? "NombrePerfil" : "NombrePadre";

            var permisosHijos = new List<string>();
            var familiasHijas = new List<string>();

            using (var cmdPermisos = new SqlCommand($"SELECT NombrePermiso FROM {tablaPermisos} WHERE {colPadre} = @Padre", _sqlserver))
            {
                cmdPermisos.Parameters.AddWithValue("@Padre", padre.Nombre);
                using (var reader = cmdPermisos.ExecuteReader())
                {
                    while (reader.Read()) permisosHijos.Add(reader["NombrePermiso"].ToString());
                }
            }

            foreach (var perm in permisosHijos)
            {
                padre.AgregarHijo(new PermisoSimple { Nombre = perm });
            }

            string colHijo = padre.EsRol ? "NombreFamilia" : "NombreHijo";
            using (var cmdFamilias = new SqlCommand($"SELECT {colHijo} FROM {tablaFamilias} WHERE {colPadreHijo} = @Padre", _sqlserver))
            {
                cmdFamilias.Parameters.AddWithValue("@Padre", padre.Nombre);
                using (var reader = cmdFamilias.ExecuteReader())
                {
                    while (reader.Read()) familiasHijas.Add(reader[colHijo].ToString());
                }
            }

            foreach (var nombreHijo in familiasHijas)
            {
                var subFamilia = new Familia { Nombre = nombreHijo, EsRol = false };
                CargarHijosComponente(subFamilia);
                padre.AgregarHijo(subFamilia);
            }
        }

        private bool ExisteNombre(string nombre, string tabla)
        {
            using (var cmdCheck = new SqlCommand($"SELECT COUNT(*) FROM {tabla} WHERE Nombre = @Nombre", _sqlserver))
            {
                cmdCheck.Parameters.AddWithValue("@Nombre", nombre);
                try
                {
                    if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();
                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    return count > 0;
                }
                finally
                {
                    if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
                }
            }
        }

        public void GuardarPermiso(PermisoSimple permiso)
        {
            if (ExisteNombre(permiso.Nombre, "PermisoSimple"))
            {
                throw new ArgumentException(LanguageManager.Instance.GetTraduction("PerfilDalText2"));
            }

            _sqlcommand.CommandText = "INSERT INTO PermisoSimple (Nombre) VALUES (@Nombre)";
            _sqlcommand.Parameters.Clear();
            _sqlcommand.Parameters.AddWithValue("@Nombre", permiso.Nombre);

            EjecutarNonQuery();
        }

        public void EliminarPermiso(string nombre)
        {
            _sqlcommand.CommandText = "DELETE FROM PermisoSimple WHERE Nombre = @Nombre";
            _sqlcommand.Parameters.Clear();
            _sqlcommand.Parameters.AddWithValue("@Nombre", nombre);

            EjecutarNonQuery();
        }

        public void GuardarFamilia(Familia familia)
        {
            string tabla = familia.EsRol ? "Perfil" : "Familia";

            if (ExisteNombre(familia.Nombre, tabla))
            {
                string tipoComponente = familia.EsRol ? "perfil" : "familia";
                throw new ArgumentException($"{LanguageManager.Instance.GetTraduction("PerfilDalText2")} {tipoComponente} {LanguageManager.Instance.GetTraduction("PerfilDalText3")}");
            }

            _sqlcommand.CommandText = $"INSERT INTO {tabla} (Nombre) VALUES (@Nombre)";
            _sqlcommand.Parameters.Clear();
            _sqlcommand.Parameters.AddWithValue("@Nombre", familia.Nombre);

            EjecutarNonQuery();
        }

        public void EliminarFamilia(Familia familia)
        {
            string tabla = familia.EsRol ? "Perfil" : "Familia";
            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();

                LimpiarRelacionesComponente(familia.Nombre, familia.EsRol);

                _sqlcommand.CommandText = $"DELETE FROM {tabla} WHERE Nombre = @Nombre";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Nombre", familia.Nombre);
                _sqlcommand.ExecuteNonQuery();
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
            }
        }


        public bool ElPerfilEstaAsignadoAUsuarios(string nombrePerfil)
        {
            _sqlcommand.CommandText = "SELECT COUNT(*) FROM dbo.Usuario WHERE Rol = @Rol";
            _sqlcommand.Parameters.Clear();
            _sqlcommand.Parameters.AddWithValue("@Rol", nombrePerfil);

            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();
                int cantidad = (int)_sqlcommand.ExecuteScalar();
                return cantidad > 0;
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
                _sqlcommand.Parameters.Clear();
            }
        }

        public bool LaFamiliaEstaEnUsoComoHijo(string nombreFamilia)
        {
            _sqlcommand.CommandText = @"
        SELECT COUNT(*) FROM (
            SELECT NombreHijo FROM Familia_Familia WHERE NombreHijo = @Nombre
            UNION ALL
            SELECT NombreFamilia FROM Perfil_Familia WHERE NombreFamilia = @Nombre
        ) AS Usos";
            _sqlcommand.Parameters.Clear();
            _sqlcommand.Parameters.AddWithValue("@Nombre", nombreFamilia);

            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();
                int cantidad = (int)_sqlcommand.ExecuteScalar();
                return cantidad > 0;
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
                _sqlcommand.Parameters.Clear();
            }
        }

        public void GuardarRelaciones(Familia padre)
        {
            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();

                LimpiarRelacionesComponente(padre.Nombre, padre.EsRol);

                foreach (var hijo in padre.ObtenerHijos())
                {
                    if (hijo is PermisoSimple permiso)
                    {
                        string tabla = padre.EsRol ? "Perfil_PermisoSimple" : "Familia_PermisoSimple";
                        string col1 = padre.EsRol ? "NombrePerfil" : "NombreFamilia";

                        using (var cmd = new SqlCommand($"INSERT INTO {tabla} ({col1}, NombrePermiso) VALUES (@Padre, @Hijo)", _sqlserver))
                        {
                            cmd.Parameters.AddWithValue("@Padre", padre.Nombre);
                            cmd.Parameters.AddWithValue("@Hijo", permiso.Nombre);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (hijo is Familia subFamilia)
                    {
                        string tabla = padre.EsRol ? "Perfil_Familia" : "Familia_Familia";
                        string col1 = padre.EsRol ? "NombrePerfil" : "NombrePadre";
                        string col2 = padre.EsRol ? "NombreFamilia" : "NombreHijo";

                        using (var cmd = new SqlCommand($"INSERT INTO {tabla} ({col1}, {col2}) VALUES (@Padre, @Hijo)", _sqlserver))
                        {
                            cmd.Parameters.AddWithValue("@Padre", padre.Nombre);
                            cmd.Parameters.AddWithValue("@Hijo", subFamilia.Nombre);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
            }
        }

        private void LimpiarRelacionesComponente(string nombrePadre, bool esRol)
        {
            string query1 = esRol ? "DELETE FROM Perfil_PermisoSimple WHERE NombrePerfil = @Padre" : "DELETE FROM Familia_PermisoSimple WHERE NombreFamilia = @Padre";
            string query2 = esRol ? "DELETE FROM Perfil_Familia WHERE NombrePerfil = @Padre" : "DELETE FROM Familia_Familia WHERE NombrePadre = @Padre";

            using (var cmd1 = new SqlCommand(query1, _sqlserver))
            {
                cmd1.Parameters.AddWithValue("@Padre", nombrePadre);
                cmd1.ExecuteNonQuery();
            }
            using (var cmd2 = new SqlCommand(query2, _sqlserver))
            {
                cmd2.Parameters.AddWithValue("@Padre", nombrePadre);
                cmd2.ExecuteNonQuery();
            }
        }


        private void EjecutarNonQuery()
        {
            try
            {
                if (_sqlserver.State != ConnectionState.Open) _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            finally
            {
                if (_sqlserver.State == ConnectionState.Open) _sqlserver.Close();
                _sqlcommand.Parameters.Clear();
            }
        }
    }
}