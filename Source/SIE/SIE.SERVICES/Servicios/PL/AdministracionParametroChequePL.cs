using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AdministracionParametroChequePL 
    {

        /// <summary>
        /// Metodo para Guardar la Parametro Banco
        /// </summary>
        /// <param name="Info">Representa la entidad que se va a grabar</param>
        public int GuardarParametroCheque(List<CatParametroBancoInfo> Info)
        {
            try
            {
                Logger.Info();
                var administracionParametroChequeBL = new AdministracionParametroChequeBL();
                int Resultado = administracionParametroChequeBL.GuardarParametroCheque(Info);
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
        /// Metodo para editar la Parametro Banco
        /// </summary>
        /// <param name="Info">Representa la entidad que se va a editar</param>
        public int EditarParametroCheque(List<CatParametroBancoInfo> Info)
        {
            try
            {
                Logger.Info();
                var administracionParametroChequeBL = new AdministracionParametroChequeBL();
                int Resultado = administracionParametroChequeBL.EditarParametroCheque(Info);
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
        /// Obtiene los paramatros de los cheques paginado y filtrado
        /// </summary>
        /// <param name="pagina">Datos de la paginacion</param>
        /// <param name="filtro">Filtro de busqueda</param>
        /// <returns>Resultado con los datos requeridos</returns>
        public ResultadoInfo<CatParametroBancoInfo> ObtenerParametroChequePaginado(PaginacionInfo pagina, CatParametroBancoInfo filtro)
        {
            try
            {
                Logger.Info();

                ResultadoInfo<CatParametroBancoInfo> result = AdministracionParametroChequeBL.ObtenerParametroChequePaginado(pagina, filtro);
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
        /// Obtiene el parametro del cheque por descripcion
        /// </summary>
        /// <param name="filtro">Objeto con la descripcion para realizar el filtrado</param>
        /// <returns>Resultado</returns>
        public CatParametroBancoInfo ObtenerParametroChequePorDescipcion(CatParametroBancoInfo filtro)
        {
            try
            {
                Logger.Info();

                CatParametroBancoInfo result = AdministracionParametroChequeBL.ObtenerParametroChequePorDescripcion(filtro);
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
