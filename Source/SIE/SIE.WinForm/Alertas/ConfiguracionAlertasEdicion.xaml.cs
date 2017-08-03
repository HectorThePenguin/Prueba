using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Enums;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;

namespace SIE.WinForm.Alertas
{
    /// <summary>
    /// Interaction logic for ConfiguracionAlertasEdicion.xaml
    /// </summary>
    public partial class ConfiguracionAlertasEdicion
    {
        private List<AlertaAccionInfo> accionesDgAux;
        private List<AlertaAccionInfo> original;
        private bool actualizar, close, boolEditar;

        #region CONTRUCTOR
        public ConfiguracionAlertasEdicion(ConfiguracionAlertasGeneraInfo configuracionAlertaSelec)
        {
            InitializeComponent();
            InicializaContexto();
            accionesDgAux = new List<AlertaAccionInfo>();
            original = new List<AlertaAccionInfo>();
            ConsultarAlertas();

            if (configuracionAlertaSelec.ListaAlertaAccionInfo == null)
            {
                accionesDgAux = new List<AlertaAccionInfo>();
            }
            else
            {
                Contexto = configuracionAlertaSelec;
                accionesDgAux = Contexto.ListaAlertaAccionInfo;
                cmbAlerta.IsEnabled = false;
            }

            gridDatos.ItemsSource = accionesDgAux;
            ConsultarAcciones();
            CargaComboNivelAlerta();
            if (configuracionAlertaSelec.ListaAlertaAccionInfo != null)
            {
                original.AddRange(configuracionAlertaSelec.ListaAlertaAccionInfo);
            }
        }
        #endregion

        #region PROPIEDADES
        private ConfiguracionAlertasGeneraInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }

