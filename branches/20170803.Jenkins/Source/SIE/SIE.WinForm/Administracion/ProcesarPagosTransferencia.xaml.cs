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

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ProcesarPagosTransferencia.xaml
    /// </summary>
    public partial class ProcesarPagosTransferencia
    {
        #region PROPIEDADES
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TraspasoMpPaMedInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TraspasoMpPaMedInfo)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion PROPIEDADES

        #region CONSTRUCTOR
        public ProcesarPagosTransferencia()
        {
            InitializeComponent();
            InicializaContexto();
            ucPaginacion.Inicio = 1;
            skAyudaOrganizacionOrigen.ObjetoNegocio = new OrganizacionPL();
            AsignarEventosAyudas();

            ucPaginacion.DatosDelegado += ObtenerListaPagos;
            ucPaginacion.AsignarValoresIniciales();
            ucPaginacion.Contexto = Contexto;

            Buscar();
        }
        #endregion CONSTRUCTOR
        
        #region MÉTODOS
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
                new TraspasoMpPaMedInfo
                {
                    OrganizacionOrigen = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo
                        {
                            TipoOrganizacionID = TipoOrganizacion.Centro.GetHashCode()
                        }
                    }
                };
        }

        /// <summary>
        /// Asigna los eventos a los controles de ayuda
        /// </summary>
        private void AsignarEventosAyudas()
        {
            skAyudaOrganizacionOrigen.AyudaConDatos += (o, args) =>
            {
                Contexto.OrganizacionOrigen.TipoOrganizacion = new TipoOrganizacionInfo
                {
                    TipoOrganizacionID = TipoOrganizacion.Centro.GetHashCode()
                };
            };
        }

        private void Limpiar()
        {
            InicializaContexto();
            txtDescripcion.Text = string.Empty;
            cmbActivo.SelectedIndex = -1;
            gridDatos.ItemsSource = new List<PagoTransferenciaInfo>();
            skAyudaOrganizacionOrigen.AsignarFoco();
            Buscar();
        }

        public void Buscar()
        {
            ObtenerListaPagos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaPagos(int inicio, int limite)
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

                var folio = 0;
                var centro = 0;
                if (txtDescripcion.Text.Trim() != string.Empty)
                {
                    folio = Convert.ToInt32(txtDescripcion.Text.Trim());
                }

                if (skAyudaOrganizacionOrigen.Clave.Trim() != string.Empty && Convert.ToInt32(skAyudaOrganizacionOrigen.Clave) > 0)
                {
                    centro = Convert.ToInt32(skAyudaOrganizacionOrigen.Clave);
                }

                var pagoPL = new PagoTransferenciaPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                var resultadoInfo = pagoPL.ObtenerPorPagina(pagina, new PagoTransferenciaInfo(), centro, folio);
                if (resultadoInfo != null && resultadoInfo.Lista != null && resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<PagoTransferenciaInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProcesarPagosTransferencia_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProcesarPagosTransferencia_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion MÉTODOS

        #region EVENTOS
        /// <summary>
        /// Evento que se ejecuta al dar clic al boton limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        
        private void btnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var infoSelecionado = (PagoTransferenciaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (infoSelecionado != null)
                {
                    var pagoEdicion = new RegistrarPagosTransferencia(infoSelecionado);
                    pagoEdicion.Left = (ActualWidth - pagoEdicion.Width) / 2;
                    pagoEdicion.Top = ((ActualHeight - pagoEdicion.Height) / 2);
                    pagoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                    pagoEdicion.ShowDialog();
                    if(pagoEdicion.folio != 0)
                    {
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProcesarPagosTransferencia_Error, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtDescripcion_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "D1":
                case "D2":
                case "D3":
                case "D4":
                case "D5":
                case "D6":
                case "D7":
                case "D8":
                case "D9":
                case "D0":
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            int valor = 0;
            bool result = Int32.TryParse(txtDescripcion.Text, out valor);
            if (!result)
            {
                txtDescripcion.Text = string.Empty;
            }
        }

       #endregion EVENTOS
        
    }
}
