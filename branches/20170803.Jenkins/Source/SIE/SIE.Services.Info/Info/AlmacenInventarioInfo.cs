using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class AlmacenInventarioInfo : INotifyPropertyChanged
    {
        private decimal precioPromedio;
        private decimal cantidad;
        /// <summary>
        /// identificador del inventrio
        /// </summary>
        public int AlmacenInventarioID { get; set; }

        /// <summary>
        /// Identificador del Almacen
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// Almacén del inventario
        /// </summary>
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// identificador del producto
        /// </summary>
        public int ProductoID { get; set; }

        /// <summary>
        /// Producto en inventario
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Cantidad minima
        /// </summary>
        public int Minimo { get; set; }

        /// <summary>
        /// Cantidad Maxima
        /// </summary>
        public int Maximo { get; set; }

        /// <summary>
        /// Precio Promedio del producto
        /// </summary>
        public decimal PrecioPromedio
        {
            get { return precioPromedio; }
            set
            {
                precioPromedio = value;
                NotifyPropertyChanged("PrecioPromedio");
            }
        }

        /// <summary>
        /// Cantidad en existencia del producto
        /// </summary>
        public decimal Cantidad
        {
            get { return cantidad; }
            set
            {
                cantidad = value;
                NotifyPropertyChanged("Cantidad");
            }
        }

        /// <summary>
        /// Importe del producto
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Dias de Reorden
        /// </summary>
        public int DiasReorden { get; set; }

        /// <summary>
        /// Capacidad de Almacenaje
        /// </summary>
        public int CapacidadAlmacenaje { get; set; }

        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario Creacion del registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Usuario Modificacion del registro
        /// </summary>
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// Indica si el registro  se encuentra Activo
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Producto en inventario
        /// </summary>
        public List<AlmacenInventarioLoteInfo> ListaAlmacenInventarioLote { get; set; }

        /// <summary>
        /// Identifica si el moviemiento es entrada para descontar o sumar al Inventario la cantidad
        /// </summary>
        public bool EsEntrada { get; set; }

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
