using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class BancoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de banco por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<BancoInfo> ObtenerPorPagina(PaginacionInfo pagina, BancoInfo filtro)
        {
            ResultadoInfo<BancoInfo> bancoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Banco_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    bancoLista = MapBancoDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return bancoLista;
        }

        /// <summary>
        ///     Obtiene un Banco por Id
        /// </summary>
        /// <param name="bancoID"></param>
        /// <returns></returns>
        internal BancoInfo ObtenerPorID(int bancoID)
        {
            BancoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametroPorID(bancoID);
                DataSet ds = Retrieve("Banco_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapBancoDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }


        /// <summary>
        /// Obtiene el chofer por su identificador 
        /// </summary>
        /// <param name="bancoInfo"></param>
        /// <returns></returns>
        internal BancoInfo ObtenerPorID(BancoInfo bancoInfo)
        {
            BancoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametroPorID(bancoInfo);
                DataSet ds = Retrieve("Banco_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapBancoDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene un Banco por telefono
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        internal BancoInfo ObtenerPorTelefono(string telefono)
        {
            BancoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametroPorTelefono(telefono);
                DataSet ds = Retrieve("Banco_ObtenerPorTelefono", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapBancoDAL.ObtenerPorTelefono(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        ///     Obtiene un Banco por descripcion
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        internal BancoInfo ObtenerPorDescripcion(string descripcion)
        {
            BancoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametroPorDescripcion(descripcion);
                DataSet ds = Retrieve("Banco_ObtenerPorDescripcion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapBancoDAL.ObtenerPorDescripcion(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de usuarios paginada por 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal ResultadoInfo<BancoInfo> ObtenerPorDescripcion(PaginacionInfo pagina, BancoInfo filtro,
                                                                IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<BancoInfo> bancoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametrosPorPagina(pagina, filtro,
                                                                                                 dependencias);
                DataSet ds = Retrieve("Banco_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    bancoLista = MapBancoDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return bancoLista;
        }

        /// <summary>
        ///     Metodo que crear un banco
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(BancoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametrosCrear(info);
                Create("Banco_Crear", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Metodo que actualiza un banco
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(BancoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxBancoDAL.ObtenerParametrosActualizar(info);
                Update("Banco_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal IList<BancoInfo> ObtenerTodos()
        {
            IList<BancoInfo> bancoLista = null;
            try
            {
                DataSet ds = Retrieve("Banco_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    bancoLista = MapBancoDAL.ObtenerTodos(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return bancoLista;
        }
    }
}
