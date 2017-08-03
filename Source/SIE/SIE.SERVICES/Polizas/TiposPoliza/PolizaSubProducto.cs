using System;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Collections.Generic;
using System.IO;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSubProducto : PolizaAbstract
    {
        #region VARIABLES

        #endregion VARIABLES

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var premezcla = datosPoliza as ContenedorEntradaMateriaPrimaInfo;
            IList<PolizaInfo> polizaPremezcla = ObtenerPoliza(premezcla);
            return polizaPremezcla;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(ContenedorEntradaMateriaPrimaInfo premezcla)
        {
            var polizaPremezcla = new List<PolizaInfo>();

            OrganizacionInfo organizacion =
                ObtenerOrganizacionIVA(premezcla.EntradaProducto.Organizacion.OrganizacionID);

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            IList<UnidadMedicionInfo> unidadesMedicion = ObtenerUnidadesMedicion();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.PolizaSubProducto.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA", TipoPoliza.PolizaSubProducto));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            IList<ClaseCostoProductoInfo> clasesCostoProducto =
                ObtenerCostosProducto(premezcla.EntradaProducto.AlmacenMovimiento.Almacen.AlmacenID);

            ProductoInfo producto;
            PremezclaDetalleInfo premezclaDetalle;
            DatosPolizaInfo datos;

            int folio = premezcla.EntradaProducto.Folio;
            DateTime fecha = premezcla.EntradaProducto.Fecha;

            var renglon = 0;
            int milisegundo = DateTime.Now.Millisecond;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(folio).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(milisegundo);
            ref3.Append(postFijoRef3);

            string archivoFolio = ObtenerArchivoFolio(fecha);
            string unidad;

            //string numeroReferencia = string.Format("{0}{1}", folio, ObtenerNumeroReferencia);
            string numeroReferencia = ObtenerNumeroReferenciaFolio(folio);

            ClaseCostoProductoInfo costoProducto;
            CuentaSAPInfo cuentaSap;
            PolizaInfo poliza;
            decimal importe = 0;
            decimal importeDetalle = 0;
            for (var index = 0; index < premezcla.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos.Count; index++)
            {
                premezclaDetalle = premezcla.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos[index];

                producto = premezclaDetalle.Producto;

                unidad =
                    unidadesMedicion.Where(clave => clave.UnidadID == producto.UnidadMedicion.UnidadID).Select(
                        uni => uni.ClaveUnidad).FirstOrDefault();

                costoProducto =
                    clasesCostoProducto.FirstOrDefault(clave => clave.ProductoID == producto.ProductoId);
                if (costoProducto == null)
                {
                    costoProducto = new ClaseCostoProductoInfo();
                }
                cuentaSap =
                    cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAPID == costoProducto.CuentaSAPID);
                if (cuentaSap == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO HAY CONFIGURACION PARA EL PRODUCTO",
                                                              producto.Descripcion));
                }

                importeDetalle = premezclaDetalle.Kilogramos*premezclaDetalle.Lote.PrecioPromedio;
                importe += importeDetalle;
                renglon++;
                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = fecha,
                                Folio = folio.ToString(),
                                Importe =
                                    string.Format("{0}",
                                                  (importeDetalle*-1).
                                                      ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSap.CuentaSAP,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = producto.Descripcion,
                                PesoOrigen = Math.Round(premezclaDetalle.Kilogramos, 2),
                                Division = organizacion.Division,
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                                         tipoMovimiento,
                                                         folio,
                                                         premezclaDetalle.Kilogramos.ToString("N2"),
                                                         unidad, producto.Descripcion,
                                                         (importeDetalle).ToString("F2"),
                                                         postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad)
                            };
                poliza = GeneraRegistroPoliza(datos);
                polizaPremezcla.Add(poliza);
            }
            producto = premezcla.EntradaProducto.Producto;

            var productoBL = new ProductoBL();
            producto = productoBL.ObtenerPorID(producto);

            unidad =
                    unidadesMedicion.Where(clave => clave.UnidadID == producto.UnidadMedicion.UnidadID).Select(
                        uni => uni.ClaveUnidad).FirstOrDefault();
            costoProducto =
                     clasesCostoProducto.FirstOrDefault(clave => clave.ProductoID == producto.ProductoId);
            if (costoProducto == null)
            {
                costoProducto = new ClaseCostoProductoInfo();
            }
            cuentaSap = cuentasSap.FirstOrDefault(clave => clave.CuentaSAPID == costoProducto.CuentaSAPID);
            if (cuentaSap == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "NO HAY CONFIGURACION PARA EL PRODUCTO",
                                                          producto.Descripcion));
            }
            decimal peso = premezcla.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos.Sum(
                pre => Math.Abs(pre.Kilogramos));
            renglon++;
            datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = fecha,
                            Folio = folio.ToString(),
                            Importe =
                                string.Format("{0}",
                                              (importe).ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = cuentaSap.CuentaSAP,
                            ArchivoFolio = archivoFolio,
                            DescripcionCosto = premezcla.EntradaProducto.Producto.Descripcion,
                            PesoOrigen =
                                Math.Round(peso, 2),
                            Division = organizacion.Division,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                                     tipoMovimiento,
                                                     folio,
                                                     (peso).ToString("N2"),
                                                     unidad, premezcla.EntradaProducto.Producto.Descripcion,
                                                     (importe).ToString("F2"),
                                                     postFijoRef3),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad)
                        };
            poliza = GeneraRegistroPoliza(datos);
            polizaPremezcla.Add(poliza);

            return polizaPremezcla;
        }

        #endregion METODOS PRIVADOS
    }
}
