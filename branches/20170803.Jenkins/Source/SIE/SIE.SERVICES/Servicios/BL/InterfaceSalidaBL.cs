using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class InterfaceSalidaBL
    {
        /// <summary>
        ///     Metodo que crear una interfaceSalida
        /// </summary>
        /// <param name="interfaceSalidaInfo"></param>
        internal void GuardarInterfaceSalida(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaDAL = new InterfaceSalidaDAL();
                interfaceSalidaDAL.Crear(interfaceSalidaInfo);
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
        ///     Obtiene una Interface de Salida por Id
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaInfo ObtenerPorID(int salidaID, int organizacionID)
        {
            InterfaceSalidaInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaDAL = new InterfaceSalidaDAL();
                interfaceSalidaInfo = interfaceSalidaDAL.ObtenerPorID(salidaID, organizacionID);
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
            return interfaceSalidaInfo;
        }

        /// <summary>
        ///     Obtiene una Lista de Interface de Salida
        /// </summary>
        /// <returns></returns>
        internal IList<InterfaceSalidaInfo> ObtenerTodos()
        {
            IList<InterfaceSalidaInfo> interfacesSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaDAL = new InterfaceSalidaDAL();
                interfacesSalidaInfo = interfaceSalidaDAL.ObtenerTodos();
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
            return interfacesSalidaInfo;
        }

        /// <summary>
        ///     Obtiene una Interface de Salida por SalidaID y OrganizacionID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorSalidaOrganizacion(EntradaGanadoInfo entradaGanado)
        {

            var entradaGanadoCosteo = new EntradaGanadoCosteoInfo();
            try
            {
                Logger.Info();
                var interfaceSalidaDAL = new InterfaceSalidaDAL();
                var interfacesSalidaInfo = interfaceSalidaDAL.ObtenerPorSalidaOrganizacion(entradaGanado);
                entradaGanadoCosteo.ListaEntradaDetalle = new List<EntradaDetalleInfo>();
                entradaGanadoCosteo.ListaCostoEntrada = new List<EntradaGanadoCostoInfo>();

                var diasEstancia = new List<double>();

                var listaEntradaGanadoCostoAuxiliar = new List<EntradaGanadoCostoInfo>();

                var tipoGanadoBL = new TipoGanadoBL();

                List<TipoGanadoInfo> tiposGanado = tipoGanadoBL.ObtenerTodos();

                if (interfacesSalidaInfo == null)
                {
                    return entradaGanadoCosteo;
                }
                interfacesSalidaInfo.ListaInterfaceDetalle.ForEach(det =>
                    {
                        var entradaGanadoDetalle = new EntradaDetalleInfo
                            {
                                TipoGanado = det.TipoGanado,
                                Cabezas = det.Cabezas,
                                Importe = det.Importe,
                                PrecioKilo = det.PrecioKG,
                                PesoOrigen = det.ListaInterfaceSalidaAnimal.Sum(ani => ani.PesoOrigen),
                                ListaTiposGanado = tiposGanado,
                                FechaSalidaInterface = interfacesSalidaInfo.FechaSalida,
                            };
                        entradaGanadoCosteo.ListaEntradaDetalle.Add(entradaGanadoDetalle);
                        if (det.ListaInterfaceSalidaAnimal != null)
                        {
                            det.ListaInterfaceSalidaAnimal.ForEach(animal =>
                                {
                                    if (animal.ListaSalidaCostos != null)
                                    {
                                        var listaCostosAnimal = (from costo in animal.ListaSalidaCostos
                                                                
                                                                 select new EntradaGanadoCostoInfo
                                                                     {
                                                                         Costo = costo.Costo,
                                                                         Importe = costo.Importe,
                                                                         TieneCuenta = true,
                                                                         //CuentaProvision = claveContable.Valor,
                                                                         //DescripcionCuenta = claveContable.Descripcion
                                                                     });
                                        listaEntradaGanadoCostoAuxiliar.AddRange(listaCostosAnimal);

                                        var span = interfacesSalidaInfo.FechaSalida - animal.FechaCompra;
                                        diasEstancia.Add(span.TotalDays);
                                    }
                                });
                        }
                    });

                var costosAgrupados = (from costos in listaEntradaGanadoCostoAuxiliar
                                       group costos by costos.Costo.CostoID
                                           into costoAgrupado
                                           let interfaceSalidaCostoInfo = costoAgrupado.FirstOrDefault()
                                           let claveContable =
                                                                    ObtenerCuentaInventario(interfaceSalidaCostoInfo.Costo,
                                                                                            entradaGanado.
                                                                                                OrganizacionOrigenID, ClaveCuenta.CuentaInventarioTransito)
                                           where interfaceSalidaCostoInfo != null
                                           select new EntradaGanadoCostoInfo
                                           {
                                               Costo = interfaceSalidaCostoInfo.Costo,
                                               TieneCuenta = true,
                                               Importe = costoAgrupado.Sum(cos => cos.Importe),
                                               CuentaProvision = claveContable.Valor,
                                               DescripcionCuenta = claveContable.Descripcion,
                                               Origen = true
                                           }).ToList();

                entradaGanadoCosteo.ListaCostoEntrada.AddRange(costosAgrupados);
                entradaGanadoCosteo.DiasEstancia = Convert.ToInt32(diasEstancia.Average());
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
            return entradaGanadoCosteo;
        }

        internal ClaveContableInfo ObtenerCuentaInventario(CostoInfo costo, int organizacionID, ClaveCuenta claveCuenta)
        {
            var cuentaBL = new CuentaBL();
            var claveCuentaString = Auxiliar.Auxiliar.ObtenerClaveCuenta(claveCuenta);
            var claveContable = cuentaBL.ObtenerPorClaveCuentaOrganizacion(claveCuentaString, organizacionID);
            if (claveContable != null)
            {
                claveContable.Valor = string.Format("{0}{1}", claveContable.Valor, costo.ClaveContable);
            }
            else
            {
                claveContable = new ClaveContableInfo();
            }
            return claveContable;
        }

        internal ClaveContableInfo ObtenerCuentaInventario(int organizacionID, ClaveCuenta claveCuenta)
        {
            var cuentaBL = new CuentaBL();
            var claveCuentaString = Auxiliar.Auxiliar.ObtenerClaveCuenta(claveCuenta);
            var claveContable = cuentaBL.ObtenerPorClaveCuentaOrganizacion(claveCuentaString, organizacionID);
            if (claveContable != null)
            {
                claveContable.Valor = string.Format("{0}", claveContable.Valor);
            }
            else
            {
                claveContable = new ClaveContableInfo();
            }
            return claveContable;
        }

        internal Dictionary<int, decimal> ObtenerCostoFleteProrrateado(EntradaGanadoInfo entradaGanado, List<EntradaGanadoCostoInfo> listaCostosEmbarque, List<EntradaGanadoInfo> listaCompraDirecta)
        {
            var pesosOrigen = new Dictionary<int, decimal>();
            var importesProrrateado = new Dictionary<int, decimal>();

            var interfaceSalidaDAL = new InterfaceSalidaDAL();
            List<InterfaceSalidaInfo>  listaInterfaceSalida = null;

            listaInterfaceSalida = listaCompraDirecta.Any() ? 
                interfaceSalidaDAL.ObtenerPorEmbarqueIDConCompraDirecta(entradaGanado) : 
                interfaceSalidaDAL.ObtenerPorEmbarqueID(entradaGanado);
            

            if(listaInterfaceSalida == null)
            {
                return null;
            }

            entradaGanado.ListaInterfaceSalida = listaInterfaceSalida;

            var detalles = listaInterfaceSalida.SelectMany(isa => isa.ListaInterfaceDetalle).SelectMany(isd => isd.ListaInterfaceSalidaAnimal);

            var pesosDetalles = (from isa in detalles
                                 group isa by isa.SalidaID
                                     into agrupado
                                     select new
                                         {
                                             SalidaID = agrupado.Key,
                                             PesoOrigen = agrupado.Sum(animal => animal.PesoCompra)
                                         }).ToList();

            pesosDetalles.ForEach(pesos => pesosOrigen.Add(pesos.SalidaID, pesos.PesoOrigen));

            if(!pesosDetalles.Any())
            {
                return null;
            }

            decimal pesoTotalOrigen = pesosDetalles.Sum(pes => pes.PesoOrigen);
            decimal pesoEscala = 0;

            if (entradaGanado.TipoOrigen != (int) TipoOrganizacion.CompraDirecta)
            {
                pesoEscala = pesosOrigen[entradaGanado.FolioOrigen];

                if (listaCompraDirecta.Any())
                {
                    //Obtener Los pesos de las compras directas ya costeadas
                    var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                    var entradaGanadoCosteoInfo = new EntradaGanadoCosteoInfo();
                    decimal pesoTotalOrigenCompraDirecta = 0;

                    foreach (var compraDirecta in listaCompraDirecta)
                    {
                        pesoTotalOrigenCompraDirecta = 0;
                        entradaGanadoCosteoInfo = entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(compraDirecta.EntradaGanadoID);
                        if (entradaGanadoCosteoInfo != null && entradaGanadoCosteoInfo.ListaEntradaDetalle != null &&
                            entradaGanadoCosteoInfo.ListaEntradaDetalle.Any())
                        {
                            pesoTotalOrigenCompraDirecta =
                                (from entradaDetalle in entradaGanadoCosteoInfo.ListaEntradaDetalle
                                    select entradaDetalle.PesoOrigen)
                                    .Sum();
                        }
                        pesoTotalOrigen = pesoTotalOrigen + pesoTotalOrigenCompraDirecta;
                    }
                }
            }

            listaCostosEmbarque.ForEach(costos =>
                {
                    var costoUnitarioKilo = costos.Importe / pesoTotalOrigen;
                    ////TODO:Con el peso escala cuando es compra directa
                    decimal costoFlete = Math.Round(costoUnitarioKilo * pesoEscala, 2);

                    importesProrrateado.Add(costos.Costo.CostoID, costoFlete);
                });

            return importesProrrateado;
        }

        /// <summary>
        ///     Obtiene una Interface de Salida por Id
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal int ObtenerPorEmbarque(int salidaID, int organizacionID, int organizacionOrigenID)
        {
            int resultado;
            try
            {
                Logger.Info();
                var interfaceSalidaDAL = new InterfaceSalidaDAL();
                resultado = interfaceSalidaDAL.ObtenerPorEmbarque(salidaID, organizacionID, organizacionOrigenID);
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
            return resultado;
        }
    }
}
