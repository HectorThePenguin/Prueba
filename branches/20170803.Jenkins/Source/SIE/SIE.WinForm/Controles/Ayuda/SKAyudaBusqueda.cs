using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Services.Info.Enums;
using iTextSharp.text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Base.Validadores;
using SIE.Services.Info.Ayuda;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SuKarne.Controls.PaginControl;
using System.ComponentModel;

namespace SIE.WinForm.Controles.Ayuda
{
    internal class SKAyudaBusqueda<T> : Window
    {
        #region VARIABLES

        private DataGrid grdDatos;
        private Label lblFiltro;
        private PaginacionControl pagControl;
        private TextBox txtValorFiltro;
        private Titulo titulo;

        private bool confirmaSalir = true;
        private static IDictionary<string, object> ValoresAnteriores = new Dictionary<string, object>();

        private string descripcionAnterior = string.Empty;

        #endregion VARIABLES

        #region PROPIEDADES

        /// <summary>
        /// Valor de Descripcion del Catalogo
        /// </summary>
        public string Descripcion { get; private set; }

        /// <summary>
        /// Clave del Catalogo
        /// </summary>
        public string Clave { get; private set; }

        /// <summary>
        /// Identificador unico del Catalogo
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Ayuda en la cual se Generara la Busqueda
        /// </summary>
        private Object AyudaPL { get; set; }

        /// <summary>
        /// Clase info que se estara Consultando
        /// </summary>
        public T Info { get; set; }

        /// <summary>
        /// Campos de los cuales se obtendran los datos
        /// a mostrar en Ayuda
        /// </summary>
        private IList<string> CamposInfo { get; set; }

        /// <summary>
        /// Lista de Catalogos con los cuales Tendra Dependencia la Ayuda
        /// </summary>
        private IList<IDictionary<IList<string>, Object>> Dependencias { get; set; }

        /// <summary>
        /// Mensaje que Mostrara Cuando se Quiera Cerrar la Ventana
        /// </summary>
        public string MensajeCerrar { private get; set; }

        /// <summary>
        /// Mensaje que Mostrara Cuando se Quiera Realizar una Busqueda 
        /// Con el Campo de Busqueda en Blanco
        /// </summary>
        public string MensajeBusqueda { private get; set; }

        /// <summary>
        /// Mensaje que Mostrara Cuando se Quiera Agregar un Elemento
        /// Seleccionado
        /// </summary>
        public string MensajeAgregar { private get; set; }

        /// <summary>
        /// Leyenda que se Mostrara para el Campo de Busqueda
        /// </summary>
        public string TituloEtiqueta { private get; set; }

        /// <summary>
        /// Indica si se presiono el Boton de Cancelar
        /// </summary>
        public bool Cancelado { get; set; }

        /// <summary>
        /// Metodo que se Llamaara cuando se
        /// requiera consultar por Paginacion
        /// </summary>
        public String MetodoPaginado { get; set; }

        /// <summary>
        /// Titulo que se Mostrara en
        /// la Busqueda de la Ayuda
        /// </summary>
        public String TituloPantalla { get; set; }

        /// <summary>
        /// Clave que se mostrara
        /// </summary>
        public string PropiedadClave { get; set; }

        /// <summary>
        /// Descripcion que se mostrara
        /// </summary>
        public string PropiedadDescripcion { get; set; }
        /// <summary>
        /// Llave unica que identifica el registro
        /// </summary>
        public string PropiedadIdOcultoGrid { get; set; }

        /// <summary>
        /// Atributo por el cual se hara referencia a la clave
        /// </summary>
        public string AtributoClave { get; set; }
        /// <summary>
        /// Atributo por el cual se hara referencia a la descripcion
        /// </summary>
        public string AtributoDescripcion { get; set; }
        /// <summary>
        /// Atributo por el cual se hara referencia a la llave unica
        /// </summary>
        public string AtributoIdOcultoGrid { get; set; }

        private TipoCampo tipoCampoCodigo = TipoCampo.NumerosLetrasSinAcentos;

        /// <summary>
        /// Atributo para poder sobrecargar el comportamiento de la entrada
        /// de caracteres en la búesuqeda.
        /// </summary>
        public TipoCampo TipoCampoCodigo
        {
            get { return tipoCampoCodigo; }
            set { tipoCampoCodigo = value; }
        }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public SKAyudaBusqueda(Object ayuda)
        {
            AyudaPL = ayuda;
            AgregarControles();
        }

