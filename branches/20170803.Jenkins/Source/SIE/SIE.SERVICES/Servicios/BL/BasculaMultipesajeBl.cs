using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    public class BasculaMultipesajeBL
    {
        /// <summary>
        /// Inserta un registro del modulo BasculaMultipesaje
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        /// <returns>regresa el  folio del registro que se acaba de registrar</returns>
        internal long InsertarBasculaMultipesaje(BasculaMultipesajeInfo basculaMultipesajeInfo)
        {
            try
            {
                Logger.Info();
                var insertarBasculaMultipesaje = new BasculaMultipesajeDAL();
                basculaMultipesajeInfo.FechaPesoBruto = null;
                basculaMultipesajeInfo.FechaPesoTara = null;
                long resultado = 0;
                if (basculaMultipesajeInfo.EsPesoBruto)
                {
                    basculaMultipesajeInfo.FechaPesoBruto = DateTime.Now; 
                }
                else
                {
                    basculaMultipesajeInfo.FechaPesoTara = DateTime.Now;
                }

                resultado = insertarBasculaMultipesaje.InsertarBasculaMultipesaje(basculaMultipesajeInfo);
                return resultado;
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
        /// Obtiene los datos del folio consultado para el modulo BasculaMultipesaje
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>regresa los datos del folio consultado</returns>
        internal BasculaMultipesajeInfo ConsultarBasculaMultipesaje(long folio, int organizacionId)
        {
            BasculaMultipesajeInfo resultado;
            try
            {
                Logger.Info();
                var basculaDal = new BasculaMultipesajeDAL();
                resultado = basculaDal.ConsultarBasculaMultipesaje(folio, organizacionId);   
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
        /// Actualiza el registro del modulo BasculaMultipesaje
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        /// <returns></returns>
        internal void ActualizarBasculaMultipesaje(BasculaMultipesajeInfo basculaMultipesajeInfo)
        {
            try
            {
                Logger.Info();
                if (basculaMultipesajeInfo.EsPesoBruto)
                {
                    basculaMultipesajeInfo.FechaPesoBruto = DateTime.Now;
                }
                else
                {
                    basculaMultipesajeInfo.FechaPesoTara = DateTime.Now;  
                }
                var basculaDal = new BasculaMultipesajeDAL();
                basculaDal.ActualizarBasculaMultipesaje(basculaMultipesajeInfo);
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
        /// metodo para la ayuda de folio
        /// </summary>
        /// <param name="folio"></param>
        /// <returns>regresa el folio consultado</returns>
        public FolioMultipesajeInfo ObtenerFolioMultipesajePorId(long folio, int organizacionId)
        {
            FolioMultipesajeInfo info;
            try
            {
                Logger.Info();
                var basculaMultipesajeDal = new BasculaMultipesajeDAL();
                info = basculaMultipesajeDal.ObtenerFolioPorId(folio, organizacionId);
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
        /// Obtiene un lista paginada de folios para el modulo BasculaMultipesaje para la ayuda de folio
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>una lista de folios del dia</returns>
        internal ResultadoInfo<FolioMultipesajeInfo> ObtenerPorPaginaFolios(PaginacionInfo pagina, FolioMultipesajeInfo filtro)
        {
            ResultadoInfo<FolioMultipesajeInfo> result;
            try
            {
                Logger.Info();
                BasculaMultipesajeDAL basculaMultipesajeDal = new BasculaMultipesajeDAL();
                result = basculaMultipesajeDal.ObtenerPorPaginaFiltroFolios(pagina, filtro);
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

        ///<summary>
        /// Obtiene la informacion de usuario previamente logeado
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <return>regresa la informacion del usuario logueado</return>
        internal UsuarioInfo ObtenerUsuarioPorID(int usuarioId)
        {
            UsuarioInfo result;
            try
            {
                Logger.Info();
                UsuarioDAL usuarioDal = new UsuarioDAL();
                result = usuarioDal.ObtenerPorID(usuarioId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw  new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }

            return result;
        }
    }
}

