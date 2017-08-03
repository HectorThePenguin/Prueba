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
    internal class MapMenuDAL
    {
        /// <summary>
        ///     Metodo que obtuene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<MenuInfo> ObtenerTodos(DataSet ds)
        {
            IList<MenuInfo> listaMenu;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                listaMenu = (from groupinfo in dt.AsEnumerable()
                             select new MenuInfo
                                 {
                                     ModuloID = groupinfo.Field<int>("ModuloID"),
                                     Modulo = groupinfo.Field<string>("Modulo"),
                                     FormularioID = groupinfo.Field<int>("FormularioID"),
                                     Formulario = groupinfo.Field<string>("Formulario"),
                                     WinForm = groupinfo.Field<string>("WinForm"),
                                     Imagen = groupinfo.Field<string>("Imagen"),
                                     Control = groupinfo.Field<string>("Control"),
                                     PadreID = groupinfo.Field<int?>("PadreID"),
                                     Padre = groupinfo.Field<string>("Padre"),
                                     ImagenPadre = groupinfo.Field<string>("ImagenPadre"),
                                     OrdenFormulario = groupinfo.Field<int>("OrdenFormulario"),
                                     OrdenModulo = groupinfo.Field<int>("OrdenModulo"),
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaMenu;
        }
    }
}