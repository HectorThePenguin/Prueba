using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Collections.Generic;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class IngredienteDAL : DALBase
    {
        /// <summary>
        /// Obtiene un ingrediente por id
        /// Activo - Solo activos
        /// Inactivo - Solo inactivos
        /// Todos - Ambos
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal IngredienteInfo ObtenerPorId(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorId(ingrediente,estatus);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    ingrediente = MapIngredienteDAL.ObtenerPorId(ds);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return ingrediente;
        }

        /// <summary>
        /// Obtiene una lista de ingredientes
        /// Activo - Solo activos
        /// Inactivo - Solo inactivos
        /// Todos - Ambos
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <param name="estatus"></param>
        /// <returns>Lista de IngredienteInfo</returns>
        internal List<IngredienteInfo> ObtenerPorFormula(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            List<IngredienteInfo> listaIngredientes = null;

            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorFormula(ingrediente, estatus);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorFormula", parameters);
                if (ValidateDataSet(ds))
                {
                    listaIngredientes = MapIngredienteDAL.ObtenerPorFormula(ds);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaIngredientes;
        }

        /// <summary>
        /// Metodo para Crear un registro de Ingrediente
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(IngredienteInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosCrear(info);
                int result = Create("Ingrediente_Crear", parameters);
                return result;
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
        /// Metodo para actualizar un registro de Ingrediente
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(IngredienteInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosActualizar(info);
                Update("Ingrediente_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<IngredienteInfo> ObtenerPorPagina(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorPagina", parameters);
                ResultadoInfo<IngredienteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerPorPagina(ds);
                }
                return result;
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
        /// Obtiene una lista de Ingrediente
        /// </summary>
        /// <returns></returns>
        internal IList<IngredienteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Ingrediente_ObtenerTodos");
                IList<IngredienteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerTodos(ds);
                }
                return result;
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<IngredienteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Ingrediente_ObtenerTodos", parameters);
                IList<IngredienteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerTodos(ds);
                }
                return result;
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
        /// Obtiene un registro de Ingrediente
        /// </summary>
        /// <param name="ingredienteID">Identificador de la Ingrediente</param>
        /// <returns></returns>
        internal IngredienteInfo ObtenerPorID(int ingredienteID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorID(ingredienteID);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorID", parameters);
                IngredienteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerPorID(ds);
                }
                return result;
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
        /// Obtiene un registro de Ingrediente
        /// </summary>
        /// <param name="ingredienteID">Identificador de la Ingrediente</param>
        /// <returns></returns>
        internal IngredienteInfo ObtenerPorIdOrganizacionFormulaProducto(int formula, int ProductoId, int OrganizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerPorIdOrganizacionFormulaProducto(formula, ProductoId, OrganizacionID);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorIdOrganizacionFormulaProducto", parameters);
                IngredienteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerPorIDOrganizacionFormulaProducto(ds);
                }
                return result;
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
        /// Obtiene un registro de Ingrediente
        /// </summary>
        /// <param name="descripcion">Descripción de la Ingrediente</param>
        /// <returns></returns>
        internal IngredienteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorDescripcion", parameters);
                IngredienteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerPorDescripcion(ds);
                }
                return result;
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<FormulaInfo> ObtenerPorPaginaFormulaOrganizacion(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorPaginaFormulaOrganizacion(pagina, filtro);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorPaginaFormulaOrganizacion", parameters);
                ResultadoInfo<FormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerPorPaginaFormulaOrganizacion(ds);
                }
                return result;
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<FormulaInfo> ObtenerPorPaginaFormula(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosPorPaginaFormula(pagina, filtro);
                DataSet ds = Retrieve("Ingrediente_ObtenerPorPaginaFormula", parameters);
                ResultadoInfo<FormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIngredienteDAL.ObtenerPorPaginaFormula(ds);
                }
                return result;
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
        /// Metodo para Crear un registro de Ingrediente
        /// </summary>
        /// <param name="listaIngredientes">Valores de la entidad que será creada</param>
        internal int GuardarIngredientesXML(List<IngredienteInfo> listaIngredientes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIngredienteDAL.ObtenerParametrosGuardarIngredientesXML(listaIngredientes);
                int result = Create("Ingrediente_GuardarXML", parameters);
                return result;
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
    }
}
