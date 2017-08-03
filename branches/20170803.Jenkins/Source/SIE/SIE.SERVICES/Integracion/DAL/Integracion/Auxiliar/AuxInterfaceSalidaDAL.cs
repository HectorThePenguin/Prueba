using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxInterfaceSalidaDAL
    {
        /// <summary>
        /// Obtiene los parametros para Ejecutar el Procedimiento
        /// Almacenado InterfaceSalida_Crear
        /// </summary>
        /// <param name="interfaceSalidaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@SalidaID", interfaceSalidaInfo.SalidaID},
                        {"@OrganizacionDestinoID", interfaceSalidaInfo.OrganizacionDestino.OrganizacionID},
                        {"@OrganizacionID", interfaceSalidaInfo.Organizacion.OrganizacionID},
                        {"@Cabezas", interfaceSalidaInfo.Cabezas},
                        {"@FechaSalida", interfaceSalidaInfo.FechaSalida},
                        {"@EsRuteo", interfaceSalidaInfo.EsRuteo},
                        {"@Activo", interfaceSalidaInfo.Activo},
                        {"@UsuarioRegistro", interfaceSalidaInfo.UsuarioCreacionID},
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
        /// Obtiene los parametros para Ejecutar el Procedimiento Almacenado
        /// InterfaceSalida_ObtenerPorID
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int salidaID, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@SalidaID", salidaID},
                        {"@OrganizacionID", organizacionID},
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
        /// Obtiene los parametros para Ejecutar el Procedimiento Almacenado
        /// InterfaceSalida_ObtenerPorID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorSalidaOrganizacion(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@SalidaID", entradaGanado.FolioOrigen},
                        {"@OrganizacionID", entradaGanado.OrganizacionOrigenID},
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
        /// Obtiene los parametros para Ejecutar el Procedimiento Almacenado
        /// InsterfaceSalida_ObtenerPorEmbarqueID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", entradaGanado.EmbarqueID}
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
        /// Obtiene los parametros para Ejecutar el Procedimiento Almacenado
        /// InterfaceSalida_ObtenerPorEmbarque
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEmbarque(int salidaID, int organizacionID, int organizacionOrigenID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@OrganizacionOrigenID", organizacionOrigenID},
                        {"@Salida", salidaID},
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
