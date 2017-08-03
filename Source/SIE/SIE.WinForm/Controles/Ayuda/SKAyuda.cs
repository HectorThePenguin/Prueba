using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Validadores;
using SIE.Services.Info.Enums;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Windows.Media.Imaging;

namespace SIE.WinForm.Controles.Ayuda
{
    public delegate void ObtenerDatosDelegado(string filtro);
    public delegate void AyudaDelegado();

    public class SKAyuda<T> : UserControl
    {
        public AyudaDelegado LlamadaMetodos;
        public AyudaDelegado LlamadaMetodosNoExistenDatos;
        public ObtenerDatosDelegado ObtenerDatos;
        public event EventHandler AyudaLimpia;
        public event EventHandler AyudaConDatos;
        private TipoCampo tipoCampoCodigo = TipoCampo.NumerosLetrasSinAcentos;

        #region VARIABLES
        

        private bool existenDatos;
        private Button btnBuscar;
        private TextBox txtDescripcion;
        private TextBox txtId;
        private TextBox txtOculto;
        private bool ventanaBusqueda;
        private bool campoNumerico;
        private bool mostrarIconoBuscar = true;

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// Ayuda en la cual se Generara la Busqueda
        /// </summary>
        public Object AyudaPL { get; set; }

        /// <summary>
        /// Clase info que se estara Consultando
        /// </summary>
        public T Info { get; set; }

        /// <summary>
        /// Coleccion que contiene las Dependencias
        /// </summary>
        public IList<IDictionary<IList<string>, object>> Dependencias { get; set; }

        /// <summary>
        /// Campos de los cuales se obtendran los datos
        /// a mostrar en Ayuda
        /// </summary>
        public IList<string> CamposInfo { get; set; }

        /// <summary>
        /// Ruta a la cual se accedera el Id
        /// </summary>
        public string PropertyPathId { get; private set; }

        /// <summary>
        /// Ruta a la cual se accedera la Descripcion
        /// </summary>
        public string PropertyPathDescripcion { get; private set; }

        public string PropertyHidden { get; set; }

        /// <summary>
        /// Clave del Catalogo
        /// </summary>
        public string Clave
        {
            get { return txtId.Text; }
            set { txtId.Text = value; }
        }

        /// <summary>
        /// Valor de Descripcion del Catalogo
        /// </summary>
        public string Descripcion
        {
            get { return txtDescripcion.Text; }
            set { txtDescripcion.Text = value; }
        }

        /// <summary>
        /// Identificador Unico del Catalogo
        /// </summary>
        public  string Id
        {
            get { return txtOculto.Text; }
            set { txtOculto.Text = value; }
        }

        /// <summary>
        /// Indica si se utilizara uno o dos controles de texto
        /// true - Se mostrara solo un control de texto
        /// false - Se mostraran ambos controles de texto
        /// </summary>
        public bool AyudaSimple { get; set; }

        /// <summary>
        /// Largo del Campo Descripcion en Caso de que el Campo ID no se Muestre
        /// , como cuando se utiliza una ayuda simple
        /// </summary>
        public int LargoCampoDescripcion { private get; set; }

        /// <summary>
        /// Largo del Campo Descripcion en Caso de que el Campo ID no se Muestre
        /// , como cuando se utiliza una ayuda simple
        /// </summary>
        public int LargoCampoID { private get; set; }

        /// <summary>
        /// Largo del Campo Descripcion en Caso de que el Campo ID no se Muestre
        /// , como cuando se utiliza una ayuda simple
        /// </summary>
        public int MaxLengthCampoID { private get; set; }

        /// <summary>
        /// Mensaje que se Mostrara Cuando la Clave Ingresada No Exista
        /// </summary>
        public string MensajeClaveInexistente { get; set; }

        /// <summary>
        /// Mensaje que Mostrara Cuando se Quiera Cerrar la Ventana
        /// </summary>
        public string MensajeBusquedaCerrar { private get; set; }

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
        /// Lista de Catalogos con los cuales Tendra Dependencia la Ayuda
        /// </summary>
        public IDictionary<string, string> MensajeDependencias { get; set; }

        /// <summary>
        /// Metodo que se Llamara cuando se
        /// requiera consultar por Clave
        /// </summary>
        public String MetodoPorId { get; set; }

        /// <summary>
        /// Metodo que se Llamaara cuando se
        /// requiera consultar por Descripcion
        /// </summary>
        public String MetodoPorDescripcion { get; set; }

        /// <summary>
        /// Metodo que se Llamaara cuando se
        /// requiera consultar por Paginacion
        /// </summary>
        public String MetodoPaginadoBusqueda { get; set; }

        /// <summary>
        /// Titulo que se Mostrara en
        /// la Busqueda de la Ayuda
        /// </summary>
        public String TituloPantalla { get; set; }

