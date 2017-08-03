using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;


namespace SIE.Services.Integracion.DAL
{
    internal class EntradaGanadoSobranteDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear una entrada de ganado Sobrante
        /// 
        /// </summary>
        /// <param name="entradaGanadoSobranteInfo"></param>
        internal int GuardarEntradaGanadoSobrante(EntradaGanadoSobranteInfo entradaGanadoSobranteInfo)
        {
            int entradaGanadoID;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = 
                    AuxEntradaGanadoSobranteDAL.ObtenerParametrosCrear(entradaGanadoSobranteInfo);
                entradaGanadoID = Create("EntradaGanadoSobrante_Crear", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoID;
        }

        /// <summary>
        /// Metodo para obtener las cabezas sobrantes de una partida
        /// </summary>
        /// <param name="entradaGanadoId"></param>
        /// <returns></returns>
        internal List<EntradaGanadoSobranteInfo> ObtenerSobrantePorEntradaGanado(int entradaGanadoId)
        {
            List<EntradaGanadoSobranteInfo> entradaGanadoInfo = null;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@EntradaGanadoID", entradaGanadoId } };
                DataSet ds = Retrieve("EntradaGanadoSobrante_ObtenerSobrantePorEntradaGanado", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaGanadoInfo = MapEntradaSobranteDAL.ObtenerSobrantePorEntradaGanado(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo para Cambiar el estado de costeado de un animal en la tabla EntradaGanadoSobrante
        /// </summary>
        /// <param name="cabezasSobrante"></param>
        /// <returns></returns>
        internal int ActualizarCosteadoEntradaGanadoSobrante(List<EntradaGanadoSobranteInfo> cabezasSobrante)
        {
            int resp = -1;
            try
            {
                Logger.Info();
                var xml =
                  new XElement("ROOT",
                        from sobrante in cabezasSobrante
                        select new XElement("CabezasSobrante",
                                new XElement("EntradaGanadoSobranteID", sobrante.EntradaGanadoSobranteID),
                                new XElement("AnimalID", sobrante.Animal.AnimalID),
                                new XElement("UsuarioModificacionID", sobrante.UsuarioModificacionID))
                            );

                var parametros = new Dictionary<string, object> { { "@CabezasSobrante", xml.ToString() } };

                Update("EntradaGanadoSobrante_ActualizarCosteadoEntradaGanadoSobrante", parametros);
                resp = 1;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resp;
        }
    }
}
