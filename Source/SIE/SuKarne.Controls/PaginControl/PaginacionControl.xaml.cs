using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace SuKarne.Controls.PaginControl
{
    public delegate void LlenadoDatosDelegado(int inicio, int limite);

    /// <summary>
    /// Interaction logic for PaginacionControl.xaml
    /// </summary>
    public partial class PaginacionControl
    {
        public LlenadoDatosDelegado DatosDelegado;

        public PaginacionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Contexto al cual estara ligado
        /// el control
        /// </summary>
        public object Contexto
        {
            get { return DataContext; }
            set { DataContext = value; }
        }

        /// <summary>
        /// Contexto al cual estara ligado
        /// el control
        /// </summary>
        private object contextoAnterior;
        public object ContextoAnterior
        {
            get { return contextoAnterior; }
            set { contextoAnterior = value; }
        }

        private int limite;
        public int Limite
        {
            get
            {
                if (Inicio == 1)
                {
                    limite = Convert.ToInt32(cboResultado.SelectedValue);
                    if (limite == 0)
                    {
                        limite = 15;
                    }
                }
                return limite;
            }
            set { limite = value; }
        }

        private int inicio;
        public int Inicio
        {
            get { return inicio; }
            set
            {
                inicio = value;
                if (TotalRegistros == 0)
                {
                    ActualizaEtiquetaPagina(0);
                }
                else
                {
                    ActualizaEtiquetaPagina(inicio);
                }
            }
        }

        public int NumeroDePaginas
        {
            get { return Convert.ToInt32(lblPaginasTotales.Content); }
            set { lblPaginasTotales.Content = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Numero total de Registros
        /// </summary>
        public int TotalRegistros
        {
            get
            {
                return Convert.ToInt32(lblTotalRegistros.Content);
            }
            set
            {
                int totalPages = (value/Limite);

                if ((value%Limite) > 0)
                {
                    totalPages++;
                }
                NumeroDePaginas = totalPages;
                lblTotalRegistros.Content = value.ToString(CultureInfo.InvariantCulture);
                if (value < 1)
                {
                    btnAnterior.IsEnabled = false;
                    btnSiguiente.IsEnabled = false;
                    cboResultado.IsEnabled = false;
                }
                else
                {
                    if (!btnAnterior.IsEnabled)
                    {
                        btnAnterior.IsEnabled = true;
                        btnSiguiente.IsEnabled = true;
                        cboResultado.IsEnabled = true;
                    }
                }
                if (value < (Limite * Inicio))
                {
                    ActualizaEtiquetaPagina(1);
                }
                if (value == 0)
                {
                    ActualizaEtiquetaPagina(0);
                }
                else
                {
                    ActualizaEtiquetaPagina(Inicio);
                }
            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            int paginaActual = ValidaCambioContexto(false);
            if (paginaActual > 0)
            {
                int limite = paginaActual*Limite;
                int inicio = limite - Limite + 1;
                Inicio = paginaActual;
                if (DatosDelegado != null)
                {
                    DatosDelegado(inicio, limite);
                }
                ActualizaEtiquetaPagina(Inicio);
            }
        }

        private int ValidaCambioContexto(bool siguiente)
        {
            int paginaActual;
            if (Contexto != null && !CompararObjetos(Contexto, ContextoAnterior))
            {
                paginaActual = 1;
                Inicio = 1;
                lblPaginaActual.Content = inicio.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                Inicio = Convert.ToInt32(lblPaginaActual.Content);
                if (siguiente)
                {
                    paginaActual = Inicio + 1;
                }
                else
                {
                    paginaActual = Inicio - 1;
                }
                int paginasTotales = Convert.ToInt32(lblPaginasTotales.Content);
                if (paginaActual > paginasTotales)
                {
                    paginaActual--;
                    Inicio = paginaActual;
                }
            }
            ContextoAnterior = ClonarInfo(Contexto);
            return paginaActual;
        }

        public bool CompararObjetos(object contextoActual, object contextoAnterior)
        {
            PropertyInfo[] propertiesActuales = contextoActual.GetType().GetProperties();
            var iguales = true;
            foreach (var propertyInfo in propertiesActuales)
            {
                MethodInfo getMethod = contextoActual.GetType().GetProperty(propertyInfo.Name).GetGetMethod();
                if (getMethod != null)
                {
                    object valorPropiedadActual = contextoActual.GetType().GetProperty(propertyInfo.Name).GetValue(contextoActual, null);
                    object valorPropiedadAnterior = contextoAnterior.GetType().GetProperty(propertyInfo.Name).GetValue(contextoAnterior, null);
                    if (valorPropiedadActual != null && valorPropiedadActual.GetType().Namespace.Contains(".Info.Info"))
                    {
                        valorPropiedadActual = CompararObjetos(valorPropiedadActual, valorPropiedadAnterior);
                        valorPropiedadAnterior = CompararObjetos(valorPropiedadActual, valorPropiedadAnterior);
                    }
                    TypeCode tipo = Type.GetTypeCode(propertyInfo.PropertyType);
                    switch(tipo)
                    {
                        case TypeCode.String:
                            if (string.IsNullOrWhiteSpace(Convert.ToString(valorPropiedadActual)) 
                                && string.IsNullOrWhiteSpace(Convert.ToString(valorPropiedadAnterior)))
                            {
                                continue;
                            }
                            break;
                    }
                    if (valorPropiedadActual == null && valorPropiedadAnterior == null)
                    {
                        continue;
                    }
                    if (valorPropiedadActual == null && valorPropiedadAnterior != null)
                    {
                        iguales = false;
                        break;
                    }
                    if (valorPropiedadActual != null && valorPropiedadAnterior == null)
                    {
                        iguales = false;
                        break;
                    }
                    if (!valorPropiedadActual.Equals(valorPropiedadAnterior))
                    {
                        iguales = false;
                        break;
                    }
                }
            }
            return iguales;
        }

        /// <summary>
        /// Genera un Clone de la Instancia que se le envia
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private Object ClonarInfo(Object info)
        {
            if (info == null)
            {
                return null;
            }
            Object clone = Activator.CreateInstance(info.GetType(), null);
            PropertyInfo[] properties = info.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                MethodInfo getMethod = info.GetType().GetProperty(propertyInfo.Name).GetGetMethod();
                if (getMethod != null)
                {
                    object valorPropiedad = info.GetType().GetProperty(propertyInfo.Name).GetValue(info, null);
                    if (valorPropiedad != null && valorPropiedad.GetType().Namespace.Contains(".Info.Info"))
                    {
                        valorPropiedad = ClonarInfo(valorPropiedad);
                    }
                    MethodInfo setMethod = clone.GetType().GetProperty(propertyInfo.Name).GetSetMethod();
                    if (setMethod != null)
                    {
                        clone.GetType().GetProperty(propertyInfo.Name).SetValue(clone, valorPropiedad, null);
                    }
                }
            }
            return clone;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            int paginaActual = ValidaCambioContexto(true);
            if (paginaActual <= NumeroDePaginas)
            {
                int limite = paginaActual*Limite;
                int inicio = limite - Limite + 1;
                Inicio = paginaActual;
                if (DatosDelegado != null)
                {
                    DatosDelegado(inicio, limite);
                }
                ActualizaEtiquetaPagina(Inicio);
            }
        }

        private void ActualizaEtiquetaPagina(int paginaActual)
        {
            if (TotalRegistros == 0)
            {
                lblPaginaActual.Content = "0";
            }
            else
            {
                lblPaginaActual.Content = paginaActual.ToString();
            }
        }

        public void AsignarValoresIniciales()
        {
            Inicio = 1;
            Limite = 15;
            ActualizaEtiquetaPagina(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarCombo();
            if (Contexto != null)
            {
                ContextoAnterior = ClonarInfo(Contexto);
            }            
        }

        private void CargarCombo()
        {            
            var records = new[] {"15", "30", "50"};
            cboResultado.ItemsSource = records;
            cboResultado.SelectedIndex = 0;
            Limite = 15;
            cboResultado.SelectionChanged += cboResultado_SelectionChanged;
        }

        private void cboResultado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Limite = Convert.ToInt32(cboResultado.SelectedValue);
            Inicio = 1;
            if (DatosDelegado != null)
            {
                DatosDelegado(1, Limite);
            }            
        }
    }
}