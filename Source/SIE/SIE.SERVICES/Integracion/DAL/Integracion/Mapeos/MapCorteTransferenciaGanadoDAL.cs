using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapCorteTransferenciaGanadoDAL
    {
        /// <summary>
        /// Obtener permisos trampa
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static TrampaInfo ObtenerPermisoTrampa(DataSet ds)
        {
            var trampaInfo = new TrampaInfo();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    trampaInfo.TrampaID = dr.Field<int>("TrampaID");
                    trampaInfo.Descripcion = dr.Field<string>("Descripcion");
                    trampaInfo.Organizacion =
                        new OrganizacionInfo
                            {
                                OrganizacionID = dr.Field<int>("OrganizacionID")
                            };
                    trampaInfo.TipoTrampa = Convert.ToChar(dr.Field<string>("TipoTrampa"));
                    trampaInfo.HostName = dr.Field<string>("HostName");
                    trampaInfo.Activo = dr.Field<bool>("Activo").BoolAEnum();
                    trampaInfo.FechaCreacion = dr.Field<DateTime>("FechaCreacion");
                    trampaInfo.UsuarioCreacionID = dr.Field<int>("UsuarioCreacionID");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return trampaInfo;
        }

        /// <summary>
        /// Metodo para obtener los datos de total de cabezas de corte de transferencia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CorteTransferenciaTotalCabezasInfo ObtenerCabezasCortadas(DataSet ds)
        {
            CorteTransferenciaTotalCabezasInfo totalInfo = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                totalInfo = new CorteTransferenciaTotalCabezasInfo();

                foreach (DataRow dr in dt.Rows)
                {
                    totalInfo.Total = dr.Field<int>("Total");
                    totalInfo.TotalCortadas = dr.Field<int>("TotalCortadas");
                    totalInfo.TotalPorCortar = dr.Field<int>("TotalPorCortar");
               }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return totalInfo;
        }
        /// <summary>
        /// Obtener entrada de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EntradaGanadoInfo ObtenerEntradaGanado(DataSet ds)
        {
            var entradaGanadoInfo = new EntradaGanadoInfo();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    entradaGanadoInfo.FechaEntrada = Convert.ToDateTime(dr["FechaEntrada"]);
                    entradaGanadoInfo.EmbarqueID = dr.Field<int>("EmbarqueID");
                    entradaGanadoInfo.OrganizacionID = dr.Field<int>("OrganizacionID");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
        /// <summary>
        /// Obtener corrales por tipo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CorralInfo ObtenerCorralesTipo(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                             }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Se obtiene los tratamientos aplicados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<TratamientoInfo> ObtenerTratamientosAplicados(DataSet ds)
        {
            List<TratamientoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                    select new TratamientoInfo
                    {
                        TratamientoID = info.Field<int>("TratamientoID"),
                        FechaAplicacion = info.Field<DateTime>("FechaMovimiento"),
                        CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                        TipoTratamientoInfo = new TipoTratamientoInfo
                                                  {
                                                      TipoTratamientoID = info.Field<int>("TipoTratamientoID")
                                                  }
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
