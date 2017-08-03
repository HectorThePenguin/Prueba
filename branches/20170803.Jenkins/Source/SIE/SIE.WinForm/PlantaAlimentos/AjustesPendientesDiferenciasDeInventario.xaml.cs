using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para AjustesPendientesDiferenciasDeInventario.xaml
    /// </summary>
    public partial class AjustesPendientesDiferenciasDeInventario
    {
        #region Propiedades
        private int usuarioID;
        public List<DiferenciasDeInventariosInfo> ListaAjustesPendientesBuscados = new List<DiferenciasDeInventariosInfo>();
        public List<DiferenciasDeInventariosInfo> ListaAjustesPendientesSeleccionados = new List<DiferenciasDeInventariosInfo>();
        public List<DiferenciasDeInventariosInfo> ListaAjustesPendientesPrincipal = new List<DiferenciasDeInventariosInfo>();

        public List<DiferenciasDeInventariosInfo> ListaAjustesPendientesRegresoSeleccionados
        {
            get { return ListaAjustesPendientesSeleccionados; }
            set { ListaAjustesPendientesSeleccionados = value; }
        }
        #endregion

        #region Constructor
        //Constructor de la clase
        public AjustesPendientesDiferenciasDeInventario(List<DiferenciasDeInventariosInfo> listaAjustesPendientesInfo)
        {
            InitializeComponent();
            ListaAjustesPendientesPrincipal = listaAjustesPendientesInfo;
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Carga combos y listado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjustesPendientesDiferenciasDeInventario_OnLoaded(object sender, RoutedEventArgs e)
        {
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            CargarComboAjuste();
            CargarComboEstatus();
            RealizarBusqueda();
        }

        /// <summary>
        /// Obtiene elementos seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Se verifica si hay elementos seleccionados
                var seleccionados = ListaAjustesPendientesBuscados.Any(diferenciasInventarioInfo => diferenciasInventarioInfo.Seleccionado);
                if (seleccionados)
                {
                    foreach (var diferenciasInvInfo in ListaAjustesPendientesBuscados.Where(diferenciasInvInfo => diferenciasInvInfo.Seleccionado))
                    {
                        ListaAjustesPendientesRegresoSeleccionados.Add(diferenciasInvInfo);
                    }
                    Close();
                }
                else
                {
                    //Mensaje debe seleccionar ajuste
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AjustesPendientesDiferenciasDeInventario_MensajeAgregarAjustes,
                    MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //Excepcion
            }
        }

        /// <summary>
        /// Busqueda 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            RealizarBusqueda();
        }

        /// <summary>
        /// Eliminar ajuste y cambiar estatus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var diferenciasDeInventarioInfo = (DiferenciasDeInventariosInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (diferenciasDeInventarioInfo != null)
                {
                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AjustesPendientesDiferenciasDeInventario_MensajeEliminarAjuste,
                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                    {
                        //Eliminar ajuste
                        var almacenMovimientoPl = new AlmacenMovimientoPL();
                        almacenMovimientoPl.ActualizarEstatus(new AlmacenMovimientoInfo()
                        {
                            AlmacenMovimientoID = diferenciasDeInventarioInfo.AlmacenMovimiento.AlmacenMovimientoID,
                            Status = Estatus.DifInvCancelado.GetHashCode(),
                            UsuarioModificacionID = usuarioID
                        });
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.AjustesPendientesDiferenciasDeInventario_MensajeEliminadoCorrectamente,
                        MessageBoxButton.OK, MessageImage.Correct);
                        RealizarBusqueda();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //Excepcion
            }
        }

        /// <summary>
        /// Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.AjustesPendientesDiferenciasDeInventario_MensajeCancelar,
               MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                //Eliminar ajuste
                Close();
            }
        }

        /// <summary>
        /// Selecciona todos los elementos del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkTodos_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((CheckBox)sender).IsChecked;
            SetCheckbox(isChecked != null && isChecked.Value);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Cargar combo ajuste
        /// </summary>
        private void CargarComboAjuste()
        {
            try
            {
                CboAjuste.Items.Insert(0, Properties.Resources.DiferenciasDeInventario_CboSeleccione);
                CboAjuste.Items.Insert(TipoAjusteEnum.Merma.GetHashCode(), TipoAjusteEnum.Merma);
                CboAjuste.Items.Insert(TipoAjusteEnum.Superávit.GetHashCode(), TipoAjusteEnum.Superávit);
                CboAjuste.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Carga combo estatus
        /// </summary>
        private void CargarComboEstatus()
        {
            try
            {
                var estatusPl = new EstatusPL();
                var listaEstatus = estatusPl.ObtenerEstatusTipoEstatus(TipoEstatusEnum.DiferenciaDeInventarios.GetHashCode());
                var listaEstatusFiltrado = listaEstatus.Where(estatusInfo => estatusInfo.EstatusId == Estatus.DifInvAutorizado.GetHashCode() || estatusInfo.EstatusId == Estatus.DifInvRechazado.GetHashCode() || estatusInfo.EstatusId == Estatus.DifInvPendiente.GetHashCode()).ToList();

                CboEstatus.ItemsSource = null;
                var estatusInfoN = new EstatusInfo { Descripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione, EstatusId = 0 };
                listaEstatusFiltrado.Insert(0, estatusInfoN);
                CboEstatus.ItemsSource = listaEstatusFiltrado;
                CboEstatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //Excepcion
            }
        }

        /// <summary>
        /// Busca de acuerdo a lo especificado en los combos
        /// </summary>
        private void RealizarBusqueda()
        {
            try
            {
                var listaEstatus = new List<EstatusInfo>();
                var listaTipoMovimiento = new List<TipoMovimientoInfo>();

                //Obtener ajuste
                if (CboAjuste.SelectedItem == null || CboAjuste.SelectedIndex == 0)
                {
                    listaTipoMovimiento.Add(new TipoMovimientoInfo()
                    {
                        TipoMovimientoID = TipoMovimiento.EntradaPorAjuste.GetHashCode()
                    });
                    listaTipoMovimiento.Add(new TipoMovimientoInfo()
                    {
                        TipoMovimientoID = TipoMovimiento.SalidaPorAjuste.GetHashCode()
                    });
                }
                else
                {
                    if (CboAjuste.SelectedIndex == TipoAjusteEnum.Merma.GetHashCode())
                    {
                        listaTipoMovimiento.Add(new TipoMovimientoInfo()
                        {
                            TipoMovimientoID = TipoMovimiento.SalidaPorAjuste.GetHashCode()
                        });
                    }
                    if (CboAjuste.SelectedIndex == TipoAjusteEnum.Superávit.GetHashCode())
                    {
                        listaTipoMovimiento.Add(new TipoMovimientoInfo()
                        {
                            TipoMovimientoID = TipoMovimiento.EntradaPorAjuste.GetHashCode()
                        });
                    }
                }

                //Obtener estatus
                if (CboEstatus.SelectedItem == null || CboEstatus.SelectedIndex == 0)
                {
                    listaEstatus.Add(new EstatusInfo() { EstatusId = Estatus.DifInvAutorizado.GetHashCode() });
                    listaEstatus.Add(new EstatusInfo() { EstatusId = Estatus.DifInvPendiente.GetHashCode() });
                    listaEstatus.Add(new EstatusInfo() { EstatusId = Estatus.DifInvRechazado.GetHashCode() });
                }
                else
                {
                    listaEstatus.Add((EstatusInfo)CboEstatus.SelectedItem);
                }

                //Llenar grid
                var diferenciasDeInventariosPl = new DiferenciasDeInventarioPL();
                ListaAjustesPendientesBuscados =
                    diferenciasDeInventariosPl.ObtenerAjustesPendientesPorUsuario(listaEstatus, listaTipoMovimiento,
                        new UsuarioInfo() { UsuarioCreacionID = usuarioID });

                if (ListaAjustesPendientesBuscados != null)
                {
                    var listAux = (from diferenciasInventarioV in ListaAjustesPendientesBuscados from diferenciasInventarioI in ListaAjustesPendientesPrincipal where diferenciasInventarioV.AlmacenMovimiento.AlmacenMovimientoID == diferenciasInventarioI.AlmacenMovimiento.AlmacenMovimientoID select diferenciasInventarioV).ToList();

                    //Eliminar de lista los items de la lista principal
                    foreach (var diferenciasDeInventariosInfoP in listAux)
                    {
                        ListaAjustesPendientesBuscados.Remove(diferenciasDeInventariosInfoP);
                    }

                    foreach (var diferenciasDeInventariosInfoPar in ListaAjustesPendientesBuscados)
                    {
                        if (diferenciasDeInventariosInfoPar.Estatus.EstatusId !=
                            Estatus.DifInvAutorizado.GetHashCode())
                        {
                            diferenciasDeInventariosInfoPar.Editable = true;
                            diferenciasDeInventariosInfoPar.Eliminable = true;
                        }

                        diferenciasDeInventariosInfoPar.DescripcionAjuste =
                            diferenciasDeInventariosInfoPar.AlmacenMovimiento.TipoMovimientoID ==
                            TipoMovimiento.SalidaPorAjuste.GetHashCode()
                                ? TipoAjusteEnum.Merma.ToString()
                                : TipoAjusteEnum.Superávit.ToString();
                        diferenciasDeInventariosInfoPar.KilogramosFisicos =
                            diferenciasDeInventariosInfoPar.AlmacenMovimientoDetalle.Cantidad;

                        //Obtener kilogramos totales
                        var almacenMovimientoDetalle = new AlmacenMovimientoDetalle()
                        {
                            AlmacenInventarioLoteId = diferenciasDeInventariosInfoPar.AlmacenInventarioLote.AlmacenInventarioLoteId
                        };
                        var listaMovimientos = new List<TipoMovimientoInfo>
                        {
                            new TipoMovimientoInfo() {TipoMovimientoID = TipoMovimiento.EntradaPorCompra.GetHashCode()}
                        };
                        var almacenMovimientoDetallePl = new AlmacenMovimientoDetallePL();
                        var listaAlmacenMovimientoDetalle =
                            almacenMovimientoDetallePl.ObtenerAlmacenMovimientoDetallePorLoteId(
                                almacenMovimientoDetalle,
                                listaMovimientos);
                        if (listaAlmacenMovimientoDetalle != null)
                        {
                            var kilogramosTotales = (from almacenMovimientoDetalleInfo in listaAlmacenMovimientoDetalle
                                                     select almacenMovimientoDetalleInfo.Cantidad).Sum();
                            diferenciasDeInventariosInfoPar.KilogramosTotales = kilogramosTotales;

                            if (diferenciasDeInventariosInfoPar.AlmacenMovimiento.TipoMovimientoID ==
                                TipoMovimiento.SalidaPorAjuste.GetHashCode())
                            {
                                diferenciasDeInventariosInfoPar.KilogramosTeoricos =
                                    diferenciasDeInventariosInfoPar.KilogramosTotales -
                                    diferenciasDeInventariosInfoPar.KilogramosFisicos;
                            }
                            else
                            {
                                diferenciasDeInventariosInfoPar.KilogramosTeoricos =
                                    diferenciasDeInventariosInfoPar.KilogramosTotales +
                                    diferenciasDeInventariosInfoPar.KilogramosFisicos;
                            }

                            diferenciasDeInventariosInfoPar.PorcentajeAjuste =
                                (diferenciasDeInventariosInfoPar.AlmacenMovimientoDetalle.Cantidad /
                                 diferenciasDeInventariosInfoPar.KilogramosTotales) * 100;
                        }
                        //
                        diferenciasDeInventariosInfoPar.AlmacenMovimiento.UsuarioModificacionID = usuarioID;
                        diferenciasDeInventariosInfoPar.AlmacenMovimientoDetalle.UsuarioModificacionID = usuarioID;
                        diferenciasDeInventariosInfoPar.AlmacenInventarioLote.UsuarioModificacionId = usuarioID;
                    }

                    GridDiferenciasDeInventarios.ItemsSource = null;
                    GridDiferenciasDeInventarios.ItemsSource = ListaAjustesPendientesBuscados;

                }
                else
                {
                    GridDiferenciasDeInventarios.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //Excepcion
            }
        }

        /// <summary>
        /// Marca como seleccionado los elementos del grid
        /// </summary>
        /// <param name="value"></param>
        private void SetCheckbox(bool value)
        {
            try
            {
                var diferenciasInventarioLista = new List<DiferenciasDeInventariosInfo>();
                foreach (var diferenciasDeInventarioInfo in GridDiferenciasDeInventarios.Items.Cast<DiferenciasDeInventariosInfo>())
                {
                    diferenciasDeInventarioInfo.Seleccionado = value;
                    diferenciasInventarioLista.Add(diferenciasDeInventarioInfo);
                }
                GridDiferenciasDeInventarios.ItemsSource = diferenciasInventarioLista;
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