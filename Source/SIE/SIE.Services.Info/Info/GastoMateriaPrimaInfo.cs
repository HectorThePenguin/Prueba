using System;
using System.ComponentModel;
using SIE.Services.Info.Enums;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class GastoMateriaPrimaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private bool unidadMedida;
        private decimal kilogramos;

        private AlmacenInfo almacen;
        private OrganizacionInfo organizacion;
        private ProductoInfo producto;

        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int GastoMateriaPrimaID { get; set; }

        /// <summary>
        /// Organizacion de la solicitud
        /// </summary>
        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        /// <summary>
        /// Identificador del Gasto Materia Prima
        /// </summary>
        public TipoMovimientoInfo TipoMovimiento { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Producto del Gasto Materia Prima
        /// </summary>
        public ProductoInfo Producto
        {
            get { return producto; }
            set
            {
                producto = value;
                NotifyPropertyChanged("Producto");
            }
        }

        /// <summary>
        /// Identifica si tiene cuenta
        /// </summary>
        public bool TieneCuenta { get; set; }

        /// <summary>
        /// CuentaSAP del Gasto Materia Prima
        /// </summary>
        public CuentaSAPInfo CuentaSAP { get; set; }

        /// <summary>
        /// Proveedor del Gasto Materia Prima
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// AlmacenInventarioLote del Gasto Materia Prima
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }

        /// <summary>
        /// Importe del Gasto Materia Prima
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Identifica si el importe contiene IVA
        /// </summary>
        public bool Iva { get; set; }

        ///// <summary>
        ///// Identifica si el importe contiene Retencion
        ///// </summary>
        //public bool Retencion { get; set; }

        /// <summary>
        /// Observaciones del Gasto Materia Prima
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Fecha en que se creo la solicitud
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Almacen ID del Producto seleccionado
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// Contiene el identificador del movimiento realizadon
        /// </summary>
        public long? AlmacenMovimientoID { get; set; }

        /// <summary>
        /// Indica si el Gasto es de Entrada por Ajuste o Salida por Ajuste, (Poliza)
        /// </summary>
        public bool EsEntrada { get; set; }

        /// <summary>
        /// Almacen ID del Producto seleccionado
        /// </summary>
        public long FolioGasto { get; set; }

        /// <summary>
        /// Almacen ID del Producto seleccionado
        /// </summary>
        public TipoFolio TipoFolio { get; set; }

        /// <summary>
        /// Contiene los detalles del producto que no tiene lote
        /// </summary>
        public AlmacenInventarioInfo AlmacenInventario { get; set; }

        /// <summary>
        /// Indica si se selecciono uniadad de medida
        /// </summary>
        public bool UnidadMedida
        {
            get { return unidadMedida; }
            set
            {
                unidadMedida = value;
                if (!unidadMedida)
                {
                    kilogramos = 0;
                    NotifyPropertyChanged("Kilogramos");
                }
                NotifyPropertyChanged("UnidadMedida");
            }
        }

        /// <summary>
        /// Cantidad de kilogramos afectados
        /// </summary>
        public decimal Kilogramos
        {
            get { return kilogramos; }
            set
            {
                kilogramos = value;
                NotifyPropertyChanged("Kilogramos");
            }
        }

        /// <summary>
        /// Almacen
        /// </summary>
        public AlmacenInfo Almacen
        {
            get { return almacen; }
            set
            {
                almacen = value;
                NotifyPropertyChanged("Almacen");
            }
        }

        /// <summary>
        /// Folio del Movimiento Generado
        /// </summary>
        public long FolioMovimiento { get; set; }

        public IList<AreteInfo> AretesCapturados { get; set; }

        public bool GuardaAretes { get; set; }

        public bool EsAreteSukarne { get; set; }

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
