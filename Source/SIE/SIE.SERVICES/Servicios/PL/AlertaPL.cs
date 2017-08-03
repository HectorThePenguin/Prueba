using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
   public class AlertaPL
    {
       /// <summary>
        /// Obtiene una lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion que se usara para la consulta</param>
        /// <param name="filtro">filtros de busqueda para la busqueda</param>
        /// <returns>Regresa una lista de alertas que concuerden con los filtros de busqueda proporcionados</returns>
        public static ResultadoInfo<AlertaInfo> ObtenerPorPagina(PaginacionInfo pagina, AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                //var alertaBL = new AlertaBL();
                ResultadoInfo<AlertaInfo> result = AlertaBL.ObtenerPorPagina(pagina, filtro);
                return result;
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
        /// Metodo para Guardar/Modificar una alerta
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar o actualizar</param>
        public static int Guardar(AlertaInfo info)
        {
            try
            {
                Logger.Info();
                //var alertaBL = new AlertaBL();
                int result = AlertaBL.GuardarCambios(info);
                return result;
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
       /// valida si ya se encuentra registrada una alerta con una descripcion y modulo especificos
       /// </summary>
       /// <param name="info">descripcion y modulo de la alerta que se verificara existencia</param>
       /// <returns>Regresa true si existe la alerta proporcionada</returns>
       public static bool ExisteAlerta(AlertaInfo info)
       {
           try
            {
                Logger.Info();
                //var alertaBL = new AlertaBL();
                bool result = AlertaBL.ExisteAlerta(info);
                return result;
           }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
       }
    }
}
