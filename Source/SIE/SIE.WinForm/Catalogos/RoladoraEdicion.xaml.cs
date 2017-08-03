using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.BL;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for RoladoraEdicion.xaml
    /// </summary>
    public partial class RoladoraEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private RoladoraInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (RoladoraInfo) DataContext;
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
        public RoladoraEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad Roladora Existente
        /// </summary>
        /// <param name="roladoraInfo"></param>
        public RoladoraEdicion(RoladoraInfo roladoraInfo)
        {
           InitializeComponent();
           roladoraInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = roladoraInfo;
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
            skAyudaOrganizacion.AsignarFoco();
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaOrganizacion.AyudaConDatos += (o, args) =>
            {
                Contexto.Organizacion.TipoOrganizacion = new TipoOrganizacionInfo
                {
                    TipoOrganizacionID = Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                };
                Contexto.Organizacion.ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                        {
                            new TipoOrganizacionInfo
                                {
                                    TipoOrganizacionID = Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                                }
                        };
            };
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
            Contexto = new RoladoraInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo
                    {
                        TipoOrganizacionID = Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                    },
                    ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                                {
                                    new TipoOrganizacionInfo
                                        {
                                            TipoOrganizacionID = Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()   
                                        }
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
                if (string.IsNullOrWhiteSpace(txtRoladoraID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.RoladoraEdicion_MsgRoladoraIDRequerida;
                    txtRoladoraID.Focus();
                }
                else if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.RoladoraEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.RoladoraEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.RoladoraEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int roladoraId = Extensor.ValorEntero(txtRoladoraID.Text);
                    string descripcion = txtDescripcion.Text;

                    var roladoraPL = new RoladoraBL();
                    RoladoraInfo roladora = roladoraPL.ObtenerPorDescripcion(descripcion);

                    if (roladora != null && (roladoraId == 0 || roladoraId != roladora.RoladoraID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.RoladoraEdicion_MsgDescripcionExistente, roladora.RoladoraID);
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
                    var roladoraPL = new RoladoraBL();
                    roladoraPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.RoladoraID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Roladora_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Roladora_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }       

        #endregion Métodos

    }
}