        public SKAyudaBusqueda(IList<string> camposInfo)
        {
            CamposInfo = camposInfo;
            AgregarControles();
        }

        public SKAyudaBusqueda(Object ayuda, string tituloEtiqueta, string tituloPantalla, T info, string propiedadClave
                             , string propiedadDescripcion, string propiedadOculta
                             , string atributoClave, string atributoDescripcion, string atributoOculto)
        {
            AyudaPL = ayuda;
            TituloEtiqueta = tituloEtiqueta;
            TituloPantalla = tituloPantalla;
            Info = info;
            PropiedadClave = propiedadClave;
            PropiedadDescripcion = propiedadDescripcion;
            PropiedadIdOcultoGrid = propiedadOculta;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            AtributoIdOcultoGrid = atributoOculto;
            AgregarControles();
        }

        public SKAyudaBusqueda(Object ayuda, IList<IDictionary<IList<string>, Object>> dependencias
                             , string tituloEtiqueta, string tituloPantalla, T info, string propiedadClave
                             , string propiedadDescripcion, string propiedadOculta
                             , string atributoClave, string atributoDescripcion, string atributoOculto)
        {
            AyudaPL = ayuda;
            TituloEtiqueta = tituloEtiqueta;
            Dependencias = dependencias;
            TituloPantalla = tituloPantalla;
            Info = info;
            PropiedadClave = propiedadClave;
            PropiedadDescripcion = propiedadDescripcion;
            PropiedadIdOcultoGrid = propiedadOculta;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            AtributoIdOcultoGrid = atributoOculto;
            AgregarControles();
        }

        public SKAyudaBusqueda(Object ayuda, IList<string> camposInfo, string tituloEtiqueta, string tituloPantalla)
        {
            AyudaPL = ayuda;
            TituloEtiqueta = tituloEtiqueta;
            CamposInfo = camposInfo;
            TituloPantalla = tituloPantalla;
            AgregarControles();
        }

        public SKAyudaBusqueda(Object ayuda, IList<string> camposInfo, IList<IDictionary<IList<string>, Object>> dependencias, string tituloEtiqueta, string tituloPantalla)
        {
            AyudaPL = ayuda;
            TituloEtiqueta = tituloEtiqueta;
            CamposInfo = camposInfo;
            Dependencias = dependencias;
            TituloPantalla = tituloPantalla;
            AgregarControles();
        }

        #endregion CONSTRUCTORES

        #region METODOS        

        /// <summary>
        /// Agrega los Controles Para Mostrarse
        /// en Ventana de Ayuda
        /// </summary>
        private void AgregarControles()
        {
            Width = 700;
            Height = 505;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            KeyUp += SKAyudaBusqueda_KeyUp;
            Loaded += OnLoaded;
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            Background = new SolidColorBrush(Colors.Black);

            var grupo = new GroupBox
                {
                    Header = SuKarne.Controls.Properties.Resources.Ayuda_GrupoHeader, 
                    Margin = new Thickness(15,0,15,0)
                };
            Grid grdFiltros = GenerarGridFiltros();
            if (grdFiltros != null)
            {
                grupo.Content = grdFiltros;
                Grid grdPrincipal = GenerarGridPrincipal(grupo);
                AddChild(grdPrincipal);
            }
            txtValorFiltro.Focus();
        }

