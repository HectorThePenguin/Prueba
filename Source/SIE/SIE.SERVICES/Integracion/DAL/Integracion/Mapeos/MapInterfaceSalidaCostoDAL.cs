using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapInterfaceSalidaCostoDAL
    {
        /// <summary>
        /// Obtener los animales y sus costos en la interface salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<InterfaceSalidaCostoInfo> ObtenerCostoAnimales(DataSet ds)
        {
            List<InterfaceSalidaCostoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new InterfaceSalidaCostoInfo
                         {
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             SalidaID = info.Field<int>("SalidaID"),
                             Arete = info.Field<string>("Arete"),
                             FechaCompra = info.Field<DateTime>("FechaCompra"),
                             Costo = new CostoInfo { CostoID = info.Field<int>("CostoID") },
                             Importe = info.Field<decimal>("Importe"),
                             FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                             UsuarioRegistro = info.Field<string>("UsuarioRegistro")
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
    }
}
