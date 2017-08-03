using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxEntradaGanadoDAL
    {
        /// <summary>
        /// Metodo que obtiene los parametros para guardar una entrada
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                int? corralID = entradaGanado.CorralID;

                corralID = corralID == 0 ? null : corralID;

                int? loteID = entradaGanado.Lote.LoteID;
                loteID = loteID == 0 ? null : loteID;

                parametros = new Dictionary<string, object>
                    {                        
                        {"@OrganizacionID", entradaGanado.OrganizacionID},
                        {"@OrganizacionOrigenID", entradaGanado.OrganizacionOrigenID},
                        {"@FechaEntrada", entradaGanado.FechaEntrada},
                        {"@EmbarqueID", entradaGanado.EmbarqueID},
                        {"@FolioOrigen", entradaGanado.FolioOrigen},
                        {"@FechaSalida", entradaGanado.FechaSalida},
                        {"@ChoferID", entradaGanado.ChoferID},
                        {"@JaulaID", entradaGanado.JaulaID},
                        {"@CabezasOrigen", entradaGanado.CabezasOrigen},
                        {"@CabezasRecibidas", entradaGanado.CabezasRecibidas},
                        {"@OperadorID", entradaGanado.OperadorID},
                        {"@PesoBruto", entradaGanado.PesoBruto},
                        {"@PesoTara", entradaGanado.PesoTara},
                        {"@EsRuteo", entradaGanado.EsRuteo},
                        {"@Fleje", entradaGanado.Fleje},
                        {"@CheckList", entradaGanado.CheckList},
                        {"@CorralID", corralID },
                        {"@LoteID", loteID},
                        {"@CamionID", entradaGanado.CamionID},
                        {"@Observacion", entradaGanado.Observacion},
                        {"@ImpresionTicket", entradaGanado.ImpresionTicket},
                        {"@Costeado", entradaGanado.Costeado},
                        {"@Manejado", entradaGanado.Manejado},
                        {"@Activo", entradaGanado.Activo},                        
                        {"@Usuario", entradaGanado.UsuarioCreacionID},    
                        {"@TipoFolio", TipoFolio.EntradaGanado.GetHashCode()},
                        {"@Guia", entradaGanado.Guia},
                        {"@Factura", entradaGanado.Factura},
                        {"@Poliza", entradaGanado.Poliza},
                        {"@HojaEmbarque", entradaGanado.HojaEmbarque},
                        {"@ManejoSinEstres", entradaGanado.ManejoSinEstres},
                        {"@CertificadoZoosanitario", entradaGanado.CertificadoZoosanitario},
                        {"@PruebasTB", entradaGanado.PruebasTB},
                        {"@PruebasTR", entradaGanado.PruebasTR},
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
        /// Metodo que obtiene los parametros para guardar una lista de entradas de condiciones 
        /// </summary>
        /// <param name="listaEntradaCondicion"></param>
        /// <param name="entradaGanadoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoCondicion(IEnumerable<EntradaCondicionInfo> listaEntradaCondicion, int entradaGanadoID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlCondiciones = new XElement("ROOT",
                                        from entradaCondicion in listaEntradaCondicion
                                        select new XElement("EntradaGanado",
                                                    new XElement("EntradaCondicionID", entradaCondicion.EntradaCondicionID),
                                                    new XElement("EntradaGanadoID", entradaGanadoID),
                                                    new XElement("CondicionID", entradaCondicion.CondicionID),
                                                    new XElement("Cabezas", entradaCondicion.Cabezas),
                                                    new XElement("Activo", Convert.ToBoolean(entradaCondicion.Activo)),
                                                    new XElement("RegistroModificado", entradaCondicion.RegistroModificado),
                                                    new XElement("Usuario", entradaCondicion.UsuarioID)));

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlCondiciones", xmlCondiciones.ToString()},                        
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
        /// Metodo que obtiene los parametros para guardar una entrada
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EntradaGanadoID", entradaGanado.EntradaGanadoID},                                                
                        {"@OrganizacionOrigenID", entradaGanado.OrganizacionOrigenID},
                        {"@FechaEntrada", entradaGanado.FechaEntrada},
                        {"@EmbarqueID", entradaGanado.EmbarqueID},
                        {"@FolioOrigen", entradaGanado.FolioOrigen},
                        {"@FechaSalida", entradaGanado.FechaSalida},
                        {"@ChoferID", entradaGanado.ChoferID},
                        {"@JaulaID", entradaGanado.JaulaID},
                        {"@CabezasOrigen", entradaGanado.CabezasOrigen},
                        {"@CabezasRecibidas", entradaGanado.CabezasRecibidas},
                        {"@OperadorID", entradaGanado.OperadorID},
                        {"@PesoBruto", entradaGanado.PesoBruto},
                        {"@PesoTara", entradaGanado.PesoTara},
                        {"@EsRuteo", entradaGanado.EsRuteo},
                        {"@Fleje", entradaGanado.Fleje},
                        {"@CheckList", entradaGanado.CheckList},
                        {"@CorralID", entradaGanado.CorralID},
                        {"@LoteID", entradaGanado.Lote.LoteID},
                        {"@Observacion", entradaGanado.Observacion},                                                
                        {"@Activo", entradaGanado.Activo},                        
                        {"@Usuario", entradaGanado.UsuarioModificacionID},     
                        {"@ImpresionTicket", entradaGanado.ImpresionTicket},
                        {"@Guia", entradaGanado.Guia},
                        {"@Factura", entradaGanado.Factura},
                        {"@Poliza", entradaGanado.Poliza},
                        {"@HojaEmbarque", entradaGanado.HojaEmbarque},
                        {"@ManejoSinEstres", entradaGanado.ManejoSinEstres},
                        {"@CamionID", entradaGanado.CamionID},
                        {"@CertificadoZoosanitario", entradaGanado.CertificadoZoosanitario},
                        {"@PruebaTB", entradaGanado.PruebasTB},
                        {"@PruebaTR", entradaGanado.PruebasTR},
                        {"@CondicionJaulaID", entradaGanado.CondicionJaula.CondicionJaulaID},
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por ID 
        /// </summary>
        /// <param name="entradaGanadoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorId(int entradaGanadoID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EntradaGanadoID", entradaGanadoID}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioEntrada(int folioEntrada, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", folioEntrada},
                        {"@OrganizacionID", organizacionID}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por ID de Organizacion
        /// </summary>        
        /// <param name="organizacionID"></param>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorIDOrganizacion(int organizacionID, int tipoCorralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@TipoCorralID",   tipoCorralID}
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
        /// Metodo que obtiene los parametros para obtener un listado de entradas de ganado activas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasActivasPorPagina(PaginacionInfo pagina, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {                        
                        {"@OrganizacionID", organizacionID},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada
        /// </summary>
        /// <param name="entradaGanadoInfo">Folio a Buscar</param>
        /// <param name="dependencias">Dependencias que se tenga hacia otros Catalogos</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioEntrada(EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaGanadoInfo.Observacion}
                    };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada paginado
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se Realizara la Paginacion</param>
        /// <param name="entradaGanadoInfo">Folio a Buscar</param>
        /// <param name="dependencias">Dependencias que se tenga hacia otros Catalogos</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasPorPagina(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaGanadoInfo.FolioEntrada},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada paginado
        /// </summary>
        /// <param name="Pagina">Objeto con el Cual se Realizara la Paginacion</param>
        /// <param name="FolioEntrada">Folio a Buscar</param>
        /// <param name="Dependencias">Dependencias que se tenga hacia otros Catalogos</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasPorPagina(PaginacionInfo Pagina, int FolioEntrada, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", FolioEntrada},
                        {"@Inicio", Pagina.Inicio},
                        {"@Limite", Pagina.Limite}
                    };
                AuxDAL.ObtenerDependencias(parametros, Dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEmbarqueID(int embarqueID, int organizacionOrigenID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", embarqueID},
                                     {"@OrganizacionOrigenID", organizacionOrigenID}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEmbarqueID(int embarqueID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@EmbarqueID", embarqueID},
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada
        /// </summary>
        /// <param name="folioEntrada">Folio a Buscar</param>
        /// <param name="Dependencias">Dependencias que se tenga hacia otros Catalogos</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioEntrada(int folioEntrada, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", folioEntrada}
                    };
                AuxDAL.ObtenerDependencias(parametros, Dependencias);
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
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, EntradaGanadoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", filtro.CodigoCorral},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        /// Metodo que obtiene los parametros para obtener las partidas programadas
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaProgramadas(int folioEntrada, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", folioEntrada},
                        {"@OrganizacionID", organizacionID}
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
        /// Metodo que obtiene los parametros para obtener las partidas programadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaProgramadasPorPaginas(PaginacionInfo pagina, int folioEntrada, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", folioEntrada},
                        {"@OrganizacionID", organizacionID},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        internal static Dictionary<string, object> ObtenerCalidadPorSexo(string sexo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Sexo", sexo}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Tipo de Corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorTipoCorral(int organizacionID, int tipoCorralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@TipoCorralID", tipoCorralID}
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
        /// Metodo que obtiene los parametros para guardar una entrada
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCosteado(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EntradaGanadoID", entradaGanado.EntradaGanadoID},                                                
                        {"@Usuario", entradaGanado.UsuarioModificacionID},     
                        {"@Costeado", entradaGanado.Costeado},
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada paginado
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se Realizara la Paginacion</param>
        /// <param name="entradaGanadoInfo">Folio a Buscar</param>
        /// <param name="dependencias">Dependencias que se tenga hacia otros Catalogos</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasPorPaginaRecibidoDependencia(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaGanadoInfo.FolioEntrada},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@Observacion", entradaGanadoInfo.Observacion ?? string.Empty}
                    };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada paginado
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se Realizara la Paginacion</param>
        /// <param name="entradaGanadoInfo">Folio a Buscar</param>
        /// <param name="dependencias">Dependencias que se tenga hacia otros Catalogos</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasPorPaginaCosteadoDependencia(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaGanadoInfo.FolioEntrada},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@Observacion", entradaGanadoInfo.Observacion ?? string.Empty}
                    };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Metodo que obtiene los parametros para consultar la captura de Calidad de Ganado
        /// </summary>
        /// <param name="filtroCalificacionGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerEntradaGanadoCapturaCalidad(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", filtroCalificacionGanado.FolioEntrada},                                                
                        {"@OrganizacionID", filtroCalificacionGanado.OrganizacionID}
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
        /// Metodo que obtiene los parametros para actualizar las cabezas muertas
        /// </summary>
        /// <param name="filtroCalificacionGanadoInfo"></param>

        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCabezasMuertas(FiltroCalificacionGanadoInfo filtroCalificacionGanadoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EntradaGanadoID", filtroCalificacionGanadoInfo.EntradaGanadoID},                                                
                        {"@CabezasMuertas", filtroCalificacionGanadoInfo.CabezasMuertas},
                        {"@UsuarioID", filtroCalificacionGanadoInfo.UsuarioID},
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Tipo de Corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCorralDisponible(int organizacionID, int corralID, int embarqueID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@CorralID", corralID},
                        {"@EmbarqueID", embarqueID}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEntradaPorLote(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", lote.LoteID},
                        {"@CorralID", lote.CorralID},
                        {"@OrganizacionID", lote.OrganizacionID}
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
        /// Obtiene un diccionario de parametros para la ejecucion del
        /// procedimiento almacenado EntradaGanado_ObtenerPorFolioEntradaOrganizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioEntradaPorOrganizacion(int folioEntrada, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", folioEntrada},
                        {"@OrganizacionID", organizacionID}
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
        /// Metodo para mapear los parametros para obtener las entradas costeadas sin prorratear
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerEntradaCorralLote(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CorralID", lote.CorralID},
                        {"@LoteID", lote.LoteID}
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
        /// Metodo para mapear los parametros para obtener las entradas costeadas sin prorratear
        /// </summary>
        /// <param name="tipoOrganizacion"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerEntradaCosteadasSinProrratear(int tipoOrganizacion)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoOrganizacion", tipoOrganizacion}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosEntradaPaginado(PaginacionInfo pagina, EntradaGanadoInfo entradaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaInfo.FolioEntrada},
                        {"@OrganizacionID", entradaInfo.OrganizacionID},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@Observacion", entradaInfo.Observacion}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorGanadoRecibidas(EntradaGanadoInfo entradaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaInfo.Observacion},
                        {"@OrganizacionID", entradaInfo.OrganizacionID},
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
        /// Obtiene un diccionario con los parametros necesarios para
        /// la ejecucion del procedimiento almacenado EntradaGanado_ObtenerEntradaPorLoteXML
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEntradaPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlCondiciones = new XElement("ROOT",
                                                  from lote in lotes
                                                  select new XElement("CorralesLotes",
                                                                      new XElement("CorralID", lote.CorralID),
                                                                      new XElement("LoteID", lote.LoteID)));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlCorralLote", xmlCondiciones.ToString()},
                        {"@OrganizacionID", organizacionId},
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
        /// 
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuarioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCorral(LoteInfo lote, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@LoteID", lote.LoteID},
                    {"@CorralID", corralInfoDestino.CorralID},
                    {"@UsuarioModificacionID", usuarioInfo.UsuarioID}
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
        /// Metodo para obtener una partida de compra directa de una lista de partidas para un lote
        /// </summary>
        /// <param name="entradaSeleccionada"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPartidaCompraDirecta(EntradaGanadoInfo entradaSeleccionada)
        {


            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var lista = entradaSeleccionada.FolioEntradaAgrupado.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("Partidas",
                                                new XElement("NoPartida", partida))
                                   );
                parametros = new Dictionary<string, object>
                {
                    {"@NoPartida", xml.ToString()},
                    {"@LoteID", entradaSeleccionada.LoteID}
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
        /// del procedimiento almacenado EntradaGanado_ObtenerEntradaPorIDXML
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEntradasPorIDs(List<int> entradas)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlCondiciones = new XElement("ROOT",
                                                  from entrada in entradas
                                                  select new XElement("EntradaGanado",
                                                                      new XElement("EntradaGanadoID", entrada)));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlEntradaGanadoID", xmlCondiciones.ToString()},
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado EntradaGanado_ActualizarCabezasOrigen
        /// </summary>
        /// <param name="entradaGanadoID"> </param>
        /// <param name="cabezasOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ActualizaCabezasOrigen(int entradaGanadoID, int cabezasOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CabezasOrigen", cabezasOrigen},
                        {"@EntradaGanadoID", entradaGanadoID}
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
        /// del procedimiento almacenado EntradaGanado_ObtenerFolioEntradaXML
        /// </summary>
        /// <param name="foliosOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioOrigenXML(List<EntradaGanadoInfo> foliosOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlFolios = new XElement("ROOT",
                                                  from entrada in foliosOrigen
                                                  select new XElement("EntradaGanado",
                                                                      new XElement("FolioEntrada", entrada.FolioOrigen),
                                                                      new XElement("OrganizacionID", entrada.OrganizacionID)));
                parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlFolioEntrada", xmlFolios.ToString()},
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado EntradaGanado_ObtenerImpresionTarjetaRecepcion
        /// </summary>
        /// <param name="filtro"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasImpresionTarjetaRecepcion(FiltroImpresionTarjetaRecepcion filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@FechaEntrada", filtro.Fecha},
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado EntradaGanado_ObtenerImpresionTarjetaRecepcion
        /// </summary>
        /// <param name="filtro"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasImpresionCalidadGanado(FiltroImpresionCalidadGanado filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@FechaEntrada", filtro.Fecha},
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioEntradaCortadaIncompleta(EntradaGanadoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", filtro.FolioEntrada},
                        {"@OrganizacionID", filtro.OrganizacionID}
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
        /// Metodo que obtiene los parametros para obtener una entrada de ganado por Folio de entrada paginado
        /// </summary>
        /// <param name="pagina">Objeto con el Cual se Realizara la Paginacion</param>
        /// <param name="entradaGanadoInfo">Folio a Buscar</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaGanadoPaginaCortadasIncompletas(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", entradaGanadoInfo.FolioEntrada},
                        {"@OrganizacionID", entradaGanadoInfo.OrganizacionID},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@OrganizacionOrigen", entradaGanadoInfo.OrganizacionOrigen ?? string.Empty}
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
        /// Metodo que obtiene los parametros para obtener los Aretes de una partida
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReemplazoArete(EntradaGanadoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FolioEntrada", filtro.FolioEntrada},
                        {"@OrganizacionID", filtro.OrganizacionID}
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
        /// Metodo que obtiene los parametros para guardar el reemplazo de aretes
        /// </summary>
        /// <param name="listaAretes"></param>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReemplazoAretes(List<FiltroAnimalesReemplazoArete> listaAretes, EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlAretes = new XElement("ROOT",
                                             from arete in listaAretes
                                             select new XElement("Aretes",
                                                                 new XElement("AreteCentro", arete.AreteCentro),
                                                                 new XElement("AreteCorte", arete.AreteCorte)
                                                                 )
                                             );

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAretes", xmlAretes.ToString()},
                        {"@FolioOrigen", entradaGanado.FolioOrigen},
                        {"@OrganizacionOrigenID", entradaGanado.OrganizacionOrigenID},
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
        /// Metodo que obtiene los parametros para obtener las cabezas de las entradas de ganado
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCabezasEntradasRuteo(int embarqueID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", embarqueID}
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
        /// obtener parametros consultar entrada ganado por loteID y corralID
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaGanadoLoteCorral(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CorralID", lote.CorralID},
                        {"@LoteID",lote.LoteID},
                        {"@OrganizacionID",lote.OrganizacionID}
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
