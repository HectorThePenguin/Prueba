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
    /// Interaction logic for SupervisorEnfermeriaEdicion.xaml
    /// </summary>
    public partial class SupervisorEnfermeriaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SupervisorEnfermeriaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (SupervisorEnfermeriaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;
        
        /// <summary>
        /// Propiedad para validar si cambia la organización
        /// del contexto
        /// </summary>
        private int organizacionID;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public SupervisorEnfermeriaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           AgregaAyudas();
        }

        /// <summary>
        /// Constructor para editar una entidad SupervisorEnfermeria Existente
        /// </summary>
        /// <param name="supervisorEnfermeriaInfo"></param>
        public SupervisorEnfermeriaEdicion(SupervisorEnfermeriaInfo supervisorEnfermeriaInfo)
        {
            InitializeComponent();

            organizacionID = supervisorEnfermeriaInfo.Organizacion.OrganizacionID;
            OrganizacionInfo organizacion = supervisorEnfermeriaInfo.Organizacion; 

            AgregaAyudas();
            supervisorEnfermeriaInfo.Organizacion = organizacion;
            supervisorEnfermeriaInfo.Enfermeria.OrganizacionInfo = organizacion;
            supervisorEnfermeriaInfo.Operador.Organizacion = organizacion;
            supervisorEnfermeriaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = supervisorEnfermeriaInfo;
            skAyudaOrganizacion.IsEnabled = false;

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
            skAyudaEnfermeria.ObjetoNegocio = new EnfermeriaPL();
            skAyudaOperador.ObjetoNegocio = new OperadorPL();

            //ayuda Organización
            skAyudaOrganizacion.AyudaConDatos += (o, args) => AyudaConDatos();
            skAyudaOrganizacion.AyudaLimpia += (o, args) => AyudaLimpia();

            skAyudaEnfermeria.PuedeBuscar = PuedeBuscar;
            skAyudaOperador.PuedeBuscar = PuedeBuscar;
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
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var organizacion = new OrganizacionInfo
                                   {
                                       TipoOrganizacion = new TipoOrganizacionInfo
                                                              {
                                                                  TipoOrganizacionID =
                                                                      Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                                                              }
                                   };
            Contexto = new SupervisorEnfermeriaInfo
            {
                Organizacion = organizacion,
                Enfermeria = InicializaEnfermeria(organizacion),
                Operador = InicializaOpeador(organizacion),
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        private EnfermeriaInfo InicializaEnfermeria(OrganizacionInfo organizacion)
        {
            return new EnfermeriaInfo
            {
                OrganizacionInfo = organizacion
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        private OperadorInfo InicializaOpeador(OrganizacionInfo organizacion)
        {
            return new OperadorInfo
            {
                Rol = new RolInfo(),
                Organizacion = organizacion
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
                if (string.IsNullOrWhiteSpace(txtSupervisorEnfermeriaID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.SupervisorEnfermeriaEdicion_MsgSupervisorEnfermeriaIDRequerida;
                    txtSupervisorEnfermeriaID.Focus();
                }
                else if (Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.SupervisorEnfermeriaEdicion_MsgOrganizacicionRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (Contexto.Enfermeria.EnfermeriaID == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.SupervisorEnfermeriaEdicion_MsgEnfermeriaIDRequerida;
                    skAyudaEnfermeria.AsignarFoco();
                }
                else if (Contexto.Operador.OperadorID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.SupervisorEnfermeriaEdicion_MsgOperadorIDRequerida;
                    skAyudaOperador.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.SupervisorEnfermeriaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int supervisorEnfermeriaId = Extensor.ValorEntero(txtSupervisorEnfermeriaID.Text);

                    var supervisorEnfermeriaBL = new SupervisorEnfermeriaBL();
                    SupervisorEnfermeriaInfo supervisorEnfermeria = supervisorEnfermeriaBL.ObtenerPorEnfermeriaOperador(Contexto);

                    if (supervisorEnfermeria != null && (supervisorEnfermeriaId == 0 || supervisorEnfermeriaId != supervisorEnfermeria.SupervisorEnfermeriaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.SupervisorEnfermeriaEdicion_MsgDescripcionExistente, supervisorEnfermeria.SupervisorEnfermeriaID);
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
                    int supervisorEnfermeriaID = Contexto.SupervisorEnfermeriaID;
                    var supervisorEnfermeriaPL = new SupervisorEnfermeriaBL();
                    Contexto.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    Contexto.EnfermeriaID = Contexto.Enfermeria.EnfermeriaID;
                    Contexto.OperadorID = Contexto.Operador.OperadorID;

                    supervisorEnfermeriaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (supervisorEnfermeriaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.SupervisorEnfermeria_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.SupervisorEnfermeria_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los datos de la entidad Operador 
        /// </summary>
        private void AgregaAyudas()
        {
            skAyudaEnfermeria.ObjetoNegocio = new EnfermeriaPL();
            skAyudaOperador.ObjetoNegocio = new OperadorPL();
        }

        /// <summary>
        /// Asigna la organización  a las  ayudas que dependan
        /// del campo organización.
        /// </summary>
        private void AyudaConDatos()
        {
            var organizacion =
                skAyudaOrganizacion.Contexto as OrganizacionInfo;

            skAyudaEnfermeria.LimpiarCampos();
            skAyudaOperador.LimpiarCampos();

            Contexto.Enfermeria = InicializaEnfermeria(organizacion);
            Contexto.Operador = InicializaOpeador(organizacion);
            BindearDependencias();
        }

        /// <summary>
        /// Limpia los valores de las ayudas que dependen de la organización.
        /// </summary>
        private void AyudaLimpia()
        {
            if (!skAyudaOrganizacion.BusquedaActivada)
            {
                int claveOrganizacion = string.IsNullOrWhiteSpace(skAyudaOrganizacion.Clave)
                                            ? 0
                                            : int.Parse(skAyudaOrganizacion.Clave);

                if (claveOrganizacion != organizacionID)
                {
                    Contexto.Enfermeria = InicializaEnfermeria(new OrganizacionInfo());
                    Contexto.Operador = InicializaOpeador(new OrganizacionInfo());
                    BindearDependencias();
                }
            }
        }

        /// <summary>
        /// Refresca los valores de las ayudas que tienen
        /// dependencia con el campo organización.
        /// </summary>
        private void BindearDependencias()
        {
            skAyudaEnfermeria.Contexto = Contexto.Enfermeria;
            skAyudaOperador.Contexto = Contexto.Operador;
        }

        /// <summary>
        /// Valida que se haya capturado una organización.
        /// </summary>
        /// <returns></returns>
        private bool PuedeBuscar()
        {
            bool puede = Contexto.Organizacion != null && Contexto.Organizacion.OrganizacionID > 0;
            if (!puede)
            {
                SkMessageBox.Show(this, Properties.Resources.SupervisorEnfermeriaEdicion_MsgOrganizacicionRequerida, MessageBoxButton.OK, MessageImage.Warning);
            }
            return puede;
        }

     
        #endregion Métodos
    }
}

