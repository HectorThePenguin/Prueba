using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Recepcion
{
    public partial class ConteoCalificacion : PageBase
    {
        private static readonly List<int> CalidadesValidas = new List<int> { 3, 16, 10, 23, 12, 25, 9, 22, 27, 28 };

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obtiene las calidades de ganado validas para el conteo
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ConteoCalificacionModel ObtenerCalidadesConteo()
        {
            var calidadGanadoPL = new CalidadGanadoPL();
            IList<CalidadGanadoInfo> calidadesTodas = calidadGanadoPL.ObtenerTodos(EstatusEnum.Activo);
            List<CalidadGanadoInfo> calidadesMachos = null;
            List<CalidadGanadoInfo> calidadesHembras = null;
            var calidades = new ConteoCalificacionModel();
            if(calidadesTodas != null && calidadesTodas.Any())
            {
                List<CalidadGanadoInfo> calidadesFiltradas = calidadesTodas.Where(cali => cali.CalidadGanadoID != null && CalidadesValidas.Contains(cali.CalidadGanadoID.Value)).ToList();
                if(calidadesFiltradas.Any())
                {
                    calidadesMachos = calidadesFiltradas.Where(cali => cali.Sexo == Sexo.Macho).OrderBy(ca=> ca.Descripcion).ToList();
                    calidadesHembras = calidadesFiltradas.Where(cali => cali.Sexo == Sexo.Hembra).OrderBy(ca => ca.Descripcion).ToList();
                }
                if (calidadesMachos != null)
                    calidades.CalidadMachos = calidadesMachos;
                calidades.CalidadHembras = calidadesHembras;
            }
            return calidades;
        }

        /// <summary>
        /// Metodo para Consultar la información de la entrada de Ganado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static EntradaGanadoInfo TraerInformacionEntrada(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroCalificacionGanado.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                var entradaGanado = entradaGanadoPL.ObtenerEntradaGanadoCapturaCalidad(filtroCalificacionGanado);
                return entradaGanado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Guardar la calificacion de Ganado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static void Guardar(FiltroCalificacionGanadoInfo filtroCalificacionGanadoInfo)
        {
            try
            {
                var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                filtroCalificacionGanadoInfo.UsuarioID = seguridad.Usuario.UsuarioID;
                entradaGanadoCosteoPL.GuardarCalidadGanado(filtroCalificacionGanadoInfo);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}