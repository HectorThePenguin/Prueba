﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Formula.xaml
    /// </summary>
    public partial class Formula
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FormulaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FormulaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Formula()
        {
            InitializeComponent();
            InicializaContexto();
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
                ucPaginacion.DatosDelegado += ObtenerListaFormula;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Formula_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Formula_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var formulaEdicion = new FormulaEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.Formula_Nuevo_Titulo }
                };
                MostrarCentrado(formulaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Formula_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var formulaInfoSelecionado = (FormulaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (formulaInfoSelecionado != null)
                {
                    var formulaEdicion = new FormulaEdicion(formulaInfoSelecionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.Formula_Editar_Titulo }
                    };
                    MostrarCentrado(formulaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Formula_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FormulaInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Producto = new ProductoInfo
                                              {
                                                  Familia = new FamiliaInfo(),
                                                  SubFamilia = new SubFamiliaInfo
                                                                   {
                                                                       Familia = new FamiliaInfo()
                                                                   }
                                              }
                           };
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaFormula(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private FormulaInfo ObtenerFiltros()
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
        private void ObtenerListaFormula(int inicio, int limite)
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
                var formulaPL = new FormulaPL();
                FormulaInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<FormulaInfo> resultadoInfo = formulaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Formula>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Formula_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Formula_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}