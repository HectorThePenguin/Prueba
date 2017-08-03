using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ProblemaEdicion.xaml
    /// </summary>
    public partial class ProblemaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProblemaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (ProblemaInfo) DataContext;
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
        public ProblemaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           CargaComboTipoProblema();
        }

        /// <summary>
        /// Constructor para editar una entidad Problema Existente
        /// </summary>
        /// <param name="problemaInfo"></param>
        public ProblemaEdicion(ProblemaInfo problemaInfo)
        {
            InitializeComponent();
            problemaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = problemaInfo;
            CargaComboTipoProblema();
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
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProblemaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoProblema =  new TipoProblemaInfo()
            };
            txtDescripcion.Focus();
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
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (Contexto.TipoProblema.TipoProblemaId == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaEdicion_MsgTipoProblemaIDRequerida;
                    cmbTipoProblema.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int problemaId = Contexto.ProblemaID;
                    string descripcion = Contexto.Descripcion;

                    using (var problemaBL = new ProblemaBL())
                    {
                        ProblemaInfo problema = problemaBL.ObtenerPorDescripcion(descripcion);

                        if (problema != null && (problemaId == 0 || problemaId != problema.ProblemaID))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.ProblemaEdicion_MsgDescripcionExistente,
                                                    problema.ProblemaID);
                        }
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
                    using (var problemaBL = new ProblemaBL())
                    {
                        int problemaID = Contexto.ProblemaID;
                        Contexto.TipoProblemaID = Contexto.TipoProblema.TipoProblemaId;
                        problemaBL.Guardar(Contexto);
                        SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                        if (problemaID != 0)
                        {
                            confirmaSalir = false;
                            Close();
                        }
                        else
                        {
                            InicializaContexto();
                        }
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Problema_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Problema_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Problema 
        /// </summary>
        private void CargaComboTipoProblema()
        {
            using (var tipoProblemaBL = new TipoProblemaBL())
            {
                var tipoProblema = new TipoProblemaInfo
                {
                    TipoProblemaId = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<TipoProblemaInfo> listaTipoProblema = tipoProblemaBL.ObtenerTodos(EstatusEnum.Activo);
                listaTipoProblema.Insert(0, tipoProblema);
                cmbTipoProblema.ItemsSource = listaTipoProblema;
                if (Contexto.ProblemaID == 0)
                {
                    cmbTipoProblema.SelectedItem = tipoProblema;
                }
            }
        }

        #endregion Métodos


    }
}

