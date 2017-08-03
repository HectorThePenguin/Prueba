using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroReporteAuxiliarMP : INotifyPropertyChanged
    {
        private OrganizacionInfo organizacion;
        private ProductoInfo producto;
        private AlmacenInfo almacen;
        private AlmacenInventarioLoteInfo almacenInventarioLote;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        /// <summary>
        /// Organizacion del Reporte
        /// </summary>
        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                if (value != organizacion)
                {
                    organizacion = value;
                    NotifyPropertyChanged("Organizacion");
                }
            }
        }

        /// <summary>
        /// Producto del Reporte
        /// </summary>
        public ProductoInfo Producto
        {
            get { return producto; }
            set
            {
                if (value != producto)
                {
                    producto = value;
                    NotifyPropertyChanged("Producto");
                }
            }
        }
        /// <summary>
        /// Almacen para obtener la informacion
        /// </summary>
        public AlmacenInfo Almacen
        {
            get { return almacen; }
            set
            {
                if (value != almacen)
                {
                    almacen = value;
                    NotifyPropertyChanged("Almacen");
                }
            }
        }
        /// <summary>
        /// Lote del Almacen
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote
        {
            get { return almacenInventarioLote; }
            set
            {
                if (value != almacenInventarioLote)
                {
                    almacenInventarioLote = value;
                    NotifyPropertyChanged("AlmacenInventarioLote");
                }
            }
        }
        /// <summary>
        /// Fecha de Inicio de Reporte
        /// </summary>
        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set
            {
                if (value != fechaInicio)
                {
                    fechaInicio = value;
                    NotifyPropertyChanged("FechaInicio");
                }
            }
        }
        /// <summary>
        /// Fecha de Fin del reporte
        /// </summary>
        public DateTime FechaFin
        {
            get { return fechaFin; }
            set
            {
                if (value != fechaFin)
                {
                    fechaFin = value;
                    NotifyPropertyChanged("FechaFin");
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
