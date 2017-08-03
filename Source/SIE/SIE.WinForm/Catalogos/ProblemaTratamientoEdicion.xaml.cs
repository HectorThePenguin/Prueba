using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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
    /// Interaction logic for ProblemaTratamientoEdicion.xaml
    /// </summary>
    public partial class ProblemaTratamientoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProblemaTratamientoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProblemaTratamientoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Se utiliza para consultar los Tratamientos
        /// </summary>
        private List<TratamientoInfo> listaTratamientos;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProblemaTratamientoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad ProblemaTratamiento Existente
        /// </summary>
        /// <param name="problemaTratamientoInfo"></param>
        public ProblemaTratamientoEdicion(ProblemaTratamientoInfo problemaTratamientoInfo)
        {
            InitializeComponent();
            problemaTratamientoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = problemaTratamientoInfo;
            skAyudaProblema.IsEnabled = false;
            skAyudaOrganizacion.IsEnabled = false;
            cmbSexo.IsEnabled = false;
            ituTratamiento.IsEnabled = false;
            //skAyudaTratamiento.IsEnabled = false;
            lblRequeridoProblema.Content = string.Empty;
            lblRequeridoSexo.Content = string.Empty;
            lblRequeridoTratamiento.Content = string.Empty;
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
            skAyudaProblema.ObjetoNegocio = new ProblemaBL();
            //skAyudaTratamiento.ObjetoNegocio = new TratamientoPL();
            skAyudaProblema.AyudaConDatos += (o, args) =>
            {
                var contextoAyuda = skAyudaProblema.Contexto as ProblemaInfo;
                contextoAyuda.TipoProblema = new TipoProblemaInfo();
            };
            //skAyudaTratamiento.AyudaConDatos += (o, args) =>
            //{
            //    var contextoAyuda = skAyudaTratamiento.Contexto as TratamientoInfo;
            //    contextoAyuda.Organizacion = Contexto.Organizacion;
            //    contextoAyuda.Sexo = Contexto.Tratamiento.Sexo;
            //};

            ConsultarTratamientos();
            //txtDescripcion.Focus();
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
        private void CargarTratamiento()
        {
            TratamientoInfo tratamiento =
                listaTratamientos.FirstOrDefault(
                    tra =>
                    tra.Sexo == Contexto.Tratamiento.Sexo && tra.CodigoTratamiento == Contexto.Tratamiento.CodigoTratamiento
                    && tra.Organizacion.OrganizacionID == Contexto.Tratamiento.Organizacion.OrganizacionID);

            if(tratamiento == null)
            {
                ituTratamiento.Value = 0;
                Contexto.Tratamiento.TratamientoID = 0;
                Contexto.Tratamiento.CodigoTratamiento = 0;
                Contexto.TratamientoID = 0;
                ituRangoInicial.Value = 0;
                ituRangoFinal.Value = 0;
                SkMessageBox.Show(this, Properties.Resources.ProblemaTratamientoEdicion_MsgTratamientoInexistente, MessageBoxButton.OK, MessageImage.Warning);
                return;
            }
            Contexto.Tratamiento.TratamientoID = tratamiento.TratamientoID;
            Contexto.Tratamiento.CodigoTratamiento = tratamiento.CodigoTratamiento;
            ituRangoInicial.Value = tratamiento.RangoInicial;
            ituRangoFinal.Value = tratamiento.RangoFinal;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void ConsultarTratamientos()
        {
            var tratamientoPL = new TratamientoPL();
            IList<TratamientoInfo> tratamientosConsultar = tratamientoPL.ObtenerTodos();
            listaTratamientos = tratamientosConsultar.ToList();
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProblemaTratamientoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Problema = new ProblemaInfo(),
                Tratamiento = new TratamientoInfo
                {
                    Organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = SIE.Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode() }
                    },
                    CodigoTratamiento = 0
                },
                Organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = SIE.Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode() }
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
                if (string.IsNullOrWhiteSpace(txtProblemaTratamientoID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgProblemaTratamientoIDRequerida;
                    txtProblemaTratamientoID.Focus();
                }
                else if (Contexto.Tratamiento.Organizacion == null || Contexto.Tratamiento.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgErorrSinOrganizacion;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (Contexto.Problema == null || Contexto.Problema.ProblemaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgProblemaIDRequerida;
                    skAyudaProblema.AsignarFoco();
                }
                else if (Contexto.Tratamiento.Sexo == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgSexoRequerida;
                    cmbSexo.Focus();
                }
                else if (Contexto.Tratamiento == null || Contexto.Tratamiento.CodigoTratamiento == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgTratamientoIDRequerida;
                    //skAyudaTratamiento.AsignarFoco();
                    ituTratamiento.Focus();
                }
                else if (Contexto.Tratamiento == null || Contexto.Tratamiento.TratamientoID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgTratamientoIDRequerida;
                    //skAyudaTratamiento.AsignarFoco();
                    ituTratamiento.Focus();
                }
                else if (!ituDias.Value.HasValue || (ituDias.Value.HasValue && ituDias.Value.Value == 0) || Contexto.Dias == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgDiasRequerida;
                    ituDias.Focus();
                }
                else if (!ituOrden.Value.HasValue || (ituOrden.Value.HasValue && ituOrden.Value.Value == 0) || Contexto.Orden == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgOrdenRequerida;
                    ituOrden.Focus();
                }

                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaTratamientoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int problemaTratamientoId = Extensor.ValorEntero(txtProblemaTratamientoID.Text);
                    Contexto.ProblemaID = Contexto.Problema.ProblemaID;

                    var problemaTratamientoBL = new ProblemaTratamientoBL();
                    ProblemaTratamientoInfo problemaTratamiento =
                        problemaTratamientoBL.ObtenerProblemaTratamientoExiste(Contexto);

                    if (problemaTratamiento != null && (problemaTratamientoId == 0 || problemaTratamientoId != problemaTratamiento.ProblemaTratamientoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ProblemaTratamientoEdicion_MsgDescripcionExistente, problemaTratamiento.ProblemaTratamientoID);
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


        /// <summary>%
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    int problemaTratamientoID = Contexto.ProblemaTratamientoID;
                    var problemaTratamientoBL = new ProblemaTratamientoBL();
                    Contexto.ProblemaID = Contexto.Problema.ProblemaID;
                    Contexto.TratamientoID = Contexto.Tratamiento.TratamientoID;
                    problemaTratamientoBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (problemaTratamientoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.ProblemaTratamiento_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ProblemaTratamiento_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

        private void ItuTratamiento_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (Contexto.Tratamiento != null)
                {
                    if (Contexto.Tratamiento.CodigoTratamiento > 0)
                    {
                        CargarTratamiento();
                        if (Contexto.Tratamiento.CodigoTratamiento == 0)
                        {
                            e.Handled = true;
                            //skAyudaTratamiento.AsignarFoco();
                            ituTratamiento.Focus();
                        }
                    }
                }
            }
        }

        private void CmbSexo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Contexto.Tratamiento != null && Contexto.Tratamiento.CodigoTratamiento > 0)
            {
                //ituTratamiento.Value = 0;
                ituRangoInicial.Value = 0;
                ituRangoFinal.Value = 0;
                //skAyudaTratamiento.LimpiarCampos();
                ituTratamiento.Focus();
            }

        }
    }
}

