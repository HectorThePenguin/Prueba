using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Properties;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    internal class CrearContratoBL
    {
        /// <summary>
        /// Metodo que guarda un contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="listaOtrosCostos"></param>
        /// <returns></returns>
        internal ContratoInfo Guardar(ContratoInfo contratoInfo, List<CostoInfo> listaOtrosCostos)
        {
            //var contratoCreado = new ContratoInfo();
            try
            {
                var contratoDetalleBl = new ContratoDetalleBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                var almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                var contratoParcialBl = new ContratoParcialBL();
                var almacenMovimientoInfo = new AlmacenMovimientoInfo();
                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle();
                var almacenInventarioInfo = new AlmacenInventarioInfo();
                var almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo();
                var almacenProveedor = new ProveedorAlmacenInfo();
                var almacenInfo = new AlmacenInfo();
                var contratoHumedadBl = new ContratoHumedadBL();
                decimal cantidadParcialidades = 0;
                decimal precioParcialidades = 0;
                decimal importeParcialidades = 0;
                decimal importeCostos = 0;

                int contratoID = 0;

                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();

                    contratoID = contratoInfo.ContratoId;

                    if (contratoInfo.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero || contratoInfo.TipoContrato.TipoContratoId == (int)TipoContratoEnum.EnTransito)
                    {
                        //Obtener almacen de proveedor
                        var proveedorAlmacenBl = new ProveedorAlmacenBL();
                        var proveedorInfo = new ProveedorInfo() { ProveedorID = contratoInfo.Proveedor.ProveedorID };
                        almacenProveedor = proveedorAlmacenBl.ObtenerPorProveedorId(proveedorInfo);
                        almacenInfo = new AlmacenInfo() { AlmacenID = almacenProveedor.AlmacenId };
                    }

                    //Se guarda cuando es nuevo
                    if (!contratoInfo.Guardado)
                    {
                        var contratoBl = new ContratoBL();

                        //Guardar contrato
                        var contratoInfoRet = contratoBl.Guardar(contratoInfo, TipoFolio.Contrato.GetHashCode());
                        contratoInfo.ContratoId = contratoInfoRet.ContratoId;
                        contratoInfo.Folio = contratoInfoRet.Folio;

                        //Guardar contrato detalle
                        if (contratoInfo.ListaContratoDetalleInfo != null &&
                            contratoInfo.ListaContratoDetalleInfo.Count > 0)
                        {
                            contratoDetalleBl.Guardar(contratoInfo.ListaContratoDetalleInfo, contratoInfo);
                        }
                    }

                    //Guardar humedades cuanto el contrato fue guardado previamente
                    if (contratoInfo.Guardado)
                    {
                        var listaHumedades = (from contratoHumedadInfo in contratoInfo.ListaContratoHumedad
                                              where !contratoHumedadInfo.Guardado
                                              select new ContratoHumedadInfo()
                                              {
                                                  ContratoID = contratoInfo.ContratoId,
                                                  PorcentajeHumedad = contratoHumedadInfo.PorcentajeHumedad,
                                                  FechaInicio = contratoHumedadInfo.FechaInicio,
                                                  Activo = EstatusEnum.Activo,
                                                  UsuarioCreacionId = contratoInfo.UsuarioCreacionId
                                              }).ToList();
                        if (listaHumedades.Count > 0)
                        {
                            contratoHumedadBl.CrearHumedadParcial(listaHumedades);
                        }
                        //
                    }

                    if (listaOtrosCostos != null && listaOtrosCostos.Count > 0)
                    {
                        importeCostos = listaOtrosCostos.Where(listaOtrosCosto => listaOtrosCosto.Editable).Sum(listaOtrosCosto => listaOtrosCosto.ImporteCosto);
                    }

                    //Guardar contrato parcial
                    if (contratoInfo.Parcial == CompraParcialEnum.Parcial)
                    {
                        if (contratoInfo.ListaContratoParcial != null)
                        {
                            //Guardar parcialidades
                            var listaContratoParcial = (from contratoParcialInfo in contratoInfo.ListaContratoParcial
                                where !contratoParcialInfo.Guardado
                                select new ContratoParcialInfo()
                                {
                                    ContratoId = contratoInfo.ContratoId,
                                    Cantidad = contratoParcialInfo.Cantidad,
                                    Importe = contratoParcialInfo.Importe,
                                    TipoCambio = contratoParcialInfo.TipoCambio,
                                    Activo = EstatusEnum.Activo,
                                    UsuarioCreacionId = contratoInfo.UsuarioModificacionId
                                }).ToList();
                            if (listaContratoParcial.Count > 0)
                            {
                                contratoParcialBl.CrearContratoParcial(listaContratoParcial);

                                if (contratoInfo.TipoContrato.TipoContratoId == (int) TipoContratoEnum.BodegaTercero ||
                                    contratoInfo.TipoContrato.TipoContratoId == (int) TipoContratoEnum.EnTransito)
                                {
                                    cantidadParcialidades =
                                        contratoInfo.ListaContratoParcial.Where(
                                            listaContratoP => !listaContratoP.Guardado)
                                            .Sum(listaContratoP => listaContratoP.Cantidad);
                                    precioParcialidades =
                                        contratoInfo.ListaContratoParcial.Where(
                                            listaContratoP => !listaContratoP.Guardado)
                                            .Sum(listaContratoP => listaContratoP.Importe);

                                    decimal importeAlmacenInventario;
                                    decimal cantidadAlmacenInventario = 0;

                                    //Obtener inventario actual
                                    almacenInventarioInfo =
                                        almacenInventarioBl.ObtenerPorAlmacenIdProductoId(new AlmacenInventarioInfo()
                                        {
                                            AlmacenID = almacenInfo.AlmacenID,
                                            ProductoID = contratoInfo.Producto.ProductoId,
                                            Activo = EstatusEnum.Activo
                                        });
                                    importeParcialidades = cantidadParcialidades * precioParcialidades;
                                    if (almacenInventarioInfo != null)
                                    {
                                        cantidadAlmacenInventario += almacenInventarioInfo.Cantidad +
                                                                     cantidadParcialidades;
                                        importeAlmacenInventario = almacenInventarioInfo.Importe +
                                                                   importeParcialidades + importeCostos;

                                        //Actualizar almacen inventario
                                        almacenInventarioInfo = new AlmacenInventarioInfo()
                                        {
                                            ProductoID = contratoInfo.Producto.ProductoId,
                                            AlmacenID = almacenProveedor.AlmacenId,
                                            AlmacenInventarioID = almacenInventarioInfo.AlmacenInventarioID,
                                            Minimo = almacenInventarioInfo.Minimo,
                                            Maximo = almacenInventarioInfo.Maximo,
                                            PrecioPromedio = importeAlmacenInventario/cantidadAlmacenInventario,
                                            Cantidad = cantidadAlmacenInventario,
                                            Importe = importeAlmacenInventario,
                                            UsuarioModificacionID = contratoInfo.UsuarioModificacionId
                                        };
                                        almacenInventarioBl.Actualizar(almacenInventarioInfo);
                                    }
                                    else
                                    {
                                        importeAlmacenInventario = importeParcialidades + importeCostos;
                                        //Insertar registro en almacen inventario
                                        almacenInventarioInfo = new AlmacenInventarioInfo()
                                        {
                                            AlmacenID = almacenProveedor.AlmacenId,
                                            ProductoID = contratoInfo.Producto.ProductoId,
                                            Minimo = 0,
                                            Maximo = 0,
                                            PrecioPromedio = importeAlmacenInventario/cantidadParcialidades,
                                            Cantidad = cantidadParcialidades,
                                            Importe = importeAlmacenInventario,
                                            UsuarioCreacionID = contratoInfo.UsuarioCreacionId
                                        };
                                        int almacenInventarioId = almacenInventarioBl.Crear(almacenInventarioInfo);
                                        almacenInventarioInfo.AlmacenInventarioID = almacenInventarioId;
                                    }

                                    almacenMovimientoDetalleInfo =
                                        almacenMovimientoDetalleBl.ObtenerPorContratoId(
                                            new AlmacenMovimientoDetalle()
                                            {
                                                ContratoId = contratoInfo.ContratoId
                                            });
                                    //Se obtienen movimientos detalles para el contrato si es un contrato guardado
                                    if (contratoInfo.Guardado && almacenMovimientoDetalleInfo != null)
                                    {
                                        almacenInventarioLoteInfo =
                                            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                                almacenMovimientoDetalleInfo.AlmacenInventarioLoteId);
                                        //Se actualiza el lote
                                        var importeAlmacenInventarioLote = almacenInventarioLoteInfo.Importe +
                                                                           importeParcialidades + importeCostos;
                                        var cantidadAlmacenInventarioLote = almacenInventarioLoteInfo.Cantidad +
                                                                            cantidadParcialidades;
                                        almacenInventarioLoteInfo.Cantidad = cantidadAlmacenInventarioLote;
                                        almacenInventarioLoteInfo.PrecioPromedio = importeAlmacenInventarioLote/
                                                                                   cantidadAlmacenInventarioLote;
                                        almacenInventarioLoteInfo.Importe = importeAlmacenInventarioLote;
                                        almacenInventarioLoteInfo.UsuarioModificacionId =
                                            contratoInfo.UsuarioModificacionId;
                                        almacenInventarioLoteBl.Actualizar(almacenInventarioLoteInfo);
                                    }
                                    else
                                    {
                                        //Se inserta un lote por contrato
                                        importeParcialidades = importeParcialidades + importeCostos;
                                        almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo()
                                        {
                                            AlmacenInventario =
                                                new AlmacenInventarioInfo()
                                                {
                                                    AlmacenInventarioID = almacenInventarioInfo.AlmacenInventarioID
                                                },
                                            Cantidad = cantidadParcialidades,
                                            PrecioPromedio = importeParcialidades/cantidadParcialidades,
                                            Piezas = 0,
                                            Importe = importeParcialidades,
                                            Activo = EstatusEnum.Activo,
                                            UsuarioCreacionId = contratoInfo.UsuarioCreacionId,
                                        };
                                        almacenInventarioLoteInfo.AlmacenInventarioLoteId =
                                            almacenInventarioLoteBl.Crear(almacenInventarioLoteInfo,
                                                almacenInventarioInfo);
                                    }

                                    //Guardar movimiento y movimiento detalle 
                                    if (listaContratoParcial.Count > 0)
                                    {
                                        //MAL
                                        var tipoMovimiento = contratoInfo.TipoContrato.TipoContratoId ==
                                                             (int) TipoContratoEnum.BodegaTercero
                                            ? TipoMovimiento.EntradaBodegaTerceros.GetHashCode()
                                            : TipoMovimiento.EntradaBodegaEnTránsito.GetHashCode();
                                        //
                                        foreach (var contratoParcialInfo in listaContratoParcial)
                                        {
                                            if (!contratoParcialInfo.Guardado)
                                            {
                                                //Insertar en almacen movimiento
                                                almacenMovimientoInfo = new AlmacenMovimientoInfo()
                                                {
                                                    AlmacenID = almacenInventarioInfo.AlmacenID,
                                                    TipoMovimientoID = tipoMovimiento,
                                                    ProveedorId = contratoInfo.Proveedor.ProveedorID,
                                                    Status = (int) Estatus.AplicadoInv,
                                                    UsuarioCreacionID = contratoInfo.UsuarioCreacionId
                                                };
                                                long almacenMovimientoId =
                                                    almacenMovimientoBl.Crear(almacenMovimientoInfo);
                                                almacenMovimientoInfo.AlmacenMovimientoID = almacenMovimientoId;

                                                //Insertar en almacen movimiento detalle
                                                almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle()
                                                {
                                                    AlmacenMovimientoID = almacenMovimientoInfo.AlmacenMovimientoID,
                                                    AlmacenInventarioLoteId =
                                                        almacenInventarioLoteInfo.AlmacenInventarioLoteId,
                                                    ContratoId = contratoInfo.ContratoId,
                                                    Piezas = 0,
                                                    ProductoID = contratoInfo.Producto.ProductoId,
                                                    Precio = contratoParcialInfo.Importe,
                                                    Cantidad = contratoParcialInfo.Cantidad,
                                                    Importe = contratoParcialInfo.Cantidad*contratoParcialInfo.Importe,
                                                    UsuarioCreacionID = contratoInfo.UsuarioCreacionId,
                                                };
                                                almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleInfo);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //Si hay costos se actualiza el inventario
                                var listaCostos = (from otrosCostosInfo in listaOtrosCostos
                                                            where otrosCostosInfo.Editable
                                                            select new AlmacenMovimientoCostoInfo()
                                                            ).ToList();
                                if (listaCostos.Count > 0)
                                {
                                    //Obtener inventario actual
                                    almacenInventarioInfo =
                                        almacenInventarioBl.ObtenerPorAlmacenIdProductoId(new AlmacenInventarioInfo()
                                        {
                                            AlmacenID = almacenInfo.AlmacenID,
                                            ProductoID = contratoInfo.Producto.ProductoId,
                                            Activo = EstatusEnum.Activo
                                        });
                                    if (almacenInventarioInfo != null)
                                    {
                                        almacenInventarioInfo.Importe = almacenInventarioInfo.Importe +
                                                                   importeCostos;

                                        //Actualizar almacen inventario
                                        almacenInventarioInfo = new AlmacenInventarioInfo()
                                        {
                                            ProductoID = contratoInfo.Producto.ProductoId,
                                            AlmacenID = almacenProveedor.AlmacenId,
                                            AlmacenInventarioID = almacenInventarioInfo.AlmacenInventarioID,
                                            Minimo = almacenInventarioInfo.Minimo,
                                            Maximo = almacenInventarioInfo.Maximo,
                                            PrecioPromedio = almacenInventarioInfo.Importe/almacenInventarioInfo.Cantidad,
                                            Cantidad = almacenInventarioInfo.Cantidad,
                                            Importe = almacenInventarioInfo.Importe,
                                            UsuarioModificacionID = contratoInfo.UsuarioModificacionId
                                        };
                                        almacenInventarioBl.Actualizar(almacenInventarioInfo);
                                    }

                                    almacenMovimientoDetalleInfo =
                                        almacenMovimientoDetalleBl.ObtenerPorContratoId(
                                            new AlmacenMovimientoDetalle()
                                            {
                                                ContratoId = contratoInfo.ContratoId
                                            });
                                    //Se obtienen movimientos detalles para el contrato si es un contrato guardado
                                    if (contratoInfo.Guardado && almacenMovimientoDetalleInfo != null)
                                    {
                                        almacenInventarioLoteInfo =
                                            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                                almacenMovimientoDetalleInfo.AlmacenInventarioLoteId);
                                        //Se actualiza el lote
                                        almacenInventarioLoteInfo.Importe = almacenInventarioLoteInfo.Importe + importeCostos;
                                        almacenInventarioLoteInfo.Cantidad = almacenInventarioLoteInfo.Cantidad;
                                        almacenInventarioLoteInfo.PrecioPromedio = almacenInventarioLoteInfo.Importe /
                                                                                   almacenInventarioLoteInfo.Cantidad;
                                        almacenInventarioLoteInfo.Importe = almacenInventarioLoteInfo.Importe;
                                        almacenInventarioLoteInfo.UsuarioModificacionId =
                                            contratoInfo.UsuarioModificacionId;
                                        almacenInventarioLoteBl.Actualizar(almacenInventarioLoteInfo);
                                    }
                                }
                            }
                        }
                }
                else
                {
                    //Afectar inventario cuando el contrato no este guardado y el contrato sea total
                    if (contratoInfo.TipoContrato.TipoContratoId == (int) TipoContratoEnum.BodegaTercero ||
                        contratoInfo.TipoContrato.TipoContratoId == (int) TipoContratoEnum.EnTransito)
                    {
                        decimal importeAlmacenInventario;
                        decimal cantidadAlmacenInventario = 0;
                        var tipoMovimiento = contratoInfo.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero
                        ? TipoMovimiento.EntradaBodegaTerceros.GetHashCode()
                        : TipoMovimiento.EntradaBodegaEnTránsito.GetHashCode();

                        //Si no se encuentra el producto en almaceninventario se inserta
                        var contratoImporte = contratoInfo.Cantidad * contratoInfo.Precio;
                        almacenInventarioInfo =
                        almacenInventarioBl.ObtenerPorAlmacenIdProductoId(new AlmacenInventarioInfo()
                        {
                            AlmacenID = almacenInfo.AlmacenID,
                            ProductoID = contratoInfo.Producto.ProductoId,
                            Activo = EstatusEnum.Activo
                        });
                        if (almacenInventarioInfo != null)
                        {
                            if (!contratoInfo.Guardado)
                            {
                                cantidadAlmacenInventario += almacenInventarioInfo.Cantidad + contratoInfo.Cantidad;
                                importeAlmacenInventario = almacenInventarioInfo.Importe + contratoImporte + importeCostos;
                            }
                            else
                            {
                                cantidadAlmacenInventario += almacenInventarioInfo.Cantidad;
                                importeAlmacenInventario = almacenInventarioInfo.Importe + importeCostos;
                            }
                            //Actualizar almacen inventario
                            almacenInventarioInfo = new AlmacenInventarioInfo()
                            {
                                ProductoID = contratoInfo.Producto.ProductoId,
                                AlmacenID = almacenProveedor.AlmacenId,
                                AlmacenInventarioID = almacenInventarioInfo.AlmacenInventarioID,
                                Minimo = almacenInventarioInfo.Minimo,
                                Maximo = almacenInventarioInfo.Maximo,
                                PrecioPromedio = importeAlmacenInventario / cantidadAlmacenInventario,
                                Cantidad = cantidadAlmacenInventario,
                                Importe = importeAlmacenInventario,
                                UsuarioModificacionID = contratoInfo.UsuarioModificacionId
                            };
                            almacenInventarioBl.Actualizar(almacenInventarioInfo);
                        }
                        else
                        {
                            importeAlmacenInventario = contratoImporte;
                            importeAlmacenInventario += importeCostos;

                            //Insertar registro en almacen inventario
                            almacenInventarioInfo = new AlmacenInventarioInfo()
                            {
                                AlmacenID = almacenProveedor.AlmacenId,
                                ProductoID = contratoInfo.Producto.ProductoId,
                                Minimo = 0,
                                Maximo = 0,
                                PrecioPromedio = importeAlmacenInventario / contratoInfo.Cantidad,
                                Cantidad = contratoInfo.Cantidad,
                                Importe = importeAlmacenInventario,
                                UsuarioCreacionID = contratoInfo.UsuarioCreacionId
                            };
                            int almacenInventarioId = almacenInventarioBl.Crear(almacenInventarioInfo);
                            almacenInventarioInfo.AlmacenInventarioID = almacenInventarioId;
                        }

                        //Se el contrato ya esta guardado solo se agrega el costo al lote si no se inserta
                        if (contratoInfo.Guardado)
                        {
                            almacenMovimientoDetalleInfo =
                                almacenMovimientoDetalleBl.ObtenerPorContratoId(
                                    new AlmacenMovimientoDetalle()
                                    {
                                        ContratoId = contratoInfo.ContratoId
                                    });
                            almacenInventarioLoteInfo =
                                almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                    almacenMovimientoDetalleInfo.AlmacenInventarioLoteId);
                            //Se actualiza el lote
                            var importeAlmacenInventarioLote = almacenInventarioLoteInfo.Importe + importeCostos;
                            almacenInventarioLoteInfo.PrecioPromedio = importeAlmacenInventarioLote /
                                                                       almacenInventarioLoteInfo.Cantidad;
                            almacenInventarioLoteInfo.Importe = importeAlmacenInventarioLote;
                            almacenInventarioLoteInfo.UsuarioModificacionId =
                                contratoInfo.UsuarioModificacionId;
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLoteInfo);
                        }
                        else
                        {
                            importeAlmacenInventario = contratoInfo.Precio * contratoInfo.Cantidad;
                            importeAlmacenInventario += importeCostos;
                            almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo()
                            {
                                AlmacenInventario =
                                    new AlmacenInventarioInfo()
                                    {
                                        AlmacenInventarioID = almacenInventarioInfo.AlmacenInventarioID
                                    },
                                Cantidad = contratoInfo.Cantidad,
                                PrecioPromedio = importeAlmacenInventario / contratoInfo.Cantidad,
                                Piezas = 0,
                                Importe = importeAlmacenInventario,
                                Activo = EstatusEnum.Activo,
                                UsuarioCreacionId = contratoInfo.UsuarioCreacionId,
                            };
                            int almacenInventarioLoteId = almacenInventarioLoteBl.Crear(almacenInventarioLoteInfo,
                                almacenInventarioInfo);
                            almacenInventarioLoteInfo.AlmacenInventarioLoteId = almacenInventarioLoteId;

                            //Insertar en almacen movimiento
                            almacenMovimientoInfo = new AlmacenMovimientoInfo()
                            {
                                AlmacenID = almacenInventarioInfo.AlmacenID,
                                TipoMovimientoID = tipoMovimiento,
                                ProveedorId = contratoInfo.Proveedor.ProveedorID,
                                Status = (int)Estatus.AplicadoInv,
                                UsuarioCreacionID = contratoInfo.UsuarioCreacionId
                            };
                            long almacenMovimientoId = almacenMovimientoBl.Crear(almacenMovimientoInfo);
                            almacenMovimientoInfo.AlmacenMovimientoID = almacenMovimientoId;

                            //Insertar en almacen movimiento detalle
                            almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle()
                            {
                                AlmacenMovimientoID = almacenMovimientoInfo.AlmacenMovimientoID,
                                AlmacenInventarioLoteId = almacenInventarioLoteInfo.AlmacenInventarioLoteId,
                                ContratoId = contratoInfo.ContratoId,
                                Piezas = 0,
                                ProductoID = contratoInfo.Producto.ProductoId,
                                Precio = contratoInfo.Precio,
                                Cantidad = contratoInfo.Cantidad,
                                Importe = contratoInfo.Cantidad * contratoInfo.Precio,
                                UsuarioCreacionID = contratoInfo.UsuarioCreacionId,
                            };
                            almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleInfo);
                        }

                    }
                }

                    //Guardar costos
                    //Se guardan costos cuando el contrato sea bodega terceros o en transito
                    if (contratoInfo.TipoContrato.TipoContratoId != TipoContratoEnum.BodegaNormal.GetHashCode())
                    {
                        almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle()
                        {
                            ContratoId = contratoInfo.ContratoId
                        };
                        almacenMovimientoDetalleInfo =
                            almacenMovimientoDetalleBl.ObtenerPorContratoId(almacenMovimientoDetalleInfo);

                        if (almacenMovimientoDetalleInfo != null)
                        {
                            if (listaOtrosCostos != null && listaOtrosCostos.Count > 0)
                            {
                                var listaAlmacenMovimientoCostoInfo = (from costoInfo in listaOtrosCostos
                                                                       where costoInfo.Editable
                                                                       select new AlmacenMovimientoCostoInfo()
                                                                       {
                                                                           AlmacenMovimientoId = almacenMovimientoDetalleInfo.AlmacenMovimientoID,
                                                                           Proveedor = costoInfo.Proveedor,
                                                                           CuentaSap = costoInfo.CuentaSap,
                                                                           Costo = new CostoInfo(){CostoID = costoInfo.CostoID},
                                                                           Importe = costoInfo.ImporteCosto,
                                                                           Cantidad = costoInfo.ToneladasCosto * 1000,
                                                                           Activo = EstatusEnum.Activo,
                                                                           TieneCuenta = costoInfo.TieneCuenta,
                                                                           Iva = costoInfo.AplicaIva,
                                                                           Retencion = costoInfo.AplicaRetencion,
                                                                           UsuarioCreacionId = contratoInfo.UsuarioCreacionId
                                                                       }).ToList();
                                if (listaAlmacenMovimientoCostoInfo.Count > 0)
                                {
                                    almacenMovimientoCostoBl.CrearCostos(listaAlmacenMovimientoCostoInfo);
                                }
                            }
                        }
                    }

                    #region POLIZA          

                    if (almacenMovimientoInfo != null && almacenMovimientoDetalleInfo != null)
                    {
                        long almacenMovimientoID = almacenMovimientoInfo.AlmacenMovimientoID == 0
                            ? almacenMovimientoDetalleInfo.AlmacenMovimientoID
                            : almacenMovimientoInfo.AlmacenMovimientoID;
                        almacenMovimientoInfo = almacenMovimientoBl.ObtenerPorId(almacenMovimientoID);
                    }
                    if (almacenMovimientoInfo == null)
                    {
                        almacenMovimientoInfo = new AlmacenMovimientoInfo();
                    }

                    var polizaContratoModel = new PolizaContratoModel
                    {
                        Contrato = contratoInfo,
                        OtrosCostos = listaOtrosCostos,
                        AlmacenMovimiento = almacenMovimientoInfo
                    };
                    PolizaAbstract poliza = null;
                    IList<PolizaInfo> polizaContrato;
                    var polizaBL = new PolizaBL();
                    var tipoPolizaBL = new TipoPolizaBL();
                    var tipoPoliza = TipoPoliza.PolizaContratoTerceros;
                    if(contratoInfo.ListaContratoParcial == null)
                    {
                        contratoInfo.ListaContratoParcial = new List<ContratoParcialInfo>();
                    }
                    if (contratoID == 0 || contratoInfo.ListaContratoParcial.Any(parcial=> !parcial.Guardado))
                    {
                        if (contratoInfo.TipoContrato.TipoContratoId != TipoContratoEnum.BodegaNormal.GetHashCode())
                        {
                            var tipoContrato = (TipoContratoEnum) contratoInfo.TipoContrato.TipoContratoId;
                            switch (tipoContrato)
                            {
                                case TipoContratoEnum.BodegaTercero:
                                    tipoPoliza = TipoPoliza.PolizaContratoTerceros;
                                    poliza =
                                        FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(
                                            TipoPoliza.PolizaContratoTerceros);
                                    break;
                                case TipoContratoEnum.EnTransito:
                                    tipoPoliza = TipoPoliza.PolizaContratoTransito;
                                    poliza =
                                        FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(
                                            TipoPoliza.PolizaContratoTransito);
                                    break;
                            }

                            if (contratoInfo.Parcial == CompraParcialEnum.Parcial)
                            {
                                poliza =
                                       FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(
                                           TipoPoliza.PolizaContratoParcialidades);
                                if (contratoInfo.ListaContratoParcial != null && contratoInfo.ListaContratoParcial.Any())
                                {
                                    foreach (var parcialidad in contratoInfo.ListaContratoParcial.Where(parci=> !parci.Guardado))
                                    {
                                        polizaContratoModel.ContratoParcial = parcialidad;
                                        polizaContrato = poliza.GeneraPoliza(polizaContratoModel);
                                        GuardarPoliza(polizaBL, polizaContrato, contratoInfo, tipoPoliza);
                                    }
                                }
                            }
                            else
                            {
                                TipoPolizaInfo tipoPolizaInfo = tipoPolizaBL.ObtenerPorID(tipoPoliza.GetHashCode());
                                polizaContrato = polizaBL.ObtenerPoliza(tipoPoliza,
                                                                        contratoInfo.Organizacion.OrganizacionID,
                                                                        contratoInfo.Fecha,
                                                                        contratoInfo.Folio.ToString(),
                                                                        tipoPolizaInfo.ClavePoliza, 1);
                                if (polizaContrato == null || !polizaContrato.Any())
                                {
                                    polizaContrato = poliza.GeneraPoliza(polizaContratoModel);
                                    GuardarPoliza(polizaBL, polizaContrato, contratoInfo, tipoPoliza);
                                }
                            }
                        }
                    }
                    if (listaOtrosCostos != null && listaOtrosCostos.Any())
                    {
                        if (listaOtrosCostos.Any(costo=> costo.Editable))
                        {
                            //long almacenMovimientoID = almacenMovimientoInfo.AlmacenMovimientoID == 0
                            //                       ? almacenMovimientoDetalleInfo.AlmacenMovimientoID
                            //                       : almacenMovimientoInfo.AlmacenMovimientoID;
                            //almacenMovimientoInfo = almacenMovimientoBl.ObtenerPorId(almacenMovimientoID);
                            polizaContratoModel.AlmacenMovimiento = almacenMovimientoInfo;
                            tipoPoliza = TipoPoliza.PolizaContratoOtrosCostos;
                            polizaContratoModel.OtrosCostos =
                                polizaContratoModel.OtrosCostos.Where(costo => costo.Editable).ToList();
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
                            polizaContrato = poliza.GeneraPoliza(polizaContratoModel);
                            GuardarPoliza(polizaBL, polizaContrato, contratoInfo, tipoPoliza);
                        }
                    }
                    #endregion POLIZA

                    transaction.Complete();
                }
            }
            catch(ExcepcionServicio)
            {
                throw;    
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
            return contratoInfo;
        }

        private void GuardarPoliza(PolizaBL polizaBL, IList<PolizaInfo> polizaContrato, ContratoInfo contratoInfo, TipoPoliza tipoPoliza)
        {
            if (polizaContrato != null && polizaContrato.Any())
            {
                polizaContrato.ToList().ForEach(datos =>
                {
                    datos.OrganizacionID =
                        contratoInfo.Organizacion.OrganizacionID;
                    datos.UsuarioCreacionID =
                        contratoInfo.UsuarioCreacionId;
                    datos.ArchivoEnviadoServidor = 1;
                });
                polizaBL.GuardarServicioPI(polizaContrato, tipoPoliza);
            }
        }

        /// <summary>
        /// Metodo que actualiza el estado de un contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal void ActualizarEstado(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var contratoBl = new ContratoBL();
                    contratoBl.ActualizarEstado(contratoInfo, EstatusEnum.Inactivo);

                    if (contratoInfo.ListaContratoDetalleInfo != null)
                    {
                        var contratoDetalleBl = new ContratoDetalleBL();
                        foreach (var contratoDetalleInfo in contratoInfo.ListaContratoDetalleInfo)
                        {
                            contratoDetalleInfo.UsuarioModificacionId = contratoInfo.UsuarioModificacionId;
                            contratoDetalleBl.ActualizarEstado(contratoDetalleInfo, EstatusEnum.Inactivo);
                        }
                    }

                    if (contratoInfo.Parcial == CompraParcialEnum.Parcial)
                    {
                        if (contratoInfo.ListaContratoParcial != null)
                        {
                            var contratoParcialBl = new ContratoParcialBL();
                            foreach (var contratoParcialInfo in contratoInfo.ListaContratoParcial)
                            {
                                contratoParcialInfo.UsuarioModificacionId = contratoInfo.UsuarioModificacionId;
                                contratoParcialBl.ActualizarEstado(contratoParcialInfo, EstatusEnum.Inactivo);
                            }
                        }
                    }

                    transaction.Complete();
                }
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
        /// Imprime el ticket del contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        internal void ImprimirContrato(ContratoInfo contratoInfo)
        {
            var reporte = new Document(PageSize.A4, 10, 10, 35, 75);
            try
            {
                const string nombreArchivo = "Contrato.pdf";

                if (File.Exists(nombreArchivo))
                {
                    try
                    {
                        File.Delete(nombreArchivo);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoEnUso);
                    }
                }

                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));
                reporte.Open();

                var dirLogo = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\skLogo.png";
                var fuenteDatos = new Font { Size = 8, Color = Color.BLACK };

                var imgSuperior = Image.GetInstance(dirLogo);
                imgSuperior.ScaleAbsolute(90f, 25f);

                //float[] medidaCeldas = { 0.75f, 1.20f, 0.75f, 0.75f, 0.75f, 1.20f, 1.0f, 2.25f, 0.85f, 0.85f, 0.85f };
                var table = new PdfPTable(4);
                //table.SetWidths(medidaCeldas);
                //

                //////TITULO
                var cell = new PdfPCell(imgSuperior) { Padding = 2, Border = 0 };
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.BorderWidth = 1.2f;
                cell.BackgroundColor = Color.RED;// new Color(0xC0, 0xC0, 0xC0);
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Organizacion.Descripcion, FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD)));
                // the first cell spans 10 columns
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                cell.BorderWidth = 1.2f;
                cell.BackgroundColor = Color.RED;// new Color(0xC0, 0xC0, 0xC0);
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) { Border = 0 };
                table.AddCell(cell);
                reporte.Add(table);

                cell = new PdfPCell(new Phrase(" ", fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                //////////////Datos generales
                table = new PdfPTable(8);
                //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //Font fuenteDatosCursiva = new Font(bfTimes, 9, Font.ITALIC, Color.RED);

                var fuenteDatosCursiva = new Font { Size = 10, Color = Color.BLACK, };

                cell = new PdfPCell(new Phrase("Contrato de Compra de Materia Prima", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.TOP_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                ///////
                cell = new PdfPCell(new Phrase("Fecha:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Fecha.ToString("dd'/'MM'/'yyyy"), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Folio:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Folio.ToString(CultureInfo.InvariantCulture), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);
                ///////

                ///////
                cell = new PdfPCell(new Phrase("División:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Organizacion.Descripcion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Producto:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Producto.ProductoDescripcion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);
                ///////

                ///////
                cell = new PdfPCell(new Phrase("Cuenta:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                var descripcionCuenta = contratoInfo.TipoContrato.TipoContratoId != TipoContratoEnum.BodegaNormal.GetHashCode() ? contratoInfo.Cuenta.Descripcion : string.Empty;
                cell = new PdfPCell(new Phrase(descripcionCuenta, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Proveedor:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Proveedor.Descripcion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);
                ///////

                ///////
                cell = new PdfPCell(new Phrase("Tipo Compra:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.TipoContrato.Descripcion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Toneladas:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(String.Format("{0} Ton", contratoInfo.CantidadToneladas.ToString("N3", CultureInfo.InvariantCulture)), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moneda:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                var descripcionMoneda = contratoInfo.TipoCambio.Descripcion == ResourceServices.ContratoBL_DescripcionMonedaDolarMayuscula ? ResourceServices.ContratoBL_DescripcionMonedaDolar : ResourceServices.ContratoBL_DescripcionMonedaPesos;
                cell = new PdfPCell(new Phrase(descripcionMoneda, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);
                ///////

                ///////
                cell = new PdfPCell(new Phrase("Precio:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                var moneda = contratoInfo.TipoCambio.Descripcion == ResourceServices.ContratoBL_DescripcionMonedaDolarMayuscula ? " Dlls" : " Pesos";
                if (contratoInfo.TipoCambio.Descripcion == ResourceServices.ContratoBL_DescripcionMonedaDolarMayuscula)
                {
                    cell = new PdfPCell(new Phrase(String.Format("{0} {1}", contratoInfo.PrecioToneladas.ToString("N2", CultureInfo.InvariantCulture), moneda), fuenteDatosCursiva))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                        Colspan = 2
                    };
                    table.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(String.Format("{0} {1}", contratoInfo.PrecioToneladas.ToString("N2", CultureInfo.InvariantCulture), moneda), fuenteDatosCursiva))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                        Colspan = 2
                    };
                    table.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase("Compra:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                var compra = contratoInfo.Parcial == CompraParcialEnum.Parcial ? "Parcial" : "Total";
                cell = new PdfPCell(new Phrase(compra, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Merma Permitida:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Merma.ToString(CultureInfo.InvariantCulture) + " %", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);
                ///////

                cell = new PdfPCell(new Phrase("Tolerancia:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.Tolerancia.ToString(CultureInfo.InvariantCulture) + " %", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);
                ///////
                cell = new PdfPCell(new Phrase("Flete:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.TipoFlete.Descripcion.ToString(CultureInfo.InvariantCulture), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Peso Negociado:", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(contratoInfo.PesoNegociar.ToString(CultureInfo.InvariantCulture), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Empty, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Empty, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Indicadores de Calidad", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.TOP_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Indicador", fuenteDatosCursiva))
                {
                    BackgroundColor = Color.RED,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 4
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("% Permitido", fuenteDatosCursiva))
                {
                    BackgroundColor = Color.RED,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 4
                };
                table.AddCell(cell);

                foreach (var indicadorInfo in contratoInfo.ListaContratoDetalleInfo)
                {
                    cell = new PdfPCell(new Phrase(indicadorInfo.Indicador.Descripcion, fuenteDatosCursiva))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                        Colspan = 4
                    };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(indicadorInfo.PorcentajePermitido.ToString("N2", CultureInfo.InvariantCulture), fuenteDatosCursiva))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER,
                        Colspan = 4
                    };
                    table.AddCell(cell);
                }
                ///////

                reporte.Add(table);

                reporte.Close();

                SendToPrinter(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (DocumentException ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (IOException ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Enviar la pantalla de impresion
        /// </summary>
        /// <param name="archivo"></param>
        private void SendToPrinter(string archivo)
        {
            try
            {
                Process.Start(archivo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
    }
}