using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Base.Log;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for Medicamentos.xaml
    /// </summary>
    public partial class Medicamentos : Window
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IList<TratamientoInfo> _listaTratamientos;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _corralSeleccionado;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _areteSeleccionado;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tratamientos">Lista de tratamientos</param>
        /// <param name="corral">Corral</param>
        /// <param name="arete">Numero de Arete</param>
        public Medicamentos(IList<TratamientoInfo> tratamientos, string corral, string arete)
        {
            _listaTratamientos = tratamientos;
            _corralSeleccionado = corral;
            _areteSeleccionado = arete;
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa datos de usuario
        /// </summary>
        private void IniciarDatosUsuario()
        {
            lblCorralDestinoGenerado.Content = _corralSeleccionado;
            lblNumeroAreteGenerado.Content = _areteSeleccionado;

            CargarMedicamentos();
        }

        /// <summary>
        /// Carga la lista de medicamentos a los tratamientos seleccionados
        /// </summary>
        private void CargarMedicamentos()
        {
            try
            {
                var traPl = new TratamientoPL();
                if (_listaTratamientos != null){
                    traPl.ObtenerProductosPorTratamiento(_listaTratamientos);
                    IList<ProductoInfo> listaProductos = 
                            _listaTratamientos.Where(
                                            item => item.Seleccionado && item.Habilitado).SelectMany(
                                                item => item.Productos).ToList();
                    var lista = new List<ProductoInfo>();
                    foreach (var producto in listaProductos)
                    {
                        var existe = true;
                        foreach (var productoInfo in lista)
                        {
                            if (producto.ProductoId == productoInfo.ProductoId)
                            {
                                existe = false;
                            }
                        }
                        if (existe)
                        {
                            lista.Add(producto);
                        }
                    }
                    //listaProductos


                    var contador = 0;
                    foreach (var producto in lista)
                    {
                        producto.Renglon = ++contador;
                    }

                    dgMedicamentos.ItemsSource = lista;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Medicamentos_NoExisteTratamientos,
                       MessageBoxButton.OK,
                       MessageImage.Warning);
                }

            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.Medicamentos_ErrorCargarMedicamentos,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
            }
        }

        /// <summary>
        /// Handler del evento loaded de la pagina para mostrar datos de usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Medicamentos_Loaded(object sender, RoutedEventArgs e)
        {
           IniciarDatosUsuario();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
