using BE;
using iTextSharp.text.pdf.codec.wmf;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class PrestamosDAL : AbstractDAL<Prestamo>
    {
        public List<Prestamo> ListarTodosLosPrestamosDVH()
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            try
            {
                _sqlcommand.CommandText = @"
                    SELECT p.* FROM Prestamo AS p";

                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prestamo prestamo = new Prestamo
                        {
                            NroPrestamo = Convert.ToInt32(reader["NroPrestamo"]),
                            Estado = reader["Estado"].ToString(),
                            CodigoInterno = reader["CodigoInterno"].ToString(),
                            FechaProgramada = Convert.ToDateTime(reader["FechaProgramada"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            FechaDevolucionReal = reader["FechaDevolucionReal"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["FechaDevolucionReal"]),
                            Dni_Cliente = Convert.ToInt32(reader["Dni_Cliente"]),
                            Dni_Usuario = Convert.ToInt32(reader["Dni_Usuario"]),
                            DVH = reader["DVH"].ToString()
                        };
                        prestamos.Add(prestamo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos del cliente: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
            return prestamos;
        }

        
        public void RegistrarPrestamo(Prestamo prestamo)
        {
            try
            {
                _sqlcommand.CommandText = "INSERT INTO Prestamo (Estado, CodigoInterno, FechaProgramada, FechaEmision, Dni_Cliente, Dni_Usuario) " +
                                          "VALUES (@Estado, @CodigoInterno, @FechaProgramada, @FechaEmision, @Dni_Cliente, @Dni_Usuario)";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Estado", prestamo.Estado);
                _sqlcommand.Parameters.AddWithValue("@CodigoInterno", prestamo.CodigoInterno);
                _sqlcommand.Parameters.AddWithValue("@FechaProgramada", prestamo.FechaProgramada);
                _sqlcommand.Parameters.AddWithValue("@FechaEmision", prestamo.FechaEmision);
                _sqlcommand.Parameters.AddWithValue("@Dni_Cliente", prestamo.Dni_Cliente);
                _sqlcommand.Parameters.AddWithValue("@Dni_Usuario", prestamo.Dni_Usuario);
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el préstamo: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
        }

        public void RegistrarDevolucion(Prestamo prestamo)
        {
            try
            {
                _sqlcommand.CommandText = "UPDATE Prestamo SET Estado = @Estado, FechaDevolucionReal = @FechaDevolucionReal WHERE NroPrestamo = @NroPrestamo";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Estado", prestamo.Estado);
                _sqlcommand.Parameters.AddWithValue("@FechaDevolucionReal", prestamo.FechaDevolucionReal);
                _sqlcommand.Parameters.AddWithValue("@NroPrestamo", prestamo.NroPrestamo);
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la devolución: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
        }

        public List<Prestamo> ListarPrestamos(string dniCliente, string estado, string titulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            try
            {
                _sqlcommand.Parameters.Clear();
                var filtros = new List<string>();

                if (!string.IsNullOrWhiteSpace(dniCliente))
                {
                    filtros.Add("Dni_Cliente = @Dni_Cliente");
                    _sqlcommand.Parameters.AddWithValue("@Dni_Cliente", dniCliente);
                }

                if (!string.IsNullOrWhiteSpace(estado))
                {
                    filtros.Add("Estado = @estado");
                    _sqlcommand.Parameters.AddWithValue("@estado", estado);
                }


                if (!string.IsNullOrWhiteSpace(titulo))
                {
                    filtros.Add("T.Titulo LIKE @Titulo)");

                    _sqlcommand.Parameters.AddWithValue("@Titulo", "%" + titulo + "%");
                }

                if (fechaInicial != DateTime.MinValue && fechaFinal != DateTime.MinValue)
                {
                    filtros.Add("FechaEmision BETWEEN @FechaInicial AND @FechaFinal");

                    _sqlcommand.Parameters.AddWithValue("@FechaInicial", fechaInicial);
                    _sqlcommand.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                }

                string query = "SELECT p.*, l.Titulo AS TituloLibro FROM Prestamo P INNER JOIN Libro L ON L.CodigoInterno = P.CodigoInterno ";

                if (filtros.Count > 0)
                {
                    query += " WHERE " + string.Join(" AND ", filtros);
                }

                _sqlcommand.CommandText = query;
                _sqlserver.Open();


                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prestamo prestamo = new Prestamo
                        {
                            NroPrestamo = Convert.ToInt32(reader["NroPrestamo"]),
                            Estado = reader["Estado"].ToString(),
                            CodigoInterno = reader["CodigoInterno"].ToString(),
                            FechaProgramada = Convert.ToDateTime(reader["FechaProgramada"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            FechaDevolucionReal = reader["FechaDevolucionReal"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["FechaDevolucionReal"]),
                            Dni_Cliente = Convert.ToInt32(reader["Dni_Cliente"]),
                            Dni_Usuario = Convert.ToInt32(reader["Dni_Usuario"]),
                            DVH = reader["DVH"].ToString()
                        };
                        prestamos.Add(prestamo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
            return prestamos;
        }
        
        public List<Prestamo> ListarPrestamosCliente(int dniCliente)
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            try
            {
                _sqlcommand.CommandText = @"
                    SELECT p.*, l.Titulo AS TituloLibro
                    FROM Prestamo AS p
                    INNER JOIN Libro AS l ON l.CodigoInterno = p.CodigoInterno
                    WHERE p.Dni_Cliente = @Dni_Cliente
                      AND l.Titulo LIKE @Titulo";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Dni_Cliente", dniCliente);
                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prestamo prestamo = new Prestamo
                        {
                            NroPrestamo = Convert.ToInt32(reader["NroPrestamo"]),
                            Estado = reader["Estado"].ToString(),
                            CodigoInterno = reader["CodigoInterno"].ToString(),
                            FechaProgramada = Convert.ToDateTime(reader["FechaProgramada"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            FechaDevolucionReal = reader["FechaDevolucionReal"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["FechaDevolucionReal"]),
                            Dni_Cliente = Convert.ToInt32(reader["Dni_Cliente"]),
                            Dni_Usuario = Convert.ToInt32(reader["Dni_Usuario"]),
                            DVH = reader["DVH"].ToString()
                        };
                        prestamos.Add(prestamo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos del cliente: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
            return prestamos;
        }

        public Prestamo RetornarPrestamo(int nroPrestamo)
        {
            Prestamo prestamo = null;
            try
            {
                _sqlcommand.CommandText = @"
                    SELECT p.*, l.Titulo AS TituloLibro
                    FROM Prestamo AS p
                    INNER JOIN Libro AS l ON l.CodigoInterno = p.CodigoInterno
                    WHERE p.NroPrestamo = @NroPrestamo";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@NroPrestamo", nroPrestamo);
                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        prestamo = new Prestamo
                        {
                            NroPrestamo = Convert.ToInt32(reader["NroPrestamo"]),
                            Estado = reader["Estado"].ToString(),
                            CodigoInterno = reader["CodigoInterno"].ToString(),
                            FechaProgramada = Convert.ToDateTime(reader["FechaProgramada"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            FechaDevolucionReal = reader["FechaDevolucionReal"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["FechaDevolucionReal"]),
                            Dni_Cliente = Convert.ToInt32(reader["Dni_Cliente"]),
                            Dni_Usuario = Convert.ToInt32(reader["Dni_Usuario"]),
                            DVH = reader["DVH"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al retornar el préstamo: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
            return prestamo;
        }

        public void ActualizarPrestamoDVH(int nroPrestamo, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Prestamo SET DVH = @DVH WHERE NroPrestamo = @NroPrestamo";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DVH", dvhCalculado);
                _sqlcommand.Parameters.AddWithValue("@NroPrestamo", nroPrestamo);
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el DVH del préstamo: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
        }

        public List<Prestamo> ListarPrestamosPendientes(string estado)
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            try
            {
                _sqlcommand.CommandText = @"
                    SELECT p.*, l.Titulo AS TituloLibro
                    FROM Prestamo AS p
                    INNER JOIN Libro AS l ON l.CodigoInterno = p.CodigoInterno
                    WHERE p.Estado = @Estado";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@Estado", estado);

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prestamo prestamo = new Prestamo
                        {
                            NroPrestamo = Convert.ToInt32(reader["NroPrestamo"]),
                            Estado = reader["Estado"].ToString(),
                            CodigoInterno = reader["CodigoInterno"].ToString(),
                            FechaProgramada = Convert.ToDateTime(reader["FechaProgramada"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            FechaDevolucionReal = reader["FechaDevolucionReal"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["FechaDevolucionReal"]),
                            Dni_Cliente = Convert.ToInt32(reader["Dni_Cliente"]),
                            Dni_Usuario = Convert.ToInt32(reader["Dni_Usuario"]),
                            DVH = reader["DVH"].ToString()
                        };
                        prestamos.Add(prestamo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos pendientes: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
            return prestamos;
        }

        public List<Prestamo> ListarTodosLosPrestamos()
        {
            List<Prestamo> prestamos = new List<Prestamo>();
            try
            {
                _sqlcommand.CommandText = @"
                    SELECT *
                    FROM Prestamo";
                _sqlcommand.Parameters.Clear();

                _sqlserver.Open();
                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prestamo prestamo = new Prestamo
                        {
                            NroPrestamo = Convert.ToInt32(reader["NroPrestamo"]),
                            Estado = reader["Estado"].ToString(),
                            CodigoInterno = reader["CodigoInterno"].ToString(),
                            FechaProgramada = Convert.ToDateTime(reader["FechaProgramada"]),
                            FechaEmision = Convert.ToDateTime(reader["FechaEmision"]),
                            FechaDevolucionReal = reader["FechaDevolucionReal"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["FechaDevolucionReal"]),
                            Dni_Cliente = Convert.ToInt32(reader["Dni_Cliente"]),
                            Dni_Usuario = Convert.ToInt32(reader["Dni_Usuario"]),
                        };
                        prestamos.Add(prestamo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los préstamos pendientes: " + ex.Message);
            }
            finally
            {
                if (_sqlserver.State == System.Data.ConnectionState.Open)
                    _sqlserver.Close();
            }
            return prestamos;
        }

        public object[] ListarEstados()
        {
            List<string> estados = new List<string>();
            try
            {
                string query = @"SELECT DISTINCT Estado FROM Prestamo ORDER BY Estado";
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        estados.Add(reader["Estado"].ToString());
                    }
                }

                return estados.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
    }
}
