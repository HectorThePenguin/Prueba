using System;
using System.Collections.Generic;
using System.Web.Services;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Filtros;

namespace SIE.Web.PlantaAlimentos
{
    public partial class CheckListRolado : PageBase
    {
        #region PAGE LOAD

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion PAGE LOAD

        #region METODOS WEB

        [WebMethod]
        public static UsuarioInfo ObtenerUsuarioLogeado(int usuarioID)
        {
            var usuarioPL = new UsuarioPL();
            UsuarioInfo usuario = usuarioPL.ObtenerSupervisorID(usuarioID);
            return usuario;
        }

        [WebMethod]
        public static IList<CheckListRoladoraInfo> ObtenerNotificaciones()
        {
            IList<CheckListRoladoraInfo> notificaciones;
            using (var checkListRoladoraBL = new CheckListRoladoraBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                notificaciones = checkListRoladoraBL.ObtenerNotificaciones(organizacionID);
            }
            return notificaciones;
        }

        [WebMethod]
        public static CheckListRoladoraInfo ObtenerPorTurno(int turno)
        {
            CheckListRoladoraInfo datosGenerales;
            using (var checkListRoladoraBL = new CheckListRoladoraBL())
            {
                int organizacionID = 0;
                var usuario = ObtenerSeguridad() as SeguridadInfo;
                if (usuario != null)
                {
                    organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
                }
                datosGenerales = checkListRoladoraBL.ObtenerPorTurno(organizacionID, turno);
                if (datosGenerales != null)
                {
                    datosGenerales.Usuario = usuario.Usuario;
                }
            }
            return datosGenerales;
        }

        [WebMethod]
        public static dynamic ObtenerCheckList(int turno, int roladoraId)
        {
            dynamic checkList;
            using (var checkListRoladoraBL = new CheckListRoladoraBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                checkList = checkListRoladoraBL.ObtenerCheckList(organizacionID, turno, roladoraId);
            }
            return checkList;
        }

        [WebMethod]
        public static dynamic ObtenerCheckListCompleto(int turno, int roladoraId)
        {
            dynamic checkList;
            using (var checkListRoladoraDetalleBL = new CheckListRoladoraDetalleBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                checkList = checkListRoladoraDetalleBL.ObtenerCheckListCompleto(organizacionID, turno, roladoraId);
            }
            return checkList;
        }

        [WebMethod]
        public static dynamic ObtenerParametros()
        {
            dynamic checkList;
            using (var checkListRoladoraAccionBL = new CheckListRoladoraAccionBL())
            {
                checkList = checkListRoladoraAccionBL.ObtenerParametros();
            }
            return checkList;
        }

        [WebMethod]
        public static dynamic ValidarRango(int preguntaId, int rangoId, string parametros)
        {
            dynamic clase;
            using (var checkListRoladoraRangoBL = new CheckListRoladoraRangoBL())
            {
                clase = checkListRoladoraRangoBL.ObtenerClaseRango(preguntaId, rangoId, parametros);
            }
            return clase;
        }

        [WebMethod]
        public static FiltroUsuarioInfo ValidarCredencialesUsuario(string usuario, string contraseña)
        {
            var usuarioPL = new UsuarioPL();
            FiltroUsuarioInfo filtroUsuario = usuarioPL.ValidarUsuarioCompleto(usuario.Trim(), contraseña);
            return filtroUsuario;
        }

        [WebMethod]
        public static string GuardarCheckList(CheckListRoladoraInfo checkListRoladora
                                            , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                            , List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle)
        {
            using (var checkListRoladoBL = new CheckListRoladoraBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                checkListRoladoBL.GuardarCheckList(checkListRoladora, checkListRoladoraGeneral,
                                                   checkListRoladoraDetalle, organizacionID);
            }
            string resultado = NotificacionesActuales();
            return resultado;
        }

        [WebMethod]
        public static string GuardarParametrosCheckList(List<CheckListRoladoraInfo> listaCheckListRoladora
                                                      , CheckListRoladoraGeneralInfo checkListRoladoraGeneral
                                                      ,  List<CheckListRoladoraHorometroInfo> listaCheckListRoladoraHorometro)
        {
            using (var checkListRoladoBL = new CheckListRoladoraBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                checkListRoladoBL.GuardarParametrosCheckList(listaCheckListRoladora, checkListRoladoraGeneral, listaCheckListRoladoraHorometro, organizacionID);
            }
            string resultado = NotificacionesActuales();
            return resultado;
        }

        [WebMethod]
        public static ParametrosCheckListRoladoModel ObtenerGranoEnteroDieselCaldera(string fechaInicio
                                                                                   , string horaInicio)
        {
            ParametrosCheckListRoladoModel parametrosCheckListRoladoModel;
            using (var checkListRoladoraBL = new CheckListRoladoraBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                var fecha = AgregarHoraFecha(Convert.ToDateTime(fechaInicio), horaInicio);
                parametrosCheckListRoladoModel = checkListRoladoraBL.ObtenerGranoEnteroDieselCaldera(organizacionID,
                                                                                                     fecha);
            }
            return parametrosCheckListRoladoModel;
        }

        private static string NotificacionesActuales()
        {
            IList<CheckListRoladoraInfo> notificaciones = ObtenerNotificaciones();
            return notificaciones == null ? "0" : Convert.ToString(notificaciones.Count);
        }

        private static DateTime AgregarHoraFecha(DateTime fecha, string horaMinutos)
        {
            var hora = Convert.ToDouble(horaMinutos.Split(':')[0]);
            var minutos = Convert.ToDouble(horaMinutos.Split(':')[1]);
            return fecha.AddHours(hora).AddMinutes(minutos);
        }

        private static int ObtenerOrganizacionID()
        {
            int organizacionID = 0;
            var usuario = ObtenerSeguridad() as SeguridadInfo;
            if (usuario != null)
            {
                organizacionID = usuario.Usuario.Organizacion.OrganizacionID;
            }
            return organizacionID;
        }

        [WebMethod]
        public static List<CheckListRoladoraHorometroInfo> ObtenerHorometros(int turno)
        {
            List<CheckListRoladoraHorometroInfo> resultado;
            using (var checkListRoladoBL = new CheckListRoladoraBL())
            {
                int organizacionID = ObtenerOrganizacionID();
                resultado = checkListRoladoBL.ObtenerHorometros(turno, organizacionID);
                //resultado.ForEach(f=> f.FechaServidor = f.FechaServidor.AddHours(5));
            }
            return resultado;
        }
        #endregion METODOS WEB
    }
}
