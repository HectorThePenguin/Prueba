using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class TratamientoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int codigoTratamiento = 0;
        private int _tipoTratamiento;
        private string _auxiliar = string.Empty;
        /// <summary>
        /// Identificador del tratamiento
        /// </summary>
        public int TratamientoID { get; set; }

        public string TipoDescripcion { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionId { get; set; }

        /// <summary>
        /// Codigo del tratamiento
        /// </summary>
        public int CodigoTratamiento
        {
            get { return codigoTratamiento; }
            set
            {
                if (value != codigoTratamiento)
                {
                    codigoTratamiento = value;
                    NotifyPropertyChanged("CodigoTratamiento");
                }
            }
        }

        /// <summary>
        /// Descripcion del codigo de tratamiento
        /// </summary>
        public string Descripcion
        {
            get
            {
                return string.Format("{0:d3}", CodigoTratamiento);
            }
        }
        /// <summary>
        /// Sexo aplicado al tratamiento
        /// </summary>
        public Sexo Sexo { get; set; }
        /// <summary>
        /// Rango inicial de peso aplicado al tratamiento
        /// </summary>
        public int RangoInicial { get; set; }
        /// <summary>
        /// Rango final de peso aplicado al tratamiento
        /// </summary>
        public int RangoFinal { get; set; }
        /// <summary>
        /// Peso aplicado al tratamiento
        /// </summary>
        public decimal Peso { get; set; }

        /// <summary>
        /// Tipo de tratamiento
        /// </summary>
        public int TipoTratamiento
        {
            get
            {
                return _tipoTratamiento;
            }
            set
            {
                _tipoTratamiento = value;
                //Mostrar seleccionado cuando es para corte
                if (_tipoTratamiento == 1 || _tipoTratamiento == 2)
                    Seleccionado = true;
            }
        }

        /// <summary>
        /// Especifica si se ha seleccionado el elemento desde una lista
        /// </summary>
        private bool seleccionado;

        public bool Seleccionado
        {
            get { return seleccionado; }
            set
            {
                seleccionado = value;
                NotifyPropertyChanged("Seleccionado");
            }
        }

        /// <summary>
        /// Deshabilitar elementos de una lista
        /// </summary>
        private bool habilitado;

        public bool Habilitado
        {
            get { return habilitado; }
            set
            {
                habilitado = value;
                NotifyPropertyChanged("Habilitado");
            }
        }

        /// <summary>
        /// Lista de productos de un tratamiento
        /// </summary>
        public IList<ProductoInfo> Productos { get; set; }

        /// <summary>
        /// Lista de productos para un tratamiento
        /// </summary>
        public List<TratamientoProductoInfo> ListaTratamientoProducto { get; set; }

        /// <summary>
        /// Tipo de Tratamiento
        /// </summary>
        public TipoTratamientoInfo TipoTratamientoInfo { get; set; }

        /// <summary>
        /// Organización del Tratamiento
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Fecha en que fue aplicado el tratamiento
        /// </summary>
        public DateTime FechaAplicacion { get; set; }

        /// <summary>
        /// Dias
        /// </summary>
        public int Dias { get; set; }
        /// <summary>
        /// Identificador del problema
        /// </summary>
        public int ProblemaID { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }

        public string ProductoDescripcion
        {
            get
            {
                if (Productos != null && Productos.Count > 0)
                {
                    return Productos.First().ProductoDescripcion;
                }
                return "";
            }
        }

        public string Auxiliar
        {
            get { return _auxiliar; }
            set
            {
                if (value != _auxiliar)
                {
                    _auxiliar = value;
                    NotifyPropertyChanged("Auxiliar");
                }
            }
        }
        
        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
