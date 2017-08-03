using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxControlEntradaGanadoDAL
    {
        /// <summary>
        ///  Obtener parametros para control de entrada ganado 
        /// </summary>
        /// <param name="almacenId"></param>
        /// <returns></returns>
        
        internal static Dictionary<string, object> ObtenerParametrosObtenerControlEntradaGanadoPorID(int animalID, int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", animalID},
                            {"@EntradaGanadoID", entradaGanadoID},
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
        /// Obtener xml para guardar Control Entrada Ganado
        /// </summary>
        /// <param name="controlEntradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarControlEntradaGanado(ControlEntradaGanadoInfo controlEntradaGanado)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from ControlEntradaDetalle in controlEntradaGanado.ListaControlEntradaGanadoDetalle
                                select
                                    new XElement("ControlEntradaDetalle",
                                        new XElement("CostoID", ControlEntradaDetalle.Costo.CostoID),
                                        new XElement("Importe", ControlEntradaDetalle.Importe),
                                        new XElement("Activo", (int)ControlEntradaDetalle.Activo),
                                        new XElement("UsuarioCreacionID", ControlEntradaDetalle.UsuarioCreacionID)
                                ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoID", controlEntradaGanado.EntradaGanado.EntradaGanadoID},
                            {"@AnimalID",controlEntradaGanado.Animal.AnimalID},
                            {"@PesoCompra",controlEntradaGanado.Animal.PesoCompra},
                            {"@Activo",(int)controlEntradaGanado.Activo},
                            {"@UsuarioCreacionID",controlEntradaGanado.UsuarioCreacionID},
                            {"@Xml", xml.ToString()}
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
        /// Obtener parametros para eliminar control de entrada de ganado
        /// </summary>
        /// <param name="EntradaGanadoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEliminaControlEntradaGanado(int EntradaGanadoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoID", EntradaGanadoID},
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
