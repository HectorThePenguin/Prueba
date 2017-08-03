using System;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoAyuda : Attribute
    {
        /// <summary>
        /// Nombre de la propiedad que se
        /// hara referencia en la Clase Info
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Metodo que sera Invocado
        /// </summary>
        public String MetodoInvocacion { get; set; }

        /// <summary>
        /// Define la etiqueta que se mostrara en el 
        /// encabezado de la columna en el grid de ayuda
        /// </summary>
        public String EncabezadoGrid { get; set; }

        /// <summary>
        /// Indica si se manda llamar desde el popup
        /// </summary>
        public bool PopUp { get; set; }

        /// <summary>
        /// Indica si el objeto esta en
        /// un contenedor
        /// </summary>
        public bool EstaEnContenedor { get; set; }

        /// <summary>
        /// Indica el Nombre del Objeto
        /// dentro del Contenedor
        /// </summary>
        public String NombreContenedor { get; set; }
    }
}
