using DAL;
using Microsoft.Data.SqlClient;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UsuarioDAL:AbstractDAL<UsuarioBE>
    {
        public UsuarioDAL() : base() { }

        public void Registrar(UsuarioBE entity)
        {
            try
            {
                _sqlcommand.CommandText = @"INSERT INTO Usuario (DNI, Nombre, Apellido, UserName, Password, Email, Bloqueado, Activo, IntentoFallido, Perfil_Id)
                                            SELECT @dni, @nombre, @apellido, @username, @password, @email, 0, 1, 0, p.Perfil_ID
                                            FROM Perfil p
                                            WHERE p.Nombre = @rol;";

                _sqlcommand.Parameters.AddWithValue("@dni", entity.DNI);
                _sqlcommand.Parameters.AddWithValue("@nombre", entity.Nombre);
                _sqlcommand.Parameters.AddWithValue("@apellido", entity.Apellido);
                _sqlcommand.Parameters.AddWithValue("@username", entity.Username);
                _sqlcommand.Parameters.AddWithValue("@password", entity.Password);
                _sqlcommand.Parameters.AddWithValue("@email", entity.Email);
                _sqlcommand.Parameters.AddWithValue("@rol", entity.Rol);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public void BloquearUsuario(string username)
        {
            throw new NotImplementedException();
        }
        public int ObtenerIntentosFallidos(string dNI)
        {
            throw new NotImplementedException();
        }

        public UsuarioBE ObtenerPorUsuario(string usuario)
        {
            try
            {
                
                _sqlcommand.CommandText = "select us.DNI,us.Nombre,us.Apellido,us.UserName,us.Password,us.Email,us.Bloqueado,us.Activo, p.Nombre from Usuario us " +
                                            "inner join Perfil p on us.Perfil_ID= p.Perfil_ID where us.UserName=@username;";
                _sqlcommand.Parameters.AddWithValue("@username", usuario);

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        return new UsuarioBE(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetBoolean(7), reader.GetString(8));

                    }

                }
                return null;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public void ResetearIntentos(string username)
        {
            throw new NotImplementedException();
        }

        public void SumarIntento(string username)
        {
            throw new NotImplementedException();
        }

        public UsuarioBE BuscarUsuarioPorDNI(string dNI)
        {
            try
            {

                _sqlcommand.CommandText = "select us.DNI,us.Nombre,us.Apellido,us.UserName,us.Password,us.Email,us.Bloqueado,us.Activo, p.Nombre from Usuario us " +
                    "inner join Perfil p on us.Perfil_ID= p.Perfil_ID where us.DNI=@dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", dNI);

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        return new UsuarioBE(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetBoolean(7), reader.GetString(8));

                    }

                }
                return null;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public List<UsuarioBE> ListarTodosLosUsuarios()
        {
            try
            {
                _sqlcommand.CommandText = @"select us.DNI,us.Nombre,us.Apellido,us.UserName,us.Password,us.Email,us.Bloqueado,us.Activo, p.Nombre from Usuario us inner join Perfil p on us.Perfil_ID= p.Perfil_ID;";
                _sqlserver.Open();
                var reader = _sqlcommand.ExecuteReader();
                var Usuariolst = new List<UsuarioBE>();
                while (reader.Read())
                {
                    Usuariolst.Add(new UsuarioBE(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetBoolean(7), reader.GetString(8)));
                }
                return Usuariolst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public void EliminarLogico(string _dni)
        {
            try
            {
                _sqlcommand.CommandText = "update Usuario set Activo = 0 where DNI=@dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", _dni);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
    }
}
