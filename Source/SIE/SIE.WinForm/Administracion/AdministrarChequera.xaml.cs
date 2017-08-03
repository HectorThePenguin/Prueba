using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para AdministrarChequera.xaml
    /// </summary>
    public partial class AdministrarChequera
    {

        #region  PROPIEDADES
        private ChequeraInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ChequeraInfo)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion

        #region  CONSTRUCTOR
        public AdministrarChequera()
        {
            InitializeComponent();
            InicializaContexto();
            LLenarComboDivision();
            LlenarComboEstatus();
            skAyudaCentroAcopio.ObjetoNegocio = new OrganizacionPL();
            skAyudaCentroAcopio.AyudaConDatos += (sender, args) =>
            {
                var division = (OrganizacionInfo)cboDivision.SelectedItem;
                if (cboDivision.SelectedIndex > 0)
                {
                    Contexto.CentroAcopio.Division = division.Division;
                }
                else
                {
                    Contexto.CentroAcopio.Division = ".";    
                }
                
            };
            ObtenerChequeras();
            cboDivision.SelectedIndex = 0;
            cmbActivo.SelectedIndex = 0;
        }
        #endregion

        #region  METODOS
        private void InicializaContexto()
        {
            Contexto = new ChequeraInfo()
            {
                CentroAcopio = new OrganizacionInfo()
                                   {

                                       TipoOrganizacion = new TipoOrganizacionInfo
                                       {
                                           TipoOrganizacionID = TipoOrganizacion.Centro.GetHashCode()
                                       }
                                   }
            };
            Contexto.CentroAcopio.Division = ".";

        }

        private void AgregarElementoInicialDivision(IList<OrganizacionInfo> listOrganizacion)
        {
            var listDivisionInicial = new OrganizacionInfo { OrganizacionID = 0, Descripcion = Properties.Resources.cbo_Seleccione, Division = Properties.Resources.cbo_Seleccione };
            if (!listOrganizacion.Contains(listDivisionInicial))
            {
                listOrganizacion.Insert(0, listDivisionInicial);
            }
        }

        private void AgregarElementoInicialEtapas(IList<ChequeraEtapasInfo> listEtapa)
        {
            var listInicial = new ChequeraEtapasInfo { EtapaId = 0, Descripcion = Properties.Resources.cbo_Seleccione };
            if (!listEtapa.Contains(listInicial))
            {
                listEtapa.Insert(0, listInicial);
            }
        }

        private void LLenarComboDivision()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                var listDivision = organizacionPL.ObtenerTipoGanaderas();

                if (listDivision != null && listDivision.Any())
                {
                    AgregarElementoInicialDivision(listDivision);
                    Contexto.ListDivision = listDivision;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        private void LlenarComboEstatus()
        {
            try
            {
                var pl = new ChequeraEtapasPL();
                var lista = pl.ObtenerTodos();

                if (lista != null && lista.Any())
                {
                    AgregarElementoInicialEtapas(lista);
                    Contexto.ListEtapas = lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Limpiar()
        {
            gridDatos.ItemsSource = new List<ChequeraInfo>();
            skAyudaCentroAcopio.LimpiarCampos();
            txtChequera.Text = string.Empty;
            ObtenerChequeras();
            cmbActivo.SelectedIndex = 0;
            cboDivision.SelectedIndex = 0;
            cboDivision.Focus();
        }

        private void ObtenerChequeras()
        {
            try
            {
                var chequeraPL = new ChequeraPL();
                var lista = chequeraPL.ObtenerTodos();
                gridDatos.ItemsSource = lista;
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void ObtenerChequerasPorFiltro()
        {
            try
            {
                var chequeraPL = new ChequeraPL();
                if (cmbActivo.SelectedIndex <= 0)
                {
                    Contexto.EstatusIdString = string.Empty;
                }
                var lista = chequeraPL.ObtenerPorFiltro(Contexto);
                gridDatos.ItemsSource = lista;
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void ValidarParametrosDeBusqueda()
        {
            try
            {
                if (cmbActivo.SelectedIndex >= 0)
                {
                    var estatus = Convert.ToInt32(cmbActivo.SelectedValue) - 1;
                    Contexto.EstatusIdString = estatus.ToString();
                }
                else
                {
                    Contexto.EstatusIdString = string.Empty;
                }

                if (cboDivision.SelectedIndex > 0)
                {
                    var division = (OrganizacionInfo)cboDivision.SelectedItem;
                    Contexto.Division.Division = division.Division;
                }
                else
                {
                    Contexto.Division.Division = "";
                }
                Contexto.NumeroChequera = txtChequera.Text.Trim();

                if (string.IsNullOrEmpty(skAyudaCentroAcopio.Clave.Trim()) || skAyudaCentroAcopio.Clave.Trim() == "0")
                {
                    Contexto.CentroAcopioId = 0;
                }
                else
                {
                    Contexto.CentroAcopioId = Convert.ToInt32(skAyudaCentroAcopio.Clave.Trim());
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

        #region EVENTOS
        private void btnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var pagoEdicion = new RegistrarChequera();
                if (!pagoEdicion._regresar)
                {
                    pagoEdicion.Left = (ActualWidth - pagoEdicion.Width) / 2;
                    pagoEdicion.Top = ((ActualHeight - pagoEdicion.Height) / 2);
                    pagoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                    pagoEdicion.ShowDialog();
                    if (pagoEdicion._seGuardo)
                    {
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            ValidarParametrosDeBusqueda();
            ObtenerChequerasPorFiltro();
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var infoSelecionado = (ChequeraInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                infoSelecionado.CentroAcopio = new OrganizacionInfo
                                                   {
                                                       OrganizacionID = infoSelecionado.CentroAcopioId,
                                                       Descripcion = infoSelecionado.CentroAcopioDescripcion
                                                   };
                infoSelecionado.Division = new OrganizacionInfo
                                               {
                                                   OrganizacionID = infoSelecionado.DivisionId,
                                                   Descripcion = infoSelecionado.DivisionDescripcion
                                               };

                infoSelecionado.ChequeraEtapas = new ChequeraEtapasInfo
                {
                    EtapaId = infoSelecionado.EstatusId,
                    Descripcion = infoSelecionado.EstatusDescripcion
                };


                infoSelecionado.Banco = new BancoInfo
                {
                    BancoID = infoSelecionado.BancoId,
                    Descripcion = infoSelecionado.BancoDescripcion
                };

                var pagoEdicion = new RegistrarChequera(infoSelecionado);
                pagoEdicion.Left = (ActualWidth - pagoEdicion.Width) / 2;
                pagoEdicion.Top = ((ActualHeight - pagoEdicion.Height) / 2);
                pagoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                if (!pagoEdicion._regresar)
                {
                    pagoEdicion.ShowDialog();
                    if (pagoEdicion._seGuardo)
                    {
                        ObtenerChequerasPorFiltro();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void CboDivision_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            skAyudaCentroAcopio.LimpiarCampos();
            if (cboDivision.SelectedIndex > 0)
            {
                var division = (OrganizacionInfo)cboDivision.SelectedItem;
                Contexto.CentroAcopio.Division = division.Division;
            }
            else
            {
                Contexto.CentroAcopio.Division = ".";
            }
        }
        #endregion
    }
}
