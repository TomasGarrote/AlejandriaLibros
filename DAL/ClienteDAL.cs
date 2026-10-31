using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public class ClienteDAL : AbstractDAL<Cliente>
    {
        public void RegistrarCliente(Cliente cliente)
        {
            try
            {
                _sqlcommand.CommandText = @"INSERT INTO Cliente (Dni, Nombre, Apellido, Telefono, Email, Activo)
                        VALUES (@dni, @nombre, @apellido, @telefono, @email, @activo)";

                _sqlcommand.Parameters.AddWithValue("@dni", cliente.DNI);
                _sqlcommand.Parameters.AddWithValue("@nombre", cliente.Nombre);
                _sqlcommand.Parameters.AddWithValue("@apellido", cliente.Apellido);
                _sqlcommand.Parameters.AddWithValue("@telefono", cliente.Telefono);
                _sqlcommand.Parameters.AddWithValue("@email", cliente.Email);
                _sqlcommand.Parameters.AddWithValue("@activo", cliente.Activo);

                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
        public void ModificarCliente(Cliente cliente)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Cliente
                                            SET Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Email = @email, Activo = @activo
                                            WHERE Dni = @dni";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@dni", cliente.DNI);
                _sqlcommand.Parameters.AddWithValue("@nombre", cliente.Nombre);
                _sqlcommand.Parameters.AddWithValue("@apellido", cliente.Apellido);
                _sqlcommand.Parameters.AddWithValue("@telefono", cliente.Telefono);
                _sqlcommand.Parameters.AddWithValue("@email", cliente.Email);
                _sqlcommand.Parameters.AddWithValue("@activo", cliente.Activo);
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
        public void BorrarCliente(string Dni)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Cliente SET Activo = 0 WHERE Dni = @dni";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@dni", Dni);
                _sqlserver.Open();
                _sqlcommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlserver.Close();
            }
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                _sqlcommand.CommandText = @"SELECT c.Dni, c.Nombre, c.Apellido, c.Telefono, c.Email, c.Activo
                      FROM Cliente c WHERE c.Activo = 1";
                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            DNI = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            Email = reader.GetString(4),
                            Activo = reader.GetInt32(5)
                        });
                    }
                }

                return clientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
        public List<Cliente> ListarClientesDVH()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                _sqlcommand.CommandText = @"SELECT c.Dni, c.Nombre, c.Apellido, c.Telefono, c.Email, c.Activo, c.DVH
                      FROM Cliente c";
                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            DNI = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            Email = reader.GetString(4),
                            Activo = reader.GetInt32(5),
                            DVH = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                        });
                    }
                }

                return clientes;
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }
        public Cliente RetornarCliente(string DNI)
        {
            try
            {
                _sqlcommand.CommandText = @"SELECT c.Dni, c.Nombre, c.Apellido, c.Telefono, c.Email, c.Activo, c.DVH
                      FROM Cliente c
                      WHERE c.Dni = @dni";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@dni", DNI);
                _sqlserver.Open();

                using (var reader = _sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cliente
                        {
                            DNI = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            Email = reader.GetString(4),
                            Activo = reader.GetInt32(5),
                            DVH = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                        };
                    }
                }

                return null;
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlcommand.Parameters.Clear();
                _sqlserver.Close();
            }
        }

        public void ActualizarDVH(int dni, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Cliente SET DVH = @DVH WHERE DNI = @dni";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DVH", dvhCalculado);
                _sqlcommand.Parameters.AddWithValue("@dni", dni);
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
