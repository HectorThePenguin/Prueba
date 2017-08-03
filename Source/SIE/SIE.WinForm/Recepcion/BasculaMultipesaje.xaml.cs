using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.IO.Ports;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Impresora;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for BasculaMultipesaje.xaml
    /// </summary>
    public partial class BasculaMultipesaje
    {

        #region VARIABLES

        /// <summary>
        /// esta variable es utilizada para saber si registrara o actualizara
        ///  en la base de datos
        /// </summary>
        bool actualizar = false;

        private bool registroActualizado = false;

        private string conceptoTipoPesaje;

        /// <summary>
        /// Nombre de la impresora
        /// </summary>
        private string nombreImpresora;

        /// <summary>
        /// Despues de guardar un nuevo registro regresa el folio con el 
        /// que se registro y es almacenado  en folioDespuesDeGuardar
        /// </summary>
        private long folioDespuesDeGuardar;

        private SerialPortManager spManager;

        #endregion

        #region CONSTRUCTORES

        public BasculaMultipesaje()
        {
            InitializeComponent();
            if(InicializaContexto())
            { 
                InicializaCampos();
            }
        }

        #endregion CONSTRUCTORES

        #region PROPIEDADES

        /// <summary>
        /// Se declara el contexto
        /// </summary>
        private BasculaMultipesajeInfo BasculaMultipesajeContext
        {
            get
            {
                //if (DataContext == null)
                //{
                //    InicializaContexto();
                //}
                
                return (BasculaMultipesajeInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region METODOS
        /// <summary>
        /// Se inicializa el contesto 
        /// </summary>
        private bool InicializaContexto()
        {
            bool resultado = false;
            var organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            OperadorInfo operadorInfo = ObtenerOperador(organizacionID);
            
            BasculaMultipesajeContext = new BasculaMultipesajeInfo();

            if (operadorInfo.Usuario == null)
            {
                operadorInfo.Usuario = new UsuarioInfo();
            }

            if (validarOperador(operadorInfo))
            {

                BasculaMultipesajeContext = new BasculaMultipesajeInfo()
                {
                    FechaCreacion = DateTime.Now,
                    OrganizacionInfo = new OrganizacionInfo()
                    {
                        OrganizacionID = organizacionID
                    },
                    QuienRecibe = operadorInfo == null ? new OperadorInfo()
                    {
                        Usuario = new UsuarioInfo()
                    } : operadorInfo,
                    FolioMultipesaje = new FolioMultipesajeInfo(),
                };

                AsignarUsuarioLogueado();
                SkAyudaFolio.ObjetoNegocio = new BasculaMultipesajePL(organizacionID);
                SkAyudaQuienRecibe.ObjetoNegocio = new OperadorPL();

                nombreImpresora = ConfigurationManager.AppSettings["nombreImpresora"].Trim();
                folioDespuesDeGuardar = 0;
                resultado = true;
            }
            else
            {
                HabilitarCampos(false);
                HabilitarDeshabilitarBotonGuardar(false);
                HabilitarDeshabilitarPesoBotones(false, false, false);
                btnCancelar.IsEnabled = false;
                btnImprimirTicket.IsEnabled = false;
            }
            
            return resultado;
        }

        private bool validarOperador(OperadorInfo operadorInfo)
        {
            bool resultado = false;
            if (operadorInfo.EstatusUsuario == EstatusEnum.Inactivo)
            {
                MostrarMensaje(Properties.Resources.Login_ErrorUsuarioLocal, MessageImage.Warning);
            }
            else
            {
                if (operadorInfo.OperadorID == 0)
                {
                    MostrarMensaje(Properties.Resources.General_OperadorNoAsignado, MessageImage.Warning);
                }
                else
                {
                    if (operadorInfo.Activo == EstatusEnum.Inactivo)
                    {
                        MostrarMensaje(Properties.Resources.General_OperadorNoActivo, MessageImage.Warning);
                    }
                    else
                    {
                        if (operadorInfo.Rol == null || operadorInfo.Rol.RolID == 0)
                        {
                            MostrarMensaje(Properties.Resources.General_OperadorRolNoAsignado, MessageImage.Warning);
                        }
                        else
                        {
                            if (operadorInfo.Rol.Activo == EstatusEnum.Inactivo)
                            {
                                MostrarMensaje(Properties.Resources.General_OperadorRolNoActivo, MessageImage.Warning);
                            }
                            else
                            {
                                resultado = true;
                            }
                        }
                    }
                }
            }

            return resultado;
        }
        /// <summary>
        /// Consulta el folio y regresa los datos para mostrarlos
        /// </summary>
        private void ObtenerPesaje(int organizacionID)
        {
            if (BasculaMultipesajeContext != null && BasculaMultipesajeContext.FolioMultipesaje != null)
            {
                long folio = BasculaMultipesajeContext.FolioMultipesaje.Folio;
                BasculaMultipesajePL basculaPl = new BasculaMultipesajePL(organizacionID);
                BasculaMultipesajeInfo resultado = basculaPl.ConsultaBasculaMultipesaje(folio, organizacionID);
                LlenarCamposConPesajes(resultado);
                registroActualizado = BasculaMultipesajeContext.PesoBruto > 0 && BasculaMultipesajeContext.PesoTara > 0;
            }
        }

        private OperadorInfo ObtenerOperador(int organizacionID)
        {
            return ObtenerOperador(AuxConfiguracion.ObtenerUsuarioLogueado(), organizacionID);
        }

        private OperadorInfo ObtenerOperador(int usuarioId, int organizacionID)
        {
            OperadorPL operadorPl = new OperadorPL();
            OperadorInfo operador = operadorPl.ObtenerPorUsuarioId(usuarioId, organizacionID);
            if(operador == null)
            {
                operador = new OperadorInfo();
            }
            return operador;
        }

        /// <summary>
        /// Se Inicializan los campos con su valor inicial
        /// </summary>
        private void InicializaCampos()
        {
            if (BasculaMultipesajeContext != null)
            {
                AsignarUsuarioLogueado();
                chkEnvioSAP.IsEnabled = true;
                chkEnvioSAP.IsChecked = false;
                BasculaMultipesajeContext.Chofer = string.Empty;
                BasculaMultipesajeContext.FechaCreacion = DateTime.Now;
                BasculaMultipesajeContext.PesoBruto = 0;
                BasculaMultipesajeContext.PesoTara = 0;
                BasculaMultipesajeContext.PesoNeto = 0;
                BasculaMultipesajeContext.Placas = string.Empty;
                BasculaMultipesajeContext.Producto = string.Empty;
                BasculaMultipesajeContext.FolioMultipesaje = new FolioMultipesajeInfo();
                BasculaMultipesajeContext.FolioMultipesaje.Folio = 0;
                BasculaMultipesajeContext.Observacion = string.Empty;
                txtDisplay.Text = string.Empty;
                txtObservacion.IsEnabled = true;
                HabilitarDeshabilitarPesoBotones(true, true, false);
                HabilitarDeshabilitarBotonGuardar(false);
                actualizar = false;
                HabilitarCampos(true);
            }
            else
            {
                HabilitarDeshabilitarBotonGuardar(false);
                HabilitarDeshabilitarPesoBotones(false, false, false);
            }
        }

        /// <summary>
        /// Se llenan los campos del modulo si encontro el numero de folio que
        ///  se consulto si no se encuentra se reinician los campos a su valos predeterminado
        /// </summary>
        /// <param name="resultado"></param>
        private void LlenarCamposConPesajes(BasculaMultipesajeInfo resultado)
        {
            if (resultado != null)
            {
                BasculaMultipesajeContext.Chofer = resultado.Chofer;
                BasculaMultipesajeContext.FechaPesoBruto = resultado.FechaPesoBruto;
                BasculaMultipesajeContext.FechaPesoTara = resultado.FechaPesoTara;
                BasculaMultipesajeContext.FechaCreacion = resultado.FechaCreacion;
                BasculaMultipesajeContext.FechaModificacion = resultado.FechaModificacion;
                BasculaMultipesajeContext.PesoBruto = resultado.PesoBruto;
                BasculaMultipesajeContext.PesoTara = resultado.PesoTara;
                BasculaMultipesajeContext.Placas = resultado.Placas;
                BasculaMultipesajeContext.Producto = resultado.Producto;
                BasculaMultipesajeContext.PesoNeto = 0;
                BasculaMultipesajeContext.Observacion = resultado.Observacion;
                BasculaMultipesajeContext.QuienRecibe = ObtenerOperador(resultado.UsuarioCreacion, AuxConfiguracion.ObtenerOrganizacionUsuario());
                BasculaMultipesajeContext.EnvioSAP = resultado.EnvioSAP;
                BasculaMultipesajeContext.UsuarioCreacion = resultado.UsuarioCreacion;
                //BasculaMultipesajeContext.QuienRecibe.Usuario = new UsuarioInfo()
                //{
                //    UsuarioID = resultado.QuienRecibe.OperadorID
                //};
                SkAyudaQuienRecibe.Clave = BasculaMultipesajeContext.QuienRecibe.OperadorID.ToString();
                SkAyudaQuienRecibe.Descripcion = BasculaMultipesajeContext.QuienRecibe.NombreCompleto;
                ValidarVistaPesaje();
                actualizar = true;
                HabilitarDeshabilitarBotonGuardar(false);
                HabilitarCampos(false);
            }
            else
            {
                InicializaCampos();
                HabilitarCampos(true);
            }
        }

        /// <summary>
        /// Metodo donde se habilitan/deshablitan los campos chofer, placas, producto
        /// </summary>
        /// <param name="habilitado"></param>
        private void HabilitarCampos(bool habilitado)
        {
            txtChofer.IsEnabled = habilitado;
            txtPlacas.IsEnabled = habilitado;
            txtProducto.IsEnabled = habilitado;
            chkEnvioSAP.IsEnabled = habilitado;
            SkAyudaFolio.IsEnabled = habilitado;
        }

        /// <summary>
        /// Se valida los campos de pesobruto y pesotara dependiendo 
        /// los datos almacenados en el contexto
        /// </summary>
        private void ValidarVistaPesaje()
        {
            txtDisplay.Text = string.Empty;
            txtDisplay.IsReadOnly = false;
            txtObservacion.IsEnabled = true;

            if (BasculaMultipesajeContext.PesoBruto == 0)
            {
                HabilitarDeshabilitarBotonGuardar(true);
                btnCapturarPesoTara.IsEnabled = false;
                btnCapturarPesoBruto.IsEnabled = true;
            }
            else
            {
                HabilitarDeshabilitarBotonGuardar(true);
                btnCapturarPesoBruto.IsEnabled = false;
                btnCapturarPesoTara.IsEnabled = true;
            }

            if (BasculaMultipesajeContext.PesoBruto > 0 && BasculaMultipesajeContext.PesoTara > 0)
            {
                HabilitarDeshabilitarPesoBotones(false, false, true);
                LlenarPesoNeto();
                txtObservacion.IsEnabled = false;
            }
        }

        /// <summary>
        /// Se habilitan/deshabilitan los radiobutton dependiendo el contexto
        /// </summary>
        /// <param name="esPesoBruto"></param>
        /// <param name="esPesoTara"></param>
        private void HabilitarDeshabilitarPesoBotones(bool esPesoBruto, bool esPesoTara, bool readonlyDisplay)
        {
            btnCapturarPesoTara.IsEnabled = esPesoTara;
            btnCapturarPesoBruto.IsEnabled = esPesoBruto;
            txtDisplay.IsReadOnly = readonlyDisplay;
        }

        private void LlenarPesoNeto()
        {
            if (BasculaMultipesajeContext.PesoBruto > 0 && BasculaMultipesajeContext.PesoTara > 0)
            {
                BasculaMultipesajeContext.PesoNeto = BasculaMultipesajeContext.PesoBruto -
                                                                     BasculaMultipesajeContext.PesoTara;
            }
        }

        /// <summary>
        /// Se obtiene el usuario que esta logeado actualmente
        /// </summary>
        private void AsignarUsuarioLogueado()
        {
            BasculaMultipesajeContext.UsuarioCreacion = AuxConfiguracion.ObtenerUsuarioLogueado();
        }

        /// <summary>
        /// Se guarda/actualiza en la base de datos el registro
        /// </summary>
        /// <returns></returns>
        private bool GenerarRegisto()
        {
            try
            {
                Logger.Info();
                BasculaMultipesajePL basculaPL = new BasculaMultipesajePL();
                
                if (actualizar)
                {
                    if (BasculaMultipesajeContext.PesoBruto > BasculaMultipesajeContext.PesoTara)
                    {
                        BasculaMultipesajeContext.UsuarioCreacion = AuxConfiguracion.ObtenerUsuarioLogueado();
                        basculaPL.GuardarBasculaMultipesaje(BasculaMultipesajeContext, actualizar);
                        MostrarMensaje(Properties.Resources.BasculaMultipesaje_RegistroModificado, MessageImage.Correct);
                    }
                    else
                    {
                        MostrarMensaje(Properties.Resources.BasculaMultipesaje_PesoBrutoMenorPesoTara, MessageImage.Warning);
                        return false;
                    }                  
                }
                else
                {
                    folioDespuesDeGuardar = basculaPL.GuardarBasculaMultipesaje(BasculaMultipesajeContext, actualizar);
                }

                return true;
            }
            catch (Exception ex)
            {
                MostrarMensaje(Properties.Resources.BasculaMultipesaje_RegistroIncorrecto, MessageImage.Warning);
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }   
            
        }

        /// <summary>
        /// Se inician por defecto los campos si es usuario cancela la operacion
        /// </summary>
        private void LimpiarPantalla()
        {
            if (InicializaContexto())
            {
                InicializaCampos();
            }
            HabilitarDeshabilitarPesoBotones(true, true, false);
            HabilitarDeshabilitarBotonGuardar(false);
        }

        /// <summary>
        /// Se valida que los campos no esten vacios o que se registre pesaje en 0
        /// </summary>
        /// <returns>True si la validacion fue correcta / False si la validacion fue incorrecta </returns>
        private string ObtenerMensajeCamposVacios()
        {
            if (string.IsNullOrEmpty(BasculaMultipesajeContext.Chofer) || string.IsNullOrWhiteSpace(BasculaMultipesajeContext.Chofer))
            {
                return Properties.Resources.BasculaMultipesaje_ChoferVacio;
            }

            if (string.IsNullOrEmpty(BasculaMultipesajeContext.Placas) || string.IsNullOrWhiteSpace(BasculaMultipesajeContext.Placas))
            {
                return Properties.Resources.BasculaMultipesaje_PlacaVacia;
            }

            if (string.IsNullOrEmpty(BasculaMultipesajeContext.Producto) || string.IsNullOrWhiteSpace(BasculaMultipesajeContext.Producto))
            {
                return Properties.Resources.BasculaMultipesaje_ProductoVacio;
            }


            if (BasculaMultipesajeContext.FolioMultipesaje.Folio > 0)
            {
                if (BasculaMultipesajeContext.PesoBruto <= 0)
                {
                    return Properties.Resources.BasculaMultipesaje_PesoBrutoEsCero;
                }
                if (BasculaMultipesajeContext.PesoTara <= 0)
                {
                    return Properties.Resources.BasculaMultipesaje_PesoTaraEsCero;
                }
                if (BasculaMultipesajeContext.PesoBruto <= BasculaMultipesajeContext.PesoTara)
                {
                    return Properties.Resources.BasculaMultipesaje_PesoBrutoMenorPesoTara;
                }
            }
            
            return null;
        } 

        /// <summary>
        /// Se habilita/deshabilita el boton de guardar
        /// </summary>
        /// <param name="habilitado"></param>
        private void HabilitarDeshabilitarBotonGuardar(bool habilitado)
        {
            btnGuardar.IsEnabled = habilitado;
        }

        /// <summary>
        /// metodo para mostrar mensajes
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="imagen"></param>
        private void MostrarMensaje(string mensaje, MessageImage imagen)
        {
            SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK, imagen);
        }

        #endregion METODOS

        #region EVENTOS

        /// <summary>
        /// evento que se ejecuta cuando se manda imprimir el ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimirTicket_Click(object sender, RoutedEventArgs e)
        {
            string mensajesCamposVacios = ObtenerMensajeCamposVacios();
            if (mensajesCamposVacios == null)
            {
                if (actualizar)
                {
                    if (BasculaMultipesajeContext.PesoBruto > BasculaMultipesajeContext.PesoTara)
                    {
                        Imprimir();
                    }
                    else
                    {
                        MostrarMensaje(Properties.Resources.BasculaMultipesaje_PesoBrutoMenorPesoTara, MessageImage.Warning);
                    }                  
                }
                else
                {
                    if (BasculaMultipesajeContext.PesoBruto + BasculaMultipesajeContext.PesoTara > 0)
                    {
                        if (GenerarRegisto())
                        {
                            Imprimir();
                            MostrarMensaje(
                                String.Format(Properties.Resources.BasculaMultipesaje_TicketGenerado,
                                    folioDespuesDeGuardar.ToString(CultureInfo.InvariantCulture)), MessageImage.Correct);
                            InicializaCampos();
                        }
                    }
                    else
                    {
                        MostrarMensaje(Properties.Resources.BasculaMultipesaje_PesoTaraBrutoVacio, MessageImage.Warning);

                    }
                }
            }
            else
            {
                MostrarMensaje(mensajesCamposVacios, MessageImage.Warning);
            }
        }

        /// <summary>
        /// evento para validar que solo tenga numeros 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloNumeros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// evento para validar que no acepte espacios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloNumeros_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InicializarBascula();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (spManager != null)
                {
                    spManager.StopListening();
                    spManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// evento que se ejecuta cuando se actualizara el registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string mensajeCamposVacios = ObtenerMensajeCamposVacios();
            if (mensajeCamposVacios == null)
            {
                GenerarRegisto();
                InicializaCampos();
                HabilitarDeshabilitarBotonGuardar(false);
            }
        }

        /// <summary>
        /// evento que se ejecuta cuando la ayuda de QuienRecibe pierde el foco 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkAyudaQuienRecibe_LostFocus(object sender, RoutedEventArgs e)
        {
            AsignarUsuarioLogueado();
        }

        /// <summary>
        /// evento que se ejecuta cuando la ayuda Folio pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkAyudaFolio_LostFocus(object sender, RoutedEventArgs e)
        {
            var organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            if (SkAyudaFolio.IsEnabled)
            {
                ObtenerPesaje(organizacionID);
            }
        }

        /// <summary>
        /// evento que se ejecuta cuando se cancela la operacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.BasculaMultipesaje_Cancelacion,
                            MessageBoxButton.YesNo,
                            MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LimpiarPantalla();
            }
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (TxtPesoBruto.IsEnabled)
                {
                    TxtPesoBruto.Focus();
                }
                else
                {
                    if (TxtPesoTara.IsEnabled)
                    {
                        TxtPesoTara.Focus();
                    }
                }
                e.Handled = true;
            }
        }

        private void onClick_btnCapturarPesoTara(object sender, RoutedEventArgs e)
        {

            if (txtDisplay.Text.Trim().Length > 0)
            {
                int peso = int.Parse(txtDisplay.Text);
                if (peso > 0)
                {
                    BasculaMultipesajeContext.PesoTara = peso;
                    if (BasculaMultipesajeContext.PesoBruto == 0 || BasculaMultipesajeContext.PesoBruto > BasculaMultipesajeContext.PesoTara)
                    {
                        BasculaMultipesajeContext.EsPesoBruto = false;
                        HabilitarDeshabilitarPesoBotones(false, false, true);
                        LlenarPesoNeto();
                        conceptoTipoPesaje = Properties.Resources.BasculaMultipesaje_Concepto_invertido;
                    }
                    else
                    {
                        MostrarMensaje(Properties.Resources.BasculaMultipesaje_PesoBrutoMenorPesoTara, MessageImage.Warning);
                    }
                }
            }
        }

        private void onClick_btnCapturarPesoBruto(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Trim().Length > 0)
            {
                int peso = int.Parse(txtDisplay.Text);
                if (peso > 0)
                {
                    BasculaMultipesajeContext.PesoBruto = peso;
                    if (BasculaMultipesajeContext.PesoBruto > BasculaMultipesajeContext.PesoTara)
                    {
                        BasculaMultipesajeContext.EsPesoBruto = true;
                        HabilitarDeshabilitarPesoBotones(false, false, true);
                        LlenarPesoNeto();
                        conceptoTipoPesaje = Properties.Resources.BasculaMultipesaje_Concepto_normal;
                    }
                    else
                    {
                        MostrarMensaje(Properties.Resources.BasculaMultipesaje_PesoBrutoMenorPesoTara, MessageImage.Warning);
                    }
                }
            }
        }

        private void TxtDisplay_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        #endregion EVENTOS

        #region Impresion

        /// <summary>
        /// Imprime el ticket
        /// </summary>
        private void Imprimir()
        {
            try
            {
                ImprimirTicket();
                if (!registroActualizado)
                {
                    HabilitarDeshabilitarBotonGuardar(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);   
            }
        }

        private void ImprimirTicket()
        {
            string concepto = string.Empty;
            int lineaConcepto = 5;
            ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
            int maxCaracteresLinea = configuracion.MaxCaracteresLinea;
            string nombreFuente = ConfigurationManager.AppSettings["NombreFuente"];

            var opcionesLinea = new OpcionesLinea
            {
                Fuente = new Font(nombreFuente, 10),
                MargenIzquierdo = 10
            };

            var ticket = new Ticket
            {
                OpcionesImpresora = new OpcionesImpresora
                {
                    //Impresora = configuracion.ImpresoraRecepcionGanado,
                    Impresora = AuxConfiguracion.ObtenerConfiguracion().ImpresoraRecepcionGanado,
                    MaximoLinea = maxCaracteresLinea
                }
            };

            var organizacion = AuxConfiguracion.ObtenerOrganizacionUsuario();
            OrganizacionPL organizacionPl = new OrganizacionPL();
            OrganizacionInfo organizacionInfo = organizacionPl.ObtenerPorID(organizacion);

            List<string> lineasString = new List<string>()
            {
                /* Fecha */         string.Format("{0} {1}        {2}{3}", Properties.Resources.BasculaMultipesaje_lblHora,  BasculaMultipesajeContext.FechaCreacion.ToShortTimeString(), Properties.Resources.BasculaMultipesaje_lblFecha, BasculaMultipesajeContext.FechaCreacion.ToShortDateString()),
                /* organizacion */  organizacionInfo.Descripcion,
                                    string.Empty,
                /* ticket */        string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_ticketNumero, actualizar ? BasculaMultipesajeContext.FolioMultipesaje.Folio : folioDespuesDeGuardar),
                /* placas */        string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_ticketPlacas, BasculaMultipesajeContext.Placas),
                /* chofer */        string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_ticketChofer, BasculaMultipesajeContext.Chofer),
                /* producto */      string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_ticketProducto, BasculaMultipesajeContext.Producto)
            };

            if (actualizar)
            {              
                lineasString.Add(string.Format("{0}   {1}", Properties.Resources.BasculaMultipesaje_ticketPesoBruto,
                    Convert.ToDouble(BasculaMultipesajeContext.PesoBruto).ToString("N", CultureInfo.InvariantCulture)));
                lineasString.Add(string.Format("{0}    {1}", Properties.Resources.BasculaMultipesaje_ticketPesoTara,
                    Convert.ToDouble(BasculaMultipesajeContext.PesoTara).ToString("N", CultureInfo.InvariantCulture)));
                lineasString.Add(string.Format("{0}    {1}", Properties.Resources.BasculaMultipesaje_ticketPesoNeto,
                    Convert.ToDouble(BasculaMultipesajeContext.PesoNeto).ToString("N", CultureInfo.InvariantCulture)));

                if (registroActualizado)
                {
                    DateTime fechaModificacion = BasculaMultipesajeContext.FechaModificacion != null ? (DateTime)BasculaMultipesajeContext.FechaModificacion : DateTime.Now;
                    lineasString[0] = string.Format("{0} {1}        {2}{3}", Properties.Resources.BasculaMultipesaje_lblHora, fechaModificacion.ToShortTimeString(), Properties.Resources.BasculaMultipesaje_lblFecha, fechaModificacion.ToShortDateString());
                    lineasString.Insert(3, "          *** COPIA ***    ");
                    lineasString.Insert(4,
                        string.Format("{0} {1}        {2}{3}", Properties.Resources.BasculaMultipesaje_lblHora,
                            DateTime.Now.ToShortTimeString(), Properties.Resources.BasculaMultipesaje_lblFecha,
                            DateTime.Now.ToShortDateString()));
                    lineasString.Insert(5, string.Empty);
                    DateTime fechaPesoBruto = BasculaMultipesajeContext.FechaPesoBruto != null ? (DateTime) BasculaMultipesajeContext.FechaPesoBruto : DateTime.Now;
                    concepto = fechaPesoBruto.CompareTo(BasculaMultipesajeContext.FechaPesoTara) > 0
                        ? Properties.Resources.BasculaMultipesaje_Concepto_invertido
                        : Properties.Resources.BasculaMultipesaje_Concepto_normal;
                    lineaConcepto = 8;
                }
                else
                {
                    lineasString[0] = string.Format("{0} {1}        {2}{3}", Properties.Resources.BasculaMultipesaje_lblHora, DateTime.Now.ToShortTimeString(), Properties.Resources.BasculaMultipesaje_lblFecha, DateTime.Now.ToShortDateString());
                    concepto = BasculaMultipesajeContext.FechaPesoBruto == null
                        ? Properties.Resources.BasculaMultipesaje_Concepto_invertido
                        : Properties.Resources.BasculaMultipesaje_Concepto_normal;
                }
            }
            else
            {
                if (BasculaMultipesajeContext.PesoBruto > 0)
                {
                    lineasString.Add(string.Format("{0}   {1}", Properties.Resources.BasculaMultipesaje_ticketPesoBruto,
                        Convert.ToDouble(BasculaMultipesajeContext.PesoBruto).ToString("N", CultureInfo.InvariantCulture)));
                    concepto = Properties.Resources.BasculaMultipesaje_Concepto_normal;
                }
                else
                {
                    lineasString.Add(string.Format("{0}    {1}", Properties.Resources.BasculaMultipesaje_ticketPesoTara,
                        Convert.ToDouble(BasculaMultipesajeContext.PesoTara).ToString("N", CultureInfo.InvariantCulture)));
                    concepto = Properties.Resources.BasculaMultipesaje_Concepto_invertido;
                }
            }

            lineasString.Insert(lineaConcepto, string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_Concepto, concepto));

            if (!string.IsNullOrEmpty(BasculaMultipesajeContext.Observacion) && !string.IsNullOrWhiteSpace(BasculaMultipesajeContext.Observacion))
            {
                lineasString.Add(string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_ticketObservacion, BasculaMultipesajeContext.Observacion));
            }

            lineasString.Add(string.Format("{0} {1}", Properties.Resources.BasculaMultipesaje_ticketRecibio, SkAyudaQuienRecibe.Descripcion));

            var lineaVacia = new LineaImpresionInfo { Texto = string.Empty, Opciones = opcionesLinea };

            IList<LineaImpresionInfo> lineas = new List<LineaImpresionInfo>();
            foreach (string renglon in lineasString)
            {
                var linea = new LineaImpresionInfo
                {
                    Texto = renglon,
                    Opciones = opcionesLinea
                };

                lineas.Add(linea);
                lineas.Add(lineaVacia);
            }

            ticket.Imrpimir(lineas);
        }

        #endregion

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
                    
                    HabilitarDeshabilitarPesoBotones(true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MostrarMensaje(Properties.Resources.BasculaMateriaPrima_MsgErrorBascula, MessageImage.Warning);
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

                    //Aqui es para que se este reflejando la bascula en el display
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

        private void CommandBinding_CanExecutePaste(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }
    }
}
