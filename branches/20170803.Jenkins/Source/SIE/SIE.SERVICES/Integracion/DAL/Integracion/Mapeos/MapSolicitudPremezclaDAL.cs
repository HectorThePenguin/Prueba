
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapSolicitudPremezclaDAL
    {
        /// <summary>
        /// Obtiene una solicitud
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SolicitudPremezclaInfo ObtenerSolicitud(DataSet ds)
        {
            SolicitudPremezclaInfo solicitud;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                solicitud = (from info in dt.AsEnumerable()
                                        select new SolicitudPremezclaInfo()
                             {
                                 SolicitudPremezclaId = info.Field<int>("SolicitudPremezclaID"),
                                 Organizacion = new OrganizacionInfo(){OrganizacionID = info.Field<int>("OrganizacionID")},
                                 Fecha = info.Field<DateTime>("Fecha"),
                                 FechaInicio = info.Field<DateTime>("FechaInicio"),
                                 Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 UsuarioCreacion = new UsuarioInfo(){UsuarioID = info.Field<int>("UsuarioCreacionID")}
                             }).First();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return solicitud;
        }
    }
}
