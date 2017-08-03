using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPagoTransferenciaDAL
    {
        internal static ResultadoInfo<PagoTransferenciaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<PagoTransferenciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<PagoTransferenciaInfo> lista = (from info in dt.AsEnumerable()
                                                     select new PagoTransferenciaInfo
                                          {
                                              PagoId = info.Field<int>("PagoId"),
                                              BancoId = info.Field<int>("BancoId"),
                                              ProveedorId = info.Field<int>("Proveedor"),
                                              BancoDescripcion = info.Field<string>("BancoDescripcion"),
                                              ProveedorDescripcion = info.Field<string>("ProveedorDescripcion"),
                                              FolioEntrada = info.Field<int>("FolioEntrada"),
                                              Fecha = info.Field<DateTime>("Fecha"),
                                              CentroAcopioId = info.Field<int>("CentroAcopioId"),
                                              CentroAcopioDescripcion = info.Field<string>("CentroAcopioDescripcion"),
                                              Importe = info.Field<decimal>("Importe")
                                          }).ToList();

                resultado = new ResultadoInfo<PagoTransferenciaInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
