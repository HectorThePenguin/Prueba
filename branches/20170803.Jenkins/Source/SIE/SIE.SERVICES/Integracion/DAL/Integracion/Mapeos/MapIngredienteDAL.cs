using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapIngredienteDAL
    {
        /// <summary>
        /// Obtiene un ingrediente
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>IngredienteInfo</returns>
        internal static IngredienteInfo ObtenerPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IngredienteInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo()
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo(){OrganizacionID = info.Field<int>("OrganizacionID")},
		                     Formula = new FormulaInfo(){FormulaId = info.Field<int>("FormulaID")},
		                     Producto = new ProductoInfo(){ProductoId = info.Field<int>("ProductoID")},
		                     PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de ingredientes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<IngredienteInfo> ObtenerPorFormula(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<IngredienteInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo()
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Formula = new FormulaInfo() { FormulaId = info.Field<int>("FormulaID") },
                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoID") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<IngredienteInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IngredienteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<IngredienteInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<IngredienteInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IngredienteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IngredienteInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IngredienteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        //ObtenerPorIDOrganizacionFormulaProducto
        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IngredienteInfo ObtenerPorIDOrganizacionFormulaProducto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IngredienteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IngredienteInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IngredienteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<FormulaInfo> ObtenerPorPaginaFormula(DataSet ds)
        {
            try
            {
                List<FormulaInfo> formulas = null;
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtIngredientes = ds.Tables[ConstantesDAL.DtIngredientes];

                formulas = (from formula in dt.AsEnumerable()
                            select new FormulaInfo
                                {
                                    FormulaId = formula.Field<int>("FormulaID"),
                                    Descripcion = formula.Field<string>("Formula"),
                                    TipoFormula = new TipoFormulaInfo
                                        {
                                            TipoFormulaID = formula.Field<int>("TipoFormulaID"),
                                            Descripcion = formula.Field<string>("TipoFormula")
                                        },
                                        Producto = new ProductoInfo
                                            {
                                                ProductoId = formula.Field<int>("ProductoID"),
                                                ProductoDescripcion = formula.Field<string>("Producto")
                                            }
                                }).ToList();

                List<IngredienteInfo> listaIngredientes =
                    (from info in dtIngredientes.AsEnumerable()
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Producto") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                formulas.ForEach(form=>
                    {
                        form.ListaIngredientes =
                            listaIngredientes.Where(ingr => ingr.Formula.FormulaId == form.FormulaId).ToList();
                    });

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<FormulaInfo>
                    {
                        Lista = formulas,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<FormulaInfo> ObtenerPorPaginaFormulaOrganizacion(DataSet ds)
        {
            try
            {
                List<FormulaInfo> formulas = null;
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtIngredientes = ds.Tables[ConstantesDAL.DtIngredientes];

                formulas = (from formula in dt.AsEnumerable()
                            select new FormulaInfo
                            {
                                Organizacion = new OrganizacionInfo { OrganizacionID = formula.Field<int>("OrganizacionID"), Descripcion = formula.Field<string>("OrganizacionDesc") },
                                FormulaId = formula.Field<int>("FormulaID"),
                                Descripcion = formula.Field<string>("Formula"),
                                TipoFormula = new TipoFormulaInfo
                                {
                                    TipoFormulaID = formula.Field<int>("TipoFormulaID"),
                                    Descripcion = formula.Field<string>("TipoFormula")
                                },
                                Producto = new ProductoInfo
                                {
                                    ProductoId = formula.Field<int>("ProductoID"),
                                    ProductoDescripcion = formula.Field<string>("Producto")
                                }
                            }).ToList();

                List<IngredienteInfo> listaIngredientes =
                    (from info in dtIngredientes.AsEnumerable()
                     // where info.Field<bool>("Activo").BoolAEnum()  == Info.Enums.EstatusEnum.Activo
                     select
                         new IngredienteInfo
                         {
                             IngredienteId = info.Field<int>("IngredienteID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Producto") },
                             PorcentajeProgramado = info.Field<decimal>("PorcentajeProgramado"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         } ).ToList();

                formulas.ForEach(form =>
                {
                    form.ListaIngredientes =
                        listaIngredientes.Where(ingr => ingr.Formula.FormulaId == form.FormulaId && ingr.Organizacion.OrganizacionID == form.Organizacion.OrganizacionID).ToList();
                });

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<FormulaInfo>
                    {
                        Lista = formulas,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
