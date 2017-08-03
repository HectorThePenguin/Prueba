
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIE.Base.Exepciones;         
using SIE.Base.Infos;              
using SIE.Base.Log;                
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Servicios.PL;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Calidad
{
    /// <summary>
    /// Lógica de interacción para AnalisisGrasa.xaml
    /// </summary>
    public partial class AnalisisGrasa
    {
        //Probando Version
        // hola
        //Contiene la informacion del formulario a agregar
        private AnalisisGrasaInfo analisisGrasaInfo;
        //Contiene la lista que se muestra en el grid  
        private List<AnalisisGrasaInfo> listaAnalisisGrasa;
        //Contiene la ayuda para la Entrada de Producto
        private SKAyuda<EntradaProductoInfo> skAyudaEntradaProducto;
        //Posicion de la lista en Edicion
        private bool bolEditando;
       
        public AnalisisGrasa()
        {
            InitializeComponent();
            InicializaFormulario();
            InicializaGridAnalisis();
        }

        #region Metodos

        //Inicializa el Formulario
        private void InicializaFormulario()
        {
            bolEditando = false;
            btnAgregarActualizar.Content = Properties.Resources.AnalisisGrasa_btnAgregarActualizar_Agregar;
            btnAgregarActualizar.UpdateLayout();
            AgregarAyudaEntradaProducto();
            IList<TipoMuestraEnum> tipoMuestraEnum = Enum.GetValues(typeof(TipoMuestraEnum)).Cast<TipoMuestraEnum>().ToList();
            cmbTipoMuestra.ItemsSource = tipoMuestraEnum;
            cmbTipoMuestra.SelectedIndex = 0;
            txtPesoMuestra.Value = 0;
            txtPesoTuboSeco.Value = 0;
            txtImpuresas.Value = 0;
            txtPesoTuboMuestra.Value = 0;
            txtOrigen.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            analisisGrasaInfo = new AnalisisGrasaInfo();
            analisisGrasaInfo.Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]) };
            analisisGrasaInfo.UsuarioCreacion = new UsuarioInfo() { UsuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]) };
            analisisGrasaInfo.Activo = true;
        }

        //Inicializa el grid y enlaza la fuente
        private void InicializaGridAnalisis() 
        {
            listaAnalisisGrasa = new List<AnalisisGrasaInfo>();
            dgAnalisisGrasas.ItemsSource = listaAnalisisGrasa;
        }

        //Agrega la ayuda para la organizacion
        private void AgregarAyudaEntradaProducto()
        {  
            try
            {
                skAyudaEntradaProducto = new SKAyuda<EntradaProductoInfo>(0,
                    false,
                    new EntradaProductoInfo()
                    {
                        FolioBusqueda = string.Empty,
                        DescripcionProducto = string.Empty,
                        Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]) },
                        Producto = new ProductoInfo { FamiliaId = (int)FamiliasEnum.MateriaPrimas},
                        Estatus = new EstatusInfo() { EstatusId = (int)Estatus.Aprobado }
                    },
                    "PropiedadFolioEstatus",
                    "PropiedadDescripcionProductoEstatus",
                    true,
                    133,
                    true)
                {
                    AyudaPL = new EntradaProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.AnalisisGrasa_AyudaEntradaProducto_ClaveInexistente,
                    MensajeBusquedaCerrar = Properties.Resources.AnalisisGrasa_AyudaEntradaProducto_Cerrar,
                    MensajeBusqueda = Properties.Resources.AnalisisGrasa_AyudaEntradaProducto_Busqueda,
                    MensajeAgregar = Properties.Resources.AnalisisGrasa_AyudaEntradaProducto_Agregar,
                    TituloEtiqueta = Properties.Resources.AnalisisGrasa_AyudaEntradaProducto_TituloEtiqueta,
                    TituloPantalla = Properties.Resources.AnalisisGrasa_AyudaEntradaProducto_Titulo,
                    MetodoPorDescripcion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus"

                };

                skAyudaEntradaProducto.ObtenerDatos += ObtenerDatosAyudaEntradaProducto;
                skAyudaEntradaProducto.LlamadaMetodosNoExistenDatos += LimpiarTodoAyudaEntradaProducto;

                skAyudaEntradaProducto.AsignaTabIndex(0);
                splAyudaEntradaProducto.Children.Clear();
                splAyudaEntradaProducto.Children.Add(skAyudaEntradaProducto);
                skAyudaEntradaProducto.TabIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            } 
        }  

        //Inicializa el filtro de la ayuda de entrada producto
        private void InicializaFiltroAyudaEntradaProducto() 
        {
            skAyudaEntradaProducto.Info = new EntradaProductoInfo()
            {
                FolioBusqueda = string.Empty,
                DescripcionProducto = string.Empty,
                Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]) },
                Producto = new ProductoInfo { FamiliaId = (int)FamiliasEnum.MateriaPrimas },
                Estatus = new EstatusInfo() { EstatusId = (int)Estatus.Aprobado }
            };
        }

        //Asigna la informacion de la ayuda cuando se selecciona un registro
        private void ObtenerDatosAyudaEntradaProducto(string filtro)
        {
            analisisGrasaInfo.EntradaProdructo = skAyudaEntradaProducto.Info;
            txtOrigen.Text = String.Empty;
            txtPlaca.Text = String.Empty;
            if (skAyudaEntradaProducto.Info.RegistroVigilancia != null)
            {
                txtOrigen.Text = skAyudaEntradaProducto.Info.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion;
                txtPlaca.Text = skAyudaEntradaProducto.Info.RegistroVigilancia.Camion.PlacaCamion;
            }
            InicializaFiltroAyudaEntradaProducto(); 
        }

        //Limpia la ayuda cuando no existe el registro buscado
        private void LimpiarTodoAyudaEntradaProducto()
        {
            skAyudaEntradaProducto.Info = null;
            txtOrigen.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            InicializaFiltroAyudaEntradaProducto();
        }

        //Valida los datos requeridos
        private bool ValidarDatos()
        {
            if (analisisGrasaInfo.EntradaProdructo == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionFolio,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                skAyudaEntradaProducto.AsignarFoco();
                return false;
            }

            if (decimal.Parse(txtPesoMuestra.Value.ToString()) == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionPesoMuestra,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtPesoMuestra.Focus();
                return false;
            }

            if (decimal.Parse(txtPesoTuboSeco.Value.ToString()) == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionPesoTuboSeco,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtPesoTuboSeco.Focus();
                return false;
            }

            if (decimal.Parse(txtPesoTuboMuestra.Value.ToString()) == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionPesoTuboMuestra,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtPesoTuboMuestra.Focus();
                return false;
            }
          
            if (decimal.Parse(txtImpuresas.Value.ToString()) == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionImpureza,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtImpuresas.Focus();
                return false;
            }
            if(txtObservaciones.Text == string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionObservaciones,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtObservaciones.Focus();
                return false;
            }
            return true;
        }

        //Reasigna los valores en datos generales cuando se edita un renglon del grid
        private void RestaurarAnalisisGrasa(AnalisisGrasaInfo analisisGrasaInfoGrid)
        {
            analisisGrasaInfo = analisisGrasaInfoGrid;
            InicializaFiltroAyudaEntradaProducto();
            skAyudaEntradaProducto.Clave = analisisGrasaInfoGrid.EntradaProdructo.Folio.ToString();
            if (analisisGrasaInfoGrid.TipoMuestra == "Grasa"){
                cmbTipoMuestra.SelectedItem = TipoMuestraEnum.Grasa;
            }
            else {
                cmbTipoMuestra.SelectedItem = TipoMuestraEnum.Sebo;
            }
            txtOrigen.Text = analisisGrasaInfoGrid.EntradaProdructo.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion;
            txtPlaca.Text = analisisGrasaInfoGrid.EntradaProdructo.RegistroVigilancia.Camion.PlacaCamion;
            txtPesoMuestra.Value = analisisGrasaInfoGrid.PesoMuestra;
            txtPesoTuboMuestra.Value = analisisGrasaInfoGrid.PesoTuboMuestra;
            txtPesoTuboSeco.Value = analisisGrasaInfoGrid.PesoTuboSeco;
            txtImpuresas.Value = analisisGrasaInfoGrid.Impurezas;
            txtObservaciones.Text = analisisGrasaInfoGrid.Observaciones;
        }

        #endregion 

        #region Eventos

        //Se activa cuando hacen click sobre el boton editar en el grid
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bolEditando = true;
                btnAgregarActualizar.Content = Properties.Resources.AnalisisGrasa_btnAgregarActualizar_Actualizar;
                btnAgregarActualizar.Tag = dgAnalisisGrasas.SelectedIndex;
                RestaurarAnalisisGrasa((AnalisisGrasaInfo)dgAnalisisGrasas.SelectedItem);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCuenta_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
            
        //Se activa cuando dan click sobre el boton
        private void BtnAgregarActualizar_OnClick(object sender, RoutedEventArgs e)
        {
            analisisGrasaInfo.TipoMuestra = cmbTipoMuestra.Text;
            analisisGrasaInfo.PesoMuestra = (decimal)txtPesoMuestra.Value;
            analisisGrasaInfo.PesoTuboMuestra = (decimal)txtPesoTuboMuestra.Value;
            analisisGrasaInfo.PesoTuboSeco = (decimal)txtPesoTuboSeco.Value;
            analisisGrasaInfo.Impurezas = (decimal)txtImpuresas.Value;
            analisisGrasaInfo.Observaciones = txtObservaciones.Text;
            if (ValidarDatos())
            {
                if (bolEditando) 
                {
                    listaAnalisisGrasa[(int)btnAgregarActualizar.Tag] = analisisGrasaInfo;
                }
                else 
                {
                    listaAnalisisGrasa.Add(analisisGrasaInfo);  
                }
                InicializaFormulario();
                dgAnalisisGrasas.Items.Refresh();
            }
        }

        //Se activa cuando dan click sobre el boton
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            InicializaFormulario();
        }

        //Se activa cuando dan click sobre el boton
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e) 
        {
            if (listaAnalisisGrasa.Count == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AnalisisGrasa_ValidacionGrid,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                skAyudaEntradaProducto.AsignarFoco();
            }
            else
            {
                bool guardoAnalisis = false;
                var analisisGrasaPl = new AnalisisGrasaPL();
                guardoAnalisis =analisisGrasaPl.Guardar(listaAnalisisGrasa);
                if (guardoAnalisis)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeGuardadoExito,
                        MessageBoxButton.OK, MessageImage.Correct);                  
                    InicializaFormulario();
                    InicializaGridAnalisis();
                }
            }
        }

        //Se activa cuando dan click sobre el boton
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) 
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.GastosMateriasPrimas_MensajeCancelar,
               MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializaGridAnalisis();
                InicializaFormulario();
            }
        }

        //Evento que se activa cuando se pierde el foco del control
        private void TxtDecimal_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var txtEditado = (DecimalUpDown)sender;
            if (txtEditado.Value == null){ txtEditado.Value = 0; }
        }

        //Ocurre cuando se teclea en el control
        private void TxtObservaciones_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarCaracterValidoTexo(e.Text);
        }

        //Ocurre cuando se obtiene el foco en el control
        private void CmbTipoMuestra_GotFocus(object sender, RoutedEventArgs e)
        {
            if (skAyudaEntradaProducto.Clave == "0")
            {
                skAyudaEntradaProducto.LimpiarCampos();
                txtOrigen.Text = string.Empty;
                txtPlaca.Text = string.Empty;
                InicializaFiltroAyudaEntradaProducto();
            }
        }

        #endregion 
       
    }
}
