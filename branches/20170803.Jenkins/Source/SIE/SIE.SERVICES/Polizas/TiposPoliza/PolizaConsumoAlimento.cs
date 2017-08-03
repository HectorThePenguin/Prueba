using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaConsumoAlimento : PolizaAbstract
    {
        #region CONSTRUCTORES
        
        #endregion CONSTRUCTORES

        #region METODOS SOBREESCRITOS
        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var contenedor = datosPoliza as PolizaConsumoAlimentoModel;
            IList<PolizaInfo> poliza = ObtenerPoliza(contenedor);
            return poliza;
        }
        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(PolizaConsumoAlimentoModel contenedor)
        {
            var polizasConsumo = new List<PolizaInfo>();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.ConsumoAlimento.GetHashCode());            
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA", TipoPoliza.ConsumoAlimento));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

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

            string numeroDocumento = ObtenerNumeroReferenciaFolio(contenedor.AlmacenMovimiento.FolioMovimiento);

            DateTime fecha = contenedor.Reparto.Fecha;
            string archivoFolio = ObtenerArchivoFolio(fecha);
            IList<CostoInfo> costos = ObtenerCostos();
            IList<FormulaInfo> formulas = ObtenerFormulas();

            OrganizacionInfo organizacion;
            List<RepartoDetalleInfo> repatoAgrupado = null;
            List<ProduccionFormulaDetalleInfo> produccionAgrupado;

            CostoInfo costo =
                        costos.FirstOrDefault(
                            tipo => "002".Equals(tipo.ClaveContable));
            if (costo == null)
            {
                costo = new CostoInfo();
            }
            PolizaInfo polizaConsumo;

            IList<CuentaSAPInfo> cuentasSAP = ObtenerCuentasSAP();
            if (cuentasSAP == null)
            {
                cuentasSAP = new List<CuentaSAPInfo>();
            }
            CuentaSAPInfo cuentaSap;

            if (contenedor.Reparto != null && contenedor.Reparto.DetalleReparto != null
                && contenedor.Reparto.DetalleReparto.Any())
            {
                repatoAgrupado = contenedor.Reparto.DetalleReparto
                    .GroupBy(formu => formu.FormulaIDServida)
                    .Select(agrupado => new RepartoDetalleInfo
                                            {
                                                OrganizacionID =
                                                    agrupado.Select(org => org.OrganizacionID).FirstOrDefault(),
                                                Importe = agrupado.Sum(imp => imp.Importe),
                                                FormulaIDServida =
                                                    agrupado.Select(form => form.FormulaIDServida).FirstOrDefault()
                                            }).Where(imp => imp.Importe > 0).ToList();
                for (var indexReparto = 0; indexReparto < repatoAgrupado.Count; indexReparto++)
                {
                    RepartoDetalleInfo detalle = repatoAgrupado[indexReparto];

                    organizacion = ObtenerOrganizacionIVA(detalle.OrganizacionID);                    
                    cuentaSap = cuentasSAP.FirstOrDefault(clave => clave.CuentaSAP.Equals("1151401002"));
                    if (cuentaSap == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "CUENTA NO CONFIGURADA PARA",
                                                                  costo.Descripcion));
                    }
                    var datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = fecha,
                        Folio = numeroDocumento,
                        Importe = string.Format("{0}", detalle.Importe.ToString("F2")),
                        Renglon = Convert.ToString(linea),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        ClaseDocumento = postFijoRef3,
                        Cuenta = cuentaSap.CuentaSAP,
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = cuentaSap.Descripcion,
                        PesoOrigen = 0,
                        Division = organizacion.Division,
                        TipoDocumento = textoDocumento,
                        Concepto = String.Format("{0}-{1} {2}",
                                                 tipoMovimiento,
                                                 numeroDocumento,
                                                 cuentaSap.CuentaSAP),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    linea++;
                    polizaConsumo = GeneraRegistroPoliza(datos);
                    polizasConsumo.Add(polizaConsumo);
                }
            }
            int almacenID = 0;
            if (contenedor.ProduccionFormula != null && contenedor.ProduccionFormula.ProduccionFormulaDetalle != null
                && contenedor.ProduccionFormula.ProduccionFormulaDetalle.Any())
            {
                produccionAgrupado =
                    contenedor.ProduccionFormula.ProduccionFormulaDetalle
                        .GroupBy(formu => formu.ProduccionFormulaId)
                        .Select(agrupado => new ProduccionFormulaDetalleInfo
                                                {
                                                    ProduccionFormulaId = agrupado.Key,
                                                    OrganizacionID =
                                                        agrupado.Select(org => org.OrganizacionID).FirstOrDefault(),
                                                    AlmacenID = agrupado.Select(alm => alm.AlmacenID).FirstOrDefault()
                                                }).ToList();                
                ClaseCostoProductoInfo producto;
                IList<ClaseCostoProductoInfo> almacenesProductosCuentas;
                FormulaInfo formula;
                for (var indexFormula = 0; indexFormula < produccionAgrupado.Count; indexFormula++)
                {
                    ProduccionFormulaDetalleInfo detalle = produccionAgrupado[indexFormula];

                    almacenID = detalle.AlmacenID;
                    almacenesProductosCuentas = ObtenerCostosProducto(almacenID);
                    organizacion = ObtenerOrganizacionIVA(detalle.OrganizacionID);
                    formula = formulas.FirstOrDefault(clave => clave.FormulaId == detalle.ProduccionFormulaId);
                    if (formula == null)
                    {
                        formula =
                            formulas.FirstOrDefault(clave => clave.Producto.ProductoId == detalle.ProduccionFormulaId);
                        if (formula == null)
                        {
                            formula = new FormulaInfo
                                          {
                                              Producto = new ProductoInfo()
                                          };
                        }
                    }
                    decimal importe = 0;
                    if (repatoAgrupado != null)
                    {
                        importe =
                            repatoAgrupado.Where(formu => formu.FormulaIDServida == formula.FormulaId).Sum(
                                imp => imp.Importe);
                        if (importe == 0)
                        {
                            importe =
                                repatoAgrupado.Where(formu => formu.FormulaIDServida == formula.Producto.ProductoId).Sum
                                    (imp => imp.Importe);
                        }
                    }
                    if (importe == 0)
                    {
                        continue;
                    }
                    producto =
                        almacenesProductosCuentas.FirstOrDefault(p => p.ProductoID == formula.Producto.ProductoId);
                    if (producto == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "CUENTA NO CONFIGURADA PARA EL PRODUCTO",
                                                                  formula.Producto.Descripcion));
                    }
                    cuentaSap = cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAPID == producto.CuentaSAPID);
                    if (cuentaSap == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "CUENTA NO CONFIGURADA PARA EL PRODUCTO",
                                                                  formula.Producto.Descripcion));
                    }
                    var datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = fecha,
                                        Importe = string.Format("{0}", (importe*-1).ToString("F2")),
                                        Renglon = Convert.ToString(linea),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        Cuenta = cuentaSap.CuentaSAP,
                                        ClaseDocumento = postFijoRef3,
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = cuentaSap.Descripcion,
                                        PesoOrigen = 0,
                                        Division = organizacion.Division,
                                        TipoDocumento = textoDocumento,
                                        Folio = numeroDocumento,
                                        Concepto = String.Format("{0}-{1} {2}",
                                                 tipoMovimiento,
                                                 numeroDocumento,
                                                 cuentaSap.CuentaSAP),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                    linea++;
                    polizaConsumo = GeneraRegistroPoliza(datos);
                    polizasConsumo.Add(polizaConsumo);
                }
            }
            return polizasConsumo;
        }

        #endregion METODOS PRIVADOS
    }
}
