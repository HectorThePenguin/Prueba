using System.Collections.Generic;

namespace SIE.Base.Infos
{
    public class ResultadoInfo<T>
    {
        /// <summary>
        ///     Lista de Grupos
        /// </summary>
        public IList<T> Lista { get; set; }

        /// <summary>
        ///     Total de Registros
        /// </summary>
        public int TotalRegistros { get; set; }
    }
}