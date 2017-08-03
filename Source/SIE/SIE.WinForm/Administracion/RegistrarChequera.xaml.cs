using System;
using System.Collections.Generic;
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
using System.Linq;
using System.ComponentModel;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RegistrarChequera.xaml
    /// </summary>
    public partial class RegistrarChequera : Window
    {

        #region PROPIEDADES
        private bool _esNuevo;
        public bool _seGuardo;
        public bool _regresar;
        public bool _confirmar = true;
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

        #region CONSTRUCTOR
        public RegistrarChequera()
        {
            try
            {
                InitializeComponent();
                InicializaContexto();
                _esNuevo = true;
                LLenarComboDivision(_esNuevo);
                LLenarComboBanco(_esNuevo);
                LlenarComboEstatus(_esNuevo);
                ObtenerDetalleChequera();
                if (!Validar(false))
                {
                    _regresar = true;
                    Close();
                    _confirmar = false;
                }
                dtpFecha.SelectedDate = DateTime.Now.Date;
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
                    ObtenerConsecutivo(Contexto.CentroAcopio.OrganizacionID);
                };
                cboDivision.SelectedIndex = 0;
                cboBanco.SelectedIndex = 0;
                cboEstatus.SelectedIndex = 0;
                cboDivision.Focus();
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.RegistrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
                _regresar = true;
                _confirmar = false;
                Close();
            }
        }

        public RegistrarChequera(ChequeraInfo info)
        {
            try
            {
                _esNuevo = false;
                InitializeComponent();
                BloquearControles(_esNuevo);
                Contexto = info;
                Contexto.ListBanco = new List<BancoInfo>();
                Contexto.ListDivision = new List<OrganizacionInfo>();
                Contexto.ListEtapas = new List<ChequeraEtapasInfo>();
                LLenarComboDivision(_esNuevo);
                LLenarComboBanco(_esNuevo);
                LlenarComboEstatus(true);
                ObtenerDetalleChequera();
                cboEstatus.SelectedIndex = info.ChequeraEtapas.EtapaId + 1;
                if (info.ChequeraEtapas.EtapaId == EstatusChequeraEnum.Cancelada.GetHashCode()-1)
                {
                    cboEstatus.IsEnabled = false;
                    btnGuardar.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.RegistrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
                _regresar = true;
                _confirmar = false;
                Close();
            }
        }
        #endregion

        #region METODOS
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

        private void Guardar()
        {
            try
            {
                if (Validar(true))
                {
                    var existeEstatus = false;
                    if (cboEstatus.SelectedValue.GetHashCode() != EstatusChequeraEnum.Cancelada.GetHashCode() && cboEstatus.SelectedValue.GetHashCode() != EstatusChequeraEnum.Cerrada.GetHashCode())
                    {
                        // Es menos uno ya que el catálogo de centros se definió diferente al de SIAP, allá el consecutivo inicia en 0 y no en 1
                        existeEstatus = ValidarSiExisteChequeraActiva(Contexto.CentroAcopio.OrganizacionID, true, cboEstatus.SelectedValue.GetHashCode() - 1);
                    }

                    if (!existeEstatus)
                    {
                        var sepuedeRealizarCambio = true;
                        if (!_esNuevo && Contexto.EstatusId + 1 == EstatusChequeraEnum.Cerrada.GetHashCode() && (cboEstatus.SelectedValue.GetHashCode() == EstatusChequeraEnum.Activa.GetHashCode() || cboEstatus.SelectedValue.GetHashCode() == EstatusChequeraEnum.Disponible.GetHashCode()))
                        {
                            if (ValidarChequesGirados(Contexto.CentroAcopio.OrganizacionID, Contexto.ChequeraId))
                            {
                                sepuedeRealizarCambio = false;
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.RegistrarChequera_ValidarChequesGirados, cboEstatus.Text), MessageBoxButton.OK, MessageImage.Warning);
                            }                            
                        }

                        if (sepuedeRealizarCambio)
                        {
                            var pl = new ChequeraPL();
                            Contexto.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                            Contexto.ChequeraEtapas.EtapaId = cboEstatus.SelectedValue.GetHashCode()-1;
                            var result = pl.Guardar(Contexto);
                            if (result >= 0)
                            {
                                txtChequera.Text = result.ToString();
                                txtNumeroChequera.Text = result.ToString();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.RegistrarChequera_Exito, result), MessageBoxButton.OK, MessageImage.Correct);
                                _seGuardo = true;
                                _confirmar = false;
                                Close();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RegistrarChequera_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                            }
                        }                        
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format(Properties.Resources.RegistrarChequera_ExisteChequeraActiva, cboEstatus.Text, Contexto.CentroAcopio.OrganizacionID), MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.RegistrarChequera_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BloquearControles(bool enable)
        {
            cboDivision.IsEnabled = enable;
            txtChequera.IsEnabled = enable;
            txtChequeInicial.IsEnabled = enable;
            dtpFecha.IsEnabled = enable;
            skAyudaCentroAcopio.IsEnabled = enable;
            txtChequeFinal.IsEnabled = enable;
            txtNumeroChequera.IsEnabled = enable;
            cboBanco.IsEnabled = enable;
        }

        private void ObtenerDetalleChequera()
        {
            var pl = new ChequeraPL();
            if (_esNuevo)
            {
                txtDisponible.Text = string.Format("0");
                txtGirados.Text = string.Format("0");
                txtCancelados.Text = string.Format("0");
            }
            else
            {
                var detalle = pl.ObtenerDetalleChequera(Contexto.ChequeraId,Contexto.CentroAcopio.OrganizacionID);
                txtDisponible.Text = detalle.ChequesDisponibles.ToString();
                txtGirados.Text = detalle.ChequesGirados.ToString();
                txtCancelados.Text = detalle.ChequesCancelados.ToString();
                dtpFecha.SelectedDate = detalle.FechaCreacion.Date;
                txtNumeroChequera.Text = detalle.NumeroChequera;
                txtChequeInicial.Text = detalle.ChequeInicial.ToString();
                txtChequeFinal.Text = detalle.ChequeFinal.ToString();
                txtChequera.Text = detalle.ChequeraId.ToString();
                Contexto.NumeroChequera = detalle.ChequeraId.ToString();
            }
        }

        private void LLenarComboDivision(bool nuevo)
        {
            if(nuevo)
            {
                var organizacionPL = new OrganizacionPL();
                var listDivision = organizacionPL.ObtenerTipoGanaderas();

                if (listDivision != null && listDivision.Any())
                {
                    AgregarElementoInicialDivision(listDivision);
                    Contexto.ListDivision = listDivision;
                }
            }
            else
            {
                var list = new OrganizacionInfo { OrganizacionID = Contexto.Division.OrganizacionID, Descripcion = Contexto.Division.Descripcion };
                Contexto.ListDivision.Insert(0, list);
                cboDivision.SelectedIndex = 0;
            }
        }

        private void LLenarComboBanco(bool nuevo)
        {
            if (nuevo)
            {
                var bancoPL = new BancoPL();
                var listBanco = bancoPL.ObtenerTodos();

                if (listBanco != null && listBanco.Any())
                {
                    AgregarElementoInicialBanco(listBanco);
                    Contexto.ListBanco = listBanco;
                }
            }
            else
            {
                var listBanco = new BancoInfo { BancoID = Contexto.Banco.BancoID, Descripcion = Contexto.Banco.Descripcion };
                Contexto.ListBanco.Insert(0, listBanco);
                cboBanco.SelectedIndex = 0;
            }
        }

        private void LlenarComboEstatus(bool nuevo)
        {
            if (nuevo)
            {

                var pl = new ChequeraEtapasPL();
                var lista = pl.ObtenerTodos();

                if (lista != null && lista.Any())
                {
                    AgregarElementoInicialEtapas(lista);
                    Contexto.ListEtapas = lista;
                }
            }
            else
            {
                    var listDivision = new ChequeraEtapasInfo{ EtapaId = Contexto.ChequeraEtapas.EtapaId, Descripcion = Contexto.ChequeraEtapas.Descripcion };
                    Contexto.ListEtapas.Insert(0, listDivision);
                cboEstatus.SelectedIndex = 0;
            }
        }

        private void AgregarElementoInicialDivision(IList<OrganizacionInfo> listOrganizacion)
        {
            var listDivisionInicial = new OrganizacionInfo { OrganizacionID = 0, Descripcion = Properties.Resources.cbo_Seleccione, Division = Properties.Resources.cbo_Seleccione };
            if (!listOrganizacion.Contains(listDivisionInicial))
            {
                listOrganizacion.Insert(0, listDivisionInicial);
            }
        }

        private void AgregarElementoInicialBanco(IList<BancoInfo> listBanco)
        {
            var listBancoInicial = new BancoInfo { BancoID = 0, Descripcion = Properties.Resources.cbo_Seleccione };
            if (!listBanco.Contains(listBancoInicial))
            {
                listBanco.Insert(0, listBancoInicial);
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

        private bool Validar(bool guardar)
        {
            bool result = false;
            try
            {
                if (guardar)
                {
                    if(_esNuevo)
                    {

                        if (cboDivision.SelectedIndex > 0)
                        {
                            if (Convert.ToInt32(skAyudaCentroAcopio.Clave) > 0)
                            {
                                if (string.IsNullOrEmpty(txtChequeInicial.Text.Trim()))
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RegistrarChequera_ValidacionChequeInicial, MessageBoxButton.OK, MessageImage.Warning);
                                    txtChequeInicial.Focus();
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(txtChequeFinal.Text.Trim()))
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.RegistrarChequera_ValidacionChequeFinal, MessageBoxButton.OK, MessageImage.Warning);
                                        txtChequeFinal.Focus();
                                    }
                                    else
                                    {
                                        if ((Convert.ToInt32(txtChequeFinal.Text.Trim()) < Convert.ToInt32(txtChequeInicial.Text.Trim())))
                                        {
                                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RegistrarChequera_RangoChequesInvalido, MessageBoxButton.OK, MessageImage.Warning);
                                            txtChequeFinal.Focus();
                                        }
                                        else
                                        {
                                            if ((Convert.ToInt32(txtChequeFinal.Text.Trim()) - Convert.ToInt32(txtChequeInicial.Text.Trim())) >= 500)
                                            {
                                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.RegistrarChequera_DiferenciaEntreCheques, MessageBoxButton.OK, MessageImage.Warning);
                                                txtChequeFinal.Focus();
                                            }
                                            else
                                            {
                                                if (cboBanco.SelectedIndex > 0)
                                                {
                                                    if (cboEstatus.SelectedIndex > 0)
                                                    {
                                                        result = true;
                                                    }
                                                    else
                                                    {
                                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.RegistrarChequera_SelEstatus, MessageBoxButton.OK, MessageImage.Warning);
                                                        cboEstatus.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.RegistrarChequera_SelBanco, MessageBoxButton.OK, MessageImage.Warning);
                                                    cboBanco.Focus();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RegistrarChequera_SelCA, MessageBoxButton.OK, MessageImage.Warning);
                                skAyudaCentroAcopio.AsignarFoco();
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarChequera_SelDivision, MessageBoxButton.OK, MessageImage.Warning);
                            cboDivision.Focus();
                        }
                    }
                    else
                    {
                        result = true; 
                    }
                }
                else
                {
                    if (Contexto.ListDivision.Any())
                    {
                        if (Contexto.ListBanco.Any())
                        {
                            if (Contexto.ListEtapas.Any())
                            {
                                result = true;
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RegistrarChequera_SinEstatus, MessageBoxButton.OK, MessageImage.Warning);
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarChequera_SinBanco, MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarChequera_SinDivisiones, MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
            }
        
            return result;
        }

        private void ObtenerConsecutivo(int OrganizacionId)
        {
            var pl = new ChequeraPL();
            var list = pl.ObtenerConsecutivo(OrganizacionId);
            txtChequera.Text = list.ToString();
            txtNumeroChequera.Text = list.ToString();
            Contexto.NumeroChequera = list.ToString();
        }

        private bool ValidarSiExisteChequeraActiva(int organizacionId, bool validar, int estatusId)
        {
            var result = false;
            var pl = new ChequeraPL();
            if(validar)
            {
                result = pl.ValidarSiExisteChequeraActiva(organizacionId, estatusId);
            }
            return result;
        }

        private bool ValidarChequesGirados(int organizacionId, int chequeraId)
        {
            var pl = new ChequeraPL();
            return pl.ValidarChequesGirados(organizacionId, chequeraId);
        }

        private void Salir()
        {
            if (btnGuardar.IsEnabled)
            {
                MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                          MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _confirmar = false;
                    Close();
                }
            }
            else
            {
                _confirmar = false;
                Close();
            }
        }

        #endregion

        #region EVENTOS
        private void btnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Salir();
        }

        private void btnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void CboDivision_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
                _confirmar = false;
                Close();
            }
        }

        private void txtChequeInicial_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int valor = 0;
                bool result = Int32.TryParse(txtChequeInicial.Text, out valor);
                if (!result)
                {
                    txtChequeInicial.Text = string.Empty;
                }
                else
                {
                    Contexto.ChequeInicial = int.Parse(txtChequeInicial.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
                txtChequeFinal.Text = string.Empty;
                txtChequeFinal.Focus();
            }
        }

        private void txtChequeFinal_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int valor = 0;
                bool result = Int32.TryParse(txtChequeFinal.Text, out valor);
                if (!result)
                {
                    txtChequeFinal.Text = string.Empty;
                }
                else
                {
                    Contexto.ChequeFinal = int.Parse(txtChequeFinal.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarChequera_ErrorGeneral, MessageBoxButton.OK, MessageImage.Error);
                txtChequeFinal.Text = string.Empty;
                txtChequeFinal.Focus();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
       {
           if (_confirmar)
           {
               if (btnGuardar.IsEnabled)
               {
                   MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                             MessageImage.Question);
                   if (result == MessageBoxResult.No)
                   {
                       e.Cancel = true;
                   }
               }
           }
       }
        #endregion
    }
}
