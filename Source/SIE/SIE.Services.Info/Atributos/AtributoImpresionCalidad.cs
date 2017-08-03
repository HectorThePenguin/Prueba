using System;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoImpresionCalidad : Attribute
    {
        /// <summary>
        /// Indica el id de la Calidad de Ganado
        /// </summary>
        public int CalidadGanadoID { get; set; }
    }
}
