using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for OrganizacionEdicion.xaml
    /// </summary>
    public partial class OrganizacionEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private OrganizacionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (OrganizacionInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;
        
        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public OrganizacionEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarCombos();
            skAyudaZona.ObjetoNegocio = new ZonaPL();
            
        }


        /// <summary>
        /// Constructor para editar una entidad Organizacion Existente
        /// </summary>
        /// <param name="organizacionInfo"></param>
        public OrganizacionEdicion(OrganizacionInfo organizacionInfo)
        {
            InitializeComponent();
            organizacionInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            if (organizacionInfo.Zona == null)
            {
                organizacionInfo.Zona = new ZonaInfo();
            }
            Contexto = organizacionInfo;
            skAyudaZona.ObjetoNegocio = new ZonaPL();
            skAyudaZona.AyudaConDatos += (sender, args) =>
            {
            };
            CargarCombos();
        }

        #endregion Constructores
 
        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para regresar a la pantalla anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            skAyudaZona.LimpiarCampos();
            Close();
        }

        private void TxtAceptaNumerosLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        private void TxtAceptaNumerosLetrasParentesisPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuionParentesis(e.Text);
        }
     
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new OrganizacionInfo
            {
                Descripcion = string.Empty,
                Direccion = string.Empty,
                TipoOrganizacion = new TipoOrganizacionInfo(),
                Iva = new IvaInfo(),
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
              
                Zona  = new  ZonaInfo()
                            {
                                Pais = new PaisInfo()
                                           {
                                               PaisID = 0
                                           }
                            }
            };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.OrganizacionEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbTipoOrganizacion.SelectedItem == null || Contexto.TipoOrganizacion.TipoOrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.OrganizacionEdicion_MsgTipoOrganizacionRequerida;
                    cmbTipoOrganizacion.Focus();
                }
                else if (cmbIva.SelectedItem == null || Contexto.Iva.IvaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.OrganizacionEdicion_MsgIvaRequerida;
                    cmbIva.Focus();
                }
                else
                {
                    int organizacionId = Extensor.ValorEntero(txtOrganizacionId.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    if (skAyudaZona.Clave != string.Empty && !skAyudaZona.Clave.Equals("0"))
                    {
                        Contexto.Zona.ZonaID = Convert.ToInt32(skAyudaZona.Clave);
                    }
                    else
                    {
                        Contexto.Zona.ZonaID = null;
                    }
                    
                    
                    var organizacionPL = new OrganizacionPL();
                    OrganizacionInfo organizacion = organizacionPL.ObtenerPorDescripcion(descripcion);

                    if (organizacion != null && (organizacionId == 0 || organizacionId != organizacion.OrganizacionID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.OrganizacionEdicion_MsgDescripcionExistente,
                                                organizacion.OrganizacionID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }

            return resultado;
        }

        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var organizacionPL = new OrganizacionPL();
                    organizacionPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if(Contexto.OrganizacionID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Organizacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Organizacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }

        }

        /// <summary>
        /// Método para cargar el combo tipos de organización
        /// </summary>
        private void CargarCombos()
        {
            CargarComboTiposOrganizacion();
            CargarComboIva();
        }

        /// <summary>
        /// Método para cargar el combo tipos de organización
        /// </summary>
        private void CargarComboTiposOrganizacion()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();
            var tipoOrganizacion = new TipoOrganizacionInfo
            {
                TipoOrganizacionID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoOrganizacionInfo> listaTiposOrganizacion = tipoOrganizacionPL.ObtenerTodos(EstatusEnum.Activo);
            listaTiposOrganizacion.Insert(0, tipoOrganizacion);
            cmbTipoOrganizacion.ItemsSource = listaTiposOrganizacion;
            cmbTipoOrganizacion.SelectedItem = tipoOrganizacion;
        }
        
        /// <summary>
        /// Método para cargar el combo iva
        /// </summary>
        private void CargarComboIva()
        {
            var ivaPL = new IvaPL();
            var iva = new IvaInfo
            {
                IvaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<IvaInfo> listaIva = ivaPL.ObtenerTodos(EstatusEnum.Activo);
            listaIva.Insert(0, iva);
            cmbIva.ItemsSource = listaIva;
            cmbIva.SelectedItem = iva;
        }
        #endregion Métodos

    }
}

