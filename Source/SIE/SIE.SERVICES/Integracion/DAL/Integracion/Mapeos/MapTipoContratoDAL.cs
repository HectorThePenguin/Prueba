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
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTipoContratoDAL
    {
        /// <summary>
        /// Obtiene una lista de tipos de contrato
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>Lista de TipoContratoInfo</returns>
        internal static List<TipoContratoInfo> ObtenerTodos(DataSet ds)
        {
            List<TipoContratoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new TipoContratoInfo
                         {
                             TipoContratoId = info.Field<int>("TipoContratoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = EstatusEnum.Activo
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene una lista de tipos de contrato
        /// </summary>
        /// <param name="ds"></param>
        internal static TipoContratoInfo ObtenerPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                TipoContratoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoContratoInfo
                             {
								TipoContratoId = info.Field<int>("TipoContratoID"),
								Descripcion = info.Field<string>("Descripcion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
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
