using System;
namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoDataLink : Attribute
    {
        /// <summary>
        ///Propiedad para indicar el inicio de la posicion a buscar en el archivo DataLink
        /// </summary>
        public int IndiceInicio { get; set; }

        /// <summary>
        ///Propiedad para indicar el cuantos caracteres tomara para el valor
        /// </summary>
        public int Longitud { get; set; }

        /// <summary>
        ///Propiedad para indicar tipo de dato que tendra la propiedad
        /// </summary>
        public TypeCode TipoDato { get; set; }
    }
}
