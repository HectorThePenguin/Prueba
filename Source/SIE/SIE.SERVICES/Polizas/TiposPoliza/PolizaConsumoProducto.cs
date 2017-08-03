using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using System.Linq;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaConsumoProducto : PolizaAbstract
    {
        #region VARIABLES PRIVADAS

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var contenedor = datosPoliza as List<ContenedorAlmacenMovimientoCierreDia>;
            IList<PolizaInfo> poliza = ObtenerPoliza(contenedor);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(List<ContenedorAlmacenMovimientoCierreDia> contenedor)
        {
            var polizasConsumo = new List<PolizaInfo>();
            IList <CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            int organizacionID = contenedor[0].Almacen.Organizacion.OrganizacionID;
            int almacenID = contenedor[0].Almacen.AlmacenID;
            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);
            int tipoOrigenID = organizacion.TipoOrganizacion.TipoProceso.TipoProcesoID;

            IList<CostoInfo> costos = ObtenerCostos();

            IList<CuentaAlmacenSubFamiliaInfo> cuentasSubFamilia;

            var movimientos = contenedor.GroupBy(prod => new { prod.Producto.ProductoId, prod.Producto.Descripcion })
                                        .Select(mov => new
                                        {
                                            Importe = mov.Sum(det => det.AlmacenMovimientoDetalle.Importe),
                                            ProductoID = mov.Key.ProductoId,
                                            Producto = mov.Key.Descripcion,
                                            FechaMovimiento = mov.Select(fech => fech.AlmacenMovimiento.FechaMovimiento).FirstOrDefault(),
                                            SubFamilia = mov.Select(sf => sf.Producto.SubFamilia.Descripcion).FirstOrDefault(),
                                            SubFamiliaID = mov.Select(sf => sf.Producto.SubFamilia.SubFamiliaID).FirstOrDefault(),
                                            Familia = mov.Select(fam => fam.Producto.SubFamilia.Familia.Descripcion).FirstOrDefault(),
                                            TipoTratamientoID = mov.Select(trat => trat.AlmacenMovimientoDetalle.Tratamiento.TipoTratamientoInfo.TipoTratamientoID).FirstOrDefault(),
                                            TipoTratamiento = mov.Select(trat => trat.AlmacenMovimientoDetalle.Tratamiento.TipoTratamientoInfo.Descripcion).FirstOrDefault(),
                                            Almacen = mov.Select(alm => alm.Almacen.Descripcion).FirstOrDefault(),
                                            AlmacenID = mov.Select(alm => alm.Almacen.AlmacenID).FirstOrDefault(),
                                            FolioAlmacen = mov.Select(folio => folio.FolioAlmacen).FirstOrDefault()
                                        }).ToList();
            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.ConsumoProducto.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA", TipoPoliza.ConsumoProducto));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            const int TIPO_COSTO = 3;
            var linea = 1;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(
                string.Format("{0}{1}{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year).PadLeft(
                    10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(DateTime.Now.Millisecond);
            ref3.Append(postFijoRef3);

            ParametroOrganizacionInfo parametroOrganizacion =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOENG.ToString());
            string numeroDocumento = ObtenerNumeroReferencia;

            var archivoFolio = string.Empty;
            var filtros = new FiltroCierreDiaInventarioInfo
            {
                AlmacenID = almacenID,
                TipoMovimientoID = TipoMovimiento.DiferenciasDeInventario.GetHashCode()
            };
            var almacenBL = new AlmacenBL();
            int folioAlmacen = almacenBL.ObtenerFolioAlmacenConsulta(filtros);
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            almacenMovimientoBL.ActualizarFolioAlmacen(almacenID);
            if (movimientos.Any())
            {
                var folioRef = new StringBuilder();
                folioRef.Append(almacenID.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
                folioRef.Append(folioAlmacen.ToString(CultureInfo.InvariantCulture).PadLeft(7, '0'));
                DateTime fecha = movimientos[0].FechaMovimiento;
                archivoFolio = ObtenerArchivoFolio(fecha);
                numeroDocumento = folioRef.ToString();

                for (var indexMovimientos = 0; indexMovimientos < movimientos.Count; indexMovimientos++)
                {
                    var mov = movimientos[indexMovimientos];
                    string descripcionMovimiento = ObtenerDescripcionMovimiento(mov.TipoTratamientoID);
                    CostoInfo costo =
                        costos.FirstOrDefault(
                            tipo =>
                            tipo.TipoCosto.TipoCostoID == TIPO_COSTO &&
                            tipo.Descripcion.IndexOf(descripcionMovimiento, StringComparison.InvariantCultureIgnoreCase) >=
                            0);
                    if (costo == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA",
                                                                  descripcionMovimiento));
                    }
                    ClaveContableInfo claveContable = ObtenerCuentaInventario(costo, organizacionID, tipoOrigenID);
                    if (claveContable == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA EL COSTO",
                                                                  costo.Descripcion));
                    }
                    var datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = mov.FechaMovimiento,
                                        Importe = string.Format("{0}", mov.Importe.ToString("F2")),
                                        Renglon = Convert.ToString(linea++),
                                        Division = organizacion.Division,
                                        ImporteIva = "0",
                                        ClaseDocumento = postFijoRef3,
                                        Ref3 = ref3.ToString(),
                                        Cuenta = claveContable.Valor,
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = claveContable.Descripcion,
                                        CentroCosto =
                                            claveContable.Valor.StartsWith(PrefijoCuentaCentroCosto)
                                                ? parametroOrganizacion.Valor
                                                : string.Empty,
                                        PesoOrigen = 0,
                                        TipoDocumento = textoDocumento,
                                        Folio = folioAlmacen.ToString(),
                                        Concepto = String.Format("{0}-{1} {2}",
                                                                 tipoMovimiento,
                                                                 folioAlmacen,
                                                                 mov.Producto),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                    PolizaInfo polizaConsumo = GeneraRegistroPoliza(datos);
                    polizasConsumo.Add(polizaConsumo);
                }
            }

            movimientos = contenedor.GroupBy(prod => new { prod.Producto.SubFamilia.SubFamiliaID }).Select(mov => new
            {
                Importe = mov.Sum(det => det.AlmacenMovimientoDetalle.Importe),
                ProductoID = 0,
                Producto = string.Empty,
                FechaMovimiento = mov.Select(fech => fech.AlmacenMovimiento.FechaMovimiento).FirstOrDefault(),
                SubFamilia = mov.Select(sf => sf.Producto.SubFamilia.Descripcion).FirstOrDefault(),
                mov.Key.SubFamiliaID,
                Familia = mov.Select(fam => fam.Producto.SubFamilia.Familia.Descripcion).FirstOrDefault(),
                TipoTratamientoID = mov.Select(trat => trat.AlmacenMovimientoDetalle.Tratamiento.TipoTratamientoInfo.TipoTratamientoID).FirstOrDefault(),
                TipoTratamiento = mov.Select(trat => trat.AlmacenMovimientoDetalle.Tratamiento.TipoTratamientoInfo.Descripcion).FirstOrDefault(),
                Almacen = mov.Select(alm => alm.Almacen.Descripcion).FirstOrDefault(),
                AlmacenID = mov.Select(alm => alm.Almacen.AlmacenID).FirstOrDefault(),
                FolioAlmacen = mov.Select(folio => folio.FolioAlmacen).FirstOrDefault()
            }).ToList();
            if (movimientos != null && movimientos.Any())
            {
                CuentaSAPInfo cuentaSap;
                CuentaAlmacenSubFamiliaInfo cuentaSubFamilia;

                for (var indexMovimientos = 0; indexMovimientos < movimientos.Count; indexMovimientos++)
                {
                    var mov = movimientos[indexMovimientos];
                    string descripcionMovimiento = ObtenerDescripcionMovimiento(mov.TipoTratamientoID);
                    CostoInfo costo =
                        costos.FirstOrDefault(
                            tipo =>
                            tipo.TipoCosto.TipoCostoID == TIPO_COSTO &&
                            tipo.Descripcion.IndexOf(descripcionMovimiento, StringComparison.InvariantCultureIgnoreCase) >=
                            0);
                    if (costo == null)
                    {
                        costo = new CostoInfo();
                    }
                    ClaveContableInfo claveContable = ObtenerCuentaInventario(costo, organizacionID, tipoOrigenID);
                    if (claveContable == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA EL COSTO",
                                                                  costo.Descripcion));
                    }
                    cuentasSubFamilia = ObtenerCostosSubFamilia(mov.AlmacenID);
                    if (cuentasSubFamilia == null)
                    {
                        cuentasSubFamilia = new List<CuentaAlmacenSubFamiliaInfo>();
                    }
                    cuentaSubFamilia = cuentasSubFamilia.FirstOrDefault(clave => clave.SubFamiliaID == mov.SubFamiliaID);
                    if (cuentaSubFamilia == null)
                    {
                        cuentaSubFamilia = new CuentaAlmacenSubFamiliaInfo();
                    }
                    cuentaSap = cuentasSap.FirstOrDefault(clave => clave.CuentaSAPID == cuentaSubFamilia.CuentaSAPID);
                    if (cuentaSap == null)
                    {
                        throw new ExcepcionServicio(string.Format("CUENTA NO CONFIGURADA PARA EL PRODUCTO {0}",
                                                                  mov.Producto));
                    }
                    var datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = mov.FechaMovimiento,
                                        Importe = string.Format("{0}", (mov.Importe*-1).ToString("F2")),
                                        Renglon = Convert.ToString(linea++),
                                        ImporteIva = "0",
                                        ClaseDocumento = postFijoRef3,
                                        Division = organizacion.Division,
                                        Ref3 = ref3.ToString(),
                                        Cuenta = cuentaSap.CuentaSAP,
                                        ArchivoFolio = archivoFolio,
                                        CentroCosto =
                                            cuentaSap.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                                                ? parametroOrganizacion.Valor
                                                : string.Empty,
                                        DescripcionCosto = cuentaSap.Descripcion,
                                        PesoOrigen = 0,
                                        TipoDocumento = textoDocumento,
                                        ComplementoRef1 = string.Empty,
                                        Folio = folioAlmacen.ToString(),
                                        Concepto = String.Format("{0}-{1} {2}",
                                                                 tipoMovimiento,
                                                                 folioAlmacen,
                                                                 mov.Producto),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                    PolizaInfo polizaConsumo = GeneraRegistroPoliza(datos);
                    polizasConsumo.Add(polizaConsumo);
                }
            }
            return polizasConsumo;
        }

        private string ObtenerDescripcionMovimiento(int tipoTratamientoID)
        {
            var tipoTratamiento = (TipoTratamiento)tipoTratamientoID;
            string descripcionMovimiento = string.Empty;
            switch (tipoTratamiento)
            {
                case TipoTratamiento.Corte:
                case TipoTratamiento.EnfermeriaAlCorte:
                    descripcionMovimiento = "Implante";
                    break;
                case TipoTratamiento.Reimplante:
                    descripcionMovimiento = "Reimplante";
                    break;
                case TipoTratamiento.Enfermeria:
                    descripcionMovimiento = "Enfermer";
                    break;
            }
            return descripcionMovimiento;
        }

        #endregion METODOS PRIVADOS
    }
}
