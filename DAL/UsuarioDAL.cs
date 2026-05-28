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
        public UsuarioDAL() : base()
        {
            
        }

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

        public UsuarioBE ObtenerPorUserName(string usuario)
        {
            throw new NotImplementedException();
        }

        public void ResetearIntentos(UsuarioBE usuario)
        {
            throw new NotImplementedException();
        }

        public void SumarIntentoFallido(UsuarioBE usuario)
        {
            throw new NotImplementedException();
        }
    }
}
