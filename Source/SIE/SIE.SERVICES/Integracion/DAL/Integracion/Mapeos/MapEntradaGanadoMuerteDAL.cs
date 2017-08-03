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
    internal class MapEntradaGanadoMuerteDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoMuerteInfo> ObtenerMuertesEnTransitoPorEntradaGanadoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                List<EntradaGanadoMuerteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoMuerteInfo
                         {
                             EntradaGanadoMuerteID = info.Field<int>("EntradaGanadoMuerteID"),
                             EntradaGanado = new EntradaGanadoInfo
                             {
                                 EntradaGanadoID = info.Field<int>("EntradaGanadoID")
                             },
                             Animal = new AnimalInfo
                             {
                                 Arete = info.Field<string>("Arete")
                             },
                             FolioMuerte = info.Field<int>("FolioMuerte"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                             Peso = info.Field<decimal>("Peso"),
                             EntradaGanadMuerteDetalle = (from det in dtDetalle.AsEnumerable()
                                                          where det.Field<int>("EntradaGanadoMuerteID")
                                                          == info.Field<int>("EntradaGanadoMuerteID")
                                                          select new EntradaGanadoMuerteDetalleInfo
                                                          {
                                                              EntradaGanadoMuerte = new EntradaGanadoMuerteInfo
                                                              {
                                                                  EntradaGanadoMuerteID = det.Field<int>("EntradaGanadoMuerteID"),
                                                              },
                                                              Costo = new CostoInfo
                                                              {
                                                                  CostoID = det.Field<int>("CostoID")
                                                              },
                                                              Importe = det.Field<decimal>("Importe")
                                                          }).ToList()
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

