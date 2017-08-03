using System;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class AlertaBL
    {
        /// <summary>
        /// Obtiene un lista de Alertas paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion de los alertas a mostrar</param>
        /// <param name="filtro">filtros de busqueda de alertas</param>
        /// <returns>Regresa una lista de alertas que concuerden con los filtros de busqueda proporcionados</returns>
        internal static ResultadoInfo<AlertaInfo> ObtenerPorPagina(PaginacionInfo pagina, AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var AlertaDAL = new AlertaDAL();
                ResultadoInfo<AlertaInfo> result = AlertaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// <param name="info">Representa la entidad que se va a grabar</param>
        internal static int GuardarCambios(AlertaInfo info)
        {
            try
            {
                Logger.Info();
                var alertaDAL = new AlertaDAL();
                int result = info.AlertaID;
                if (info.AlertaID == 0)//si es un registro nuevo se manda guardar
                {
                    result = alertaDAL.Crear(info);
                }
                else
                {
                    alertaDAL.Actualizar(info);//si es una edicion se manda modificar
                }
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
        /// valida si ya se encuentra registrada la alerta en el modulo especificado
        /// </summary>
        /// <param name="filtro">descripcion y modulo de la alerta que se verificara existencia</param>
        /// <returns>regresa true si una alerta con esa descripcion ya esta registrada</returns>
        internal static bool ExisteAlerta(AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var alertaDAL = new AlertaDAL();
                bool result = alertaDAL.AlertaExiste(filtro);
                return result;//regresa true si una alerta con esa descripcion ya esta registrada
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
    }
}
