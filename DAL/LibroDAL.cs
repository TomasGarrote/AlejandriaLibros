using BE;
using iTextSharp.text;
using Microsoft.Data.SqlClient;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class LibroDAL : AbstractDAL<Libro>
    {
        // CU-001: Visualizar Catálogo
        public List<Libro> ListarDisponibles()
        {
            List<Libro> lista = new List<Libro>();
            try
            {
                string query = @"SELECT CodigoInterno, ISBN, Titulo, Autor, Categoria, Ubicacion, Precio, StockDisponible
                      FROM Libro
                      WHERE StockDisponible > @stock AND Activo = @activo   
                      ORDER BY Titulo";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@stock", 0);
                _sqlcommand.Parameters.AddWithValue("@activo", 1);

                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Libro
                        {
                            CodigoInterno = Convert.ToInt32(reader["CodigoInterno"]),
                            ISBN = reader["ISBN"].ToString(),
                            Titulo = reader["Titulo"].ToString(),
                            Autor = reader["Autor"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            StockDisponible = (int)reader["StockDisponible"]
                        });
                    }
                }
                
                return lista;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public Libro ConsultarLibro(int codigoInterno)
        {
            try
            {
                Libro libro = new Libro();

                string query = "SELECT TOP 1 * FROM Libro WHERE CodigoInterno = @cod";
                {
                    _sqlcommand.Parameters.AddWithValue("@cod", codigoInterno);
                    _sqlcommand.CommandText = query;
                    _sqlserver.Open();

                    using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            libro.CodigoInterno = Convert.ToInt32(reader["CodigoInterno"]);
                            libro.ISBN = reader["ISBN"].ToString();
                            libro.Titulo = reader["Titulo"].ToString();
                            libro.Autor = reader["Autor"].ToString();
                            libro.Categoria = reader["Categoria"].ToString();
                            libro.Ubicacion = reader["Ubicacion"].ToString();
                            libro.Precio = (decimal)reader["Precio"];
                            libro.StockDisponible = (int)reader["StockDisponible"];
                            
                        }
                    }

                    return libro;


                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int ConsultarStock(int codigoInterno)
        {
            try
            {
                string query = "SELECT StockDisponible FROM Libro WHERE CodigoInterno = @cod";
                {
                    _sqlcommand.Parameters.AddWithValue("@cod", codigoInterno);
                    _sqlcommand.CommandText = query;
                    _sqlserver.Open();
                    return (int)_sqlcommand.ExecuteScalar();
                }
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

        public void DescontarStock(int codigoInterno, int cantidad)
        {
            try
            {
                string query = "UPDATE Libro SET StockDisponible = StockDisponible - @cant WHERE CodigoInterno = @cod";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@cant", cantidad);
                _sqlcommand.Parameters.AddWithValue("@cod", codigoInterno);
                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
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

        public List<Libro> ListarLibrosPorFiltro(string categoria, string autor, string titulo, string isbn, string ubicacion)
        {
            List<Libro> lista = new List<Libro>();
            try
            {
                string query = @"SELECT CodigoInterno, ISBN, Titulo, Autor, Categoria, Ubicacion, Precio, StockDisponible
                      FROM Libro l
                      WHERE StockDisponible > @stock AND Activo = @activo";

                _sqlcommand.Parameters.AddWithValue("@stock", 0);
                _sqlcommand.Parameters.AddWithValue("@activo", 1);

                if (!string.IsNullOrEmpty(categoria))
                {
                    query += " AND l.Categoria LIKE @Categoria";
                    _sqlcommand.Parameters.AddWithValue("@Categoria", "%" + categoria + "%");
                }

                if (!string.IsNullOrEmpty(autor))
                {
                    query += " AND l.Autor LIKE @Autor";
                    _sqlcommand.Parameters.AddWithValue("@Autor", "%" + autor + "%");
                }

                if (!string.IsNullOrEmpty(titulo))
                {
                    query += " AND l.Titulo LIKE @Titulo";
                    _sqlcommand.Parameters.AddWithValue("@Titulo", "%" + titulo + "%");
                }

                if (!string.IsNullOrEmpty(isbn))
                {
                    query += " AND l.ISBN LIKE @ISBN";
                    _sqlcommand.Parameters.AddWithValue("@ISBN", "%" + isbn + "%");
                }

                if (!string.IsNullOrEmpty(ubicacion))
                {
                    query += " AND l.Ubicacion LIKE @Ubicacion";
                    _sqlcommand.Parameters.AddWithValue("@Ubicacion", "%" + ubicacion + "%");
                }

                query += " ORDER BY l.Titulo";

                _sqlcommand.CommandText = query;
                _sqlserver.Open();

                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Libro
                        {
                            CodigoInterno = Convert.ToInt32(reader["CodigoInterno"]),
                            ISBN = reader["ISBN"].ToString(),
                            Titulo = reader["Titulo"].ToString(),
                            Autor = reader["Autor"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            StockDisponible = (int)reader["StockDisponible"]
                        });
                    }
                }

                return lista;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public List<Libro> ListarTodosLosLibrosDVH()
        {
            List<Libro> lista = new List<Libro>();
            try
            {
                string query = @"SELECT CodigoInterno, ISBN, Titulo, Autor, Categoria, Ubicacion, Precio, StockDisponible, Activo, DVH
                      FROM Libro";
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Libro
                        {
                            CodigoInterno = Convert.ToInt32(reader["CodigoInterno"]),
                            ISBN = reader["ISBN"].ToString(),
                            Titulo = reader["Titulo"].ToString(),
                            Autor = reader["Autor"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            StockDisponible = (int)reader["StockDisponible"],
                            Activo = Convert.ToInt32(reader["Activo"]),
                            DVH = reader["DVH"].ToString()
                        });
                    }
                }

                return lista;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public List<Libro> ListarTodosLosLibros()
        {
            List<Libro> lista = new List<Libro>();
            try
            {
                string query = @"SELECT CodigoInterno, ISBN, Titulo, Autor, Categoria, Ubicacion, Precio, StockDisponible, Activo
                      FROM Libro";
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Libro
                        {
                            CodigoInterno = Convert.ToInt32(reader["CodigoInterno"]),
                            ISBN = reader["ISBN"].ToString(),
                            Titulo = reader["Titulo"].ToString(),
                            Autor = reader["Autor"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            Ubicacion = reader["Ubicacion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            StockDisponible = (int)reader["StockDisponible"],
                            Activo = Convert.ToInt32(reader["Activo"])
                        });
                    }
                }

                return lista;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
        public object [] ListarCategorias()
        {
            List<string> categorias = new List<string>();
            try
            {
                string query = @"SELECT DISTINCT Categoria FROM Libro ORDER BY Categoria";
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(reader["Categoria"].ToString());
                    }
                }

                return categorias.ToArray();
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
        public object [] ListarAutores()
        {
            List<string> autores = new List<string>();
            try
            {
                string query = @"SELECT DISTINCT Autor FROM Libro ORDER BY Autor";
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                using (SqlDataReader reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        autores.Add(reader["Autor"].ToString());
                    }
                }

                return autores.ToArray();
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

        public void ActualizarDVH(string codInterno, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Libro SET DVH = @DVH WHERE CodigoInterno = @codInter";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DVH", dvhCalculado);
                _sqlcommand.Parameters.AddWithValue("@codInter", codInterno);
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

        public void SumarStock(int codigoInterno, int cantidad)
        {
            try
            {
                string query = "UPDATE Libro SET StockDisponible = StockDisponible + @cant WHERE CodigoInterno = @cod";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@cant", cantidad);
                _sqlcommand.Parameters.AddWithValue("@cod", codigoInterno);
                _sqlcommand.CommandText = query;
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
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
