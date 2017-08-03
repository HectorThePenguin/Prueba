using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapRecibirProductoAlmacenReplicaDAL
    {
        internal static List<string> Guardar(DataSet ds)
        {
            var lista = new List<string>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                lista.AddRange(Enumerable.Select(dt.AsEnumerable(), row => row[0].ToString()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<string> Consultar(DataSet ds)
        {
            var lista = new List<string>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                lista.AddRange(Enumerable.Select(dt.AsEnumerable(), row => row[0].ToString()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
    }
}
