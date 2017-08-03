using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapChequeraDAL
    {
        internal static List<ChequeraInfo> ObtenerTodos(DataSet ds)
        {
            List<ChequeraInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ChequeraInfo
                         {
                             ChequeraId = info.Field<int>("ChequeraId"),
                             DivisionDescripcion = info.Field<string>("DivisionDescripcion"),
                             DivisionId = info.Field<int>("DivisionId"),
                             CentroAcopioId = info.Field<int>("CentroAcopioId"),
                             CentroAcopioDescripcion = info.Field<string>("CentroAcopioDescripcion"),
                             NumeroChequera = info.Field<string>("NumeroChequera"),
                             BancoId = info.Field<int>("BancoId"),
                             BancoDescripcion = info.Field<string>("BancoDescripcion"),
                             EstatusId = info.Field<int>("EstatusID"),
                             EstatusDescripcion = info.Field<string>("EstatusDescripcion")
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static ChequeraInfo ObtenerPorChequera(DataSet ds)
        {
            ChequeraInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ChequeraInfo
                                    {
                                        ChequeraId = info.Field<int>("ChequeraId"),
                                        CentroAcopioId = info.Field<int>("OrganizacionId"),
                                        NumeroChequera = info.Field<string>("NumeroChequera"),
                                        BancoId = info.Field<int>("BancoId"),
                                        ChequeInicial = info.Field<int>("ChequeIDInicial"),
                                        ChequeFinal = info.Field<int>("ChequeIDFinal"),
                                        EstatusId = info.Field<int>("EstatusId"),
                                        FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                        ChequesDisponibles = info.Field<int>("Disponibles"),
                                        ChequesGirados = info.Field<int>("Girados"),
                                        ChequesCancelados = info.Field<int>("Cancelados")
                                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static int ObtenerConsecutivo(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var result = int.Parse(dt.Rows[0][0].ToString());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static int ObtenerFolio(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var result = int.Parse(dt.Rows[0][0].ToString());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}