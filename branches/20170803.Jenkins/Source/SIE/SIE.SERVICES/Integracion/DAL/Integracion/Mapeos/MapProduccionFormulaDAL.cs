
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
    internal class MapProduccionFormulaDAL
    {
        /// <summary>
        /// Obtiene un objeto al guardar un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProduccionFormulaInfo GuardarProduccionFormula(DataSet ds)
        {
            ProduccionFormulaInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ProduccionFormulaInfo
                                 {
                                     ProduccionFormulaId = info.Field<int>("ProduccionFormulaID"),
                                     FolioFormula = info.Field<int>("FolioFormula"),
                                     Organizacion =
                                         new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                         },
                                     Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID") },
                                     CantidadProducida = info.Field<decimal>("CantidadProducida"),
                                     FechaProduccion = info.Field<DateTime>("FechaProduccion"),
                                     AlmacenMovimientoEntradaID = info.Field<long?>("AlmacenMovimientoEntradaID") != null ? info.Field<long>("AlmacenMovimientoEntradaID") : 0,
                                     AlmacenMovimientoSalidaID = info.Field<long?>("AlmacenMovimientoSalidaID") != null ? info.Field<long>("AlmacenMovimientoSalidaID") : 0,
                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                     UsuarioCreacionId = info.Field<int>("UsuarioCreacionID")
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<ProduccionFormulaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProduccionFormulaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionFormulaInfo
                         {
                             ProduccionFormulaId = info.Field<int>("ProduccionFormulaID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             FolioFormula = info.Field<int>("FolioFormula"),
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             CantidadProducida = info.Field<decimal>("CantidadProducida"),
                             FechaProduccion = info.Field<DateTime>("FechaProduccion"),
                             AlmacenMovimientoEntradaID = info.Field<long?>("AlmacenMovimientoEntradaID") != null ? info.Field<long>("AlmacenMovimientoEntradaID") : 0,
                             AlmacenMovimientoSalidaID = info.Field<long?>("AlmacenMovimientoSalidaID") != null ? info.Field<long>("AlmacenMovimientoSalidaID") : 0,
                             DescripcionFormula = info.Field<string>("Formula"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProduccionFormulaInfo>
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ProduccionFormulaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionFormulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionFormulaInfo
                         {
                             ProduccionFormulaId = info.Field<int>("ProduccionFormulaID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             FolioFormula = info.Field<int>("FolioFormula"),
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             CantidadProducida = info.Field<decimal>("CantidadProducida"),
                             FechaProduccion = info.Field<DateTime>("FechaProduccion"),
                             AlmacenMovimientoEntradaID = info.Field<long?>("AlmacenMovimientoEntradaID") != null ? info.Field<long>("AlmacenMovimientoEntradaID") : 0,
                             AlmacenMovimientoSalidaID = info.Field<long?>("AlmacenMovimientoSalidaID") != null ? info.Field<long>("AlmacenMovimientoSalidaID") : 0,
                             DescripcionFormula = info.Field<string>("Formula"),
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
        public static ProduccionFormulaInfo ObtenerPorFolioMovimiento(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionFormulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionFormulaInfo
                         {
                             ProduccionFormulaId = info.Field<int>("ProduccionFormulaID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             FolioFormula = info.Field<int>("FolioFormula"),
                             Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID"), Descripcion = info.Field<string>("Formula") },
                             CantidadProducida = info.Field<decimal>("CantidadProducida"),
                             FechaProduccion = info.Field<DateTime>("FechaProduccion"),
                             AlmacenMovimientoEntradaID = info.Field<long?>("AlmacenMovimientoEntradaID") != null ? info.Field<long>("AlmacenMovimientoEntradaID") : 0,
                             AlmacenMovimientoSalidaID = info.Field<long?>("AlmacenMovimientoSalidaID") != null ? info.Field<long>("AlmacenMovimientoSalidaID") : 0,
                             DescripcionFormula = info.Field<string>("Formula"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
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
        public static ProduccionFormulaInfo ObtenerPorIDCompleto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                ProduccionFormulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionFormulaInfo
                         {
                             ProduccionFormulaId = info.Field<int>("ProduccionFormulaID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             FolioFormula = info.Field<int>("FolioFormula"),
                             Formula = new FormulaInfo
                                 {
                                     FormulaId = info.Field<int>("FormulaID"),
                                     Descripcion = info.Field<string>("Formula"),
                                     Producto = new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         }
                                 },
                             CantidadProducida = info.Field<decimal>("CantidadProducida"),
                             FechaProduccion = info.Field<DateTime>("FechaProduccion"),
                             AlmacenMovimientoEntradaID = info.Field<long?>("AlmacenMovimientoEntradaID") != null ? info.Field<long>("AlmacenMovimientoEntradaID") : 0,
                             AlmacenMovimientoSalidaID = info.Field<long?>("AlmacenMovimientoSalidaID") != null ? info.Field<long>("AlmacenMovimientoSalidaID") : 0,
                             DescripcionFormula = info.Field<string>("Formula"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             ProduccionFormulaDetalle = (from detalle in dtDetalle.AsEnumerable()
                                                         select new ProduccionFormulaDetalleInfo
                                                             {
                                                                 ProduccionFormulaId = detalle.Field<int>("ProduccionFormulaID"),
                                                                 ProduccionFormulaDetalleId = detalle.Field<int>("ProduccionFormulaDetalleID"),
                                                                 Producto = new ProductoInfo
                                                                     {
                                                                         ProductoId = detalle.Field<int>("ProductoID"),
                                                                         ProductoDescripcion = detalle.Field<string>("Producto"),
                                                                         UnidadMedicion = new UnidadMedicionInfo
                                                                         {
                                                                             UnidadID = detalle.Field<int>("UnidadID"),
                                                                             ClaveUnidad = detalle.Field<string>("ClaveUnidad")
                                                                         }
                                                                     },
                                                                 CantidadProducto = detalle.Field<decimal>("CantidadProducto"),
                                                                 Ingrediente = new IngredienteInfo
                                                                     {
                                                                         IngredienteId = detalle.Field<int>("IngredienteID")
                                                                     },
                                                                 AlmacenInventarioLoteID = detalle.Field<int>("AlmacenInventarioLoteID")
                                                             }).ToList()
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
        /// Obtiene una lista de produccion de formula
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<ProduccionFormulaInfo> ObtenerPorConciliacion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var distribuciones = new List<ProduccionFormulaInfo>();
                ProduccionFormulaInfo distribucion;
                while (reader.Read())
                {
                    distribucion = new ProduccionFormulaInfo
                    {
                        ProduccionFormulaId = Convert.ToInt32(reader["ProduccionFormulaID"]),
                        Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                                               Descripcion = Convert.ToString(reader["Organizacion"])
                                           },
                        FolioFormula = Convert.ToInt32(reader["FolioFormula"]),
                        Formula = new FormulaInfo
                        {
                            FormulaId = Convert.ToInt32(reader["FormulaID"]),
                            Descripcion = Convert.ToString(reader["Formula"]),
                            Producto = new ProductoInfo
                            {
                                ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                ProductoDescripcion = Convert.ToString(reader["Producto"])
                            }
                        },
                        CantidadProducida = Convert.ToDecimal(reader["CantidadProducida"]),
                        FechaProduccion = Convert.ToDateTime(reader["FechaProduccion"]),
                        AlmacenMovimientoEntradaID = reader["AlmacenMovimientoEntradaID"] != null ? Convert.ToInt64(reader["AlmacenMovimientoEntradaID"]) : 0,
                        AlmacenMovimientoSalidaID = reader["AlmacenMovimientoSalidaID"] != null ? Convert.ToInt64(reader["AlmacenMovimientoSalidaID"]) : 0,
                        DescripcionFormula = Convert.ToString(reader["Formula"]),
                        Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                    };
                    distribuciones.Add(distribucion);
                }
                reader.NextResult();
                var distribucionIngredientesOrganizaciones = new List<ProduccionFormulaDetalleInfo>();
                ProduccionFormulaDetalleInfo distribucionIngredienteOrganizacion;
                while (reader.Read())
                {
                    distribucionIngredienteOrganizacion = new ProduccionFormulaDetalleInfo
                    {
                        Producto = new ProductoInfo
                        {
                            ProductoId = Convert.ToInt32(reader["ProductoID"]),
                            Descripcion = Convert.ToString(reader["Producto"]),
                            ProductoDescripcion = Convert.ToString(reader["Producto"]),
                            UnidadMedicion = new UnidadMedicionInfo
                            {
                                UnidadID = Convert.ToInt32(reader["UnidadID"]),
                                ClaveUnidad = Convert.ToString(reader["ClaveUnidad"])
                            },
                        },
                        CantidadProducto = Convert.ToDecimal(reader["CantidadProducto"]),
                        Ingrediente = new IngredienteInfo
                        {
                            IngredienteId = Convert.ToInt32(reader["IngredienteID"])
                        },
                        AlmacenInventarioLoteID =  Convert.ToInt32(reader["AlmacenInventarioLoteID"]),
                        ProduccionFormulaId = Convert.ToInt32(reader["ProduccionFormulaID"]),
                        ProduccionFormulaDetalleId = Convert.ToInt32(reader["ProduccionFormulaDetalleID"]),
                    };
                    distribucionIngredientesOrganizaciones.Add(distribucionIngredienteOrganizacion);
                }
                distribuciones.ForEach(datos =>
                                           {
                                               datos.ProduccionFormulaDetalle =
                                                   distribucionIngredientesOrganizaciones.Where(
                                                       id => id.ProduccionFormulaId == datos.ProduccionFormulaId).ToList();
                                           });
                return distribuciones;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Obtiene una lista de produccion de formula
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<ProduccionFormulaInfo> ObtenerResumenDeProduccion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var ResumenProduccionLista = new List<ProduccionFormulaInfo>();
                var formula = new ProduccionFormulaInfo();
                while (reader.Read())
                {
                    formula = new ProduccionFormulaInfo
                    {
                        Formula = new FormulaInfo
                        {
                            FormulaId = Convert.ToInt32(reader["FormulaID"]),
                            Descripcion = Convert.ToString(reader["Descripcion"])
                        },
                        CantidadProducida = Convert.ToDecimal(reader["Cantidad"]),
                        CantidadReparto = Convert.ToDecimal(reader["CantidadServida"])
                    };
                    ResumenProduccionLista.Add(formula);
                }
                return ResumenProduccionLista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
