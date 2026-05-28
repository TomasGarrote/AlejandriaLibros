using Servicios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class BitacoraDAL : AbstractDAL<Bitacora>
    {
        public BitacoraDAL()
        {
            
        }

        public void RegistrarEvento(Bitacora unEvento)
        {
            try
            {
                _sqlcommand.CommandText = @"INSERT INTO Bitacora (DNI, Fecha, Modulo, Descripcion, Criticidad) 
                                           VALUES (@DNI, @Fecha, @Modulo, @Descripcion, @Criticidad)";

                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DNI", unEvento.DNI);
                _sqlcommand.Parameters.AddWithValue("@Fecha", unEvento.Fecha);
                _sqlcommand.Parameters.AddWithValue("@Modulo", unEvento.Modulo);
                _sqlcommand.Parameters.AddWithValue("@Descripcion", unEvento.Descripcion);
                _sqlcommand.Parameters.AddWithValue("@Criticidad", unEvento.Criticidad);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            finally
            {
                _sqlserver.Close();
            }
        }

        public List<Bitacora> ListarEventos()
        {
            List<Bitacora> lista = new List<Bitacora>();

            try
            {
                _sqlcommand.CommandText = @"SELECT Numero, DNI, Fecha, Modulo, Descripcion, Criticidad 
                                           FROM Bitacora 
                                           WHERE Fecha >= DATEADD(day, -3, GETDATE())
                                           ORDER BY Fecha DESC, Numero DESC";

                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bitacora ev = new Bitacora
                        {
                            Numero = reader.GetInt32(0),
                            DNI = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Modulo = reader.GetString(3),
                            Descripcion = reader.GetString(4),
                            Criticidad = reader.GetInt32(5)
                        };
                        lista.Add(ev);
                    }
                }
            }
            finally
            {
                _sqlserver.Close();
            }

            return lista;
        }

        public List<Bitacora> FiltrarEventos(string nombre, string apellido, string login, string modulo, string evento, DateTime desde, DateTime hasta, int? criticidad)
        {
            List<Bitacora> lista = new List<Bitacora>();

            try
            {
                string query = @"SELECT b.Numero, b.DNI, b.Fecha, b.Modulo, b.Descripcion, b.Criticidad 
                                 FROM Bitacora b 
                                 INNER JOIN Usuario u ON b.DNI = u.DNI 
                                 WHERE 1=1";

                _sqlcommand.Parameters.Clear();

                if (!string.IsNullOrEmpty(nombre))
                {
                    query += " AND u.Nombre LIKE @Nombre";
                    _sqlcommand.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                }
                if (!string.IsNullOrEmpty(apellido))
                {
                    query += " AND u.Apellido LIKE @Apellido";
                    _sqlcommand.Parameters.AddWithValue("@Apellido", "%" + apellido + "%");
                }
                if (!string.IsNullOrEmpty(login))
                {
                    query += " AND u.UserName = @Login";
                    _sqlcommand.Parameters.AddWithValue("@Login", login);
                }
                if (!string.IsNullOrEmpty(modulo))
                {
                    query += " AND b.Modulo = @Modulo";
                    _sqlcommand.Parameters.AddWithValue("@Modulo", modulo);
                }
                if (!string.IsNullOrEmpty(evento))
                {
                    query += " AND b.Descripcion LIKE @Evento";
                    _sqlcommand.Parameters.AddWithValue("@Evento", "%" + evento + "%");
                }
                if (criticidad.HasValue)
                {
                    query += " AND b.Criticidad = @Criticidad";
                    _sqlcommand.Parameters.AddWithValue("@Criticidad", criticidad.Value);
                }

                query += " AND b.Fecha BETWEEN @Desde AND @Hasta";
                _sqlcommand.Parameters.AddWithValue("@賠esde", desde.Date);
                _sqlcommand.Parameters.AddWithValue("@Hasta", hasta.Date.AddDays(1).AddTicks(-1));

                query += " ORDER BY b.Fecha DESC, b.Numero DESC";

                _sqlcommand.CommandText = query;
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bitacora ev = new Bitacora
                        {
                            Numero = reader.GetInt32(0),
                            DNI = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Modulo = reader.GetString(3),
                            Descripcion = reader.GetString(4),
                            Criticidad = reader.GetInt32(5)
                        };
                        lista.Add(ev);
                    }
                }
            }
            finally
            {
                _sqlserver.Close();
            }

            return lista;
        }
    }
}