        /// <summary>
        /// Ruta a la cual se accedera el Bindeo del ID
        /// </summary>
        public string BindingID { get; set; }

        /// <summary>
        /// Ruta a la cual se accedera el Bindeo de la Descripción de la ayuda
        /// </summary>
        public string BindingDescripcion { get; set; }

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
        /// <summary>
        /// Indica que se binderan los campos
        /// </summary>
        public bool BindearCampos { get; set; }

        public Action BusquedaVacia { get; set; }

        public Func<bool> PuedeBuscar { get; set; }

        public string MensajeNoPuedeBuscar { get; set; }

        /// <summary>
        /// obtiene le ID del proveedor del control SplAyudaProveedor
        /// </summary>
        public int IdProveedor { get; set; }

        public TipoCampo TipoCampoCodigo
        {
            get { return tipoCampoCodigo; }
            set { tipoCampoCodigo = value; }
        }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        protected SKAyuda()
        {

        }

		public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion, bool aceptaSoloNumeros, int idproveedor)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            campoNumerico = aceptaSoloNumeros;
            IdProveedor = idproveedor;

            AgregarControles();
        }
		
        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, bool mostrarLupa, T info, string atributoClave, string atributoDescripcion, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            campoNumerico = aceptaSoloNumeros;
            mostrarIconoBuscar = mostrarLupa;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , bool bindeo, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            BindearCampos = bindeo;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , bool bindeo, int largoCampoId, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            BindearCampos = bindeo;
            LargoCampoID = largoCampoId;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , bool bindeo, int largoCampoId,int maxLengthCampoId, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            BindearCampos = bindeo;
            LargoCampoID = largoCampoId;
            campoNumerico = aceptaSoloNumeros;
            MaxLengthCampoID = maxLengthCampoId;
            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , int largoCampoId, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            LargoCampoID = largoCampoId;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , string atributoIdOcultoGrid, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;            
            AtributoIdOcultoGrid = atributoIdOcultoGrid;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , string atributoIdOcultoGrid, bool bindeo, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            AtributoIdOcultoGrid = atributoIdOcultoGrid;
            BindearCampos = bindeo;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(int largoCampoDescripcion, bool ayudaSimple, T info, string atributoClave, string atributoDescripcion
                     , string atributoIdOcultoGrid, bool bindeo, int largoCampoID, int maxLenthCampoID, bool aceptaSoloNumeros)
        {
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            LargoCampoID = largoCampoID;
            MaxLengthCampoID = maxLenthCampoID;
            Info = info;
            AtributoClave = atributoClave;
            AtributoDescripcion = atributoDescripcion;
            AtributoIdOcultoGrid = atributoIdOcultoGrid;
            BindearCampos = bindeo;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }


        public SKAyuda(string propertyPathId, string propertyPathDescripcion, int largoCampoDescripcion,
                       bool ayudaSimple, bool aceptaSoloNumeros)
        {
            PropertyPathId = propertyPathId;
            PropertyPathDescripcion = propertyPathDescripcion;
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(string propertyPathId, string propertyPathDescripcion, int largoCampoDescripcion,
                     bool ayudaSimple, string bindingID, string bindingDescripcion, bool aceptaSoloNumeros)
        {
            PropertyPathId = propertyPathId;
            PropertyPathDescripcion = propertyPathDescripcion;
            BindingID = bindingID;
            BindingDescripcion = bindingDescripcion;
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            campoNumerico = aceptaSoloNumeros;
            
            AgregarControles();
        }

        public SKAyuda(string propertyPathId, string propertyPathDescripcion, int largoCampoDescripcion,
                      bool ayudaSimple, string bindingID, string bindingDescripcion, int largoCampoID, int maxLengthCampoId
                     , bool aceptaSoloNumeros)
        {
            PropertyPathId = propertyPathId;
            PropertyPathDescripcion = propertyPathDescripcion;
            BindingID = bindingID;
            BindingDescripcion = bindingDescripcion;
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            LargoCampoID = largoCampoID;
            MaxLengthCampoID = maxLengthCampoId;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(Object ayuda, string propertyPathId, string propertyPathDescripcion, int largoCampoDescripcion,
                       bool ayudaSimple, bool aceptaSoloNumeros)
        {
            AyudaPL = ayuda;
            PropertyPathId = propertyPathId;
            PropertyPathDescripcion = propertyPathDescripcion;
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(IList<string> camposInfo, string propertyPathId, string propertyPathDescripcion,
                       int largoCampoDescripcion, bool ayudaSimple, bool aceptaSoloNumeros)
        {
            CamposInfo = camposInfo;
            PropertyPathId = propertyPathId;
            PropertyPathDescripcion = propertyPathDescripcion;
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        public SKAyuda(Object ayuda, IList<string> camposInfo, string propertyPathId, string propertyPathDescripcion,
                       int largoCampoDescripcion, bool ayudaSimple, bool aceptaSoloNumeros)
        {
            AyudaPL = ayuda;
            CamposInfo = camposInfo;
            PropertyPathId = propertyPathId;
            PropertyPathDescripcion = propertyPathDescripcion;
            AyudaSimple = ayudaSimple;
            LargoCampoDescripcion = largoCampoDescripcion;
            campoNumerico = aceptaSoloNumeros;

            AgregarControles();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        /// <summary>
        /// Limpia los Controles del Control de Ayuda
        /// </summary>
        public void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
            AuxAyuda.InicializaPropiedad(Info);
        }

        /// <summary>
        /// Agrega los Controles Que Seran Mostrados
        /// </summary>
        protected void AgregarControles()
        {
            var grd = new Grid();
            grd.RowDefinitions.Add(new RowDefinition());

            var colTextId = new ColumnDefinition();
            var colTextDescripcion = new ColumnDefinition();
            var colBoton = new ColumnDefinition();

            txtId = GenerarTextBoxId();
            Grid.SetRow(txtId, 0);
            Grid.SetColumn(txtId, 0);

            txtOculto = GeneraTextBoxOculto();
            Grid.SetRow(txtOculto, 0);
            Grid.SetColumn(txtOculto, 0);

            txtDescripcion = GenerarTextBoxDescripcion();
            btnBuscar = GenerarBotonAyuda();                

            if (!mostrarIconoBuscar)
            {
                btnBuscar.Visibility = Visibility.Hidden;
            }

            if (AyudaSimple)
            {
                colTextId.Width = new GridLength(1);
            }
            else
            {
                colTextId.Width = LargoCampoID == 0 ? new GridLength(60) : new GridLength(LargoCampoID + 5);
            }

            Grid.SetRow(txtDescripcion, 1);
            Grid.SetColumn(txtDescripcion, 1);
            Grid.SetRow(btnBuscar, 2);
            Grid.SetColumn(btnBuscar, 2);

            if (LargoCampoDescripcion < 100)
            {
                LargoCampoDescripcion += 5;
            }
            colTextDescripcion.Width = new GridLength(LargoCampoDescripcion + 5);
            colBoton.Width = new GridLength(40);

            grd.ColumnDefinitions.Add(colTextId);
            grd.ColumnDefinitions.Add(colTextDescripcion);
            grd.ColumnDefinitions.Add(colBoton);

            grd.Children.Add(txtId);
            grd.Children.Add(txtOculto);
            grd.Children.Add(txtDescripcion);
            grd.Children.Add(btnBuscar);

            AddChild(grd);
        }

        /// <summary>
        /// Genera un Control Oculto
        /// </summary>
        /// <returns></returns>
        private TextBox GeneraTextBoxOculto()
        {
            var txthidden = new TextBox { Visibility = Visibility.Hidden, Width = 0 };
            if (!String.IsNullOrWhiteSpace(AtributoIdOcultoGrid))
            {
                PropertyHidden = AuxAyuda.ObtenerPropiedadBindeo(Info, AtributoIdOcultoGrid);
            }

            if (!String.IsNullOrWhiteSpace(PropertyHidden))
            {
                Binding bindOculto;
                var esContenedor = AuxAyuda.EstaEnContendor(Info, PropertyHidden, AtributoIdOcultoGrid);
                if (esContenedor)
                {
                    var contenedor = AuxAyuda.ObtenerContenedor(Info, PropertyHidden, AtributoIdOcultoGrid);
                    bindOculto = new Binding(string.Concat(contenedor, ".", PropertyHidden))
                                     {
                                         Mode = BindingMode.Default,
                                         UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                     };
                }
                else
                {
                    bindOculto = new Binding(PropertyHidden)
                                     {
                                         Mode = BindingMode.Default,
                                         UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                     };
                }
                bindOculto.TargetNullValue = 0;
                txthidden.SetBinding(TextBox.TextProperty, bindOculto);
            }
            return txthidden;
        }

        /// <summary>
        /// Genera el Control para la Clave
        /// </summary>
        /// <returns></returns>
        private TextBox GenerarTextBoxId()
        {
            var textBoxId = new TextBox();

            if (AyudaSimple)
            {
                textBoxId.Visibility = Visibility.Hidden;
            }
            else
            {
                textBoxId.Width = 50;
                if (MaxLengthCampoID != 0)
                {
                    textBoxId.MaxLength = MaxLengthCampoID;
                }
                else
                {
                    textBoxId.MaxLength = 9;
                }
            }

            if (campoNumerico)
            {
                textBoxId.PreviewTextInput += Text_Numeros;
            }
            else
            {
                textBoxId.PreviewTextInput += Text_NumerosLetrasGuiones;
            }
            textBoxId.CharacterCasing = CharacterCasing.Upper;

            if (LargoCampoID != 0)
            {
                textBoxId.Width = LargoCampoID;
            }
            textBoxId.TextChanged += txtId_TextChanged;
            textBoxId.LostFocus += txtId_LostFocus;
            textBoxId.KeyDown += txtId_KeyDown;

            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                if (!string.IsNullOrWhiteSpace(AtributoClave))
                {
                    PropertyPathId = AuxAyuda.ObtenerPropiedadBindeo(Info, AtributoClave);
                }
            }
            if (BindingID != null)
            {
                var bind = new Binding(BindingID)
                               {Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged};
                textBoxId.SetBinding(TextBox.TextProperty, bind);
            }
            else
            {
                if (BindearCampos)
                {
                    if (CamposInfo == null || CamposInfo.Count == 0)
                    {
                        if (!String.IsNullOrWhiteSpace(PropertyPathId))
                        {
                            var esContenedor = AuxAyuda.EstaEnContendor(Info, PropertyPathId, AtributoClave);
                            Binding bind;
                            if (esContenedor)
                            {
                                var contenedor = AuxAyuda.ObtenerContenedor(Info, PropertyPathId, AtributoClave);
                                bind = new Binding(string.Concat(contenedor, ".", PropertyPathId))
                                           {
                                               Mode = BindingMode.Default,
                                               UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                           };
                            }
                            else
                            {
                                bind = new Binding(PropertyPathId)
                                           {
                                               Mode = BindingMode.Default,
                                               UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                           };
                            }
                            bind.TargetNullValue = string.Empty;
                            textBoxId.SetBinding(TextBox.TextProperty, bind);
                        }
                    }
                }
            }

            return textBoxId;
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            BuscarValoresAyuda(e, txtId, txtDescripcion);
        }

        /// <summary>
        /// Genera el Control para la Descripcion
        /// </summary>
        /// <returns></returns>
        private TextBox GenerarTextBoxDescripcion()
        {
            var textBoxDescripcion = new TextBox
                {
                    Width = LargoCampoDescripcion,
                    IsReadOnly = true                    
                };
            if (AyudaSimple)
            {
                textBoxDescripcion.IsReadOnly = false;                
                textBoxDescripcion.TextChanged += txtDescripcion_TextChanged;
                textBoxDescripcion.LostFocus += txtDescripcion_LostFocus;
                textBoxDescripcion.KeyDown += txtDescripcion_KeyDown;
                textBoxDescripcion.MaxLength = 9;

                if (campoNumerico)
                {
                    textBoxDescripcion.PreviewTextInput += Text_Numeros;
                }
                else
                {
                    textBoxDescripcion.PreviewTextInput += Text_NumerosLetrasGuiones;
                }
            }
            else
            {
                textBoxDescripcion.IsTabStop = false;
            }

            textBoxDescripcion.CharacterCasing = CharacterCasing.Upper;

            if (CamposInfo == null || CamposInfo.Count == 0)
            {
                if (!string.IsNullOrWhiteSpace(AtributoDescripcion))
                {
                    PropertyPathDescripcion = AuxAyuda.ObtenerPropiedadBindeo(Info, AtributoDescripcion);
                }                
            }
            if (BindingDescripcion != null)
            {
                var bind = new Binding(BindingDescripcion)
                               {Mode = BindingMode.Default, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged};
                textBoxDescripcion.SetBinding(TextBox.TextProperty, bind);
            }
            else
            {
                if (BindearCampos)
                {
                    if (CamposInfo == null || CamposInfo.Count == 0)
                    {
                        if (!String.IsNullOrWhiteSpace(PropertyPathDescripcion))
                        {
                            var esContenedor = AuxAyuda.EstaEnContendor(Info, PropertyPathDescripcion,
                                                                        AtributoDescripcion);
                            Binding bind = null;
                            if (esContenedor)
                            {
                                var contenedor = AuxAyuda.ObtenerContenedor(Info, PropertyPathDescripcion,
                                                                            AtributoDescripcion);
                                if (!String.IsNullOrWhiteSpace(PropertyPathDescripcion))
                                {
                                    bind = new Binding(string.Concat(contenedor, ".", PropertyPathDescripcion))
                                                   {
                                                       Mode = BindingMode.Default,
                                                       UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                                   };
                                }
                            }
                            else
                            {
                                bind = new Binding(PropertyPathDescripcion)
                                {
                                    Mode = BindingMode.Default,
                                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                };
                            }
                            if (bind != null)
                            {
                                if (campoNumerico)
                                {
                                    bind.TargetNullValue = 0;
                                }
                                else
                                {
                                    bind.TargetNullValue = string.Empty;
                                }
                                textBoxDescripcion.SetBinding(TextBox.TextProperty, bind);   
                            }                            
                        }
                    }
                }
            }
            return textBoxDescripcion;
        }

        /// <summary>
        /// Genera el Control de Ayuda, Que Despliega
        /// una Ventana Emergente para Busqueda
        /// </summary>
        /// <returns></returns>
        private Button GenerarBotonAyuda()
        {
            btnBuscar = new Button {Style = (Style) Application.Current.FindResource("BuscarIcono")};
            btnBuscar.Click += BtnBuscarClick;
            var imgBuscar = new Image
            {
                Source = new BitmapImage(new Uri("../Imagenes/buscar.ico", UriKind.Relative)),                
            };
            btnBuscar.Content = imgBuscar;
            btnBuscar.IsTabStop = false;
            return btnBuscar;
        }
       

        /// <summary>
        /// Obtiene los Valores Por Filtro el Proporcionado
        /// </summary>
        /// <param name="valorFiltro"></param>
        private void ObtenerValoresAyudaCamposInfo(string valorFiltro)
        {
            existenDatos = false;
            var tiposParametros = new List<Type>();
            var valoresParametros = new List<Object>();
            try
            {
                SKAyudaBusqueda<T>.InicializaPropiedadesInfoBusquedaCamposInfo(Info, CamposInfo);
                bool esNumerico = AyudaValidador.EsValorNumerico(valorFiltro);

                string metodoInvocacion = esNumerico ? MetodoPorId : MetodoPorDescripcion;

                if (esNumerico)
                {
                    try
                    {
                        Info.GetType().GetProperty(PropertyPathId).SetValue(Info, Convert.ToInt32(valorFiltro), null);
                    }
                    catch (Exception)
                    {
                        Info.GetType().GetProperty(PropertyPathId).SetValue(Info, valorFiltro, null);
                    }                    
                }
                else
                {
                    Info.GetType().GetProperty(PropertyPathDescripcion).SetValue(Info, valorFiltro, null);
                }
                AuxAyuda.AgregarFiltros(false, Info, tiposParametros, valoresParametros);

                object resultadoInvocacion = null;
                Type ayudaTipo = AyudaPL.GetType();

                if (Dependencias != null)
                {
                    AuxAyuda.AgregarFiltros(false, Dependencias, tiposParametros, valoresParametros);
                }

                MethodInfo metodo = ayudaTipo.GetMethod(metodoInvocacion, tiposParametros.ToArray());
                if (metodo != null)
                {
                    resultadoInvocacion = metodo.Invoke(AyudaPL, valoresParametros.ToArray());
                }
                var resultado = (T)resultadoInvocacion;
                AsignaValoresCamposInfo(resultado);
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

        /// <summary>
        /// Asigna los Valores Obtenidos a los Controles
        /// </summary>
        /// <param name="resultado"></param>
        private void AsignaValoresCamposInfo(T resultado)
        {
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;            
            if (resultado != null)
            {
                if (CamposInfo != null && CamposInfo.Count > 0)
                {
                    existenDatos = true;
                    for (int indexCamposInfo = 0; indexCamposInfo < CamposInfo.Count; indexCamposInfo++)
                    {
                        object valorInfo =
                            resultado.GetType().GetProperty(CamposInfo[indexCamposInfo]).GetValue(resultado, null);
                        if (AyudaValidador.EsValorNumerico(Convert.ToString(valorInfo)))
                        {
                            txtId.Text = Convert.ToString(valorInfo);
                        }
                        else
                        {
                            txtDescripcion.Text = Convert.ToString(valorInfo);
                        }
                    }
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[1], MensajeClaveInexistente, MessageBoxButton.OK,
                                  MessageImage.Warning);                
            }
        }

        /// <summary>
        /// Ejecuta las validaciones para ver si puede realizar la busqueda de elementos.
        /// </summary>
        /// <returns></returns>
        private bool validarSiPuedeBuscar()
        {
            if (PuedeBuscar != null)
            {
                if (!PuedeBuscar())
                {
                    if (!string.IsNullOrWhiteSpace(MensajeNoPuedeBuscar))
                    {
                        SkMessageBox.Show(Application.Current.Windows[1], MensajeNoPuedeBuscar, MessageBoxButton.OK,
                                  MessageImage.Warning);
                    }
                    txtDescripcion.Clear();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Obtiene los Valores Por Filtro el Proporcionado
        /// </summary>
        /// <param name="valorFiltro"></param>
        private void ObtenerValoresAyuda(string valorFiltro)
        {
            if (validarSiPuedeBuscar())
            {

                existenDatos = false;
                var tiposParametros = new List<Type>();
                var valoresParametros = new List<Object>();
                try
                {
                    SKAyudaBusqueda<T>.InicializaPropiedadesInfoBusqueda(Info, PropertyPathId, PropertyPathDescripcion
                                                                       , PropertyHidden);
                    string metodoInvocacion;
                    if (AyudaSimple)
                    {
                        metodoInvocacion = AuxAyuda.ObtenerMetodoEjecutar(Info, PropertyPathDescripcion, false, AtributoDescripcion);
                    }
                    else
                    {
                        metodoInvocacion = AuxAyuda.ObtenerMetodoEjecutar(Info, PropertyPathId, false, AtributoClave);
                    }
                    try
                    {
                        if (AyudaSimple)
                        {
                            Info.GetType().GetProperty(PropertyPathDescripcion).SetValue(Info, valorFiltro, null);
                        }
                        else
                        {
                            Info.GetType().GetProperty(PropertyPathId).SetValue(Info, valorFiltro, null);
                        }
                    }
                    catch (Exception)
                    {
                        Info.GetType().GetProperty(PropertyPathId).SetValue(Info, Convert.ToInt32(valorFiltro), null);
                    }

                    AuxAyuda.AgregarFiltros(false, Info, tiposParametros, valoresParametros);

                    var ayudaTipo = AyudaPL.GetType();

                    if (Dependencias != null)
                    {
                        AuxAyuda.AgregarFiltros(false, Dependencias, tiposParametros, valoresParametros);
                    }

                    var metodo = ayudaTipo.GetMethod(metodoInvocacion, tiposParametros.ToArray());
                    if (metodo != null)
                    {
                        object resultadoInvocacion = metodo.Invoke(AyudaPL, valoresParametros.ToArray());
                        var resultado = (T)resultadoInvocacion;
                        AsignaValores(resultado);
                    }
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

        /// <summary>
        /// Asigna los Valores Obtenidos a los Controles
        /// </summary>
        /// <param name="resultado"></param>
        private void AsignaValores(T resultado)
        {
            if (resultado == null)
            {
                SkMessageBox.Show(Application.Current.Windows[1], MensajeClaveInexistente, MessageBoxButton.OK, MessageImage.Warning);
                txtDescripcion.Text = string.Empty;
                txtId.Text = string.Empty;
                if (BusquedaVacia != null)
                {
                    BusquedaVacia();
                }
            }
            else
            {
                txtDescripcion.Text = string.Empty;
                txtId.Text = string.Empty;
                existenDatos = true;
                txtId.Text = Convert.ToString(resultado.GetType().GetProperty(PropertyPathId).GetValue(resultado,
                                                                                                       null));
                txtDescripcion.Text =
                    Convert.ToString(resultado.GetType().GetProperty(PropertyPathDescripcion).GetValue(resultado,
                                                                                                       null));
                if (!String.IsNullOrWhiteSpace(PropertyHidden))
                {
                    txtOculto.Text =
                        Convert.ToString(resultado.GetType().GetProperty(PropertyHidden).GetValue(resultado, null));
                }
                AuxAyuda.AsignaValoresInfo(Info, resultado);
                if (AyudaConDatos != null)
                {
                    AyudaConDatos(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Busca los Valores de Ayuda
        /// </summary>
        /// <param name="e"></param>
        /// <param name="valor"></param>
        /// <param name="descripcion"></param>
        private void BuscarValoresAyuda(KeyEventArgs e, TextBox valor, TextBox descripcion)
        {
            
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                ValidaRegistro(valor, descripcion, e);                                
            }
            else
            {
                if (e.Key == Key.F1)
                {
                    MostrarBusquedaAyuda();
                }
                else
                {
                    if (e.Key == Key.Delete || e.Key == Key.Back)
                    {
                        descripcion.Text = string.Empty;
                    }
                }
            }
        }

        private void ValidaRegistro(TextBox valor, TextBox descripcion, KeyEventArgs e)
        {
            if (valor.Text.Trim().Length > 0)
            {
                if (((descripcion.Text.Trim().Length == 0 || descripcion.Text == "0")) 
                        && (string.Compare(valor.Text, "0", StringComparison.CurrentCultureIgnoreCase) != 0))
                {
                    bool dependenciasValidas = DependenciasValidas();
                    if (dependenciasValidas)
                    {
                        try
                        {
                            if (CamposInfo == null || CamposInfo.Count == 0)
                            {
                                ObtenerValoresAyuda(valor.Text);
                            }
                            else
                            {
                                ObtenerValoresAyudaCamposInfo(valor.Text);
                            }
                            if (existenDatos)
                            {
                                if (valor.Text.Trim().Length > 0)
                                {
                                    if (ObtenerDatos != null)
                                    {
                                        if (String.IsNullOrWhiteSpace(PropertyHidden))
                                        {
                                            ObtenerDatos(valor.Text);
                                        }
                                        else
                                        {
                                            ObtenerDatos(txtOculto.Text);
                                        }
                                    }
                                    if (LlamadaMetodos != null)
                                    {
                                        LlamadaMetodos();
                                    }
                                    existenDatos = false;
                                }
                            }
                            else
                            {
                                if (e != null)
                                {
                                    AsignarFoco();
                                    e.Handled = true;
                                }
                                if (LlamadaMetodosNoExistenDatos != null)
                                {
                                    LlamadaMetodosNoExistenDatos();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            SkMessageBox.Show(Application.Current.Windows[1], SuKarne.Controls.Properties.Resources.Ayuda_MensajeError,
                                              MessageBoxButton.OK,
                                              MessageImage.Error);
                        }
                    }
                    else
                    {
                        if (AyudaSimple)
                        {
                            txtDescripcion.Text = string.Empty;
                        }
                        else
                        {
                            txtId.Text = string.Empty;
                        }
                        if (e != null)
                        {
                            e.Handled = true;
                        }
                    }
                }   
            }
            else
            {
                if (LlamadaMetodosNoExistenDatos != null)
                {
                    LlamadaMetodosNoExistenDatos();
                }
            }
        }

        /// <summary>
        /// Valida que los Valores de Dependencia
        /// Sean Validos
        /// </summary>
        /// <returns></returns>
        private bool DependenciasValidas()
        {
            bool resultado = true;

            HashSet<string> dependenciasInvalidas = AuxAyuda.ValidaDependencias(Dependencias);
            if (dependenciasInvalidas != null && dependenciasInvalidas.Count > 0)
            {
                resultado = false;
                var sb = new StringBuilder();
                foreach (string campoInvalido in dependenciasInvalidas)
                {
                    sb.Append(MensajeDependencias[campoInvalido]).Append("\n");
                }
                SkMessageBox.Show(Application.Current.Windows[1], sb.ToString(), MessageBoxButton.OK, MessageImage.Stop);
            }

            return resultado;
        }

        /// <summary>
        /// Llama la Ventana Emergente de Ayuda
        /// </summary>
        private void MostrarBusquedaAyuda()
        {
            if (validarSiPuedeBuscar())
            {
                var dependenciasValidas = DependenciasValidas();
                if (!dependenciasValidas) return;
                SKAyudaBusqueda<T> ayudaBusqueda;
                if (CamposInfo == null || CamposInfo.Count == 0)
                {
                    SKAyudaBusqueda<T>.InicializaPropiedadesAnteriores(Info, PropertyPathId, PropertyPathDescripcion,
                                                                       PropertyHidden);
                    SKAyudaBusqueda<T>.InicializaPropiedadesInfoBusqueda(Info, PropertyPathId, PropertyPathDescripcion,
                                                                         PropertyHidden);
                    if (Dependencias == null)
                    {
                        ayudaBusqueda = new SKAyudaBusqueda<T>(AyudaPL, TituloEtiqueta, TituloPantalla, Info
                                                               , PropertyPathId, PropertyPathDescripcion, PropertyHidden
                                                               , AtributoClave, AtributoDescripcion, AtributoIdOcultoGrid);
                    }
                    else
                    {
                        ayudaBusqueda = new SKAyudaBusqueda<T>(AyudaPL, Dependencias, TituloEtiqueta, TituloPantalla, Info
                                                               , PropertyPathId, PropertyPathDescripcion, PropertyHidden
                                                               , AtributoClave, AtributoDescripcion, AtributoIdOcultoGrid);
                    }
                }
                else
                {
                    SKAyudaBusqueda<T>.InicializaPropiedadesAnterioresCamposInfo(Info, CamposInfo);
                    SKAyudaBusqueda<T>.InicializaPropiedadesInfoBusquedaCamposInfo(Info, CamposInfo);
                    if (Dependencias == null)
                    {
                        ayudaBusqueda = new SKAyudaBusqueda<T>(AyudaPL, CamposInfo, TituloEtiqueta, TituloPantalla);
                    }
                    else
                    {
                        ayudaBusqueda = new SKAyudaBusqueda<T>(AyudaPL, CamposInfo, Dependencias, TituloEtiqueta,
                                                               TituloPantalla);
                    }
                    ayudaBusqueda.Info = Info;
                }
                LimpiaPropieadEntera();
                ayudaBusqueda.MensajeCerrar = MensajeBusquedaCerrar;
                ayudaBusqueda.MensajeBusqueda = MensajeBusqueda;
                ayudaBusqueda.MensajeAgregar = MensajeAgregar;
                ayudaBusqueda.MetodoPaginado = MetodoPaginadoBusqueda;

                ayudaBusqueda.Owner = Application.Current.Windows[1];
                if (CamposInfo == null || CamposInfo.Count == 0)
                {
                    ayudaBusqueda.ObtenerValoresAyuda(1, 15);
                }
                else
                {
                    ayudaBusqueda.ObtenerValoresAyudaCamposInfo(1, 15);
                }

                ayudaBusqueda.TipoCampoCodigo = tipoCampoCodigo;
                ayudaBusqueda.ShowDialog();
                ventanaBusqueda = true;
                if (!ayudaBusqueda.Cancelado)
                {
                    txtDescripcion.Text = ayudaBusqueda.Descripcion;
                    txtId.Text = ayudaBusqueda.Clave;
                    txtOculto.Text = ayudaBusqueda.Id;
                    AsignarFoco();
                    var keyboardFocus = Keyboard.FocusedElement as UIElement;
                    var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                    if (keyboardFocus != null)
                    {
                        keyboardFocus.MoveFocus(tRequest);
                    }
                    if (AyudaConDatos != null)
                    {
                        AyudaConDatos(this, EventArgs.Empty);
                    }
                }
                ventanaBusqueda = false;
                LlamadaDelegados();
            }
        }


        /// <summary>
        /// Manda Llamar los Delegados
        /// </summary>
        private void LlamadaDelegados()
        {
            if (txtId.Text.Trim().Length > 0)
            {
                string valor;
                if (string.IsNullOrWhiteSpace(PropertyHidden))
                {
                    if (AyudaSimple)
                    {
                        valor = txtDescripcion.Text;
                    }
                    else
                    {
                        valor = txtId.Text;
                    }                    
                }
                else
                {
                    valor = txtOculto.Text;
                }

                if (ObtenerDatos != null)
                {
                    ObtenerDatos(valor);
                }
                if (LlamadaMetodos != null)
                {
                    LlamadaMetodos();
                }
            }
        }

        /// <summary>
        /// Asigna el Foco al Control de Ayuda
        /// </summary>
        public void AsignarFoco()
        {
            if (txtDescripcion != null && txtId != null)
            {
                if (AyudaSimple)
                {                 
                    txtDescripcion.Focus();
                }
                else
                {                    
                    txtId.Focus();
                }
            }
        }

        /// <summary>
        /// Asigna el orden del TabIndex del control
        /// </summary>
        public void AsignaTabIndex(int tabIndex)
        {
            if (AyudaSimple)
            {
                txtDescripcion.TabIndex = tabIndex;
                txtId.IsTabStop = false;
            }
            else
            {
                txtId.TabIndex = tabIndex;
                txtDescripcion.IsTabStop = false;
            }
        }
        

        /// <summary>
        /// Limpia el control oculto
        /// </summary>
        private void LimpiaPropieadEntera()
        {
            AuxAyuda.InicializaPropiedad(Info);
        }

        #endregion METODOS

        #region EVENTOS
        
        private void BtnBuscarClick(object sender, RoutedEventArgs e)
        {
            MostrarBusquedaAyuda();            
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            BuscarValoresAyuda(e, txtDescripcion, txtId);
        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsEnabled)
            {
                if (!ventanaBusqueda)
                {
                    if (!existenDatos)
                    {
                        existenDatos = false;
                        if (!txtDescripcion.IsReadOnly)
                        {
                            txtId.Text = string.Empty;
                            LimpiaPropieadEntera();
                            if (AyudaLimpia != null)
                            {
                                AyudaLimpia(this, EventArgs.Empty);
                            }
                        }
                    }
                }
            }
        }

        private void txtDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidaRegistro(txtDescripcion, txtId, null);
        }
        
        private void txtId_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidaRegistro(txtId, txtDescripcion, null);
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsEnabled)
            {
                if (!ventanaBusqueda)
                {
                    if (!existenDatos)
                    {
                        existenDatos = false;
                        if (txtDescripcion.IsReadOnly && txtDescripcion.Text.Trim().Length > 0)
                        {
                            txtDescripcion.Text = string.Empty;
                            LimpiaPropieadEntera();
                            if (AyudaLimpia != null)
                            {
                                AyudaLimpia(this, EventArgs.Empty);
                            }
                        }
                    }
                }
            }
        }

        private void Text_Numeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void Text_NumerosLetrasGuiones(object sender, TextCompositionEventArgs e)
        {
            switch (TipoCampoCodigo)
            {
                case TipoCampo.NoEspecificado:
                    break;
                default:
                    e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuion(e.Text);
                    break;
            }
        }
        #endregion EVENTOS
    }
}