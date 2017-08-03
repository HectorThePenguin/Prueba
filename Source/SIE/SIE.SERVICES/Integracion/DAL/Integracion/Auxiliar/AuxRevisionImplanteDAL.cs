using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// The aux revision implante dal.
    /// </summary>
    internal class AuxRevisionImplanteDAL
    {
        /// <summary>
        /// The obtener parametro por id.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static Dictionary<string, object> ObtenerParametrosAreaRevision()
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// The obtener parametros causas.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static Dictionary<string, object> ObtenerParametrosCausas()
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// The obtener guardar revision implante.
        /// </summary>
        /// <param name="listaRevisionImplante">
        /// The lista revision implante.
        /// </param>
        /// <param name="usuario">
        /// The usuario.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static Dictionary<string, object> ObtenerGuardarRevisionImplante(List<RevisionImplanteInfo> listaRevisionImplante, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                        from info in listaRevisionImplante
                        select
                            new XElement("RevisionReimplante",
                                new XElement("LoteID", info.Lote.LoteID),
                                new XElement("AnimalID", info.Animal.AnimalID),
                                new XElement("Fecha", info.Fecha),
                                new XElement("AreaRevisionID", info.LugarValidacion.AreaRevisionId),
                                new XElement("EstadoImplanteID", info.Causa.CausaId),
                                new XElement("UsuarioCreacionID", usuario.UsuarioID)));
                var parametros =
                    new Dictionary<string, object>
                        {
                            { "@XmlRevision", xml.ToString() }
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los parametros para la revision actual
        /// </summary>
        /// <param name="animal">
        /// Animal actual.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal static Dictionary<string, object> ObtenerParametrosRevisionActual(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalId", animal.AnimalID},
                            {"@Activo", (int)EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
