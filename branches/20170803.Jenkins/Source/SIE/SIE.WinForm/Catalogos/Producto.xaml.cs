using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto
    {
        #region PROPIEDADES

        private ProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private IList<FamiliaInfo> Familia { get; set; }
        private IList<SubFamiliaInfo> SubFamilia { get; set; }
        private IList<UnidadMedicionInfo> Unidad { get; set; }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public Producto()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {                
                ucPaginacion.DatosDelegado += ObtenerProductos;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                CargarCboEstatus();
                CargarComboFamilia();
                CargarComboUnidades();
                CargarSubFamilia();
                Buscar();
                txtDescripcion.Focus();                
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Producto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Producto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento al Cambiar el indice seleccionado
        /// en combo de Familia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboFamilia_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int familiaId = Extensor.ValorEntero(Convert.ToString(cboFamilia.SelectedValue));
            if (familiaId > 0)
            {
                AsignarSubFamiliaCombo(familiaId);
            }
            else
            {
                DeshabilitaComboSubfamilia();
            }
            Contexto.FamiliaId = familiaId;
        }        

        /// <summary>
        /// Ejecuta una Busqueda con los Filtros
        /// seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Levanta la ventana de edicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            try
            {
                var productoSelecionado = btn.CommandParameter as ProductoInfo;
                if (productoSelecionado != null)
                {
                    var productoInfo = new ProductoInfo
                    {
                        Activo = productoSelecionado.Activo,
                        ProductoDescripcion = productoSelecionado.ProductoDescripcion,
                        ProductoId = productoSelecionado.ProductoId,
                        FamiliaId = productoSelecionado.FamiliaId,
                        SubfamiliaId = productoSelecionado.SubfamiliaId,
                        UnidadId = productoSelecionado.UnidadId,
                        Familias = Familia,
                        SubFamilias = SubFamilia,
                        UnidadesMedidcion = Unidad,
                        ManejaLote = productoSelecionado.ManejaLote,
                        MaterialSAP = productoSelecionado.MaterialSAP
                    };

                    var productoEdicion = new ProductoEdicion(productoInfo)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.Producto_Editar_Titulo }
                    };
                    MostrarCentrado(productoEdicion);
                    ReiniciarValoresPaginador();
                    CambiarLeyendaCombo();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Grupo_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Manda llamar la pantalla para 
        /// Generar un Nuevo Producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var productoInfo = new ProductoInfo
                                       {
                                           Familias = Familia,
                                           SubFamilias = SubFamilia,
                                           UnidadesMedidcion = Unidad
                                       };
                var productoEdicion = new ProductoEdicion(productoInfo)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.Producto_Nuevo_Titulo }
                };
                MostrarCentrado(productoEdicion);
                ReiniciarValoresPaginador();
                CambiarLeyendaCombo();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Producto_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida que el control solo acepte números y letras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Filtra las Subfamilias para asignarlas
        /// al combo
        /// </summary>
        /// <param name="familiaId"></param>
        private void AsignarSubFamiliaCombo(int familiaId)
        {
            IList<SubFamiliaInfo> subFamilias =
                SubFamilia.Where(fam => fam.Familia.FamiliaID == familiaId).ToList();
            if (subFamilias.Any())
            {
                subFamilias.Insert(0, new SubFamiliaInfo { SubFamiliaID = 0, Descripcion = Properties.Resources.cbo_Seleccionar });
                cboSubFamilia.ItemsSource = subFamilias;                
                cboSubFamilia.SelectedIndex = 0;
            }
            else
            {
                DeshabilitaComboSubfamilia();
            }
        }

        /// <summary>
        /// Deshabilita el combo de subfamilia
        /// </summary>
        private void DeshabilitaComboSubfamilia()
        {
            var subFamilias = new List<SubFamiliaInfo>();
            var subFamiliaInfo = new SubFamiliaInfo {SubFamiliaID = 0, Descripcion = Properties.Resources.cbo_Seleccionar};
            subFamilias.Insert(0, subFamiliaInfo);
            cboSubFamilia.ItemsSource = subFamilias;
            cboSubFamilia.SelectedItem = subFamiliaInfo;
        }

        /// <summary>
        /// Obtiene una Lista de Productos
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerProductos(int inicio, int limite)
        {
            try
            {
                var productoPL = new ProductoPL();
                ProductoInfo filtros = ObtenerFiltros();
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ProductoInfo> resultadoInfo = productoPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<ProductoInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Producto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Producto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Buscar una lista de productos
        /// </summary>
        private void Buscar()
        {
            ObtenerProductos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ProductoInfo ObtenerFiltros()
        {
            ProductoInfo filtro;
            try
            {
                string descripcion = txtDescripcion.Text;
                int familiaId = Extensor.ValorEntero(Convert.ToString(cboFamilia.SelectedValue));
                int subFamiliaId = Extensor.ValorEntero(Convert.ToString(cboSubFamilia.SelectedValue));         
                filtro = new ProductoInfo
                {
                    ProductoDescripcion = descripcion,
                    Activo = (EstatusEnum)cboEstatus.SelectedValue,
                    FamiliaId = familiaId,
                    SubfamiliaId = subFamiliaId,                    
                };                
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return filtro;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProductoInfo();
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = EstatusEnum.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Carga el combo de Familia
        /// </summary>
        private void CargarComboFamilia()
        {
            try
            {
                var familiaPL = new FamiliaPL();
                Familia = familiaPL.ObtenerTodos(EstatusEnum.Activo);                
                var familiaInfo = new FamiliaInfo {FamiliaID = 0, Descripcion = Properties.Resources.cbo_Seleccionar};
                Familia.Insert(0, familiaInfo);                
                cboFamilia.ItemsSource = Familia;
                cboFamilia.SelectedItem = familiaInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene las Subfamilia
        /// </summary>
        private void CargarSubFamilia()
        {
            try
            {
                var subFamiliaPL = new SubFamiliaPL();
                SubFamilia = subFamiliaPL.ObtenerTodos(EstatusEnum.Activo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }            
        }        

        /// <summary>
        /// Carga el combo de Unidades
        /// </summary>
        private void CargarComboUnidades()
        {
            try
            {
                var unidadPL = new UnidadMedicionPL();
                Unidad = unidadPL.ObtenerTodos(EstatusEnum.Activo);
                if (Unidad != null && Unidad.Any())
                {
                    Unidad.Insert(0,
                                    new UnidadMedicionInfo
                                        {UnidadID = 0, Descripcion = Properties.Resources.cbo_Seleccionar});
                }              
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }       
        }

        /// <summary>
        /// Cambiar leyenda de Todos por Seleccione
        /// </summary>
        private void CambiarLeyendaCombo()
        {
            UnidadMedicionInfo unidadTodos =
                Unidad.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccione));
            if (unidadTodos != null)
            {
                unidadTodos.Descripcion = Properties.Resources.cbo_Seleccionar;
            }

            FamiliaInfo familiaTodos =
                Familia.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccione));
            if (familiaTodos != null)
            {
                familiaTodos.Descripcion = Properties.Resources.cbo_Seleccionar;
            }
        }

        /// <summary>
        /// Reinicia el valor del inicio de la página.
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion METODOS
    }
}
