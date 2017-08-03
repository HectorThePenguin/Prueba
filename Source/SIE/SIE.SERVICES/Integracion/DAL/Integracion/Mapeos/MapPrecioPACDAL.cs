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
    internal class MapPrecioPACDAL
    {
        /// <summary>
        /// Asigna el listado de diferencias de inventario obtenidas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PrecioPACInfo ObtenerActivoPorOrganizacion(DataSet ds)
        {
            PrecioPACInfo precioPac;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                precioPac = (from info in dt.AsEnumerable()
                             select new PrecioPACInfo
                                        {
                                            Precio = info.Field<decimal>("Precio"),
                                            OrganizacionID = info.Field<int>("OrganizacionID"),
                                            Organizacion = new OrganizacionInfo
                                                               {
                                                                   OrganizacionID = info.Field<int>("OrganizacionID")
                                                               },
                                            PrecioViscera = info.Field<decimal>("PrecioViscera"),
                                            TipoPAC = new TipoPACInfo
                                                          {
                                                              TipoPACID = info.Field<int>("TipoPACID")
                                                          },
                                            TipoPACID = info.Field<int>("TipoPACID"),
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return precioPac;
        }
    }
}
