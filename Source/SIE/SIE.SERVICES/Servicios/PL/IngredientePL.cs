using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class IngredientePL
    {
        /// <summary>
        /// Obtiene un ingrediente con su detalle
        /// Activo - Solo activos
        /// Inactivo - Solo inactivos
        /// Todos - Ambos
        /// </summary>
        /// <returns>IngredienteInfo</returns>
        public IngredienteInfo ObtenerPorId(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            try
            {
                var ingredienteBl = new IngredienteBL();
                ingrediente = ingredienteBl.ObtenerPorId(ingrediente,estatus);
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
        /// Obtiene una lista de ingredientes por formula
        /// Activo - Solo activos
        /// Inactivo - Solo inactivos
        /// Todos - Ambos
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <param name="estatus"></param>
        /// <returns>Lista de IngredienteInfo</returns>
        public List<IngredienteInfo> ObtenerPorFormula(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            List<IngredienteInfo> listaIngredientes = null;
            
            try
            {
                var ingredienteBl = new IngredienteBL();
                listaIngredientes = ingredienteBl.ObtenerPorFormula(ingrediente, estatus);
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
        /// Metodo para Guardar/Modificar una entidad Ingrediente
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(IngredienteInfo info)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                int result = ingredienteBL.Guardar(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<IngredienteInfo> ObtenerPorPagina(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                ResultadoInfo<IngredienteInfo> result = ingredienteBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<IngredienteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                IList<IngredienteInfo> result = ingredienteBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<IngredienteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                IList<IngredienteInfo> result = ingredienteBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="ingredienteID"></param>
        /// <returns></returns>
        public IngredienteInfo ObtenerPorID(int ingredienteID)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                IngredienteInfo result = ingredienteBL.ObtenerPorID(ingredienteID);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="ingredienteID"></param>
        /// <returns></returns>
        public IngredienteInfo ObtenerPorIdOrganizacionFormulaProducto(int formula, int ProductoId, int OrganizacionID)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                IngredienteInfo result = ingredienteBL.ObtenerPorIdOrganizacionFormulaProducto(formula, ProductoId ,OrganizacionID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public IngredienteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                IngredienteInfo result = ingredienteBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<FormulaInfo> ObtenerPorPaginaFormulaOrganizacion(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                ResultadoInfo<FormulaInfo> result = ingredienteBL.ObtenerPorPaginaFormulaOrganizacion(pagina, filtro);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<FormulaInfo> ObtenerPorPaginaFormula(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                ResultadoInfo<FormulaInfo> result = ingredienteBL.ObtenerPorPaginaFormula(pagina, filtro);
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
        /// Metodo para Guardar/Modificar una entidad Ingrediente
        /// </summary>
        /// <param name="listaIngredientes">Representa la entidad que se va a grabar</param>
        public int GuardarIngredientesXML(List<IngredienteInfo> listaIngredientes)
        {
            try
            {
                Logger.Info();
                var ingredienteBL = new IngredienteBL();
                int result = ingredienteBL.GuardarIngredientesXML(listaIngredientes);
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
