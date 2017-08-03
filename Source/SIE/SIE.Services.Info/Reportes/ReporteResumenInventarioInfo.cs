using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteResumenInventarioInfo
    {
        /// <summary>
        /// CodigoFamilia
        /// </summary>
        public int CodigoFamilia { get; set; }
        /// <summary>
        /// Familia
        /// </summary>
        public string Familia { get; set; }
        /// <summary>
        /// CodigoSubFamilia
        /// </summary>
        public int CodigoSubFamilia { get; set; }
        /// <summary>
        /// SubFamilia
        /// </summary>
        public string SubFamilia { get; set; }
        /// <summary>
        /// Código
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Producto
        /// </summary>
        public string Producto { get; set; }
        /// <summary>
        /// Almacen
        /// </summary>
        public int Almacen { get; set; }
        /// <summary>
        /// TipoAlmacen
        /// </summary>
        public string TipoAlmacen { get; set; }
        /// <summary>
        /// CantidadInventario
        /// </summary>
        //public Decimal CantidadInventario { get; set; }
        //public Decimal CantidadInventarioInicial {
        //    get { return CantidadInventario + UnidadSalida - UnidadEntrada; }
        //}
        //public Decimal ImporteInventarioInicial {
        //    get { return ImporteMovimiento + ImporteSalida - ImporteEntrada; }
        //}

        public Decimal CantidadInventario { get; set; }
        public Decimal CantidadInventarioInicial
        {
            get; set; //get { return CantidadInventario ; }
        }
        public Decimal ImporteInventarioInicial
        {
            get; set;//get { return ImporteMovimiento ; }
        }

        /// <summary>
        /// CantidadMovimiento
        /// </summary>
        public Decimal CantidadMovimiento { get; set; }
        /// <summary>
        /// ImporteMovimiento
        /// </summary>
        public Decimal ImporteMovimiento { get; set; }
        /// <summary>
        /// EsEntrada
        /// </summary>
        public Boolean EsEntrada { get; set; }
        /// <summary>
        /// EsSalida
        /// </summary>
        public Boolean EsSalida { get; set; }
        /// <summary>
        /// TipoMovimiento
        /// </summary>
        public string TipoMovimiento { get; set; }
        /// <summary>
        /// TipoMovimientoID
        /// </summary>
        public int TipoMovimientoId { get; set; }
        /// <summary>
        /// UnidadEntrada
        /// </summary>
        public Decimal UnidadEntrada { get; set; }
        /// <summary>
        /// UnidadSalida
        /// </summary>
        public Decimal UnidadSalida { get; set; }
        /// <summary>
        /// ImporteEntrada
        /// </summary>
        public Decimal ImporteEntrada { get; set; }
        /// <summary>
        /// ImporteSalida
        /// </summary>
        public Decimal ImporteSalida { get; set; }
        /// <summary>
        /// FechaInicial
        /// </summary>
        public DateTime FechaInicial { get; set; }
        /// <summary>
        /// FechaFinal
        /// </summary>
        public DateTime FechaFinal { get; set; }
        /// <summary>
        /// Titulo
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion
        /// </summary>
        public string Organizacion { get; set; }

        /// <summary>
        /// Fecha inicio con formato
        /// </summary>
        public string FechaInicioConFormato
        {
            get
            {
                return FechaInicial.ToString("dd/MM/yyyy");

            }

        }
        /// <summary>
        /// Fecha fin con formato
        /// </summary>
        public string FechaFinConFormato
        {
            get
            {
                return FechaFinal.ToString("dd/MM/yyyy");

            }

        }

        public string FechaCompuesta
        {
            get { return String.Format("Del {0} al {1}", FechaInicioConFormato, FechaFinConFormato); }
        }
    }
}

