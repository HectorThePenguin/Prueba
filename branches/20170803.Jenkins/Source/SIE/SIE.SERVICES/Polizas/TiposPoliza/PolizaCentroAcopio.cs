using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SuKarne.Controls.Impresora;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SIE.Services.Polizas.TiposPoliza
{
    /// <summary>
    /// Clase para Generar Poliza de Centro de Acopio
    /// </summary>
    public class PolizaCentroAcopio : PolizaAbstract
    {
        public PolizaCentroAcopio()
        {
        }

        /// <summary>
        /// Genera la Poliza
        /// </summary>
        public override void GenerarPoliza(ContenedorCosteoEntradaGanadoInfo contenedorCosteoEntrada)
        {
            try
            {                
                var ticket = new Ticket
                {
                    OpcionesImpresora = new OpcionesImpresora
                    {
                        Impresora = NombreImpresora,
                        MaximoLinea = MaxCaracteresLinea
                    }
                };
                IList<EntradaGanadoCostoInfo> costoProveedores = contenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada
                                                                    .Where(prov => prov.TieneCuenta == false)
                                                                    .Select(prov => prov).ToList();

                IList<EntradaGanadoCostoInfo> costoCuenta = contenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada
                                                                        .Where(cuenta => cuenta.TieneCuenta == true)
                                                                        .Select(cuenta => cuenta).ToList();
                IList<LineaImpresionInfo> lineas = new List<LineaImpresionInfo>();
                IList<int> clavesProveedor = costoProveedores.Select(clave => clave.Proveedor.ProveedorID).ToList();
                if (clavesProveedor != null)
                {
                    var operadorBL = new OperadorBL();
                    OperadorInfo operador = operadorBL.ObtenerPorID(contenedorCosteoEntrada.EntradaGanado.OperadorID);
                    string organizacionOrigen = contenedorCosteoEntrada.EntradaGanado.OrganizacionOrigen;
                    string folio = Convert.ToString(contenedorCosteoEntrada.EntradaGanado.FolioEntrada);
                    string fecha = contenedorCosteoEntrada.EntradaGanado.FechaEntrada.ToShortDateString();

                    IList<EntradaGanadoCostoInfo> entradaGanadoCosto;
                    for (int indexClavesProveedor = 0; indexClavesProveedor < clavesProveedor.Count; indexClavesProveedor++)
                    {
                        entradaGanadoCosto = costoProveedores.Where(clave => clave.Proveedor.ProveedorID
                                                                     == clavesProveedor[indexClavesProveedor])
                                                                 .Select(datos => datos).ToList();
                        GeneraLineaCabecero(lineas, folio);
                        GeneraLineaReferenciaFecha(lineas, organizacionOrigen, fecha);
                        GeneraLineaProveedorComprador(lineas
                                                    , entradaGanadoCosto[0].Proveedor, operador);
                        for (int indexCostos = 0; indexCostos < entradaGanadoCosto.Count; indexCostos++)
                        {
                            GeneraLineaGuiones(lineas, '-', 90);
                            GeneraLineaEncabezadoDetalle(lineas);

                            IList<EntradaDetalleInfo> detalleEntrada = contenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle
                                                                      .Where(det => det.EntradaGanadoCosteoID == entradaGanadoCosto[indexCostos].EntradaGanadoCosteoID)
                                                                      .Select(detalle => detalle).ToList();
                            GeneraLineasDetalle(lineas, detalleEntrada, contenedorCosteoEntrada.EntradaGanado);
                            GeneraLineaGuiones(lineas, '-', 90);
                            GeneraLineaTotales(lineas, detalleEntrada);
                            GeneraLineaGuiones(lineas, '-', 90);

                            decimal pesoNeto = contenedorCosteoEntrada.EntradaGanado.PesoBruto - contenedorCosteoEntrada.EntradaGanado.PesoTara;
                            GeneraLineasEncabezadoMerma(lineas, detalleEntrada, pesoNeto);
                            GeneraLineaEncabezadoCalidad(lineas, contenedorCosteoEntrada);
                            IList<EntradaGanadoCalidadInfo> calidades = contenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado
                                                                        .Where(cal => cal.EntradaGanadoCosteoID 
                                                                      == entradaGanadoCosto[indexCostos].EntradaGanadoCosteoID).ToList();
                            GeneraLineaCalidad(lineas, calidades, organizacionOrigen);

                            GeneraLineaEncabezadoCostos(lineas);
                            GeneraLineaCostos(lineas, costoProveedores);
                            GeneraLineaCostosTotales(lineas);
                            GeneraLineaTotalCostos(lineas, costoProveedores);
                            GeneraLineaGuiones(lineas, '_', 90);
                            GeneraLineaEncabezadoRegistroContable(lineas);
                            GeneraLineaSubEncabezadoRegistroContable(lineas);
                            GeneraLineaRegistroContable(lineas);
                            GeneraLineaGuiones(lineas, '-', 90);
                            GenerarLineaSumaRegistroContable(lineas);
                            GeneraLineaGuiones(lineas, '-', 90);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Genera linea para totales
        /// de registro contable
        /// </summary>
        /// <param name="lineas"></param>
        private void GenerarLineaSumaRegistroContable(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 50
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(String.Empty.PadRight(35));
            SB.Append("{1}").Append(String.Empty.PadRight(5));
            SB.Append("{2}");
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), "*=  SUMAS  -=>", "633.201.98", "633.201.98"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera linea para los registros
        /// contable
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaRegistroContable(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(String.Empty.PadRight(5));
            SB.Append("{1}").Append(String.Empty.PadRight(5));
            SB.Append("{2}").Append(String.Empty.PadRight(15));
            SB.Append("{3}").Append(String.Empty.PadRight(15));
            SB.Append("{4}").Append(String.Empty.PadRight(5));
            //TODO: HACER RECORRIDO DE COLECCION
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), "Registro Contable", "No.", "234234"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera el subencabezado para
        /// los registros contables
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaSubEncabezadoRegistroContable(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(String.Empty.PadRight(5));
            SB.Append("{1}").Append(String.Empty.PadRight(5));
            SB.Append("{2}").Append(String.Empty.PadRight(15));
            SB.Append("{3}").Append(String.Empty.PadRight(15));
            SB.Append("{4}").Append(String.Empty.PadRight(5));
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), "Registro Contable", "No.", "234234"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera liena de Encabezado de Registro contable
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaEncabezadoRegistroContable(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 30
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(String.Empty.PadRight(5));
            SB.Append("{1}").Append(String.Empty.PadRight(2));
            SB.Append("{2}");
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), "Registro Contable", "No.", "234234"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera linea para el total de costo
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaTotalCostos(IList<LineaImpresionInfo> lineas, IList<EntradaGanadoCostoInfo> costos)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 15
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(string.Empty.PadRight(30));
            SB.Append("{1}");

            decimal totalCosto = costos.Sum(cos => cos.Importe);
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), "* Total *", totalCosto),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            linea = new LineaImpresionInfo
            {
                Texto = String.Empty,
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
            lineas.Add(linea);

            SB = new StringBuilder();
            SB.Append("ELABORO:").Append("_".PadRight(10)).Append(String.Empty.PadRight(3));
            SB.Append("REVISO:").Append("_".PadRight(10)).Append(String.Empty.PadRight(3));
            SB.Append("RECIBIO:").Append("_".PadRight(10)).Append(String.Empty.PadRight(3));
            linea = new LineaImpresionInfo
            {
                Texto = String.Empty,
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera linea para Costo
        /// </summary>
        /// <param name="lineas"></param>
        /// <param name="costos"></param>
        private void GeneraLineaCostos(IList<LineaImpresionInfo> lineas, IList<EntradaGanadoCostoInfo> costos)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            StringBuilder SB;
            for (int indexCostos = 0; indexCostos < costos.Count; indexCostos++)
            {
                SB = new StringBuilder();
                SB.Append("{0}").Append(String.Empty.PadRight(5));
                SB.Append("{1}").Append(String.Empty.PadRight(5));
                SB.Append(String.Empty.PadRight(15));
                SB.Append("{3}:");
                var linea = new LineaImpresionInfo
                {
                    Texto = String.Format(SB.ToString()
                                        , costos[indexCostos].Costo.Descripcion
                                        , costos[indexCostos].Importe
                                        , "Observaciones"),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
            }
        }

        /// <summary>
        /// Genera linea para totales de costo
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaCostosTotales(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0} {1}", "----------------", "----------------"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera Linea de Encabezado de Costo
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaEncabezadoCostos(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(String.Empty.PadRight(5));
            SB.Append("{1}").Append(String.Empty.PadRight(5));
            SB.Append("{2}").Append(String.Empty.PadRight(5));
            SB.Append("{3}:");
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), "COSTOS", "PARCIAL", "TOTAL", "OBSERVACIONES"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera linea de Merma
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineasEncabezadoMerma(IList<LineaImpresionInfo> lineas, IList<EntradaDetalleInfo> detalles, decimal pesoLlegada)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            decimal pesoOrigenTotal = detalles.Sum(pesoOrigen => pesoOrigen.PesoOrigen);
            var porcentajeMerma = Math.Round(((pesoOrigenTotal - pesoLlegada) / pesoOrigenTotal) * 100, 2);
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}   {1} kg   {2}%", "MERMA DE TRANSPORTE:"
                                     , Math.Round(pesoOrigenTotal * porcentajeMerma,2), porcentajeMerma),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera la Linea para Encabezado de Calidad
        /// </summary>
        /// <param name="lineas"></param>
        /// <param name="contenedorCosteoEntrada"></param>
        private void GeneraLineaEncabezadoCalidad(IList<LineaImpresionInfo> lineas, ContenedorCosteoEntradaGanadoInfo contenedorCosteoEntrada)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            StringBuilder SB = new StringBuilder();
            IList<String> calidades = contenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Where(sexo => 'M'.Equals(sexo.CalidadGanado.Sexo)).Select(calidad => calidad.CalidadGanado.Descripcion).ToList();
            for (int indexCalidad = 0; indexCalidad < calidades.Count; indexCalidad++)
			{
                SB.Append(calidades[indexCalidad]);
                SB.Append(String.Empty.PadRight(5));
			}
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}   {1}        {2}", "CALIDAD", "Sexo", SB.ToString()),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera Linea de Calidad
        /// </summary>
        /// <param name="lineas"></param>
        /// <param name="contenedorCosteoEntrada"></param>
        private void GeneraLineaCalidad(IList<LineaImpresionInfo> lineas, IList<EntradaGanadoCalidadInfo> calidades, string organizacionOrigen)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 20
            };
            StringBuilder SB = new StringBuilder();
            IList<int> calidadMachos = calidades.Where(sexo => 'M'.Equals(sexo.CalidadGanado.Sexo)).Select(calidad => calidad.Valor).ToList();
            for (int indexCalidadMachos = 0; indexCalidadMachos < calidadMachos.Count; indexCalidadMachos++)
            {
                SB.Append(calidadMachos[indexCalidadMachos]);
                SB.Append(String.Empty.PadRight(5));
            }
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format("{1}        {2}", "Machos", SB.ToString()),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            SB = new StringBuilder();
            IList<int> calidadHembras = calidades.Where(sexo => 'H'.Equals(sexo.CalidadGanado.Sexo)).Select(calidad => calidad.Valor).ToList();
            for (int indexCalidadHembras = 0; indexCalidadHembras < calidadHembras.Count; indexCalidadHembras++)
            {
                SB.Append(calidadHembras[indexCalidadHembras]);
                SB.Append(String.Empty.PadRight(5));
            }
            linea = new LineaImpresionInfo
            {
                Texto = String.Format("{1}        {2}", "Hembras", SB.ToString()),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            linea = new LineaImpresionInfo
            {
                Texto = String.Format("{1}-->  {2}", "ORIGEN", organizacionOrigen),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera linea Encabezado de la Poliza
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaCabecero(IList<LineaImpresionInfo> lineas, string folio)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            var linea = new LineaImpresionInfo
            {
                Texto = "GANADERIA INTEGRAL VIZUR. S.A. DE CV",
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 20
            };
            linea = new LineaImpresionInfo
            {
                Texto = "Nota De Entrada De Ganado",
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 30
            };
            linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}                                      {1}. {2}", "Engorda \"A\"", "FOLIO No", folio),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera linea de Referencia y Fecha de la Poliza
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaReferenciaFecha(IList<LineaImpresionInfo> lineas, string organizacionOrigen, string fecha)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 50
            };
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}:{1}", "REFERENCIA", organizacionOrigen),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}: {1}", "FECHA", fecha),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera Linea para Proveedor y Comprador
        /// </summary>
        /// <param name="lineas"></param>
        /// <param name="proveedor"></param>
        private void GeneraLineaProveedorComprador(IList<LineaImpresionInfo> lineas, ProveedorInfo proveedor, OperadorInfo operador)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}: {1}", "PROVEEDOR", proveedor.Descripcion),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            linea = new LineaImpresionInfo
            {
                Texto = String.Format("{0}: {1}", "COMPRADOR", operador.NombreCompleto),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera Linea con Guiones
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaGuiones(IList<LineaImpresionInfo> lineas, char caracterRelleno,  int numeroGuiones)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 10
            };
            var linea = new LineaImpresionInfo
            {
                Texto = caracterRelleno.ToString().PadRight(numeroGuiones),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera Linea de Encabezados para el Detalle
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaEncabezadoDetalle(IList<LineaImpresionInfo> lineas)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 20
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(string.Empty.PadRight(2));
            SB.Append("{1}").Append(string.Empty.PadRight(2));
            SB.Append("{2}").Append(string.Empty.PadRight(11));
            SB.Append("{3}").Append(string.Empty.PadRight(66));
            SB.Append("{4}");
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString()
                                     , "---","PESO DEL GANADO", "---", "CABEZAS", "PROMEDIO"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);

            opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 23
            };
            SB = new StringBuilder();
            SB.Append("{0}").Append(string.Empty.PadRight(6));
            SB.Append("{1}").Append(string.Empty.PadRight(10));
            SB.Append("{2}").Append(string.Empty.PadRight(11));
            SB.Append("{3}").Append(string.Empty.PadRight(6));
            SB.Append("{4}").Append(string.Empty.PadRight(7));
            SB.Append("{5}").Append(string.Empty.PadRight(6));
            SB.Append("{6}-{7}").Append(string.Empty.PadRight(9));
            SB.Append("{8}").Append(string.Empty.PadRight(8));
            SB.Append("{9}");
            linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString()
                                    , "ORIGEN", "LLEGADA", "TOTAL", "TIPO GANADO", "PRECIO", "IMPORTE", "LOTE","CORRAL"
                                    , "PESO", "IMPORTE"),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }

        /// <summary>
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="lineas"></param>
        /// <param name="contenedorCosteoEntrada"></param>
        private void GeneraLineasDetalle(IList<LineaImpresionInfo> lineas, IList<EntradaDetalleInfo> detalles, EntradaGanadoInfo entradaGanado)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 20
            };
            LineaImpresionInfo linea;
            EntradaDetalleInfo detalle;
            for (int indexDetalle = 0; indexDetalle < detalles.Count; indexDetalle++)
			{
                detalle = detalles[indexDetalle];

                decimal pesoPromedio = decimal.Round(detalle.PesoOrigen / detalle.Cabezas, 0);
                decimal importePromedio = decimal.Round(detalle.Importe / detalle.Cabezas, 0);

                StringBuilder SB = new StringBuilder();
                SB.Append("{0}").Append(String.Empty.PadRight(6));
                SB.Append("{1}").Append(String.Empty.PadRight(6));
                SB.Append("{2}").Append(String.Empty.PadRight(6));
                SB.Append("{3}").Append(String.Empty.PadRight(6));
                SB.Append("{4}").Append(String.Empty.PadRight(6));
                SB.Append("{5}").Append(String.Empty.PadRight(6));
                SB.Append("{6}-{7}").Append(String.Empty.PadRight(6));
                SB.Append("{8}").Append(String.Empty.PadRight(6));
                SB.Append("{9}");
                linea = new LineaImpresionInfo
                {
                    Texto = String.Format(SB.ToString()
                                         , detalle.PesoOrigen, detalle.PesoLlegada, detalle.Cabezas
                                         , detalle.TipoGanado.Descripcion, detalle.PrecioKilo, detalle.Importe
                                         , entradaGanado.Lote.Lote, entradaGanado.CodigoCorral, pesoPromedio, importePromedio),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
			}
        }

        /// <summary>
        /// Genera los totales por Detalle
        /// </summary>
        /// <param name="lineas"></param>
        private void GeneraLineaTotales(IList<LineaImpresionInfo> lineas, IList<EntradaDetalleInfo> detalles)
        {
            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(NombreFuente, 10),
                MargenIzquierdo = 20
            };
            StringBuilder SB = new StringBuilder();
            SB.Append("{0}").Append(String.Empty.PadRight(5));
            SB.Append("{1}").Append(String.Empty.PadRight(5));
            SB.Append("{2}").Append(String.Empty.PadRight(10));
            SB.Append("{3}").Append(String.Empty.PadRight(5));
            SB.Append("{4}").Append(String.Empty.PadRight(10));
            SB.Append("{5}").Append(String.Empty.PadRight(5));
            SB.Append("{6}");

            decimal sumaPesoOrigen = detalles.Sum(pesoOrigen => pesoOrigen.PesoOrigen);
            decimal sumaPesoLlegada = detalles.Sum(pesoLlegada => pesoLlegada.PesoLlegada);
            int sumaCabezas = detalles.Sum(cabezas => cabezas.Cabezas);
            decimal promedioPrecio = detalles.Sum(precio => precio.PrecioKilo) / detalles.Count;
            decimal sumaImporte = detalles.Sum(importe => importe.Importe);
            decimal promedioPeso = sumaPesoOrigen / sumaCabezas;
            decimal promedioImporte = sumaImporte / sumaCabezas;
            var linea = new LineaImpresionInfo
            {
                Texto = String.Format(SB.ToString(), sumaPesoOrigen, sumaPesoLlegada, sumaCabezas, promedioPrecio, sumaImporte, promedioPeso, promedioImporte),
                Opciones = opcionesLinea
            };
            lineas.Add(linea);
        }
    }
}
