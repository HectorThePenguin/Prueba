using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CuentaAlmacenSubFamiliaEdicion.xaml
    /// </summary>
    public partial class CuentaAlmacenSubFamiliaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaAlmacenSubFamiliaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CuentaAlmacenSubFamiliaInfo)DataContext;
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
        public CuentaAlmacenSubFamiliaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad CuentaAlmacenSubFamilia Existente
        /// </summary>
        /// <param name="cuentaAlmacenSubFamiliaInfo"></param>
        public CuentaAlmacenSubFamiliaEdicion(CuentaAlmacenSubFamiliaInfo cuentaAlmacenSubFamiliaInfo)
        {
            InitializeComponent();
            cuentaAlmacenSubFamiliaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = cuentaAlmacenSubFamiliaInfo;
            skAyudaAlmacen.IsEnabled = false;
            skAyudaSubFamilia.IsEnabled = false;
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
            skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
            skAyudaSubFamilia.ObjetoNegocio = new SubFamiliaPL();
            skAyudaCuentaSAP.ObjetoNegocio = new CuentaSAPPL();

            skAyudaAlmacen.AyudaConDatos += (o, args) =>
                {
                    Contexto.Almacen.TipoAlmacen = new TipoAlmacenInfo();
                    Contexto.Almacen.TipoAlmacenID = 0;
                };

            skAyudaSubFamilia.AyudaConDatos += (o, args) =>
            {
                Contexto.SubFamilia.Familia = new FamiliaInfo();
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
            Contexto = new CuentaAlmacenSubFamiliaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                SubFamilia = new SubFamiliaInfo
                    {
                        Familia = new FamiliaInfo()
                    },
                CuentaSAP = new CuentaSAPInfo(),
                Almacen = new AlmacenInfo
                    {
                        Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
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
                if (string.IsNullOrWhiteSpace(txtCuentaAlmacenSubFamiliaID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaAlmacenSubFamiliaEdicion_MsgCuentaAlmacenSubFamiliaIDRequerida;
                    txtCuentaAlmacenSubFamiliaID.Focus();
                }
                else if (Contexto.Almacen == null || Contexto.Almacen.AlmacenID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaAlmacenSubFamiliaEdicion_MsgAlmacenIDRequerida;
                    skAyudaAlmacen.AsignarFoco();
                }
                else if (Contexto.SubFamilia == null || Contexto.SubFamilia.SubFamiliaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaAlmacenSubFamiliaEdicion_MsgSubFamiliaIDRequerida;
                    skAyudaSubFamilia.AsignarFoco();
                }
                else if (Contexto.CuentaSAP == null || Contexto.CuentaSAP.CuentaSAPID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaAlmacenSubFamiliaEdicion_MsgCuentaSAPIDRequerida;
                    skAyudaCuentaSAP.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaAlmacenSubFamiliaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int cuentaAlmacenSubFamiliaId = Extensor.ValorEntero(txtCuentaAlmacenSubFamiliaID.Text);

                    var cuentaAlmacenSubFamiliaBL = new CuentaAlmacenSubFamiliaBL();
                    IList<CuentaAlmacenSubFamiliaInfo> cuentasAlmacen = cuentaAlmacenSubFamiliaBL.ObtenerCostosSubFamilia(Contexto.Almacen.AlmacenID);

                    CuentaAlmacenSubFamiliaInfo cuentaAlmacenSubFamilia =
                        cuentasAlmacen.FirstOrDefault(
                            cuenta => cuenta.SubFamilia.SubFamiliaID == Contexto.SubFamilia.SubFamiliaID);

                    if (cuentaAlmacenSubFamilia != null && (cuentaAlmacenSubFamiliaId == 0 || cuentaAlmacenSubFamiliaId != cuentaAlmacenSubFamilia.CuentaAlmacenSubFamiliaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CuentaAlmacenSubFamiliaEdicion_MsgDescripcionExistente, cuentaAlmacenSubFamilia.CuentaAlmacenSubFamiliaID);
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
                    var cuentaAlmacenSubFamiliaPL = new CuentaAlmacenSubFamiliaBL();
                    cuentaAlmacenSubFamiliaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CuentaAlmacenSubFamiliaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.CuentaAlmacenSubFamilia_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.CuentaAlmacenSubFamilia_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos

    }
}

