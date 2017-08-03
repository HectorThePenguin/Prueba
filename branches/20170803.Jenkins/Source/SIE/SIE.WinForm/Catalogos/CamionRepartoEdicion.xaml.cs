using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CamionRepartoEdicion.xaml
    /// </summary>
    public partial class CamionRepartoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CamionRepartoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CamionRepartoInfo) DataContext;
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
        public CamionRepartoEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad CamionReparto Existente
        /// </summary>
        /// <param name="camionRepartoInfo"></param>
        public CamionRepartoEdicion(CamionRepartoInfo camionRepartoInfo)
        {
           InitializeComponent();
           camionRepartoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = camionRepartoInfo;
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
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaCentroCosto.ObjetoNegocio = new CentroCostoBL();
            skAyudaOrganizacion.AsignarFoco();
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CamionRepartoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo(),
                CentroCosto = new CentroCostoInfo()
            };
            skAyudaOrganizacion.AsignarFoco();
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
                if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionRepartoEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.NumeroEconomico))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionRepartoEdicion_MsgNumeroEconomicoRequerida;
                    txtNumeroEconomico.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CamionRepartoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int camionRepartoId = Contexto.CamionRepartoID;
                    string descripcion = Contexto.NumeroEconomico;
                    int organizacionID = Contexto.Organizacion.OrganizacionID;

                    using(var camionRepartoBL = new CamionRepartoBL())
                    {
                        CamionRepartoInfo camionReparto = camionRepartoBL.ObtenerPorDescripcion(descripcion, organizacionID);

                        if (camionReparto != null && (camionRepartoId == 0 || camionRepartoId != camionReparto.CamionRepartoID))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.CamionRepartoEdicion_MsgDescripcionExistente, camionReparto.CamionRepartoID);
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
                    using(var camionRepartoBL = new CamionRepartoBL())
                    {
                        int camionRepartoID = Contexto.CamionRepartoID;
                        Contexto.OrganizacionID = Contexto.Organizacion.OrganizacionID;
                        Contexto.CentroCostoID = Contexto.CentroCosto.CentroCostoID;
                        camionRepartoBL.Guardar(Contexto);
                        SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                        if (camionRepartoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.CamionReparto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.CamionReparto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos
    }
}

