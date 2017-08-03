using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaPremezcla : PolizaAbstract
    {
        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var distribucionIngredientes = datosPoliza as DistribucionDeIngredientesInfo;
            IList<PolizaInfo> polizas = ObtenerPoliza(distribucionIngredientes);
            return polizas;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(DistribucionDeIngredientesInfo distribucionIngredientes)
        {
            var polizaPremezcla = new List<PolizaInfo>();

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            IList<UnidadMedicionInfo> unidadesMedicion = ObtenerUnidadesMedicion();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.PolizaPremezcla.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA", TipoPoliza.PolizaPremezcla));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            var renglon = 0;
            int milisegundo = DateTime.Now.Millisecond;

            ProductoInfo producto = distribucionIngredientes.Producto;

            DateTime fecha = distribucionIngredientes.FechaEntrada;
            int folio = producto.ProductoId;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(folio).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(milisegundo);
            ref3.Append(postFijoRef3);

            var almacenMovimientoBL = new AlmacenMovimientoBL();

            string archivoFolio = ObtenerArchivoFolio(fecha);
            //string numeroReferencia = ObtenerNumeroReferencia;

            List<DistribucionDeIngredientesOrganizacionInfo> organizaciones = distribucionIngredientes.ListaOrganizaciones;
            DistribucionDeIngredientesOrganizacionInfo organizacionDistribucion;
            ClaseCostoProductoInfo claseCostoProducto;
            CuentaAlmacenSubFamiliaInfo almacenesSubFamilia;
            CuentaSAPInfo cuentaSap;
            PolizaInfo poliza;
            DatosPolizaInfo datos;

            bool tieneIva = distribucionIngredientes.Iva == 1;
            bool costosTienenIva =
                distribucionIngredientes.ListaPremezclaDistribucionCosto.Any(tieneIVA => tieneIVA.Iva);

            string unidad =
                unidadesMedicion.Where(clave => clave.UnidadID == producto.UnidadId).Select(uni => uni.ClaveUnidad).
                    FirstOrDefault();

            OrganizacionInfo organizacion = null;
            decimal totalIva = 0;

            /* Se calcula el costo extra y se proratea entre las organizaciones*/
            decimal costoTotal = 0;
            decimal costoUnitario = 0;

            organizaciones = organizaciones.Where(cant => cant.CantidadSurtir > 0 || cant.CantidadNueva > 0).ToList();
            int cantidad;
            string numeroReferencia = string.Empty;
            for (var indexOrganizaciones = 0; indexOrganizaciones < organizaciones.Count; indexOrganizaciones++) //Polizas por organizacion  
            {
                organizacionDistribucion = organizaciones[indexOrganizaciones];
                AlmacenMovimientoInfo movimientoGenerado = almacenMovimientoBL.ObtenerPorId(organizacionDistribucion.AlmaceMovimiento.AlmacenMovimientoID);
                numeroReferencia = ObtenerNumeroReferenciaFolio(movimientoGenerado.FolioMovimiento);
                cuentaSap = null;
                if (organizacionDistribucion.Lote.AlmacenInventario.Almacen.TipoAlmacenID ==
                    TipoAlmacenEnum.Enfermeria.GetHashCode()
                    ||
                    organizacionDistribucion.Lote.AlmacenInventario.Almacen.TipoAlmacenID ==
                    TipoAlmacenEnum.ManejoGanado.GetHashCode())
                {
                    IList<CuentaAlmacenSubFamiliaInfo> cuentasAlmacenSubFamilia =
                        ObtenerCostosSubFamilia(organizacionDistribucion.Lote.AlmacenInventario.Almacen.AlmacenID);

                    almacenesSubFamilia =
                        cuentasAlmacenSubFamilia.FirstOrDefault(sub => sub.SubFamiliaID == producto.SubfamiliaId);
                    if (almacenesSubFamilia != null)
                    {
                        cuentaSap =
                            cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenesSubFamilia.CuentaSAPID);
                    }
                }
                else
                {
                    IList<ClaseCostoProductoInfo> claseCostosProductos =
                        ObtenerCostosProducto(organizacionDistribucion.Lote.AlmacenInventario.Almacen.AlmacenID);
                    claseCostoProducto =
                        claseCostosProductos.FirstOrDefault(prod => prod.ProductoID == producto.ProductoId);
                    if (claseCostoProducto != null)
                    {
                        cuentaSap =
                            cuentasSap.FirstOrDefault(clave => clave.CuentaSAPID == claseCostoProducto.CuentaSAPID);
                    }
                }


                if (cuentaSap == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO HAY CONFIGURACION PARA EL PRODUCTO",
                                                              producto.Descripcion));
                }
                organizacion = ObtenerOrganizacionIVA(organizacionDistribucion.Organizacion.OrganizacionID);
                cantidad = organizacionDistribucion.CantidadSurtir;
                if (cantidad == 0)
                {
                    cantidad = organizacionDistribucion.CantidadNueva;
                }

                if (distribucionIngredientes.ListaOrganizaciones.Any())
                {
                    costoTotal = distribucionIngredientes.ListaPremezclaDistribucionCosto.Sum(c => c.Importe);
                    costoUnitario = costoTotal / distribucionIngredientes.CantidadTotal;
                }
                //Polizas de Cargo por Organizacion
                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = fecha,
                                Folio = numeroReferencia,
                                Importe =
                                    string.Format("{0}",
                                                  (organizacionDistribucion.CostoTotal + (costoUnitario * organizacionDistribucion.CantidadSurtir)).ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(++renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSap.CuentaSAP,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaSap.Descripcion,
                                PesoOrigen =
                                    Math.Round(Convert.ToDecimal(organizacionDistribucion.CantidadSurtir), 2),
                                Division = organizacion.Division,
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                                         tipoMovimiento,
                                                         numeroReferencia,
                                                         cantidad.ToString("F0"),
                                                         unidad, producto.Descripcion,
                                                         (organizacionDistribucion.CostoTotal).ToString("F2"),
                                                         postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                poliza = GeneraRegistroPoliza(datos);
                polizaPremezcla.Add(poliza);
                
                //Si el cabecero tiene IVA o los costos tienen iva, crea una linea de iva por organizacion en la poliza
                if (tieneIva || costosTienenIva)
                {
                    decimal importeIva = tieneIva?
                            organizacionDistribucion.CostoTotal*(organizacion.Iva.TasaIva/100):0;
                    totalIva += importeIva;
                    CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                        clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                    if (cuentaIva == null)
                    {
                        throw new ExcepcionServicio(
                            string.Format("CUENTA DE IVA NO CONFIGURADA PARA LA ORGANIZACION {0}",
                                          organizacion.Descripcion));
                    }

                    decimal porcientoOrg = (decimal)organizacionDistribucion.CantidadSurtir / (decimal)distribucionIngredientes.CantidadTotal;

                    var totalCostosIva = costosTienenIva?
                        distribucionIngredientes.ListaPremezclaDistribucionCosto.Where(iva => iva.Iva).Sum(sumIva => sumIva.Importe * (organizacion.Iva.TasaIva / 100)):0;
                    decimal porcientoIva = totalCostosIva*porcientoOrg;
                    var totalImporteCostosIva = costosTienenIva
                        ? distribucionIngredientes.ListaPremezclaDistribucionCosto.Where(iva => iva.Iva)
                            .Sum(sumIva => sumIva.Importe)
                        : 0;
                    var porcentajeImporteCostosIva = costosTienenIva ? totalImporteCostosIva * porcientoOrg : 0;
                    //Poliza de Iva de importe + Iva de costos

                    var importeIVA = costosTienenIva && tieneIva
                        ? (organizacionDistribucion.CostoTotal + porcentajeImporteCostosIva)
                        : (costosTienenIva && !tieneIva) ? porcentajeImporteCostosIva : (!costosTienenIva && tieneIva) ? organizacionDistribucion.CostoTotal : 0;

                    datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = fecha,
                                    Folio = numeroReferencia,
                                    Importe =
                                        string.Format("{0}",
                                                      (importeIva + porcientoIva).ToString("F2")),
                                    IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                                    Renglon = Convert.ToString(++renglon),
                                    ImporteIva = (importeIVA).ToString("F2"),
                                    Ref3 = ref3.ToString(),
                                    Cuenta = cuentaIva.CuentaSAP,
                                    ClaveImpuesto = ClaveImpuesto,
                                    CondicionImpuesto = CondicionImpuesto,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = cuentaIva.Descripcion,
                                    PesoOrigen =
                                        Math.Round(Convert.ToDecimal(organizacionDistribucion.CantidadSurtir), 2),
                                    Division = organizacion.Division,
                                    TipoDocumento = textoDocumento,
                                    ClaseDocumento = postFijoRef3,
                                    Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                                             tipoMovimiento,
                                                             numeroReferencia,
                                                             cantidad.ToString("F0"),
                                                             unidad, producto.Descripcion,
                                                             (organizacionDistribucion.CostoTotal).ToString("F2"),
                                                             postFijoRef3),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                    poliza = GeneraRegistroPoliza(datos);
                    polizaPremezcla.Add(poliza);
                    //Poliza de Costo de la distribucion por organizacion mas IVA pagada al proveedor
                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia,
                        FechaEntrada = fecha,
                        Folio = numeroReferencia,
                        Importe =
                            string.Format("{0}",
                                          ((importeIva + organizacionDistribucion.CostoTotal)*-1).ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(++renglon),
                        ImporteIva = String.Empty,
                        Ref3 = ref3.ToString(),
                        ClaveProveedor = distribucionIngredientes.Proveedor.CodigoSAP,
                        
                        ClaveImpuesto = ClaveImpuesto,
                        CondicionImpuesto = CondicionImpuesto,
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = distribucionIngredientes.Proveedor.CodigoSAP,
                        PesoOrigen =
                            Math.Round(Convert.ToDecimal(organizacionDistribucion.CantidadSurtir), 2),
                        Division = organizacion.Division,
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                                 tipoMovimiento,
                                                 numeroReferencia,
                                                 cantidad.ToString("F0"),
                                                 unidad, producto.Descripcion,
                                                 (organizacionDistribucion.CostoTotal).ToString("F2"),
                                                 postFijoRef3),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    poliza = GeneraRegistroPoliza(datos);
                    polizaPremezcla.Add(poliza);
                }

                IList<int> costosConRetencion =
                    distribucionIngredientes.ListaPremezclaDistribucionCosto.Where(x => x.Retencion)
                        .Select(x => x.Costo.CostoID)
                        .ToList();
                foreach (var costo in distribucionIngredientes.ListaPremezclaDistribucionCosto)
                {
                    if (costo.Retencion)
                    {
                        var retencionBL = new RetencionBL();
                        var retenciones = retencionBL.ObtenerRetencionesConCosto(costosConRetencion);
                        RetencionInfo retencion = null;
                        if (retenciones != null && retenciones.Any())
                        {
                            retencion =
                                retenciones.FirstOrDefault(
                                    costoRet => costoRet.CostoID == costo.Costo.CostoID);
                        }
                        if (retencion != null)
                        {
                            if (!costo.Iva)
                            {
                                renglon++;
                                datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia.ToString(),
                                    FechaEntrada = distribucionIngredientes.FechaEntrada,
                                    Folio = numeroReferencia,
                                    Division = organizacion.Division,
                                    ClaveProveedor = costo.Proveedor.CodigoSAP,
                                    Importe = string.Format("{0}", (costo.Importe * -1).ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = costo.Proveedor.Descripcion,
                                    PesoOrigen = organizacionDistribucion.CantidadSurtir,
                                    TipoDocumento = textoDocumento,
                                    ClaseDocumento = postFijoRef3,
                                    Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                        tipoMovimiento,
                                        numeroReferencia,
                                        organizacionDistribucion.CantidadExistente.ToString("N0"),
                                        unidad, costo.Costo.Descripcion,
                                        costoUnitario.ToString("N2"), postFijoRef3),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                                poliza = GeneraRegistroPoliza(datos);
                                polizaPremezcla.Add(poliza);
                            }
                            var parametrosRetencion = new StringBuilder();
                            parametrosRetencion.Append(String.Format("{0}{1}"
                                , retencion.IndicadorRetencion
                                , retencion.TipoRetencion));
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia.ToString(),
                                FechaEntrada = distribucionIngredientes.FechaEntrada,
                                Folio = numeroReferencia,
                                Division = organizacion.Division,
                                ClaveProveedor = costo.Proveedor.CodigoSAP,
                                Importe = string.Format("-{0}", "0"),
                                IndicadorImpuesto = parametrosRetencion.ToString(),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                CodigoRetencion = retencion.IndicadorImpuesto,
                                TipoRetencion = retencion.IndicadorRetencion,
                                ArchivoFolio = archivoFolio.ToString(),
                                DescripcionCosto = costo.Proveedor.Descripcion,
                                PesoOrigen = organizacionDistribucion.CantidadSurtir,
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                    tipoMovimiento,
                                    numeroReferencia,
                                    organizacionDistribucion.CantidadExistente.ToString("N0"),
                                    unidad, costo.Costo.Descripcion,
                                    costoUnitario.ToString("N2"), postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizaPremezcla.Add(poliza);
                        }
                    }
                        decimal porcientoOrg = (decimal) organizacionDistribucion.CantidadSurtir/
                                               (decimal) distribucionIngredientes.CantidadTotal;
                        var importePorciento = costo.Iva
                            ? (costo.Importe*((organizacion.Iva.TasaIva/100) + 1))*porcientoOrg
                            : costo.Importe*porcientoOrg;

                        var descripcion = costo.TieneCuenta ? costo.CuentaSAP.Descripcion : costo.Proveedor.Descripcion;
                        var cuenta = costo.TieneCuenta ? costo.CuentaSAP.CuentaSAP : costo.Proveedor.CodigoSAP;


                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = fecha,
                            Folio = numeroReferencia,
                            Importe =
                                string.Format("{0}",
                                    ((importePorciento)*-1).ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(++renglon),
                            ImporteIva = String.Empty,
                            Ref3 = ref3.ToString(),
                            Cuenta = String.Empty,
                            ClaveProveedor = String.Empty,
                            ClaveImpuesto = ClaveImpuesto,
                            CondicionImpuesto = CondicionImpuesto,
                            ArchivoFolio = archivoFolio,
                            DescripcionCosto = descripcion,
                            PesoOrigen =
                                Math.Round(Convert.ToDecimal(organizacionDistribucion.CantidadSurtir), 2),
                            Division = organizacion.Division,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                tipoMovimiento,
                                numeroReferencia,
                                cantidad.ToString("F0"),
                                unidad, descripcion,
                                (organizacionDistribucion.CostoTotal).ToString("F2"),
                                postFijoRef3),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        if (costo.TieneCuenta)
                        {
                            datos.Cuenta = cuenta;
                        }
                        else
                        {
                            datos.ClaveProveedor = cuenta;
                        }
                        poliza = GeneraRegistroPoliza(datos);
                        polizaPremezcla.Add(poliza);
                }
            }
            if (!tieneIva && !costosTienenIva)
            {
                decimal importe = organizaciones.Sum(imp => imp.CostoTotal) + totalIva;
                decimal peso = organizaciones.Sum(kg => kg.CantidadSurtir);
                if (peso == 0)
                {
                    peso = organizaciones.Sum(kg => kg.CantidadNueva);
                }
                bool esCuenta = false;
                var proveedor = distribucionIngredientes.Proveedor.CodigoSAP;
                if (!proveedor.StartsWith("0"))
                {
                    esCuenta = true;
                }
                //numeroReferencia = ObtenerNumeroReferencia;
                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = fecha,
                    Folio = numeroReferencia,
                    Importe =
                        string.Format("{0}",
                                      (distribucionIngredientes.CostoTotal * -1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(++renglon),
                    ImporteIva = "0",
                    Ref3 = ref3.ToString(),
                    ClaveProveedor = esCuenta ? String.Empty : distribucionIngredientes.Proveedor.CodigoSAP,
                    Cuenta = esCuenta ? distribucionIngredientes.Proveedor.CodigoSAP : String.Empty,
                    ArchivoFolio = archivoFolio,
                    DescripcionCosto = distribucionIngredientes.Proveedor.Descripcion,
                    PesoOrigen =
                        Math.Round(peso, 2),
                    Division = organizacion.Division,
                    TipoDocumento = textoDocumento,
                    ClaseDocumento = postFijoRef3,
                    Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                             tipoMovimiento,
                                             numeroReferencia,
                                             (peso).ToString("F0"),
                                             unidad, producto.Descripcion,
                                             (importe).ToString("F2"),
                                             postFijoRef3),
                    Sociedad = organizacion.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                poliza = GeneraRegistroPoliza(datos);
                polizaPremezcla.Add(poliza);
            }
            return polizaPremezcla;
        }

        #endregion METODOS PRIVADOS
    }
}
