using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionAlertasPL
    {
        /// <summary>
        /// Obtiene los datos de la consulta al procedmiento almacenado ConfigurarAlertasConsulta 
        /// </summary>
        /// <param name="paginas"></param>
        /// <param name="filtros"></param>
        /// <returns>regresa los datos consultados</returns>
        public ResultadoInfo<ConfiguracionAlertasGeneraInfo> ConsultaConfiguracionAlertas(PaginacionInfo paginas, ConfiguracionAlertasGeneraInfo filtros)
        {
            ResultadoInfo<ConfiguracionAlertasGeneraInfo> resultado;
            try
            {
                Logger.Info();
                var configAlertasBL = new ConfiguracionAlertasBL();
                resultado = configAlertasBL.ConsultarConfiguracionAlertas(paginas, filtros);
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

        /// <summary>
        /// Validacion del query para registrar como una nueva configuración
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>regresa los datos consultados</returns>
        public bool ValidacionQuery(ConfiguracionAlertasGeneraInfo filtros)
        {
            bool resultado;
            try
            {
                Logger.Info();
                var configAlertasBL = new ConfiguracionAlertasBL();
                resultado = configAlertasBL.ValidacionQuery(filtros);
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

        /// <summary>
        /// Registra una nueva configuracion alerta en la tabla AlertaConfiguracion
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>regresa los datos consultados</returns>
        public bool InsertarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros)
        {
            bool resultado;
            try
            {
                Logger.Info();
                var configAlertasBL = new ConfiguracionAlertasBL();
                resultado = configAlertasBL.InsertarConfiguracionAlerta(filtros);
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

        /// <summary>
        /// Edita alguna configuracion alerta en la tabla AlertaConfiguracion
        /// </summary>
        /// <param name="filtros">Nuevos datos para su edicion</param>
        /// <param name="original">Lista Acciones con las que llega la configuracion inicialmente</param>
        /// <returns>true si se guardo, false si no se guardo</returns>
        public bool EditarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros,List<AlertaAccionInfo> original)
        {
            bool resultado;
            try
            {
                Logger.Info();
                var configAlertasBl = new ConfiguracionAlertasBL();
                resultado = configAlertasBl.EditarConfiguracionAlerta(filtros, original);
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

        /// <summary>
        /// metodo que obtiene por id una alerta
        /// </summary>
        /// <param name="idAlerta"></param>
        /// <returns>regresa el folio consultado</returns>
        public AlertaInfo ObtenerAlertaPorId(AlertaInfo idAlerta)
        {
            AlertaInfo info;
            try
            {
                Logger.Info();
                var configAlertaBL = new ConfiguracionAlertasBL();
                info = configAlertaBL.ObtenerAlertaPorId(Convert.ToInt64(idAlerta.AlertaID));
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
            return info;
        }

        /// <summary>
        /// Metodo que obtiene todas las alertas activas
        /// </summary>
        /// <returns>regresa el folio consultado</returns>
        public List<AlertaInfo> ObtenerTodasLasAlertasActivas()
        {
            List<AlertaInfo> info;
            try
            {
                Logger.Info();
                var configAlertaBL = new ConfiguracionAlertasBL();
                info = configAlertaBL.ObtenerTodasLasAlertasActivas();
            }
            catch (ExcepcionGenerica)
            {
                info = new List<AlertaInfo>();
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                info = new List<AlertaInfo>();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// metodo que obtiene todas las acciones activas
        /// </summary>
        /// <returns>regresa el folio consultado</returns>
        public List<AccionInfo> ObtenerTodasLasAccionesActivas()
        {
            List<AccionInfo> info;
            try
            {
                Logger.Info();
                var configAlertaBL = new ConfiguracionAlertasBL();
                info = configAlertaBL.ObtenerTodasLasAccionesActivas();
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
            return info;
        }

        /// <summary>
        /// Metodo que obtiene la lista de Acciones asignadas a alguna Configuracion de Alerta
        /// </summary>
        /// <param name="id">ID de la configuracion de alerta</param>
        /// <returns>Lista de alerta acciones</returns>
        public List<AlertaAccionInfo> ObtenerListaAcciones(int id)
        {
            List<AlertaAccionInfo> info;
            try
            {
                Logger.Info();
                var listaAcciones = new ConfiguracionAlertasBL();
                info = listaAcciones.ObtenerListaAcciones(id);
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

            return info;
        }

        /// <summary>
        /// Obtiene un lista paginada de Alertas para el modulo ConfiguracionAlertas para la ayuda de Alertas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>una lista de folios del dia</returns>
        public ResultadoInfo<AlertaInfo> ObtenerPorPaginaAlertas(PaginacionInfo pagina, AlertaInfo filtro)
        {
            ResultadoInfo<AlertaInfo> resultadoAlertas;
            try
            {
                Logger.Info();
                var configAlertasBl = new ConfiguracionAlertasBL();
                resultadoAlertas = configAlertasBl.ObtenerPorPaginaAlertas(pagina, filtro);
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
            return resultadoAlertas;
        }
    }
}
