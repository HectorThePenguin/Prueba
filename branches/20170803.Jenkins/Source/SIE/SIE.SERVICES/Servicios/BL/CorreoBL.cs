using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Properties;

namespace SIE.Services.Servicios.BL
{
    internal class CorreoBL
    {
        /// <summary>
        /// Envia correo electronico
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="correoAenviar"></param>
        /// <returns></returns>
        internal ResultadoOperacion EnviarCorreoElectronicoInsidencia(OrganizacionInfo organizacion, CorreoInfo correoAenviar)
        {


            var resultado = new ResultadoOperacion();
            try
            {

                var bitacoraBl = new BitacoraErroresDAL();

                var notificaciones = bitacoraBl.ObtenerNotificacionesPorAcciones(correoAenviar.AccionSiap);

                if (notificaciones == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.Correo_ErrorParametrosCorreoDestino;
                    return resultado;
                }
                if (notificaciones.Count == 0)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.Correo_SinDestinatario;
                    return resultado;
                }
                correoAenviar.Correos = new List<string>();
                foreach (var destinatario in notificaciones)
                {
                    correoAenviar.Correos.Add(destinatario.UsuarioDestino);
                }

                resultado = EnviarCorreo(correoAenviar, organizacion);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                resultado.Resultado = false;
                resultado.Mensaje = ResourceServices.Correo_ErrorInesperado;
                return resultado;
            }
          
            return resultado;
        }
        /// <summary>
        /// Enviar corre electronico
        /// </summary>
        /// <param name="correoAenviar"></param>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal ResultadoOperacion EnviarCorreo(CorreoInfo correoAenviar, OrganizacionInfo organizacion)
        {
            var resultado = new ResultadoOperacion{Resultado = true};
            try
            {

                var smtp = new SmtpClient();
                var correo = new MailMessage();
                var parametrosBl = new ConfiguracionParametrosBL();
                var parametrosServidorSmtp = new ConfiguracionParametrosInfo
                {
                    OrganizacionID = organizacion.OrganizacionID,
                    TipoParametro = (int) TiposParametrosEnum.ConfiguracionSmtp
                };
                IList<ConfiguracionParametrosInfo> datosServidorSmtp =
                    parametrosBl.ObtenerPorOrganizacionTipoParametro(parametrosServidorSmtp);

                if (datosServidorSmtp == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.Correo_ErrorParametrosServidor;
                    return resultado;
                }

                ServidorSmtpInfo servidorSmtp = ObtenerInformacionServidor(datosServidorSmtp);

                if (servidorSmtp == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.Correo_ErrorParametrosServidor;
                    return resultado;
                }
                correo.From = new MailAddress(servidorSmtp.Cuenta, correoAenviar.NombreOrigen);

                if (correoAenviar.Correos == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.Correo_SinDestinatario;
                    return resultado;
                }

                foreach (var destinatario in correoAenviar.Correos)
                {
                    correo.To.Add(destinatario);
                }

                correo.Body = correoAenviar.Mensaje;
                correo.Subject = correoAenviar.Asunto;
                correo.SubjectEncoding = Encoding.UTF8;

                if(!string.IsNullOrWhiteSpace(correoAenviar.ArchivoAdjunto))
                {
                    var archivoAdjunto = new Attachment(correoAenviar.ArchivoAdjunto);
                    correo.Attachments.Add(archivoAdjunto);
                }
                

                correo.BodyEncoding = Encoding.UTF8;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.High;

                smtp.Host = servidorSmtp.Servidor;
                smtp.Port = int.Parse(servidorSmtp.Puerto);
                smtp.EnableSsl = servidorSmtp.RequiereSsl;

                smtp.Credentials = new NetworkCredential(servidorSmtp.Cuenta, servidorSmtp.Autentificacion);
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los datos del servidor
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private ServidorSmtpInfo ObtenerInformacionServidor(IList<ConfiguracionParametrosInfo> parametros)
        {
            var servidor = new ServidorSmtpInfo();
            try
            {
                foreach (var parametro in parametros)
                {
                    if (parametro.Clave == ParametrosEnum.puerto.ToString())
                    {
                        servidor.Puerto = parametro.Valor;
                    }
                    else
                    {
                        if (parametro.Clave == ParametrosEnum.correoOrigen.ToString())
                        {
                            servidor.Cuenta = parametro.Valor;
                        }
                        else
                        {
                            if (parametro.Clave == ParametrosEnum.servidorSmtp.ToString())
                            {
                                servidor.Servidor = parametro.Valor;
                            }
                            else
                            {
                                if (parametro.Clave == ParametrosEnum.autentificacion.ToString())
                                {
                                    servidor.Autentificacion = parametro.Valor;
                                }
                                else
                                {
                                    if (parametro.Clave == ParametrosEnum.requiereSsl.ToString())
                                    {
                                        bool requiereSsl;
                                        servidor.RequiereSsl = bool.TryParse(parametro.Valor, out requiereSsl) && requiereSsl;
                                    }
                                }
                            }
                        }
                    }
                           
                }
            }
            catch (Exception)
            {
                servidor = null;
            }
            return servidor;
        }
        /// <summary>
        /// Obtiene las notiificaciones para una accion
        /// </summary>
        /// <param name="accionSiap">Accion siap</param>
        /// <returns></returns>
        internal List<NotificacionesInfo> ObtenerNotificacionesPorAcciones(AccionesSIAPEnum accionSiap)
        {
            List<NotificacionesInfo> resultado;
            try
            {
                Logger.Info();
                var bitacorraDal = new BitacoraErroresDAL();
                resultado = bitacorraDal.ObtenerNotificacionesPorAcciones(accionSiap);
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
            return resultado;
        }
    }
}
