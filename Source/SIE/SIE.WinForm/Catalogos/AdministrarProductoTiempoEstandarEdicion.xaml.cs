using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AdministrarProductoTiempoEstandarEdicion.xaml
    /// </summary>
    public partial class AdministrarProductoTiempoEstandarEdicion 
    {
        private SKAyuda<ProductoInfo> skAyudaProductos;
        private bool esEdicion;
        private bool confirmacion;

        private ProductoTiempoEstandarInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProductoTiempoEstandarInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto =
               new ProductoTiempoEstandarInfo
               {
                   Estatus = Services.Info.Enums.EstatusEnum.Activo,
                   Producto = new ProductoInfo(),
                   ProductoTiempoEstandarID = 0,
                   Tiempo = string.Empty
               };
        }

        public AdministrarProductoTiempoEstandarEdicion()
        {
            InitializeComponent();
            InicialiazarComboHoras();
            confirmacion = true;
            txtID.Text = "0";
            cmbEstatus.ItemsSource = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>();
            cmbEstatus.SelectedItem = EstatusEnum.Activo;
            cmbEstatus.IsEnabled = false;
            cmbHora.SelectedIndex = 0;
            cmbMinutos.SelectedIndex = 0;
            //cmbEstatus.SelectedIndex = 1;
            CargarAyudaProductos();
            esEdicion = false;
            SplAyudaProducto.Focus();
            skAyudaProductos.AsignarFoco();
        }

        public void InicialiazarComboHoras()
        {
            for(int i=0; i < 24; i++)
            {
                cmbHora.Items.Add(i.ToString().PadLeft(2,'0'));
            }

            for (int i = 0; i < 60; i++)
            {
                cmbMinutos.Items.Add(i.ToString().PadLeft(2, '0'));
            }
        }

        public void CargarHora(string hora, string segundos)
        {
            cmbHora.SelectedItem = hora;
            cmbMinutos.SelectedItem = segundos;
        }

        public AdministrarProductoTiempoEstandarEdicion(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            InitializeComponent();
            InicialiazarComboHoras();
            confirmacion = true;
            esEdicion = true;
            txtID.Text = productoTiempoEstandarInfo.ProductoTiempoEstandarID.ToString();
            cmbEstatus.ItemsSource = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>();
            cmbEstatus.SelectedItem = productoTiempoEstandarInfo.Estatus;
            //cmbEstatus.SelectedIndex = 1;
            CargarAyudaProductos();
            //skAyudaProductos.AyudaSimple = true;
            skAyudaProductos.Clave = productoTiempoEstandarInfo.Producto.ProductoId.ToString();
            skAyudaProductos.Descripcion = productoTiempoEstandarInfo.Producto.Descripcion;
            skAyudaProductos.IsEnabled = false;
            string[] tiempo = productoTiempoEstandarInfo.Tiempo.Split(':');
            CargarHora(tiempo[0], tiempo[1]);
            //SplAyudaProducto.Contexto = productoTiempoEstandarInfo.Producto;
        }

        private void CargarAyudaProductos()
        {
            var productoInfo = new ProductoInfo
            {
                ProductoId = 0,
                Activo = EstatusEnum.Activo
            };
            ProductoPL AyudaPL = new ProductoPL();
            skAyudaProductos = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadProductoID"
                                                   , "PropiedadDescripcionMuestreoTamanoFibra",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaLeyendaProducto,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoTitulo,
            };
            skAyudaProductos.MetodoPorId = "ObtenerPorID";
            skAyudaProductos.MetodoPaginadoBusqueda = "ObtenerPorPagina";
            skAyudaProductos.ObtenerDatos += ObtenerDatosProducto;
            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaProductos);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(string clave)
        {
            try
            {
                skAyudaProductos.Info = new ProductoInfo
                {
                    ProductoId = 0,
                    Activo = EstatusEnum.Activo
                };
                //TxtDestino.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosProducto, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        private bool ValidarCamposObligatorios()
        {
            bool resultado = false;
            if (skAyudaProductos.Clave.Trim().Length > 0)
            {
                if(cmbHora.SelectedIndex > 0 || cmbMinutos.SelectedIndex > 0)
                {
                    resultado = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionProductoTiempoEstandar_EdicionMsgErrorCapturarTiempo, MessageBoxButton.OK,
                    MessageImage.Error);
                    cmbHora.Focus();
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionProductoTiempoEstandar_EdicionMsgErrorCapturarProducto, MessageBoxButton.OK,
                    MessageImage.Error);
                SplAyudaProducto.Focus();
                
            }
            return resultado;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(esEdicion)
            {
                cmbHora.Focus();
            }
            else
            {
                skAyudaProductos.AsignarFoco();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ProductoTiempoEstandarInfo productoTiempo = new ProductoTiempoEstandarInfo();
            ProductoTiempoEstandarPL productoTiempoPl = new ProductoTiempoEstandarPL();
            ProductoPL productoPl = new ProductoPL();
            ProductoInfo productoInfo = new ProductoInfo();

            try
            {
                if (ValidarCamposObligatorios())
                {
                    productoTiempo.Tiempo = string.Format("{0}:{1}:00", cmbHora.Text.ToString().PadLeft(2, '0'), cmbMinutos.Text.ToString().PadLeft(2, '0'));
                    productoTiempo.Estatus = (EstatusEnum)cmbEstatus.SelectedItem;
                    productoTiempo.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    productoTiempo.Producto.ProductoId = int.Parse(skAyudaProductos.Clave);
                    productoInfo = productoPl.ObtenerPorID(productoTiempo.Producto);

                    if (productoInfo != null && productoInfo.ProductoId != 0)
                    {
                        if (esEdicion)
                        {
                            productoTiempo.ProductoTiempoEstandarID = int.Parse(txtID.Text);
                            if (productoTiempoPl.ActualizarProductoTiempoEstandar(productoTiempo))
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.AdministracionProductoTiempoEstandar_EdicionMsgGuardar, MessageBoxButton.OK,
                                MessageImage.Correct);
                                confirmacion = false; 
                                this.Close();
                            }
                        }
                        else
                        {
                            var productoTiempoExistenteInfo = productoTiempoPl.ObtenerPorProductoID(productoTiempo);
                            if (productoTiempoExistenteInfo == null || productoTiempoExistenteInfo.ProductoTiempoEstandarID == 0)
                            {
                                if (productoTiempoPl.GuardarProductoTiempoEstandar(productoTiempo))
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.AdministracionProductoTiempoEstandar_EdicionMsgGuardar, MessageBoxButton.OK,
                                    MessageImage.Correct);
                                    confirmacion = false; 
                                    this.Close();
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.AdministracionProductoTiempoEstandar_EdicionMsgErrorGuardar, MessageBoxButton.OK,
                                    MessageImage.Error);
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    string.Format(Properties.Resources.AdministracionProductoTiempoEstandar_EdicionMsgProductoTiempoEstandarYaRegistrado,
                                    productoTiempoExistenteInfo.Producto.Descripcion,
                                    productoTiempoExistenteInfo.ProductoTiempoEstandarID), MessageBoxButton.OK,
                                    MessageImage.Error);

                                
                                cmbHora.SelectedIndex = 0;
                                cmbMinutos.SelectedIndex = 0;
                                skAyudaProductos.LimpiarCampos();
                                skAyudaProductos.AsignarFoco();
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.AdministracionProductoTiempoEstandar_EdicionProductoNoValido, MessageBoxButton.OK,
                            MessageImage.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            ex.Message.ToString(), MessageBoxButton.OK,
                            MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmacion)
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
    }
}
