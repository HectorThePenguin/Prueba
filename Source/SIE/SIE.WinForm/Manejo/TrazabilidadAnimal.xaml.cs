using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TrazabilidadAnimal.xaml
    /// </summary>
    public partial class TrazabilidadAnimal
    {
        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TrazabilidadAnimalInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TrazabilidadAnimalInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TrazabilidadAnimal()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TrazabilidadAnimalInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo
                    {
                        TipoOrganizacionID = 2
                    }
                },
                Activo = EstatusEnum.Activo,
                Animal = new AnimalInfo()
            };
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            DgMovimientoAnimal.ItemsSource = null;
        }

        #endregion

        #region Eventos
        /// <summary>
        /// On Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrazabilidadAnimal_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                skAyudaOrganizacion.txtClave.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento Obtener costos del animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCostos_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contexto != null && Contexto.Animal != null &&
                    Contexto.Animal.ListaConsumosAnimal != null &&
                    Contexto.Animal.ListaCostosAnimal.Any())
                {
                    var trazabilidadCostosDialog = new TrazabilidadAnimalCostos(Contexto.Animal.ListaCostosAnimal)
                    {
                        Left = (ActualWidth - Width) / 2,
                        Top = ((ActualHeight - Height) / 2) + 132,
                        Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                    };
                    trazabilidadCostosDialog.ShowDialog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_SinCostos,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorCostos,
                                MessageBoxButton.OK,
                                MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento obtener los consumos del animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConsumo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contexto != null && Contexto.Animal != null &&
                    Contexto.Animal.ListaConsumosAnimal != null &&
                    Contexto.Animal.ListaConsumosAnimal.Any())
                {
                    var trazabilidadConsumoDialog = new TrazabilidadAnimalConsumo(Contexto.Animal.ListaConsumosAnimal)
                    {
                        Left = (ActualWidth - Width)/2,
                        Top = ((ActualHeight - Height)/2) + 132,
                        Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                    };
                    trazabilidadConsumoDialog.ShowDialog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_SinConsumo,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorConsumo,
                                MessageBoxButton.OK,
                                MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento del boton buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Si no se selecciono una organización
                if (Contexto.Organizacion.OrganizacionID > 0)
                {
                    //Si no se capturaron los aretes del animal
                    if (!(String.IsNullOrEmpty(Contexto.Animal.Arete.Trim()) && 
                        String.IsNullOrEmpty(Contexto.Animal.AreteMetalico.Trim())))
                    {
                        //Se manda buscar los datos del animal 
                        var animalPl = new AnimalPL();
                        Contexto = animalPl.ObtenerTrazabilidadAnimal(Contexto, true);
                        if (Contexto != null)
                        {
                            if (Contexto.ExisteDuplicados && Contexto.AnimalesDuplicados.Any())
                            {
                                //Me muestra un dialogo para seleccionar de los aretes encontrados
                                AnimalesDuplicados();
                            }
                            else
                            {
                                if (Contexto.Animal != null && Contexto.Animal.AnimalID > 0)
                                {
                                    DgMovimientoAnimal.ItemsSource = Contexto.Animal.ListaAnimalesMovimiento;
                                    Habilitar(true);
                                }
                                else
                                {
                                    txtArete.Focus();
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.TrazabilidadAnimal_NoExisteArete,
                                        MessageBoxButton.OK,
                                        MessageImage.Warning);
                                }
                            }
                        }
                        else
                        {
                            txtArete.Focus();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.TrazabilidadAnimal_NoExisteArete,
                             MessageBoxButton.OK,
                             MessageImage.Warning);
                        }
                    }
                    else
                    {
                        txtArete.Focus();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.TrazabilidadAnimal_CapturarArete,
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                    }
                }
                else
                {
                    skAyudaOrganizacion.txtClave.Focus();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_SeleccionarOrganizacion,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorAlBuscar, 
                                MessageBoxButton.OK, 
                                MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para limpiar el contexto de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Limpiar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento para mostrar los productos aplicados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMostrarProductosAplicadosClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            dynamic animalMovimientoInfo = btn.CommandParameter;
            animalMovimientoInfo = (AnimalMovimientoInfo)animalMovimientoInfo;

            if (animalMovimientoInfo != null && 
                animalMovimientoInfo.ListaTratamientosAplicados != null)
            {
                var listaTratamientos = (List<TratamientoAplicadoInfo>)animalMovimientoInfo.ListaTratamientosAplicados;
                if (listaTratamientos.Any())
                {
                    var trazabilidadAnimalProductoDialog = new TrazabilidadAnimalProducto(animalMovimientoInfo)
                    {
                        Left = (ActualWidth - Width) / 2,
                        Top = ((ActualHeight - Height) / 2) + 132,
                        Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                    };
                    trazabilidadAnimalProductoDialog.ShowDialog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_SinProductos,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.TrazabilidadAnimal_SinProductos,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
            }
        }

        /// <summary>
        /// Evento para mostrar los costos de abasto del animal
        /// </summary>
        /// <param name="sender">Parametro estandar sender</param>
        /// <param name="e"> Parametro estandar e </param>
        private void btnCostosAbastos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contexto != null && Contexto.Animal != null &&
                    Contexto.Animal.ListaCostosAbastoAnimal.Any())
                {
                    var trazabilidadCostosAbastoDialog = new TrazabilidadAnimalCostosAbasto(Contexto.Animal.ListaCostosAbastoAnimal)
                    {
                        Left = (ActualWidth - Width) / 2,
                        Top = ((ActualHeight - Height) / 2) + 132,
                        Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                    };
                    trazabilidadCostosAbastoDialog.ShowDialog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_SinCostos,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorCostos,
                                MessageBoxButton.OK,
                                MessageImage.Error);
            }
        }

        /// <summary>
        /// Muestra la pantalla de historial de consumo en abastos del animal
        /// </summary>
        /// <param name="sender">objeto que desencadeno el evento</param>
        /// <param name="e">parametros del evento</param>
        private void btnConsumoAbastos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contexto != null && Contexto.Animal != null && Contexto.Animal.ListaConsumoAbastoAnimal.Any())
                {
                    var trazabilidadConsumoAbastoDialog = new TrazabilidadAnimalConsumoAbasto(Contexto.Animal.ListaConsumoAbastoAnimal)
                    {
                        Left = (ActualWidth - Width) / 2,
                        Top = ((ActualHeight - Height) / 2) + 132,
                        Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                    };
                    trazabilidadConsumoAbastoDialog.ShowDialog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.TrazabilidadAnimal_SinConsumo,
                                   MessageBoxButton.OK,
                                   MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorConsumo,
                                MessageBoxButton.OK,
                                MessageImage.Error);
            }
        }


        #endregion

        #region Metodos

        /// <summary>
        /// Metodo para mostrar los animales duplicados
        /// </summary>
        private void AnimalesDuplicados()
        {
            try
            {
                var animalPl = new AnimalPL();
                // Se manda abrir el dialogo de los animales duplicados
                var trazabilidadAnimalDuplicado = new TrazabilidadAnimalDuplicado(Contexto.AnimalesDuplicados)
                {
                    Left = (ActualWidth - Width) / 2,
                    Top = ((ActualHeight - Height) / 2) + 132,
                    Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                };
                trazabilidadAnimalDuplicado.ShowDialog();
                if (trazabilidadAnimalDuplicado.AnimalSeleccionado != null)
                {
                    Contexto.Animal = trazabilidadAnimalDuplicado.AnimalSeleccionado;

                    if (Contexto.Animal != null && Contexto.Animal.AnimalID > 0)
                    {
                        //Se busca la trazabilidad de animal seleccionado
                        Contexto = animalPl.ObtenerTrazabilidadAnimal(Contexto, false);

                        if (Contexto.Animal != null && Contexto.Animal.AnimalID > 0)
                        {
                            DgMovimientoAnimal.ItemsSource = Contexto.Animal.ListaAnimalesMovimiento;
                            Habilitar(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorMostrarAretesDuplicados,
                                MessageBoxButton.OK,
                                MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para limpiar pantalla
        /// </summary>
        private void Limpiar()
        {
            InicializaContexto();
            Habilitar(false);
            skAyudaOrganizacion.txtClave.Focus();
        }

        /// <summary>
        /// Metodo para habilitar controles
        /// </summary>
        private void Habilitar(bool estado)
        {
            btnConsumo.IsEnabled = estado;
            btnCostos.IsEnabled = estado;
            btnCostosAbastos.IsEnabled = estado;
            btnConsumoAbastos.IsEnabled = estado;

            txtArete.IsEnabled = !estado;
            txtAreteRFID.IsEnabled = !estado;
            skAyudaOrganizacion.IsEnabled = !estado;
        }
        #endregion
    }
}
