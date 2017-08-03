using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class BasculaMultipesajePL
    {
        public BasculaMultipesajePL()
        {
            organizacionId = 0;
        }

        public BasculaMultipesajePL(int organizacionID)
        {
            organizacionId = organizacionID;
        }

        private int organizacionId;
        /// <summary>
        /// Inserta o actualiza un registro del modulo BasculaMultipesaje
        /// </summary>
        /// <param name="basculaMultipesajeInfo"></param>
        /// <param name="actualizar"></param>
        /// <returns>regresa el  folio del registro que se acaba de registrar</returns>
        public long GuardarBasculaMultipesaje(BasculaMultipesajeInfo basculaMultipesajeInfo, bool actualizar)
        {
            try
            {
                Logger.Info();
                var insertarBasculaMultipesaje = new BasculaMultipesajeBL();
                long resultado = 0;
                if (actualizar)
                {
                   insertarBasculaMultipesaje.ActualizarBasculaMultipesaje(basculaMultipesajeInfo);
                }
                else
                {
                  resultado = insertarBasculaMultipesaje.InsertarBasculaMultipesaje(basculaMultipesajeInfo);
                }

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
        public BasculaMultipesajeInfo ConsultaBasculaMultipesaje(long folio, int organizacionId)
        {
            BasculaMultipesajeInfo resultado;
            try
            {
                Logger.Info();
                var basculaBL = new BasculaMultipesajeBL();
                resultado = basculaBL.ConsultarBasculaMultipesaje(folio, organizacionId);
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
        /// metodo para la ayuda de folio
        /// </summary>
        /// <param name="folioMultipesajeInfo"></param>
        /// <returns>regresa el folio consultado</returns>
        public FolioMultipesajeInfo ObtenerFolioMultipesajePorId(FolioMultipesajeInfo folioMultipesajeInfo, int organizacionId)
        {
            FolioMultipesajeInfo info;
            try
            {
                Logger.Info();
                var basculaMultipesajeBl = new BasculaMultipesajeBL();
                info = basculaMultipesajeBl.ObtenerFolioMultipesajePorId(Convert.ToInt64(folioMultipesajeInfo.Folio), organizacionId);
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
        public ResultadoInfo<FolioMultipesajeInfo> ObtenerPorPaginaFolios(PaginacionInfo pagina, FolioMultipesajeInfo filtro)
        {
            ResultadoInfo<FolioMultipesajeInfo> resultadoFolios;
            try
            {
                Logger.Info();
                var basculaMultipesajeBl = new BasculaMultipesajeBL();

                if(organizacionId != 0)
                {
                    filtro.OrganizacionId = organizacionId;
                }
                resultadoFolios = basculaMultipesajeBl.ObtenerPorPaginaFolios(pagina, filtro);
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
            return resultadoFolios;
        }

        ///<summary>
        /// Obtiene la informacion de usuario previamente logeado
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <return>regresa la informacion del usuario logueado</return>
        public UsuarioInfo ObtenerUsuarioPorID(int usuarioId)
        {
            UsuarioInfo result = new UsuarioInfo();
            try
            {
                Logger.Info();
                var usuarioBl = new BasculaMultipesajeBL();
                result = usuarioBl.ObtenerUsuarioPorID(usuarioId);
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
