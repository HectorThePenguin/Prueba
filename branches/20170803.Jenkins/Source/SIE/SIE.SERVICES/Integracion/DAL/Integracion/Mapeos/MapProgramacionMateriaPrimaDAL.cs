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
    public class MapProgramacionMateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene la lista de programacion materia prima
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ProgramacionMateriaPrimaInfo> ObtenerProgramacionMateriaPrima(DataSet ds)
        {
            List<ProgramacionMateriaPrimaInfo> listaProgramacion;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaProgramacion = (from info in dt.AsEnumerable()
                                select new ProgramacionMateriaPrimaInfo
                                {
                                    ProgramacionMateriaPrimaId = info.Field<int>("ProgramacionMateriaPrimaID"),
                                    PedidoDetalleId = info.Field<int>("PedidoDetalleID"),
                                    Organizacion = new OrganizacionInfo{ OrganizacionID = info.Field<int>("OrganizacionID")},
                                    Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID") },
                                    InventarioLoteOrigen = new AlmacenInventarioLoteInfo{AlmacenInventarioLoteId = info.Field<int>("InventarioLoteIDOrigen")},
                                    CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                                    CantidadEntregada = info["CantidadEntregada"] == DBNull.Value ? 0 : info.Field<decimal>("CantidadEntregada"),
                                    Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones"),
                                    Justificacion = info["Justificacion"] == DBNull.Value ? string.Empty : info.Field<string>("Justificacion"),
                                    FechaProgramacion = info["FechaProgramacion"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaProgramacion"),
                                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaProgramacion;
        }

        internal static ProgramacionMateriaPrimaInfo ObtenerPorPesajeMateriaPrima(DataSet ds)
        {
            ProgramacionMateriaPrimaInfo programacion;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                programacion = (from info in dt.AsEnumerable()
                                     select new ProgramacionMateriaPrimaInfo
                                     {
                                         ProgramacionMateriaPrimaId = info.Field<int>("ProgramacionMateriaPrimaID"),
                                         PedidoDetalleId = info.Field<int>("PedidoDetalleID"),
                                         Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                                         Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID") },
                                         InventarioLoteOrigen = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("InventarioLoteIDOrigen") },
                                         CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                                         CantidadEntregada = info["CantidadEntregada"] == DBNull.Value ? 0 : info.Field<decimal>("CantidadEntregada"),
                                         Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones"),
                                         Justificacion = info["Justificacion"] == DBNull.Value ? string.Empty : info.Field<string>("Justificacion"),
                                         FechaProgramacion = info["FechaProgramacion"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaProgramacion"),
                                     }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return programacion;
        }

        /// <summary>
        /// Obtiene una programacion de materia prima
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProgramacionMateriaPrimaInfo ObtenerPorProgramacionMateriaPrimaTicket(DataSet ds)
        {
            ProgramacionMateriaPrimaInfo programacion;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                programacion = (from info in dt.AsEnumerable()
                                select new ProgramacionMateriaPrimaInfo
                                           {
                                               ProgramacionMateriaPrimaId =
                                                   info.Field<int>("ProgramacionMateriaPrimaID"),
                                               PedidoDetalleId = info.Field<int>("PedidoDetalleID"),
                                               Organizacion =
                                                   new OrganizacionInfo
                                                       {
                                                           OrganizacionID = info.Field<int>("OrganizacionID")
                                                       },
                                               Almacen = new AlmacenInfo
                                                             {
                                                                 AlmacenID = info.Field<int>("AlmacenID")
                                                             },
                                               InventarioLoteOrigen =
                                                   new AlmacenInventarioLoteInfo
                                                       {
                                                           AlmacenInventarioLoteId =
                                                               info.Field<int>("InventarioLoteIDOrigen")
                                                       },
                                               CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                                               CantidadEntregada = info.Field<decimal?>("CantidadEntregada") ?? 0,
                                               Observaciones = info.Field<string>("Observaciones"),
                                               Justificacion = info.Field<string>("Justificacion"),
                                               FechaProgramacion =
                                                   info.Field<DateTime?>("FechaProgramacion") ?? new DateTime(),
                                           }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return programacion;
        }
    }
}
