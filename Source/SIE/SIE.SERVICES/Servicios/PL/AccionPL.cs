using System;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;

namespace SIE.Services.Servicios.PL
{
    public  class AccionPL
    {
        /// <summary>
        /// Metodo para Guardar la Accion
        /// </summary>
        /// <param name="Info">Representa la entidad que se va a grabar</param>
        public int Guardar(AdministrarAccionInfo Info)
        {
            try
            {
                Logger.Info();
                var accionBL = new AccionBL();
                int Resultado = accionBL.Guardar(Info);
                return Resultado;
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
        /// Obtiene una lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion usada en la consulta</param>
        /// <param name="filtro">filtros o parametros de busqueda</param>
        /// <returns></returns>
        public static ResultadoInfo<AdministrarAccionInfo> ObtenerPorPagina(PaginacionInfo pagina, AdministrarAccionInfo filtro)
        {
            try
            {
                Logger.Info();
                //var AccionBL = new AccionBL();
                ResultadoInfo<AdministrarAccionInfo> result = AccionBL.ObtenerPorPagina(pagina, filtro);
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
        /// Busca una accion en la base de datos que coincida con la descripcion proporcionada
        /// </summary>
        /// <param name="Descripcion">Descripcion de la accion que se buscara en la base de datos</param>
        /// <returns>Regresa la informacion de la accion que se encontro</returns>
        public static AdministrarAccionInfo ObtenerPorDescripcion(String Descripcion)
        {
            try
            {
                Logger.Info();
                //var AccionBL = new AccionBL();
                AdministrarAccionInfo result = AccionBL.ObtenerPorDescripcion(Descripcion);
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
        /// valida si la accion proporcionada es la ultima accion no asignada en alguna configuracion
        /// </summary>
        /// <param name="accionId">Id de la accion que se buscara</param>
        /// <returns>Regresa True si es valida, false si no lo es</returns>
        public static bool ValidarAsignacionesAsignadasById(int accionId)
        {
            try
            {
                Logger.Info();
                //var accionBl = new AccionBL();
                bool result = AccionBL.ValidarAsignacionesAsignadasById(accionId);
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
    }

}