        /// <summary>
        /// Genera el Grid en el Cual Estaran
        /// Contenidos los Controles
        /// </summary>
        /// <param name="grupo"></param>
        /// <returns></returns>
        private Grid GenerarGridPrincipal(GroupBox grupo)
        {
            var grdPrincipal = new Grid { Background = new SolidColorBrush(Colors.White), Margin = new Thickness(2) };

            grdPrincipal.ColumnDefinitions.Add(new ColumnDefinition());
            grdPrincipal.ColumnDefinitions.Add(new ColumnDefinition());
            grdPrincipal.ColumnDefinitions.Add(new ColumnDefinition());

            var rowControlTitulo = new RowDefinition { Height = new GridLength(50) };
            var rowControlesFiltros = new RowDefinition {Height = new GridLength(50)};
            var rowEspacio = new RowDefinition {Height = new GridLength(10)};
            var rowGrid = new RowDefinition {Height = new GridLength(350)};

            var rowPaginacion = new RowDefinition
                {
                    Height = new GridLength(50),
                };

            grdPrincipal.RowDefinitions.Add(rowControlTitulo);
            grdPrincipal.RowDefinitions.Add(rowControlesFiltros);
            grdPrincipal.RowDefinitions.Add(rowEspacio);
            grdPrincipal.RowDefinitions.Add(rowGrid);
            grdPrincipal.RowDefinitions.Add(rowPaginacion);

            titulo = GeneraControlTitulo();
            Grid.SetRow(titulo, 0);
            Grid.SetColumn(titulo, 0);
            Grid.SetColumnSpan(titulo, 3);

            Grid.SetRow(grupo, 1);
            Grid.SetColumn(grupo, 0);
            Grid.SetColumnSpan(grupo, 3);

            grdDatos = GeneraDataGridValores();
            Grid.SetRow(grdDatos, 3);
            Grid.SetColumn(grdDatos, 0);
            Grid.SetColumnSpan(grdDatos, 3);

            pagControl = GeneraControlPaginacion();
            Grid.SetRow(pagControl, 4);
            Grid.SetColumn(pagControl, 0);
            Grid.SetColumnSpan(pagControl, 3);

            grdPrincipal.Children.Add(titulo);
            grdPrincipal.Children.Add(grupo);
            grdPrincipal.Children.Add(grdDatos);
            grdPrincipal.Children.Add(pagControl);

            return grdPrincipal;
        }

        /// <summary>
        /// Genera el control para titulo
        /// </summary>
        /// <returns></returns>
        private Titulo GeneraControlTitulo()
        {
            var titulo = new Titulo
                             {
                                 Margin = new Thickness(20, 10, 20, 0),
                                 VisibleCerrar = Visibility.Visible,
                                 TextoTitulo = TituloPantalla
                             };
            return titulo;
        }

        /// <summary>
        /// Instancia el Control de Paginacion
        /// </summary>
        /// <returns></returns>
        private PaginacionControl GeneraControlPaginacion()
        {
            pagControl = new PaginacionControl {Margin = new Thickness(18, 0, 20, 0)};
            pagControl.AsignarValoresIniciales();
            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                pagControl.DatosDelegado += ObtenerValoresAyuda;
            }
            else
            {
                pagControl.DatosDelegado += ObtenerValoresAyudaCamposInfo;
            }
            pagControl.Contexto = Info;
            return pagControl;
        }

        /// <summary>
        /// Instancia el Grid en Donde se
        /// Desplegaran los Datos
        /// </summary>
        /// <returns></returns>
        private DataGrid GeneraDataGridValores()
        {
            grdDatos = new DataGrid();

            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                GenerarColumnasOcultas(grdDatos);
            }
            else
            {
                var columnID = new DataGridTextColumn
                {
                    Header = GridAyudaEnum.Clave.ToString(),
                    Binding = new Binding(GridAyudaEnum.Clave.ToString()),
                    Width = new DataGridLength(100)
                };
                grdDatos.Columns.Add(columnID);

                var columnDescripcion = new DataGridTextColumn
                {
                    Header = GridAyudaEnum.Descripcion.ToString(),
                    Binding = new Binding(GridAyudaEnum.Descripcion.ToString()),
                    Width = new DataGridLength(536)

                };
                grdDatos.Columns.Add(columnDescripcion);
            }
            grdDatos.SelectionMode = DataGridSelectionMode.Single;
            grdDatos.MouseDoubleClick += grdDatos_MouseDoubleClick;
            grdDatos.PreviewKeyDown += grdDatos_PreviewKeyDown;

