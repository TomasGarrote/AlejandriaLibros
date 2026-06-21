using Microsoft.Data.SqlClient;
using Servicios;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class BitacoraDAL : AbstractDAL<Bitacora>
    {
        public BitacoraDAL() { }

        public void RegistrarEvento(Bitacora unEvento)
        {
            try
            {
                _sqlcommand.CommandText = @"INSERT INTO Bitacora (Login, Fecha, Modulo, Evento, Criticidad) 
                           VALUES (@Login, @Fecha, @Modulo, @Evento, @Criticidad)";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Login", unEvento.Login);
                _sqlcommand.Parameters.AddWithValue("@Fecha", unEvento.Fecha);
                _sqlcommand.Parameters.AddWithValue("@Modulo", unEvento.Modulo);
                _sqlcommand.Parameters.AddWithValue("@Evento", unEvento.Evento);
                _sqlcommand.Parameters.AddWithValue("@Criticidad", unEvento.Criticidad);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el evento en la bitácora.", ex);
            }
            finally
            {
                _sqlserver.Close();
            }
        }


        public List<Bitacora> FiltrarEventos(string nombre, string apellido, string login, string modulo, string evento, DateTime desde, DateTime hasta, int? criticidad)
        {
            List<Bitacora> lista = new List<Bitacora>();
            try
            {
                string query = @"SELECT b.Id_Evento, b.Login, b.Fecha, b.Modulo, 
                                 b.Evento, b.Criticidad
                                 FROM Bitacora b 
                                 INNER JOIN Usuario u ON b.Login = u.UserName
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
                    query += " AND b.Login = @Login";
                    _sqlcommand.Parameters.AddWithValue("@Login", login);
                }
                if (!string.IsNullOrEmpty(modulo))
                {
                    query += " AND b.Modulo = @Modulo";
                    _sqlcommand.Parameters.AddWithValue("@Modulo", modulo);
                }
                if (!string.IsNullOrEmpty(evento))
                {
                    query += " AND b.Evento LIKE @Evento";
                    _sqlcommand.Parameters.AddWithValue("@Evento", "%" + evento + "%");
                }
                if (criticidad.HasValue)
                {
                    query += " AND b.Criticidad = @Criticidad";
                    _sqlcommand.Parameters.AddWithValue("@Criticidad", criticidad.Value);
                }

                query += " AND b.Fecha BETWEEN @Desde AND @Hasta";
                _sqlcommand.Parameters.AddWithValue("@Desde", desde.Date);
                _sqlcommand.Parameters.AddWithValue("@Hasta", hasta.Date.AddDays(1).AddTicks(-1));

                query += " ORDER BY b.Fecha DESC, b.Id_Evento DESC";

                _sqlcommand.CommandText = query;
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Bitacora
                        {
                            Id_Evento = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Modulo = reader.GetString(3),
                            Evento = reader.GetString(4),
                            Criticidad = reader.GetInt32(5)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar los eventos.", ex);
            }
            finally
            {
                _sqlserver.Close();
            }
            return lista;
        }

        public List<string> ObtenerLogins()
        {
            List<string> logins = new List<string>();
            try
            {
                _sqlcommand.CommandText = "SELECT DISTINCT UserName FROM Usuario ORDER BY UserName";
                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                    while (reader.Read())
                        logins.Add(reader.GetString(0));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los logins.", ex);
            }
            finally
            {
                _sqlserver.Close();
            }
            return logins;
        }

        public Usuario ObtenerUsuarioPorLogin(string login)
        {
            Usuario usuario = null;
            try
            {
                _sqlcommand.CommandText = @"SELECT DNI, Nombre, Apellido, UserName 
                                            FROM Usuario 
                                            WHERE UserName = @Login";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Login", login);
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            DNI = reader.GetString(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Username = reader.GetString(3)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario.", ex);
            }
            finally
            {
                _sqlserver.Close();
            }
            return usuario;
        }
    }
}