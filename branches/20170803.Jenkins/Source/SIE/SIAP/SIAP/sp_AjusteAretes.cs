using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_AjusteAretes(SqlInt32 organizacionId, SqlDateTime fechaSacrificio, SqlInt32 longitud)
    {
        try
        {
            new AjusteAretes((m) => SqlContext.Pipe.Send(m), longitud.Value)
            .Ejecutar(organizacionId.Value, fechaSacrificio.Value);
        }
        catch (Exception ex)
        {
            EnviarError_BD(ex);
            ex = null;
        }
    }

    static void EnviarError_BD(Exception ex)
    {
        if (SqlContext.IsAvailable)
        {
            using (SqlConnection conn = new SqlConnection(Querys.Conexion))
            {
                conn.Open();
                var query = string.Format(Querys.Error, ex.Message);
                SqlCommand comm = new SqlCommand(query, conn);
                SqlContext.Pipe.ExecuteAndSend(comm);
                conn.Close();
            }
        }
        else
        {
            throw ex;
        }
    }
}
