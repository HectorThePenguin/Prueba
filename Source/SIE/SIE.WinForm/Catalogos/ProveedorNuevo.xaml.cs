using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using SIE.Services.Info.Enums;
using System.Data;
using SIE.Base.Log;
using SIE.WinForm.Auxiliar;
using System.Text.RegularExpressions;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ProveedorNuevo.xaml
    /// </summary>
    public partial class ProveedorNuevo
    {
        private string Sociedad = "212";
        private IList<TipoProveedorInfo> listaTipoProveedor;

        public ProveedorNuevo()
        {
            InitializeComponent();
            InicializaContexto();

        }

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProveedorInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProveedorInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var tipoProv = new TipoProveedorPL();
            listaTipoProveedor = tipoProv.ObtenerTodos();
            cmbTipoProveedor.IsEnabled = true;
            cmbTipoProveedor.ItemsSource = listaTipoProveedor;
            cmbTipoProveedor.IsEnabled = false;
            cmbEstatus.IsEnabled = false;

        }



        #region EVENTOS

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarProveedor();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarProveedor();
        }

        private void cmbTipoProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoProveedor.SelectedValue != null
                && (cmbTipoProveedor.SelectedValue.ToString().Trim() == TipoProveedorEnum.ProveedoresFletes.GetHashCode().ToString().Trim()
                || cmbTipoProveedor.SelectedValue.ToString().Trim() == TipoProveedorEnum.Comisionistas.GetHashCode().ToString().Trim()
                ))
            {
                txtCorreo.IsEnabled = true;
            }
            else
            {
                txtCorreo.Text = "";
                txtCorreo.IsEnabled = false;
            }
        }

        private void GuardarProveedor()
        {
            if (ValidacionesPrevias())
            {
                Contexto.Activo = EstatusEnum.Activo;
                ProveedorPL prov = new ProveedorPL();
                prov.Guardar(Contexto);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.Proveedor_MsgGuardarExitoso,
                            MessageBoxButton.OK,
                            MessageImage.Correct);
                InicializaContexto();
                txtIdProveedor.Text = string.Empty;
                txtIdProveedor.Focus();
                cmbTipoProveedor.IsEnabled = false;

            }

        }

        private bool ValidacionesPrevias()
        {
            if (String.IsNullOrEmpty(Contexto.Descripcion.Trim()))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.Proveedor_DescripcionRequerido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                return false;
            }

            if (Contexto.TipoProveedor == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.Proveedor_TipoProveedorRequerido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                return false;
            }

            if (Contexto.Correo != null && !ValidarCorreoElectronico(Contexto.Correo))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.Proveedor_CorreoRequerido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                return false;
            }

            return true;
        }
        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProveedorInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
            };
        }


        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                BuscarProveedor();
            }
        }

        private void txtIdProveedor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidarSoloNumeros(e.Text);
        }

        private bool ValidarSoloNumeros(string valor)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(valor));

            return !string.IsNullOrWhiteSpace(valor) && (ascci < 48 || ascci > 57);
        }


        private Boolean ValidarCorreoElectronico(String email)
        {
            String expresion;
            Boolean resultado = false;
            if (email.Trim().Length > 0)
            {
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(email, expresion))
                {
                    if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
                else
                {
                    resultado = false;
                }
            }
            else
            {
                resultado = true;
            }

            return resultado;
        }

        private void BuscarProveedor()
        {
            string IdProveedor = txtIdProveedor.Text.Trim();

            if (IdProveedor.Length > 0)
            {
                IdProveedor = IdProveedor.PadLeft(10, '0');
                txtIdProveedor.Text = IdProveedor;

                //Se valida si existen en base de datos local
                var proveedorPl = new ProveedorPL();
                ProveedorInfo proveedor = proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo()
                                                        {
                                                            CodigoSAP = IdProveedor
                                                        });


                if (proveedor != null)
                {
                    if (proveedor.Activo == EstatusEnum.Inactivo)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.Proveedor_ExisteCodigoInactivo,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.Proveedor_ExisteCodigo,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                    }
                    txtIdProveedor.Text = string.Empty;
                    txtIdProveedor.Focus();
                    InicializaContexto();
                    return;
                }

                //Consultamos el proveedor en SAP
                DataTable resultado = proveedorPl.VerificaProveedorSAP(Sociedad, IdProveedor);

                if (resultado.Rows.Count > 0)
                {
                    Contexto.Descripcion = resultado.Rows[0]["Descripcion"].ToString();
                    Contexto.CodigoSAP = IdProveedor;
                    Contexto.Activo = EstatusEnum.Activo;
                    cmbEstatus.IsEnabled = false;

                    string bloqueado = resultado.Rows[0]["Bloqueado"].ToString();
                    if (bloqueado == "X")
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.Proveedor_ExisteCodigoInactivoSAP,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                        InicializaContexto();
                        txtIdProveedor.Text = string.Empty;
                        txtIdProveedor.Focus();
                        return;
                    }

                    bool valido = false;
                    foreach (TipoProveedorInfo row in listaTipoProveedor)
                    {
                        if (row.CodigoGrupoSap.ToString() == resultado.Rows[0]["GrupoID"].ToString())
                        {

                            TipoProveedorInfo tp = new TipoProveedorInfo();
                            tp.TipoProveedorID = row.TipoProveedorID;
                            Contexto.TipoProveedor = tp;
                            cmbTipoProveedor.IsEnabled = false;
                            valido = true;
                        }
                    }
                    if (!valido)
                        cmbTipoProveedor.IsEnabled = true;

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.Proveedor_CodigoInvalido,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                    InicializaContexto();
                    txtIdProveedor.Text = string.Empty;
                    txtIdProveedor.Focus();

                }


            }


        }


        #endregion METODOS


    }
}
