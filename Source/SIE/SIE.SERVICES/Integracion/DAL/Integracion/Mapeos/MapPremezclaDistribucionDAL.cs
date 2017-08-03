using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPremezclaDistribucionDAL
    {
        /// <summary>
        /// Obtiene la entidad de la premezcla
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PremezclaDistribucionInfo ObtenerPremezclaDistribucion(DataSet ds)
        {
            PremezclaDistribucionInfo premezcla;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                premezcla = (from info in dt.AsEnumerable()
                             select new PremezclaDistribucionInfo
                         {
                             PremezclaDistribucionId = info.Field<int>("PremezclaDistribucionID"),
                             ProductoId = info.Field<int>("ProductoID"),
                             FechaEntrada = info["FechaEntrada"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaEntrada"),
                             CantidadExistente = info.Field<long>("CantidadExistente"),
                             CostoUnitario = info.Field<decimal>("CostoUnitario"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return premezcla;
        }

        /// <summary>
        /// Obtiene una lista de distribucion de ingredientes
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<DistribucionDeIngredientesInfo> ObtenerPremezclaDistribucionConciliacion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var distribuciones = new List<DistribucionDeIngredientesInfo>();
                DistribucionDeIngredientesInfo distribucion;
                while (reader.Read())
                {
                    distribucion = new DistribucionDeIngredientesInfo
                    {
                        Producto = new ProductoInfo
                                       {
                                           ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                           Descripcion = Convert.ToString(reader["Producto"]),
                                           UnidadId = Convert.ToInt32(reader["UnidadID"]),
                                           SubfamiliaId = Convert.ToInt32(reader["SubFamiliaID"])
                                       },
                        Iva = Convert.ToInt32(reader["IVA"]),
                        CantidadExistente = Convert.ToInt32(reader["CantidadExistente"]),
                        CostoUnitario = Convert.ToInt32(reader["CostoUnitario"]),
                        PremezclaDistribucionID = Convert.ToInt32(reader["PremezclaDistribucionID"]),
                        FechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
                        AlmaceMovimientoID = Convert.ToInt64(reader["AlmacenMovimientoID"]),
                        Proveedor = new ProveedorInfo
                        {
                            ProveedorID = Convert.ToInt32(reader["ProveedorID"]),
                            Descripcion = Convert.ToString(reader["Proveedor"]),
                            CodigoSAP = Convert.ToString(reader["CodigoSAP"])
                        }
                    };
                    distribuciones.Add(distribucion);
                }
                reader.NextResult();
                var distribucionIngredientesOrganizaciones = new List<DistribucionDeIngredientesOrganizacionInfo>();
                DistribucionDeIngredientesOrganizacionInfo distribucionIngredienteOrganizacion;
                while (reader.Read())
                {
                    distribucionIngredienteOrganizacion = new DistribucionDeIngredientesOrganizacionInfo
                    {
                        Lote = new AlmacenInventarioLoteInfo
                                   {
                                       Lote = Convert.ToInt32(reader["Lote"]),
                                       AlmacenInventario = new AlmacenInventarioInfo
                                                               {
                                                                   Almacen = new AlmacenInfo
                                                                                 {
                                                                                     AlmacenID = Convert.ToInt32(reader["AlmacenID"])
                                                                                 }
                                                               }
                                   },
                        Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = Convert.ToInt32(reader["OrganizacionID"])
                                           },
                        CantidadSurtir = Convert.ToInt32(reader["CantidadASurtir"]),
                        CantidadNueva = Convert.ToInt32(reader["CantidadASurtir"]),
                        PremezclaDistribucionID = Convert.ToInt32(reader["PremezclaDistribucionID"]),
                    };
                    distribucionIngredientesOrganizaciones.Add(distribucionIngredienteOrganizacion);
                }
                distribuciones.ForEach(datos =>
                                           {
                                               datos.ListaOrganizaciones =
                                                   distribucionIngredientesOrganizaciones.Where(
                                                       id => id.PremezclaDistribucionID == datos.PremezclaDistribucionID)
                                                       .Select(org => new DistribucionDeIngredientesOrganizacionInfo
                                                                          {
                                                                              Lote = org.Lote,
                                                                              Organizacion = org.Organizacion,
                                                                              CantidadSurtir = org.CantidadNueva,
                                                                              PremezclaDistribucionID =
                                                                                  org.PremezclaDistribucionID,
                                                                              CostoTotal =
                                                                                  org.CantidadNueva*datos.CostoUnitario
                                                                          }).ToList();
                                           });
                return distribuciones;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        internal static List<PremezclaDistribucionCostoInfo> ObtenerPremezclaDistribucionCosto(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var costos = new List<PremezclaDistribucionCostoInfo>();
                PremezclaDistribucionCostoInfo costo;
                while (reader.Read())
                {
                    bool tieneCuenta = Convert.ToBoolean(reader["TieneCuenta"]);
                    costo = new PremezclaDistribucionCostoInfo
                    {

                        PremezcaDistribucionCostoID = Convert.ToInt64(reader["PremezclaDistribucionCostoID"]),
                        PremezclaDistribucionID = Convert.ToInt32(reader["PremezclaDistribucionID"]),
                        Costo = new CostoInfo
                        {
                            CostoID = Convert.ToInt32(reader["CostoID"])
                        },
                        TieneCuenta = Convert.ToBoolean(reader["TieneCuenta"]),
                        Proveedor = !tieneCuenta ? new ProveedorInfo { ProveedorID = Convert.ToInt32(reader["ProveedorID"]), Descripcion = Convert.ToString(reader["DescripcionProveedor"]), CodigoSAP = Convert.ToString(reader["CodigoSAP"]) } : null,
                        //CuentaProvision = tieneCuenta ? new CuentaSAPInfo { CuentaSAP = Convert.ToString(reader["CuentaProvision"]), Descripcion = Convert.ToString(reader["DescripcionCuenta"]) }: null,
                        CuentaProvision = tieneCuenta ? Convert.ToString(reader["CuentaProvision"]):String.Empty,
                        DescripcionCuenta = tieneCuenta ? Convert.ToString(reader["DescripcionProveedor"]):String.Empty,
                        Importe = Convert.ToDecimal(reader["Importe"]),
                        Iva = Convert.ToBoolean(reader["Iva"]),
                        Retencion = Convert.ToBoolean(reader["Retencion"]),
                        EditarCuentaProveedor = true,
                        EditarIvaRetencion = true,
                        EditarTieneCuenta = true,
                        CostoEmbarque = true

                    };
                    costos.Add(costo);
                }
                return costos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
    }