using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxCorteGanadoDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosExisteAreteMetalicoEnPartida(AnimalInfo animalInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@AreteMetalico", animalInfo.AreteMetalico},
                                     {"@OrganizacionIDEntrada", animalInfo.OrganizacionIDEntrada}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="noTipoCorral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int noTipoCorral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var lista = ganadoEnfermeria.FolioEntradaAgrupado.Split('|');

               
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("PartidasCorte",
                                                new XElement("NoPartida", partida.Trim()))
                                   );
                parametros = new Dictionary<string, object>
                                 {
                                     {"@NoPartida", xml.ToString()},
                                     {"@GrupoCorralID",noTipoCorral},
                                     {"@OrganizacionID",ganadoEnfermeria.OrganizacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros para nombre proveedor
        /// </summary>
        /// <param name="noEmbarqueID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosNombreProveedor(int noEmbarqueID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@NoEmbarqueID", noEmbarqueID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

       
        /// <summary>
        /// Obtiene parametros para ver si existe arete en partida
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosExisteAreteEnPartida(AnimalInfo animalInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Arete", animalInfo.Arete},
                                     {"@OrganizacionIDEntrada", animalInfo.OrganizacionIDEntrada}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        
        /// <summary>
        /// Obtiene parametros para ver si existe arete en partida
        /// </summary>
        /// <param name="movimientosAlmacen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosRegistrarMovimientosAlmacen(MovimientosAlmacen movimientosAlmacen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@bGenerarFolios", movimientosAlmacen.GenerarFolios},
                    {"@xmlMov", movimientosAlmacen.XmlMov},
                    {"@FolioAsignado", movimientosAlmacen.FolioAsignado}
                                     
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros buscar las cabezas portadas  de una partida
        /// </summary>
        /// <param name="cabezasCortadas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCabezasCortadas(CabezasCortadas cabezasCortadas)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("PartidasCorte",
                                                new XElement("NoPartida", partida))
                                   );
                parametros = new Dictionary<string, object>
                {
                    {"@NoPartida", xml.ToString()},
                    {"@OrganizacionID", cabezasCortadas.OrganizacionID},
                    {"@TipoCorral", cabezasCortadas.TipoCorral},
                    {"@TipoMovimiento", cabezasCortadas.TipoMovimiento}
                                     
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros buscar las cabezas portadas  de una partida
        /// </summary>
        /// <param name="cabezasMuertas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCabezasMuertas(CabezasCortadas cabezasMuertas)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var lista = cabezasMuertas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("PartidasCorte",
                                                new XElement("NoPartida", partida))
                                   );
                parametros = new Dictionary<string, object>
                {
                    {"@NoPartida", xml.ToString()},
                    {"@OrganizacionID", cabezasMuertas.OrganizacionID}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros para ver si existen pesos
        /// </summary>
        /// <param name="OrganizacionID"></param>
        /// <param name="loteOrigenID"></param>
        /// <param name="corralOrigenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObteenerPesosOrigenLlegada(int OrganizacionID, int corralOrigenID, int loteOrigenID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                {
                    {"@CorralID", corralOrigenID},
                    {"@LoteID", loteOrigenID},
                    {"@OrganizacionID", OrganizacionID}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros buscar las cabezas Sobrantes cortadas de una partida
        /// </summary>
        /// <param name="paramCabezaSobrantesCortadas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCabezasSobrantesCortadas(CabezasCortadas paramCabezaSobrantesCortadas)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var lista = paramCabezaSobrantesCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("PartidasCorte",
                                                new XElement("NoPartida", partida))
                                   );
                parametros = new Dictionary<string, object>
                {
                    {"@NoPartida", xml.ToString()},
                    {"@OrganizacionID", paramCabezaSobrantesCortadas.OrganizacionID}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

    }
}
