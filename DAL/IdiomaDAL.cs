using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class IdiomaDAL : AbstractDAL<IdiomaDAL>
    {
        public string ObtenerIdioma(string userName)
        {
            try
            {
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = @"
                    SELECT CodigoIdioma
                    FROM Idioma
                    WHERE UserName = @UserName";

                _sqlcommand.Parameters.AddWithValue(
                    "@UserName",
                    userName);

                _sqlserver.Open();

                object resultado =
                    _sqlcommand.ExecuteScalar();

                if (resultado == null)
                    return "es";

                return resultado.ToString();
            }
            finally
            {
                if (_sqlserver.State ==
                    System.Data.ConnectionState.Open)
                {
                    _sqlserver.Close();
                }

                _sqlcommand.Parameters.Clear();
            }
        }

        public void GuardarIdioma(
            string userName,
            string codigoIdioma)
        {
            try
            {
                _sqlcommand.Parameters.Clear();

                _sqlcommand.CommandText = @"
                IF EXISTS
                (
                    SELECT 1
                    FROM Idioma
                    WHERE UserName = @UserName
                )
                BEGIN
                    UPDATE Idioma
                    SET CodigoIdioma = @CodigoIdioma
                    WHERE UserName = @UserName
                END
                ELSE
                BEGIN
                    INSERT INTO Idioma
                    (
                        UserName,
                        CodigoIdioma
                    )
                    VALUES
                    (
                        @UserName,
                        @CodigoIdioma
                    )
                END";

                _sqlcommand.Parameters.AddWithValue(
                    "@UserName",
                    userName);

                _sqlcommand.Parameters.AddWithValue(
                    "@CodigoIdioma",
                    codigoIdioma);

                _sqlserver.Open();

                _sqlcommand.ExecuteNonQuery();
            }
            finally
            {
                if (_sqlserver.State ==
                    System.Data.ConnectionState.Open)
                {
                    _sqlserver.Close();
                }

                _sqlcommand.Parameters.Clear();
            }
        }
    }
}
