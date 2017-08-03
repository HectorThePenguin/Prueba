using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para AjusteDeInventario.xaml
    /// </summary>
    public partial class AjusteDeInventario
    {
        private int organizacionID;
        private int usuarioID;
        private int almacenID;
        private long folioMovimiento;
        private long almacenMovimientoID;
        private string nombreUsuario;
        private List<AjusteDeInventarioDiferenciasInventarioInfo> listadoProductos;
        #region Constructor
        public AjusteDeInventario(AlmacenMovimientoInfo seleccion)
        {
            InitializeComponent();
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();
            nombreUsuario = Application.Current.Properties["Nombre"].ToString();
            almacenID = seleccion.AlmacenID;
            folioMovimiento = seleccion.FolioMovimiento;
            almacenMovimientoID = seleccion.AlmacenMovimientoID;
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Selecciona todos los checkbox del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkTodos_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((CheckBox)sender).IsChecked;
            SetCheckbox(isChecked != null && isChecked.Value);
        }

        /// <summary>
        /// Evento seleccionar y deseleccionar un elemento del check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelect_Click(object sender, RoutedEventArgs e)
        {
            RecorrerCheck();
        }

        /// <summary>
        /// Evento click del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ajusteDeInventarioPL = new AjusteDeInventarioPL();
                if (txtObservaciones.Text.Trim() != String.Empty)
                {
                    if (listadoProductos != null)
                    {
                        var seleccionados = listadoProductos.Any(articulosComprobar => articulosComprobar.Seleccionado);
                        if (seleccionados)
                        {
                            var almacenMovimientoInfo = new AlmacenMovimientoInfo
                            {
                                AlmacenMovimientoID = almacenMovimientoID,
                                Almacen = new AlmacenInfo
                                    {
                                        AlmacenID = almacenID
                                    },
                                Observaciones = txtObservaciones.Text,
                                UsuarioModificacionID = usuarioID,
                                OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                            };
                            IList<ResultadoPolizaModel> pdfs = ajusteDeInventarioPL.GuardarAjusteDeInventario(listadoProductos,
                                                                                    almacenMovimientoInfo);
                            if (pdfs != null)
                            {
                                for (var i = 0; i < pdfs.Count; i++)
                                {
                                    if (pdfs[i].PDFs != null)
                                    {
                                        foreach (var memoryStream in pdfs[i].PDFs)
                                        {
                                            ImprimePoliza(memoryStream.Value, memoryStream.Key);
                                        }
                                    }
                                }
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.AjusteDeInventario_MensajeGuardar,
                               MessageBoxButton.OK, MessageImage.Correct);

                                Close();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.AjusteDeInventario_ErrorGuardar,
                                MessageBoxButton.OK, MessageImage.Warning);
                            }
                        }
                        else
                        {
                            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.AjusteDeInventario_NoSeRealizaranAjustes,
                                MessageBoxButton.OK, MessageImage.Warning) != MessageBoxResult.OK) return;
                            var almacenMovimientoInfo = new AlmacenMovimientoInfo
                            {
                                AlmacenMovimientoID = almacenMovimientoID,
                                Almacen = new AlmacenInfo
                                    {
                                        AlmacenID = almacenID
                                    },
                                Observaciones = txtObservaciones.Text,
                                UsuarioModificacionID = usuarioID,
                                OrganizacionID = organizacionID
                            };
                            IList<ResultadoPolizaModel> pdfs = ajusteDeInventarioPL.GuardarAjusteDeInventario(listadoProductos,
                                                                                    almacenMovimientoInfo);
                            if (pdfs != null)
                            {
                                for (var i = 0; i < pdfs.Count; i++)
                                {
                                    if (pdfs[i].PDFs != null)
                                    {
                                        foreach (var memoryStream in pdfs[i].PDFs)
                                        {
                                            ImprimePoliza(memoryStream.Value, memoryStream.Key);
                                        }
                                    }
                                }
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.AjusteDeInventario_MensajeGuardar,
                               MessageBoxButton.OK, MessageImage.Correct);

                                Close();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.AjusteDeInventario_ErrorGuardar,
                                MessageBoxButton.OK, MessageImage.Warning);
                            }
                        }
                    }
                    else
                    {
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.AjusteDeInventario_NoSeRealizaranAjustes,
                            MessageBoxButton.OK, MessageImage.Warning) != MessageBoxResult.OK) return;
                        var almacenMovimientoInfo = new AlmacenMovimientoInfo
                        {
                            AlmacenMovimientoID = almacenMovimientoID,
                            Observaciones = txtObservaciones.Text,
                            UsuarioModificacionID = usuarioID,
                            Almacen = new AlmacenInfo
                            {
                                AlmacenID = almacenID
                            },
                            OrganizacionID = organizacionID
                        };
                        IList<ResultadoPolizaModel> pdfs = ajusteDeInventarioPL.GuardarAjusteDeInventario(listadoProductos,
                                                                                    almacenMovimientoInfo);
                        if (pdfs != null)
                        {

                            for (var i = 0; i < pdfs.Count; i++)
                            {
                                if (pdfs[i].PDFs != null)
                                {
                                    foreach (var memoryStream in pdfs[i].PDFs)
                                    {
                                        ImprimePoliza(memoryStream.Value, memoryStream.Key);
                                    }
                                }
                            }
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.AjusteDeInventario_MensajeGuardar,
                           MessageBoxButton.OK, MessageImage.Correct);

                            Close();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.AjusteDeInventario_ErrorGuardar,
                            MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AjusteDeInventario_DebeCapturarObservaciones,
                    MessageBoxButton.OK, MessageImage.Warning);
                    txtObservaciones.Focus();
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void ImprimePoliza(MemoryStream pdf, TipoPoliza tipoPoliza)
        {
            var exportarPoliza = new ExportarPoliza();
            exportarPoliza.ImprimirPoliza(pdf, string.Format("{0} {1}", "Poliza", tipoPoliza));
        }

        /// <summary>
        /// Evento loaded de ucTitulo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucTitulo_Loaded(object sender, RoutedEventArgs e)
        {
            CargarCboAlmacenes();
            LlenarDatosAlmacenMovimiento();
            LlenarGridDiferencias();
            txtObservaciones.Focus();
        }

        /// <summary>
        /// Evento click de boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.AjusteDeInventario_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para Checar los checkbox del Grid
        /// </summary>
        /// <param name="value"></param>
        private void SetCheckbox(bool value)
        {
            try
            {
                IList<AjusteDeInventarioDiferenciasInventarioInfo> diferenciasInventarioLista = new List<AjusteDeInventarioDiferenciasInventarioInfo>();
                foreach (var ajusteDeInventarioDiferenciasGanado in gridDatos.Items.Cast<AjusteDeInventarioDiferenciasInventarioInfo>())
                {
                    ajusteDeInventarioDiferenciasGanado.Seleccionado = value;
                    diferenciasInventarioLista.Add(ajusteDeInventarioDiferenciasGanado);
                }
                listadoProductos = (List<AjusteDeInventarioDiferenciasInventarioInfo>)diferenciasInventarioLista;
                gridDatos.ItemsSource = diferenciasInventarioLista;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Llena datos seccion ajuste de inventario e historial
        /// </summary>
        private void LlenarDatosAlmacenMovimiento()
        {
            var usuarioPL = new UsuarioPL();
            var almacenMovimientoInfo = new AlmacenMovimientoInfo
            {
                AlmacenID = almacenID,
                FolioMovimiento = folioMovimiento,
                AlmacenMovimientoID = almacenMovimientoID
            };
            var almacenPL = new AlmacenPL();
            var almacenMovimiento = almacenPL.ObtenerAlmacenMovimiento(almacenMovimientoInfo);

            if (almacenMovimiento == null) return;
            //Informacion almacen
            txtFolioInventarioFisico.Text = Convert.ToString(almacenMovimiento.FolioMovimiento);

            var tipoMovimiento = new TipoMovimientoPL();
            var resultado = tipoMovimiento.ObtenerPorID(almacenMovimiento.TipoMovimientoID);
            if (resultado != null)
            {
                txtTipoMovimiento.Text = resultado.Descripcion;
            }

            //Fecha actual
            var fechaActual = DateTime.Now;
            txtFecha.Text = fechaActual.ToString("dd'/'MM'/'yyyy' 'hh':'mm  tt", CultureInfo.InvariantCulture);
            //Obtener descripcion de estatus
            var ajusteDeInventarioPL = new AjusteDeInventarioPL();
            var estatusInfo = ajusteDeInventarioPL.ObtenerEstatusInfo(almacenMovimiento);
            if (estatusInfo != null)
            {
                txtEstado.Text = estatusInfo.Descripcion;
            }

            //Usuarios creacion y modificacion
            var usuario = usuarioPL.ObtenerPorID(almacenMovimiento.UsuarioCreacionID);
            if (usuario != null)
            {
                txtCreadoPor.Text = usuario.Nombre;
            }

            txtModificadoPor.Text = nombreUsuario;
            txtUltimaActualizacion.Text = fechaActual.ToString("dd'/'MM'/'yyyy' 'hh':'mm  tt", CultureInfo.InvariantCulture);

            //Fecha creacion y modificacion
            //var fechaVal = new DateTime(1492, 10, 12);
            var fechaCreacion = almacenMovimiento.FechaCreacion;
            txtFechaCreacion.Text = fechaCreacion.ToString("dd'/'MM'/'yyyy' 'hh':'mm  tt", CultureInfo.InvariantCulture);

        }

        /// <summary>
        /// Metodo que llena grid de diferencias de inventario
        /// </summary>
        private void LlenarGridDiferencias()
        {
            try
            {
                var ajusteDeInventarioPL = new AjusteDeInventarioPL();
                var almacenMovimientoInfo = new AlmacenMovimientoInfo
                {
                    AlmacenID = almacenID,
                    AlmacenMovimientoID = almacenMovimientoID
                };
                listadoProductos = ajusteDeInventarioPL.ObtenerDiferenciaInventarioFisicoTeorico(almacenMovimientoInfo, organizacionID);
                if (listadoProductos != null)
                {
                    listadoProductos = FiltrarProductosAlmacen(listadoProductos);
                    if (listadoProductos.Count > 0)
                    {
                        gridDatos.ItemsSource = listadoProductos;
                    }
                    else
                    {
                        txtObservaciones.Focus();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.AjusteDeInventario_MensajeNoDiferencias,
                        MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    txtObservaciones.Focus();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AjusteDeInventario_MensajeNoDiferencias,
                    MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private List<AjusteDeInventarioDiferenciasInventarioInfo> FiltrarProductosAlmacen(List<AjusteDeInventarioDiferenciasInventarioInfo> inventariosAlmacenCierres)
        {
            var parametroGeneralPL = new ParametroGeneralPL();
            ParametroGeneralInfo parametroGeneral =
                parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.PRODUCTOAJUSTAR.ToString());

            var productosEliminados = new List<int>();
            if (parametroGeneral != null)
            {
                List<string> productos = parametroGeneral.Valor.Split('|').ToList();
                productosEliminados = productos.Select(p => Convert.ToInt32(p)).ToList();
            } List<int> productosActuales = inventariosAlmacenCierres.Select(prod => prod.ProductoID).ToList();
            List<int> productosPorMostrar = productosActuales.Except(productosEliminados).ToList();
            inventariosAlmacenCierres =
                inventariosAlmacenCierres.Join(productosPorMostrar, x => x.ProductoID, i => i, (inv, prod) => inv).ToList();
            return inventariosAlmacenCierres;
        }

        /// <summary>
        /// Metodo que corre el check box y obtiene los elementos
        /// </summary>
        public void RecorrerCheck()
        {
            try
            {
                IList<AjusteDeInventarioDiferenciasInventarioInfo> diferenciasInventarioLista = gridDatos.Items.Cast<AjusteDeInventarioDiferenciasInventarioInfo>().ToList();
                listadoProductos = (List<AjusteDeInventarioDiferenciasInventarioInfo>)diferenciasInventarioLista;
                if (listadoProductos == null) return;
                foreach (var ajusteDeInventarioDiferenciasInventarioInfo in listadoProductos)
                {
                    if (ajusteDeInventarioDiferenciasInventarioInfo.Seleccionado == false)
                    {
                        chkTodos.IsChecked = false;
                        break;
                    }
                }
                gridDatos.ItemsSource = listadoProductos;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que llena combo almacenes y extablecer almacen
        /// </summary>
        private void CargarCboAlmacenes()
        {
            try
            {
                var almacenPl = new AlmacenPL();
                var listaAlmacenInfo = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionID);
                if (listaAlmacenInfo == null) return;
                var almacenes = new AlmacenInfo { Descripcion = Properties.Resources.AjusteDeInventario_Seleccione, AlmacenID = 0 };
                listaAlmacenInfo.Insert(0, almacenes);
                CboAlmacenes.ItemsSource = listaAlmacenInfo;
                CboAlmacenes.SelectedValue = almacenID;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        #endregion
    }
}
