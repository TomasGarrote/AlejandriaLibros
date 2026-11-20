using BE;
using iTextSharp.text;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ClienteHistorialDAL : AbstractDAL<ClienteHistorial>
    {
        public ClienteHistorialDAL() { }

        public List<ClienteHistorial> ListarTodoElHistorial()
        {
            List<ClienteHistorial> lista = new List<ClienteHistorial>();
            try
            {
                _sqlcommand.CommandText = @"SELECT IdHistorial, DNI, Nombre, Apellido, Email, Telefono, 
                                            FechaCambio, Operacion, Usuario, VersionActiva, DVH 
                                            FROM ClienteHistorial 
                                            ORDER BY FechaCambio DESC, IdHistorial DESC";
                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ClienteHistorial
                        {
                            IdHistorial = reader.GetInt32(0),
                            DNI = reader.GetInt32(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                            FechaCambio = reader.GetDateTime(6),
                            Operacion = reader.GetString(7),
                            Usuario = reader.GetString(8),
                            VersionActiva = reader.GetBoolean(9),
                            DVH = reader.GetString(10)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el historial de clientes", ex);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
            return lista;
        }

        public List<ClienteHistorial> FiltrarHistorial(int? dni, string nombre, string apellido, DateTime? desde, DateTime? hasta)
        {
            List<ClienteHistorial> lista = new List<ClienteHistorial>();
            try
            {
                string query = @"SELECT IdHistorial, DNI, Nombre, Apellido, Email, Telefono, 
                                 FechaCambio, Operacion, Usuario, VersionActiva, DVH 
                                 FROM ClienteHistorial 
                                 WHERE 1=1";

                _sqlcommand.Parameters.Clear();

                if (dni.HasValue && dni.Value > 0)
                {
                    query += " AND CAST(DNI AS VARCHAR(50)) LIKE @DNI";
                    _sqlcommand.Parameters.AddWithValue("@DNI", "%" + dni.Value + "%");
                }
                if (!string.IsNullOrEmpty(nombre))
                {
                    query += " AND Nombre LIKE @Nombre";
                    _sqlcommand.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");
                }
                if (!string.IsNullOrEmpty(apellido))
                {
                    query += " AND Apellido LIKE @Apellido";
                    _sqlcommand.Parameters.AddWithValue("@Apellido", "%" + apellido + "%");
                }
                if (desde.HasValue && hasta.HasValue)
                {
                    query += " AND FechaCambio BETWEEN @Desde AND @Hasta";
                    _sqlcommand.Parameters.AddWithValue("@Desde", desde.Value.Date);
                    _sqlcommand.Parameters.AddWithValue("@Hasta", hasta.Value.Date.AddDays(1).AddTicks(-1));
                }

                query += " ORDER BY FechaCambio DESC, IdHistorial DESC";

                _sqlcommand.CommandText = query;
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ClienteHistorial
                        {
                            IdHistorial = reader.GetInt32(0),
                            DNI = reader.GetInt32(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                            FechaCambio = reader.GetDateTime(6),
                            Operacion = reader.GetString(7),
                            Usuario = reader.GetString(8),
                            VersionActiva = reader.GetBoolean(9),
                            DVH = reader.GetString(10)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar el historial de clientes", ex);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
            return lista;
        }

        public List<ClienteHistorial> ObtenerHistorialPorCliente(int dni)
        {
            return FiltrarHistorial(dni, null, null, null, null);
        }

        public void ActualizarDVH(int idHistorial, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE ClienteHistorial SET DVH = @DVH WHERE IdHistorial = @IdHistorial";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DVH", dvhCalculado);
                _sqlcommand.Parameters.AddWithValue("@IdHistorial", idHistorial);
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el DVH en ClienteHistorial", ex);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public ClienteHistorial ObtenerVersionHistorial(int idVersion)
        {
            try
            {
                _sqlcommand.CommandText = @"SELECT IdHistorial, DNI, Nombre, Apellido, Email, Telefono, 
                                 FechaCambio, Operacion, Usuario, VersionActiva, DVH 
                                 FROM ClienteHistorial 
                                 WHERE IdHistorial = @idVersion";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@idVersion", idVersion);
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    ClienteHistorial clienteHistorial = new ClienteHistorial();
                    while (reader.Read())
                    {
                        clienteHistorial.IdHistorial = reader.GetInt32(0);
                        clienteHistorial.DNI = reader.GetInt32(1);
                        clienteHistorial.Nombre = reader.GetString(2);
                        clienteHistorial.Apellido = reader.GetString(3);
                        clienteHistorial.Email = reader.IsDBNull(4) ? null : reader.GetString(4);
                        clienteHistorial.Telefono = reader.IsDBNull(5) ? null : reader.GetString(5);
                        clienteHistorial.FechaCambio = reader.GetDateTime(6);
                        clienteHistorial.Operacion = reader.GetString(7);
                        clienteHistorial.Usuario = reader.GetString(8);
                        clienteHistorial.VersionActiva = reader.GetBoolean(9);
                        clienteHistorial.DVH = reader.GetString(10);
                    }
                    return clienteHistorial;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la versión específica del historial de clientes", ex);
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
    }
}
