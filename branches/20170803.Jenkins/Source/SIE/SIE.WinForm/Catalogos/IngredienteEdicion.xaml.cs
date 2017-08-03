using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Info.Constantes;
using SIE.Base.Infos;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for IngredienteEdicion.xaml
    /// </summary>
    public partial class IngredienteEdicion
    {
        #region Propiedades

        private bool EsEdicion { get; set; }
        private SKAyuda<ProductoInfo> skAyudaProducto;

        private ContenedorIngredientesInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContenedorIngredientesInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private void CargarGridIngredientes()
        {
            gridDatosProducto.ItemsSource = null;
            var listaObservable = Contexto.ListaIngredientes.ConvertirAObservable().Where(x=> x.Activo == Contexto.Activo);
            gridDatosProducto.ItemsSource = listaObservable;
            RefrescarPorcentaje();
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
        public IngredienteEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            InicializaCampos();
            Contexto.Activo = EstatusEnum.Activo;
            Contexto.Formula.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto.Formula.UsuarioModificacionID = 0;
            
            skAyudaOrganizacion.BusquedaActivada = false;
            
            EsEdicion = false;
            CargarGridIngredientes();
            
        }

        /// <summary>
        /// Constructor para editar una entidad Ingrediente Existente
        /// </summary>
        /// <param name="formula"></param>
        public IngredienteEdicion(FormulaInfo formula)
        {
            EsEdicion = true;
            InitializeComponent();
            InicializaCampos();
            Contexto.ListaIngredientes = formula.ListaIngredientes;
            
            if (formula.ListaIngredientes.Count > 0 && formula.ListaIngredientes.First().Organizacion != null)
            {
                Contexto.Organizacion = formula.ListaIngredientes.First().Organizacion;

                for (int i = 0; i < Contexto.ListaIngredientes.Count; i++)
                {
                    if (Contexto.ListaIngredientes[i].Producto.Descripcion == null || Contexto.ListaIngredientes[i].Producto.Descripcion.Trim().Length == 0)
                    Contexto.ListaIngredientes[i].Producto.Descripcion = Contexto.ListaIngredientes[i].Producto.ProductoDescripcion;
                }
            }
            else
            {
                Contexto.Organizacion = new OrganizacionInfo();
            }
            Contexto.Formula = formula;
            Contexto.Activo = formula.Activo;
            formula.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            CargarGridIngredientes();
            skAyudaFormula.IsEnabled = false;
            skAyudaOrganizacion.IsEnabled = false;
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
            gridDatosProducto.ItemsSource = Contexto.ListaIngredientes;
            gridDatosProducto.DataContext = Contexto.ListaIngredientes;
            CargarGridIngredientes();
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

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Nuevo Producto
        /// </summary>
        private void BotonNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ingrediente = new IngredienteInfo
                {
                    Producto = new ProductoInfo
                    {
                        //Descripcion = " ",
                        SubFamilia = new SubFamiliaInfo(),
                        Familia = new FamiliaInfo(),
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
                            },
                    },
                    PorcentajeProgramado = 0.0M,
                    Activo = EstatusEnum.Activo,
                    HabilitaEdicion = true,
                    TieneCambios = true,
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                };

                Contexto.ListaIngredientes.Add(ingrediente);
                gridDatosProducto.ItemsSource = null;
                gridDatosProducto.ItemsSource = Contexto.ListaIngredientes;
                
                CargarGridIngredientes();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.IngredienteEdicionProducto_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Evento que se ejecuta cuando el Control es creado
        /// </summary>
        private void stpAyudaProducto_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stackPanel = (StackPanel)sender;

                if (stackPanel.Children.Count == 0)
                {
                    AgregarAyudaProducto(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            

            Contexto = new ContenedorIngredientesInfo
            {
                Organizacion = new OrganizacionInfo(),
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                ListaIngredientes = new List<IngredienteInfo>(),
                Formula = new FormulaInfo
                {
                    TipoFormula = new TipoFormulaInfo()
                }
            };
        }

        private void InicializaCampos()
        {
            skAyudaFormula.ObjetoNegocio = new FormulaPL();
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            

            skAyudaFormula.LimpiarCampos();
            skAyudaOrganizacion.LimpiarCampos();


        }

        /// <summary>
        /// Agrega control de Ayuda para Costo
        /// </summary>
        private void AgregarAyudaProducto(StackPanel stackPanel)
        {
            skAyudaProducto = new SKAyuda<ProductoInfo>(160, false, new ProductoInfo()
                                                        , "PropiedadProductoIDIngredientesFamilias"
                                                        , "PropiedadDescripcionIngredientesFamilias"
                                                        , true, 80, 10, true);
            skAyudaProducto.BindearCampos = true;
            skAyudaProducto.AyudaPL = new ProductoPL();

            skAyudaProducto.Info = new ProductoInfo
            {
                //Descripcion = " ",
                Familias = new List<FamiliaInfo>
                            {
                                new FamiliaInfo { FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Premezclas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Alimento.GetHashCode() },
                            },
                SubfamiliaId = 0
            };

            skAyudaProducto.MensajeClaveInexistente = Properties.Resources.AyudaProducto_CodigoInvalido;
            skAyudaProducto.TituloPantalla = Properties.Resources.AyudaProducto_Busqueda_Titulo;
            skAyudaProducto.MensajeBusqueda = Properties.Resources.AyudaProducto_Busqueda;
            skAyudaProducto.MensajeAgregar = Properties.Resources.AyudaProducto_Seleccionar;
            skAyudaProducto.MensajeBusquedaCerrar = Properties.Resources.AyudaProducto_SalirSinSeleccionar;
            skAyudaProducto.TituloEtiqueta = Properties.Resources.AyudaProducto_LeyendaBusqueda;
            
            skAyudaProducto.MetodoPaginadoBusqueda = "ObtenerIngredientesPorFamiliasPaginado";
            skAyudaProducto.MetodoPorDescripcion = "ObtenerIngredientesPorFamiliasPaginado";
            skAyudaProducto.MetodoPorId = "ObtenerIngredientesPorIDFamilias";
            
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProducto);

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
                if(Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = "Debe ingresar la organización.";
                    skAyudaOrganizacion.AsignarFoco();
                }

                if (resultado && Contexto.Formula.FormulaId == 0)
                {
                    resultado = false;
                    mensaje = "Debe ingresar la fórmula.";
                    skAyudaFormula.AsignarFoco();
                }

                if (resultado && Contexto.ListaIngredientes.Count == 0)
                {
                    resultado = false;
                    mensaje = "Debe agregar por lo menos un registro al grid de datos.";
                }

                if (resultado && Contexto.ListaIngredientes.Count > 0)
                {
                    foreach (var ingrediente in Contexto.ListaIngredientes)
                    {
                        if(ingrediente.Producto.ProductoId == 0)
                        {
                            resultado = false;
                            mensaje = "Debe ingresar el producto.";
                            break;
                        }
                    }
                }

                if (resultado && Contexto.ListaIngredientes.Count > 0)
                {
                    foreach (var ingrediente in Contexto.ListaIngredientes)
                    {
                        if (ingrediente.PorcentajeProgramado == 0)
                        {
                            resultado = false;
                            mensaje = "Debe ingresar el porcentaje programado.";
                            break;
                        }
                    }
                }


                decimal porcentajeFinal =
                    Contexto.ListaIngredientes.Where(ing => ing.Activo == EstatusEnum.Activo).Sum(
                        ing => ing.PorcentajeProgramado);

                if (resultado && porcentajeFinal != 100)
                {
                    resultado = false;
                    mensaje = string.Format(Properties.Resources.IngredienteEdicion_MsgPorcentajeIncorrecto,porcentajeFinal);
                }

                if(resultado && !BuscarRepetidos(Contexto.ListaIngredientes))
                {
                    resultado = false;
                    mensaje = string.Format(Properties.Resources.IngredienteEdicion_ProductoRepetido);
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

        private bool BuscarRepetidos(List<IngredienteInfo> listaIngredientes)
        {
            bool resultado = true;
            int total = 0;

            foreach(var ingrediente in listaIngredientes)
            {
                total = 0;
                if (ingrediente.Activo == EstatusEnum.Activo)
                {
                    foreach (var ingredienteAux in listaIngredientes)
                    {
                        if (ingredienteAux.Activo == EstatusEnum.Activo)
                        {
                            if (ingrediente.Producto.ProductoId == ingredienteAux.Producto.ProductoId)
                            {
                                total++;
                            }

                            if (total > 1)
                            {
                                resultado = false;
                                break;
                            }
                        }
                    }

                    if (!resultado)
                    {
                        break;
                    }
                }
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
                    var ingredientePL = new IngredientePL();
                    var formulaPL = new FormulaPL();
                    for (int i = 0; i < Contexto.ListaIngredientes.Count; i++ )
                    {
                        Contexto.ListaIngredientes[i].Organizacion = Contexto.Organizacion;
                        Contexto.ListaIngredientes[i].TieneCambios = true;
                        if (Contexto.ListaIngredientes[i].UsuarioCreacionID == 0)
                        {
                            Contexto.ListaIngredientes[i].UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                            Contexto.ListaIngredientes[i].UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                        }
                        else
                        {
                            Contexto.ListaIngredientes[i].UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                        }
                        
                        Contexto.ListaIngredientes[i].Formula = Contexto.Formula;
                    }
                    if (!EsEdicion)
                    {
                        ResultadoInfo<FormulaInfo> formulas = ingredientePL.ObtenerPorPaginaFormulaOrganizacion( new Base.Infos.PaginacionInfo{  Inicio = 1, Limite= 1}, new IngredienteInfo{Formula = Contexto.Formula, Organizacion = Contexto.Organizacion });

                        if (formulas == null || formulas.Lista.Count == 0)
                        {
                            ingredientePL.GuardarIngredientesXML(Contexto.ListaIngredientes.Where(ing => ing.TieneCambios).ToList());
                            formulaPL.Guardar(Contexto.Formula);


                            SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                            confirmaSalir = false;
                            Close();
                        }
                        else
                        {
                            SkMessageBox.Show(this, "Fórmula ya tiene configuración de ingredientes.", MessageBoxButton.OK, MessageImage.Error);
                            skAyudaFormula.LimpiarCampos();
                            skAyudaFormula.AsignarFoco();
                        }
                    }
                    else
                    {
                        ingredientePL.GuardarIngredientesXML(Contexto.ListaIngredientes.Where(ing => ing.TieneCambios).ToList());
                        formulaPL.Guardar(Contexto.Formula);


                        SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                        confirmaSalir = false;
                        Close();
                    }
                    
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Ingrediente_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Ingrediente_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        public bool validarDecimal(string numero)
        {
            bool resultado = false;

            decimal numeroDecimal = 0.0M;
            if (numero.Length > 0 && numero.Substring(0, 1) != "." && numero.Substring(numero.Length - 1, 1) != "." )
            {
                if (decimal.TryParse(numero, out numeroDecimal))
                {
                    
                    resultado = true;
                }
            }

            if(!resultado)
            {
                SkMessageBox.Show(this, "Numero invalido", MessageBoxButton.OK, MessageImage.Error);
            }
            return resultado;
        }

        private void RefrescarPorcentaje()
        {
            decimal porcentajeFinal =
                   Contexto.ListaIngredientes.Where(ing => ing.Activo == EstatusEnum.Activo).Sum(
                       ing => ing.PorcentajeProgramado);

            lblTotalPorcentaje.Content = porcentajeFinal;
        }
        #endregion Métodos

        private void txtPorcentaje_LostFocus(object sender, RoutedEventArgs e)
        {
            decimal numero = 0.0M;
            string valor = ((TextBox)sender).Text.Trim();
            if(!validarDecimal(valor))
            {
                ((TextBox)sender).Text = string.Empty;
            }
            else
            { 
                numero= decimal.Parse(valor);
                ((TextBox)sender).Text = string.Format("{0:0.000}", numero);
                RefrescarPorcentaje();
            }
        }

        private void cmbEstatus_DropDownClosed(object sender, EventArgs e)
        {
            RefrescarPorcentaje();
        }

        private void cmbEstatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contexto.Activo = (EstatusEnum)(((ComboBox)sender).SelectedItem);
            CargarGridIngredientes();
            RefrescarPorcentaje();
        }

        private void cmbEstatusAyudas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefrescarPorcentaje();
        }
    }
}

