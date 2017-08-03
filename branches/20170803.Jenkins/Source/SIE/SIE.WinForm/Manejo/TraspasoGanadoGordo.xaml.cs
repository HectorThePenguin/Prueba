using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;
using System.Collections.ObjectModel;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TraspasoGanadoGordo.xaml
    /// </summary>
    public partial class TraspasoGanadoGordo
    {
        #region VARIABLES
        private SerialPortManager spManager;
        private ObservableCollection<InterfaceSalidaTraspasoDetalleInfo> listadoDeTraspaso;

        private InterfaceSalidaTraspasoInfo Contexto
        {
            get { return (InterfaceSalidaTraspasoInfo)DataContext; }
            set { DataContext = value; }
        }

        #endregion VARIABLES

        #region CONTRUCTORES

        public TraspasoGanadoGordo()
        {
            InitializeComponent();
            Inicializar();
            ObtenerTipoGanado();
            ObtenerFormulas();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        public void Inicializar()
        {
            listadoDeTraspaso = new ObservableCollection<InterfaceSalidaTraspasoDetalleInfo>();
            GridOrganizacionesTraspaso.ItemsSource = listadoDeTraspaso;
            BtnAgregar.IsEnabled = true;
            TxtCorral.IsEnabled = true;
            skAyudaOrganizacion.IsEnabled = true;
            TxtFolioTraspaso.IsEnabled = true;
            Habilitar(false);
            InicializaContexto();
        }

        private void InicializaContexto()
        {
            var fechaPl = new FechaPL();
            var fecha = fechaPl.ObtenerFechaActual();
            Contexto = new InterfaceSalidaTraspasoInfo
            {
                FechaEnvio = fecha.FechaActual,

                OrganizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario(),
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                LoteInfo = new LoteInfo(),
                CorralInfo = new CorralInfo(),
                LoteProyecion = new LoteProyeccionInfo(),
                TipoGanado = new TipoGanadoInfo(),
                ListaInterfaceSalidaTraspasoDetalle = new List<InterfaceSalidaTraspasoDetalleInfo>()
            };
            if (Contexto.OrganizacionDestino == null || Contexto.OrganizacionDestino.OrganizacionID == 0)
            {
                Contexto.OrganizacionDestino = new OrganizacionInfo
                                                   {
                                                       TipoOrganizacion = new TipoOrganizacionInfo
                                                                              {
                                                                                  TipoOrganizacionID =
                                                                                      TipoOrganizacion.Ganadera.
                                                                                      GetHashCode()
                                                                              }
                                                   };
            }

            skAyudaOrganizacion.AsignarFoco();
        }

        private void CargarInterfaceSalidaTraspaso()
        {
            try
            {
                var interfaceSalidaTraspasoPl = new InterfaceSalidaTraspasoPL();

                Contexto = interfaceSalidaTraspasoPl.ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(Contexto);
                if (Contexto == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoGordo_MensajeFolioInvalido,
                            MessageBoxButton.OK, MessageImage.Warning);
                    Inicializar();
                    TxtFolioTraspaso.Focus();
                    return;
                }
                listadoDeTraspaso = new ObservableCollection<InterfaceSalidaTraspasoDetalleInfo>(Contexto.ListaInterfaceSalidaTraspasoDetalle);
                GridOrganizacionesTraspaso.ItemsSource = listadoDeTraspaso;

                Contexto.CabezasEnvio = 0;
                skAyudaOrganizacion.IsEnabled = false;
                TxtFolioTraspaso.IsEnabled = false;
                Contexto.CorralInfo = new CorralInfo();
                Contexto.LoteInfo = new LoteInfo();
                Contexto.LoteProyecion = new LoteProyeccionInfo();
                Contexto.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                Contexto.OrganizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                Contexto.TipoGanado = new TipoGanadoInfo();
                HabilitarControlesPesos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoGordo_MensajeErrorAlConsultarFolio,
                            MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void HabilitarControlesPesos()
        {
            if (Contexto.PesoTara != 0 || Contexto.PesoBruto != 0)
            {
                TxtCorral.IsEnabled = false;
                BtnAgregar.IsEnabled = false;
                btnCapturarPesoTara.IsEnabled = true;
                btnCapturarPesoBruto.IsEnabled = true;
                if (Contexto.PesoTara != 0)
                {
                    btnCapturarPesoTara.IsEnabled = false;
                }
                if (Contexto.PesoBruto != 0)
                {
                    btnCapturarPesoBruto.IsEnabled = false;
                }
            }
            if (Contexto.InterfaceSalidaTraspasoId == 0 && Contexto.PesoTara == 0)
            {
                btnCapturarPesoTara.IsEnabled = true;
                btnCapturarPesoBruto.IsEnabled = false;
            }
        }

        private void Habilitar(bool activar)
        {
            //TxtFolioTraspaso.IsEnabled = activar;
            TxtLote.IsEnabled = activar;
            CboSexo.IsEnabled = activar;
            CboTipoGanado.IsEnabled = activar;
            TxtPesoProyectado.IsEnabled = activar;
            TxtGananciaDiaria.IsEnabled = activar;
            TxtDiasEngorda.IsEnabled = activar;
            CboFormula.IsEnabled = activar;
            TxtDiasFormula.IsEnabled = activar;
            TxtCabezas.IsEnabled = activar;
            chkTraspasoGanado.IsEnabled = activar;
            chkSacrificioGanado.IsEnabled = activar;
        }

        private void ObtenerFormulas()
        {
            var formulaPl = new FormulaPL();
            IList<FormulaInfo> formulas = formulaPl.ObtenerTodos();
            if (formulas == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeNoSeEncontraronRegistrosFormula,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            CboFormula.ItemsSource = formulas;
        }

        private void ObtenerTipoGanado()
        {
            var tipoGanadoPl = new TipoGanadoPL();
            IList<TipoGanadoInfo> tipoGanado = tipoGanadoPl.ObtenerTodos();
            if (tipoGanado == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeNoSeEncontroTipoDeGanado,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            CboTipoGanado.ItemsSource = tipoGanado;
        }

        private bool ValidarCorralDentroDelGrid()
        {
            bool corralValido = true;
            if (listadoDeTraspaso == null || listadoDeTraspaso.Count == 0) return true;

            List<InterfaceSalidaTraspasoDetalleInfo> lista =
                listadoDeTraspaso.Where(t => t.Corral.CorralID.Equals(Contexto.CorralInfo.CorralID)).ToList();
            bool existeCorralDestino = lista.Count > 0;

            if (existeCorralDestino)
            {
                corralValido = false;
                skAyudaOrganizacion.AsignarFoco();
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeElCorralYaSeEncuentra,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            return corralValido;
        }

        private bool ValidarTipoTraspaso()
        {
            bool tipoTraspaso = true;
            if (listadoDeTraspaso != null && listadoDeTraspaso.Any())
            {
                var tipoTraspasoDiferenteSeleccionado = false;
                if (Contexto.TraspasoGanado)
                {
                    tipoTraspasoDiferenteSeleccionado = listadoDeTraspaso.Any(t => t.SacrificioGanado);
                }
                if (Contexto.SacrificioGanado)
                {
                    tipoTraspasoDiferenteSeleccionado = listadoDeTraspaso.Any(t => t.TraspasoGanado);
                }

                if (tipoTraspasoDiferenteSeleccionado)
                {
                    tipoTraspaso = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.TraspasoGanadoGordo_MensajeTipoTraspasoDiferenteSeleccionado,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            return tipoTraspaso;
        }

        private bool ValidaCabezasLote()
        {
            var cabezasValidas = true;
            if (listadoDeTraspaso != null && listadoDeTraspaso.Any())
            {
                List<InterfaceSalidaTraspasoDetalleInfo> lista =
                    listadoDeTraspaso.Where(
                        t => t.Corral.CorralID.Equals(Contexto.CorralInfo.CorralID) && !t.Registrado).ToList();
                if (lista.Any())
                {
                    int cabezasCapturadas = lista.Sum(cab => cab.Cabezas);
                    if ((cabezasCapturadas + Contexto.CabezasEnvio) > Contexto.LoteInfo.Cabezas)
                    {
                        cabezasValidas = false;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.
                                              TraspasoGanadoGordo_MensajeTipoTraspasoCabezasInsuficiantes,
                                          MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
            }
            return cabezasValidas;
        }

        private bool ValidarCamposObligatorios()
        {
            if (Contexto.OrganizacionDestino == null || Contexto.OrganizacionDestino.OrganizacionID == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeSeleccionarDestino,
                    MessageBoxButton.OK, MessageImage.Warning);
                skAyudaOrganizacion.AsignarFoco();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Contexto.CorralInfo.Codigo))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarCampoCorral,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtCorral.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Contexto.LoteInfo.Lote))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarCampoLote,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtLote.Focus();
                return false;
            }

            if (CboSexo.SelectedIndex == -1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeSeleccionarCampoSexo,
                    MessageBoxButton.OK, MessageImage.Warning);
                CboSexo.Focus();
                return false;
            }

            if (CboTipoGanado.SelectedIndex == -1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeSeleccionarTipoGanado,
                    MessageBoxButton.OK, MessageImage.Warning);
                CboTipoGanado.Focus();
                return false;
            }

            if (Contexto.TipoGanado == null || Contexto.TipoGanado.PesoSalida == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarPesoProyectado,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtPesoProyectado.Focus();
                return false;
            }

            if (Contexto.LoteProyecion == null || Contexto.LoteProyecion.GananciaDiaria == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarGananciaDiaria,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtGananciaDiaria.Focus();
                return false;
            }

            if (Contexto.DiasEngorda == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarDiasEngorda,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtDiasEngorda.Focus();
                return false;
            }

            if (CboFormula.SelectedIndex == -1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeSeleccionarCampoFormulas,
                    MessageBoxButton.OK, MessageImage.Warning);
                CboFormula.Focus();
                return false;
            }

            if (Contexto.DiasFormula == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarDiasFormula,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtCabezas.Focus();
                return false;
            }

            if (Contexto.LoteInfo.Cabezas == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeDebeIngresarCabezas,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtCabezas.Focus();
                return false;
            }

            if (!Contexto.TraspasoGanado && !Contexto.SacrificioGanado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeSeleccionarTipoTraspaso,
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            if (Contexto.CabezasEnvio == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeCabezasEnvioIgualCero,
                    MessageBoxButton.OK, MessageImage.Warning);
                TxtCabezasEnvio.Focus();
                return false;
            }

            if (Contexto.CabezasEnvio > Contexto.LoteInfo.Cabezas)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.TraspasoGanadoGordo_MensajeCabezasEnvioMayorCabezas,
                    MessageBoxButton.OK, MessageImage.Warning);
                Contexto.CabezasEnvio = 0;
                TxtCabezasEnvio.Focus();
                return false;
            }
            return true;
        }

        private void ObtenerDiasEngorda(IEnumerable<AnimalInfo> animales)
        {
            var entradaGanadoPL = new EntradaGanadoPL();
            List<EntradaGanadoInfo> entradas = animales.Select(x => new EntradaGanadoInfo
            {
                FolioOrigen = x.FolioEntrada,
                OrganizacionID = Contexto.OrganizacionId
            }).ToList();
            entradas = entradaGanadoPL.ObtenerEntradasPorFolioOrigenXML(entradas);
            if (entradas != null && entradas.Any())
            {
                var count = entradas.Count;
                double temp = 0D;
                for (int i = 0; i < count; i++)
                {
                    temp += entradas[i].FechaEntrada.Ticks / (double)count;
                }
                var fechaPromedio = new TimeSpan((long)temp);
                var fechaActual = new TimeSpan(DateTime.Now.Ticks);
                Contexto.DiasEngorda = (fechaActual - fechaPromedio).Days;
            }
        }

        private void ObtenerFormula()
        {
            var repartoPL = new RepartoPL();
            List<RepartoInfo> repartos = repartoPL.ObtenerPorCorralOrganizacion(Contexto.CorralInfo.CorralID,
                                                                              Contexto.OrganizacionId);
            if (repartos != null && repartos.Any())
            {
                repartos = repartos.OrderByDescending(fecha => fecha.Fecha).ToList();
                Contexto.Formula = repartos.Select(x => new FormulaInfo
                {
                    FormulaId =
                        x.DetalleReparto.Select(
                            formula => formula.FormulaIDServida).
                        FirstOrDefault()
                }).FirstOrDefault();
                var detallesRepartoFecha = repartos.Select(x => new
                {
                    x.Fecha,
                    Formula =
                x.DetalleReparto.Select(
                    formula => formula.FormulaIDServida).
                FirstOrDefault()
                }).ToList();
                int diasFormula = 0;
                if (detallesRepartoFecha.Count > 1)
                {
                    diasFormula = ObtenerDiasFormula(detallesRepartoFecha, 1, 1);
                }
                var repartosZilmax = repartos.Select(x => new
                {
                    x.Fecha,
                    Formula =
                x.DetalleReparto.Select(
                    formula => formula.TipoFormula == TipoFormula.Finalizacion.GetHashCode()).
                FirstOrDefault()
                }).ToList();
                if (repartosZilmax.Any())
                {
                    var repartosZilmaxValidos = repartosZilmax.Where(zil => zil.Formula);
                    if (repartosZilmaxValidos.Any())
                    {
                        int diasZilmax = ObtenerDiasFormula(repartosZilmaxValidos.ToList(), 1, 1);
                        Contexto.DiasZilmax = diasZilmax;
                    }
                    else
                    {
                        Contexto.DiasZilmax = 0;
                    }
                }
                Contexto.DiasFormula = diasFormula;
            }
        }

        private int ObtenerDiasFormula(dynamic detalles, int indice, int cantidadDias)
        {
            if (indice == detalles.Count)
            {
                return cantidadDias;
            }
            if (detalles[indice - 1].Formula == detalles[indice].Formula)
            {
                if (detalles[indice - 1].Fecha != detalles[indice].Fecha)
                {
                    cantidadDias++;
                }
                indice++;
                cantidadDias = ObtenerDiasFormula(detalles, indice, cantidadDias);
            }
            return cantidadDias;
        }

        private void ObtenerValoresGanado(List<AnimalInfo> animales)
        {
            if (animales != null && animales.Any())
            {
                var ganado = animales.GroupBy(tipo => tipo.TipoGanado.TipoGanadoID)
                                     .Select(grupo => new
                                     {
                                         TipoGanadoID = grupo.Key,
                                         Cantidad = grupo.Count(),
                                         Sexo = grupo.Select(sexo => sexo.TipoGanado.Sexo).FirstOrDefault()
                                     }).ToList();
                int hembras = ganado.Where(sexo => sexo.Sexo == Sexo.Hembra).Sum(sexo => sexo.Cantidad);
                int machos = ganado.Where(sexo => sexo.Sexo == Sexo.Macho).Sum(sexo => sexo.Cantidad);
                var sexoGanado = Sexo.Hembra;
                if (hembras == machos || machos > hembras)
                {
                    sexoGanado = Sexo.Macho;
                }
                Contexto.Sexo = sexoGanado;
                Contexto.TipoGanado = ganado.GroupBy(tipo => tipo.TipoGanadoID)
                    .Select(grupo => new
                    {
                        TipoGanadoID = grupo.Key,
                        Totales = grupo.Sum(tipo => tipo.Cantidad)
                    }).OrderByDescending(tot => tot.Totales)
                    .Select(tipo => new TipoGanadoInfo
                    {
                        TipoGanadoID = tipo.TipoGanadoID
                    }).FirstOrDefault();
            }
        }

        private void ObtenerPesos()
        {
            var loteProyeccionPL = new LoteProyeccionPL();
            Contexto.LoteProyecion = loteProyeccionPL.ObtenerPorLote(Contexto.LoteInfo);
        }

        private bool ValidarCorral(IEnumerable<LoteInfo> lotes)
        {
            var valido = true;
            var loteInfos = lotes as LoteInfo[] ?? lotes.ToArray();
            if (lotes == null || !loteInfos.Any())
            {
                valido = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeCorralInvalido,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            if (loteInfos.All(activo => activo.Activo != EstatusEnum.Activo))
            {
                valido = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeCorralSinLoteActivo,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            if (loteInfos.Any(prod => prod.Corral.TipoCorral.TipoCorralID != TipoCorral.Produccion.GetHashCode()))
            {
                valido = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeCorralNoProduccion,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            return valido;
        }

        private void BuscarFolioTraspaso(object sender, EventArgs e)
        {
            try
            {
                if (Contexto.OrganizacionId == Contexto.OrganizacionDestino.OrganizacionID)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.TraspasoGanadoGordo_MensajeMismaOrganizacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaOrganizacion.LimpiarCampos();
                    skAyudaOrganizacion.AsignarFoco();
                    return;
                }
                TxtFolioTraspaso.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeErrorConsultarFolioTraspaso,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void RadioButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contexto.DiasFormula == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                           Properties.Resources.TraspasoGanadoGordo_MensajeSinDiasFormula,
                                           MessageBoxButton.OK, MessageImage.Warning);
                    Contexto.TraspasoGanado = false;
                    Contexto.SacrificioGanado = false;
                    return;
                }

                if (Contexto.TraspasoGanado)
                {
                    if (Contexto.Formula.TipoFormula.TipoFormulaID == TipoFormula.Finalizacion.GetHashCode())
                    {
                        Contexto.TraspasoGanado = false;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.TraspasoGanadoGordo_MensajeFormulaFinalizacionEnTraspaso,
                                          MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                if (Contexto.SacrificioGanado)
                {
                    if (Contexto.DiasZilmax < 25)
                    {
                        Contexto.SacrificioGanado = false;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.TraspasoGanadoGordo_MensajeDiasFormula,
                                          MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoGordo_MensajeErrorTraspaso,
                            MessageBoxButton.OK, MessageImage.Warning);
            }

        }

        #endregion METODOS

        #region EVENTOS

        private void TraspasoGanadoGordo_Loaded(object sender, RoutedEventArgs e)
        {
            InicializarBascula();
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaOrganizacion.AyudaConDatos += BuscarFolioTraspaso;
            GridOrganizacionesTraspaso.ItemsSource = null;
            if (listadoDeTraspaso == null)
            {
                listadoDeTraspaso = new ObservableCollection<InterfaceSalidaTraspasoDetalleInfo>();
            }
            GridOrganizacionesTraspaso.ItemsSource = listadoDeTraspaso;
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.MensajeCancelarPantalla,
               MessageBoxButton.YesNo, MessageImage.Question) == MessageBoxResult.Yes)
            {
                Inicializar();
                //InicializarGrid();
            }
        }

        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarCamposObligatorios())
                {
                    if (ValidarCorralDentroDelGrid())
                    {
                        if (ValidarTipoTraspaso())
                        {
                            if (ValidaCabezasLote())
                            {
                                if (GridOrganizacionesTraspaso.Items.Count == 0)
                                {
                                    GridOrganizacionesTraspaso.ItemsSource = listadoDeTraspaso;
                                }
                                var detalle = new InterfaceSalidaTraspasoDetalleInfo
                                               {
                                                   Lote = Contexto.LoteInfo,
                                                   Corral = Contexto.CorralInfo,
                                                   TipoGanado = Contexto.TipoGanado,
                                                   PesoProyectado = Contexto.TipoGanado.PesoSalida,
                                                   GananciaDiaria = Contexto.LoteProyecion.GananciaDiaria,
                                                   DiasEngorda = Contexto.DiasEngorda,
                                                   Formula = Contexto.Formula,
                                                   DiasFormula = Contexto.DiasFormula,
                                                   Cabezas = Contexto.CabezasEnvio,
                                                   Registrado = false,
                                                   OrganizacionDestino = Contexto.OrganizacionDestino,
                                                   TraspasoGanado = Contexto.TraspasoGanado,
                                                   SacrificioGanado = Contexto.SacrificioGanado,
                                                   UsuarioCreacionID = Contexto.UsuarioModificacionID == null ? Contexto.UsuarioCreacionID : Contexto.UsuarioModificacionID.Value
                                               };
                                var item = Extensor.ClonarInfo(detalle) as InterfaceSalidaTraspasoDetalleInfo;
                                listadoDeTraspaso.Add(item);
                                
                                Habilitar(false);

                                Contexto.LoteInfo = new LoteInfo();
                                Contexto.CorralInfo = new CorralInfo();
                                Contexto.LoteProyecion = new LoteProyeccionInfo();
                                Contexto.TipoGanado = new TipoGanadoInfo();
                                Contexto.Formula = new FormulaInfo();
                                Contexto.DiasEngorda = 0;
                                Contexto.DiasFormula = 0;
                                Contexto.CabezasEnvio = 0;
                                skAyudaOrganizacion.IsEnabled = false;
                                TxtCorral.Focus();
                                //Inicializar();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TraspasoGanadoGordo_MensajeErrorAlAgregarOrganizacion,
                            MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            Inicializar();
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listadoDeTraspaso.Count > 0)
                {
                    var interfazSalidaTraspasoPl = new InterfaceSalidaTraspasoPL();

                    Contexto.CabezasEnvio = listadoDeTraspaso.Sum(det => det.Cabezas);
                    var listadoNoRegistrados = listadoDeTraspaso.Where(t => t.Registrado == false).ToList();

                    Contexto.ListaInterfaceSalidaTraspasoDetalle = listadoNoRegistrados.ToList();
                    Dictionary<long, decimal> resultado =
                        interfazSalidaTraspasoPl.GuardarListadoInterfaceSalidaTraspaso(Contexto);

                    long folio = 0;
                    string costo = string.Empty;
                    if (resultado != null)
                    {
                        KeyValuePair<long, decimal> datos = resultado.First();
                        folio = datos.Key;
                        costo = datos.Value.ToString("C2");
                    }

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.TraspasoGanadoGordo_MensajeGuardadoOk,
                                                    folio, costo),
                                      MessageBoxButton.OK, MessageImage.Correct);
                    Inicializar();
                    listadoDeTraspaso = new ObservableCollection<InterfaceSalidaTraspasoDetalleInfo>();
                    GridOrganizacionesTraspaso.ItemsSource = null;
                    txtDisplay.Value = 0;
                    HabilitarControlesPesos();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TraspasoGanadoGordo_MensajeNoSeAgregaronDatos,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.InnerException.Message
                                , MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TraspasoGanadoGordo_MensajeErrorAlGuardar,
                        MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtCorralLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Contexto.CorralInfo.Codigo))
                {
                    var lotePL = new LotePL();
                    IEnumerable<LoteInfo> lotes =
                        lotePL.ObtenerPorCodigoCorralOrganizacionID(Contexto.OrganizacionId, Contexto.CorralInfo.Codigo);
                    bool valido = ValidarCorral(lotes);
                    if (valido)
                    {
                        var animalPL = new AnimalPL();
                        LoteInfo lote = lotes.ToList().FirstOrDefault(activo => activo.Activo == EstatusEnum.Activo);
                        if (lote != null)
                        {
                            List<AnimalInfo> animales =
                                animalPL.ObtenerAnimalesPorLote(Contexto.OrganizacionId, lote.LoteID);
                            Contexto.LoteInfo = lote;
                            Contexto.CorralInfo = lote.Corral;
                            ObtenerValoresGanado(animales);
                            ObtenerPesos();
                            ObtenerFormula();
                            ObtenerDiasEngorda(animales);
                        }
                        if (!listadoDeTraspaso.Any())
                        {
                            chkTraspasoGanado.IsEnabled = true;
                            chkSacrificioGanado.IsEnabled = true;
                        }
                    }
                    else
                    {
                        if (Contexto.LoteInfo.Corral == null)
                        {
                            Contexto.LoteInfo.Corral = new CorralInfo();
                        }
                        Contexto.LoteInfo.Corral.Codigo = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeErrorConsultarCorral,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS

        /// <summary>
        /// Metodo que se Ejecuta Cuando Se Captura Algun Peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CambioPeso(object sender, TextChangedEventArgs e)
        {
            decimal pesoBruto;
            decimal pesoTara;

            decimal.TryParse(txtPesoBruto.Text, out pesoBruto);
            decimal.TryParse(txtPesoTara.Text, out pesoTara);

            txtPesoNeto.Text = string.Format("{0:#,#}", pesoBruto - pesoTara);
        }

        /// <summary>
        /// Metodo para Capturar Peso Bruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapturarPesoBruto_Click(object sender, RoutedEventArgs e)
        {
            CapturaPeso(txtPesoBruto);
        }

        /// <summary>
        /// Metodo Para Capturar Peso Tara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapturarPesoTara_Click(object sender, RoutedEventArgs e)
        {
            CapturaPeso(txtPesoTara);
        }

        /// <summary>
        /// Valida si el Peso es Valido
        /// para ser Capturado
        /// </summary>
        /// <param name="textPeso"></param>
        private void CapturaPeso(TextBox textPeso)
        {

            decimal peso;
            decimal.TryParse(txtDisplay.Text, out peso);

            if (peso > 0)
            {
                textPeso.Text = peso.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_PesoValido, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (btnCapturarPesoBruto.IsEnabled)
                {
                    btnCapturarPesoBruto.Focus();
                }
                else
                {
                    if (btnCapturarPesoTara.IsEnabled)
                    {
                        btnCapturarPesoTara.Focus();
                    }
                }
                e.Handled = true;
            }
        }

        private void TxtFolioTraspaso_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtFolioTraspaso.Value.HasValue && TxtFolioTraspaso.Value.Value != 0)
                {
                    CargarInterfaceSalidaTraspaso();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoGanadoGordo_MensajeErrorConsultarCorral,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #region Bascula
        /// <summary>
        /// Método que inicializa los datos de la bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                spManager = new SerialPortManager();
                spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);

                if (spManager != null)
                {

                    spManager.StartListening(AuxConfiguracion.ObtenerConfiguracion().PuertoBascula,
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaBaudrate),
                        ObtenerParidad(AuxConfiguracion.ObtenerConfiguracion().BasculaParidad),
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaDataBits),
                        ObtenerStopBits(AuxConfiguracion.ObtenerConfiguracion().BasculaBitStop));

                    //basculaConectada = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaMateriaPrima_MsgErrorBascula,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Cambia la variable string en una entidad Parity
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private Parity ObtenerParidad(string parametro)
        {
            Parity paridad;

            switch (parametro)
            {
                case "Even":
                    paridad = Parity.Even;
                    break;
                case "Mark":
                    paridad = Parity.Mark;
                    break;
                case "None":
                    paridad = Parity.None;
                    break;
                case "Odd":
                    paridad = Parity.Odd;
                    break;
                case "Space":
                    paridad = Parity.Space;
                    break;
                default:
                    paridad = Parity.None;
                    break;
            }
            return paridad;
        }

        /// <summary>
        /// Cambia la variable string en una entidad StopBit
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private StopBits ObtenerStopBits(string parametro)
        {
            StopBits stopBit;

            switch (parametro)
            {
                case "None":
                    stopBit = StopBits.None;
                    break;
                case "One":
                    stopBit = StopBits.One;
                    break;
                case "OnePointFive":
                    stopBit = StopBits.OnePointFive;
                    break;
                case "Two":
                    stopBit = StopBits.Two;
                    break;
                default:
                    stopBit = StopBits.One;
                    break;
            }
            return stopBit;
        }
        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            string strEnd = "", peso = "";
            double val;
            try
            {
                strEnd = spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    peso = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txtDisplay.Text = peso.ToString(CultureInfo.InvariantCulture);
                    }), null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion
    }
}