                return (ConfiguracionAlertasGeneraInfo)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConfiguracionAlertasGeneraInfo
            {
                AlertaInfo = new AlertaInfo
                {
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    ConfiguracionAlerta = new ConfiguracionAlertasInfo
                    {
                        AlertaConfiguracionID = 0,
                        Activo = EstatusEnum.Activo,
                        Datos = string.Empty,
                        Fuentes = string.Empty,
                        Condiciones = string.Empty,
                        Agrupador = string.Empty
                    }

                },
                AccionInfo = new AccionInfo
                {
                    ListaObservaciones = new List<ObservacionInfo>()
                },
                ListaAccionInfo = new List<AccionInfo>(),
                ConfiguracionAlertas = new ConfiguracionAlertasInfo
                {
                    AlertaConfiguracionID = 0,
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Activo = EstatusEnum.Activo,
                    Datos = string.Empty,
                    Fuentes = string.Empty,
                    Condiciones = string.Empty,
                    Agrupador = string.Empty,
                    NivelAlerta = new NivelAlertaInfo()
                },
                ListaAlertaAccionInfo = new List<AlertaAccionInfo>()
            };
        }

        /// <summary>
        /// Metodo que bloquea los campos dentro de la pantalla Configuracion Alertas Edicion
        /// </summary>
        private void BloquearPantalla()
        {
            cmbAlerta.IsEnabled = false;
            cmbAcciones.IsEnabled = false;
            cmbNivelAlerta.IsEnabled = false;
            txtDatos.IsEnabled = false;
            txtFuentes.IsEnabled = false;
            txtCondiciones.IsEnabled = false;
            txtAgrupador.IsEnabled = false;
            gridDatos.IsEnabled = false;
            GuardarAcciones.IsEnabled = false;


            if (Contexto.ConfiguracionAlertas.AlertaConfiguracionID != 0)
            {
                LimpiarCampos();
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_EdicionBloqueoDePantalla,
                    MessageBoxButton.OK, MessageImage.Warning);
                close = true;
                Close();
            }
            else if (cmbAcciones.Items.Count <= 1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_EdicionBloqueoDePantallaAccion,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
            else if (cmbAlerta.Items.Count <= 1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_EdicionBloqueoDePantallaAlerta,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
            else if (cmbNivelAlerta.Items.Count <= 1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_EdicionBloqueoDePantallaNivelDeAlerta,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        ///<summary>
        /// Verificara si la no existe acciones registradas que esten activas
        /// </summary>
        private bool ListaAccionVacia()
        {
            return (cmbAcciones.Items.Count <= 1);
        }

        ///<summary>
        /// Verificara si la no existe niveles de alerta registradas que esten activas
        /// </summary>
        private bool ListaNivelDeAlertaVacia()
        {
            return (cmbNivelAlerta.Items.Count <= 1);
        }

        ///<summary>
        /// Verificara si la no existe alertas registradas que esten activas
        /// </summary>
        private bool ListaAlertaVacia()
        {
            return (cmbAlerta.Items.Count <= 1);
        }

        ///<summary>
        /// Metodo para limpiar los campos despues de un registro
        /// </summary>
        private void LimpiarCampos()
        {
            InicializaContexto();
            accionesDgAux.Clear();
            gridDatos.ItemsSource = null;
            var accion = new AccionInfo
            {
                AccionID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            cmbAcciones.SelectedItem = accion;

            var alerta = new AlertaInfo
            {
                AlertaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            cmbAlerta.SelectedItem = alerta;

            var nivelAlerta = new NivelAlertaInfo
            {
                NivelAlertaId = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            cmbNivelAlerta.SelectedItem = nivelAlerta;
        }

        /// <summary>
        /// Metodo para cargar el comboBox de Alertas
        /// </summary>
        private void ConsultarAlertas()
        {
            try
            {
                var configAlertasPL = new ConfiguracionAlertasPL();
                var alerta = new AlertaInfo
                {
                    AlertaID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<AlertaInfo> alertasActivasCB = configAlertasPL.ObtenerTodasLasAlertasActivas();

                alertasActivasCB.Insert(0, alerta);
                cmbAlerta.ItemsSource = alertasActivasCB;
                cmbAlerta.SelectedItem = alerta;
                cmbAlerta.SelectedIndex = 0;
                if (Contexto.AlertaInfo.Descripcion == null || Contexto.AlertaInfo.AlertaID == 0)
                {
                    Contexto.AlertaInfo = alerta;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_ErrorConsultarTodasLasAlertasActivas, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }

        ///<summary>
        /// Metodo que valida que una alerta no tenga una configuracion
        /// </summary>
        private void ValidarAlertaRegistrada(ConfiguracionAlertasGeneraInfo filtros)
        {
            var configAlertaPl = new ConfiguracionAlertasPL();
            AlertaInfo alertaInfo = filtros.AlertaInfo;
            AlertaInfo result = configAlertaPl.ObtenerAlertaPorId(alertaInfo);
            if (result != null && boolEditar == false)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionAlerta_AlertaYaConfigurada, MessageBoxButton.OK, MessageImage.Error);
            }
            else
            {
                ValidarQuery();
            }
            boolEditar = false;
        }

        ///<summary>
        /// Despues de validar el query si este se valido correctamente se procede a guardar/modificar la configuracion
        /// </summary>
        private void GuardarConfiguracionAlerta()
        {
            try
            {
                var configNuevaAlerta = new ConfiguracionAlertasPL();

                ConfiguracionAlertasGeneraInfo filtros = ObtenerFiltros();
                Contexto.ListaAlertaAccionInfo = accionesDgAux;

                bool result;

                if (Contexto.ConfiguracionAlertas.AlertaConfiguracionID == 0)
                {
                    result = configNuevaAlerta.InsertarConfiguracionAlerta(filtros);
                }
                else
                {
                    //Seccion para la edicion de una configuracion de alerta
                    filtros.AlertaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    result = configNuevaAlerta.EditarConfiguracionAlerta(filtros, original);
                    actualizar = true;
                }

                if (result)
                {
                    var mensaje = SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_NuevoRegistro_Correcto,
                        MessageBoxButton.OK, MessageImage.Correct);
                    if (mensaje == MessageBoxResult.OK)
                    {
                        close = true;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_ErrorGuardarNuevaConfiguracion,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        ///<summary>
        /// Metodo que Validara el query para poder registrar una nueva configuración
        /// </summary>
        private void ValidarQuery()
        {
            try
            {
                var configAccionesPL = new ConfiguracionAlertasPL();
                ConfiguracionAlertasGeneraInfo filtros = ObtenerFiltros();
                string datosConfiguracion = filtros.ConfiguracionAlertas.Datos.ToUpper();
                if (!datosConfiguracion.Contains("AS ORGANIZACIONID"))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfiguracionAlerta_NuevoRegistro_ErrorValidacion_ColumnasObligatorias,
                        MessageBoxButton.OK, MessageImage.Error);
                }
                else
                {
                    var result = configAccionesPL.ValidacionQuery(filtros);
                    if (result)
                    {
                        GuardarConfiguracionAlerta();
                    }
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_NuevoRegistro_ErrorValidacion,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar el comboBox de Acciones
        /// </summary>
        private void ConsultarAcciones()
        {
            try
            {
                var configAccionesPL = new ConfiguracionAlertasPL();
                var accion = new AccionInfo
                {
                    AccionID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<AccionInfo> accionesActivasCB = configAccionesPL.ObtenerTodasLasAccionesActivas();
                accionesActivasCB.Insert(0, accion);
                cmbAcciones.ItemsSource = accionesActivasCB;
                cmbAlerta.SelectedItem = accion;

                Contexto.AccionInfo = new AccionInfo
                {
                    AccionID = accion.AccionID,
                    Descripcion = accion.Descripcion
                };

                if (Contexto.AccionInfo.Descripcion == null || Contexto.AccionInfo.AccionID == 0)
                {
                    Contexto.AccionInfo = accion;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_ErrorConsultarTodasLasAccionesActivas,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar el comboBox de Nivel de Alertas
        /// </summary>
        private void CargaComboNivelAlerta()
        {
            try
            {
                var nivelAlertaPl = new RolPL();
                var nivelAlerta = new NivelAlertaInfo
                {
                    NivelAlertaId = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<NivelAlertaInfo> listaNivelAlerta = nivelAlertaPl.NivelAlertaInfo();
                listaNivelAlerta.Insert(0, nivelAlerta);
                cmbNivelAlerta.ItemsSource = listaNivelAlerta;
                cmbNivelAlerta.SelectedItem = nivelAlerta;
                if (Contexto.ConfiguracionAlertas.NivelAlerta.Descripcion != null || Contexto.ConfiguracionAlertas.NivelAlerta.NivelAlertaId != 0)
                {
                    cmbNivelAlerta.SelectedItem = Contexto.ConfiguracionAlertas.NivelAlerta;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_ErrorConsultarTodasLasNivelDeAlertaActivas,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            var result = MessageBoxResult.No;
            if (close)
            {
                Contexto = null;
            }
            else
            {
                if (Contexto.ConfiguracionAlertas.AlertaConfiguracionID == 0)
                {
                    result = SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_CerrarSinGuardar,
                        MessageBoxButton.YesNo, MessageImage.Question);
                }
                else if (!actualizar)
                {
                    result = SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_CancelarEdicion,
                        MessageBoxButton.YesNo, MessageImage.Question);
                }

                if (result == MessageBoxResult.Yes || actualizar)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
                close = false;
                actualizar = false;
            }
        }

        /// <summary>
        /// Evento que se genera al cargar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfiguracionAlertasEdicion_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ListaAccionVacia() || ListaAlertaVacia() || ListaNivelDeAlertaVacia() || cmbAlerta.SelectedItem == null)
            {
                BloquearPantalla();
            }
            txtDatos.Focus();
        }

        /// <summary>
        /// Evento para cancelar la operacion y errar la ventana 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (Contexto.ConfiguracionAlertas.AlertaConfiguracionID == 0)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.ConfiguracionAlerta_CerrarSinGuardar,
                    MessageBoxButton.YesNo, MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    LimpiarCampos();
                }
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Eveto que se genera al dar click en guardar y comienza a generar las validaciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var item = (AlertaInfo)cmbAlerta.SelectedItem;
            var nivelAlertaInfo = (NivelAlertaInfo)cmbNivelAlerta.SelectedItem;

            if (item.AlertaID == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_Editar_MensajeSeleccioneUnaAlerta,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                if (txtDatos.Text == string.Empty.Trim())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfiguracionAlertas_CamposRequeridoDatos,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    ConfiguracionAlertasGeneraInfo filtros = ObtenerFiltros();
                    if (txtFuentes.Text == string.Empty.Trim())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ConfiguracionAlertas_CamposRequeridoFuentes,
                            MessageBoxButton.OK, MessageImage.Warning);
                    }
                    else
                    {
                        if (txtCondiciones.Text == string.Empty.Trim())
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ConfiguracionAlertas_CamposRequeridoCondiciones,
                                MessageBoxButton.OK, MessageImage.Warning);
                        }
                        else
                        {
                            if (nivelAlertaInfo.NivelAlertaId == 0)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.ConfiguracionAlerta_Editar_MensajeSeleccioneUnNivelAlerta,
                                    MessageBoxButton.OK, MessageImage.Warning);
                            }
                            else
                            {
                                if (accionesDgAux.Count <= 0)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.ConfiguracionAlertas_AccionRequerida,
                                        MessageBoxButton.OK, MessageImage.Warning);
                                }
                                else
                                {
                                    filtros.ConfiguracionAlertas.Agrupador = txtAgrupador.Text;
                                    if (Contexto.ConfiguracionAlertas.AlertaConfiguracionID != 0)
                                    {
                                        boolEditar = true;
                                    }

                                    ValidarAlertaRegistrada(filtros);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ConfiguracionAlertasGeneraInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Elimina una accion del DataGrid por medio de una Confirmación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminarAccionDelGrid_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(this,
                Properties.Resources.ConfiguracionAlerta_EliminarAccionDataGrid,
                MessageBoxButton.YesNo, MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var botonEditar = (Button)e.Source;
                try
                {
                    var cuentaGastosInfoSelecionado = (AlertaAccionInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                    if (cuentaGastosInfoSelecionado != null)
                    {
                        accionesDgAux.RemoveAll(x => x.AccionId == cuentaGastosInfoSelecionado.AccionId);
                        gridDatos.ItemsSource = null;
                        gridDatos.ItemsSource = accionesDgAux;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfiguracionAlerta_ErrorEliminarAccionesDelDataGrid,
                        MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Evento que se genera al precionar el boton >> que registra una accion
        /// al datagrd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgregarAlDataGrid(object sender, RoutedEventArgs e)
        {
            var item = (AccionInfo)cmbAcciones.SelectedItem;

            AlertaAccionInfo alertaAccion = new AlertaAccionInfo
            {
                AccionId = item.AccionID,
                Descripcion = item.Descripcion
            };

            if (alertaAccion.AccionId == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionAlerta_Editar_MensajeSeleccioneUnaAccion, MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                if (accionesDgAux.Count <= 0)
                {
                    accionesDgAux.Add(alertaAccion);
                }
                else if (accionesDgAux.Exists(x => x.AccionId == alertaAccion.AccionId))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfiguracionAlerta_Editar_MensajeAccionRepetida,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    accionesDgAux.Add(alertaAccion);
                }
            }

            gridDatos.ItemsSource = null;
            gridDatos.ItemsSource = accionesDgAux;
            var accion = new AccionInfo
            {
                AccionID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            cmbAcciones.SelectedItem = accion;
        }

        #endregion
    }
}