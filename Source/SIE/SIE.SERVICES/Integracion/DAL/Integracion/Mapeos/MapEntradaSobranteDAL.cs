using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapEntradaSobranteDAL
    {
        /// <summary>
        /// Metodo para obtener las cabezas sobrantes de una partida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoSobranteInfo> ObtenerSobrantePorEntradaGanado(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaGanadoSobranteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoSobranteInfo
                         {
                             EntradaGanadoSobranteID = info.Field<int>("EntradaGanadoSobranteID"),
                             EntradaGanado = new EntradaGanadoInfo
                             {
                                 EntradaGanadoID = info.Field<int>("EntradaGanadoID")
                             },
                             Animal = new AnimalInfo
                             {
                                 AnimalID = info.Field<long>("AnimalID"),
                                 PesoCompra = info.Field<int>("PesoCompra")
                             },
                             Importe = info.Field<decimal>("Importe"),
                             Costeado = info.Field<bool>("Costeado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
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

