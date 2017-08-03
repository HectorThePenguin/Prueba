using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class CargaMPPAModel 
    {
        /// <summary>
        /// Id del almacen a cargar, Celda A
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 1, TipoDato = TypeCode.Int32, AceptaVacio = false)]
        public int AlmacenID { get; set; }

        /// <summary>
        /// Id del producto a cargar, Celda B
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 2, TipoDato = TypeCode.Int32, AceptaVacio = false)]
        public int ProductoID { get; set; }

        /// <summary>
        /// Número de Lote, si no maneja lote poner un 0, Celda C
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 3, TipoDato = TypeCode.Int32, AceptaVacio = true)]
        public int Lote { get; set; }
        /// <summary>
        /// Cantidad inicial del inventario, Celda D
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 4, TipoDato = TypeCode.Decimal, AceptaVacio = false)]
        public decimal CantidadTamanioLote { get; set; }
        /// <summary>
        /// Importe inicial del inventario, Celda E
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 5, TipoDato = TypeCode.Decimal, AceptaVacio = false)]
        public decimal ImporteTamanioLote { get; set; }
        /// <summary>
        /// Cantidad actual del inventario, Celda F
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 6, TipoDato = TypeCode.Decimal, AceptaVacio = false)]
        public decimal CantidadActual { get; set; }
        /// <summary>
        /// Importe actual del inventario, Celda G
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 7, TipoDato = TypeCode.Decimal, AceptaVacio = false)]
        public decimal ImporteActual { get; set; }
        /// <summary>
        /// Piezas del Lote, en caso de que aplique, Celda H
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 8, TipoDato = TypeCode.Int32, AceptaVacio = true)]
        public int Piezas { get; set; }
        /// <summary>
        /// Fecha Inicio del Lote, en caso de que aplique, Celda I
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 9, TipoDato = TypeCode.DateTime, AceptaVacio = true)]
        public DateTime FechaInicioLote { get; set; }

        #region Propiedades Ajenas al Excel
        /// <summary>
        /// Inventario, para validar si ya existe
        /// </summary>
        public AlmacenInventarioInfo AlmacenInventario { get; set; }
        /// <summary>
        /// Lote, para validar si ya existe
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }
        /// <summary>
        /// Almacen para mostrar la descripcion, en caso de conflicto 
        /// </summary>
        public AlmacenInfo Almacen { get; set; }
        /// <summary>
        /// Producto para mostrar la descripcion, en caso de conflicto 
        /// </summary>
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Mensaje que marca si el renglon tiene conflictos
        /// </summary>
        public string MensajeAlerta { get; set; }

        #endregion Propiedades Ajenas al Excel
    }
}
