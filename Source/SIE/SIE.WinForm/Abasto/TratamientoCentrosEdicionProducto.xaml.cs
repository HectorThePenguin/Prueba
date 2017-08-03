using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Abasto
{

    public partial class TratamientoCentrosEdicionProducto
    {

        #region CONSTRUCTORES
        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        public TratamientoCentrosEdicionProducto(TratamientoProductoInfo tratamientoProducto, int organizacionOrigenId)
        {
            try
            {
                InitializeComponent();
                TratamientoProducto = tratamientoProducto;
                organizacionID = organizacionOrigenId;
                if (tratamientoProducto.Factor)
                {
                    ckbFactor.IsChecked = true;
                }
                else {
                    iudMacho.IsEnabled = false;
                    iudHembra.IsEnabled = false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion CONSTRUCTORES

        #region PROPIEDADES

        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private TratamientoProductoInfo TratamientoProducto
        {
            get
            {
                return (TratamientoProductoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        public bool ConfirmaSalir = true;

        #endregion PROPIEDADES

        #region VARIABLES

        /// <summary>
        /// Control para la ayuda de Producto
        /// </summary>
        private SKAyuda<ProductoInfo> skAyudaProducto;

        /// <summary>
        /// Control para la ayuda de Producto
        /// </summary>
        private IEnumerable<SubFamiliaInfo> subFamilias;

        private int organizacionID;
        #endregion VARIABLES

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta cuando inicia la pantalla
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarSubFamilias();
                AgregarAyudaProducto();
                CargarComboFamilias();
                if (TratamientoProducto.Producto.SubFamilia.SubFamiliaID == 0)
                {
                    CargarComboSubFamiliaDefault();
                }
                cboFamilia.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Aceptar
        /// </summary>
        private void btnAceptar_OnClick(object sender, RoutedEventArgs e)
        {
            if(!ValidarProducto())
            {
                return;
            }
            ConfirmaSalir = false;
            Close();
        }

        private void CboFamilia_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //AsignaSubfamilias(true);
                CargarComboSubFamilias();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ErrorFamilia, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        private void CboSubFamilia_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AsignaDependenciasAyudaProducto(skAyudaProducto, cboSubFamilia);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ErrorSubFamilia, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConfirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Cancelar
        /// </summary>
        private void btnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ckbFactor_Unchecked(object sender, RoutedEventArgs e)
        {
            iudHembra.IsEnabled = false;
            iudMacho.IsEnabled = false;
        }

        private void ckbFactor_Checked(object sender, RoutedEventArgs e)
        {
            iudHembra.IsEnabled = true;
            iudMacho.IsEnabled = true;
        }
        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private bool ValidarProducto()
        {
            if(TratamientoProducto.Producto.Familia.FamiliaID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_CampoFamilia, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if(TratamientoProducto.Producto.SubFamilia.SubFamiliaID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_CampoSubFamilia, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if(string.IsNullOrWhiteSpace(TratamientoProducto.Producto.ProductoDescripcion) || TratamientoProducto.Producto.ProductoId == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_CampoProducto, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if(TratamientoProducto.Dosis == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_CampoDosis, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            var isChecked = Convert.ToBoolean(ckbFactor.IsChecked);
            if (isChecked && TratamientoProducto.FactorMacho == 0)
            {
                iudMacho.Focus();
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ValidarFactorMacho, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            else {
                if (isChecked && TratamientoProducto.FactorHembra == 0)
                {
                    SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ValidarFactorHembra, MessageBoxButton.OK, MessageImage.Warning);
                    iudHembra.Focus();
                    return false;
                }
                else
                {
                    if (TratamientoProducto.FactorMacho >= 10000 || TratamientoProducto.FactorHembra >= 10000)
                    {
                        SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_ValidarFactorHembrTratamientoEdicionProducto_ValidarRangoFactor, MessageBoxButton.OK, MessageImage.Warning);
                        return false;
                    }
                    else
                    {
                        TratamientoProducto.Factor = Convert.ToBoolean(ckbFactor.IsChecked);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarSubFamilias()
        {
            var subFamiliaPL = new SubFamiliaPL();
            subFamilias = subFamiliaPL.Centro_ObtenerTodos(EstatusEnum.Activo);
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboSubFamiliaDefault()
        {
            var subFamiliaSeleccione = new SubFamiliaInfo
            {
                SubFamiliaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione
            };
            var listaOrdenada = new List<SubFamiliaInfo>();
            listaOrdenada.Insert(0, subFamiliaSeleccione);
            cboSubFamilia.ItemsSource = listaOrdenada;
            if (TratamientoProducto.Producto.SubFamilia == null)
            {
                cboSubFamilia.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboSubFamilias()
        {
            var familiaSeleccionada = (FamiliaInfo)cboFamilia.SelectedItem;
            if (familiaSeleccionada == null || familiaSeleccionada.FamiliaID == 0)
            {
                return;
            }
            var subFamiliaSeleccione = new SubFamiliaInfo
                {
                    SubFamiliaID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione
                };
            var listaOrdenada = subFamilias.Where(sub => sub.Familia.FamiliaID == familiaSeleccionada.FamiliaID).OrderBy(sub => sub.Descripcion).ToList();
            listaOrdenada.Insert(0, subFamiliaSeleccione);
            
            cboSubFamilia.ItemsSource = listaOrdenada;
            if(TratamientoProducto.Producto.SubFamilia == null)
            {
                cboSubFamilia.SelectedIndex = 0;
            }
            if(listaOrdenada.Count <= 1)
            {
                SkMessageBox.Show(this, Properties.Resources.TratamientoEdicionProducto_SinSubfamilias, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboFamilias()
        {
            var familiaPL = new FamiliaPL();
            var familiaSeleccione = new FamiliaInfo
            {
                FamiliaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione
            };
            IEnumerable<FamiliaInfo> familias = familiaPL.Centros_ObtenerTodos(EstatusEnum.Activo);
            var listaOrdenada = familias.OrderBy(tipo => tipo.Descripcion).ToList();
            listaOrdenada.Insert(0, familiaSeleccione);
            cboFamilia.ItemsSource = listaOrdenada;
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaProducto()
        {

            skAyudaProducto = new SKAyuda<ProductoInfo>(160, false, TratamientoProducto.Producto
                                                                , "PropiedadProductoIDTratamientoCentros"
                                                                , "PropiedadDescripcionProductoTratamientoCentros"
                                                                , true, true)
            {
                AyudaPL = new ProductoPL()
            };

            splAyudaProducto.Children.Clear();
            splAyudaProducto.Children.Add(skAyudaProducto);

            skAyudaProducto.MensajeClaveInexistente = Properties.Resources.Producto_CodigoInvalido;
            skAyudaProducto.MensajeAgregar = Properties.Resources.Producto_Seleccionar;
            skAyudaProducto.MensajeBusqueda = Properties.Resources.Producto_Busqueda;
            skAyudaProducto.MensajeBusquedaCerrar = Properties.Resources.Producto_SalirSinSeleccionar;
            skAyudaProducto.TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaProducto;
            skAyudaProducto.TituloPantalla = Properties.Resources.BusquedaProdcuto_Titulo;

            skAyudaProducto.MensajeDependencias = null;
            IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
            mensajeDependencias.Add("SubFamiliaID",
                                    Properties.Resources.Producto_SeleccionarSubFamilia);
            skAyudaProducto.MensajeDependencias = mensajeDependencias;
            skAyudaProducto.AsignaTabIndex(2);

            AsignaDependenciasAyudaProducto(skAyudaProducto, cboSubFamilia);
        }

        /// <summary>
        /// Metodo para agregar las dependencias a las ayudas de Organización Origen y Destino
        /// </summary>
        private void AsignaDependenciasAyudaProducto(SKAyuda<ProductoInfo> controlAyuda, ComboBox combo)
        {
            controlAyuda.Dependencias = null;

            IList<IDictionary<IList<String>, Object>> dependencias = new List<IDictionary<IList<String>, Object>>();


            IDictionary<IList<string>, object> dependecia = new Dictionary<IList<String>, Object>();
            IList<string> camposDependientes = new List<String> { "SubFamiliaID"};
            dependecia.Add(camposDependientes, combo.SelectedItem);
            dependencias.Add(dependecia);

            camposDependientes = new List<String> { "OrganizacionID" };
            dependecia.Add(camposDependientes, organizacionID);
            dependencias.Add(dependecia);

            controlAyuda.Dependencias = dependencias;
        }
        #endregion METODOS
       
    }
}
