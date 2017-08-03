using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
   public class ConfiguracionAlertasBL
    {
        /// <summary>
        /// Obtiene los datos de la consulta al procedmiento almacenado ConfigurarAlertasConsulta 
        /// </summary>
        /// <param name="paginas"></param>
        /// <param name="filtros"></param>
        /// <returns>regresa los datos consultados</returns>
       internal ResultadoInfo<ConfiguracionAlertasGeneraInfo> ConsultarConfiguracionAlertas(PaginacionInfo paginas, ConfiguracionAlertasGeneraInfo filtros)
        {
            ResultadoInfo<ConfiguracionAlertasGeneraInfo> resultado;
            try
            {
                Logger.Info();
                var configAlertasDAL = new ConfiguracionAlertasDAL();
                resultado = configAlertasDAL.ConsultarConfiguracionAlertas(paginas, filtros);
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
       /// 
       /// </summary>
       /// <param name="filtros"></param>
       /// <returns></returns>
       internal bool InsertarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros)
       {
           bool resultado;
           
           try
           {
               Logger.Info();
               using (var transaction = new TransactionScope())
               {
                   var configAlertasDAL = new ConfiguracionAlertasDAL();
                   resultado = configAlertasDAL.InsertarConfiguracionAlerta(filtros);

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

           return resultado;
       }

       /// <summary>
       /// Edita alguna configuracion alerta en la tabla AlertaConfiguracion
       /// </summary>
       /// <param name="filtros">Nuevos datos para su edicion</param>
       /// <param name="original">Lista Acciones con las que llega la configuracion inicialmente</param>
       /// <returns>true si se guardo, false si no se guardo</returns>
       internal bool EditarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros, List<AlertaAccionInfo> original)
       {
           bool resultado;
           List<AlertaAccionInfo> aux = new List<AlertaAccionInfo>();
           List<AlertaAccionInfo> aux2 = new List<AlertaAccionInfo>();
           try
           {
               Logger.Info();
               using (var transaction = new TransactionScope())
               {

                   foreach (var alertaAccionInfo in original)
                   {
                       foreach (var accionInfo in filtros.ListaAlertaAccionInfo.Where(accionInfo => alertaAccionInfo.AccionId == accionInfo.AccionId))
                       {
                           aux.Add(alertaAccionInfo);
                           aux2.Add(accionInfo);
                       }
                   }

                   foreach (var alertaAccionInfo in aux)
                   {
                       original.Remove(alertaAccionInfo);
                   }

                   foreach (var alertaAccionInfo in aux2)
                   {
                       filtros.ListaAlertaAccionInfo.Remove(alertaAccionInfo);
                   }

                   foreach (var alertaAccionInfo in original)
                   {
                       filtros.ListaAlertaAccionInfo.Add(alertaAccionInfo);
                   }

                   var configAlertasDal = new ConfiguracionAlertasDAL();
                   resultado = configAlertasDal.EditarConfiguracionAlerta(filtros);
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

           return resultado;
       }

       /// <summary>
       /// Validacion de query para poder registrar una nueva configuración
       /// </summary>
       /// <param name="filtros"></param>
       /// <returns>regresa los datos consultados</returns>
       internal bool ValidacionQuery(ConfiguracionAlertasGeneraInfo filtros)
       {
           bool resultado;
           try
           {
               Logger.Info();
               using (var transaction = new TransactionScope())
               {

                   var validarQuery = new ConfiguracionAlertasDAL();
                  var query = String.Format("SELECT  {0} {1}  FROM {2} {3} WHERE {4} {5} {6}",
                filtros.ConfiguracionAlertas.Datos, Environment.NewLine, filtros.ConfiguracionAlertas.Fuentes,
                Environment.NewLine, filtros.ConfiguracionAlertas.Condiciones, Environment.NewLine,
                filtros.ConfiguracionAlertas.Agrupador == string.Empty ? string.Empty : "GROUP BY " + filtros.ConfiguracionAlertas.Agrupador);
                resultado = validarQuery.EjecutarQuery(query);
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

           return resultado;
       }
       
        /// <summary>
        /// metodo para la ayuda obtene todas las alertas activas
        /// </summary>
        /// <returns>regresa la Alerta consultada</returns>
       public List<AlertaInfo> ObtenerTodasLasAlertasActivas()
        {
            List<AlertaInfo> info;
            try
            {
                Logger.Info();
                var configAlertaDal = new ConfiguracionAlertasDAL();
                info = configAlertaDal.ObtenerTodasLasAlertasActivas();
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
       /// metodo que obtiene todas las acciones activas
       /// </summary>
       /// <returns>regresa la Alerta consultada</returns>
       public List<AccionInfo> ObtenerTodasLasAccionesActivas()
       {
           List<AccionInfo> info;
           try
           {
               Logger.Info();
               var configAlertaDal = new ConfiguracionAlertasDAL();
               info = configAlertaDal.ObtenerTodasLasAccionesActivas();
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
       /// Metodo que obtiene las acciones que estan ligadas a una IDAlerta en especifico
       /// para cargar las acciones al editar una configuración alerta
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public List<AlertaAccionInfo> ObtenerListaAcciones(int id)
       {
           List<AlertaAccionInfo> info;
           try
           {
               Logger.Info();
               var listaAcciones = new ConfiguracionAlertasDAL();
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
        /// MEtodo que obtiene una alerta por medio de su ID
        /// </summary>
        /// <param name="idAlerta"></param>
        /// <returns>regresa la Alerta consultada</returns>
        public AlertaInfo ObtenerAlertaPorId(long idAlerta)
        {
            AlertaInfo info;
            try
            {
                Logger.Info();
                var configAlertaDal = new ConfiguracionAlertasDAL();
                info = configAlertaDal.ObtenerAlertaPorId(idAlerta);
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
        /// Obtiene un lista paginada de alertas para el modulo configuracion alerta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>una lista de Alertas</returns>
        internal ResultadoInfo<AlertaInfo> ObtenerPorPaginaAlertas(PaginacionInfo pagina, AlertaInfo filtro)
        {
            ResultadoInfo<AlertaInfo> result;
            try
            {
                Logger.Info();
                ConfiguracionAlertasDAL configAlertasDal = new ConfiguracionAlertasDAL();
                result = configAlertasDal.ObtenerPorPaginaFiltroAlertas(pagina, filtro);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
    }
}
