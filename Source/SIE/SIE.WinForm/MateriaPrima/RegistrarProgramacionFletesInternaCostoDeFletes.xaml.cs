using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para RegistrarProgramacionFletesInternaCostoDeFletes.xaml
    /// </summary>
    public partial class RegistrarProgramacionFletesInternaCostoDeFletes
    {


        #region Atributos
        public List<FleteInternoCostoInfo> ListadoFleteInternoCostoPrincipal = new List<FleteInternoCostoInfo>();
        public List<FleteInternoCostoInfo> ListadoFleteInternoCostoFiltrado = new List<FleteInternoCostoInfo>();
        private SKAyuda<CostoInfo> skAyudaCostos;
        private int usuarioID;
        public string cadena;

        #endregion

        #region Propiedades
        public List<FleteInternoCostoInfo> ListadoFleteInternoCostoReturn
        {
            get { return ListadoFleteInternoCostoPrincipal; }
            set { ListadoFleteInternoCostoPrincipal = value; }
        }

        public string Cadena
        {
            get { return cadena; }
            set { cadena = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RegistrarProgramacionFletesInternaCostoDeFletes(List<FleteInternoCostoInfo> listaFleteInternoInfo)
        {
            InitializeComponent();
            ListadoFleteInternoCostoPrincipal.Clear();
            ListadoFleteInternoCostoPrincipal.AddRange(listaFleteInternoInfo);
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            AgregarAyudaCosto();
            CargarGridCostos();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Agrega un costo al grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool actualizar = (string)BtnAgregar.Content == Properties.Resources.DiferenciasDeInventario_LblActualizar;
                //Validar pesos antes de guardar
                var resultadoValidacion = ValidarAgregar(actualizar);
                if (resultadoValidacion.Resultado)
                {
                    if ((string)BtnAgregar.Content == Properties.Resources.DiferenciasDeInventario_LblActualizar)
                    {
                        foreach (var fleteInternoCostoP in ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoP => fleteInternoCostoP.Costo.CostoID ==
                                                                                                                                             Convert.ToInt32(skAyudaCostos.Clave)))
                        {
                            fleteInternoCostoP.Tarifa = Convert.ToDecimal(TxtTarifa.Value);
                            if (fleteInternoCostoP.Guardado)
                            {
                                fleteInternoCostoP.Modificado = true;
                                fleteInternoCostoP.UsuarioModificacionId = usuarioID;
                            }
                        }
                        ListadoFleteInternoCostoFiltrado.Clear();
                        ListadoFleteInternoCostoFiltrado = ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoInfoP => !fleteInternoCostoInfoP.Eliminado).ToList();
                        GridCostoFletes.ItemsSource = null;
                        GridCostoFletes.ItemsSource = ListadoFleteInternoCostoFiltrado;
                        LimpiarCostos();
                        BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_BtnAgregar;
                        skAyudaCostos.IsEnabled = true;
                        skAyudaCostos.AsignarFoco();
                    }
                    else
                    {
                        AgregarCosto();
                    }
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                        ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                        : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInternaCostoFletes_MensajeErrorAgregarCosto, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Cerrar pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSalir_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Asignar foco a ayuda de costos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarProgramacionFletesInternaCostoDeFletes_OnLoaded(object sender, RoutedEventArgs e)
        {
            skAyudaCostos.AsignarFoco();
            lblTipoTarifa.Content = Cadena;
        }

        /// <summary>
        /// Elimina un costo del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEliminar = (Button)e.Source;
            try
            {
                var costoInfo = (FleteInternoCostoInfo)Extensor.ClonarInfo(botonEliminar.CommandParameter);
                if (costoInfo == null) return;

                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionEliminarCosto1 +
                   costoInfo.Costo.Descripcion + Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionEliminarCosto2,
                   MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach (var fleteInternoCostoP in ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoP => fleteInternoCostoP.Costo.CostoID ==
                                                                                                                                         costoInfo.Costo.CostoID))
                    {
                        fleteInternoCostoP.Eliminado = true;
                        fleteInternoCostoP.UsuarioModificacionId = usuarioID;
                        fleteInternoCostoP.Activo = EstatusEnum.Inactivo;
                    }

                    //Filtrar lista para quitar eliminados que no han sido guardados
                    var listaTemp = ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoInfoP => (fleteInternoCostoInfoP.Guardado) || (!fleteInternoCostoInfoP.Guardado && !fleteInternoCostoInfoP.Eliminado)).ToList();
                    ListadoFleteInternoCostoPrincipal.Clear();
                    ListadoFleteInternoCostoPrincipal.AddRange(listaTemp);
                    ListadoFleteInternoCostoFiltrado.Clear();
                    ListadoFleteInternoCostoFiltrado = ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoInfoP => !fleteInternoCostoInfoP.Eliminado).ToList();
                    GridCostoFletes.ItemsSource = null;
                    GridCostoFletes.ItemsSource = ListadoFleteInternoCostoFiltrado;
                    LimpiarCostos();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorEliminarCosto, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Avanza los focos con click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarProgramacionFletesInternaCostoDeFletes_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Editar un costo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var costoInfo = (FleteInternoCostoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                BtnAgregar.Content = Properties.Resources.OtrosCostos_MensajeCosto;

                if (costoInfo == null) return;
                skAyudaCostos.Clave = costoInfo.Costo.CostoID.ToString(CultureInfo.InvariantCulture);
                skAyudaCostos.Descripcion = costoInfo.Costo.Descripcion;
                skAyudaCostos.IsEnabled = false;
                TxtTarifa.Value = costoInfo.Tarifa;
                TxtTarifa.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Agrega ayuda costos
        /// </summary>
        private void AgregarAyudaCosto()
        {
            var tipoCosto = new TipoCostoInfo()
            {
                TipoCostoID = (int)TipoCostoEnum.Flete,
                Activo = EstatusEnum.Activo
            };
            var listaTipoCosto = new List<TipoCostoInfo> { tipoCosto };
            skAyudaCostos = new SKAyuda<CostoInfo>(200, false, new CostoInfo { ListaTipoCostos = listaTipoCosto }
                                                   , "PropiedadClaveRegistrarProgramacionInternaFletes"
                                                   , "PropiedadDescripcionRegistrarProgramacionInternaFletes", true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaCostos_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaCostos_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaCostos_BusquedaTitulo,
            };
            skAyudaCostos.ObtenerDatos += ObtenerDatosCostos;
            SplAyudaCostos.Children.Clear();
            SplAyudaCostos.Children.Add(skAyudaCostos);
        }

        /// <summary>
        /// Verifica que el costo sea de flete
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCostos(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaCostos.Info == null)
                {
                    return;
                }
                if (skAyudaCostos.Info.TipoCosto.TipoCostoID !=
                    TipoCostoEnum.Flete.GetHashCode())
                {
                    skAyudaCostos.Info = new CostoInfo
                    {
                        ListaTipoCostos = new List<TipoCostoInfo>
                        {
                            new TipoCostoInfo
                            {
                                TipoCostoID = TipoCostoEnum.Flete.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    skAyudaCostos.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.OtrosCostos_MensajeCostoInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    skAyudaCostos.Info = new CostoInfo
                    {
                        ListaTipoCostos = new List<TipoCostoInfo>
                        {
                            new TipoCostoInfo
                            {
                                TipoCostoID = TipoCostoEnum.Flete.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    TxtTarifa.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Carga grid costos
        /// </summary>
        private void CargarGridCostos()
        {
            ListadoFleteInternoCostoFiltrado =
                ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoInfoP => !fleteInternoCostoInfoP.Eliminado).ToList();
            GridCostoFletes.ItemsSource = ListadoFleteInternoCostoFiltrado;
        }

        /// <summary>
        /// Agregar costo al grid
        /// </summary>
        private void AgregarCosto()
        {
            var fleteInternoCostoInfo = new FleteInternoCostoInfo()
            {
                Costo = new CostoInfo() { CostoID = Convert.ToInt32(skAyudaCostos.Clave), Descripcion = skAyudaCostos.Descripcion },
                Tarifa = Convert.ToDecimal(TxtTarifa.Value),
                Activo = EstatusEnum.Activo,
                UsuarioCreacionId = usuarioID
            };
            ListadoFleteInternoCostoPrincipal.Add(fleteInternoCostoInfo);
            ListadoFleteInternoCostoFiltrado.Clear();
            ListadoFleteInternoCostoFiltrado = ListadoFleteInternoCostoPrincipal.Where(fleteInternoCostoInfoP => !fleteInternoCostoInfoP.Eliminado).ToList();
            GridCostoFletes.ItemsSource = null;
            GridCostoFletes.ItemsSource = ListadoFleteInternoCostoFiltrado;
            LimpiarCostos();
            skAyudaCostos.AsignarFoco();
        }

        /// <summary>
        /// Limpia ayuda y txt
        /// </summary>
        private void LimpiarCostos()
        {
            skAyudaCostos.LimpiarCampos();
            TxtTarifa.Text = string.Empty;
            skAyudaCostos.IsEnabled = true;
            skAyudaCostos.AsignarFoco();
            BtnAgregar.Content = Properties.Resources.RegistrarProgramacionFletesInterna_BtnAgregar;
        }

        /// <summary>
        /// Valida si se puede agregar el costo
        /// </summary>
        private ResultadoValidacion ValidarAgregar(bool actualizar)
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaCostos.Clave.Trim()) && String.IsNullOrEmpty(skAyudaCostos.Descripcion.Trim()))
            {
                skAyudaCostos.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionOtrosCostosIngresarCosto;
                resultado.Resultado = false;
                return resultado;
            }

            if (TxtTarifa.Text == string.Empty || Convert.ToDecimal(TxtTarifa.Value) == 0)
            {
                TxtTarifa.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionOtrosCostosIngresarTarifa;
                resultado.Resultado = false;
                return resultado;
            }

            if (!actualizar)
            {
                if (ListadoFleteInternoCostoFiltrado != null)
                {
                    if ((from fleteInternoDetalle in ListadoFleteInternoCostoFiltrado
                         where fleteInternoDetalle.Costo.CostoID == Convert.ToInt32(skAyudaCostos.Clave)
                         select fleteInternoDetalle).Any())
                    {
                        skAyudaCostos.AsignarFoco();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionOtrosCostosCostoAgregado;
                        return resultado;
                    }
                }
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Avanza al siguiente control en la pantalla
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }
        #endregion
    }
}
