using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaContrato : PolizaAbstract
    {
        #region VARIABLES

        #endregion VARIABLES

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var datosContrato = datosPoliza as PolizaContratoModel;
            IList<PolizaInfo> polizas = ObtenerPoliza(datosContrato);
            return polizas;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(PolizaContratoModel datosContrato)
        {
            var polizasContrato = new List<PolizaInfo>();

            int folioPedido = datosContrato.Contrato.Folio;
            int organizacionID = datosContrato.Contrato.Organizacion.OrganizacionID;
            DateTime fechaPedido = datosContrato.Contrato.Fecha;

            int miliSegunda = DateTime.Now.Millisecond;
            string archivoFolio = ObtenerArchivoFolio(fechaPedido);

            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);

            var tipoContrato = (TipoContratoEnum)datosContrato.Contrato.TipoContrato.TipoContratoId;
            var tipoPolizaEnum = TipoPoliza.PolizaContratoTerceros;
            switch (tipoContrato)
            {
                case TipoContratoEnum.EnTransito:
                    tipoPolizaEnum = TipoPoliza.PolizaContratoTransito;
                    break;
            }
            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == tipoPolizaEnum.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.PolizaContratoTerceros));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(folioPedido).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(miliSegunda);
            ref3.Append(postFijoRef3);

            //string numeroDocumento = ObtenerNumeroReferencia;
            string numeroDocumento = ObtenerNumeroReferenciaFolio(datosContrato.AlmacenMovimiento.FolioMovimiento);

            var proveedorAlmacenBL = new ProveedorAlmacenBL();
            ProveedorAlmacenInfo proveedorAlmacen =
                proveedorAlmacenBL.ObtenerPorProveedorId(datosContrato.Contrato.Proveedor);
            if (proveedorAlmacen == null)
            {
                throw new ExcepcionServicio(string.Format("EL PROVEEDOR {0} NO TIENE ALMACEN ASIGNADO",
                                                          datosContrato.Contrato.Proveedor.Descripcion));
            }
            decimal importe = datosContrato.Contrato.Precio*datosContrato.Contrato.Cantidad;
            var renglon = 1;
            var datos = new DatosPolizaInfo
            {
                NumeroReferencia = numeroDocumento,
                FechaEntrada = datosContrato.Contrato.Fecha,
                Folio = datosContrato.Contrato.Folio.ToString(),
                Importe =
                    string.Format("{0}", importe.ToString("F2")),
                Renglon = Convert.ToString(renglon),
                ImporteIva = "0",
                Ref3 = ref3.ToString(),
                Cuenta = datosContrato.Contrato.Cuenta.CuentaSAP,
                ArchivoFolio = archivoFolio,
                DescripcionCosto = datosContrato.Contrato.Cuenta.Descripcion,
                PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                Division = organizacion.Division,
                TipoDocumento = textoDocumento,
                ClaseDocumento = postFijoRef3,
                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                     tipoMovimiento,
                                     datosContrato.Contrato.Folio,
                                     datosContrato.Contrato.Cantidad.ToString("N0"),
                                     "KGS", datosContrato.Contrato.Producto.Descripcion,
                                     importe.ToString("N2"), postFijoRef3),
                Sociedad = organizacion.Sociedad,
                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
            };
            PolizaInfo poliza = GeneraRegistroPoliza(datos);
            polizasContrato.Add(poliza);
            
            renglon++;
            datos = new DatosPolizaInfo
            {
                NumeroReferencia = numeroDocumento,
                FechaEntrada = datosContrato.Contrato.Fecha,
                Folio = datosContrato.Contrato.Folio.ToString(),
                ClaveProveedor = datosContrato.Contrato.Proveedor.CodigoSAP,
                Importe =
                    string.Format("{0}", (importe*-1).ToString("F2")),
                Renglon = Convert.ToString(renglon),
                Division = organizacion.Division,
                ImporteIva = "0",
                Ref3 = ref3.ToString(),
                ArchivoFolio = archivoFolio,
                DescripcionCosto = datosContrato.Contrato.Proveedor.Descripcion,
                PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                TipoDocumento = textoDocumento,
                ClaseDocumento = postFijoRef3,
                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6}",
                                     tipoMovimiento,
                                     datosContrato.Contrato.Folio,
                                     datosContrato.Contrato.Cantidad.ToString("N0"),
                                     "KGS", datosContrato.Contrato.Producto.Descripcion,
                                     importe.ToString("N2"), postFijoRef3),
                Sociedad = organizacion.Sociedad,
                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
            };
            poliza = GeneraRegistroPoliza(datos);
            polizasContrato.Add(poliza);

            return polizasContrato;
        }

        #endregion METODOS PRIVADOS
    }
}
