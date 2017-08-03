using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;
using SIE.Base.Log;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using Xceed.Wpf.Toolkit;
using System.Text.RegularExpressions;

namespace SIE.WinForm.Abasto
{
    /// <summary>
    /// Lógica de interacción para EnvioDeAlimento.xaml
    /// </summary>
    public partial class EnvioDeAlimento
    {
        #region Atributos
        /// <summary>
        /// Capa PL de envio de alimento
        /// </summary>
        private EnvioAlimentoPL _envioAlimentoPL;

        /// <summary>
        /// Lista de tipos de organizacion validas para envio de alimento en el control de seleccion de organizacion destino
        /// </summary>
        private List<TipoOrganizacionInfo> _tiposOrganizacionDestino;
        #endregion
 
        #region Propiedades
        /// <summary>
        /// Contexto
        /// </summary>
        private EnvioAlimentoInfo EnvioAlimento
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (EnvioAlimentoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion

        #region CONSTRUCTORES
        /// <summary>
        /// Constructor por default de la pantalla
        /// </summary>
        public EnvioDeAlimento()
        {

            try
            {
                InitializeComponent();

                InicializaContexto();
                _envioAlimentoPL = new EnvioAlimentoPL();
                _tiposOrganizacionDestino = new List<TipoOrganizacionInfo>();
                _tiposOrganizacionDestino = new TipoOrganizacionPL().ObtenerTodos().Where(
                    tipo => tipo.TipoOrganizacionID != TipoOrganizacion.Ganadera.GetHashCode()).Where(
                    tipo2 => tipo2.TipoOrganizacionID != TipoOrganizacion.Corporativo.GetHashCode()).ToList();
                if (_tiposOrganizacionDestino != null)
                {
                    EnvioAlimento.Destino.ListaTiposOrganizacion = _tiposOrganizacionDestino;

                }
                EnvioAlimento.Destino.Activo = EstatusEnum.Activo;

                //
                this.skAyudaProducto.ObjetoNegocio = new ProductoPL();
                this.skAyudaProducto.AyudaConDatos += (sender2, args) =>
                {
                    try
                    {
                        if (EnvioAlimento.Producto != null)
                        {
                            EnvioAlimento.Producto.SubfamiliaId = int.Parse(cmbSubFamilia.SelectedValue.ToString());
                            if (EnvioAlimento.Producto.ProductoId == 0)
                            {
                                cmbAlmacen.ItemsSource = new List<AlmacenInfo>();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
                    }
                };

                this.skAyudaProducto.AyudaLimpia += skAyudaProducto_AyudaLimpia;


                this.skAyudaDestino.txtClave.Focus();
                skAyudaDestino.AyudaLimpia += skAyudaDestino_AyudaLimpia;
                skAyudaDestino.ObjetoNegocio = new OrganizacionPL();
                skAyudaDestino.AyudaConDatos += (o, args) =>
                {
                    try
                    {
                        if (_tiposOrganizacionDestino != null)
                        {
                            EnvioAlimento.Destino.ListaTiposOrganizacion = this._tiposOrganizacionDestino;
                        }
                        OrganizacionInfo destino = (OrganizacionInfo)skAyudaDestino.DataContext;
                        if (destino.Activo == EstatusEnum.Inactivo)
                        {
                            EnvioAlimento.Destino = new OrganizacionInfo();
                            destino = new OrganizacionInfo();
                            destino.Activo = EstatusEnum.Activo;
                            destino.ListaTiposOrganizacion = this._tiposOrganizacionDestino;
                            EnvioAlimento.Destino.Activo = EstatusEnum.Activo;
                            EnvioAlimento.Destino.ListaTiposOrganizacion = this._tiposOrganizacionDestino;
                            skAyudaDestino.LimpiarCampos();
                            skAyudaDestino.txtClave.Focus();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgOrganizacionDestinoNoEncontrada, MessageBoxButton.OK, MessageImage.Stop);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
                    }
                };
                this.OcultarLote(System.Windows.Visibility.Hidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            
            }
        }
        #endregion

        #region METODOS

        private void InicializaContexto()
        {
            EnvioAlimento = new EnvioAlimentoInfo
            {
                Origen = new OrganizacionInfo(),
                Destino = new OrganizacionInfo(),
                Producto = new ProductoInfo(),
                Almacen = new AlmacenInfo(),
                AlmacenInventario = new AlmacenInventarioInfo(),
                Cantidad = 0,
                Importe = 0,
                Piezas = 0,
                UsuarioCreacionID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString()),
                UsuarioModificacionID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString())
            };
            this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote = new List<AlmacenInventarioLoteInfo>();
            this.EnvioAlimento.Origen = new OrganizacionPL().ObtenerPorID(Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString()));
        }

        /// <summary>
        /// Metodo que carga la subfamilias configuradas para envio de alimento
        /// </summary>
        private void CargaSubFamilias()
        {
            List<SubFamiliaInfo> lstSubFamilias = this._envioAlimentoPL.ObtenerSubFamiliasConfiguradas();
            var subFamilia = new SubFamiliaInfo
            {
                SubFamiliaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            lstSubFamilias.Insert(0, subFamilia);
            cmbSubFamilia.ItemsSource = lstSubFamilias;
            cmbSubFamilia.SelectedItem = subFamilia;
            skAyudaProducto.IsEnabled = false;
        }

        /// <summary>
        /// Metodo que valida los campos vacios de la pantalla
        /// </summary>
        /// <returns>Regresa True si no se encontro ningun campo vacio</returns>
        private bool ValidarCamposVacios()
        {
            if (this.skAyudaDestino.txtDescripcion.Text == string.Empty || this.skAyudaDestino.txtClave.Text == string.Empty || this.skAyudaDestino.txtClave.Text == "0")
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgDestinoRequerido, MessageBoxButton.OK, MessageImage.Warning);
                skAyudaDestino.txtClave.Focus();
                return false;
            }
            else if (this.cmbSubFamilia.SelectedItem == null || (int)this.cmbSubFamilia.SelectedValue == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgSubFamiliaRequrida, MessageBoxButton.OK, MessageImage.Warning);
                cmbSubFamilia.Focus();
                return false;
            }
            else if (this.skAyudaProducto.txtDescripcion.Text == string.Empty || this.skAyudaProducto.txtClave.Text == string.Empty || this.skAyudaProducto.txtClave.Text == "0")
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCodigoProductoRequrido, MessageBoxButton.OK, MessageImage.Warning);
                skAyudaProducto.txtClave.Focus();
                return false;
            }
            else if (this.txtCantidadEnvio.Text == string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCantidadEnvioRequerido, MessageBoxButton.OK, MessageImage.Warning);
                txtCantidadEnvio.Focus();
                return false;
            }
            EnvioAlimento.Destino = (OrganizacionInfo)skAyudaDestino.DataContext;
            return true;
        }

        /// <summary>
        /// Valida que las cuentas contables de producto e inventario en transito esten configuradas.
        /// </summary>
        /// <returns>Regresa true si las cuentas contables son validas</returns>
        private bool ValidarCuentasContables()
        {
            this.EnvioAlimento.CostoProduto = this._envioAlimentoPL.ObtenerCostoAlmacenProducto(this.EnvioAlimento.Almacen.AlmacenID, this.EnvioAlimento.Producto.ProductoId);


            if (this.EnvioAlimento.CostoProduto == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCuentaProductoConfiguracion, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            if (this.EnvioAlimento.CostoProduto.Activo == EstatusEnum.Inactivo || this.EnvioAlimento.CostoProduto.CuentaSAP.Activo == EstatusEnum.Inactivo )
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCuentaProductoConfiguracion, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }

            if (this.EnvioAlimento.CostoProduto.CuentaSAP.CuentaSAP.ToString().Length < 10)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgLongitudCuentaProducto, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }


            ParametroOrganizacionInfo oParametro = this._envioAlimentoPL.ObtenerParametroCuentaTransito(this.EnvioAlimento.Origen);
            if (oParametro == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCuentaTransitoExiste, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }

            if (oParametro.Activo == EstatusEnum.Inactivo || oParametro.ParametroOrganizacionID == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCuentaTransitoConfiguracion, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }

            if (oParametro.Valor.Trim().Length < 10){
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgLongitudCuentaTransito, MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Oculta la informacion del lote
        /// </summary>
        /// <param name="visibilidad"></param>
        private void OcultarLote(System.Windows.Visibility visibilidad)
        {
            this.txtLote.Visibility = visibilidad;
            this.lblLote.Visibility = visibilidad;
            this.btnAyudaLote.Visibility = visibilidad;
            this.imgbtnAyudaLote.Visibility = visibilidad;
            txtPiezas.Visibility = visibilidad;
            txtPiezas.Text = string.Empty;
            lblPiezas.Visibility = visibilidad;
        }

        /// <summary>
        /// Carga en pantalla la informacion del producto
        /// </summary>
        /// <param name="almacenId">Id del almacen</param>
        /// <param name="descripcion">Id de la descripcion</param>
        private void CargarInformacionProducto(int almacenId, string descripcion)
        {
            this.EnvioAlimento.Almacen = new AlmacenInfo
            {
                AlmacenID = almacenId,
                Descripcion = descripcion,
            };
            this.EnvioAlimento.AlmacenInventario = this._envioAlimentoPL.ObtenerCantidadPrecioPromedioPorAlmacenID(new AlmacenInventarioInfo { ProductoID = this.EnvioAlimento.Producto.ProductoId, AlmacenID = this.EnvioAlimento.Almacen.AlmacenID });

            if (this.EnvioAlimento.Producto.ManejaLote){
                this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote = this._envioAlimentoPL.ObtenerLotesPorAlmacenProducto(this.EnvioAlimento.Almacen, this.EnvioAlimento.Producto);
                this.txtPrecioPromedio.Text = this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().PrecioPromedio.ToString();
                this.txtCantidadInventario.Text = this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad.ToString();
                this.txtLote.Text = this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Lote.ToString();
                this.OcultarLote(System.Windows.Visibility.Visible);
            } else {
                this.txtCantidadInventario.Text = this.EnvioAlimento.AlmacenInventario.Cantidad.ToString();
                this.txtPrecioPromedio.Text = this.EnvioAlimento.AlmacenInventario.PrecioPromedio.ToString();
            }

            this.btnGuardar.IsEnabled = true;
            this.txtCantidadEnvio.IsEnabled = true;
            this.txtCantidadEnvio.Background = this.skAyudaDestino.txtClave.Background;
            this.txtCantidadEnvio.Focus();
        }

        /// <summary>
        /// Limpia toda la informacion cargada en pantalla
        /// </summary>
        private void LimparPantalla()
        {
            this.skAyudaDestino.LimpiarCampos();
            cmbSubFamilia.SelectedIndex = 0;
            this.LimparSubfamilia();
            this.OcultarLote(System.Windows.Visibility.Hidden);
            this.skAyudaDestino.Focus();
            skAyudaProducto.IsEnabled = false;
        }

        /// <summary>
        /// Borra los datos en pantalla de la subfamilia seleccionada y de su producto
        /// </summary>
        private void LimparSubfamilia()
        {
            this.skAyudaProducto.LimpiarCampos();
            this.LimpiarProducto();
            this.skAyudaProducto.Focus();
        }

        /// <summary>
        /// Borra los datos en pantalla del producto
        /// </summary>
        private void LimpiarProducto()
        {
            LimpiarInfomracionAlmacen();
            this.cmbAlmacen.ItemsSource = null;
            this.cmbAlmacen.IsEnabled = false;

        }

        /// <summary>
        /// Limpia la informacion en pantalla del almacen
        /// </summary>
        private void LimpiarInfomracionAlmacen(){
            this.txtPrecioPromedio.Text = string.Empty;
            this.txtCantidadInventario.Text = string.Empty;
            if(this.EnvioAlimento.Producto == null ||  !this.EnvioAlimento.Producto.ManejaLote)
             this.OcultarLote(System.Windows.Visibility.Hidden);
            this.txtLote.Text = string.Empty;
            this.txtCantidadEnvio.Text = string.Empty;
            this.txtCantidadEnvio.IsEnabled = false;
            this.txtCantidadEnvio.Background = this.txtPrecioPromedio.Background;
            this.txtImporte.Text = string.Empty;
            this.txtPiezas.Text = string.Empty;
            this.btnGuardar.IsEnabled = false;
        }
     
        #endregion

        #region Eventos

        private void skAyudaProducto_AyudaLimpia(object sender, EventArgs e)
        {
            if (EnvioAlimento != null)
            {
                EnvioAlimento.Producto = new ProductoInfo();
                EnvioAlimento.Producto.UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString());
                EnvioAlimento.Producto.SubfamiliaId = cmbSubFamilia.SelectedValue != null ? (int)cmbSubFamilia.SelectedValue : 0;
                EnvioAlimento.Producto.ProductoId = 0;
                EnvioAlimento.Producto.Activo = EstatusEnum.Activo;
                skAyudaProducto.DataContext = EnvioAlimento.Producto;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando la ayuda deja de mostrar un producto
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void skAyudaDestino_AyudaLimpia(object sender, EventArgs e)
        {
            if (EnvioAlimento != null)
            {
                EnvioAlimento.Destino = new OrganizacionInfo();
                if (_tiposOrganizacionDestino != null)
                {
                    EnvioAlimento.Destino.Activo = EstatusEnum.Activo;
                    EnvioAlimento.Destino.ListaTiposOrganizacion = this._tiposOrganizacionDestino;
                    skAyudaDestino.DataContext = EnvioAlimento.Destino;
                }
            }
        }

        
        /// <summary>
        /// Evento que se invoca cuando se cambia la seleccion de subfamilia
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void cmbSubFamilia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.LimparSubfamilia();
            skAyudaProducto.IsEnabled = !(cmbSubFamilia.SelectedIndex == 0);
           
            if (this.cmbSubFamilia.SelectedValue != null && this.cmbSubFamilia.SelectedValue.ToString() != "0")
            {
                int subFamiliaId = int.Parse(cmbSubFamilia.SelectedValue.ToString());
                if (!this._envioAlimentoPL.HayProductosConfigurados(subFamiliaId))
                {
                    EnvioAlimento.Producto = new ProductoInfo() { UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) };
                    skAyudaProducto.Contexto = new ProductoInfo() {
                        UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) 
                    };
                    EnvioAlimento.Producto.UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString());
                    skAyudaProducto.IsEnabled = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgProductoConfiguradoSubFamilia, MessageBoxButton.OK, MessageImage.Stop);
                    cmbSubFamilia.SelectedIndex = 0;
                }
                else
                {
                    skAyudaProducto.IsEnabled = true;
                    this.EnvioAlimento.Producto.SubfamiliaId = subFamiliaId;
                    this.EnvioAlimento.Producto.UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString());
                    this.skAyudaProducto.txtClave.Focus();
                }
            }
        }

        /// <summary>
        /// Metodo que se invoca cuando la ayuda de seleccion de producto se des-selecciona
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void skAyudaProducto_LostFocus(object sender, RoutedEventArgs e)
        {

            this.EnvioAlimento.Producto = (ProductoInfo)this.skAyudaProducto.Contexto;
            this.EnvioAlimento.Producto.UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString());
            if (this.EnvioAlimento.Producto.ProductoId > 0)
            {
                if (this.EnvioAlimento.Producto.SubFamilia != null)
                {
                    this.EnvioAlimento.Producto.SubfamiliaId = this.EnvioAlimento.Producto.SubFamilia.SubFamiliaID;
                }

                if (this.EnvioAlimento.Producto.SubfamiliaId != int.Parse(this.cmbSubFamilia.SelectedValue.ToString()))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioDeAlimento_MsgProductoSubFamilia, MessageBoxButton.OK, MessageImage.Warning);            
                    this.EnvioAlimento.Producto = new ProductoInfo { ProductoId = 0, UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) };
                    skAyudaProducto.Contexto = new ProductoInfo { SubfamiliaId = (int)cmbSubFamilia.SelectedValue, UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) };
                    this.skAyudaProducto.LimpiarCampos();
                    
                    return;
                }

                if (this.EnvioAlimento.Producto.ManejaLote)
                {
                    this.OcultarLote(System.Windows.Visibility.Visible);
                }

                FiltroAlmacenProductoEnvio filtro = new FiltroAlmacenProductoEnvio
                {
                    ProductoID = this.EnvioAlimento.Producto.ProductoId,
                    UsaurioID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString()),
                    Cantidad = false,
                    Activo = true
                };

                List<AlmacenInfo> lstAlmacenes = this._envioAlimentoPL.ObtenerAlmacenesProducto(filtro);
                if (lstAlmacenes == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioDeAlimento_MsgProductoSinAlmacen, MessageBoxButton.OK, MessageImage.Stop);
                    this.EnvioAlimento.Producto = new ProductoInfo { ProductoId = 0, SubfamiliaId = (int)this.cmbSubFamilia.SelectedValue };
                    lstAlmacenes = new List<AlmacenInfo>();
                    cmbAlmacen.ItemsSource = lstAlmacenes;
                    this.skAyudaProducto.LimpiarCampos();
                    cmbAlmacen.IsEnabled = false;
                    return;
                }

                filtro.Cantidad = true;
                lstAlmacenes = this._envioAlimentoPL.ObtenerAlmacenesProducto(filtro);
                if (lstAlmacenes == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioDeAlimento_MsgProductoSinInventario, MessageBoxButton.OK, MessageImage.Stop);
                    this.EnvioAlimento.Producto = new ProductoInfo { ProductoId = 0, SubfamiliaId = (int)this.cmbSubFamilia.SelectedValue };
                    lstAlmacenes = new List<AlmacenInfo>();
                    cmbAlmacen.ItemsSource = lstAlmacenes;
                    this.skAyudaProducto.LimpiarCampos();
                    cmbAlmacen.IsEnabled = false;
                    return;
                }

                if (lstAlmacenes != null && lstAlmacenes.Count > 0)
                {
                    if (lstAlmacenes.Count > 1)
                    {
                        lstAlmacenes.Insert(0, new AlmacenInfo
                        {
                            AlmacenID = 0,
                            Descripcion = Properties.Resources.cbo_Seleccione
                        });
                    }
                    else
                    {
                        this.CargarInformacionProducto(lstAlmacenes.FirstOrDefault().AlmacenID, lstAlmacenes.FirstOrDefault().Descripcion);
                    }
                    this.cmbAlmacen.IsEnabled = true;
                    this.cmbAlmacen.ItemsSource = lstAlmacenes;
                    this.cmbAlmacen.SelectedItem = lstAlmacenes.FirstOrDefault();
                    if (lstAlmacenes.Count == 1)
                    {
                        this.txtCantidadEnvio.Focus();
                    }
                    else
                    {
                        this.cmbAlmacen.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgProductoSinInventario, MessageBoxButton.OK, MessageImage.Stop);
                    this.EnvioAlimento.Producto = new ProductoInfo { ProductoId = 0, SubfamiliaId = (int)this.cmbSubFamilia.SelectedValue };
             
                    lstAlmacenes = new List<AlmacenInfo>();
                    cmbAlmacen.ItemsSource = lstAlmacenes;
                    this.skAyudaProducto.LimpiarCampos();
                    cmbAlmacen.IsEnabled = false;
                    this.skAyudaProducto.LimpiarCampos();
                    this.skAyudaProducto.txtClave.Focus();
                }
            }
            else 
            {
                cmbAlmacen.ItemsSource = new List<AlmacenInfo>();
                cmbAlmacen.IsEnabled = false;
                skAyudaProducto.LimpiarCampos();
            }
        }

        /// <summary>
        /// Metodo que se invoca cuando la ayuda de seleccion de destino se des-selecciona
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void skAyudaDestino_LostFocus(object sender, RoutedEventArgs e)
        {
            OrganizacionInfo organizacionDestino = EnvioAlimento.Destino;
            if (organizacionDestino.OrganizacionID == 0)
            {
                organizacionDestino = (OrganizacionInfo)skAyudaDestino.DataContext;
                organizacionDestino.ListaTiposOrganizacion = this._tiposOrganizacionDestino;
            }
            if (organizacionDestino.OrganizacionID != 0 && organizacionDestino.TipoOrganizacion != null && (organizacionDestino.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Corporativo.GetHashCode()
                || organizacionDestino.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode()))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioDeAlimento_MsgErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Warning);
                skAyudaDestino.LimpiarCampos();
                return;
            }

            //validar si la organizacion destino tiene un almacen
            if (organizacionDestino != null & organizacionDestino.OrganizacionID != 0)
            {
                AlmacenPL almacenPL = new AlmacenPL();

                List<AlmacenInfo> almacenes = almacenPL.ObtenerAlmacenesPorOrganizacion(organizacionDestino.OrganizacionID);
                if (almacenes == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgDestinoNoTieneAlmacen, MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaDestino.LimpiarCampos();
                    skAyudaDestino.txtClave.Focus();
                    return;
                }
            }
            this.cmbSubFamilia.Focus();
        }
        

        /// <summary>
        /// Evento que se invoca cuando se cambia la seleccion de almacen
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void cmbAlmacen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.LimpiarInfomracionAlmacen();
                if (this.cmbAlmacen.SelectedValue != null && this.cmbAlmacen.SelectedValue.ToString() != "0")
                {
                    this.CargarInformacionProducto((int)this.cmbAlmacen.SelectedValue, (this.cmbAlmacen.SelectedItem as AlmacenInfo).Descripcion);
                }
                    EnvioAlimento.Producto.SubfamiliaId = int.Parse(cmbSubFamilia.SelectedValue.ToString());
            }

            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
  
        /// <summary>
        /// Evento que se ejecuta cuando se selecciona la opcion "Mostrar lotes"
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void BtnAyudaLote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnvioAlimentoAyudaLote ayudaLote = new EnvioAlimentoAyudaLote();
                ayudaLote.listaLotes = this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote;
                MostrarCentrado(ayudaLote);
                EnvioAlimento.Producto.SubfamiliaId = int.Parse(cmbSubFamilia.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

       
        /// <summary>
        /// Evento que se invoca cuando se des-selecciona la cantidad de envio
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void txtCantidadEnvio_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtCantidadEnvio.Text.Trim() != string.Empty)
                {
                    decimal cantidadEnviar = decimal.Parse(this.txtCantidadEnvio.Text);

                    if (cantidadEnviar == 0M)
                    {
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCantidadEnvioCero, MessageBoxButton.OK, MessageImage.Stop) == MessageBoxResult.OK)
                        {
                            this.EnvioAlimento.Cantidad = 0;
                            this.EnvioAlimento.Importe = 0;
                            this.EnvioAlimento.Piezas = 0;
                            this.txtCantidadEnvio.Text = string.Empty;
                            this.txtImporte.Text = string.Empty;
                            this.txtPiezas.Text = string.Empty;
                            txtCantidadEnvio.Focus();
                            
                            return;
                        }
                    }

                    bool cantidadMayor = (this.EnvioAlimento.Producto.ManejaLote) ? (cantidadEnviar > this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad) : (cantidadEnviar > this.EnvioAlimento.AlmacenInventario.Cantidad);
                    if (cantidadMayor)
                    {
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgCantidadMayorInventario, MessageBoxButton.OK, MessageImage.Stop) == MessageBoxResult.OK)
                        {
                            this.EnvioAlimento.Cantidad = 0;
                            this.EnvioAlimento.Importe = 0;
                            this.EnvioAlimento.Piezas = 0;
                            this.txtCantidadEnvio.Text = string.Empty;
                            this.txtImporte.Text = string.Empty;
                            this.txtPiezas.Text = string.Empty;
                            txtCantidadEnvio.Focus();
                            return;
                        }

                        
                    }
                    else
                    {
                        this.EnvioAlimento.Cantidad = cantidadEnviar;
                        if (!this.EnvioAlimento.Producto.ManejaLote)
                        {
                            this.txtPiezas.Text = "0";
                            this.EnvioAlimento.Importe = this.EnvioAlimento.Cantidad * decimal.Parse(this.txtPrecioPromedio.Text);
                            this.txtImporte.Text = this.EnvioAlimento.Importe.ToString();
                        }
                        else
                        {
                            this.EnvioAlimento.Importe = this.EnvioAlimento.Cantidad * decimal.Parse(this.txtPrecioPromedio.Text);
                            this.txtImporte.Text = this.EnvioAlimento.Importe.ToString();
                            decimal lotePzas = 0;
                            this.EnvioAlimento.Piezas = 0;
                            if (this.EnvioAlimento.Producto.SubfamiliaId == SubFamiliasEnum.Forrajes.GetHashCode())
                            {
                                if (this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Piezas > 0 && this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad > 0)
                                    lotePzas = Math.Round((this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad / this.EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Piezas), 0, MidpointRounding.AwayFromZero);
                                if (this.EnvioAlimento.Cantidad > 0 && lotePzas > 0)
                                    this.EnvioAlimento.Piezas = int.Parse(Math.Round(this.EnvioAlimento.Cantidad / lotePzas, 0, MidpointRounding.AwayFromZero).ToString());
                            }
                            this.txtPiezas.Text = this.EnvioAlimento.Piezas.ToString();
                        }
                        this.btnGuardar.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Guarda el envio de alimento
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.ValidarCamposVacios())
                {
                    if (EnvioAlimento.Destino != null && skAyudaDestino.Clave != EnvioAlimento.Destino.OrganizacionID.ToString())
                    {
                        EnvioAlimento.Destino = (OrganizacionInfo)skAyudaDestino.DataContext;
                        EnvioAlimento.Destino.ListaTiposOrganizacion = _tiposOrganizacionDestino;
                    }
                    if (EnvioAlimento.Producto != null && skAyudaProducto.Clave != EnvioAlimento.Producto.ProductoId.ToString())
                    {
                        EnvioAlimento.Producto.ProductoId = ((ProductoInfo)skAyudaDestino.DataContext).ProductoId;
                        if (EnvioAlimento.Producto != null)
                        {
                            EnvioAlimento.Producto.SubfamiliaId = int.Parse(cmbSubFamilia.SelectedValue.ToString());
                        }
                    }

                    if (this.ValidarCuentasContables())
                    {
                        int productoId = this.EnvioAlimento.Producto.ProductoId;
                        FiltroAlmacenProductoEnvio filtro = new FiltroAlmacenProductoEnvio
                        {
                            ProductoID = this.EnvioAlimento.Producto.ProductoId,
                            UsaurioID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString()),
                            Cantidad = false,
                            Activo = true
                        };
                        List<AlmacenInfo> lstAlmacenes = this._envioAlimentoPL.ObtenerAlmacenesProducto(filtro);

                        if (lstAlmacenes == null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioDeAlimento_MsgProductoSinAlmacen, MessageBoxButton.OK, MessageImage.Stop);
                            this.EnvioAlimento.Producto = new ProductoInfo { ProductoId = 0, SubfamiliaId = (int)this.cmbSubFamilia.SelectedValue };

                        
                            lstAlmacenes = new List<AlmacenInfo>();
                            cmbAlmacen.ItemsSource = lstAlmacenes;
                            this.skAyudaProducto.LimpiarCampos();
                            cmbAlmacen.IsEnabled = false;
                            return;
                        }

                        string error;
                        if (ValidarSalidaExcede(out error))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],error, MessageBoxButton.OK, MessageImage.Stop);
                               
                            cmbAlmacen.SelectedIndex = 0;
                            this.EnvioAlimento.Cantidad = 0;
                            this.EnvioAlimento.Importe = 0;
                            this.EnvioAlimento.Piezas = 0;
                            this.txtCantidadEnvio.Text = string.Empty;
                            this.txtImporte.Text = string.Empty;
                            this.txtPiezas.Text = string.Empty;
                            return;
                        }
                        this.EnvioAlimento.Producto.ProductoId = productoId;
                        EnvioAlimentoInfo confirmacionEnvio = this._envioAlimentoPL.RegistrarEnvioAlimento(this.EnvioAlimento);
                        if (confirmacionEnvio.Folio != 0 && confirmacionEnvio.Poliza != null)
                        {
                            if (confirmacionEnvio.Poliza != null)
                            {
                                new ExportarPoliza().ImprimirPoliza(confirmacionEnvio.Poliza, string.Format("{0} {1}", "Poliza", TipoPoliza.SalidaTraspaso));
                            }

                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.EnvioAlimento_MsgDatosGaurdados, confirmacionEnvio.Folio), MessageBoxButton.OK, MessageImage.Correct);
                            this.LimparPantalla();
                            this.InicializaContexto();
                            cmbSubFamilia.SelectedValue = 0;
                            this.EnvioAlimento.Destino.ListaTiposOrganizacion = _tiposOrganizacionDestino;
                            this.skAyudaDestino.txtClave.Focus();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                        }
                    }
                }
            }
            catch(Exception excepcion)
            {
                Logger.Error(excepcion);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida si el stock actual no cubre el envio que se intenta registrar
        /// </summary>
        /// <returns>Regresa True si la canidad de envio excede el inventario actual</returns>
        private bool ValidarSalidaExcede(out string error)
        {
            AlmacenInventarioPL almacenInventarioPL = new AlmacenInventarioPL();
            AlmacenInventarioInfo inventarioActualizado = almacenInventarioPL.ObtenerPorAlmacenIdProductoId(EnvioAlimento.AlmacenInventario);

            if (EnvioAlimento.Producto.ManejaLote) 
            {
                AlmacenInventarioLotePL inventarioLotePL=new AlmacenInventarioLotePL();
                AlmacenInventarioLoteInfo loteActual = inventarioLotePL.ObtenerAlmacenInventarioLotePorId(EnvioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().AlmacenInventarioLoteId);

                if (loteActual.Cantidad == 0)
                {
                    error = Properties.Resources.EnvioAlimento_MsgProductoSinInventario;
                    LimpiarProducto();
                    this.EnvioAlimento.Producto = new ProductoInfo { ProductoId = 0, SubfamiliaId = (int)this.cmbSubFamilia.SelectedValue };
                    cmbAlmacen.ItemsSource = new List<AlmacenInfo>();
                    this.skAyudaProducto.LimpiarCampos();
                    cmbAlmacen.IsEnabled = false;
                    this.skAyudaProducto.LimpiarCampos();
                    this.skAyudaProducto.txtClave.Focus();

                    txtCantidadEnvio.Text = string.Empty;
                    txtPrecioPromedio.Text = string.Empty;
                    txtCantidadInventario.Text = string.Empty;
                    return true;
                }
                
                if ((loteActual.Cantidad - decimal.Parse(txtCantidadEnvio.Text)) < 0)
                {
                    error = Properties.Resources.EnvioAlimento_MsgCantidadInventarioNegativo;
                    LimpiarProducto();
                    txtCantidadEnvio.Text = string.Empty;
                    txtPrecioPromedio.Text = string.Empty;
                    txtCantidadInventario.Text = string.Empty;
                    return true;
                }
                error = string.Empty;
                return false;
            }
            else
            {
                if (inventarioActualizado.Cantidad == 0)
                {
                    error = Properties.Resources.EnvioAlimento_MsgProductoSinInventario;
                    LimpiarProducto();
                    this.skAyudaProducto.LimpiarCampos();
                    this.skAyudaProducto.txtClave.Focus();

                    txtCantidadEnvio.Text = string.Empty;
                    txtPrecioPromedio.Text = string.Empty;
                    txtCantidadInventario.Text = string.Empty;
                    return true;
                }

                if (((inventarioActualizado.Cantidad - decimal.Parse(txtCantidadEnvio.Text.Trim())) < 0))
                {
                    error = Properties.Resources.EnvioAlimento_MsgCantidadInventarioNegativo;
                    LimpiarProducto();
                    txtCantidadEnvio.Text = string.Empty;
                    txtPrecioPromedio.Text = string.Empty;
                    txtCantidadInventario.Text = string.Empty;
                    return true;
                }
                else
                {
                    error = string.Empty;
                    return false;
                }
            }
        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgConfirmacionCancelar, MessageBoxButton.YesNo, MessageImage.Question) == MessageBoxResult.Yes)
            {
                this.InicializaContexto();
                this.LimparPantalla();
                this.skAyudaDestino.txtClave.Focus();
            }
        }

        /// <summary>
        /// Evento que se invoca al haber inicializado la el control "SKAyudaProducto" (al ser el ultimo control en inicializarse)
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void SkAyudaProducto_OnLoaded(object sender, RoutedEventArgs args)
        {
            try
            {
            this.LimparPantalla();
             
            if ((ProductoInfo)skAyudaProducto.Contexto != null)
            {
                ((ProductoInfo)skAyudaProducto.Contexto).UsuarioModificacionID = int.Parse(Application.Current.Properties["UsuarioID"].ToString());
            }

            if (!this._envioAlimentoPL.ValidarExisteParametroAyudaEnvioProducto())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.EnvioAlimento_MsgParametroAyudaEnvioNoRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                this.IsEnabled = false;
                return;
            }

            if (!this._envioAlimentoPL.ConfiguracionParametroEnvioAlimento)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgParametroAyudaRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                this.IsEnabled = false;
                return;
            }

            if (!this._envioAlimentoPL.HayOrganizacionesActivas)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimento_MsgOrganizacionRequerida, MessageBoxButton.OK, MessageImage.Stop);
                this.IsEnabled = false;
                return;
            }

            if (this._envioAlimentoPL.ParametroEnvioAlimento.Valor == string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.EnvioAlimneto_MsgParametroAyudaConfigurado, MessageBoxButton.OK, MessageImage.Error);
                this.IsEnabled = false;
                return;
            }

            this.IsEnabled = true;
            this.txtOrigen.Text = this.EnvioAlimento.Origen.Descripcion;
            this.CargaSubFamilias();    
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

    }
}
