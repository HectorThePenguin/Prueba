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
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapRuteoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<RuteoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RuteoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RuteoInfo
                         {
                             RuteoID = info.Field<int>("RuteoID"),
                             OrganizacionOrigen = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionOrigenID"), Descripcion = info.Field<string>("OrganizacionOrigen") },
                             OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionDestinoID"), Descripcion = info.Field<string>("OrganizacionDestino") },
                             NombreRuteo = info.Field<string>("NombreRuteo"),
                             Rutas = info.Field<string>("Rutas"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<RuteoInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RuteoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                RuteoInfo lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RuteoInfo
                         {
                             RuteoID = info.Field<int>("RuteoID"),
                             OrganizacionOrigen = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionOrigenID"), Descripcion = info.Field<string>("Origen") },
                             OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionDestinoID"), Descripcion = info.Field<string>("Destino") },
                             NombreRuteo = info.Field<string>("NombreRuteo"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<RuteoDetalleInfo> ObtenerDetallePorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RuteoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RuteoDetalleInfo
                         {
                             //ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID"),
                             OrganizacionOrigen = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionOrigenID"), Descripcion = info.Field<string>("OrganizacionOrigen") },
                             OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionDestinoID"), Descripcion = info.Field<string>("OrganizacionDestino") },
                             Kilometros = info.Field<decimal>("Kilometros"),
                             Horas = info.Field<double>("Horas"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<RuteoDetalleInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
