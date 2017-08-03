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
    /// Lógica de interacción para MuestreoTamanoFibra.xaml
    /// </summary>
    public partial class MuestreoTamanoFibra 
    {
    #region Atributos
        private FormulaInfo formulaInfoSeleccionado;
        private ProductoInfo productoInfoSeleccionado;

        private List<MuestreoFibraFormulaInfo> listaMuestreoFibraFormulaInfo;
        private List<MuestreoFibraProductoInfo> listaMuestreoFibraProductoInfo;

        private SKAyuda<FormulaInfo> skAyudaFormula;
        private SKAyuda<ProductoInfo> skAyudaProducto;

        private int maximoCribaEntrada;
        private int maximoCribaSalida;
        private int organizacionID;
        private int usuarioID;
        private bool bolEditando;
    #endregion

    #region Constructor
        public MuestreoTamanoFibra()
        {
            InitializeComponent();

            skAyudaFormula = null;
            skAyudaProducto = null;
            maximoCribaEntrada = 0; 
            maximoCribaSalida = 0;
            organizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            CargarCribaPermitida();
            rbProducto.IsChecked = true;
            
            AgregarAyudaProducto();
        }
    #endregion

    #region Metodos
        /// <summary>
        /// Inicializa los atributos y parte de los controles del formulario
        /// </summary>
        private void InicializarFormulario()
        {
            listaMuestreoFibraFormulaInfo = new List<MuestreoFibraFormulaInfo>();
            listaMuestreoFibraProductoInfo = new List<MuestreoFibraProductoInfo>();
            dgFormulasMuestreo.ItemsSource = listaMuestreoFibraFormulaInfo;
            dgProductosMuestreo.ItemsSource = listaMuestreoFibraProductoInfo;
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }
        
        /// <summary>
        /// Inicializa los atributos y controles para el muestreo de fibra de tipo formula
        /// </summary>
        private void InicializarMuestreoFibraFormula()
        {
            gbParametrosFormula.Visibility = Visibility.Visible;
            gbDatosMuestreoFormula.Visibility = Visibility.Visible;
            dgFormulasMuestreo.Visibility = Visibility.Visible;
            gbParametrosProductos.Visibility = Visibility.Hidden;
            gbDatosMuestreoProducto.Visibility = Visibility.Hidden;
            dgProductosMuestreo.Visibility = Visibility.Hidden;
            AgregarAyudaFormula();
            LimpiarFormulaDatosMuestreos();
            chkFormulaFinosBase.IsChecked = false;
            txtFormulaPesoFinosBase.IsEnabled = false;
            chkFormulaFinosTamiz.IsChecked = false;
            txtFormulaPesoFinosTamiz.IsEnabled = false;
            chkFormulaGrande.IsChecked = false;
            txtFormulaPesoFibraGrande.IsEnabled = false;
            chkFormulaMediana.IsChecked = false;
            txtFormulaPesoFibraMediana.IsEnabled = false;
            btnAgregarActualizar.Content = Properties.Resources.MuestreoFibra_btnAgregar;
        }

        /// <summary>
        /// Inicializa los atributos y controles para el muestreo de fibra de tipo formula
        /// </summary>
        private void InicializarMuestreoFibraProducto()
        {
            gbParametrosFormula.Visibility = Visibility.Hidden;
            gbDatosMuestreoFormula.Visibility = Visibility.Hidden;
            dgFormulasMuestreo.Visibility = Visibility.Hidden;
            gbParametrosProductos.Visibility = Visibility.Visible;
            gbDatosMuestreoProducto.Visibility = Visibility.Visible;
            dgProductosMuestreo.Visibility = Visibility.Visible;
            AgregarAyudaProducto();
            LimpiarProductoDatosMuestreos();
            chkProductoFinos.IsChecked = false;
            txtProductoPesoGranoFino.IsEnabled = false;
            chkProductoGruesos.IsChecked = false;
            txtProductoPesoGranoGrueso.IsEnabled = false;
            chkProductoMedianos.IsChecked = false;
            txtProductoPesoGranoMediano.IsEnabled = false;
            btnAgregarActualizar.Content = Properties.Resources.MuestreoFibra_btnAgregar;
        }

        /// <summary>
        /// Carga el valor de los parametros de criba entrada y criba entrada salida
        /// </summary>
        private void CargarCribaPermitida()
        {
            var parametroPl = new ParametroOrganizacionPL();
            var parametro = parametroPl.ObtenerPorOrganizacionIDClaveParametro(organizacionID, ParametrosEnum.CribaEntrada.ToString());
            if (parametro != null)
            {
                int.TryParse(parametro.Valor,out maximoCribaEntrada);
            }
            /*else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                      "No se encontro el parametro ",
                      MessageBoxButton.OK,
                      MessageImage.Stop);
            }*/
            parametro = parametroPl.ObtenerPorOrganizacionIDClaveParametro(organizacionID, ParametrosEnum.CribaSalida.ToString());
            if (parametro != null)
            {
                int.TryParse(parametro.Valor, out maximoCribaSalida);
            }
            /*else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                      "No se encontro el parametro ",
                      MessageBoxButton.OK,
                      MessageImage.Stop);
            }*/
        }

        /// <summary>
        /// Agrega la ayuda para buscar un producto por su id o su formulario de busqueda
        /// </summary>
        private void AgregarAyudaProducto(){
            try
            {
                skAyudaProducto = new SKAyuda<ProductoInfo>(170,
                    false,
                    new ProductoInfo()
                    {
                        FamiliaId = (int)FamiliasEnum.MateriaPrimas,
                        SubfamiliaId = (int)SubFamiliasEnum.Forrajes,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadClaveMuestreoTamanoFibra",
                    "PropiedadDescripcionMuestreoTamanoFibra", 
                    true,
                    50,
                    true)
                {
                    AyudaPL = new ProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.MuestreoFibra_msgAyudaProducto_ClaveInvalida,
                    MensajeBusquedaCerrar = Properties.Resources.MuestreoFibra_msgAyudaProducto_Cerrar,
                    MensajeBusqueda = Properties.Resources.MuestreoFibra_msgAyudaProducto_Buscar,
                    MensajeAgregar = Properties.Resources.MuestreoFibra_msgAyudaProducto_Agregar,
                    TituloEtiqueta = Properties.Resources.MuestreoFibra_msgAyudaProducto_Etiqueta,
                    TituloPantalla = Properties.Resources.MuestreoFibra_msgAyudaProducto_Titulo,
                    MetodoPorDescripcion = "PropiedadDescripcionMuestreoTamanoFibra"
                };

                skAyudaProducto.ObtenerDatos += ObtenerDatosAyudaProductoFormula;
                skAyudaProducto.LlamadaMetodosNoExistenDatos += LimpiarTodoAyudaProductoFormula;

                //skAyudaFormula.AsignaTabIndex(1);
                splAyudaProductoFormula.Children.Clear();
                splAyudaProductoFormula.Children.Add(skAyudaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            } 
        }

        /// <summary>
        /// Agrega la ayuda para busdcar una formula por su id o su formulario de busqueda
        /// </summary>
        private void AgregarAyudaFormula(){
            try
            {
                skAyudaFormula = new SKAyuda<FormulaInfo>(170,
                    false,
                    new FormulaInfo()
                    {
                        Descripcion = string.Empty,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadFormulaId",
                    "PropiedadDescripcion",
                    true,
                    50,
                    true)
                {
                    AyudaPL = new FormulaPL(),
                    MensajeClaveInexistente = Properties.Resources.MuestreoFibra_msgAyudaFormula_ClaveInvalida,
                    MensajeBusquedaCerrar = Properties.Resources.MuestreoFibra_msgAyudaFormula_Cerrar,
                    MensajeBusqueda = Properties.Resources.MuestreoFibra_msgAyudaFormula_Buscar,
                    MensajeAgregar = Properties.Resources.MuestreoFibra_msgAyudaFormula_Agregar,
                    TituloEtiqueta = Properties.Resources.MuestreoFibra_msgAyudaFormula_Etiqueta,
                    TituloPantalla = Properties.Resources.MuestreoFibra_msgAyudaFormula_Titulo,
                    MetodoPorDescripcion = "ObtenerPorPagina"
                };

                skAyudaFormula.ObtenerDatos += ObtenerDatosAyudaProductoFormula;
                skAyudaFormula.LlamadaMetodosNoExistenDatos += LimpiarTodoAyudaProductoFormula;

                //skAyudaFormula.AsignaTabIndex(1);
                splAyudaProductoFormula.Children.Clear();
                splAyudaProductoFormula.Children.Add(skAyudaFormula);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            } 
        }

        /// <summary>
        /// Obtiene la informacion que retorna la ayuda para la formula
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosAyudaProductoFormula(string filtro)
        {
            if (rbFormula.IsChecked.Value)
            {
                formulaInfoSeleccionado = skAyudaFormula.Info;
            }
            else
            {
                productoInfoSeleccionado = skAyudaProducto.Info;
            }

            InicializarAyuda(); 
        }

        /// <summary>
        /// Limpia la informacion de las ayudas
        /// </summary>
        private void LimpiarTodoAyudaProductoFormula()
        {
            if (rbFormula.IsChecked.Value)
            {
                skAyudaFormula.Info = null;
            }
            else
            {
                skAyudaProducto.Info = null;
            }
            InicializarAyuda();
        }

        /// <summary>
        /// Inicializa los criterios de busqueda para las ayudas
        /// </summary>
        private void InicializarAyuda() {
            if (rbFormula.IsChecked.Value)
            {
                skAyudaFormula.Info = new FormulaInfo()
                {
                    Descripcion = string.Empty,
                    Activo = EstatusEnum.Activo
                };
            }
            else
            {
                skAyudaProducto.Info = new ProductoInfo()
                {
                    FamiliaId = (int)FamiliasEnum.MateriaPrimas,
                    SubfamiliaId = (int)SubFamiliasEnum.Forrajes,
                    Activo = EstatusEnum.Activo
                };
            }
        }

        /// <summary>
        /// Limpia los datos de muestreo para el muestreo de tipo producto
        /// </summary>
        private void LimpiarProductoDatosMuestreos() 
        {
            txtProductoPesoMuestra.Value = 0;
            txtProductoPesoGranoGrueso.Value = 0;
            txtProductoPesoGranoMediano.Value = 0;
            txtProductoPesoGranoFino.Value = 0;
            txtProductoPesoCribaEntrada.Value = 0;
            txtProductoPesoCribaSalida.Value = 0;
            txtProductoObservaciones.Text = String.Empty;
            txtProductoPesoMuestra.Focus();
        }

        /// <summary>
        /// Limpia los datos de muestreo para el muestreo de tipo formula
        /// </summary>
        private void LimpiarFormulaDatosMuestreos() 
        {
            txtFormulaPesoFibraGrande.Value = 0;
            txtFormulaPesoFibraMediana.Value = 0;
            txtFormulaPesoFinosBase.Value = 0;
            txtFormulaPesoFinosTamiz.Value = 0;
            txtFormulaPesoInicial.Value = 0;
            txtFormulaOrigen.Text = String.Empty;
            txtFormulaPesoInicial.Focus();
        }

        /// <summary>
        /// Valida el muestreo de tipo Formula
        /// </summary>
        /// <returns></returns>
        private bool ValidarMuestreoFormula()
        {
            if (formulaInfoSeleccionado == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.MuestreoFibra_msgSeleccioneFormula,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                skAyudaFormula.AsignarFoco();
                return false;
            }
            if(chkFormulaGrande.IsChecked.Value == false && 
               chkFormulaMediana.IsChecked.Value == false &&
               chkFormulaFinosTamiz.IsChecked.Value == false &&
               chkFormulaFinosBase.IsChecked.Value == false)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.MuestreoFibra_msgAlMenosUnParametro,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }

            if (txtFormulaPesoInicial.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.MuestreoFibra_msgCapturaPesoInicial,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtFormulaPesoInicial.Focus();
                return false;
            }
            if (chkFormulaGrande.IsChecked.Value && txtFormulaPesoFibraGrande.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaPesoFibraGrande,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtFormulaPesoFibraGrande.Focus();
                return false;
            }
            if (chkFormulaMediana.IsChecked.Value && txtFormulaPesoFibraMediana.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaPesoFibraMediana,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtFormulaPesoFibraMediana.Focus();
                return false;
            }
            if (chkFormulaFinosTamiz.IsChecked.Value && txtFormulaPesoFinosTamiz.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaPesoFibraFinoTamiz,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtFormulaPesoFinosTamiz.Focus();
                return false;
            }
            if (chkFormulaFinosBase.IsChecked.Value && txtFormulaPesoFinosBase.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaPesoFibraFinoBase,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtFormulaPesoFinosBase.Focus();
                return false;
            }
            if (txtFormulaOrigen.Text == String.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaOrigen,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtFormulaOrigen.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Obtiene los datos de muestreo de tipo formula
        /// </summary>
        /// <param name="formulaInfo"></param>
        /// <returns></returns>
        private MuestreoFibraFormulaInfo ObtenerMuestreoFormula(FormulaInfo formulaInfo)
        {
            MuestreoFibraFormulaInfo muestraFormulaInfo = new MuestreoFibraFormulaInfo()
            {
                Organizacion = new OrganizacionInfo()
                {
                    OrganizacionID = organizacionID
                },
                Formula = formulaInfo,
                PesoInicial = (decimal)txtFormulaPesoInicial.Value,
                Grande = chkFormulaGrande.IsChecked.Value,
                PesoFibraGrande = (decimal)txtFormulaPesoFibraGrande.Value,
                Mediana = chkFormulaMediana.IsChecked.Value,
                PesoFibraMediana = (decimal)txtFormulaPesoFibraMediana.Value,
                FinoTamiz = chkFormulaFinosTamiz.IsChecked.Value,
                PesoFinoTamiz = (decimal)txtFormulaPesoFinosTamiz.Value,
                FinoBase = chkFormulaFinosBase.IsChecked.Value,
                PesoFinoBase = (decimal)txtFormulaPesoFinosBase.Value,
                Origen = txtFormulaOrigen.Text,
                Activo = true,
                UsuarioCreacion = new UsuarioInfo() { UsuarioID = usuarioID }
            };
            muestraFormulaInfo.PesoNeto = 0;
            muestraFormulaInfo.PesoNeto += muestraFormulaInfo.PesoFibraGrande;
            muestraFormulaInfo.PesoNeto += muestraFormulaInfo.PesoFibraMediana;
            muestraFormulaInfo.PesoNeto += muestraFormulaInfo.PesoFinoBase;
            muestraFormulaInfo.PesoNeto += muestraFormulaInfo.PesoFinoTamiz;
            return muestraFormulaInfo;
        }

        /// <summary>
        /// Restaura los datos de muestreo de tipo formula
        /// </summary>
        /// <param name="muestreoFormulaInfo"></param>
        private void RestaurarMuestreoFormula(MuestreoFibraFormulaInfo muestreoFormulaInfo)
        {
            LimpiarFormulaDatosMuestreos();
            formulaInfoSeleccionado = new FormulaInfo(){ 
                FormulaId = muestreoFormulaInfo.Formula.FormulaId,
                Descripcion = muestreoFormulaInfo.Formula.Descripcion
            };
            skAyudaFormula.Info = formulaInfoSeleccionado;
            skAyudaFormula.Clave = muestreoFormulaInfo.Formula.FormulaId.ToString();
            skAyudaFormula.Descripcion = muestreoFormulaInfo.Formula.Descripcion;
            
            chkFormulaGrande.IsChecked = muestreoFormulaInfo.Grande;
            txtFormulaPesoFibraGrande.IsEnabled = muestreoFormulaInfo.Grande;
            chkFormulaMediana.IsChecked = muestreoFormulaInfo.Mediana;
            txtFormulaPesoFibraMediana.IsEnabled = muestreoFormulaInfo.Mediana;
            chkFormulaFinosTamiz.IsChecked = muestreoFormulaInfo.FinoTamiz;
            txtFormulaPesoFinosTamiz.IsEnabled = muestreoFormulaInfo.FinoTamiz;
            chkFormulaFinosBase.IsChecked = muestreoFormulaInfo.FinoBase;
            txtFormulaPesoFinosBase.IsEnabled = muestreoFormulaInfo.FinoBase;
 
            txtFormulaPesoInicial.Value = muestreoFormulaInfo.PesoInicial;
            txtFormulaPesoFibraGrande.Value = muestreoFormulaInfo.PesoFibraGrande;
            txtFormulaPesoFibraMediana.Value = muestreoFormulaInfo.PesoFibraMediana;
            txtFormulaPesoFinosTamiz.Value = muestreoFormulaInfo.PesoFinoTamiz;
            txtFormulaPesoFinosBase.Value = muestreoFormulaInfo.PesoFinoBase;
            txtFormulaOrigen.Text = muestreoFormulaInfo.Origen;
            btnAgregarActualizar.Content = Properties.Resources.MuestreoFibra_btnActualizar;
        }

        /// <summary>
        /// Valida el muestreo de tipo producto
        /// </summary>
        /// <returns></returns>
        private bool ValidarMuestreoProducto()
        {
            /*if (productoInfoSeleccionado == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       "Seleccione producto",
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                skAyudaProducto.AsignarFoco();
                return false;
            }*/
            if (chkProductoGruesos.IsChecked.Value == false &&
               chkProductoMedianos.IsChecked.Value == false &&
               chkProductoFinos.IsChecked.Value == false )
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgAlMenosUnParametro,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }

            if (txtProductoPesoMuestra.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaMuestra,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtProductoPesoMuestra.Focus();
                return false;
            }

            if (chkProductoGruesos.IsChecked.Value && txtProductoPesoGranoGrueso.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                      Properties.Resources.MuestreoFibra_msgCapturaPesoGranoGrueso,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtProductoPesoGranoGrueso.Focus();
                return false;
            }
            if (chkProductoMedianos.IsChecked.Value && txtProductoPesoGranoMediano.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaPesoGranoMediano,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtProductoPesoGranoMediano.Focus();
                return false;
            }
            if (chkProductoFinos.IsChecked.Value && txtProductoPesoGranoFino.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCapturaPesoGranoFino,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtProductoPesoGranoFino.Focus();
                return false;
            }
            if (txtProductoPesoCribaEntrada.Value > maximoCribaEntrada)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCribaSuperior,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtProductoPesoCribaEntrada.Focus();
                return false;
            }
            if (txtProductoPesoCribaSalida.Value > maximoCribaSalida)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgCribaSuperior,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtProductoPesoCribaSalida.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Obtiene los datos de muestreo de tipo producto
        /// </summary>
        /// <param name="ingredienteInfo"></param>
        /// <returns></returns>
        private MuestreoFibraProductoInfo ObtenerMuestreoProducto(ProductoInfo productoInfo)
        {
            MuestreoFibraProductoInfo muestraProductoInfo = new MuestreoFibraProductoInfo()
            {
                Organizacion = new OrganizacionInfo()
                {
                    OrganizacionID = organizacionID
                },
                Producto = productoInfo,
                PesoMuestra = (decimal)txtProductoPesoMuestra.Value,
                Grueso = chkProductoGruesos.IsChecked.Value,
                PesoGranoGrueso = (decimal)txtProductoPesoGranoGrueso.Value,
                Mediano = chkProductoMedianos.IsChecked.Value,
                PesoGranoMediano = (decimal)txtProductoPesoGranoMediano.Value,
                Fino = chkProductoFinos.IsChecked.Value,
                PesoGranoFino = (decimal)txtProductoPesoGranoFino.Value,
                CribaEntrada = (int)txtProductoPesoCribaEntrada.Value,
                CribaSalida = (int)txtProductoPesoCribaSalida.Value,
                Observaciones = txtProductoObservaciones.Text,
                Activo = true,
                UsuarioCreacion = new UsuarioInfo() { UsuarioID = usuarioID }
            };
            muestraProductoInfo.PesoNeto = 0;
            muestraProductoInfo.PesoNeto += muestraProductoInfo.PesoGranoFino;
            muestraProductoInfo.PesoNeto += muestraProductoInfo.PesoGranoGrueso;
            muestraProductoInfo.PesoNeto += muestraProductoInfo.PesoGranoMediano;
            return muestraProductoInfo;
        }

        /// <summary>
        /// Resttaura los datos de muestreo de tipo producto
        /// </summary>
        /// <param name="muestreoIngredienteInfo"></param>
        private void RestaurarMuestreoProducto(MuestreoFibraProductoInfo muestreoProductoInfo)
        {
            LimpiarProductoDatosMuestreos();
            productoInfoSeleccionado = new ProductoInfo() {
               ProductoId = muestreoProductoInfo.Producto.ProductoId,
               Descripcion = muestreoProductoInfo.Producto.Descripcion
            };

            skAyudaProducto.Info = productoInfoSeleccionado;
            skAyudaProducto.Clave = muestreoProductoInfo.Producto.ProductoId.ToString();
            skAyudaProducto.Descripcion = muestreoProductoInfo.Producto.Descripcion;

            chkProductoFinos.IsChecked = muestreoProductoInfo.Fino;
            txtProductoPesoGranoFino.IsEnabled = muestreoProductoInfo.Fino;
            chkProductoMedianos.IsChecked = muestreoProductoInfo.Mediano;
            txtProductoPesoGranoMediano.IsEnabled = muestreoProductoInfo.Mediano;
            chkProductoGruesos.IsChecked = muestreoProductoInfo.Grueso;
            txtProductoPesoGranoGrueso.IsEnabled = muestreoProductoInfo.Grueso;
            txtProductoPesoMuestra.Value = muestreoProductoInfo.PesoMuestra;
            txtProductoPesoGranoFino.Value = muestreoProductoInfo.PesoGranoFino;
            txtProductoPesoGranoMediano.Value = muestreoProductoInfo.PesoGranoMediano;
            txtProductoPesoGranoGrueso.Value = muestreoProductoInfo.PesoGranoGrueso;
            txtProductoPesoCribaEntrada.Value = muestreoProductoInfo.CribaEntrada;
            txtProductoPesoCribaSalida.Value = muestreoProductoInfo.CribaSalida;
            txtProductoObservaciones.Text = muestreoProductoInfo.Observaciones;
        }
    #endregion

    #region Eventos
        //Se activa cuando se pierde el foco del control
        private void TxtDecimal_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var txtEditado = (DecimalUpDown)sender;
            if (txtEditado.Value == null) { txtEditado.Value = 0; }
        }

        //Se activa cuando hacen click sobre el boton editar en el grid
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            bolEditando = true;
            btnAgregarActualizar.Content = Properties.Resources.MuestreoFibra_btnActualizar;
            if (rbFormula.IsChecked.Value)
            {
                btnAgregarActualizar.Tag = dgFormulasMuestreo.SelectedIndex;
                RestaurarMuestreoFormula((MuestreoFibraFormulaInfo)dgFormulasMuestreo.SelectedItem);
            }
            else
            {
                btnAgregarActualizar.Tag = dgProductosMuestreo.SelectedIndex;
                RestaurarMuestreoProducto((MuestreoFibraProductoInfo)dgProductosMuestreo.SelectedItem);
            }
        }

        //Se actiba cuando hacen click sobre el boton AgregarActualizar
        private void BtnAgregarActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (rbFormula.IsChecked.Value && ValidarMuestreoFormula())
            {
                if (bolEditando)
                { 
                    listaMuestreoFibraFormulaInfo[(int)btnAgregarActualizar.Tag] = ObtenerMuestreoFormula(formulaInfoSeleccionado);
                }
                else
                {
                    listaMuestreoFibraFormulaInfo.Add(ObtenerMuestreoFormula(formulaInfoSeleccionado));
                }
                dgFormulasMuestreo.Items.Refresh();
                //InicializarMuestreoFibraFormula();
                LimpiarFormulaDatosMuestreos();
            }
            if(rbProducto.IsChecked.Value && ValidarMuestreoProducto())
            {
                if (bolEditando)
                {
                    listaMuestreoFibraProductoInfo[(int)btnAgregarActualizar.Tag] = ObtenerMuestreoProducto(productoInfoSeleccionado);
                }
                else
                {
                    listaMuestreoFibraProductoInfo.Add(ObtenerMuestreoProducto(productoInfoSeleccionado));
                }
                dgProductosMuestreo.Items.Refresh();
                //InicializarMuestreoFibraProducto();
                LimpiarProductoDatosMuestreos();
            }
            bolEditando = false;
            btnAgregarActualizar.Content = Properties.Resources.MuestreoFibra_btnAgregar; 
        }

        //Se activa cuando seleccionan una opcion del grupo
        private void RbgFormulaProducto_Checked(object sender, RoutedEventArgs e)
        {
            InicializarFormulario();
            if (rbFormula.IsChecked.Value)
            {
                InicializarMuestreoFibraFormula();
            }
            else
            {
                InicializarMuestreoFibraProducto();
            }
        }

        //Se activa cuando hacen click sobre algun checkBox del tipo Formula
        private void ChkFormula_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chekBox = (CheckBox)sender;
            if (chekBox.Name == chkFormulaGrande.Name)
            {
                txtFormulaPesoFibraGrande.Value = 0;
                txtFormulaPesoFibraGrande.IsEnabled = !txtFormulaPesoFibraGrande.IsEnabled;
            }
            else if (chekBox.Name == chkFormulaMediana.Name)
            {
                txtFormulaPesoFibraMediana.Value = 0;
                txtFormulaPesoFibraMediana.IsEnabled = !txtFormulaPesoFibraMediana.IsEnabled;
            }
            else if (chekBox.Name == chkFormulaFinosTamiz.Name)
            {
                txtFormulaPesoFinosTamiz.Value = 0;
                txtFormulaPesoFinosTamiz.IsEnabled = !txtFormulaPesoFinosTamiz.IsEnabled;
            }
            else if (chekBox.Name == chkFormulaFinosBase.Name)
            {
                txtFormulaPesoFinosBase.Value = 0;
                txtFormulaPesoFinosBase.IsEnabled = !txtFormulaPesoFinosBase.IsEnabled;
            }
        }

        //Se activa cuando hacen click sobre algun ckeckBox del tipo Producto
        private void ChkProducto_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chekBox = (CheckBox)sender;
            if (chekBox.Name == chkProductoGruesos.Name)
            {
                txtProductoPesoGranoGrueso.Value = 0;
                txtProductoPesoGranoGrueso.IsEnabled = !txtProductoPesoGranoGrueso.IsEnabled;
            }
            else if (chekBox.Name == chkProductoMedianos.Name)
            {
                txtProductoPesoGranoMediano.Value = 0;
                txtProductoPesoGranoMediano.IsEnabled = !txtProductoPesoGranoMediano.IsEnabled;
            }
            else if (chekBox.Name == chkProductoFinos.Name)
            {
                txtProductoPesoGranoFino.Value = 0;
                txtProductoPesoGranoFino.IsEnabled = !txtProductoPesoGranoFino.IsEnabled;
            }
        }

        //Se activa cuando hacen click sobre el boton Limpiar
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (rbFormula.IsChecked.Value)
            {
                //InicializarMuestreoFibraFormula();
                LimpiarFormulaDatosMuestreos();
            }
            else
            {
                //InicializarMuestreoFibraProducto();
                LimpiarProductoDatosMuestreos();
            }
            bolEditando = false;
            btnAgregarActualizar.Content = Properties.Resources.MuestreoFibra_btnGuardar;
        }

        //Se activa cuando hacen click sobre el boton Guardar
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (listaMuestreoFibraFormulaInfo.Count == 0 && listaMuestreoFibraProductoInfo.Count == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuestreoFibra_msgAlMenosUno,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                if (rbFormula.IsChecked.Value)
                {
                    skAyudaFormula.AsignarFoco();
                }
                else
                {
                    skAyudaProducto.AsignarFoco();
                }
            }
            else
            {
                bool guardoMuestreo = false;
                var muestreoTamanoFibraPl = new MuestreoTamanoFibraPL();
                if (rbFormula.IsChecked.Value)
                {
                    guardoMuestreo = muestreoTamanoFibraPl.Guardar(listaMuestreoFibraFormulaInfo);
                }
                else
                {
                    guardoMuestreo = muestreoTamanoFibraPl.Guardar(listaMuestreoFibraProductoInfo);
                }
                if (guardoMuestreo)
                {
                    InicializarFormulario();
                    if (rbFormula.IsChecked.Value)
                    {
                        InicializarMuestreoFibraFormula();
                        skAyudaFormula.AsignarFoco();
                    }
                    else
                    {
                        InicializarMuestreoFibraProducto();
                        //skAyudaProducto.AsignarFoco();
                    }
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.MuestreoFibra_msgGuardar,
                        MessageBoxButton.OK, MessageImage.Correct);
                }
            }
        }

        //Se activa cuando hacen click sobre el boton Cancelar
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
              Properties.Resources.MuestreoFibra_msgCancelar,
              MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializarFormulario();
                if (rbFormula.IsChecked.Value)
                {
                    InicializarMuestreoFibraFormula();
                    skAyudaFormula.AsignarFoco();
                }
                else
                {
                    InicializarMuestreoFibraProducto();
                    //skAyudaProducto.AsignarFoco();
                }
                 
            }
        }
    #endregion
    }
}
