using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapProgramacionReimplanteDAL
    {
        internal static ResultadoInfo<OrdenReimplanteInfo> ObtenerLotesDisponiblesReimplante(DataSet ds)
        {
            ResultadoInfo<OrdenReimplanteInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var dalT = new Implementacion.TipoGanadoDAL();

                List<OrdenReimplanteInfo> lotes = (from operador in dt.AsEnumerable()
                                select new OrdenReimplanteInfo
                                {
                                     LoteId = operador.Field<int>("LoteId"),
                                     OrganizacionId = operador.Field<int>("OrganizacionId"),
                                     IdCorralOrigen = operador.Field<int>("CorralId"),
                                     CodigoCorralOrigen = operador.Field<string>("Codigo").Trim(),
                                     FechaReimplante = operador.Field<DateTime>("FechaProyectada"),
                                     NumeroReimplante = operador.Field<int>("NumeroReimplante"),
                                     KilosProyectados = operador.Field<int>("PesoProyectado"),
                                     Cabezas = operador.Field<int>("CabezasRecibidas"),
                                     TipoGanado = dalT.ObtenerPorID(operador.Field<int>("TipoGanadoId")),
                                     TipoProcesoIdLote = operador.Field<int>("TipoProcesoID")
                                }).ToList();

                resultado = new ResultadoInfo<OrdenReimplanteInfo>
                {
                    Lista = lotes,
                    TotalRegistros = Convert.ToInt32(lotes.Count())
                };

                dt = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        internal static List<ProgramacionReinplanteInfo> ObtenerProgramacionReimplantePorLoteID(DataSet ds)
        {
            List<ProgramacionReinplanteInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from programacion in dt.AsEnumerable()
                            select new ProgramacionReinplanteInfo
                            {
                                FolioProgramacionID = programacion.Field<int>("FolioProgramacionID"),
                                OrganizacionID = programacion.Field<int>("OrganizacionID"),
                                Fecha = programacion.Field<DateTime>("Fecha"),
                                LoteID = programacion.Field<int>("LoteID"),
                                /*CorralDestinoID = programacion.Field<int>("CorralDestinoID"),*/
                                ProductoID = programacion.Field<int>("ProductoID")
                            }).ToList();
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
