using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionReimplante.xaml
    /// </summary>
    public partial class ConfiguracionReimplante
    {
        #region PROPIEDADES
        public ConfiguracionReimplanteModel Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ConfiguracionReimplanteModel)DataContext;
            }
            set { DataContext = value; }
        }

        private bool modificacionCodigo;
        

        private void InicializaContexto()
        {
            Contexto = new ConfiguracionReimplanteModel
                           {
                               Corral = new CorralInfo
                                            {
                                                Organizacion = new OrganizacionInfo
                                                                   {
                                                                       OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                                   }
                                            },
                               Lote = new LoteInfo(),
                               TipoGanado = new TipoGanadoInfo(),
                               LoteProyeccion = new LoteProyeccionInfo
                                                    {
                                                        ListaReimplantes = new List<LoteReimplanteInfo>()
                                                    },
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                           };
        }

        #endregion PROPIEDADES

        #region CONSTRUCTOR
        public ConfiguracionReimplante()
        {
            InitializeComponent();
            InicializaContexto();
        }
        #endregion CONSTRUCTOR

        #region EVENTOS

        private void ConfiguracionReimplante_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConfiguracionReimplante_ErrorValoresIniciales, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnAgregarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int ultimoNumeroReimplante = 0;

                if (Contexto.LoteProyeccion != null && Contexto.LoteProyeccion.ListaReimplantes != null && Contexto.LoteProyeccion.ListaReimplantes.Any())
                {
                    LoteReimplanteInfo ultimoReimplante = Contexto.LoteProyeccion.ListaReimplantes.Last();
                    ultimoNumeroReimplante = ultimoReimplante.NumeroReimplante;
                }
                else
                {
                    if(Contexto.LoteProyeccion == null)
                    {
                        Contexto.LoteProyeccion = new LoteProyeccionInfo();
                    }
                    if(Contexto.LoteProyeccion.ListaReimplantes == null)
                    {
                        Contexto.LoteProyeccion.ListaReimplantes = new List<LoteReimplanteInfo>();
                    }
                }
                var reimplanteAgregar = new LoteReimplanteInfo
                                                           {
                                                               NumeroReimplante = ultimoNumeroReimplante + 1,
                                                               FechaProyectada = DateTime.Now,
                                                               PesoProyectado = 0,
                                                               PermiteEditar = true
                                                           };
                Contexto.LoteProyeccion.ListaReimplantes.Add(reimplanteAgregar);
                ActualizarGrid();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConfiguracionReimplante_ErrorAgregarReimplante, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnCancelarAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            if (Contexto.LoteProyeccion != null && Contexto.LoteProyeccion.ListaReimplantes != null && Contexto.LoteProyeccion.ListaReimplantes.Any())
            {
                LoteReimplanteInfo reimplanteCancelar = Contexto.LoteProyeccion.ListaReimplantes.Last();
                if (reimplanteCancelar.LoteReimplanteID == 0)
                {
                    Contexto.LoteProyeccion.ListaReimplantes.Remove(reimplanteCancelar);
                    ActualizarGrid();
                }
            }
        }

        private void BtnGuardarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarGuardar())
                {
                    Guardar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConfiguracionReimplante_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnCancelarClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
             Properties.Resources.MensajeCancelarPantalla,
             MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }
        private void TxtCorral_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab || e.Key == Key.Enter)
                {
                    var corralPL = new CorralPL();
                    Contexto.Corral = corralPL.ObtenerPorCodicoOrganizacionCorral(Contexto.Corral);
                    if (Contexto.Corral == null)
                    {
                        InicializaContexto();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.ConfiguracionReimplante_CorralInvalido,
                                          MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                    Contexto.Lote.CorralID = Contexto.Corral.CorralID;
                    Contexto.Lote.OrganizacionID = Contexto.Corral.Organizacion.OrganizacionID;
                    ObtenerDatosProyeccion();
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ConfiguracionReimplante_ErrorConsultarCorral, MessageBoxButton.OK, MessageImage.Error);
            }

        }
        #endregion EVENTOS

        #region METODOS

        private void AsignarDatosControl()
        {
            int usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto.UsuarioCreacionID = usuarioID;
            var loteProyeccionPL = new LoteProyeccionPL();
            LoteProyeccionInfo loteProyeccionOriginal = loteProyeccionPL.ObtenerPorLoteCompleto(Contexto.Lote);
            if (loteProyeccionOriginal != null)
            {
                bool proyeccionIgual = Extensor.CompararObjetos(Contexto.LoteProyeccion, loteProyeccionOriginal, false);
                if (!proyeccionIgual)
                {
                    Contexto.LoteProyeccion.AplicaBitacora = true;
                }
                LoteProyeccionInfo proyeccionBitacora =
                    loteProyeccionPL.ObtenerBitacoraPorLoteProyeccionID(Contexto.LoteProyeccion.LoteProyeccionID);
                if (proyeccionBitacora == null)
                {
                    Contexto.LoteProyeccion.AplicaBitacora = true;
                }

            }
            DateTime fecha = dtpFechaDisponible.SelectedDate.HasValue ? dtpFechaDisponible.SelectedDate.Value : DateTime.MinValue;
            Contexto.LoteProyeccion.FechaSacrificio = fecha;
            Contexto.FechaDisponible = fecha;
            if (Contexto.LoteProyeccion.LoteProyeccionID == 0)
            {
                Contexto.LoteProyeccion.DiasEngorda = Contexto.TotalDias;
                Contexto.LoteProyeccion.UsuarioCreacionID = usuarioID;
            }
            else
            {
                Contexto.LoteProyeccion.UsuarioModificacionID = usuarioID;
            }
            if (Contexto.LoteProyeccion.ListaReimplantes != null && Contexto.LoteProyeccion.ListaReimplantes.Any())
            {
                Contexto.LoteProyeccion.ListaReimplantes.ToList().ForEach(reimp => reimp.UsuarioCreacionID = usuarioID);
            }
        }

        private void Guardar()
        {
            var loteProyeccionPL = new LoteProyeccionPL();
            AsignarDatosControl();
            loteProyeccionPL.GuardarConfiguracionReimplante(Contexto);
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
            Limpiar();

        }

        private void ActualizarGrid()
        {
            dgReimplantes.ItemsSource = Contexto.LoteProyeccion.ListaReimplantes.ConvertirAObservable();
        }

        private void ObtenerDatosProyeccion()
        {
            try
            {
                var lotePL = new LotePL();
                var loteProyeccionPL = new LoteProyeccionPL();
                Contexto.Lote = lotePL.ObtenerPorCorralID(Contexto.Lote);
                if (Contexto.Lote == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.ConfiguracionReimplante_LoteInvalido, MessageBoxButton.OK, MessageImage.Warning);
                    Limpiar();
                    return;
                }
                if(Contexto.Lote.FechaCierre.Year < 2000)
                {
                    Contexto.Lote.AplicaCierreLote = true;
                    //SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    //         Properties.Resources.ConfiguracionReimplante_LoteAbierto, MessageBoxButton.OK, MessageImage.Warning);
                    //Limpiar();
                    //return;
                }
                Contexto.LoteProyeccion = loteProyeccionPL.ObtenerPorLoteCompleto(Contexto.Lote);
                if (Contexto.LoteProyeccion != null && Contexto.LoteProyeccion.ListaReimplantes != null && Contexto.LoteProyeccion.ListaReimplantes.Any())
                {
                    Contexto.LoteProyeccion.ListaReimplantes.ToList().ForEach(reimp =>
                                                                                  {
                                                                                      reimp.PermiteEditar = reimp.FechaReal == DateTime.MinValue;
                                                                                  });
                    ActualizarGrid();
                }
                if(Contexto.LoteProyeccion == null)
                {
                    Contexto.LoteProyeccion = new LoteProyeccionInfo();
                    Contexto.LoteProyeccion.FechaEntradaZilmax = DateTime.Now;
                }
                ObtenerDatosCalculados();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ConfiguracionReimplante_ErrorConsultarProyeccion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void ObtenerDatosCalculados()
        {
            try
            {
                var animalBL = new AnimalPL();
                var lotePL = new LotePL();
                if (Contexto.LoteProyeccion != null)
                {
                    if(Contexto.Lote.FechaDisponibilidad.Date.Year > 2000)
                    {
                        Contexto.FechaDisponible = Contexto.Lote.FechaDisponibilidad;
                        
                    }
                    else
                    {
                        Contexto.FechaDisponible = Contexto.Lote.FechaInicio.AddDays(Contexto.LoteProyeccion.DiasEngorda);    
                    }
                }
                else
                {
                    Contexto.FechaDisponible = DateTime.Now;
                }
                modificacionCodigo = true;
                dtpFechaDisponible.SelectedDate = Contexto.FechaDisponible;
                var animalesLote = animalBL.ObtenerAnimalesPorLote(Contexto.Corral.Organizacion.OrganizacionID,
                                                                 Contexto.Lote.LoteID);


                if (animalesLote == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.ConfiguracionReimplante_LoteSinAnimales, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                var count = animalesLote.Count;
                double temp = 0D;
                for (int i = 0; i < count; i++)
                {
                    temp += animalesLote[i].FechaEntrada.Ticks / (double)count;
                }
                var average = new DateTime((long)temp);

                Contexto.PesoOrigen = Convert.ToInt32(animalesLote.Average(ani => ani.PesoCompra));
                Contexto.TipoGanado = lotePL.ObtenerTipoGanadoCorral(Contexto.Lote);
                Contexto.DiasEngorda = Convert.ToInt32((DateTime.Now - average).TotalDays);

                if(Contexto.Lote.AplicaCierreLote && Contexto.DiasEngorda < 21)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.ConfiguracionReimplante_LoteMenor21, MessageBoxButton.OK, MessageImage.Warning);
                    Limpiar();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ConfiguracionReimplante_ErrorCalcularDatos, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion METODOS

        private void DtpFechaDisponible_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (modificacionCodigo)
                {
                    modificacionCodigo = false;
                    return;
                }
                DateTime fecha = dtpFechaDisponible.SelectedDate.HasValue ? dtpFechaDisponible.SelectedDate.Value : DateTime.MinValue;

                if(fecha != DateTime.MinValue)
                {
                    if(fecha < DateTime.Now)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ConfiguracionReimplante_FechaMenor, MessageBoxButton.OK, MessageImage.Error);
                        btnGuardar.IsEnabled = false;
                        return;
                    }
                    Contexto.LoteProyeccion.FechaEntradaZilmax = fecha.AddDays(-35);
                    dtpFechaZilmax.SelectedDate = Contexto.LoteProyeccion.FechaEntradaZilmax;
                    Contexto.TotalDias = Convert.ToInt32((fecha - Contexto.Lote.FechaInicio).TotalDays);
                    btnGuardar.IsEnabled = true;
                }


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ConfiguracionReimplante_ErrorConsultarProyeccion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Limpiar()
        {
            InicializaContexto();
            ActualizarGrid();
            dtpFechaDisponible.SelectedDate = null;
        }

        private bool ValidarGuardar()
        {
            if(Contexto.LoteProyeccion != null && Contexto.LoteProyeccion.ListaReimplantes != null && Contexto.LoteProyeccion.ListaReimplantes.Any())
            {
                bool reimplanteMenorFecha = false;
                DateTime fechaAnterior = DateTime.Now;

                for (int i = 0; i < Contexto.LoteProyeccion.ListaReimplantes.Count; i++)
                {
                    if(i == 0)
                    {
                        fechaAnterior = Contexto.LoteProyeccion.ListaReimplantes[i].FechaProyectada;
                        continue;
                    }
                    if(fechaAnterior >= Contexto.LoteProyeccion.ListaReimplantes[i].FechaProyectada)
                    {
                        reimplanteMenorFecha = true;
                        break;
                    }
                }
                if(reimplanteMenorFecha)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConfiguracionReimplante_MensajeFechasReimplantes, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                if(Contexto.LoteProyeccion.ListaReimplantes.Any(reim=> reim.PesoProyectado <= 0))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConfiguracionReimplante_MensajePesoProyectadoCero, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }
            return true;
        }
    }
}
