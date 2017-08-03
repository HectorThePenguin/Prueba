using System;
using System.Reflection;

namespace SIE.Base.Exepciones
{
    [Serializable]
    public class ExcepcionDesconocida : ExcepcionGenerica
    {
        /// <summary>
        ///     Constructor de la clase
        /// </summary>
        /// <param name="id"></param>
        public ExcepcionDesconocida(int id)
            : base(id)
        {
        }

        /// <summary>
        ///     Constructor de la clase.
        /// </summary>
        /// <param name="mensaje">Los valores del mensaje a ser reemplazados.</param>
        public ExcepcionDesconocida(string mensaje)
            : base(mensaje)
        {
        }

        /// <summary>
        ///     Constructor de la clase.
        /// </summary>
        /// <param name="mensaje">Los valores del mensaje a ser reemplazados</param>
        /// <param name="excepcion">La excepcion interna a ser envuelta.</param>
        public ExcepcionDesconocida(string mensaje, Exception excepcion)
            : base(mensaje, excepcion)
        {
        }

        /// <summary>
        ///  Constructor de la clase
        /// </summary>
        /// <param name="methodBase"></param>
        /// <param name="excepcion"></param>
        public ExcepcionDesconocida(MethodBase methodBase, Exception excepcion)
            : base(methodBase, excepcion)
        {
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="excepcionId"></param>
        /// <param name="mensaje"></param>
        /// <param name="excepcion"></param>
        public ExcepcionDesconocida(int excepcionId, string mensaje, Exception excepcion)
            : base(excepcionId, mensaje, excepcion)
        {
        }
    }
}