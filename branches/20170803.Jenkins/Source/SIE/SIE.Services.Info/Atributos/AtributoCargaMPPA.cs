using System;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoCargaMPPA : Attribute
    {
        /// <summary>
        /// Numero de la Celda donde buscara el valor, indice 1
        /// </summary>
        public int Celda { get; set; }

        /// <summary>
        ///Propiedad para indicar tipo de dato que tendra la propiedad
        /// </summary>
        public TypeCode TipoDato { get; set; }

        /// <summary>
        ///Propiedad para indicar si la propiedad puede venir vacia
        /// </summary>
        public bool AceptaVacio { get; set; }
    }
}
