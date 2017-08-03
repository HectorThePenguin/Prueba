using System;
using System.Collections.Generic;
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
    internal class MapProveedorChoferDAL
    {
        /// <summary>
        /// Obtiene el proveedor chofer por su identificador
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorChoferInfo ObtenerProveedorChoferPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorChoferInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorChoferInfo
                         {
                             ProveedorChoferID = info.Field<int>("ProveedorChoferID"),
                             Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                             Chofer = new ChoferInfo { ChoferID = info.Field<int>("ChoferID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<ProveedorChoferInfo> ObtenerProveedorChoferPorProveedorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProveedorChoferInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorChoferInfo
                         {
                             ProveedorChoferID = info.Field<int>("ProveedorChoferID"),
                             Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                             Chofer = new ChoferInfo { ChoferID = info.Field<int>("ChoferID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        ///  Obtiene los campos de la tabla ProveedorChofer consultando por ChoferID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorChoferInfo ObtenerProveedorChoferPorChoferID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorChoferInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorChoferInfo
                         {
                             ProveedorChoferID = info.Field<int>("ProveedorChoferID"),
                             Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                             Chofer = new ChoferInfo { ChoferID = info.Field<int>("ChoferID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
