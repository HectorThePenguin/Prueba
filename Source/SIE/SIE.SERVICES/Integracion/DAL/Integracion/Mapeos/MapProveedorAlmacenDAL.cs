using System;
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
    internal class MapProveedorAlmacenDAL
    {
        /// <summary>
        /// Obtiene un proveedor almacen por proveedorid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorAlmacenInfo ObtenerPorProveedorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                ProveedorAlmacenInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorAlmacenInfo
                         {
                             ProveedorAlmacenId = info.Field<int>("ProveedorAlmacenID"),
                             ProveedorId = info.Field<int>("ProveedorID"),
                             Proveedor = new ProveedorInfo
                                 {
                                     ProveedorID = info.Field<int>("ProveedorID")
                                 },
                             AlmacenId = info.Field<int>("AlmacenID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un proveedor almacen por proveedorid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorAlmacenInfo ObtenerPorAlmacenID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                ProveedorAlmacenInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorAlmacenInfo
                         {
                             ProveedorAlmacenId = info.Field<int>("ProveedorAlmacenID"),
                             ProveedorId = info.Field<int>("ProveedorID"),
                             Proveedor = new ProveedorInfo
                                 {
                                     ProveedorID = info.Field<int>("ProveedorID")
                                 },
                             Almacen = new AlmacenInfo
                                 {
                                     AlmacenID = info.Field<int>("AlmacenID")
                                 },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un proveedor almacen por proveedorid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorAlmacenInfo ObtenerPorProveedorTipoAlmacen(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                ProveedorAlmacenInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorAlmacenInfo
                         {
                             ProveedorAlmacenId = info.Field<int>("ProveedorAlmacenID"),
                             ProveedorId = info.Field<int>("ProveedorID"),
                             Proveedor = new ProveedorInfo
                             {
                                 ProveedorID = info.Field<int>("ProveedorID")
                             },
                             AlmacenId = info.Field<int>("AlmacenID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
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
