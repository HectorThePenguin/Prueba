using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SuKarne.Controls.MessageBox;
using SuKarne.Controls.Enum;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ListaPreciosCentros.xaml
    /// </summary>
    public partial class ListaPreciosCentros
    {
        #region VARIABLES
        private ListaPreciosCentrosInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ListaPreciosCentrosInfo)DataContext;
            }
            set { DataContext = value; }
        }        
        #endregion

        #region CONSTRUCTOR

        public ListaPreciosCentros()
        {
            InitializeComponent();
            InicializaContexto();
            LlenarComboSociedad();
            cmbSociedad.SelectedIndex = 0;
            skAyudaZona.ObjetoNegocio = new ZonaPL();
            skAyudaZona.AyudaConDatos += (sender, args) =>
                                             {
                                                 gridDatos.ItemsSource = new List<ListaPreciosCentrosInfo>();
                                             };
            LLenarGrid();
        }

        #endregion CONSTRUCTOR

        #region METODOS
       
        private void InicializaContexto()
        {
            Contexto = new ListaPreciosCentrosInfo()
            {
                Zona = new ZonaInfo()
                {
                    Pais = new PaisInfo()
                    {
                        PaisID = -1
                    }
                }
            };
        }

        private void LlenarComboSociedad()
        {
            try
            {
                var sociedadPL = new SociedadPL();
                var lista = sociedadPL.ObtenerTodas();
                if (lista == null)
                {
                    lista = new List<SociedadInfo>();
                }
                var sociedad = new SociedadInfo
                {
                    SociedadID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione
                };

                lista.Insert(0, sociedad);
                cmbSociedad.ItemsSource = lista;
                cmbSociedad.SelectedValuePath = "SociedadID";
                cmbSociedad.DisplayMemberPath = "Descripcion";
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ListaPreciosCentros_MsjErrorSociedad,MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void InicializaPantalla()
        {
            InicializaContexto();
            cmbSociedad.SelectedIndex = 0;
            skAyudaZona.LimpiarCampos();
            cmbSociedad.Focus();
            gridDatos.ItemsSource = new List<ListaPreciosCentrosInfo>();
            LLenarGrid();
        }

        private void Buscar()
        {
            if(cmbSociedad.SelectedIndex > 0 && skAyudaZona.Clave != string.Empty )
            {
                Contexto.SociedadId = Convert.ToInt32(cmbSociedad.SelectedValue);
                Contexto.ZonaId = Convert.ToInt32(skAyudaZona.Clave);
            }
            LLenarGrid();
        }

        private void LLenarGrid()
        {
            try
            {
                var pl = new ListaPreciosCentrosPL();
                var result = pl.ObtenerListaPreciosCentros(Contexto);
                gridDatos.ItemsSource = result;

                if(result.Count <= 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ListaPreciosCentros_SinInformacion, MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ListaPreciosCentros_MsjErrorDatos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Guardar(List<ListaPreciosCentrosInfo> precios)
        {
            try
            {
                var pl = new ListaPreciosCentrosPL();
                var usuarioId = AuxConfiguracion.ObtenerUsuarioLogueado();
                if(pl.Guardar(precios, usuarioId))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ListaPreciosCentros_Guardar, MessageBoxButton.OK, MessageImage.Correct);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ListaPreciosCentros_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private List<ListaPreciosCentrosInfo> ObtenerInformacionGuardar()
        {
            var result = new List<ListaPreciosCentrosInfo>();
            try
            {
                foreach (var row in gridDatos.Items)
                {
                    var precios = row as ListaPreciosCentrosInfo;
                    if(precios != null)
                    {
                        result.Add(precios);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result = null;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ListaPreciosCentros_ErrorObtenerInformacion,MessageBoxButton.OK, MessageImage.Error);
            }

            return result;
        }

        private bool ValidarInformacion(List<ListaPreciosCentrosInfo> precios)
        {
            var result = precios.Count > 0;
            if (!result)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ListaPreciosCentros_ValidarInformacion, MessageBoxButton.OK, MessageImage.Warning);
            }
            return result;
        }

        #endregion METODOS

        #region EVENTOS

        private void CmbSociedad_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSociedad.SelectedIndex > 0)
            {
                var valor = Convert.ToInt32(cmbSociedad.SelectedValue);
                Contexto.Zona.Pais.PaisID = valor;
            }
            else
            {
                Contexto.Zona.Pais.PaisID = -1;
            }
            gridDatos.ItemsSource = new List<ListaPreciosCentrosInfo>();
            skAyudaZona.LimpiarCampos();
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.
                                                            ListaPreciosCentros_SalirSinConfirmar,
                                                        MessageBoxButton.YesNo, MessageImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                InicializaPantalla();
            }
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            var precios = ObtenerInformacionGuardar();
            if (ValidarInformacion(precios))
            {
                Guardar(precios);
            }
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        #endregion EVENTOS

    }
}
