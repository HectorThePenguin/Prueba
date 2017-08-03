using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.ServicioPolizasLogin;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using SIE.Services.ServicioPolizasCancelacion;

namespace SIE.Services.Polizas
{
    public abstract class PolizaAbstract
    {
        #region PROPIEDADES

        /// <summary>
        /// Obtiene la clave del impuesto
        /// </summary>
        protected string ClaveImpuesto
        {
            get { return "VST"; }
        }

        /// <summary>
        /// Obtiene la condicion del impuesto
        /// </summary>
        protected string CondicionImpuesto
        {
            get { return "MWVS"; }
        }

        /// <summary>
        /// Obtiene el prefijo del centro de costo
        /// </summary>
        protected string PrefijoCuentaCentroCosto
        {
            get { return "500"; }
        }

        /// <summary>
        /// Obtiene el prefijo del centro de costo
        /// </summary>
        protected string PrefijoCuentaCentroGasto
        {
            get { return "6"; }
        }

        /// <summary>
        /// Obtiene el prefijo del centro de beneficio
        /// </summary>
        protected string PrefijoCuentaCentroBeneficio
        {
            get { return "400"; }
        }

        /// <summary>
        /// Obtiene el post fijo de la la subfamilia granos
        /// </summary>
        protected string PostFijoSubFamiliaGranos
        {
            get { return "003"; }
        }

        /// <summary>
        /// Obtiene el post fijo de la la subfamilia no granos
        /// </summary>
        protected string PostFijoSubFamiliaNoGranos
        {
            get { return "002"; }
        }

        /// <summary>
        /// Prefijo de la cuenta de almacen
        /// </summary>
        protected string PrefijoCuentaAlmacen
        {
            get { return "1126"; }
        }

        /// <summary>
        /// Indica si es cancelacion de Poliza
        /// </summary>
        public bool Cancelacion { get; set; }

        /// <summary>
        /// Modelo de poliza
        /// </summary>
        internal PolizaModel PolizaModel { get; set; }

        /// <summary>
        /// Obtiene los tipos de poliza
        /// </summary>
        protected static IList<TipoPolizaInfo> TiposPoliza { get; set; }

        /// <summary>
        /// Obtiene la cuenta SAP a la que
        /// pertenece la piel
        /// </summary>
        protected string ObtenerCuentaPiel
        {
            get { return "4001001003"; }
        }

        /// <summary>
        /// Obtiene la cuenta SAP a la que
        /// pertenece al canal
        /// </summary>
        protected string ObtenerCuentaCanal
        {
            get { return "4001001001"; }
        }

        /// <summary>
        /// Obtiene la cuenta SAP a la que
        /// pertenece a la viscera
        /// </summary>
        protected string ObtenerCuentaViscera
        {
            get { return "4001001004"; }
        }

        /// <summary>
        /// Obtiene la cuenta para la generacion
        /// de la poliza de la 300
        /// </summary>
        protected string ObtenerCuentaPoliza300
        {
            //get { return "2005100005"; }
            get { return "2104030002"; }
        }

        /// <summary>
        /// Obtiene el numero de referencia
        /// </summary>
        protected string ObtenerNumeroReferencia
        {
            get
            {
                return string.Format("{0}{1}{2}", DateTime.Now.Hour, DateTime.Now.Minute,
                                     DateTime.Now.Millisecond);
            }
        }

        /// <summary>
        /// Prefijo con el cual empezara 
        /// el segmento
        /// </summary>
        protected string PrefijoSegmento
        {
            get
            {
                return "S";
            }
        }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        protected PolizaAbstract()
        {
            ObtenerTiposPoliza();
        }

        #endregion CONSTRUCTORES

        #region METODOS PUBLICOS

        #endregion METODOS PUBLICOS

        #region METODOS PROTEGIDOS

