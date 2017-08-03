using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using TextBox = Microsoft.Office.Interop.Excel.TextBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para ReemplazoArete.xaml
    /// </summary>
    public partial class ReemplazoArete
    {
        #region PROPIEDADES
        public FiltroReemplazoArete Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroReemplazoArete)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion PROPIEDADES

        #region CONSTRUCTORES
        public ReemplazoArete()
        {
            InitializeComponent();
            InicializaContexto();
        }
        #endregion CONSTRUCTORES

        #region EVENTOS

        private void ReemplazoArete_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                skAyudaFolioEntrada.ObjetoNegocio = new EntradaGanadoPL();
                skAyudaFolioEntrada.AyudaConDatos += (o, args) => ObtenerAretesPartida();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReemplazoArete_ErrorCargarPantalla, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnGuardarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidaGuardar())
                {
                    Guardar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReemplazoArete_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnCancelarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.ReemplazoArete_Cancelar,
                   MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    InicializaContexto();
                    dgAretes.ItemsSource = null;
                    //CargarAretesGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReemplazoArete_ErrorCargarPantalla, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtAreteNuevo_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void TxtAreteNuevo_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                //var elementoArete = (FiltroAnimalesReemplazoArete)dgAretes.CurrentCell.Item;
                //var animalPL = new AnimalPL();
                var text = (System.Windows.Controls.TextBox)sender;
                var elementoArete = Contexto.ListaAretes.FirstOrDefault(are => are.AreteCorte.Equals(text.Text)); //(FiltroAnimalesReemplazoArete)dgAretes.Items
                if (elementoArete == null)
                {
                    return;
                }
                var animalPL = new AnimalPL();
                if (string.IsNullOrWhiteSpace(elementoArete.AreteCorte))
                {
                    elementoArete.AreteCorte = elementoArete.AreteCentro;
                    return;
                }
                AnimalInfo animal = animalPL.ObtenerAnimalPorArete(elementoArete.AreteCorte,
                                                                   Contexto.EntradaGanado.OrganizacionID);
                if (animal != null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 string.Format(Properties.Resources.ReemplazoArete_ExisteArete, animal.FolioEntrada), MessageBoxButton.OK, MessageImage.Warning);
                    elementoArete.AreteCorte = elementoArete.AreteCentro;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReemplazoArete_ErrorValidarArete, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtAreteMetalicoNuevo_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var text = (System.Windows.Controls.TextBox)sender;
                var elementoArete = Contexto.ListaAretes.FirstOrDefault(are => are.AreteMetalicoCorte.Equals(text.Text)); //(FiltroAnimalesReemplazoArete)dgAretes.Items
                if (elementoArete == null)
                {
                    return;
                }
                var animalPL = new AnimalPL();
                if (string.IsNullOrWhiteSpace(elementoArete.AreteMetalicoCorte))
                {
                    elementoArete.AreteMetalicoCorte = elementoArete.AreteMetalicoCentro;
                    return;
                }

                AnimalInfo animal = animalPL.ObtenerAnimalPorAreteTestigo(elementoArete.AreteMetalicoCorte,
                                                                   Contexto.EntradaGanado.OrganizacionID);
                if (animal != null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 string.Format(Properties.Resources.ReemplazoArete_ExisteArete, animal.FolioEntrada), MessageBoxButton.OK, MessageImage.Warning);
                    elementoArete.AreteMetalicoCorte = elementoArete.AreteMetalicoCentro;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReemplazoArete_ErrorValidarArete, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS

        #region METODOS
        private void InicializaContexto()
        {
            Contexto = new FiltroReemplazoArete
            {
                EntradaGanado = new EntradaGanadoInfo
                {
                    OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                },
                UsuarioID = AuxConfiguracion.ObtenerUsuarioLogueado()
            };
        }

        private bool ValidaGuardar()
        {
            bool modificaciones =
                Contexto.ListaAretes.Any(
                    are => are.PermiteEditar && (!are.AreteCentro.Trim().Equals(are.AreteCorte.Trim())));
            if (!modificaciones)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ReemplazoArete_SinModificaciones, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            var repetidos = Contexto.ListaAretes.GroupBy(are => are.AreteCorte)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            if (repetidos.Any())
            {
                var primerRepetido = repetidos.FirstOrDefault();

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.ReemplazoArete_AreteRepetido, primerRepetido), MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }

        private void Guardar()
        {
            var entradaGanadoPL = new EntradaGanadoPL();
            entradaGanadoPL.GuardarReemplazoAretes(Contexto.ListaAretes.Where(
                    are => are.PermiteEditar && (!are.AreteCentro.Trim().Equals(are.AreteCorte.Trim()))).ToList(), Contexto.EntradaGanado);
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
            InicializaContexto();
            dgAretes.ItemsSource = null;
        }

        private void ObtenerAretesPartida()
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                List<FiltroAnimalesReemplazoArete> aretes = entradaGanadoPL.ObtenerReemplazoAretes(Contexto.EntradaGanado);
                if (aretes == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ReemplazoArete_SinAretes, MessageBoxButton.OK, MessageImage.Warning);
                    InicializaContexto();
                    return;
                }
                FiltroAnimalesReemplazoArete primerArete = aretes.FirstOrDefault();
                if (primerArete != null)
                {
                    Contexto.CabezasOrigen = primerArete.CabezasOrigen;
                    Contexto.CabezasRecibidas = primerArete.CabezasRecibidas;
                    Contexto.CabezasCortadas = aretes.Count(arete => arete.AnimalID != 0 && arete.FolioEntradaCorte == Contexto.EntradaGanado.FolioEntrada);
                }
                aretes.ForEach(are =>
                {
                    if (string.IsNullOrWhiteSpace(are.AreteCorte) || are.FolioEntradaCentro != are.FolioEntradaCorte)
                    {
                        are.PermiteEditar = true;
                        are.AreteCorte = are.AreteCentro;
                    }
                });
                Contexto.ListaAretes = aretes;
                CargarAretesGrid();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReemplazoArete_ErrorCargarAretes, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void CargarAretesGrid()
        {
            dgAretes.ItemsSource = Contexto.ListaAretes.OrderByDescending(are => are.PermiteEditar).ConvertirAObservable();
        }
        #endregion METODOS

       
    }
}
