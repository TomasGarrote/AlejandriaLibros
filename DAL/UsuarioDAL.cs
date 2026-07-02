using DAL;
using Microsoft.Data.SqlClient;
using Servicios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DAL
{
    public class UsuarioDAL:AbstractDAL<Usuario>
    {
        public UsuarioDAL() : base()
        {
            
        }

        public void Registrar(Usuario entity, string DVH)
        {
            try
            {
                _sqlcommand.CommandText = @"INSERT INTO Usuario (DNI, Nombre, Apellido, UserName, Password, Email, Bloqueado, Activo, Rol, DVH)
                                            VALUES (@dni, @nombre, @apellido, @username, @password, @email, 0, 1, @rol, @dvh);";

                _sqlcommand.Parameters.AddWithValue("@dni", entity.DNI);
                _sqlcommand.Parameters.AddWithValue("@nombre", entity.Nombre);
                _sqlcommand.Parameters.AddWithValue("@apellido", entity.Apellido);
                _sqlcommand.Parameters.AddWithValue("@username", entity.Username);
                _sqlcommand.Parameters.AddWithValue("@password", entity.Password);
                _sqlcommand.Parameters.AddWithValue("@email", entity.Email);
                _sqlcommand.Parameters.AddWithValue("@Rol", entity.Rol);
                _sqlcommand.Parameters.AddWithValue("@dvh", DVH);

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
            try
            {
                _sqlcommand.CommandText = @"update Usuario set Bloqueado = 1 where UserName=@user;";
                _sqlcommand.Parameters.AddWithValue("@user", username);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
        public int ObtenerIntentosFallidos(string dNI)
        {
            try
            {
                _sqlcommand.CommandText = @"SELECT Intentos FROM Usuario WHERE DNI = @dni;"; ;
                _sqlcommand.Parameters.AddWithValue("@dni", dNI);

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }

                }
                throw new Exception($"{LanguageManager.Instance.GetTraduction("UserDalText1")} {dNI}");
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

        public Usuario ObtenerPorUserName(string usuario)
        {
            try
            {
                
                _sqlcommand.CommandText = @"SELECT DNI, Nombre, Apellido, UserName, Password, Email, Bloqueado, Activo, Rol FROM Usuario WHERE UserName = @username;"; ;
                _sqlcommand.Parameters.AddWithValue("@username", usuario);

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        return new Usuario(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
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

        public void ResetearIntentos(Usuario usuario)
        {
            try
            {
                _sqlcommand.CommandText = @"update Usuario set Intentos = 0 where DNI=@dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", usuario.DNI);

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

        public void SumarIntentoFallido(Usuario usuario)
        {
            try
            {
                int intentos = ObtenerIntentosFallidos(usuario.DNI);

                _sqlcommand.CommandText = @"update Usuario set Intentos = @intentos where DNI=@dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", usuario.DNI);
                _sqlcommand.Parameters.AddWithValue("@intentos", intentos+1);

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

        public Usuario BuscarUsuarioPorDNI(string dNI)
        {
            try
            {

                _sqlcommand.CommandText = @"SELECT DNI, Nombre, Apellido, UserName, Password, Email, Bloqueado, Activo, Rol FROM Usuario WHERE DNI = @dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", dNI);

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        return new Usuario(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
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

        public List<Usuario> ListarTodosLosUsuarios()
        {
            try
            {
                _sqlcommand.CommandText = @"SELECT DNI, Nombre, Apellido, UserName, Password, Email, Bloqueado, Activo, Rol FROM Usuario;";
                _sqlserver.Open();
                var reader = _sqlcommand.ExecuteReader();
                var Usuariolst = new List<Usuario>();
                while (reader.Read())
                {
                    Usuariolst.Add(new Usuario(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
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
        public List<Usuario> ListarTodosLosUsuariosDVH()
        {
            try
            {
                _sqlcommand.CommandText = @"SELECT DNI, Nombre, Apellido, UserName, Password, Email, Bloqueado, Activo, Rol, Intentos,DVH FROM Usuario;";
                _sqlserver.Open();
                var reader = _sqlcommand.ExecuteReader();
                var Usuariolst = new List<Usuario>();
                while (reader.Read())
                {
                    Usuario us = new Usuario(
                            reader.GetString(0), //DNI
                            reader.GetString(1), //Nombre
                            reader.GetString(2), //Apellido
                            reader.GetString(3), //UserName
                            reader.GetString(4), //Password
                            reader.GetString(5), //Email
                            reader.GetBoolean(6), //Bloqueado
                            reader.GetBoolean(7), //Activo
                            reader.GetString(8), //Rol
                            reader.GetInt32(9), //Intentos
                            reader.GetString(10) //DVH
                            ); 

                    Usuariolst.Add(us);
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

        public void Modificar(object dni, Usuario UserNew)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Usuario SET DNI = @dninuevo, Nombre = @nombre, Apellido = @apellido, UserName = @username, Email = @email, Rol = @rol
                                            WHERE DNI = @dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", dni);
                _sqlcommand.Parameters.AddWithValue("@dninuevo", UserNew.DNI);
                _sqlcommand.Parameters.AddWithValue("@nombre", UserNew.Nombre);
                _sqlcommand.Parameters.AddWithValue("@apellido", UserNew.Apellido);
                _sqlcommand.Parameters.AddWithValue("@username", UserNew.Username);
                _sqlcommand.Parameters.AddWithValue("@email", UserNew.Email);
                _sqlcommand.Parameters.AddWithValue("@rol", UserNew.Rol);

                _sqlserver.Open();

                _sqlcommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public void DesbloquearUsuario(string dNI, string nuevaClave)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Usuario SET Bloqueado = 0, Password = @password, Intentos = 0 WHERE DNI = @dni;";

                _sqlcommand.Parameters.AddWithValue("@dni", dNI);
                _sqlcommand.Parameters.AddWithValue("@password", nuevaClave);

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

        public void CambiarClave(string usuario, string nuevaContra)
        {
            try
            {
                _sqlcommand.CommandText = "update Usuario set Password = @nuevaPassword where UserName=@usuario;";
                _sqlcommand.Parameters.AddWithValue("@usuario", usuario);
                _sqlcommand.Parameters.AddWithValue("@nuevaPassword", nuevaContra);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public void ActivarUsuario(string dNI)
        {
            try
            {
                _sqlcommand.CommandText = "update Usuario set Activo = 1 where DNI=@dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", dNI);

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

        public void ActualizarDVH(string dNI, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = "update Usuario set DVH =  @dvh WHERE DNI = @dni;";
                _sqlcommand.Parameters.AddWithValue("@dni", dNI);
                _sqlcommand.Parameters.AddWithValue("@dvh", dvhCalculado);

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
