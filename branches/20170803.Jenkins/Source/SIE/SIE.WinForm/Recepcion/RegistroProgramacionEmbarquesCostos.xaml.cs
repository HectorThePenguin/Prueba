using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SIE.Services.Info.Enums;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System.Linq;
using SuKarne.Controls.MessageBox;
using System.Windows.Controls;
using SuKarne.Controls.Enum;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for RegistroProgramacionEmbarquesCostos.xaml
    /// </summary>
    public partial class RegistroProgramacionEmbarquesCostos
    {
        #region CONSTRUCTORES
        /// <summary>
        /// Constructor Parametrizado
        /// </summary>
        public RegistroProgramacionEmbarquesCostos(List<CostoEmbarqueDetalleInfo> listaCostoEmbarqueDetalle)
        {
            InitializeComponent();
            ListaCostoEmbarqueDetalle = listaCostoEmbarqueDetalle;
            CargarListaTipoCosto();
            AgregarAyudaCosto();
            if (ListaCostoEmbarqueDetalle.Any())
            {
                LlenarGridCostos();
            }

        }
        #endregion CONSTRUCTORES

        #region VARIABLES
        /// <summary>
        /// Variable donde se almacenan los Costos del Embarque
        /// </summary>
        public List<CostoEmbarqueDetalleInfo> ListaCostoEmbarqueDetalle { get; set; }

        /// <summary>
        /// Variable que indica si el registro se esta modificando
        /// </summary>
        private bool esModificacion;

        /// <summary>
        /// Variable para utilizar la ayuda de Costos
        /// </summary>
        private SKAyuda<CostoInfo> skAyudaCosto;

        /// <summary>
        /// Variable para manejar los costos que solo levanta la ayuda
        /// </summary>
        private static readonly List<int> CostosValidosCaptura = new List<int> { 4 };

        /// <summary>
        /// Constante para manejar el Costo default
        /// </summary>
        private List<TipoCostoInfo> listaTipoCosto;
        #endregion VARIABLES

        #region METODOS

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void CargarListaTipoCosto()
        {
            var listaTipoCostos = (from costosValidos in CostosValidosCaptura
                                   select new TipoCostoInfo
                                   {
                                       TipoCostoID = costosValidos
                                   }).ToList();
            listaTipoCosto = listaTipoCostos;
        }

        /// <summary>
        /// Cargar la Auida de Costo
        /// </summary>
        private void AgregarAyudaCosto()
        {
            var costoInfo = new CostoInfo
            {
                ListaTipoCostos = listaTipoCosto
            };
            skAyudaCosto = new SKAyuda<CostoInfo>(160, false, costoInfo, "PropiedadClaveProgramacionEmbarqueCostos"
                                                , "PropiedadDescripcionProgramacionEmbarqueCostos"
                                                , "PropiedadOcultaProgramacionEmbarqueCostos", true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.Costo_CodigoInvalidoEmbarque,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };
            skAyudaCosto.LlamadaMetodos += CostoObtenido;
            SplAyudaCosto.Children.Clear();
            SplAyudaCosto.Children.Add(skAyudaCosto);
        }

        /// <summary>
        /// Asigna el Focus al control de Importe
        /// </summary>
        private void CostoObtenido()
        {
            DudImporte.Focus();

            // Buscamos una sugerencia para el costo seleccinado
            if (skAyudaCosto.Id == Costo.Fletes.GetHashCode().ToString() || skAyudaCosto.Id == Costo.GastosIndirectos.GetHashCode().ToString())
            {
                RegistroProgramacionEmbarques registroProgramacionEmbarques = (RegistroProgramacionEmbarques)Owner;
                EmbarqueInfo embarqueInfo = registroProgramacionEmbarques.ObtenerInformacionDeEmbarque();
                if (embarqueInfo.TipoEmbarque.TipoEmbarqueID != 0 && 
                    embarqueInfo.ListaEscala[0].Proveedor != null &&
                    embarqueInfo.ListaEscala[0].OrganizacionDestino != null &&
                    embarqueInfo.ListaEscala[0].OrganizacionOrigen != null)
                {
                    embarqueInfo.ListaEscala[0].ListaCostoEmbarqueDetalle = new List<CostoEmbarqueDetalleInfo>();
                    embarqueInfo.ListaEscala[0].ListaCostoEmbarqueDetalle.Add(new CostoEmbarqueDetalleInfo()
                    {
                        Costo = new CostoInfo()
                        {
                            CostoID = int.Parse(skAyudaCosto.Id)
                        }
                    });

                    CostoEmbarqueDetallePL costoEmbarqueDetallePL = new CostoEmbarqueDetallePL();
                    CostoInfo costoInfo = costoEmbarqueDetallePL.ObtenerUltimoPorCostoIDProveedorIDOrganizacionOrigenOrganizacionDestino(embarqueInfo);
  
                    if(costoInfo != null)
                        DudImporte.Value = costoInfo.ImporteCosto;
                }
            }

        }

        /// <summary>
        /// Agregar el Costo a la Lista
        /// </summary>
        private void AgregarCostoEmbarqueDetalle()
        {
            if (ValidarAgregar())
            {
                var costoEmbarqueDetalle = new CostoEmbarqueDetalleInfo
                {
                    Costo = CargarCosto(),
                    Activo = EstatusEnum.Activo,
                    Importe = DudImporte.Value.HasValue ? DudImporte.Value.Value : 0,
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Orden = 0
                };
                if (esModificacion)
                {
                    var costoModificar =
                        ListaCostoEmbarqueDetalle.FirstOrDefault(
                            cos => cos.Costo.CostoID == costoEmbarqueDetalle.Costo.CostoID
                            && cos.Activo == EstatusEnum.Activo
                            );
                    if (costoModificar == null)
                    {
                        return;
                    }
                    costoModificar.Importe = costoEmbarqueDetalle.Importe;
                }
                else
                {
                    if (
                       ListaCostoEmbarqueDetalle.Any(
                           cos =>
                           cos.Costo.CostoID == costoEmbarqueDetalle.Costo.CostoID &&
                           //cos.Orden == costoEmbarqueDetalle.Orden &&
                           cos.Activo == costoEmbarqueDetalle.Activo))
                    {
                        SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_CostoDuplicado,
                                          MessageBoxButton.OK, MessageImage.Stop);
                        skAyudaCosto.LimpiarCampos();
                        DudImporte.Value = null;
                        return;
                    }
                    ListaCostoEmbarqueDetalle.Add(costoEmbarqueDetalle);
                }
                LlenarGridCostos();
                LimpiarControles();
                BtnAgregar.Content = Properties.Resources.btnAgregar;
            }
        }

        /// <summary>
        /// Limpia los controles para poder capturar otro Costo
        /// </summary>
        private void LimpiarControles()
        {
            skAyudaCosto.LimpiarCampos();
            skAyudaCosto.IsEnabled = true;
            DudImporte.Value = null;
            esModificacion = false;
        }

        /// <summary>
        /// Validaciones previas para agregar un Costo
        /// </summary>
        private bool ValidarAgregar()
        {
            if (string.IsNullOrWhiteSpace(skAyudaCosto.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_RequeridoCosto, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            if (!DudImporte.Value.HasValue)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_ImporteValido, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            if (DudImporte.Value == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_ImporteMayorCero, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Carga el Grid con los datos de la Lista
        /// </summary>
        private void LlenarGridCostos()
        {
            DgCostos.ItemsSource = null;
            DgCostos.ItemsSource = ListaCostoEmbarqueDetalle.Where(cos => cos.Activo == EstatusEnum.Activo);
        }

        /// <summary>
        /// Carga el Costo de la Ayuda
        /// </summary>
        private CostoInfo CargarCosto()
        {
            var costo = new CostoInfo
            {
                ClaveContable = skAyudaCosto.Clave,
                CostoID = int.Parse(skAyudaCosto.Id),
                Descripcion = skAyudaCosto.Descripcion
            };
            return costo;
        }

        /// <summary>
        /// Carga los valores de la lista en los controles para modificar el Costo
        /// </summary>
        private void CargarValoresModificar(CostoEmbarqueDetalleInfo costo)
        {
            skAyudaCosto.Clave = costo.Costo.ClaveContable;
            skAyudaCosto.Descripcion = costo.Costo.Descripcion;
            DudImporte.Value = costo.Importe;
            skAyudaCosto.IsEnabled = false;
            esModificacion = true;
            skAyudaCosto.Id = string.Format("{0}", costo.Costo.CostoID);
            BtnAgregar.Content = Properties.Resources.RegistroProgramacionEmbarque_botonActualizar;

        }

        #endregion METODOS

        #region EVENTOS

        private void Window_Closing(object sender, CancelEventArgs cancelEventArgs)
        {
            if (!string.IsNullOrWhiteSpace(skAyudaCosto.Descripcion) || DudImporte.Value.HasValue)
            {
                if (SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_Cerrar, MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.No)
                {
                    cancelEventArgs.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando carga la pantalla
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            skAyudaCosto.AsignarFoco();
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void DudImporte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                BtnAgregar.Focus();
                return;
            }
           
            string valorControl = DudImporte.Text ?? string.Empty;

            if (e.Key == Key.Decimal && valorControl.IndexOf('.') > 0)
            {
                e.Handled = true;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da clic en el botón Agregar
        /// </summary>
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            skAyudaCosto.AsignarFoco();
            AgregarCostoEmbarqueDetalle();
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da clic en el botón Editar del Grid
        /// </summary>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            var costo = (CostoEmbarqueDetalleInfo)btn.CommandParameter;
            if (costo == null)
            {
                return;
            }
            CargarValoresModificar(costo);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da clic en el botón Eliminar del Grid
        /// </summary>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_MensajeEliminar, MessageBoxButton.YesNo, MessageImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }
            var btn = (Button)e.Source;
            var costo = (CostoEmbarqueDetalleInfo)btn.CommandParameter;
            if (costo == null)
            {
                return;
            }
            costo.Activo = EstatusEnum.Inactivo;
            LlenarGridCostos();
            skAyudaCosto.AsignarFoco();
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da clic en el botón Cancelar
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion EVENTOS
    }
}
