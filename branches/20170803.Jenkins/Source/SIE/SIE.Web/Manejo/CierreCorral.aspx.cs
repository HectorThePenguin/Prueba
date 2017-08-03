using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Manejo
{
    public partial class CierreCorral : PageBase
    {
        private static readonly int[] ObservacionesPisoComedero = new[] { 1 };
        private static readonly int[] ObservacionesCondicionComedero = new[] { 2 };
        private static readonly int[] ObservacionesCablesComedero = new[] { 3 };
        private static readonly int[] ObservacionesPisoBebedero = new[] { 1 };
        private static readonly int[] ObservacionesCondicionBebedero = new[] { 4 };
        private static readonly int[] ObservacionesSeguridadPuerta = new[] { 5 };
        private static readonly int[] ObservacionesPisoCorral = new[] { 6, 7 };
        private static readonly int[] ObservacionesSombras = new[] { 10 };
        private static readonly int[] ObservacionesCallejon = new[] { 7, 8 };
        private static readonly int[] ObservacionesPerimetro = new[] { 9 };


        protected void Page_Load(object sender, EventArgs e)
        {
            //hfOrganizacionID.Value = "4";
            var seguridad = (SeguridadInfo)ObtenerSeguridad();
            hfOrganizacionID.Value = seguridad.Usuario.Organizacion.OrganizacionID.ToString(CultureInfo.InvariantCulture);
            hfErrorImprimir.Value = Session["ErrorCheckList"] != null ? Session["ErrorCheckList"].ToString() : string.Empty;
            Session["ErrorCheckList"] = null;
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <param name="filtroCierreCorral">filtros donde viene la OrganizacionID</param>
        /// <returns></returns>
        [WebMethod]
        public static List<CheckListCorralInfo> TraerCorrales(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                var lotePL = new LotePL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroCierreCorral.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var corrales = lotePL.ObtenerCheckListCorral(filtroCierreCorral);
                return corrales;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la Lista de Conceptos
        /// </summary>
        /// <param name="filtroCierreCorral">filtros donde viene la OrganizacionID</param>
        /// <returns></returns>
        [WebMethod]
        public static CheckListCorralInfo TraerCorralCheckList(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                var lotePL = new LotePL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroCierreCorral.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var corral = lotePL.ObtenerCheckListCorralCompleto(filtroCierreCorral);
                if (corral == null)
                {
                    return null;
                }
                corral.ListaAcciones = TraerAcciones();
                corral.ListaConceptos = TraerConceptos();
                return corral;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la Lista de Conceptos
        /// </summary>
        /// <returns></returns>
        public static List<ConceptoInfo> TraerConceptos()
        {
            try
            {
                var conceptos = new List<ConceptoInfo>
                    {
                        new ConceptoInfo
                            {
                                ConceptoDescripcion = "HOMOGÉNEO"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "PCHICAS"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "PGRANDES"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "MADUREZ"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "CONFORMA"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "DESECHOS"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "ENFERMOS"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "GRASA"
                            },
                            new ConceptoInfo
                            {
                                ConceptoDescripcion = "REVUELTO"
                            },
                                new ConceptoInfo
                            {
                                ConceptoDescripcion = "AGUA BEBEDERO"
                            },
                                  new ConceptoInfo
                            {
                                ConceptoDescripcion = "CORTE AL REIMPLANTE",
                                AplicaSiNo = true
                            }
                    };
                return conceptos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la Lista de Acciones
        /// </summary>
        /// <returns></returns>
        public static List<AccionInfo> TraerAcciones()
        {
            try
            {
                var listaObservaciones = ObtenerObservaciones();
                var acciones = new List<AccionInfo>
                    {
                        new AccionInfo
                            {
                                Descripcion = "PISO COMEDERO",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesPisoComedero.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "CONDICIÓN COMEDERO",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesCondicionComedero.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "CABLES COMEDERO",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesCablesComedero.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "PISO BEBEDERO",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesPisoBebedero.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "CONDICIÓN BEBEDERO",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesCondicionBebedero.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                             new AccionInfo
                            {
                                Descripcion = "SEGURIDAD PUERTA",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesSeguridadPuerta.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "PISO CORRAL",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesPisoCorral.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "SOMBRAS",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesSombras.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "CALLEJÓN",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesCallejon.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            },
                        new AccionInfo
                            {
                                Descripcion = "PERÍMETRO",
                                ListaObservaciones = listaObservaciones.Where(ob => ObservacionesPerimetro.Contains(ob.TipoObservacion.TipoObservacionID)).ToList()
                            }
                    };
                return acciones;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Consultar la Lista de Observaciones
        /// </summary>
        /// <returns></returns>
        public static List<ObservacionInfo> ObtenerObservaciones()
        {
            var observacionPL = new ObservacionPL();
            var observaciones = observacionPL.ObtenerTodos(EstatusEnum.Activo);
            return observaciones.OrderBy(obs => obs.Descripcion).ToList();
        }

        /// <summary>
        /// Metodo para Guardar el CheckList
        /// </summary>
        /// <param name="checkListCorralInfo">contenedor donde se encuentra la información del Check List</param>
        /// <returns></returns>
        [WebMethod]
        public static string Guardar(CheckListCorralInfo checkListCorralInfo)
        {
            try
            {
                var rutaArchivos = string.Format("{0}{1}", HttpRuntime.AppDomainAppPath, "\\Manejo\\ArchivosCheckList\\XML CheckList.xml");
                XDocument xml = XDocument.Load(rutaArchivos);
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                checkListCorralInfo.Activo = EstatusEnum.Activo;
                checkListCorralInfo.UsuarioCreacionID = seguridad.Usuario.UsuarioID;
                checkListCorralInfo.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var checkListCorralPL = new CheckListCorralPL();

                checkListCorralPL.Guardar(checkListCorralInfo, xml);
                return "OK";
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}