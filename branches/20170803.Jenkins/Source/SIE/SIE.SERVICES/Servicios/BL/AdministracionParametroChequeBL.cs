using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class AdministracionParametroChequeBL
    {

        /// <summary>
        /// registra el parametro cheque en la base de datos
        /// </summary>
        /// <param name="Info">accion que se registrara en la base de datos</param>
        /// <returns></returns>
        internal int GuardarParametroCheque(List<CatParametroBancoInfo> Info)
        {
            try
            {
                Logger.Info();
                var administracionParametroChqueDAL = new AdministracionParametroChequeDAL();
                int result = administracionParametroChqueDAL.CrearParanetroCheque(Info);
                
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
        /// Edita el parametro cheque en la base de datos
        /// </summary>
        /// <param name="Info">parametro que se modificara en la base de datos</param>
        /// <returns></returns>
        internal int EditarParametroCheque(List<CatParametroBancoInfo> Info)
        {
            try
            {
                Logger.Info();
                var administracionParametroChqueDAL = new AdministracionParametroChequeDAL();
                int result = administracionParametroChqueDAL.EditarParametroCheque(Info);

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
        /// Obtiene los parametros de los cheques paginado
        /// </summary>
        /// <param name="pagina">Informacion del paginado</param>
        /// <param name="catParametroBancoInfo">Informacion del filtro</param>
        /// <returns>Objeto con el resultado</returns>
        internal static ResultadoInfo<CatParametroBancoInfo> ObtenerParametroChequePaginado(PaginacionInfo pagina, CatParametroBancoInfo catParametroBancoInfo)
        {
            try
            {
                Logger.Info();
                var administracionParametroChequeCostoDAL = new AdministracionParametroChequeDAL();
                ResultadoInfo<CatParametroBancoInfo> result = administracionParametroChequeCostoDAL.ObtenerCatParamatroCheque(pagina,catParametroBancoInfo);
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
        /// Obtiene el parametro cheque por descripcion
        /// </summary>
        /// <param name="catParametroBancoInfo">Informacion con la descripcion deceada</param>
        /// <returns>Objeto con el resultado</returns>
        internal static CatParametroBancoInfo ObtenerParametroChequePorDescripcion(CatParametroBancoInfo catParametroBancoInfo)
        {
            try
            {
                Logger.Info();
                var administracionParametroChequeCostoDAL = new AdministracionParametroChequeDAL();
                CatParametroBancoInfo result = administracionParametroChequeCostoDAL.ObtenerCatParamatroChequePorDescripcion(catParametroBancoInfo);
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
