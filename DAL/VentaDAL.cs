using BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class VentaDAL : AbstractDAL<Venta>
    {
        public int RegistrarVenta(Venta venta)
        {
            try
            {
                int nroVenta;
                _sqlcommand.CommandText = @"INSERT INTO Venta (Fecha, DniUsuario, MedioPago, Subtotal, Descuento, Total)
                        OUTPUT INSERTED.NroVenta
                        VALUES (@fecha, @dniEmp, @medioPago, @subtotal, @desc, @total)";
                
                _sqlcommand.Parameters.AddWithValue("@fecha", venta.Fecha);
                _sqlcommand.Parameters.AddWithValue("@dniEmp", venta.DniUsuario);
                _sqlcommand.Parameters.AddWithValue("@medioPago", venta.MedioDePago);
                _sqlcommand.Parameters.AddWithValue("@subtotal", venta.Subtotal);
                _sqlcommand.Parameters.AddWithValue("@desc", venta.Descuento);
                _sqlcommand.Parameters.AddWithValue("@total", venta.Total);

                _sqlserver.Open();
                nroVenta = (int)_sqlcommand.ExecuteScalar();
                

                foreach (var d in venta.Detalle)
                {
                    _sqlcommand.CommandText = @"INSERT INTO DetalleVenta (NroVenta, CodigoLibro, Cantidad, PrecioUnitario, Subtotal)
                            VALUES (@nro, @cod, @cant, @precio, @sub)";
                    
                    _sqlcommand.Parameters.AddWithValue("@nro", nroVenta);
                    _sqlcommand.Parameters.AddWithValue("@cod", d.CodigoLibro);
                    _sqlcommand.Parameters.AddWithValue("@cant", d.Cantidad);
                    _sqlcommand.Parameters.AddWithValue("@precio", d.PrecioUnitario);
                    _sqlcommand.Parameters.AddWithValue("@sub", d.Subtotal);
                    _sqlcommand.ExecuteNonQuery();
                    
                }

                return nroVenta;
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
        
        public List<Venta> ListarTodasLasVentas()
        {
            List<Venta> ventas = new List<Venta>();
            try
            {
                _sqlcommand.CommandText = @"SELECT v.NroVenta, v.Fecha, v.DniUsuario, v.MedioPago, v.Subtotal, v.Descuento, v.Total
                      FROM Venta v";
                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ventas.Add(new Venta
                        {
                            NroVenta = reader.GetInt32(0),
                            Fecha = reader.GetDateTime(1),
                            DniUsuario = reader.GetString(2),
                            MedioDePago = reader.GetString(3),
                            Subtotal = reader.GetDecimal(4),
                            Descuento = reader.GetDecimal(5)
                        });
                    }
                }
                
                return ventas;
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

        public List<DetalleVenta> ListarTodosLosDetalles()
        {
            List<DetalleVenta> detalles = new List<DetalleVenta>();
            try
            {

                _sqlcommand.CommandText = @"SELECT dv.NroVenta, dv.CodigoLibro, l.Titulo, dv.Cantidad, dv.PrecioUnitario, dv.Subtotal
                      FROM DetalleVenta dv JOIN Libro l ON dv.CodigoLibro = l.CodigoInterno";
                _sqlcommand.Parameters.Clear();
                _sqlserver.Open();

                using (var reader = _sqlcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new DetalleVenta
                        {
                            NroVenta = reader.GetInt32(0),
                            CodigoLibro = reader.GetInt32(1),
                            TituloLibro = reader.GetString(2),
                            Cantidad = reader.GetInt32(3),
                            PrecioUnitario = reader.GetDecimal(4)
                        });
                    }
                }

                return detalles;
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

        public void ActualizarVentaDVH(string nroVenta, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE Venta SET DVH = @DVH WHERE NroVenta = @nroVenta";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DVH", dvhCalculado);
                _sqlcommand.Parameters.AddWithValue("@nroVenta", nroVenta);
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

        public void ActualizarDetalleDVH(string nroVenta, string dvhCalculado)
        {
            try
            {
                _sqlcommand.CommandText = @"UPDATE DetalleVenta SET DVH = @DVH WHERE NroVenta = @nroVenta";
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Parameters.AddWithValue("@DVH", dvhCalculado);
                _sqlcommand.Parameters.AddWithValue("@nroVenta", nroVenta);
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