        /// <summary>
        /// Generar una entidad tipo Poliza Info para
        /// la generacion del XML de la poliza
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        protected PolizaInfo GeneraRegistroPoliza(DatosPolizaInfo datos)
        {
            if (string.IsNullOrWhiteSpace(datos.Ref3))
            {
                throw new ExcepcionServicio("REF3 SE ENCUENTRA VACIO");
            }
            if (string.IsNullOrWhiteSpace(datos.Sociedad))
            {
                throw new ExcepcionServicio("SOCIEDAD NO CONFIGURADA");
            }
            if (string.IsNullOrWhiteSpace(datos.Division))
            {
                throw new ExcepcionServicio("DIVISION NO CONFIGURADA");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(datos.FechaEntrada)))
            {
                throw new ExcepcionServicio("FECHA DOCUMENTO VACIA");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(datos.ClaseDocumento)))
            {
                throw new ExcepcionServicio("CLASE DOCUMENTO VACIO");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(datos.Importe)))
            {
                throw new ExcepcionServicio("IMPORTE VACIO");
            }

            var polizaEntradaGanado = new PolizaInfo
            {
                NumeroReferencia = datos.NumeroReferencia,
                FechaDocumento = datos.FechaEntrada.ToString("yyyyMMdd"),
                FechaContabilidad = datos.FechaEntrada.ToString("yyyyMMdd"),
                ClaseDocumento = datos.ClaseDocumento,
                Cliente = datos.Cliente,
                Sociedad = datos.Sociedad,
                Moneda = "MXN",
                CondicionImpuesto = datos.CondicionImpuesto,
                TextoDocumento = datos.TipoDocumento,
                Mes = Convert.ToString(datos.FechaEntrada.Month),
                Cuenta = datos.Cuenta,
                Proveedor = datos.ClaveProveedor,
                Importe = datos.Importe,
                IndicaImp = datos.IndicadorImpuesto,
                CentroCosto = datos.CentroCosto,
                CentroBeneficio = datos.CentroBeneficio,
                TextoAsignado = datos.Folio,
                Concepto = datos.Concepto,
                Division = datos.Division,
                BusAct = "RFBU",
                Periodo = Convert.ToString(datos.FechaEntrada.Year),
                NumeroLinea = datos.Renglon,
                Referencia1 = string.Format("{0} {1}", datos.Cabezas, datos.ComplementoRef1),
                Referencia3 = datos.Ref3,
                FechaImpuesto = datos.FechaEntrada.ToString("yyyyMMdd"),
                TipoRetencion = datos.TipoRetencion,
                CodigoRetencion = datos.CodigoRetencion,
                ImpuestoIva = datos.ImporteIva,
                ClaveImpuesto = datos.ClaveImpuesto,
                ArchivoFolio =
                    String.Format("{0}{1}", "P01", datos.ArchivoFolio),
                Descripcion = datos.DescripcionCosto,
                DescripcionProducto = datos.DescripcionProducto,
                ArchivoEnviadoServidor = datos.ArchivoEnviadoServidor,
                Segmento = datos.Segmento
            };
            return polizaEntradaGanado;
        }

        /// <summary>
        /// Generar una entidad tipo Poliza Info para
        /// la generacion del XML de la poliza
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        protected DT_POLIZA_PERIFERICO_ITEMDatos GeneraRegistroPolizaServicio(DatosPolizaInfo datos)
        {
            if (string.IsNullOrWhiteSpace(datos.Ref3))
            {
                throw new ExcepcionServicio("REF3 SE ENCUENTRA VACIO");
            }
            if (string.IsNullOrWhiteSpace(datos.Sociedad))
            {
                throw new ExcepcionServicio("SOCIEDAD NO CONFIGURADA");
            }
            if (string.IsNullOrWhiteSpace(datos.Division))
            {
                throw new ExcepcionServicio("DIVISION NO CONFIGURADA");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(datos.FechaEntrada)))
            {
                throw new ExcepcionServicio("FECHA DOCUMENTO VACIA");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(datos.ClaseDocumento)))
            {
                throw new ExcepcionServicio("CLASE DOCUMENTO VACIO");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(datos.Importe)))
            {
                throw new ExcepcionServicio("IMPORTE VACIO");
            }

            var polizaEntradaGanado = new DT_POLIZA_PERIFERICO_ITEMDatos
            {
                noref = datos.NumeroReferencia,
                fecha_doc = datos.FechaEntrada.ToString("yyyyMMdd"),
                fecha_cont = datos.FechaEntrada.ToString("yyyyMMdd"),
                clase_doc = datos.ClaseDocumento,
                cliente = datos.Cliente,
                sociedad = datos.Sociedad,
                moneda = "MXN",
                cond_imto = datos.CondicionImpuesto,
                texto_doc = datos.TipoDocumento,
                mes = Convert.ToString(datos.FechaEntrada.Month),
                cuenta = datos.Cuenta,
                proveedor = datos.ClaveProveedor,
                importe = datos.Importe,
                indica_imp = datos.IndicadorImpuesto,
                centro_cto = datos.CentroCosto,
                centro_ben = datos.CentroBeneficio,
                texto_asig = datos.Folio,
                concepto = datos.Concepto,
                division = datos.Division,
                bus_act = "RFBU",
                periodo = Convert.ToString(datos.FechaEntrada.Year),
                nolinea = datos.Renglon,
                ref1 = string.Format("{0} {1}", datos.Cabezas, datos.ComplementoRef1),
                ref2 = string.Empty,
                ref3 = datos.Ref3,
                fecha_imto = datos.FechaEntrada.ToString("yyyyMMdd"),
                tipo_ret = datos.TipoRetencion,
                cod_ret = datos.CodigoRetencion,
                imp_iva = datos.ImporteIva,
                clave_imto = datos.ClaveImpuesto,
                archifolio =
                    String.Format("{0}{1}", "P01", datos.ArchivoFolio),
                area_func = string.Empty,
                clase_movt = string.Empty,
                imp_ret = "0",
                indica_cme = string.Empty,
                orden_int = string.Empty,
                segmento = datos.Segmento,
                tipocambio = string.Empty,
            };
            return polizaEntradaGanado;
        }

        /// <summary>
        /// Obtiene la cuenta de Inventario
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="tipoOrigenId"></param>
        /// <param name="costo"> </param>
        /// <returns></returns>
        protected ClaveContableInfo ObtenerCuentaInventario(CostoInfo costo, int organizacionId, int tipoOrigenId)
        {
            const int GANADO_INTENSIVO = 8;
            const int GANADO_MAQUILA = 9;

            var claveCuenta = ClaveCuenta.CuentaInventarioGanadera;
            switch (tipoOrigenId)
            {
                case GANADO_INTENSIVO:
                    claveCuenta = ClaveCuenta.CuentaInventarioIntensivo;
                    break;
                case GANADO_MAQUILA:
                    claveCuenta = ClaveCuenta.CuentaInventarioMaquila;
                    break;
            }
            var interfaceBl = new InterfaceSalidaBL();
            var claveContableInfo = interfaceBl.ObtenerCuentaInventario(costo, organizacionId, claveCuenta);

            return claveContableInfo;
        }

        /// <summary>
        /// Obtiene la cuenta de Inventario
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="costo"> </param>
        /// <param name="tipoPoliza"> </param>
        /// <returns></returns>
        protected ClaveContableInfo ObtenerCuentaInventario(CostoInfo costo, int organizacionId, TipoPoliza tipoPoliza)
        {
            var claveCuenta = ClaveCuenta.CuentaCostoInventario;
            switch (tipoPoliza)
            {
                case TipoPoliza.SalidaMuerte:
                case TipoPoliza.ConsumoAlimento:
                case TipoPoliza.PolizaSacrificioTraspasoGanado:
                case TipoPoliza.SalidaMuerteEnTransito:
                    claveCuenta = ClaveCuenta.CuentaCostoInventario;
                    break;
                case TipoPoliza.SalidaVenta:
                case TipoPoliza.SalidaVentaEnTransito:
                    claveCuenta = ClaveCuenta.CuentaBeneficioInventario;
                    break;
                case TipoPoliza.SalidaVentaProducto:
                    claveCuenta = ClaveCuenta.CuentaBeneficioMP;
                    break;
                case TipoPoliza.SalidaGanado:
                    claveCuenta = ClaveCuenta.CuentaInventarioTransito;
                    break;
                case TipoPoliza.EntradaGanadoDurango:
                    claveCuenta = ClaveCuenta.CuentaInventarioIntensivo;
                    break;
                case TipoPoliza.SalidaVentaIntensiva:
                case TipoPoliza.SalidaMuerteIntensiva:
                    claveCuenta = ClaveCuenta.SalidaMuerteIntensiva;
					//claveCuenta = ClaveCuenta.SalidaMuerteIntensiva; [AP]
                    break;
            }
            var interfaceBl = new InterfaceSalidaBL();
            var claveContableInfo = interfaceBl.ObtenerCuentaInventario(costo, organizacionId, claveCuenta);

            return claveContableInfo;
        }

        /// <summary>
        /// Obtiene la cuenta de Inventario
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="tipoPoliza"> </param>
        /// <returns></returns>
        protected ClaveContableInfo ObtenerCuentaInventario(int organizacionId, TipoPoliza tipoPoliza)
        {
            var claveCuenta = ClaveCuenta.CuentaCostoMP;
            switch (tipoPoliza)
            {
                case TipoPoliza.SalidaVentaProducto:
                    claveCuenta = ClaveCuenta.CuentaBeneficioMP;
                    break;
            }
            var interfaceBl = new InterfaceSalidaBL();
            var claveContableInfo = interfaceBl.ObtenerCuentaInventario(organizacionId, claveCuenta);

            return claveContableInfo;
        }

        /// <summary>
        /// Obtiene las cuentas SAP
        /// </summary>
        /// <returns></returns>
        protected IList<CuentaSAPInfo> ObtenerCuentasSAP()
        {
            var cuentaSapBL = new CuentaSAPBL();
            IList<CuentaSAPInfo> cuentasSap = cuentaSapBL.ObtenerTodos();
            return cuentasSap;
        }

        /// <summary>
        /// Obtiene el nombre del archivo
        /// para la poliza
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        protected string ObtenerArchivoFolio(DateTime fecha)
        {
            var archivoFolio = new StringBuilder();
            archivoFolio.Append(fecha.ToString("yyyyMMdd"));
            archivoFolio.Append(DateTime.Now.Minute);
            archivoFolio.Append(DateTime.Now.Second);
            archivoFolio.Append(new Random(1).Next(1, 9));

            return archivoFolio.ToString();
        }

        /// <summary>
        /// Obtiene una organizacion con el IVA
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        protected OrganizacionInfo ObtenerOrganizacionIVA(int organizacionID)
        {
            var organizacionBl = new OrganizacionBL();
            var organizacion = organizacionBl.ObtenerPorIdConIva(organizacionID);
            return organizacion;
        }

        /// <summary>
        /// Obtiene una organizacion la sociedad y la divicion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        protected OrganizacionInfo ObtenerOrganizacionSociedadDivision(int organizacionID, SociedadEnum sociedad)
        {
            var organizacionBl = new OrganizacionBL();
            var organizacion = organizacionBl.ObtenerOrganizacionSociedadDivision(organizacionID, sociedad);
            return organizacion;
        }

        /// <summary>
        /// Obtiene los costos
        /// </summary>
        /// <returns></returns>
        protected IList<CostoInfo> ObtenerCostos()
        {
            var costosBL = new CostoBL();
            IList<CostoInfo> costos = costosBL.ObtenerTodos(EstatusEnum.Activo);
            return costos;
        }

        /// <summary>
        /// Obtiene las formulas
        /// </summary>
        /// <returns></returns>
        protected IList<FormulaInfo> ObtenerFormulas()
        {
            var formulaBL = new FormulaBL();
            IList<FormulaInfo> formulas = formulaBL.ObtenerTodos(EstatusEnum.Activo);
            return formulas;
        }

        /// <summary>
        /// Obtiene los productos
        /// </summary>
        /// <returns></returns>
        protected IList<ProductoInfo> ObtenerProductos()
        {
            var productoBL = new ProductoBL();
            IList<ProductoInfo> productos = productoBL.ObtenerPorEstados(EstatusEnum.Activo);
            return productos;
        }

        /// <summary>
        /// Obtiene un proveedor almancen
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        protected ProveedorAlmacenInfo ObtenerProveedorAlmacen(ProveedorInfo proveedor)
        {
            var proveedorAlmacenBL = new ProveedorAlmacenBL();
            ProveedorAlmacenInfo proveedorAlmacen = proveedorAlmacenBL.ObtenerPorProveedorId(proveedor);
            return proveedorAlmacen;
        }

        /// <summary>
        /// Obtiene un almacen por clave
        /// </summary>
        /// <param name="almanceId"></param>
        /// <returns></returns>
        protected AlmacenInfo ObtenerAlmacen(int almanceId)
        {
            var almacenBL = new AlmacenBL();
            AlmacenInfo almacen = almacenBL.ObtenerPorID(almanceId);
            return almacen;
        }

        /// <summary>
        /// Obtiene las formulas por almacen
        /// </summary>
        /// <param name="almanceId"></param>
        /// <returns></returns>
        protected IList<FormulaInfo> ObtenerFormulas(int almanceId)
        {
            var formulaBL = new FormulaBL();
            IList<FormulaInfo> formulas = formulaBL.ObtenerTodos(EstatusEnum.Activo);
            return formulas;
        }

        /// <summary>
        /// Genera linea para los registros
        /// contable
        /// </summary>
        /// <param name="polizas"> </param>
        /// <param name="cargos"> </param>
        /// <param name="abonos"> </param>
        protected virtual void GeneraLineaRegistroContable(IList<PolizaInfo> polizas, out IList<PolizaInfo> cargos, out IList<PolizaInfo> abonos)
        {
            cargos = new List<PolizaInfo>();
            abonos = new List<PolizaInfo>();
            if (Cancelacion)
            {
                cargos = polizas.Where(abo => !abo.Importe.Contains("-")).Select(abo => abo).ToList();
                abonos = polizas.Where(car => car.Importe.Contains("-")).Select(car => car).ToList();
            }
            else
            {
                cargos = polizas.Where(car => !car.Importe.Contains("-")).Select(car => car).ToList();
                abonos =
                    polizas.Where(abo => abo.Importe.Contains("-")).Select(abo => abo).ToList();
            }
        }

        /// <summary>
        /// Genera linea para totales
        /// de registro contable
        /// </summary>
        /// <param name="costos"> </param>
        /// <param name="descripcionTotal"> </param>
        protected void GenerarLineaSumaRegistroContable(IList<PolizaInfo> costos, string descripcionTotal)
        {
            decimal cargo;
            decimal abono;
            if (Cancelacion)
            {
                cargo =
                    costos.Where(car => car.Importe.Contains("-")).Sum(
                        cos => Convert.ToDecimal(cos.Importe.Replace("-", string.Empty)));
                abono =
                    costos.Where(abo => !abo.Importe.Contains("-")).Sum(
                        cos => Math.Abs(Convert.ToDecimal(cos.Importe.Replace("-", string.Empty))));
            }
            else
            {
                cargo =
                    costos.Where(car => !car.Importe.Contains("-")).Sum(
                        cos => Convert.ToDecimal(cos.Importe.Replace("-", string.Empty)));
                abono =
                    costos.Where(abo => abo.Importe.Contains("-")).Sum(
                        cos => Math.Abs(Convert.ToDecimal(cos.Importe.Replace("-", string.Empty))));
            }

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = descripcionTotal,
                                                      Desplazamiento = 3,
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = cargo.ToString("N2", CultureInfo.CurrentCulture),
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = abono.ToString("N2", CultureInfo.CurrentCulture),
                                                      Alineacion = "right"
                                                  },
                                          };
        }

        protected void GeneraLinea(int desplazamiento)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "-".PadRight(195, '-'),
                                                      Desplazamiento = desplazamiento
                                                  },
                                          };
        }

        /// <summary>
        /// Genera Linea de Encabezado de Costo
        /// </summary>
        protected void GeneraLineaEncabezadoCostos()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "COSTOS"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PARCIAL",
                                                      Alineacion = "center"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "TOTAL",
                                                      Alineacion = "center"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "OBSERVACIONES:"
                                                  },
                                          };
        }

        /// <summary>
        /// Genera linea para totales de costo
        /// </summary>
        protected void GeneraLineaCostosTotales()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "----------------",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "----------------",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                          };
        }

        /// <summary>
        /// Genera linea con totoles del costo
        /// </summary>
        /// <param name="totalCosto"></param>
        /// <param name="costoParcial"></param>
        protected void GenerarLineaTotalCosto(decimal totalCosto, bool costoParcial)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "* Total *",
                                                      Alineacion = "center"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          costoParcial
                                                              ? totalCosto.ToString("N", CultureInfo.CurrentCulture)
                                                              : string.Empty,
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = totalCosto.ToString("N", CultureInfo.CurrentCulture),
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right"
                                                  },
                                          };
        }

        /// <summary>
        /// Genera linea con la leyeda de 
        /// Elaboro, Reviso, Recibio
        /// </summary>
        protected void GenerarLineaElaboro()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}{1}", "ELABORO:", "_".PadRight(15, '_')),
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}{1}", "REVISO:", "_".PadRight(15, '_')),
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}{1}", "RECIBIO:", "_".PadRight(15, '_')),
                                                      Desplazamiento = 2
                                                  },
                                          };
        }

        /// <summary>
        /// Genera linea con la leyeda de 
        /// Elaboro, Reviso, Recibio
        /// </summary>
        protected void GenerarLineaRevisoRecibio()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}{1}", "REVISO:", "_".PadRight(15, '_')),
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}{1}", "RECIBIO:", "_".PadRight(15, '_')),
                                                          Alineacion = "right",
                                                      Desplazamiento = 2
                                                  },
                                          };
        }

        /// <summary>
        /// Genera el encabezado del registro contable
        /// </summary>
        /// <param name="folio"></param>
        protected void GeneraLineaEncabezadoRegistroContable(long folio)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Registro Contable",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Format("{0} {1} {2}", "Poliza", "No.", folio),
                                                      Desplazamiento = 3
                                                  },
                                          };
        }

        /// <summary>
        /// Genera el subencabezado del registro contable
        /// </summary>
        /// <param name="mostrarConcepto"></param>
        /// <param name="descripcionCodigo"> </param>
        /// <param name="descripcionCargo"> </param>
        /// <param name="descripcionAbono"> </param>
        protected void GeneraLineaSubEncabezadoRegistroContable(bool mostrarConcepto, string descripcionCodigo
                                                               , string descripcionCargo, string descripcionAbono)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = descripcionCodigo,
                                                      Alineacion = "left",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "DESCRIPCION",
                                                      Alineacion = "left",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = mostrarConcepto ? "CONCEPTO" : string.Empty,
                                                      Alineacion = "left",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = descripcionCargo,
                                                      Alineacion = "right",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = descripcionAbono,
                                                      Alineacion = "right",
                                                  },
                                          };
        }

        /// <summary>
        /// Obtiene el ultimo movimiento
        /// del animal
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        protected List<AnimalMovimientoInfo> ObtenerUltimoMovimiento(List<AnimalInfo> animales)
        {
            var animalMovimientoBL = new AnimalMovimientoBL();
            List<AnimalMovimientoInfo> animalesMovimiento = animalMovimientoBL.ObtenerUltimoMovimientoAnimal(animales);

            return animalesMovimiento;
        }

        /// <summary>
        /// Obtiene los almacenes con sus productos cuenta
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        protected IList<ClaseCostoProductoInfo> ObtenerCostosProducto(int almacenID)
        {
            var claseCostoProductoBL = new ClaseCostoProductoBL();
            IList<ClaseCostoProductoInfo> costosProducto = claseCostoProductoBL.ObtenerPorAlmacen(almacenID);
            return costosProducto;
        }

        /// <summary>
        /// Obtiene los almacenes con sus productos cuenta
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        protected IList<CuentaAlmacenSubFamiliaInfo> ObtenerCostosSubFamilia(int almacenID)
        {
            var claseCostoProductoBL = new CuentaAlmacenSubFamiliaBL();
            IList<CuentaAlmacenSubFamiliaInfo> costosProducto = claseCostoProductoBL.ObtenerCostosSubFamilia(almacenID);
            return costosProducto;
        }

        /// <summary>
        /// Genera Linea de Encabezados para el Detalle
        /// </summary>
        protected void GeneraLineaEncabezadoDetalle(List<PolizaEncabezadoModel> encabezados)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>(encabezados);
        }

        /// <summary>
        /// Obtiene un parametro por su organizacion y clave
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="claveParametro"></param>
        /// <returns></returns>
        protected ParametroOrganizacionInfo ObtenerParametroOrganizacionPorClave(int organizacionID, string claveParametro)
        {
            var parametroOrganizacionBL = new ParametroOrganizacionBL();
            ParametroOrganizacionInfo parametroOrganizacion =
                parametroOrganizacionBL.ObtenerPorOrganizacionIDClaveParametro(organizacionID, claveParametro);
            return parametroOrganizacion;
        }

        /// <summary>
        /// Obtiene el valor del parametro general
        /// </summary>
        /// <param name="claveParametro"></param>
        /// <returns></returns>
        protected ParametroGeneralInfo ObtenerParametroGeneralPorClave(string claveParametro)
        {
            var parametroGeneralBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametroGeneral = parametroGeneralBL.ObtenerPorClaveParametro(claveParametro);
            return parametroGeneral;
        }

        /// <summary>
        /// Obtiene los almacenes por Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        protected IList<AlmacenInfo> ObtenerAlmacenesPorOrganizacion(int organizacionID)
        {
            var almacenBL = new AlmacenBL();
            IList<AlmacenInfo> almacenes = almacenBL.ObtenerAlmacenPorOrganizacion(organizacionID);
            return almacenes;
        }

        /// <summary>
        /// Obtiene las unidades de medicion
        /// </summary>
        /// <returns></returns>
        protected IList<UnidadMedicionInfo> ObtenerUnidadesMedicion()
        {
            var unidadMedicionBL = new UnidadMedicionBL();
            IList<UnidadMedicionInfo> unidades = unidadMedicionBL.ObtenerTodos(EstatusEnum.Activo);
            return unidades;
        }

        /// <summary>
        /// Obtiene el tipo de cambio
        /// </summary>
        /// <param name="tipoCambioId"></param>
        /// <returns></returns>
        protected TipoCambioInfo ObtenerTipoCambio(int tipoCambioId)
        {
            var tipoCambioBL = new TipoCambioBL();
            TipoCambioInfo tipoCambio = tipoCambioBL.ObtenerPorId(tipoCambioId);
            return tipoCambio;
        }

        /// <summary>
        /// Obtiene los Centros de Costo activos
        /// </summary>
        /// <returns></returns>
        protected IList<CentroCostoInfo> ObtenerCentrosCosto()
        {
            var centroCostoDAL = new CentroCostoDAL();
            IList<CentroCostoInfo> centrosCosto = centroCostoDAL.ObtenerTodos(EstatusEnum.Activo);
            return centrosCosto;
        }


        /// <summary>
        /// Obtiene los camiones de reparto de la Organizacion
        /// </summary>
        /// <returns></returns>
        protected IList<CamionRepartoInfo> ObtenerCamionesRepartoPorOrganizacion(int organizacionID)
        {
            var camionRepartoDAL = new CamionRepartoDAL();
            IList<CamionRepartoInfo> camionesReparto = camionRepartoDAL.ObtenerPorOrganizacionID(organizacionID);
            return camionesReparto;
        }

        /// <summary>
        /// Obtiene los proveedores
        /// </summary>
        /// <returns></returns>
        protected IList<ProveedorInfo> ObtenerProveedores()
        {
            var proveedorBL = new ProveedorBL();
            IList<ProveedorInfo> proveedores = proveedorBL.ObtenerTodos(EstatusEnum.Activo);
            return proveedores;
        }

        /// <summary>
        /// Valida si afectara cuenta de costo
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        protected bool ValidarAfectacionCuentaCosto(ProductoInfo producto)
        {
            bool afectaCosto;
            ParametroGeneralInfo parametroGeneral =
                ObtenerParametroGeneralPorClave(ParametrosEnum.PRODCTACOSTO.ToString());
            List<ProductoInfo> productosCosto = parametroGeneral.Valor.Split('|')
                .Select(x => new ProductoInfo
                                 {
                                     ProductoId = Convert.ToInt32(x)
                                 }).ToList();
            afectaCosto = productosCosto.Any(id => id.ProductoId == producto.ProductoId);
            return afectaCosto;
        }

        /// <summary>
        /// Regresa el Folio de Referencia de la poliza
        /// </summary>
        /// <returns></returns>
        protected string ObtenerNumeroReferenciaFolio(long folioReferencia)
        {
            return folioReferencia.ToString(CultureInfo.InvariantCulture);
        }

        #endregion METODOS PROTEGIDOS

        #region METODOS PRIVADOS

        /// <summary>
        /// Obtiene los tipos de poliza
        /// </summary>
        private void ObtenerTiposPoliza()
        {
            if (TiposPoliza == null || !TiposPoliza.Any())
            {
                var tipoPolizaBL = new TipoPolizaBL();
                TiposPoliza = tipoPolizaBL.ObtenerTodos();
            }
        }

        /// <summary>
        /// Obtiene el XML que se guardara en archivo XML
        /// </summary>
        /// <param name="polizas"></param>
        /// <returns></returns>
        private string ObtenerPolizaXML(IList<PolizaInfo> polizas)
        {
            var xml =
                new XElement("MT_POLIZA_PERIFERICO",
                             from poliza in polizas
                             select new XElement("Datos",
                                                 new XElement("noref",
                                                              poliza.NumeroReferencia),
                                                 new XElement("fecha_doc", poliza.FechaDocumento),
                                                 new XElement("fecha_cont", poliza.FechaContabilidad),
                                                 new XElement("clase_doc", poliza.ClaseDocumento),
                                                 new XElement("sociedad", poliza.Sociedad),
                                                 new XElement("moneda", poliza.Moneda),
                                                 new XElement("tipocambio", poliza.TipoCambio),
                                                 new XElement("texto_doc", poliza.TextoDocumento),
                                                 new XElement("mes", poliza.Mes),
                                                 new XElement("cuenta", poliza.Cuenta),
                                                 new XElement("proveedor", poliza.Proveedor),
                                                 new XElement("cliente", poliza.Cliente),
                                                 new XElement("indica_cme", poliza.IndicaCme),
                                                 new XElement("importe",
                                                              "-0".Equals(poliza.Importe,
                                                                          StringComparison.CurrentCultureIgnoreCase)
                                                              ||
                                                              "-0.00".Equals(poliza.Importe,
                                                                             StringComparison.CurrentCultureIgnoreCase)
                                                                  ? "0"
                                                                  : poliza.Importe),
                                                 new XElement("indica_imp", poliza.IndicaImp),
                                                 new XElement("centro_cto", poliza.CentroCosto),
                                                 new XElement("orden_int", poliza.OrdenInt),
                                                 new XElement("centro_ben", poliza.CentroBeneficio),
                                                 new XElement("texto_asig", poliza.TextoAsignado),
                                                 new XElement("concepto", poliza.Concepto),
                                                 new XElement("division", poliza.Division),
                                                 new XElement("clase_movt", poliza.ClaseMovimiento),
                                                 new XElement("bus_act", poliza.BusAct),
                                                 new XElement("periodo", poliza.Periodo),
                                                 new XElement("nolinea", poliza.NumeroLinea),
                                                 new XElement("ref1", poliza.Referencia1),
                                                 new XElement("ref2", poliza.Referencia2),
                                                 new XElement("ref3", poliza.Referencia3),
                                                 new XElement("fecha_imto", poliza.FechaImpuesto),
                                                 new XElement("cond_imto", poliza.CondicionImpuesto),
                                                 new XElement("clave_imto", poliza.ClaveImpuesto),
                                                 new XElement("tipo_ret", poliza.TipoRetencion),
                                                 new XElement("cod_ret", poliza.CodigoRetencion),
                                                 new XElement("imp_ret", poliza.ImpuestoRetencion),
                                                 new XElement("imp_iva", poliza.ImpuestoIva),
                                                 new XElement("archifolio", poliza.ArchivoFolio)
                                 ));
            return xml.ToString();
        }

        /// <summary>
        /// Obtiene una instancia del servicio de PI
        /// </summary>
        /// <returns></returns>
        private SI_LOGIN_PolizasContablesClient ObtenerInstanciaServicioPI()
        {
            var parametroGeneralBL = new ParametroGeneralBL();

            ParametroGeneralInfo parametroGeneral;
            parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(
                    ParametrosEnum.ENDPOINTWSSAP.ToString());
            var client =
                new SI_LOGIN_PolizasContablesClient(parametroGeneral.Valor);
            parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(
                    ParametrosEnum.USUARIOWSSAP.ToString());
            client.ClientCredentials.UserName.UserName =
                parametroGeneral.Valor;
            parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(
                    ParametrosEnum.PASSWORDWSSAP.ToString());
            client.ClientCredentials.UserName.Password =
                parametroGeneral.Valor;

            return client;
        }

        /// <summary>
        /// Obtiene una instancia del servicio de PI
        /// </summary>
        /// <returns></returns>
        private SI_LOGIN_CancelarDoctosClient ObtenerInstanciaCancelacionPI()
        {
            var parametroGeneralBL = new ParametroGeneralBL();

            ParametroGeneralInfo parametroGeneral;
            parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(
                    ParametrosEnum.ENDPOINTWSSAP.ToString());
            var client =
                new SI_LOGIN_CancelarDoctosClient(parametroGeneral.Valor);
            parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(
                    ParametrosEnum.USUARIOWSSAP.ToString());
            client.ClientCredentials.UserName.UserName =
                parametroGeneral.Valor;
            parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(
                    ParametrosEnum.PASSWORDWSSAP.ToString());
            client.ClientCredentials.UserName.Password =
                parametroGeneral.Valor;

            return client;
        }

        /// <summary>
        /// Se obtiene los folios a cancelar
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        /// <returns></returns>
        private List<string> ObtenerFoliosCancelar(IList<DT_POLIZA_PERIFERICO_ITEMDatos> polizasPI)
        {
            return polizasPI.Select(ref3 => ref3.ref3).Distinct().ToList();
        }

        #endregion METODOS PRIVADOS

        #region METODOS ABSTRACTOS

        /// <summary>
        /// Genera la Poliza
        /// para Impresion
        /// </summary>
        public abstract MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas);

        /// <summary>
        /// Genera el XML de la Poliza
        /// </summary>
        public abstract IList<PolizaInfo> GeneraPoliza(object datosPoliza);

        #endregion METODOS ABSTRACTOS

        #region METODOS PUBLICOS

        /// <summary>
        /// Posiciona el XML generado en una ruta configurada
        /// </summary>
        /// <param name="polizas"></param>
        /// <param name="organizacionID"></param>
        public void GuardarArchivoXML(IList<PolizaInfo> polizas, int organizacionID)
        {
            ParametroGeneralInfo parametroOrganizacion = ObtenerParametroGeneralPorClave(ParametrosEnum.
                                                                                             RUTAXMLPOLIZA.
                                                                                             ToString());
            if (parametroOrganizacion == null || string.IsNullOrWhiteSpace(parametroOrganizacion.Valor))
            {
                throw new ExcepcionServicio(
                    "NO SE ENCUENTRA CONFIGURADA LA RUTA PARA EL ARCHIVO DE POLIZA EN FORMATO XML");
            }
            try
            {
                if (!Directory.Exists(parametroOrganizacion.Valor))
                {
                    Directory.CreateDirectory(parametroOrganizacion.Valor);
                }
                string nombreArchivo = polizas.Select(nombre => nombre.ArchivoFolio).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(nombreArchivo))
                {
                    throw new ExcepcionServicio("NOMBRE DE ARCHIVO INCORRECTO");
                }
                if (!nombreArchivo.Contains(".xml"))
                {
                    nombreArchivo = string.Format("{0}{1}", nombreArchivo, ".xml");
                }
                string xml = ObtenerPolizaXML(polizas);
                File.WriteAllText(Path.Combine(parametroOrganizacion.Valor, nombreArchivo),
                                  string.Format("{0}\n{1}", "<?xml version=\"1.0\" standalone=\"yes\"?>", xml));
                if (organizacionID == 5)
                {
                    try
                    {
                        File.WriteAllText(
                            Path.Combine(@"\\srv-lab2\xml", string.Format("{0}_{1}", DateTime.Now.Ticks, nombreArchivo)),
                            string.Format("{0}\n{1}", "<?xml version=\"1.0\" standalone=\"yes\"?>", xml));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (ExcepcionServicio ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Convierte una lista de DatosPoliza en una lista de DT_POLIZA_PERIFERICO_ITEMDatos
        /// para que SAP la procese
        /// </summary>
        /// <param name="polizas"></param>
        /// <returns></returns>
        public IList<DT_POLIZA_PERIFERICO_ITEMDatos> ConvertirPolizaServicio(IList<PolizaInfo> polizas)
        {
            var datosSAP = new List<DT_POLIZA_PERIFERICO_ITEMDatos>();

            List<string> referencias = polizas.Select(ref3 => ref3.Referencia3).Distinct().ToList();
            for (int indexRef3 = 0; indexRef3 < referencias.Count; indexRef3++)
            {
                datosSAP.AddRange(
                    polizas.Where(ref3 => ref3.Referencia3.Equals(referencias[indexRef3])).Select(
                        datos => new DT_POLIZA_PERIFERICO_ITEMDatos
                                    {
                                        noref = datos.NumeroReferencia ?? string.Empty,
                                        fecha_doc = datos.FechaDocumento ?? string.Empty,
                                        fecha_cont = datos.FechaDocumento ?? string.Empty,
                                        clase_doc = datos.ClaseDocumento ?? string.Empty,
                                        cliente = datos.Cliente ?? string.Empty,
                                        sociedad = datos.Sociedad ?? string.Empty,
                                        moneda = datos.Moneda ?? string.Empty,
                                        cond_imto = datos.CondicionImpuesto ?? string.Empty,
                                        texto_doc = datos.TextoDocumento ?? string.Empty,
                                        mes = datos.Mes ?? string.Empty,
                                        cuenta = datos.Cuenta ?? string.Empty,
                                        proveedor = datos.Proveedor ?? string.Empty,
                                        importe = datos.Importe ?? string.Empty,
                                        indica_imp = datos.IndicaImp ?? string.Empty,
                                        centro_cto = datos.CentroCosto ?? string.Empty,
                                        centro_ben = datos.CentroBeneficio ?? string.Empty,
                                        texto_asig = datos.TextoAsignado ?? string.Empty,
                                        concepto = datos.Concepto ?? string.Empty,
                                        division = datos.Division ?? string.Empty,
                                        bus_act = datos.BusAct ?? string.Empty,
                                        periodo = datos.Periodo ?? string.Empty,
                                        nolinea = datos.NumeroLinea ?? string.Empty,
                                        ref1 = datos.Referencia1 ?? string.Empty,
                                        ref2 = datos.Referencia2 ?? string.Empty,
                                        ref3 = datos.Referencia3 ?? string.Empty,
                                        fecha_imto = datos.FechaImpuesto ?? string.Empty,
                                        tipo_ret = datos.TipoRetencion ?? string.Empty,
                                        cod_ret = datos.CodigoRetencion ?? string.Empty,
                                        imp_iva = datos.ImpuestoIva ?? string.Empty,
                                        clave_imto = datos.ClaveImpuesto ?? string.Empty,
                                        archifolio = datos.ArchivoFolio ?? string.Empty,
                                        area_func = string.Empty,
                                        clase_movt = datos.ClaseMovimiento ?? string.Empty,
                                        imp_ret = "0",
                                        indica_cme = datos.IndicaCme ?? string.Empty,
                                        orden_int = datos.OrdenInt ?? string.Empty,
                                        segmento = datos.Segmento ?? string.Empty,
                                        tipocambio = string.Empty
                                    }));
            }
            return datosSAP;
        }

        /// <summary>
        /// Obtiene los documentos de SAP
        /// </summary>
        /// <param name="foliosACancelar"></param>
        /// <param name="polizasACancelar"></param>
        /// <returns></returns>
        public List<PolizaInfo> ObtenerDocumentosSAP(List<string> foliosACancelar, IList<PolizaInfo> polizasACancelar)
        {
            var polizas = new List<PolizaInfo>();
            for (var indexFolios = 0; indexFolios < foliosACancelar.Count; indexFolios++)
            {
                polizas.AddRange(
                    polizasACancelar.Where(concepto => concepto.Referencia3.Equals(foliosACancelar[indexFolios])));
            }
            return polizas;
        }

        /// <summary>
        /// Envia poliza a servicio de PI para ser procesada
        /// por SAP
        /// </summary>
        /// <param name="datosPoliza"></param>
        public RespuestaServicioPI EnviarPolizaServicioPI(IList<PolizaInfo> datosPoliza)
        {
            RespuestaServicioPI resultado;

            try
            {
                SI_LOGIN_PolizasContablesClient client = ObtenerInstanciaServicioPI();

                IList<DT_POLIZA_PERIFERICO_ITEMDatos> polizasPI = ConvertirPolizaServicio(datosPoliza);
                DT_PolizasContablesResItem[] respuesta = client.SI_LOGIN_PolizasContables(polizasPI.ToArray());
                if (respuesta != null && respuesta[0].MESSAGE.IndexOf("Error", StringComparison.InvariantCultureIgnoreCase) < 0)
                {
                    datosPoliza.ToList().ForEach(item => item.DocumentoSAP = respuesta[0].MESSAGE_V2);
                    resultado = new RespuestaServicioPI { Mensaje = "OK", Polizas = datosPoliza };
                }
                else
                {
                    var sb = new StringBuilder();
                    for (int indexRespuesta = 0; indexRespuesta < respuesta.Length; indexRespuesta++)
                    {
                        sb.AppendFormat("{0} {1} ; ", respuesta[indexRespuesta].MESSAGE, respuesta[indexRespuesta].MESSAGE_V4);
                    }
                    resultado = new RespuestaServicioPI { Mensaje = sb.ToString(), Polizas = datosPoliza };
                }
            }
            catch (Exception ex)
            {
                resultado = new RespuestaServicioPI { Mensaje = ex.Message, Polizas = datosPoliza };
            }
            return resultado;
        }

        /// <summary>
        /// Envia poliza a servicio de PI para ser procesada
        /// por SAP
        /// </summary>
        /// <param name="datosPoliza"></param>
        public RespuestaServicioPI CancelacionPolizaServicioPI(IList<PolizaInfo> datosPoliza)
        {
            RespuestaServicioPI resultadoCancelacion = null;

            SI_LOGIN_CancelarDoctosClient client = ObtenerInstanciaCancelacionPI();

            IList<DT_POLIZA_PERIFERICO_ITEMDatos> polizasPI = ConvertirPolizaServicio(datosPoliza);

            List<string> referencia3 = polizasPI.Select(ref3 => ref3.ref3).Distinct().ToList();

            List<string> foliosACancelar = ObtenerFoliosCancelar(polizasPI);

            List<PolizaInfo> documentosSAP = ObtenerDocumentosSAP(foliosACancelar, datosPoliza);

            List<string> documentosCancelarSAP = documentosSAP.Where(doc => doc.DocumentoSAP.Length > 0)
                                                              .Select(doc => doc.DocumentoSAP)
                                                              .Distinct().ToList();
            IList<DT_LOGIN_CancelarDoctos_ItemItem> polizasRef3;
            DT_SAP_CancelarDoctos_ResItem[] resultado;
            for (var indexRef3 = 0; indexRef3 < documentosCancelarSAP.Count; indexRef3++)
            {
                polizasRef3 = documentosSAP.Where(
                    ref3 => ref3.DocumentoSAP.Equals(documentosCancelarSAP[indexRef3])).
                    Select(
                        cance => new DT_LOGIN_CancelarDoctos_ItemItem
                        {
                            division = cance.Division,
                            sociedad = cance.Sociedad,
                            periodo = cance.Periodo,
                            noref = cance.NumeroReferencia,
                            archivo = cance.ArchivoFolio,
                            clase_doc = cance.ClaseDocumento,
                            doc_sap = cance.DocumentoSAP,
                            fecha_canc = cance.FechaDocumento
                        }).Distinct().ToList();
                resultado = client.SI_LOGIN_CancelarDoctos(polizasRef3.ToArray());
                if (resultado != null && resultado.Length > 0)
                {
                    documentosSAP.ForEach(doc => doc.DocumentoCancelacionSAP = resultado[0].MESSAGE_V2);
                    if ("$".Equals(resultado[0].MESSAGE_V2))
                    {
                        var sb = new StringBuilder();
                        for (int indexMensaje = 0; indexMensaje < resultado.Length; indexMensaje++)
                        {
                            sb.AppendFormat("{0} ; ", resultado[indexMensaje].MESSAGE);
                        }
                        resultadoCancelacion = new RespuestaServicioPI
                        {
                            Mensaje = sb.ToString(),
                            Polizas = documentosSAP
                        };
                    }
                    else
                    {
                        resultadoCancelacion = new RespuestaServicioPI
                        {
                            Mensaje = "OK",
                            Polizas = documentosSAP
                        };
                    }
                }
            }
            return resultadoCancelacion;
        }

        #endregion METODOS PUBLICOS
    }
}
