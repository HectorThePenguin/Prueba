using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAnimalDAL
    {
        /// <summary>
        ///     Metodo para obtener los parametros para guardr un Animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", animalInfo.Arete},
                            {"@AreteMetalico", animalInfo.AreteMetalico},
                            {"@FechaCompra", animalInfo.FechaCompra},
                            {"@TipoGanadoID", animalInfo.TipoGanadoID},
                            {"@CalidadGanadoID", animalInfo.CalidadGanadoID},
                            {"@ClasificacionGanadoID", animalInfo.ClasificacionGanadoID},
                            {"@PesoCompra", animalInfo.PesoCompra},
                            {"@OrganizacionIDEntrada", animalInfo.OrganizacionIDEntrada},
                            {"@FolioEntrada", animalInfo.FolioEntrada},
                            {"@PesoLlegada", animalInfo.PesoLlegada},
                            {"@Paletas", animalInfo.Paletas},
                            {"@CausaRechadoID", animalInfo.CausaRechadoID},
                            {"@Venta", animalInfo.Venta},
                            {"@Cronico", animalInfo.Cronico},
                            {"@Activo", animalInfo.Activo},
                            {"@UsuarioCreacionID", animalInfo.UsuarioCreacionID},
                            {"@CambioSexo", animalInfo.CambioSexo},
                            {"@AnimalID", animalInfo.AnimalID},
                            {"@AplicaBitacora", animalInfo.AplicaBitacora}
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
        /// Metodo para obtener los parametros para obtener Peso
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="folioEntrada"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPeso(int organizacionID, int folioEntrada)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@NoFolioEntrada", folioEntrada}
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
        /// Metodo para obtener los parametros para obtener Peso
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="listaLotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFechasUltimoMovimiento(int organizacionID, List<int> listaLotes)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from lote in listaLotes
                                select
                                    new XElement("Lotes",
                                                 new XElement("LoteID", lote))
                                    );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@XmlLotes", xml.ToString()}
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
        /// Metodo para obtener los parametros para obtener Peso
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorLote(int organizacionID, int loteID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@LoteID", loteID}
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
        /// Obtiene un diccionario con los parametros necesarios
        /// para la ejecucion del procedimiento almacenado Animal_ObtenerPorDisponibilidad
        /// </summary>
        /// <param name="lotesDisponibles"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorLoteDisponibilidad(List<DisponibilidadLoteInfo> lotesDisponibles, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                        };
                var xml =
                    new XElement("ROOT",
                                 from lotes in lotesDisponibles
                                 select new XElement("Lotes",
                                                     new XElement("LoteID",
                                                                  lotes.LoteId)
                                     ));
                parametros.Add("@XmlLote", xml.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Se crea parametros para almacenar el Animal Movimiento
        /// </summary>
        /// <param name="animalCosto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarAnimalCosto(AnimalCostoInfo animalCosto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", animalCosto.AnimalID},
                            {"@CostoID", animalCosto.CostoID},
                            {"@FolioReferencia", animalCosto.FolioReferencia},
                            {"@Importe", animalCosto.Importe},
                            {"@UsuarioCreacionID", animalCosto.UsuarioCreacionID}
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
        /// Se crea parametros para almacenar el Animal Movimiento
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarAnimalCostoXML(List<AnimalCostoInfo> listaAnimalCosto)
        {
            try
            {
                Logger.Info();
                var xml =
                 new XElement("ROOT",
                              from animalCosto in listaAnimalCosto
                              select
                                  new XElement("AnimalCosto",
                                               new XElement("CostoID", animalCosto.CostoID),
                                               new XElement("TipoReferencia", animalCosto.TipoReferencia.GetHashCode()),
                                               new XElement("FolioReferencia", animalCosto.FolioReferencia),
                                               new XElement("FechaCosto", animalCosto.FechaCosto),
                                               new XElement("AnimalID", animalCosto.AnimalID),
                                               new XElement("Importe", animalCosto.Importe),
                                               new XElement("UsuarioCreacionID", animalCosto.UsuarioCreacionID)
                                               
                                               )
                                  );
                var parametros =
                        new Dictionary<string, object>
                            {
                                    {"@AnimalCostoXML", xml.ToString()}
                                };

                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorCodigoCorral(string codigoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Codigo", codigoCorral},
                                {"@OrganizacionID", organizacionId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerUltimoMovimientoAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AnimalID", animalInfo.AnimalID},
                                {"@OrganizacionID", animalInfo.OrganizacionIDEntrada}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalPorArete(string arete, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Arete", arete},
                                {"@OrganizacionID",organizacionId}
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
        /// Metodo para mapear los animales por aretes
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalPorAretes(AnimalInfo arete, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Arete", arete.Arete},
                                {"@AreteTestigo", arete.AreteMetalico},
                                {"@OrganizacionID",organizacionId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerPesoProyectado(string arete, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Arete", arete},
                                {"@OrganizacionID",organizacionId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerExisteSalida(long animalID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AnimalID", animalID}
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
        /// Obtener parametros para guardar animal salida
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="animalMovimientoInfo"></param>
        /// <returns></returns>
         internal static Dictionary<string, object> ObtenerParametrosGuardarCorralAnimalSalida(CorralInfo corralInfo, AnimalMovimientoInfo animalMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AnimalID", animalMovimientoInfo.AnimalID},
                                {"@Corraleta",corralInfo.CorralID},
                                {"@LoteID",animalMovimientoInfo.LoteID},
                                {"@TipoMovimiento",(int)TipoMovimiento.SalidaEnfermeria},
                                {"@Activo",(int)EstatusEnum.Activo},
                                {"@UsuarioID",animalMovimientoInfo.UsuarioCreacionID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosAnimalSalidaAnimalID(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@LoteID",loteInfo.LoteID},
                                {"@ActivoID",(int)EstatusEnum.Activo},
                                {"@OrganizacionID",loteInfo.OrganizacionID}
                                
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerExisteVentaDetalle(string arete, string areteMetalico)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Arete", arete},
                                {"@AreteMetalico", areteMetalico}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerExisteDeteccion(string arete)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Arete", arete},
                                {"@AreteTestigo", ""}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosAnimalSalidaAnimalID(long animalID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AnimalID",animalID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorLoteID(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@LoteID", loteInfo.LoteID},
                                {"@OrganizacionID", loteInfo.OrganizacionID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerExisteAreteSalida(int salida, string arete)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@SalidaID", salida},
                                {"@Arete", arete}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerExisteDeteccionTestigo(string areteTestigo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Arete", ""},
                                {"@AreteTestigo", areteTestigo}
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
        /// Obtiene
        /// </summary>
        /// <param name="areteTestigo"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalPorAreteTestigo(string areteTestigo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AreteTestigo", areteTestigo},
                                {"@OrganizacionID",organizacionID}
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
        /// Obtener parametros para guardar animal salida
        /// </summary>
        /// <param name="animalSalida"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarCorralAnimalSalidaLista(List<AnimalSalidaInfo> animalSalida)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from Salida in animalSalida
                               select
                                   new XElement("AnimalSalida",
                                                new XElement("AnimalID", Salida.AnimalId),
                                                new XElement("LoteID", Salida.LoteId),
                                                new XElement("CorraletaID", Salida.CorraletaId),
                                                new XElement("TipoMovimientoID", Salida.TipoMovimientoId),
                                                new XElement("Activo", Salida.Activo),
                                                new XElement("UsuarioCreacionID", Salida.UsuarioCreacion)
                                                )
                                   );

                parametros = new Dictionary<string, object>
                                 {
                        {"@XmlGuardarAnimalSalida", xml.ToString()},
                        {"@Activo",(int)EstatusEnum.Activo}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }



        internal static Dictionary<string, object> ObtenerParametrosObtenerLoteSalidaAnimal(string arete, string areteTestigo, int organizacionID)
      {
          try
          {
              Logger.Info();
              var parametros =
                      new Dictionary<string, object>
                          {
								    {"@Arete", arete},
                                    {"@AreteTestigo", areteTestigo},
                                    {"@OrganizacionID",organizacionID}
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
        /// Metodo para generar los parametros para obtener los animales por folio entrada
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorFolioEntrada(string folioEntrada, int organizacionID)
        {
            try
            {
                Logger.Info();
                var lista = folioEntrada.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("Partidas",
                                                new XElement("FolioEntrada", partida))
                                   );
                var parametros =
                        new Dictionary<string, object>
                            {
                                    {"@FolioEntrada", xml.ToString()},
                                    {"@OrganizacionID",organizacionID}
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
        /// mapear parametros para AnimalID
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosAnimalID(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								    {"@AnimalID", animalInactivo.AnimalID}
                                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalAnimalID(long animalID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								    {"@AnimalID", animalID}
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
        /// Obtener los parametros para inactivar el animal del inventarip
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInactivarAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								    {"@AnimalID", animalInfo.AnimalID},
                                    {"@Activo", EstatusEnum.Inactivo},
                                    {"@UsuarioModificacionID", animalInfo.UsuarioModificacionID}
                                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosActualizarAnimal(long animalId, string arete, int usuario)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								    {"@AnimalId", animalId},
                                    {"@Arete", arete},
                                    {"@UsuarioId", usuario}
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
        /// Obtiene los parametros necesarios para
        /// la ejecucion del procedimiento almacenado
        /// AnimalMovimiento_ObtenerUltimoMovimientoPaginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosUltimoMovimientoPorPagina(PaginacionInfo pagina
                                                                                            , AnimalInfo animal)
        {
            Dictionary<string, object> parametros;
            try
            {
                parametros = new Dictionary<string, object>
                                 {
                        {"@OrganizacionID", animal.OrganizacionIDEntrada},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@Arete", animal.AreteMetalico},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Animal_ObtenerUltimoMovimiento
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesMuertos(AnimalInfo animal)
        {
            Dictionary<string, object> parametros;
            try
            {
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", animal.OrganizacionIDEntrada},
                                     {"@Arete", animal.AreteMetalico},
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
        /// metodo para obtener los parametros para reemplazar los aretes de un animal
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="deteccionGrabar"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReemplazarAretes(AnimalInfo animal, DeteccionInfo deteccionGrabar)
        {
            Dictionary<string, object> parametros;
            try
            {
                parametros = new Dictionary<string, object>
                                 {
                                     {"@AnimalID", animal.AnimalID},
                                     {"@Arete", animal.Arete},
                                     {"@AreteTestigo", animal.AreteMetalico},
                                     {"@CorralIDDestino", deteccionGrabar.CorralID},
                                     {"@LoteIDDestino", deteccionGrabar.LoteID},
                                     {"@UsuarioCreacionID", deteccionGrabar.UsuarioCreacionID}
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
        /// metodo para obtener los parametros para reemplazar los aretes de un animal en el mismo coral
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ReemplazarAreteMismoCorral(AnimalInfo animal)
        {
            Dictionary<string, object> parametros;
            try
            {
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Arete", animal.Arete},
                                     {"@AreteTestigo", animal.AreteMetalico},
                                     {"@CorralID", animal.CorralID},
                                     {"@LoteID", animal.LoteID},
                                     {"@UsuarioModificacionID", animal.UsuarioModificacionID}
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
        /// Se crea parametros para almacenar el Animal Consumo
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarAnimalConsumoXML(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            try
            {
                Logger.Info();
                var xml =
                 new XElement("ROOT",
                              from animalConsumo in listaAnimalConsumo
                              select
                                  new XElement("AnimalConsumo",
                                               new XElement("AnimalID", animalConsumo.AnimalId),
                                               new XElement("RepartoID", animalConsumo.RepartoId),
                                               new XElement("Fecha", animalConsumo.Fecha),
                                               new XElement("FormulaIDServida", animalConsumo.FormulaServidaId),
                                               new XElement("Cantidad", animalConsumo.Cantidad),
                                               new XElement("TipoServicioID", (int) animalConsumo.TipoServicio),
                                               new XElement("UsuarioCreacionId", animalConsumo.UsuarioCreacionId)
                                       )
                                  );
                var parametros =
                        new Dictionary<string, object>
                            {
                                    {"@AnimalConsumoXML", xml.ToString()}
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
        /// Obtiene un diccionario con los parametros necesarios
        /// para la ejecucion del procedimiento almacenado Animal_ObtenerPorDisponibilidad
        /// </summary>
        /// <param name="lotesDisponibles"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorLoteReimplante(List<DisponibilidadLoteInfo> lotesDisponibles, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                        };
                var xml =
                    new XElement("ROOT",
                                 from lotes in lotesDisponibles
                                 select new XElement("Lotes",
                                                     new XElement("LoteID",
                                                                  lotes.LoteId)
                                     ));
                parametros.Add("@XmlLote", xml.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros para obtener los dias de la ultima deteccion
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDiasUltimaDeteccion(AnimalInfo animal)
        {
            Dictionary<string, object> parametros;
            try
            {
                parametros = new Dictionary<string, object>
                                 {
                                     {"@AnimalID", animal.AnimalID},
                                     {"@Arete", animal.Arete},
                                     {"@TipoMovimiento",TipoMovimiento.EntradaEnfermeria}
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
        /// Obtiene parametros para actualizar clasificacion ganado en la tabla animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ActualizaClasificacionGanado(AnimalInfo animalInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                parametros = new Dictionary<string, object>
                                 {
                                     {"@AnimalID", animalInfo.AnimalID},
                                     {"@UsuarioModificacionID", animalInfo.UsuarioModificacionID},
                                     {"@ClasificacionGanado",animalInfo.ClasificacionGanadoID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerUltimoMovimientoAnimalXML(List<AnimalInfo> animales, int organizacionID)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from animal in animales
                                 select new XElement("Animales",
                                                     new XElement("AnimalID",
                                                                  animal.AnimalID)));

                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AnimalXml", xml.ToString()},
                                {"@OrganizacionID", organizacionID}
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado AnimalMovimiento_ObtenerAnimalesPorLoteXML
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLoteXML(List<LoteInfo> lotes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID)
                                     ));
                parametros = new Dictionary<string, object>
                                 {
                                     {"@LoteXML", xml.ToString()}
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
        /// Obtiene un diccionario con los parametros necesarios
        /// para la ejecucion del procedimiento almacenado Animal_ObtenerPorDisponibilidad
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorLoteXML(List<LoteInfo> lotes, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                        };
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID",
                                                                  lote.LoteID)
                                     ));
                parametros.Add("@XmlLote", xml.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// Animal_ObtenerPorLotesSacrificadosXML
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorLoteSacrificadosXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID",
                                                                  lote.LoteID)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlLote", xml.ToString()},
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
        /// Obtiene XML necesarios para la ejecucion del
        /// procedimiento almacenado Animal_GuardarAnimalXML
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarAnimalXML(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from animal in animales
                                 select new XElement("Animal",
                                                     new XElement("FolioEntrada", animal.FolioEntrada),
                                                     new XElement("OrganizacionIDEntrada", animal.OrganizacionIDEntrada),
                                                     new XElement("UsuarioCreacionID", animal.UsuarioCreacionID),
                                                     new XElement("Arete", animal.Arete),
                                                     new XElement("Activo", animal.Activo.GetHashCode()),
                                                     new XElement("FechaCompra", animal.FechaCompra),
                                                     new XElement("Paletas", animal.Paletas),
                                                     new XElement("PesoCompra", animal.PesoCompra),
                                                     new XElement("PesoLlegada", animal.PesoLlegada),
                                                     new XElement("PesoAlCorte", animal.PesoAlCorte),
                                                     new XElement("TipoGanadoID", animal.TipoGanadoID),
                                                     new XElement("Venta", animal.Venta),
                                                     new XElement("AreteMetalico", animal.AreteMetalico),
                                                     new XElement("CalidadGanadoID", animal.CalidadGanadoID),
                                                     new XElement("CambioSexo", animal.CambioSexo),
                                                     new XElement("ClasificacionGanadoID", animal.ClasificacionGanadoID),
                                                     new XElement("Cronico", animal.Cronico)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlAnimales", xml.ToString()},
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
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// Animal_ObtenerPorAreteOrganizacionXML
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorAreteOrganizacionXML(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from animal in animales
                                 select new XElement("Animal",
                                                     new XElement("OrganizacionIDEntrada", animal.OrganizacionIDEntrada),
                                                     new XElement("Arete", animal.Arete)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlAnimales", xml.ToString()},
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
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// Animal_ObtenerAnimalesPorXML
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorXML(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from animal in animales
                                 select new XElement("Animales",
                                                     new XElement("AnimalID", animal.AnimalID),
                                                     new XElement("Activo", animal.Activo)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalesXML", xml.ToString()},
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
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// VerificarExistenciaArete
        /// </summary>
        /// <param name="Arete">Arete a verificar</param>
        /// <param name="AreteMetalico">Arete RFID</param>
        /// <returns>Diccionario con los parametros para la consulta</returns>
        internal static Dictionary<string, object> ObtenerParametrosVerificarExisteArete(string Arete, string AreteMetalico, int Organizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", Arete},
                            {"@AreteMetalico", AreteMetalico},
                            {"@Organizacion",Organizacion}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        //internal static Dictionary<string, object> ObtenerParametrosPlancharAretes(List<AnimalInfo> animalesScp, long animalID, int loteID, int usuarioID)
        internal static Dictionary<string, object> ObtenerParametrosPlancharAretes(ControlSacrificioInfo.Planchado_Arete_Request[] planchadoArete, int usuarioID)
        {
            Dictionary<string, object> parametros;
            try
            {
                var xml =    
                    new XElement("ROOT",
                                from animal in planchadoArete
                                 select new XElement("Relacion",
                                                     new XElement("Arete", animal.Arete),
                                                     new XElement("AnimalId", animal.AnimalId)
                                     ));

                parametros = new Dictionary<string, object>
                                 {
                                     {"@Relacion", xml.ToString()},
                                     {"@UsuarioID",usuarioID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPlancharAretes(ControlSacrificioInfo.Planchado_AreteLote_Request planchadoArete, int usuarioID)
        {
            Dictionary<string, object> parametros;
            try
            {

                var sacrificio =    
                    new XElement("ROOT",
                                from sacr in planchadoArete.Sacrificio
                                 select new XElement("Relacion",
                                                        new XElement("Arete", sacr.Arete),
                                                        new XElement("AnimalId", sacr.AnimalId),
                                                        new XElement("LoteId", sacr.LoteID)
                                     ));
                
                var animal =
                    new XElement("ROOT",
                                from ani in planchadoArete.Animal
                                select new XElement("Animal",
                                                    new XElement("Arete", ani.Arete),
                                                    new XElement("LoteId", ani.LoteID)
                                    ));

                parametros = new Dictionary<string, object>
                                 {
                                     {"@xmlSacrificio", sacrificio.ToString()},
                                     {"@xmlPlanchadoLote", animal.ToString()},
                                     {"@FechaSacrificio", planchadoArete.FechaSacrificio}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        

        //--> Ernesto Cardenas Llanes 26 Octubre 2015
        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// SincronizarResumenSacrificio
        /// </summary>
        /// <param name="InformacionSIAP">Informacion recopilada pr la pantalla de Control de Sacrificio</param>
        /// <param name="UsuarioID">Usuario que realizo la operacion</param>
        /// <returns>Diccionario con los parametros para la consulta</returns>
        internal static Dictionary<string, object> ObtenerParametrosSyncResumenSacrificio(List<ControlSacrificioInfo.SincronizacionSIAP> informacionSIAP, int usuarioID)
        {
            Dictionary<string, object> parametros;
            try
            {
                var xml =    
                    new XElement("ROOT",
                                from animal in informacionSIAP
                                 select new XElement("Informacion",
                                                     new XElement("Arete", animal.Arete),
                                                     new XElement("AnimalId", animal.AnimalID),
                                                     new XElement("LoteID", animal.LoteID),
                                                     new XElement("Corral", animal.Corral)
                                     ));

                parametros = new Dictionary<string, object>
                                 {
                                     {"@InformacionSIAP", xml.ToString()},
                                     {"@UsuarioID",usuarioID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        //<-- Ernesto Cardenas Llanes 26 Octubre 2015

        /// <summary>
        /// Metodo para obtener los parametros para obtener los animales del corral
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="esPartida"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesPorCorralDeteccion(int corralID, bool esPartida)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CorralID", corralID},
                            {"@EsPartida", esPartida}
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
        /// Metodo para obtener los parametros para obtener los animales del corral
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalAntiguoCorral(int corralID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CorralID", corralID}
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
