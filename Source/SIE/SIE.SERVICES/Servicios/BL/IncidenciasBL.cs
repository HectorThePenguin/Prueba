using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using BLToolkit.Mapping.MemberMappers;
using CrystalDecisions.Shared;
using org.bouncycastle.asn1.cms;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    internal class IncidenciasBL
    {
        /// <summary>
        /// 
        /// </summary>
        private UsuarioInfo usuario =  new UsuarioInfo();

        List<AlertaInfo> listaAlertaConfiguracion = new List<AlertaInfo>();

        /// <summary>
        /// Genera Incidencias SIAP
        /// </summary>
        internal void GenerarIncidenciasSIAP()
        {
            try
            {

                List<IncidenciasInfo> listaIncidencias = new List<IncidenciasInfo>();
                List<IncidenciasInfo> listaNuevasIncidencias = new List<IncidenciasInfo>();
                List<XDocument> listaNew = new List<XDocument>();
                var usuarioBL = new UsuarioBL();
                usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.AlertaSIAP.ToString());

                IList<OrganizacionInfo> ListaOrganizacion = new List<OrganizacionInfo>();
                var organizacionBL = new OrganizacionBL();
                ListaOrganizacion = organizacionBL.ObtenerTodos();

                var IncidenciasDal = new IncidenciasDAL();
                listaAlertaConfiguracion = IncidenciasDal.ObtenerConfiguracionAlertas(EstatusEnum.Activo);
                listaIncidencias = ObtenerIncidenciasActivas();


                if (listaAlertaConfiguracion != null && listaAlertaConfiguracion.Any())
                {
                    foreach (AlertaInfo alertaInfo in listaAlertaConfiguracion)
                    {
                        List<XDocument> listaNueva = new List<XDocument>();
                        string query = CrearQuery(alertaInfo.ConfiguracionAlerta);

                        XDocument resultadoQuery = IncidenciasDal.EjecutarQuery(query);

                        if (resultadoQuery != null)
                        {
                            if (resultadoQuery.Root != null)
                            {
                                var result = resultadoQuery.Root.Elements().ToList();
                                listaNueva.AddRange(result.Select(xElement => XDocument.Parse(xElement.ToString())));
                            }

                            if (listaIncidencias != null && listaIncidencias.Any())
                            {
                                List<XDocument> listaRegistrada = new List<XDocument>();
                                var incidencias =
                                listaIncidencias.Where(x => x.Alerta.AlertaID == alertaInfo.AlertaID).ToList();
                                listaRegistrada.AddRange(incidencias.Select(incidenciasInfo => XDocument.Parse(incidenciasInfo.XmlConsulta.ToString())));

                                if (incidencias.Any())
                                {

                                    List<XDocument> registradas = new List<XDocument>();

                                    foreach (var xDocument in listaRegistrada)
                                    {
                                        var xdocumetCopy = new XDocument
                                                           (
                                                           new XElement("Table",
                                                                   from row in xDocument.Root.Elements()
                                                                   select new XElement(row.Name, row.Value)));

                                        foreach (var document in listaNueva.Where(document => XNode.DeepEquals(xdocumetCopy, document)))
                                        {
                                            listaNew.Add(document);
                                            registradas.Add(xDocument);
                                        }
                                    }
                                    if (alertaInfo.TerminadoAutomatico == EstatusEnum.Activo)
                                    {
                                        listaRegistrada = listaRegistrada.Except(registradas).ToList();


                                        if (listaRegistrada != null && listaRegistrada.Any())
                                        {
                                            foreach (IncidenciasInfo incidencia in listaRegistrada
                                                .Select(xDocument => incidencias.FirstOrDefault(x => XNode.DeepEquals(x.XmlConsulta, xDocument)))
                                                .Where(incidencia => incidencia != null).Where(incidencia => incidencia.Estatus.EstatusId != Estatus.CerrarAler.GetHashCode()))
                                            {
                                                CerrarIncidenciaAutomatico(incidencia);
                                            }
                                        }
                                    }

                                    listaNueva = listaNueva.Except(listaNew).ToList();

                                }
                            }

                            if (listaNueva.Any())
                            {
                                foreach (var xDocument in listaNueva)
                                {
                                    int organizacionID = 0;


                                    foreach (var xElement in xDocument.Root.Elements())
                                    {
                                        if (xElement.Name.ToString().ToUpper() ==
                                            "OrganizacionID".ToUpper())
                                        {
                                            int xElementOrganizacionID;
                                            if (int.TryParse(xElement.Value, out xElementOrganizacionID))
                                            {
                                                OrganizacionInfo organizacion =
                                                ListaOrganizacion.FirstOrDefault(o => o.OrganizacionID == (int.Parse(xElement.Value))
                                                                                   && o.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode());
                                                if (organizacion != null)
                                                {
                                                    organizacionID = organizacion.OrganizacionID;
                                                }
                                            }
                                            break;
                                        }
                                        if (xElement.Name.ToString().ToUpper() ==
                                            "Nombre_Del_Centro".ToUpper())
                                        {
                                            OrganizacionInfo organizacion =
                                             ListaOrganizacion.FirstOrDefault(o => o.Descripcion.ToUpper() ==
                                                                                   (xElement.Value.ToUpper()));

                                            organizacion =
                                                ListaOrganizacion.FirstOrDefault(
                                                    o =>
                                                        organizacion != null &&
                                                        o.Division.ToUpper() == organizacion.Division.ToUpper() && o.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode());
                                            if (organizacion != null)
                                            {
                                                organizacionID = organizacion.OrganizacionID;
                                            }
                                            break;
                                        }
                                    }

                                    IncidenciasInfo incidenciasInfo = new IncidenciasInfo
                                    {
                                        Organizacion = new OrganizacionInfo
                                        {
                                            OrganizacionID = organizacionID
                                        },

                                        Alerta = new AlertaInfo
                                        {
                                            AlertaID = alertaInfo.AlertaID,
                                            HorasRespuesta = alertaInfo.HorasRespuesta,
                                            ConfiguracionAlerta = new ConfiguracionAlertasInfo
                                            {
                                                NivelAlerta = new NivelAlertaInfo
                                                {
                                                    NivelAlertaId = alertaInfo.ConfiguracionAlerta.NivelAlerta.NivelAlertaId
                                                }
                                            }
                                        },
                                        XmlConsulta = xDocument,
                                        Estatus = new EstatusInfo
                                        {
                                            EstatusId = Estatus.NuevaAlert.GetHashCode()
                                        },
                                        UsuarioCreacionID = usuario.UsuarioID,
                                        Activo = EstatusEnum.Activo
                                    };
                                    listaNuevasIncidencias.Add(incidenciasInfo);
                                }
                            }

                        }
                    }

                }

                using (var transaction = new TransactionScope())
                {
                    if (listaNuevasIncidencias.Any())
                    {

                        GuardarNuevasIncidencias(listaNuevasIncidencias, TipoFolio.AlertaSiap.GetHashCode());
                    }

                    ProcesarIncicencias();

                    transaction.Complete();
                }

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene las configuraciones de alerta
        /// </summary>
        /// <param name="activo"></param>
        internal List<AlertaInfo> ObtenerConfiguracionAlertas(EstatusEnum activo)
        {
            List<AlertaInfo> listaAlertaConfiguracion = null;
            try
            {
                var IncidenciasDal = new IncidenciasDAL();
                listaAlertaConfiguracion = IncidenciasDal.ObtenerConfiguracionAlertas(activo);

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAlertaConfiguracion;
        }

        /// <summary>
        /// Cierra la incidencia
        /// </summary>
        /// <param name="incidencia"></param>
        internal void CerrarIncidenciaAutomatico( IncidenciasInfo incidencia)
        {
            var incidenciaBl = new IncidenciasBL();
            try
            {
                try
                {
                var incidenciaInfo = new IncidenciasInfo
                {
                    IncidenciasID = incidencia.IncidenciasID,
                    Estatus = new EstatusInfo
                    {
                        EstatusId = Estatus.CerrarAler.GetHashCode()
                    },
                    Alerta = new AlertaInfo
                    {
                        AlertaID = incidencia.Alerta.AlertaID,
                        HorasRespuesta = incidencia.Alerta.HorasRespuesta
                    },
                    Folio = incidencia.Folio,
                    Fecha = incidencia.Fecha,
                    FechaVencimiento = incidencia.FechaVencimiento,
                    Comentarios = null,
                    UsuarioModificacionID = usuario.UsuarioID,
                    IncidenciaSeguimiento = new IncidenciaSeguimientoInfo
                    {
                        EstatusAnterior = new EstatusInfo
                        {
                            EstatusId = incidencia.Estatus.EstatusId
                        },
                        AccionAnterior = new AccionInfo
                        {
                            AccionID = incidencia.Accion.AccionID
                        },
                        UsuarioResponsableAnterior = new UsuarioInfo
                        {
                            UsuarioID = incidencia.UsuarioResponsable.UsuarioID
                        },
                        NivelAlertaAnterior = new NivelAlertaInfo
                        {
                            NivelAlertaId = incidencia.NivelAlerta.NivelAlertaId
                        }
                    }

                };
                incidenciaBl.CerrarIncidencia(incidenciaInfo);
                }
                catch (ExcepcionDesconocida ex)
                {
                    var bitacoraBL = new BitacoraIncidenciasBL();
                    var bitacora = new BitacoraIncidenciaInfo
                    {
                        Alerta = new AlertaInfo
                        {
                            AlertaID = incidencia.Alerta.AlertaID
                        },
                        Folio = incidencia.Folio,
                        Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = incidencia.Organizacion.OrganizacionID
                        },
                        Error = ex.Message
                    };
                    bitacoraBL.GuardarErrorIncidencia(bitacora);
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
                                
        }

        /// <summary>
        /// Procese las incidencias para verificar el estado y fecha vencimiento
        /// </summary>
        internal void ProcesarIncicencias()
        {
            var incidenciaBl = new IncidenciasBL();

            try
            {
                 var listaIncidencias = new List<IncidenciasInfo>();
                listaIncidencias = ObtenerIncidenciasActivas();

                if (listaIncidencias != null)
                {
                    listaIncidencias = listaIncidencias.Where(x => x.Estatus.EstatusId != Estatus.CerrarAler.GetHashCode()).ToList();

                    if (listaIncidencias.Any())
                    {
                        foreach (var incidencia in listaIncidencias)
                        {
                            try
                            {
                                AlertaInfo alertaInfo =
                                    listaAlertaConfiguracion.FirstOrDefault(
                                        x => x.AlertaID == incidencia.Alerta.AlertaID);


                                IncidenciasInfo incidenciaInfo;
                                switch (incidencia.Estatus.EstatusId)
                                {
                                    case (int)Estatus.NuevaAlert:
                                    case (int)Estatus.RechaAlert:
                                        //Cambiar estatus a vencidas
                                        if (incidencia.FechaVencimiento <= DateTime.Now && incidencia.Accion.AccionID == 0)
                                        {
                                            incidenciaInfo = new IncidenciasInfo
                                            {
                                                IncidenciasID = incidencia.IncidenciasID,
                                                Estatus = new EstatusInfo
                                                {
                                                    EstatusId = Estatus.VenciAlert.GetHashCode()
                                                },
                                                Alerta = new AlertaInfo
                                                {
                                                    AlertaID = incidencia.Alerta.AlertaID,
                                                    HorasRespuesta = incidencia.Alerta.HorasRespuesta
                                                },
                                                Folio = incidencia.Folio,
                                                Fecha = incidencia.Fecha,
                                                Comentarios = null,
                                                UsuarioModificacionID = usuario.UsuarioID,

                                                IncidenciaSeguimiento = new IncidenciaSeguimientoInfo
                                                {
                                                    EstatusAnterior = new EstatusInfo
                                                    {
                                                        EstatusId = incidencia.Estatus.EstatusId
                                                    },
                                                    AccionAnterior = new AccionInfo
                                                    {
                                                        AccionID = incidencia.Accion.AccionID
                                                    },
                                                    UsuarioResponsableAnterior = new UsuarioInfo
                                                    {
                                                        UsuarioID = incidencia.UsuarioResponsable.UsuarioID
                                                    },
                                                    NivelAlertaAnterior = new NivelAlertaInfo
                                                    {
                                                        NivelAlertaId = incidencia.NivelAlerta.NivelAlertaId
                                                    },
                                                    FechaVencimientoAnterior = incidencia.FechaVencimiento
                                                }


                                            };

                                            incidenciaBl.IncidenciaVencida(incidenciaInfo);
                                        }
                                            //Cambiar estatus a registrada
                                        else if(incidencia.FechaVencimiento <= DateTime.Now && incidencia.Accion.AccionID > 0)
                                        {
                                            incidenciaInfo = new IncidenciasInfo
                                            {
                                                IncidenciasID = incidencia.IncidenciasID,
                                                Estatus = new EstatusInfo
                                                {
                                                    EstatusId = Estatus.RegisAlert.GetHashCode()
                                                },
                                                Alerta = new AlertaInfo
                                                {
                                                    AlertaID = incidencia.Alerta.AlertaID,
                                                    HorasRespuesta = incidencia.Alerta.HorasRespuesta
                                                },
                                                Folio = incidencia.Folio,
                                                Fecha = incidencia.Fecha,
                                                Comentarios = null,
                                                UsuarioModificacionID = usuario.UsuarioID,

                                                IncidenciaSeguimiento = new IncidenciaSeguimientoInfo
                                                {
                                                    EstatusAnterior = new EstatusInfo
                                                    {
                                                        EstatusId = incidencia.Estatus.EstatusId
                                                    },
                                                    AccionAnterior = new AccionInfo
                                                    {
                                                        AccionID = incidencia.Accion.AccionID
                                                    },
                                                    UsuarioResponsableAnterior = new UsuarioInfo
                                                    {
                                                        UsuarioID = incidencia.UsuarioResponsable.UsuarioID
                                                    },
                                                    NivelAlertaAnterior = new NivelAlertaInfo
                                                    {
                                                        NivelAlertaId = incidencia.NivelAlerta.NivelAlertaId
                                                    },
                                                    FechaVencimientoAnterior = incidencia.FechaVencimiento
                                                }


                                            };
                                            incidenciaBl.RegistrarIncidencia(incidenciaInfo);
                                        }
                                        break;
                                    case (int)Estatus.VenciAlert:
                                    case (int)Estatus.RegisAlert:
                                        //Cambiar la incidencia a vencida
                                        if (incidencia.FechaVencimiento <= DateTime.Now)
                                        {
                                            incidenciaInfo = new IncidenciasInfo
                                            {
                                                IncidenciasID = incidencia.IncidenciasID,
                                                Estatus = new EstatusInfo
                                                {
                                                    EstatusId = Estatus.VenciAlert.GetHashCode()
                                                },
                                                Alerta = new AlertaInfo
                                                {
                                                    AlertaID = incidencia.Alerta.AlertaID,
                                                    HorasRespuesta = incidencia.Alerta.HorasRespuesta
                                                },
                                                Folio = incidencia.Folio,
                                                Fecha = incidencia.Fecha,
                                                Comentarios = Properties.ResourceServices.Incidencia_Vencida,
                                                UsuarioModificacionID = usuario.UsuarioID,

                                                IncidenciaSeguimiento = new IncidenciaSeguimientoInfo
                                                {
                                                    EstatusAnterior = new EstatusInfo
                                                    {
                                                        EstatusId = incidencia.Estatus.EstatusId
                                                    },
                                                    AccionAnterior = new AccionInfo
                                                    {
                                                        AccionID = incidencia.Accion.AccionID
                                                    },
                                                    UsuarioResponsableAnterior = new UsuarioInfo
                                                    {
                                                        UsuarioID = incidencia.UsuarioResponsable.UsuarioID
                                                    },
                                                    NivelAlertaAnterior = new NivelAlertaInfo
                                                    {
                                                        NivelAlertaId = incidencia.NivelAlerta.NivelAlertaId
                                                    },
                                                    FechaVencimientoAnterior = incidencia.FechaVencimiento
                                                }
                                            };

                                            incidenciaBl.IncidenciaVencida(incidenciaInfo);
                                        }
                                        
                                        break;
                                }

                            }
                            catch (ExcepcionDesconocida ex)
                            {
                                var bitacoraBL = new BitacoraIncidenciasBL();
                                var bitacora = new BitacoraIncidenciaInfo
                                {
                                    Alerta = new AlertaInfo
                                    {
                                        AlertaID = incidencia.Alerta.AlertaID
                                    },
                                    Folio = incidencia.Folio,
                                    Organizacion = new OrganizacionInfo
                                    {
                                        OrganizacionID = incidencia.Organizacion.OrganizacionID
                                    },
                                    Error = ex.Message
                                };
                                bitacoraBL.GuardarErrorIncidencia(bitacora);
                            }

                        }
                    }
                    
                }

                
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Cambia de estatus la incidencia a registrado
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        internal void RegistrarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                var incidenciasDAL = new IncidenciasDAL();
                incidenciasDAL.RegistrarIncidencia(incidenciaInfo);

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Se cambia de estatus la incidencia a vencidad
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        internal void IncidenciaVencida(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                var incidenciasDAL = new IncidenciasDAL();
                incidenciasDAL.IncidenciaVencida(incidenciaInfo);

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para cerrar una incidencia
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        internal void CerrarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                var incidenciasDAL = new IncidenciasDAL();
                incidenciasDAL.CerrarIncidencia(incidenciaInfo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para autorizar una incidencia
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        internal void AutorizarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                var incidenciasDAL = new IncidenciasDAL();
                incidenciasDAL.AutorizarIncidencia(incidenciaInfo);
            }
            catch (ExcepcionDesconocida ex)
            {
                var bitacoraBL = new BitacoraIncidenciasBL();
                var bitacora = new BitacoraIncidenciaInfo
                {
                    Alerta = new AlertaInfo
                    {
                        AlertaID = incidenciaInfo.Alerta.AlertaID
                    },
                    Folio = incidenciaInfo.Folio,
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = incidenciaInfo.Organizacion.OrganizacionID
                    },
                    Error = ex.Message
                };
                bitacoraBL.GuardarErrorIncidencia(bitacora);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Guarda lista de incidencias
        /// </summary>
        /// <param name="listaNuevasIncidencias"></param>
        private void GuardarNuevasIncidencias(List<IncidenciasInfo> listaNuevasIncidencias, int TipoFolioID)
        {
            try
            {
                var incidenciasDAL = new IncidenciasDAL();
                incidenciasDAL.GuardarNuevasIncidencias(listaNuevasIncidencias, TipoFolioID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene todas las activas
        /// </summary>
        /// <returns></returns>
        internal List<IncidenciasInfo> ObtenerIncidenciasActivas()
        {
            List<IncidenciasInfo> lista;
            try
            {
                Logger.Info();
                var incidenciaDAL = new IncidenciasDAL();
                lista = incidenciaDAL.ObtenerIncidenciasActivas();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Genera Incidencias SIAP
        /// </summary>
        internal List<IncidenciasInfo> ObtenerIncidenciasPorOrganizacionID(int organizacionID, bool usuarioCorporativo)
        {
            List<IncidenciasInfo> lista;
            try
            {
                Logger.Info();
                var incidenciaDAL = new IncidenciasDAL();
                lista = incidenciaDAL.ObtenerIncidenciasPorOrganizacionID(organizacionID, usuarioCorporativo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Proceso para actualizar la incidencia
        /// <param name="incidencia"></param>
        /// </summary>
        internal IncidenciasInfo ActualizarIncidencia(IncidenciasInfo incidencia)
        {
            IncidenciasInfo incidenciaRegreso = null;
            try
            {
                Logger.Info();
                var incidenciaDAL = new IncidenciasDAL();
                incidenciaRegreso = incidenciaDAL.ActualizarIncidencia(incidencia);
            }
            catch (ExcepcionDesconocida ex)
            {
                var bitacoraBL = new BitacoraIncidenciasBL();
                var bitacora = new BitacoraIncidenciaInfo
                {
                    Alerta = new AlertaInfo
                    {
                        AlertaID = incidencia.Alerta.AlertaID
                    },
                    Folio = incidencia.Folio,
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = incidencia.Organizacion.OrganizacionID
                    },
                    Error = ex.Message
                };
                bitacoraBL.GuardarErrorIncidencia(bitacora);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return incidenciaRegreso;
        }

        /// <summary>
        /// Proceso para rechazar la incidencia
        /// <param name="incidencia"></param>
        /// </summary>
        internal IncidenciasInfo RechazarIncidencia(IncidenciasInfo incidencia)
        {
            IncidenciasInfo incidenciaRegreso = null;
            try
            {
                Logger.Info();
                var incidenciaDAL = new IncidenciasDAL();
                incidenciaRegreso = incidenciaDAL.RechazarIncidencia(incidencia);
            }
            catch (ExcepcionDesconocida ex)
            {
                var bitacoraBL = new BitacoraIncidenciasBL();
                var bitacora = new BitacoraIncidenciaInfo
                {
                    Alerta = new AlertaInfo
                    {
                        AlertaID = incidencia.Alerta.AlertaID
                    },
                    Folio = incidencia.Folio,
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = incidencia.Organizacion.OrganizacionID
                    },
                    Error = ex.Message
                };
                bitacoraBL.GuardarErrorIncidencia(bitacora);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return incidenciaRegreso;
        }

        /// <summary>
        /// Proceso para obtener el seguimiento de una incidencia
        /// <param name="incidenciaID"></param>
        /// </summary>
        internal List<IncidenciaSeguimientoInfo> ObtenerSeguimientoPorIncidenciaID(int incidenciaID)
        {
            List<IncidenciaSeguimientoInfo> listaSeguimiento;
            try
            {
                Logger.Info();
                var incidenciaDAL = new IncidenciasDAL();
                listaSeguimiento = incidenciaDAL.ObtenerSeguimientoPorIncidenciaID(incidenciaID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaSeguimiento;
        }

        /// <summary>
        /// Generacion de querys
        /// </summary>
        /// <param name="configuracionAlertas"></param>
        /// <returns></returns>
        private string CrearQuery(ConfiguracionAlertasInfo configuracionAlertas)
        {
            var query = String.Format("SELECT  {0} {1}  FROM {2} {3} WHERE {4} {5} {6}", 
                configuracionAlertas.Datos, Environment.NewLine, configuracionAlertas.Fuentes, 
                Environment.NewLine, configuracionAlertas.Condiciones, Environment.NewLine, 
                configuracionAlertas.Agrupador == string.Empty ? string.Empty : "GROUP BY " + configuracionAlertas.Agrupador);
            return query;
        }


    }
}
