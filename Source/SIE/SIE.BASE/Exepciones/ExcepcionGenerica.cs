using System;
using System.Reflection;

namespace SIE.Base.Exepciones
{
    [Serializable]
    public abstract class ExcepcionGenerica : Exception
    {
        /// <summary>
        /// Identificador de la exepcion 
        /// </summary>
        public int ExcepcionId { get; set; }
        /// <summary>
        ///     Inicializa un instancia de la clase GenericException.
        /// </summary>
        /// <param name="excepcionId">el Id de la excepcion.</param>
        protected ExcepcionGenerica(int excepcionId)
        {
            ExcepcionId = excepcionId;
        }

        /// <summary>
        ///     Inicializa un instancia de la clase GenericException.
        /// </summary>
        /// <param name="mensaje">Los valores del mensaje a ser reemplazados.</param>
        protected ExcepcionGenerica(string mensaje)
            : base(mensaje)
        {
        }

        /// <summary>
        ///     Inicializa un instancia de la clase GenericException.
        /// </summary>
        /// <param name="excepcionId">Id de la Excepcion.</param>
        /// <param name="mensaje">Mensaje de la excepcion.</param>
        protected ExcepcionGenerica(int excepcionId, string mensaje)
            : base(mensaje)
        {
            ExcepcionId = excepcionId;
        }

        /// <summary>
        ///     Inicializa un instancia de la clase GenericException.
        /// </summary>
        /// <param name="mensaje">Los valores del mensaje a ser reemplazados.</param>
        /// <param name="excepcion">Excepcion Interna.</param>
        protected ExcepcionGenerica(string mensaje, Exception excepcion)
            : base(mensaje, excepcion)
        {
        }

        /// <summary>
        ///     Inicializa un instancia de la clase GenericException.
        /// </summary>
        /// <param name="excepcionId">Id de la Excepcion.</param>
        /// <param name="mensaje">Mensaje de la excepcion.</param>
        /// <param name="excepcion">Excepcion Interna.</param>
        protected ExcepcionGenerica(int excepcionId, string mensaje, Exception excepcion)
            : base(mensaje, excepcion)
        {
            ExcepcionId = excepcionId;
        }

        /// <summary>
        ///     Inicializa una instancia de la clase GenericException
        /// </summary>
        /// <param name="metodo"></param>
        /// <param name="excepcion"></param>
        protected ExcepcionGenerica(MethodBase metodo, Exception excepcion)
            : base(metodo.Name, excepcion)
        {
        }        
    }
}