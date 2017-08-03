using System;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Base.Infos;

namespace SIE.Services.Servicios.BL
{
    public class AccionBL
    {
        /// <summary>
        /// registra la accion proporcionada en la base de datos
        /// </summary>
        /// <param name="Info">accion que se registrara en la base de datos</param>
        /// <returns></returns>
        internal int Guardar(AdministrarAccionInfo Info)
        {
            try
            {
                Logger.Info();
                var AccionDAL = new AccionDAL();
                int result = Info.AccionID;
                if (Info.AccionID == 0)
                {
                    result = AccionDAL.Crear(Info);
                }
                else
                {
                    AccionDAL.Actualizar(Info);
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
        /// Obtiene una lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion usada en la consulta</param>
        /// <param name="filtro">filtros o parametros de busqueda</param>
        /// <returns></returns>
        static internal ResultadoInfo<AdministrarAccionInfo> ObtenerPorPagina(PaginacionInfo pagina, AdministrarAccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var AccionDAL = new AccionDAL();
                ResultadoInfo<AdministrarAccionInfo> result = AccionDAL.ObtenerPorPagina(pagina, filtro);
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
                var AccionDAL = new AccionDAL();
                AdministrarAccionInfo result = AccionDAL.ObtenerPorDescripcion(Descripcion);
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
        internal static bool ValidarAsignacionesAsignadasById(int accionId)
        {
            try
            {
                Logger.Info();
                var accionDal = new AccionDAL();
                bool result = accionDal.ValidarAsignacionesAsignadasById(accionId);
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