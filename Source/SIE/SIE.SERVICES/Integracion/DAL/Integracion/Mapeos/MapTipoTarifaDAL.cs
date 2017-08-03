
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
    internal class MapTipoTarifaDAL
    {
        /// <summary>
        /// Obtiene una lista de tipotarifainfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoTarifaInfo> ObtenerTodos(DataSet ds)
        {
            List<TipoTarifaInfo> tipoTarifaLista;
            List<TipoTarifaInfo> tipoTarifaLista2;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                tipoTarifaLista = (from info in dt.AsEnumerable()
                                        select new TipoTarifaInfo()
                                        {
                                            TipoTarifaId = info.Field<int>("TipoTarifaID"),
                                            Descripcion = info.Field<string>("Descripcion")
                                        }).ToList();

                tipoTarifaLista.Add(new TipoTarifaInfo { TipoTarifaId = 0, Descripcion = "Seleccione" });

                var sortedDoubles =
                    from d in tipoTarifaLista
                    orderby d.TipoTarifaId ascending
                    select d;

                    tipoTarifaLista2 = sortedDoubles.ToList();
                
                

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return tipoTarifaLista2;
        }
    }
}
