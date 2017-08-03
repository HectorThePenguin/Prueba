using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Log;
using System.Text.RegularExpressions;
using System.Reflection;
using Xceed.Wpf.Toolkit;
using System.Globalization;


namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para MermaEsperada.xaml
    /// </summary>
    public partial class MermaEsperada
    {

        List<MermaEsperadaInfo> lsMermas;
        private int renglon = -1;
        private int organizacionOrigenId = 0;
        private int nuevo = 1;

        //private SKAyuda<OrganizacionInfo> skAyudaOrigen;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private MermaEsperadaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (MermaEsperadaInfo)DataContext;
            }
            set { DataContext = value; }
        }


        public MermaEsperada()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.MermaEsperada_PreguntaCancelar,
                                          MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializaContexto();
                InicializaDestino();
                txtMerma.Text = "0.0";
                lsMermas = new List<MermaEsperadaInfo>();
                dgMerma.ItemsSource = lsMermas;
                btnGuardar.IsEnabled = false;
                organizacionOrigenId = 0;
                skAyudaOrganizacion.AsignarFoco();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (lsMermas.Count > 0)
            {
                Guardar();
            }
        }

        private void Guardar()
        {
            try
            {
                MermaEsperadaPL mermaEsperadaPl = new MermaEsperadaPL();
                foreach (MermaEsperadaInfo merma in lsMermas)
                {
                    merma.UsuarioCreacionId = Contexto.UsuarioCreacionId;
                }

                mermaEsperadaPl.Guardar(lsMermas);
                InicializaContexto();
                InicializaDestino();
                txtMerma.Text = "0.0";
                lsMermas = new List<MermaEsperadaInfo>();
                dgMerma.ItemsSource = lsMermas;
                btnGuardar.IsEnabled = false;
                organizacionOrigenId = 0;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MermaEsperada_GuardoCorrectamente, MessageBoxButton.OK, MessageImage.Correct);
                skAyudaOrganizacion.AsignarFoco();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new MermaEsperadaInfo
            {
                UsuarioCreacionId = AuxConfiguracion.ObtenerUsuarioLogueado(),
                OrganizacionOrigen = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo
                    {
                        TipoProceso = new TipoProcesoInfo()
                    },
                    Iva = new IvaInfo
                    {
                        CuentaPagar = new CuentaInfo
                        {
                            TipoCuenta = new TipoCuentaInfo()
                        },
                        CuentaRecuperar = new CuentaInfo
                        {
                            TipoCuenta = new TipoCuentaInfo()
                        }
                    }
                },
                OrganizacionDestino = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo
                    {
                        TipoProceso = new TipoProcesoInfo()
                    },
                    Iva = new IvaInfo
                    {
                        CuentaPagar = new CuentaInfo
                        {
                            TipoCuenta = new TipoCuentaInfo()
                        },
                        CuentaRecuperar = new CuentaInfo
                        {
                            TipoCuenta = new TipoCuentaInfo()
                        }
                    }
                }
            };

            txtMerma.Text = "0.0";
            renglon = -1;
            nuevo = 1;
        }

        private void InicializaDestino()
        {
            skAyudaDestino.LimpiarCampos();
            nuevo = 1;
            renglon = -1;
        }

        private void ControlBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                lsMermas = new List<MermaEsperadaInfo>();
                InicializaContexto();
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaOrganizacion.AsignarFoco();

                skAyudaDestino.ObjetoNegocio = new OrganizacionPL();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtMerma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string valor = ((DecimalUpDown)sender).Value.HasValue
                              ? ((DecimalUpDown)sender).Value.ToString()
                              : string.Empty;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, valor);
        }

        private void btnbtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(skAyudaOrganizacion.Clave) > 0)
            {
                if (int.Parse(skAyudaDestino.Clave) > 0)
                {
                    agregar();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.MermaEsperada_OrganizacionDestino_Seleccionar,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.MermaEsperada_OrganizacionOrigen_Seleccionar,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
            }
        }

        private void agregar()
        {
            try
            {
                MermaEsperadaInfo nuevaMerma = new MermaEsperadaInfo();
                Decimal dMerma = new Decimal();
                if (lsMermas == null)
                {
                    lsMermas = new List<MermaEsperadaInfo>();
                }

                if (!(lsMermas.Any(registro => registro.OrganizacionDestino.OrganizacionID == int.Parse(skAyudaDestino.Clave) && registro.Nuevo != 3)) || (renglon >= 0 && lsMermas[renglon].OrganizacionDestino.OrganizacionID == int.Parse(skAyudaDestino.Clave)))
                {
                    if (Decimal.TryParse(txtMerma.Text, NumberStyles.Any, new CultureInfo("es-MX"), out dMerma))
                    {
                        if (dMerma < 100)
                        {
                            if (dMerma > 0)
                            {
                                if (!(lsMermas.Any(registro => registro.OrganizacionDestino.OrganizacionID == int.Parse(skAyudaDestino.Clave) && registro.Nuevo == 3)))
                                {
                                    if (renglon < 0)
                                    {
                                        nuevaMerma.OrganizacionOrigen = Contexto.OrganizacionOrigen;
                                        nuevaMerma.OrganizacionDestino = new OrganizacionInfo() { OrganizacionID = int.Parse(skAyudaDestino.Clave), Descripcion = skAyudaDestino.Descripcion };
                                        nuevaMerma.Merma = dMerma;
                                        nuevaMerma.Nuevo = nuevo;

                                        lsMermas.Add(nuevaMerma);
                                        dgMerma.ItemsSource = new List<MermaEsperadaInfo>();
                                        dgMerma.ItemsSource = lsMermas.Where(registro => registro.Nuevo != 3);
                                    }
                                    else
                                    {
                                        lsMermas[renglon].Nuevo = nuevo;
                                        lsMermas[renglon].Merma = dMerma;
                                        lsMermas[renglon].OrganizacionDestino = new OrganizacionInfo() { OrganizacionID = int.Parse(skAyudaDestino.Clave), Descripcion = skAyudaDestino.Descripcion };
                                        dgMerma.ItemsSource = new List<MermaEsperadaInfo>();
                                        dgMerma.ItemsSource = lsMermas.Where(registro => registro.Nuevo != 3);
                                    }
                                }
                                else
                                {
                                    foreach (MermaEsperadaInfo merma in lsMermas)
                                    {
                                        if (merma.OrganizacionDestino.OrganizacionID == int.Parse(skAyudaDestino.Clave))
                                        {
                                            merma.Nuevo = 2;
                                            merma.Merma = dMerma;
                                        }
                                    }
                                    dgMerma.ItemsSource = new List<MermaEsperadaInfo>();
                                    dgMerma.ItemsSource = lsMermas.Where(registro => registro.Nuevo != 3);
                                }
                                if (lsMermas.Any(registro => registro.Nuevo != 3))
                                {
                                    btnGuardar.IsEnabled = true;
                                }

                                InicializaDestino();
                                txtMerma.Text = "0.0";
                                btnAgregar.Content = Properties.Resources.MermaEsperada_btnAgregar;
                            }
                            else
                            {
                                //Merma menor a 0
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.MermaEsperada_MermaMinima,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                            }
                        }
                        else
                        {
                            //Merma Mayor a 100
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.MermaEsperada_MermaMaxima,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                        }
                    }
                }
                else
                {
                    //Hay un registro para el destino
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.MermaEsperada_OrganizacionExistente,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }

        }

        private void BtnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          string.Format(Properties.Resources.MermaEsperada_PreguntaEliminaDestino, ((MermaEsperadaInfo)dgMerma.SelectedItem).OrganizacionDestino.Descripcion),
                                          MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    if (((MermaEsperadaInfo)dgMerma.SelectedItem).Nuevo == 1)
                    {
                        lsMermas.Remove(((MermaEsperadaInfo)dgMerma.SelectedItem));
                        dgMerma.ItemsSource = new List<MermaEsperadaInfo>();
                        if (lsMermas != null)
                        {
                            dgMerma.ItemsSource = lsMermas.Where(registro => registro.Nuevo != 3).ToList();
                        }
                    }
                    else
                    {
                        foreach (MermaEsperadaInfo merma in lsMermas)
                        {
                            if (merma.OrganizacionDestino.OrganizacionID == ((MermaEsperadaInfo)dgMerma.SelectedItem).OrganizacionDestino.OrganizacionID)
                            {
                                merma.Nuevo = 3;
                            }
                        }

                        dgMerma.ItemsSource = new List<MermaEsperadaInfo>();
                        if (lsMermas != null)
                        {
                            dgMerma.ItemsSource = lsMermas.Where(registro => registro.Nuevo != 3).ToList();
                        }
                    }
                    if (lsMermas.Any(registro => registro.Nuevo != 3))
                    {
                        btnGuardar.IsEnabled = true;
                    }
                    else
                    {
                        btnGuardar.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }

        private void BtnEditar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Contexto.OrganizacionDestino = lsMermas[dgMerma.SelectedIndex].OrganizacionDestino;
                Contexto.Merma = lsMermas[dgMerma.SelectedIndex].Merma;

                skAyudaDestino.Clave = Contexto.OrganizacionDestino.OrganizacionID.ToString();
                skAyudaDestino.Descripcion = Contexto.OrganizacionDestino.Descripcion.ToString();
                txtMerma.Value = Contexto.Merma;

                if (lsMermas[dgMerma.SelectedIndex].Nuevo == 1)
                {
                    nuevo = lsMermas[dgMerma.SelectedIndex].Nuevo;
                }
                else
                {
                    nuevo = 2;
                }

                btnAgregar.Content = Properties.Resources.MermaEsperada_btnActualizar;

                renglon = dgMerma.SelectedIndex;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }

        private void skAyudaDestino_AyudaConDatos_1(object sender, EventArgs e)
        {
            try
            {
                if (!(Contexto.OrganizacionOrigen.OrganizacionID != Contexto.OrganizacionDestino.OrganizacionID))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.MermaEsperada_SeleccionarOrganizacionDiferente,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                    InicializaDestino();
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }

        private void btnLimpiar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                InicializaDestino();
                txtMerma.Text = "0.0";
                btnAgregar.Content = Properties.Resources.MermaEsperada_btnAgregar;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }

        private void skAyudaOrganizacion_AyudaConDatos_1(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(skAyudaOrganizacion.Clave) != organizacionOrigenId)
                {
                    MermaEsperadaPL mermaEsperadaPl = new MermaEsperadaPL();
                    Contexto.Activo = EstatusEnum.Activo;
                    lsMermas = mermaEsperadaPl.ObtenerMermaPorOrganizacionOrigenID(Contexto);
                    dgMerma.ItemsSource = new List<MermaEsperadaInfo>();
                    if (lsMermas != null)
                    {
                        dgMerma.ItemsSource = lsMermas.Where(registro => registro.Nuevo != 3).ToList();
                    }
                    organizacionOrigenId = int.Parse(skAyudaOrganizacion.Clave);

                    InicializaDestino();
                    txtMerma.Text = "0.0";
                    btnAgregar.Content = Properties.Resources.MermaEsperada_btnAgregar;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }

        private void skAyudaOrganizacion_AyudaLimpia(object sender, EventArgs e)
        {
            try
            {
                InicializaContexto();
                InicializaDestino();
                txtMerma.Text = "0.0";
                lsMermas = new List<MermaEsperadaInfo>();
                dgMerma.ItemsSource = lsMermas;
                btnGuardar.IsEnabled = false;
                organizacionOrigenId = 0;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    ex.Message,
                                    MessageBoxButton.OK,
                                    MessageImage.Error);
            }
        }
    }
}
