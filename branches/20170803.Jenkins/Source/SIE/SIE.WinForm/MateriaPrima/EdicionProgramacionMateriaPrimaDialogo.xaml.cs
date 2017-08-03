
using System.Globalization;
using System.Windows;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para EdicionProgramacionMateriaPrimaDialogo.xaml
    /// </summary>
    public partial class EdicionProgramacionMateriaPrimaDialogo
    {
        #region Atributos
            private PedidoInfo pedido;
            private PedidoDetalleInfo detalle;
            //private bool SalidaNormal = false;
        #endregion
            
        #region Constructor
            public EdicionProgramacionMateriaPrimaDialogo(PedidoInfo pedidoInfo, PedidoDetalleInfo detalleInfo)
            {

                InitializeComponent();
                InicializaDatosPedido(pedidoInfo, detalleInfo);
                InicializarDatos();

            }
        #endregion

        #region Eventos

            /// <summary>
            /// Abre ventana para crear una programacion nueva.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
            {
                var edicion = new CrearProgramacionMateriaPrimaDialogo(pedido, detalle);
                MostrarCentrado(edicion);
                InicializarGrid();
            }

    
            /// <summary>
            /// Edita la programacion seleccionada en el grid.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void BtnEditar_Click(object sender, RoutedEventArgs e)
            {
                EditarProgramacion();
            }

            /// <summary>
            /// Abre ventana para editar programacion seleccionada en el grid
            /// </summary>
            private void EditarProgramacion()
            {
                var programacion = (ProgramacionMateriaPrimaInfo)dgCantidades.SelectedItem;
                if (programacion != null)
                {
                    var edicion = new CrearProgramacionMateriaPrimaDialogo(pedido,detalle);
                    edicion.Programacion = programacion;
                    MostrarCentrado(edicion);
                    InicializarGrid();
                }
            }

        #endregion Eventos

        #region Metodos
            /// <summary>
            /// Inicializa las variables necesarias del pedido para programar.
            /// </summary>
            /// <param name="pedidoInfo"></param>
            /// <param name="detalleInfo"></param>
            private void InicializaDatosPedido(PedidoInfo pedidoInfo, PedidoDetalleInfo detalleInfo)
            {
                pedido = pedidoInfo;
                detalle = detalleInfo;
            }

            /// <summary>
            /// Inicializa los datos en pantalla
            /// </summary>
            private void InicializarDatos()
            {
                txtCantidadSolicitada.Value = (int?) detalle.CantidadSolicitada;
                txtNombreProducto.Text = detalle.Producto.ProductoDescripcion;
                txtIdSubFamilia.Text = detalle.Producto.SubFamilia.SubFamiliaID.ToString(CultureInfo.InvariantCulture);
                txtNombreSubFamilia.Text = detalle.Producto.SubFamilia.Descripcion;
                InicializarGrid();
            }

            /// <summary>
            /// Inicializa el grid con las programaciones realizadas.
            /// </summary>
            private void InicializarGrid()
            {
                dgCantidades.ItemsSource = null;
                if (detalle != null && (detalle.ProgramacionMateriaPrima != null && detalle.ProgramacionMateriaPrima.Count > 0))
                {
                   dgCantidades.ItemsSource = detalle.ProgramacionMateriaPrima; 
                }
                
                ValidaCantidadProgramada();
            }

            /// <summary>
            /// Valida que la cantidad programada no pase de la cantidad solicitada.
            /// </summary>
            private bool ValidaCantidadProgramada()
            {
                bool resultado = true;

                if (detalle.CantidadSolicitada > detalle.TotalCantidadProgramada)
                {
                    detalle.Activo = EstatusEnum.Activo;
                    btnAgregar.Visibility = Visibility.Visible;
                }
                else if(detalle.CantidadSolicitada == detalle.TotalCantidadProgramada)
                {
                    detalle.Activo = EstatusEnum.Inactivo;
                    btnAgregar.Visibility = Visibility.Hidden;
                }
                else
                {
                    resultado = false;
                }

                return resultado;
            }

        #endregion Metodos
    }
}
