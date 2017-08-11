﻿using System;                      
using System.Collections.Generic;  
using System.Reflection;           
using System.Windows;              
using System.Windows.Input;        
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

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for SubFamilia.xaml
    /// </summary>
    public partial class SubFamilia
    {
        #region Propiedad

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SubFamiliaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SubFamiliaInfo) DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public SubFamilia()
        {
            InitializeComponent();
            CargaFamilia();
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaSubFamilia;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SubFamilia_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SubFamilia_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var subFamiliaEdicion = new SubFamiliaEdicion
                                            {
                                                ucTitulo = {TextoTitulo = Properties.Resources.SubFamilia_Nuevo_Titulo}
                                            };
                MostrarCentrado(subFamiliaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SubFamilia_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button) e.Source;
            try
            {
                var subFamiliaInfoSelecionado = (SubFamiliaInfo) Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (subFamiliaInfoSelecionado != null)
                {
                    var subFamiliaEdicion = new SubFamiliaEdicion(subFamiliaInfoSelecionado)
                                                {
                                                    ucTitulo =
                                                        {TextoTitulo = Properties.Resources.SubFamilia_Editar_Titulo}
                                                };
                    MostrarCentrado(subFamiliaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SubFamilia_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new SubFamiliaInfo
                           {
                               Descripcion = string.Empty,
                               Familia = new FamiliaInfo(),
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaSubFamilia(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private SubFamiliaInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaSubFamilia(int inicio, int limite)
        {
            try
            {
                var subFamiliaPL = new SubFamiliaPL();
                SubFamiliaInfo filtros = ObtenerFiltros();
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<SubFamiliaInfo> resultadoInfo = subFamiliaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<SubFamilia>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SubFamilia_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SubFamilia_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga Familias
        /// </summary>
        private void CargaFamilia()
        {
            var familiaPL = new FamiliaPL();
            var familia = new FamiliaInfo
            {
                FamiliaID = 0,
                Descripcion = Properties.Resources.Seleccione_Todos,
            };
            IList<FamiliaInfo> listaFamilias = familiaPL.ObtenerTodos(EstatusEnum.Activo);
            listaFamilias.Insert(0, familia);
            cmbFamilia.ItemsSource = listaFamilias;
            cmbFamilia.SelectedItem = familia;
        }

        /// <summary>
        /// Reinicia el valor del inicio de la página.
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
    }
}