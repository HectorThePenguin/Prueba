using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para SolicitudPremezclas.xaml
    /// </summary>
    public partial class SolicitudPremezclas
    {
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        private List<PremezclaInfo> listaPremezcla; 
        private List<SolicitudPremezclaInfo> listaSolicitud = new List<SolicitudPremezclaInfo>();
        private int renglonModificar = 0;
        private int usuarioId = 0;
        private int organizacionAnterior = 0;
        public SolicitudPremezclas()
        {
            InitializeComponent();
            CargarAyudas();
            skAyudaOrganizacion.AsignarFoco();
            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
        }

        /// <summary>
        /// Funcion que carga las ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de folio
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            try
            {
                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(250,
                    false,
                    new OrganizacionInfo() 
                    {
                        OrganizacionID = 0,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadClaveSolicitudPremezclaOrganizacion",
                    "PropiedadDescripcionSolicitudPremezcla",
                    true,
                    50,
                    true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.SolicitudPremezcla_OrganizacionNoExiste,
                    MensajeBusquedaCerrar = Properties.Resources.SolicitudPremezcla_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.SolicitudPremezcla_Busqueda,
                    MensajeAgregar = Properties.Resources.SolicitudPremezclas_Seleccionar,
                    TituloEtiqueta = Properties.Resources.SolicitudPremezclas_LeyendaBusqueda,
                    TituloPantalla = Properties.Resources.SolicitudPremezclas_TituloBusquedaOrganizacion,
                    MetodoPorDescripcion = "ObtenerPorPaginaPremezcla"

                };

                skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
                skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarTodoOrganizacion;

                skAyudaOrganizacion.AsignaTabIndex(0);
                splAyudaOrganizacion.Children.Clear();
                splAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
                skAyudaOrganizacion.AsignarFoco();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Limpia los campos cuando no existe un valor valido en la ayuda
        /// </summary>
        private void LimpiarTodoOrganizacion()
        {
            try
            {
                skAyudaOrganizacion.LimpiarCampos();
                skAyudaOrganizacion.Info = new OrganizacionInfo()
                {
                    OrganizacionID = 0,
                    Activo = EstatusEnum.Activo
                };
                txtFecha.Text = "";
                dtpFechaInicio.SelectedDate = null;
                dtpFechaFinal.SelectedDate = null;
                dtpFechaLlegada.SelectedDate = null;
                listaPremezcla = new List<PremezclaInfo>();
                dgPremezclas.ItemsSource = new List<PremezclaInfo>();
                listaSolicitud = new List<SolicitudPremezclaInfo>();
                dgSolicitudProducto.ItemsSource = new List<SolicitudPremezclaInfo>();
                btnAgregar.Content = Properties.Resources.SolicitudMateriaPrima_btnAgregar;
                organizacionAnterior = 0;
                skAyudaOrganizacion.IsEnabled = true;
                dtpFechaInicio.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Eventos para el manejo del renglon de detalle
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FrameworkElement GetTemplateChildByName(DependencyObject parent, string name)
        {
            int childnum = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childnum; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement &&

                        ((FrameworkElement)child).Name == name)
                {
                    return child as FrameworkElement;
                }
                else
                {
                    var s = GetTemplateChildByName(child, name);
                    if (s != null)
                        return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Evento para el manejo del renglon de detalle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridResultado_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            var row = e.Row;
            FrameworkElement tb = GetTemplateChildByName(row, "RowHeaderToggleButton");
            if (tb != null)
            {
                if (row.DetailsVisibility == Visibility.Visible)
                {
                    (tb as ToggleButton).IsChecked = true;
                }
                else
                {
                    (tb as ToggleButton).IsChecked = false;
                }
            }


        }

        /// <summary>
        /// Obtiene la organizacion seleccionada en el filtro
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosOrganizacion(string filtro)
        {
            try
            {
                var premezclaPl = new PremezclaPL();
                listaPremezcla = premezclaPl.ObtenerPorOrganizacion(skAyudaOrganizacion.Info);

                if (listaPremezcla != null)
                {
                    if (listaPremezcla.Count > 0)
                    {
                        if (organizacionAnterior != int.Parse(filtro))
                        {
                            foreach (var premezclaInfo in listaPremezcla)
                            {
                                premezclaInfo.Habilitado = false;
                            }
                            dgPremezclas.ItemsSource = listaPremezcla;
                            listaSolicitud = new List<SolicitudPremezclaInfo>();
                            dgSolicitudProducto.ItemsSource = new List<SolicitudPremezclaInfo>();

                            FechaPL fechaPl = new FechaPL();
                            var fecha = fechaPl.ObtenerFechaActual();
                            txtFecha.Text = fecha.FechaActual.ToShortDateString().ToString(CultureInfo.InvariantCulture);
                            skAyudaOrganizacion.IsEnabled = false;
                            LimpiarCampos();
                            organizacionAnterior = int.Parse(filtro);
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SolicitudPremezclas_OrganizacionSinConfigurar,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    skAyudaOrganizacion.AsignarFoco();
                    LimpiarTodoOrganizacion();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarCampos()
        {
            renglonModificar = 0;
            dtpFechaLlegada.SelectedDate = null;
            if (listaPremezcla != null)
            {
                foreach (var premezclaInfo in listaPremezcla)
                {
                    premezclaInfo.Cantidad = 0;
                    premezclaInfo.Habilitado = false;
                }
                dgPremezclas.ItemsSource = new List<PremezclaInfo>();
                dgPremezclas.ItemsSource = listaPremezcla;
            }
            btnAgregar.Content = Properties.Resources.SolicitudMateriaPrima_btnAgregar;
        }

        private void DtpFechaInicio_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechaInicio.SelectedDate != null)
            {
                if (skAyudaOrganizacion.Clave != "")
                {
                    DateTime fechaConvertida;
                    if (DateTime.TryParse(dtpFechaInicio.SelectedDate.ToString(), out fechaConvertida))
                    {
                        fechaConvertida = new DateTime(fechaConvertida.Year, fechaConvertida.Month, 1);
                        DateTime fechaConvertidaMayor = fechaConvertida.AddMonths(1);
                        fechaConvertidaMayor = fechaConvertidaMayor.AddDays(-1);
                        dtpFechaInicio.SelectedDate = fechaConvertida;
                        dtpFechaFinal.SelectedDate = fechaConvertidaMayor;
                        dtpFechaLlegada.SelectedDate = null;
                        dtpFechaInicio.IsEnabled = false;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_Seleccionar,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    dtpFechaInicio.SelectedDate = null;
                    skAyudaOrganizacion.AsignarFoco();
                }
            }
        }

        private void DtpFechaLlegada_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechaLlegada.SelectedDate != null)
            {
                if (skAyudaOrganizacion.Clave != "")
                {
                    if (dtpFechaInicio.SelectedDate != null && dtpFechaFinal.SelectedDate != null)
                    {
                        DateTime fechaLLegada;
                        if (DateTime.TryParse(dtpFechaLlegada.SelectedDate.ToString(), out fechaLLegada))
                        {
                            if (!(dtpFechaInicio.SelectedDate <= dtpFechaLlegada.SelectedDate &&
                                  dtpFechaFinal.SelectedDate >= dtpFechaLlegada.SelectedDate))
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.SolicitudPremezclas_FechaLlegada,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                dtpFechaLlegada.SelectedDate = null;
                            }
                            else
                            {
                                foreach (var premezclaInfo in listaPremezcla)
                                {
                                    premezclaInfo.Habilitado = true;
                                }
                                dgPremezclas.ItemsSource = new List<PremezclaInfo>();
                                dgPremezclas.ItemsSource = listaPremezcla;
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudPremezclas_FechaLlegada,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        foreach (var premezclaInfo in listaPremezcla)
                        {
                            premezclaInfo.Habilitado = false;
                        }
                        dgPremezclas.ItemsSource = new List<PremezclaInfo>();
                        dgPremezclas.ItemsSource = listaPremezcla;
                        dtpFechaLlegada.SelectedDate = null;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_Seleccionar,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    dtpFechaLlegada.SelectedDate = null;
                    skAyudaOrganizacion.AsignarFoco();
                }
            }
            else
            {
                if (listaPremezcla != null)
                {
                    foreach (var premezclaInfo in listaPremezcla)
                    {
                        premezclaInfo.Habilitado = false;
                        premezclaInfo.Cantidad = 0;
                    }
                    dgPremezclas.ItemsSource = new List<PremezclaInfo>();
                    dgPremezclas.ItemsSource = listaPremezcla;
                }
            }
        }

        private void DtpFechaFinal_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (dtpFechaFinal.SelectedDate != null)
            {
                if (skAyudaOrganizacion.Clave != "")
                {
                    if (dtpFechaInicio.SelectedDate == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudPremezclas_FechaInicioInvalida,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        dtpFechaInicio.SelectedDate = null;
                        dtpFechaInicio.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_Seleccionar,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    dtpFechaFinal.SelectedDate = null;
                    skAyudaOrganizacion.AsignarFoco();
                }
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(
                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.SolicitudPremezclas_MsgCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarTodoOrganizacion();
            }
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (skAyudaOrganizacion.Clave != "")
            {
                if (listaPremezcla != null)
                {
                    if (listaPremezcla.Where(registro => registro.Cantidad > 0).ToList().Count == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudPremezclas_MsgDatosBlanco,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        return;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_MsgDatosBlanco,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    return;
                }
                if (dtpFechaLlegada.SelectedDate != null)
                {
                    if (dtpFechaLlegada.SelectedDate >
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                    {
                        if (renglonModificar == 0)
                        {
                            SolicitudPremezclaInfo solicitudPremezcla = new SolicitudPremezclaInfo();
                            solicitudPremezcla.Fecha = (DateTime) dtpFechaLlegada.SelectedDate;
                            if (
                                listaSolicitud.Where(registro => registro.Fecha == solicitudPremezcla.Fecha)
                                    .ToList()
                                    .Count ==
                                0)
                            {
                                solicitudPremezcla.ListaSolicitudPremezcla = new List<SolicitudPremezclaDetalleInfo>();
                                foreach (var premezclaInfo in listaPremezcla)
                                {
                                    if (premezclaInfo.Cantidad != 0)
                                    {
                                        solicitudPremezcla.ListaSolicitudPremezcla.Add(new SolicitudPremezclaDetalleInfo
                                            ()
                                        {
                                            Premezcla =
                                                new PremezclaInfo()
                                                {
                                                    PremezclaId = premezclaInfo.PremezclaId,
                                                    Descripcion = premezclaInfo.Descripcion
                                                },
                                            CantidadSolicitada = premezclaInfo.Cantidad
                                        });
                                    }
                                }
                                listaSolicitud.Add(solicitudPremezcla);
                                listaSolicitud = listaSolicitud.OrderBy(registro => registro.Fecha).ToList();

                                int contador = 1;
                                foreach (var solicitudPremezclaInfo in listaSolicitud)
                                {
                                    solicitudPremezclaInfo.SolicitudPremezclaId = contador;
                                    contador++;
                                }

                                dgSolicitudProducto.ItemsSource = new List<SolicitudPremezclaInfo>();
                                dgSolicitudProducto.ItemsSource = listaSolicitud;

                                LimpiarCampos();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.SolicitudPremezclas_MsgFechaExistente,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                            }
                        }
                        else
                        {
                            var listaSolicitudValidacion =
                                listaSolicitud.Where(
                                    registro =>
                                        registro.SolicitudPremezclaId !=
                                        listaSolicitud[renglonModificar].SolicitudPremezclaId).ToList();
                            if (listaSolicitudValidacion.Count > 0)
                            {
                                if (
                                    listaSolicitudValidacion.Where(
                                        registro => registro.Fecha == (DateTime) dtpFechaLlegada.SelectedDate)
                                        .ToList()
                                        .Count == 0)
                                {
                                    ActualizaRenglon();
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.SolicitudPremezclas_MsgFechaExistente,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                }
                            }
                            else
                            {
                                ActualizaRenglon();
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudPremezclas_FechaLlegadaInvalida,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        dtpFechaLlegada.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_MsgDatosBlanco,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    dtpFechaLlegada.Focus();
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         Properties.Resources.SolicitudPremezclas_Seleccionar,
                         MessageBoxButton.OK,
                         MessageImage.Stop);
                skAyudaOrganizacion.AsignarFoco(); 
            }
        }

        private void ActualizaRenglon()
        {
            listaSolicitud[renglonModificar].ListaSolicitudPremezcla =
                            new List<SolicitudPremezclaDetalleInfo>();
            if (dtpFechaLlegada.SelectedDate != null)
                listaSolicitud[renglonModificar].Fecha = (DateTime)dtpFechaLlegada.SelectedDate;
            foreach (var premezclaInfo in listaPremezcla)
            {
                if (premezclaInfo.Cantidad != 0)
                {
                    listaSolicitud[renglonModificar].ListaSolicitudPremezcla.Add(new SolicitudPremezclaDetalleInfo
                        ()
                    {
                        Premezcla =
                            new PremezclaInfo()
                            {
                                PremezclaId = premezclaInfo.PremezclaId,
                                Descripcion = premezclaInfo.Descripcion
                            },
                        CantidadSolicitada = premezclaInfo.Cantidad
                    });
                }
            }
            listaSolicitud = listaSolicitud.OrderBy(registro => registro.Fecha).ToList();

            int contador = 1;
            foreach (var solicitudPremezclaInfo in listaSolicitud)
            {
                solicitudPremezclaInfo.SolicitudPremezclaId = contador;
                contador++;
            }
            dgSolicitudProducto.ItemsSource = new List<SolicitudPremezclaInfo>();
            dgSolicitudProducto.ItemsSource = listaSolicitud;
            LimpiarCampos();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (dgSolicitudProducto.SelectedIndex > 0)
            {
                if (listaSolicitud[dgSolicitudProducto.SelectedIndex].Fecha > DateTime.Now)
                {
                    foreach (var premezclaInfo in listaPremezcla)
                    {
                        PremezclaInfo info = premezclaInfo;
                        var solicitudPremezcla =
                            listaSolicitud[dgSolicitudProducto.SelectedIndex].ListaSolicitudPremezcla.FirstOrDefault(
                                registro => registro.Premezcla.PremezclaId == info.PremezclaId);
                        if (solicitudPremezcla != null)
                        {
                            premezclaInfo.Cantidad = solicitudPremezcla.CantidadSolicitada;
                        }
                    }
                    dgPremezclas.ItemsSource = new List<PremezclaInfo>();
                    dgPremezclas.ItemsSource = listaPremezcla;
                    dtpFechaLlegada.SelectedDate = listaSolicitud[dgSolicitudProducto.SelectedIndex].Fecha;
                    renglonModificar = dgSolicitudProducto.SelectedIndex;
                    btnAgregar.Content = Properties.Resources.SolicitudPremezclas_btnActualizar;
                }
            }
        }

        private void RowHeaderToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = (DependencyObject)e.OriginalSource;
            while (!(obj is DataGridRow) && obj != null) obj = VisualTreeHelper.GetParent(obj);
            if (obj is DataGridRow)
            {
                (obj as DataGridRow).IsSelected = (obj as DataGridRow).DetailsVisibility != Visibility.Visible;
            }
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            if (listaSolicitud != null)
            {
                if (listaSolicitud.Count > 0)
                {
                    if (dtpFechaInicio.SelectedDate != null && dtpFechaFinal.SelectedDate != null)
                    {
                        var solicitudPremezclaPl = new SolicitudPremezclaPL();
                        var solicitud = new SolicitudPremezclaInfo();
                        solicitud.FechaInicio = (DateTime) dtpFechaInicio.SelectedDate;
                        solicitud.FechaFin = (DateTime) dtpFechaFinal.SelectedDate;
                        solicitud.Organizacion = new OrganizacionInfo(){OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave)};
                        solicitud.Activo = EstatusEnum.Activo;

                        solicitud.ListaSolicitudPremezcla = new List<SolicitudPremezclaDetalleInfo>();
                        foreach (var solicitudPremezclaInfo in listaSolicitud)
                        {
                            foreach (var solicitudPremezclaDetalleInfo in solicitudPremezclaInfo.ListaSolicitudPremezcla)
                            {
                                solicitud.ListaSolicitudPremezcla.Add(new SolicitudPremezclaDetalleInfo()
                                {
                                    FechaLlegada = solicitudPremezclaInfo.Fecha,
                                    Premezcla =
                                        new PremezclaInfo()
                                        {
                                            PremezclaId = solicitudPremezclaDetalleInfo.Premezcla.PremezclaId
                                        },
                                    CantidadSolicitada = solicitudPremezclaDetalleInfo.CantidadSolicitada
                                });
                            }
                        }
                        solicitud.UsuarioCreacion = new UsuarioInfo(){UsuarioID = usuarioId};
                        if (solicitudPremezclaPl.Guardar(solicitud))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SolicitudPremezclas_MsgGuardadoCorrecto,
                                MessageBoxButton.OK,
                                MessageImage.Correct);
                            LimpiarTodoOrganizacion();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SolicitudPremezclas_MsgOcurrioError,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_MsgDatosBlanco,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_MsgDatosBlanco,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudPremezclas_MsgDatosBlanco,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
            }
        }
    }
}
