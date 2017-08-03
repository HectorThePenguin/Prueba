
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class IngredienteBL
    {
        /// <summary>
        /// Obtiene un ingrediente con su detalle
        /// Activo - Solo activos
        /// Inactivo - Solo inactivos
        /// Todos - Ambos
        /// </summary>
        /// <returns>IngredienteInfo</returns>
        internal IngredienteInfo ObtenerPorId(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            try
            {
                var ingredienteDal = new IngredienteDAL();
                ingrediente = ingredienteDal.ObtenerPorId(ingrediente, estatus);

                if (ingrediente != null)
                {
                    if (ingrediente.Organizacion.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        ingrediente.Organizacion = organizacionBl.ObtenerPorID(ingrediente.Organizacion.OrganizacionID);
                    }
                    if (ingrediente.Formula.FormulaId > 0)
                    {
                        var formulaBl = new FormulaBL();
                        ingrediente.Formula = formulaBl.ObtenerPorID(ingrediente.Formula.FormulaId);
                    }

                    if (ingrediente.Producto.ProductoId > 0)
                    {
                        var productoBl = new ProductoBL();
                        ingrediente.Producto = productoBl.ObtenerPorID(ingrediente.Producto);
                    }
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
        /// Obtiene una lista de ingredientes por formula
        /// Activo - Solo activos
        /// Inactivo - Solo inactivos
        /// Todos - Ambos
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <param name="estatus"></param>
        /// <returns>Lista IngredienteInfo</returns>
        internal List<IngredienteInfo> ObtenerPorFormula(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            List<IngredienteInfo> listaIngredientes = null;
            List<IngredienteInfo> listaIngredientesRegreso = new List<IngredienteInfo>();
            try
            {
                var ingredienteDal = new IngredienteDAL();
                listaIngredientes = ingredienteDal.ObtenerPorFormula(ingrediente, estatus);

                if (listaIngredientes != null)
                {
                    listaIngredientesRegreso.AddRange(listaIngredientes.Select(ingredienteInfo => ObtenerPorId(ingredienteInfo,estatus)));
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

            return listaIngredientesRegreso;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Ingrediente
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(IngredienteInfo info)
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                int result = info.IngredienteId;
                if (info.IngredienteId == 0)
                {
                    result = ingredienteDAL.Crear(info);
                }
                else
                {
                    ingredienteDAL.Actualizar(info);
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
                var ingredienteDAL = new IngredienteDAL();
                ResultadoInfo<IngredienteInfo> result = ingredienteDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Ingrediente
        /// </summary>
        /// <returns></returns>
        public IList<IngredienteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                IList<IngredienteInfo> result = ingredienteDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<IngredienteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                IList<IngredienteInfo> result = ingredienteDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Ingrediente por su Id
        /// </summary>
        /// <param name="ingredienteID">Obtiene una entidad Ingrediente por su Id</param>
        /// <returns></returns>
        public IngredienteInfo ObtenerPorID(int ingredienteID)
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                IngredienteInfo result = ingredienteDAL.ObtenerPorID(ingredienteID);
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
        /// Obtiene una entidad Ingrediente por su Id
        /// </summary>
        /// <param name="ingredienteID">Obtiene una entidad Ingrediente por su Id</param>
        /// <returns></returns>
        public IngredienteInfo ObtenerPorIdOrganizacionFormulaProducto(int formula, int ProductoId ,int OrganizacionID)
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                IngredienteInfo result = ingredienteDAL.ObtenerPorIdOrganizacionFormulaProducto(formula, ProductoId, OrganizacionID);
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
        /// Obtiene una entidad Ingrediente por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public IngredienteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                IngredienteInfo result = ingredienteDAL.ObtenerPorDescripcion(descripcion);
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
                var ingredienteDAL = new IngredienteDAL();
                ResultadoInfo<FormulaInfo> result = ingredienteDAL.ObtenerPorPaginaFormulaOrganizacion(pagina, filtro);
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
                var ingredienteDAL = new IngredienteDAL();
                ResultadoInfo<FormulaInfo> result = ingredienteDAL.ObtenerPorPaginaFormula(pagina, filtro);
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
        /// <param name="listaIngredientes"></param>
        public int GuardarIngredientesXML(List<IngredienteInfo> listaIngredientes)
        {
            try
            {
                Logger.Info();
                var ingredienteDAL = new IngredienteDAL();
                var result = ingredienteDAL.GuardarIngredientesXML(listaIngredientes);
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