            return grdDatos;
        }

        /// <summary>
        /// Genera columnas ocultas para el grid
        /// </summary>
        /// <param name="grid"></param>
        private void GenerarColumnasOcultas(DataGrid grid)
        {
            var headerColId = AuxAyuda.ObtenerEncabezadoGrid(Info, PropiedadClave, AtributoClave);
            var headerColDescripcion = AuxAyuda.ObtenerEncabezadoGrid(Info, PropiedadDescripcion, AtributoDescripcion);

            var column = new DataGridTextColumn
            {
                Header = headerColId,
                Binding = new Binding(PropiedadClave),
                Width = new DataGridLength(100)

            };
            grdDatos.Columns.Add(column);

            column = new DataGridTextColumn
            {
                Header = headerColDescripcion,
                Binding = new Binding(PropiedadDescripcion),
                Width = new DataGridLength(536)

            };
            grdDatos.Columns.Add(column);

            if (!String.IsNullOrWhiteSpace(PropiedadIdOcultoGrid))
            {
                column = new DataGridTextColumn
                {
                    Binding = new Binding(PropiedadIdOcultoGrid),
                    Visibility = Visibility.Hidden
                };
                grdDatos.Columns.Add(column);
            }
            var propiedades = Info.GetType().GetProperties();
            foreach (var propertyInfo in propiedades)
            {
                if (string.CompareOrdinal(propertyInfo.Name, PropiedadClave) != 0
                       && string.CompareOrdinal(propertyInfo.Name, PropiedadDescripcion) != 0
                       && string.CompareOrdinal(propertyInfo.Name, PropiedadIdOcultoGrid) != 0)
                {
                    column = new DataGridTextColumn
                    {
                        Binding = new Binding(propertyInfo.Name),
                        Visibility = Visibility.Hidden
                    };
                    grdDatos.Columns.Add(column);
                }
            }
        }

        /// <summary>
        /// Genera el Grid en el Cual Se
        /// Mostraran los Controles para Filtrar
        /// </summary>
        /// <returns></returns>
        private Grid GenerarGridFiltros()
        {
            var grdFiltros = new Grid();

            var colLabel = new ColumnDefinition {Width = new GridLength(90)};
            grdFiltros.ColumnDefinitions.Add(colLabel);

            var colTextBox = new ColumnDefinition {Width = new GridLength(300)};
            grdFiltros.ColumnDefinitions.Add(colTextBox);

            var colBoton = new ColumnDefinition {Width = new GridLength(70)};
            grdFiltros.ColumnDefinitions.Add(colBoton);

            var colAgregar = new ColumnDefinition {Width = new GridLength(70)};
            grdFiltros.ColumnDefinitions.Add(colAgregar);

            var colCancelar = new ColumnDefinition {Width = new GridLength(70)};
            grdFiltros.ColumnDefinitions.Add(colCancelar);

            grdFiltros.RowDefinitions.Add(new RowDefinition());

            TextBox valorFiltro = GeneraTextBoxValorFiltro();
            Grid.SetColumn(valorFiltro, 1);
            Grid.SetRow(valorFiltro, 0);

            Button btnBuscar = GeneraBotonBusqueda();
            Grid.SetColumn(btnBuscar, 2);
            Grid.SetRow(btnBuscar, 0);

            Button btnAgregar = GeneraBotonAgregar();
            Grid.SetColumn(btnAgregar, 3);
            Grid.SetRow(btnAgregar, 0);

            Button btnCancelar = GeneraBotonCancelar();
            Grid.SetColumn(btnCancelar, 4);
            Grid.SetRow(btnCancelar, 0);

            Label filtro = GeneraLblFiltro();
            Grid.SetColumn(filtro, 0);
            Grid.SetRow(filtro, 0);

            grdFiltros.Children.Add(filtro);
            grdFiltros.Children.Add(valorFiltro);
            grdFiltros.Children.Add(btnBuscar);
            grdFiltros.Children.Add(btnAgregar);
            grdFiltros.Children.Add(btnCancelar);

            return grdFiltros;
        }

        /// <summary>
        /// Genera el Boton de Busqueda
        /// </summary>
        /// <returns></returns>
        private Button GeneraBotonBusqueda()
        {
            var btnBuscar = new Button {Width = 60, Height = 23, Content = SuKarne.Controls.Properties.Resources.Ayuda_Buscar};
            btnBuscar.Click += btnBuscar_Click;

            return btnBuscar;
        }

        /// <summary>
        /// Genera el Boton para Agregar
        /// </summary>
        /// <returns></returns>
        private Button GeneraBotonAgregar()
        {
            var btnAgregar = new Button {Width = 60, Height = 23, Content = SuKarne.Controls.Properties.Resources.Ayuda_Agregar};
            btnAgregar.Click += btnAgregar_Click;

            return btnAgregar;
        }

        /// <summary>
        /// Genera el Boton Cancelar
        /// </summary>
        /// <returns></returns>
        private Button GeneraBotonCancelar()
        {
            var btnCancelar = new Button {Width = 60, Height = 23, Content = SuKarne.Controls.Properties.Resources.Ayuda_Cancelar};
            btnCancelar.Click += btnCancelar_Click;

            return btnCancelar;
        }

        /// <summary>
        /// Genera la Caja de Texto en la Cual
        /// Se Proporciona el Valor para Filtrar
        /// </summary>
        /// <returns></returns>
        private TextBox GeneraTextBoxValorFiltro()
        {
            txtValorFiltro = new TextBox();
            txtValorFiltro.KeyUp += txtValorFiltro_KeyUp;
            txtValorFiltro.PreviewTextInput += TxtValorFiltroOnPreviewTextInput;
            txtValorFiltro.CharacterCasing = CharacterCasing.Upper;
            txtValorFiltro.Width = 300;
            txtValorFiltro.Height = 23;
            txtValorFiltro.MaxLength = 50;
            return txtValorFiltro;
        }

        /// <summary>
        /// Genera la Etiqueta Para la Descripcion del Filtro
        /// </summary>
        /// <returns></returns>
        private Label GeneraLblFiltro()
        {
            lblFiltro = new Label {Width = 85, Height = 28, Content = TituloEtiqueta};
            return lblFiltro;
        }

        /// <summary>
        /// Asigna los Valores Consultados al Grid
        /// </summary>
        /// <param name="resultados"></param>
        private void AsignarValoresGridCamposInfo(IList<T> resultados)
        {
            var valoresAyuda = new List<SKCamposAyudaInfo>();

            for (int indexResultado = 0; indexResultado < resultados.Count; indexResultado++)
            {
                if (resultados[indexResultado] != null)
                {
                    if (CamposInfo != null)
                    {
                        try
                        {
                            SKCamposAyudaInfo valorAyuda = new SKCamposAyudaInfo();
                            for (int indexCamposInfo = 0; indexCamposInfo < CamposInfo.Count; indexCamposInfo++)
                            {
                                object valorInfo = resultados[indexResultado].GetType()
                                    .GetProperty(CamposInfo[indexCamposInfo])
                                    .GetValue(resultados[indexResultado], null);
                                if (AyudaValidador.EsValorNumerico(Convert.ToString(valorInfo)))
                                {
                                    valorAyuda.Clave = Convert.ToString(valorInfo);
                                }
                                else
                                {
                                    valorAyuda.Descripcion = Convert.ToString(valorInfo);
                                }
                            }
                            valoresAyuda.Add(valorAyuda);
                        }
                        catch (ExcepcionGenerica)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                        }
                    }
                }
            }
            grdDatos.ItemsSource = valoresAyuda;
        }
       

        /// <summary>
        /// Asiga los Valores que se han Seleccionado
        /// en el Grid
        /// </summary>
        /// <returns></returns>
        private bool AsignarValoresSeleccionados()
        {
            var elementoSeleccionado = false;
            var renglonSeleccionado = (T)grdDatos.SelectedItem;
            if (renglonSeleccionado != null)
            {
                elementoSeleccionado = true;
                AuxAyuda.AsignaValoresInfo(Info, renglonSeleccionado);
                AsignaValorPropiedadesControl(renglonSeleccionado);
            }
            return elementoSeleccionado;
        }

        /// <summary>
        /// Obtiene los Valores para Mostrarlos en el
        /// Grid de Ayuda
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        public void ObtenerValoresAyuda(int inicio, int limite)
        {
            var tiposParametros = new List<Type>();
            var valoresParametros = new List<Object>();

            try
            {
                if (grdDatos.Items.Count > 0)
                {
                    grdDatos.ItemsSource = null;
                    grdDatos.Items.Clear();
                }
                var filtro = txtValorFiltro.Text;
                if (string.Compare(filtro.Trim(), descripcionAnterior.Trim(), StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    descripcionAnterior = filtro;
                    pagControl.Inicio = 1;
                    inicio = 1;
                }
                var metodoInvocacion = AuxAyuda.ObtenerMetodoEjecutar(Info, PropiedadDescripcion, true,
                                                                      AtributoDescripcion);
                var paginacionInfo = new PaginacionInfo {Inicio = inicio, Limite = limite};
                AuxAyuda.AgregarFiltros(false, paginacionInfo, tiposParametros, valoresParametros);

                try
                {
                    Info.GetType().GetProperty(PropiedadDescripcion).SetValue(Info, filtro, null);
                }
                catch (Exception)
                {
                    Info.GetType().GetProperty(PropiedadDescripcion).SetValue(Info, 0, null);
                }
                

                AuxAyuda.AgregarFiltros(false, Info, tiposParametros, valoresParametros);

                object resultadoInvocacion = null;
                var ayudaTipo = AyudaPL.GetType();
                MethodInfo metodo;
                if (Dependencias == null)
                {
                    metodo = ayudaTipo.GetMethod(metodoInvocacion, tiposParametros.ToArray());
                    if (metodo != null)
                    {
                        resultadoInvocacion = metodo.Invoke(AyudaPL, valoresParametros.ToArray());
                    }
                }
                else
                {
                    AuxAyuda.AgregarFiltros(false, Dependencias, tiposParametros, valoresParametros);
                    metodo = ayudaTipo.GetMethod(metodoInvocacion, tiposParametros.ToArray());
                    if (metodo != null)
                    {
                        resultadoInvocacion = metodo.Invoke(AyudaPL, valoresParametros.ToArray());
                    }
                }
                var resultadoInfo = (ResultadoInfo<T>) resultadoInvocacion;
                if (resultadoInfo != null && resultadoInfo.Lista != null && resultadoInfo.Lista.Count > 0)
                {
                    pagControl.TotalRegistros = resultadoInfo.TotalRegistros;
                    grdDatos.ItemsSource = resultadoInfo.Lista;
                }
                else
                {
                    pagControl.TotalRegistros = 0;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, SuKarne.Controls.Properties.Resources.Ayuda_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, SuKarne.Controls.Properties.Resources.Ayuda_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los Valores para Mostrarlos en el
        /// Grid de Ayuda
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        public void ObtenerValoresAyudaCamposInfo(int inicio, int limite)
        {
            var tiposParametros = new List<Type>();
            var valoresParametros = new List<Object>();

            try
            {
                if (grdDatos.Items.Count > 0)
                {
                    grdDatos.ItemsSource = null;
                    grdDatos.Items.Clear();
                }

                var filtro = txtValorFiltro.Text;
                var metodoInvocacion = MetodoPaginado;
                var paginacionInfo = new PaginacionInfo { Inicio = inicio, Limite = limite };
                AuxAyuda.AgregarFiltros(false, paginacionInfo, tiposParametros, valoresParametros);

                var propiedad = String.Empty;
                for (int indexCamposInfo = 0; indexCamposInfo < CamposInfo.Count; indexCamposInfo++)
                {
                    var indice = CamposInfo[indexCamposInfo].IndexOf("ID");

                    if (indice < 0)
                    {
                        propiedad = CamposInfo[indexCamposInfo];
                    }
                }

                Info.GetType().GetProperty(propiedad).SetValue(Info, filtro, null);
                AuxAyuda.AgregarFiltros(false, Info, tiposParametros, valoresParametros);

                object resultadoInvocacion = null;
                var ayudaTipo = AyudaPL.GetType();
                MethodInfo metodo;
                if (Dependencias == null)
                {
                    metodo = ayudaTipo.GetMethod(metodoInvocacion, tiposParametros.ToArray());
                    if (metodo != null)
                    {
                        resultadoInvocacion = metodo.Invoke(AyudaPL, valoresParametros.ToArray());
                    }
                }
                else
                {
                    AuxAyuda.AgregarFiltros(false, Dependencias, tiposParametros, valoresParametros);
                    metodo = ayudaTipo.GetMethod(metodoInvocacion, tiposParametros.ToArray());
                    if (metodo != null)
                    {
                        resultadoInvocacion = metodo.Invoke(AyudaPL, valoresParametros.ToArray());
                    }
                }
                var resultadoInfo = (ResultadoInfo<T>)resultadoInvocacion;
                if (resultadoInfo != null && resultadoInfo.Lista != null && resultadoInfo.Lista.Count > 0)
                {
                    pagControl.TotalRegistros = resultadoInfo.TotalRegistros;
                    AsignarValoresGridCamposInfo(resultadoInfo.Lista);
                }
                else
                {
                    pagControl.TotalRegistros = 0;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, SuKarne.Controls.Properties.Resources.Ayuda_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, SuKarne.Controls.Properties.Resources.Ayuda_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Asiga los Valores que se han Seleccionado
        /// en el Grid
        /// </summary>
        /// <returns></returns>
        private bool AsignarValoresSeleccionadosCamposInfo()
        {
            bool elementoSeleccionado = false;
            var renglonSeleccionado = grdDatos.SelectedItem as SKCamposAyudaInfo;
            if (renglonSeleccionado != null)
            {
                elementoSeleccionado = true;
                Descripcion = renglonSeleccionado.Descripcion;
                Clave = renglonSeleccionado.Clave;                
            }
            return elementoSeleccionado;
        }

        private void AsignarValoresSeleccionadosGridCamposInfo()
        {
            var renglonSeleccionado = grdDatos.SelectedItem as SKCamposAyudaInfo;
            if (renglonSeleccionado != null)
            {
                Descripcion = renglonSeleccionado.Descripcion;
                Clave = renglonSeleccionado.Clave;
                confirmaSalir = false;
                Close();
            }
        }    

        /// <summary>
        /// Asigna los valores del renglon seleccionado
        /// </summary>
        private void AsignarValoresSeleccionadosGrid()
        {
            var renglonSeleccionado = (T)grdDatos.SelectedItem;
            if (renglonSeleccionado != null)
            {
                AuxAyuda.AsignaValoresInfo(Info, renglonSeleccionado);
                AsignaValorPropiedadesControl(renglonSeleccionado);
                confirmaSalir = false;
                Close();
            }
        }

        /// <summary>
        /// Asigna valores a las propiedades
        /// </summary>
        /// <param name="renglonSeleccionado"></param>
        private void AsignaValorPropiedadesControl(T renglonSeleccionado)
        {
            Descripcion = Convert.ToString(renglonSeleccionado.GetType().GetProperty(PropiedadDescripcion).GetValue(renglonSeleccionado,
                                                                                    null));
            Clave = Convert.ToString(renglonSeleccionado.GetType().GetProperty(PropiedadClave).GetValue(renglonSeleccionado,
                                                                                             null));
            if (!String.IsNullOrWhiteSpace(PropiedadIdOcultoGrid))
            {
                Id =
                    Convert.ToString(
                        renglonSeleccionado.GetType().GetProperty(PropiedadIdOcultoGrid).GetValue(renglonSeleccionado,
                                                                                                  null));
            }
        }

        /// <summary>
        /// Inicializa las Propiedades del Info que
        /// se utiliza para la Busqueda
        /// </summary>
        public static void InicializaPropiedadesInfoBusquedaCamposInfo(T info, IList<string> camposInfo)
        {
            foreach (var campo in camposInfo)
            {
                var propertyInfo = info.GetType().GetProperty(campo);
                switch (propertyInfo.PropertyType.FullName)
                {
                    case "System.Int32":
                        propertyInfo.SetValue(info, 0, null);
                        break;
                    case "System.String":
                        propertyInfo.SetValue(info, String.Empty, null);
                        break;
                }
            }
        }

        /// <summary>
        /// Inicializa las Propiedades del Info que
        /// se utiliza para la Busqueda
        /// </summary>
        public static void InicializaPropiedadesAnterioresCamposInfo(T info, IEnumerable<string> camposInfo)
        {
            ValoresAnteriores.Clear();
            foreach (var campo in camposInfo)
            {
                var propertyInfo = info.GetType().GetProperty(campo);
                ValoresAnteriores.Add(propertyInfo.Name, info.GetType().GetProperty(propertyInfo.Name).GetValue(info, null));
            }
        }

        /// <summary>
        /// Inicializa las Propiedades del Info que
        /// se utiliza para la Busqueda
        /// </summary>
        public static void InicializaPropiedadesAnteriores(T info, string propiedadClave, string propiedadDescripcion
                                                            , string propiedadIdOcultoGrid)
        {
            ValoresAnteriores.Clear();
            ValoresAnteriores.Add(propiedadClave, info.GetType().GetProperty(propiedadClave).GetValue(info, null));
            ValoresAnteriores.Add(propiedadDescripcion, info.GetType().GetProperty(propiedadDescripcion).GetValue(info, null));

            if (!String.IsNullOrWhiteSpace(propiedadIdOcultoGrid))
            {
                ValoresAnteriores.Add(propiedadIdOcultoGrid, info.GetType().GetProperty(propiedadIdOcultoGrid).GetValue(info, null));
            }
        }

        /// <summary>
        /// Inicializa las Propiedades del Info que
        /// se utiliza para la Busqueda
        /// </summary>
        public static void InicializaPropiedadesInfoBusqueda(T info, string propiedadClave, string propiedadDescripcion
                                                            , string propiedadIdOcultoGrid)
        {
            try
            {
                info.GetType().GetProperty(propiedadClave).SetValue(info, 0, null);
            }
            catch (Exception)
            {
                info.GetType().GetProperty(propiedadClave).SetValue(info, String.Empty, null);
            }

            try
            {
                info.GetType().GetProperty(propiedadDescripcion).SetValue(info, String.Empty, null);
            }
            catch (Exception)
            {
                info.GetType().GetProperty(propiedadDescripcion).SetValue(info, 0, null);
            }
            
            
            if (!String.IsNullOrWhiteSpace(propiedadIdOcultoGrid))
            {
                info.GetType().GetProperty(propiedadIdOcultoGrid).SetValue(info, 0, null);
            }
        }

        /// <summary>
        /// Asigna los valores anteriores al info
        /// antes de ser llamado y limpiar los valores
        /// </summary>
        /// <param name="info"></param>
        private void AsignaValoresAnteriores(T info)
        {
            foreach (var llave in ValoresAnteriores.Keys)
            {
                var valor = ValoresAnteriores[llave];
                info.GetType().GetProperty(llave).SetValue(info, valor, null);
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            bool elementoSeleccionando;
            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                elementoSeleccionando = AsignarValoresSeleccionados();
            }
            else
            {
                elementoSeleccionando = AsignarValoresSeleccionadosCamposInfo();
            }
            if (elementoSeleccionando)
            {
                confirmaSalir = false;
                Close();
            }
            else
            {
                SkMessageBox.Show(this, MensajeAgregar, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            confirmaSalir = true;
            Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                InicializaPropiedadesInfoBusqueda(Info, PropiedadClave, PropiedadDescripcion, PropiedadIdOcultoGrid);
                ObtenerValoresAyuda(pagControl.Inicio, pagControl.Limite);
            }
            else
            {
                InicializaPropiedadesInfoBusquedaCamposInfo(Info, CamposInfo);
                ObtenerValoresAyudaCamposInfo(pagControl.Inicio, pagControl.Limite);
            }
        }

        private void txtValorFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CamposInfo == null || CamposInfo.Count == 0)
                {
                    ObtenerValoresAyuda(pagControl.Inicio, pagControl.Limite);
                }
                else
                {
                    ObtenerValoresAyudaCamposInfo(pagControl.Inicio, pagControl.Limite);
                }
            }
        }

        private void SKAyudaBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                confirmaSalir = true;
                Close();
            }
        }       

        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, MensajeCerrar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AsignaValoresAnteriores(Info);
                    Cancelado = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        void grdDatos_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.SystemKey == Key.Enter)
            {
                if (CamposInfo == null || CamposInfo.Count == 0)
                {
                    AsignarValoresSeleccionadosGrid();
                }
                else
                {
                    AsignarValoresSeleccionadosGridCamposInfo();
                }
            }
        }

        void grdDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                AsignarValoresSeleccionadosGrid();   
            }
            else
            {
                AsignarValoresSeleccionadosGridCamposInfo();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (txtValorFiltro != null)
            {
                txtValorFiltro.Focus();
            }
        }

        private void TxtValorFiltroOnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (TipoCampoCodigo)
            {
                case TipoCampo.NoEspecificado:
                    break;
                case TipoCampo.NumeroYletras:
                    e.Handled = Extensor.ValidarNumeroYletras(e.Text);
                    break;
                case TipoCampo.Numeros:
                    e.Handled = Extensor.ValidarNumeros(e.Text);
                    break;
                case TipoCampo.LetrasConAcentos:
                    e.Handled = Extensor.ValidarLetrasConAcentos(e.Text);
                    break;
                case TipoCampo.NumerosLetrasConAcentos:
                    e.Handled = Extensor.ValidarNumerosLetrasConAcentos(e.Text);
                    break;
                case TipoCampo.NumerosLetrasSinAcentos:
                    e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
                    break;
                case TipoCampo.SoloLetrasYNumerosConGuion:
                    e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuion(e.Text);
                    break;
                case TipoCampo.SoloLetrasYNumerosConGuionParentesis:
                    e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuionParentesis(e.Text);
                    break;
                case TipoCampo.SoloLetrasYNumerosConPunto:
                    e.Handled = Extensor.ValidarSoloLetrasYNumerosConPunto(e.Text);
                    break;
                case TipoCampo.SoloNumerosConPunto:
                    e.Handled = Extensor.ValidarSoloNumerosConPunto(e.Text);
                    break;
                case TipoCampo.LetraNumeroPuntoComaGuion:
                    e.Handled = !Extensor.ValidarLetraNumeroPuntoComaGuion(e.Text);
                    break;
            }
        }

        #endregion EVENTOS
    }
}
