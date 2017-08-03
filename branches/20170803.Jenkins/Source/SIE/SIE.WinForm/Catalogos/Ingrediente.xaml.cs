using System;                      
using System.Collections.Generic;  
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
using System.Linq;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Ingrediente.xaml
    /// </summary>
    public partial class Ingrediente
    {
        
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private IngredienteInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (IngredienteInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Ingrediente()
        {
            InitializeComponent();
        }
        #endregion 

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TipoFormulaPL tipoFormulaPL = new TipoFormulaPL();
                
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaProducto.ObjetoNegocio = new ProductoPL();
                skAyudaFormula.ObjetoNegocio = new FormulaPL();

                skAyudaOrganizacion.Contexto = Contexto.Organizacion;

                Contexto.Producto = new ProductoInfo
                {
                    Familias = new List<FamiliaInfo>
                                {
                                    new FamiliaInfo
                                    {
                                        FamiliaID =
                                            FamiliasEnum.MateriaPrimas.GetHashCode()
                                    },
                                new FamiliaInfo
                                    {
                                        FamiliaID =
                                            FamiliasEnum.Premezclas.GetHashCode()
                                    },
                                    new FamiliaInfo
                                    {
                                        FamiliaID =
                                            FamiliasEnum.Alimento.GetHashCode()
                                    }
                                }
                };

                //skAyudaProducto.DataContext = Contexto.Producto;
                skAyudaOrganizacion.LimpiarCampos();
                skAyudaProducto.LimpiarCampos();
                skAyudaFormula.LimpiarCampos();
                List<TipoFormulaInfo> tipos = tipoFormulaPL.ObtenerTodos() as List<TipoFormulaInfo> ;

                cmbTipoFormula.ItemsSource = (from tipo in tipos where tipo.Activo == EstatusEnum.Activo select tipo).ToList();
                cmbTipoFormula.DisplayMemberPath = "Descripcion";
                cmbTipoFormula.SelectedValuePath = "TipoFormulaID";

                ucPaginacion.DatosDelegado += ObtenerListaIngrediente;
                ucPaginacion.AsignarValoresIniciales();

                Buscar();

                skAyudaOrganizacion.AsignarFoco();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Ingrediente_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Ingrediente_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evente que levanta la ventana para registrar un nuevo ingrediente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            var ingredienteEdicion = new IngredienteEdicion()
            {
                ucTitulo = { TextoTitulo = Properties.Resources.Ingrediente_Registrar_Titulo }
            };
            MostrarCentrado(ingredienteEdicion);
            
            Buscar();
            skAyudaOrganizacion.AsignarFoco();
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
          Buscar();
        }
        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button) e.Source;
            try
            {
                //var formula = new FormulaInfo();

                var formula = (FormulaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (formula != null)
                {
                    var ingredienteEdicion = new IngredienteEdicion(formula)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Ingrediente_Editar_Titulo }
                        };
                    MostrarCentrado(ingredienteEdicion);
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Ingrediente_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new IngredienteInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Formula = new FormulaInfo
                {
                    Descripcion = " ",
                    FormulaId = 0,
                    TipoFormula = new TipoFormulaInfo()
                },
                Producto = new ProductoInfo(),
                Organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = 3}
                }
                //{
                //    OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                //}
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaIngrediente(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private IngredienteInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaIngrediente(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var ingredientePL = new IngredientePL();
                IngredienteInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                //ResultadoInfo<FormulaInfo> resultadoInfo = ingredientePL.ObtenerPorPaginaFormula(pagina, filtros);
                ResultadoInfo<FormulaInfo> resultadoInfo = ingredientePL.ObtenerPorPaginaFormulaOrganizacion(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<Ingrediente>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Ingrediente_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Ingrediente_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

