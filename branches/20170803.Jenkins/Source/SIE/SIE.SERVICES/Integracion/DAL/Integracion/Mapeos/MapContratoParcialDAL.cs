using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapContratoParcialDAL
    {
        /// <summary>
        /// Obtiene una lista de parcialidades de un contrato
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoParcialInfo> ObtenerPorContratoId(DataSet ds)
        {
            List<ContratoParcialInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ContratoParcialInfo()
                         {
                             ContratoParcialId = info.Field<int>("ContratoParcialID"),
                             ContratoId = info.Field<int>("ContratoID"),
                             Cantidad = info.Field<int>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
                             TipoCambio = new TipoCambioInfo(){TipoCambioId = info.Field<int>("TipoCambioID"), Descripcion = info.Field<string>("Descripcion"),Cambio = info.Field<decimal>("Cambio")},
                             FechaCreacion = info.Field<DateTime>("FechaCreacion")
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de contratos parciales con faltante
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoParcialInfo> ObtenerFaltantePorContratoId(DataSet ds)
        {
            List<ContratoParcialInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ContratoParcialInfo()
                         {
                             ContratoParcialId = info.Field<int>("ContratoParcialID"),
                             ContratoId = info.Field<int>("ContratoID"),
                             Cantidad = info.Field<int>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
                             ImporteConvertido = info.Field<decimal>("ImporteConvertido"),
                             TipoCambio = new TipoCambioInfo() { TipoCambioId = info.Field<int>("TipoCambioID"), Descripcion = info.Field<string>("Descripcion"), Cambio = info.Field<decimal>("Cambio") },
                             CantidadRestante = info.Field<int>("CantidadRestante"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
