namespace SIE.Base.Infos
{
    public class PaginacionInfo
    {
        /// <summary>
        /// Inicio de donde se empezara a tomar los registros
        /// </summary>
        public int Inicio { get; set; }

        /// <summary>
        /// Limite de Registros a tomar
        /// </summary>
        public int Limite { get; set; }

        /// <summary>
        /// Total de registros
        /// </summary>
        public int TotalRegistros { get; set; }

        /// <summary>
        /// Nombre de la columna a Ordenar
        /// </summary>
        public string Ordenar { get; set; }

        /// <summary>
        /// Tipo de Ordenamiento Asc o Desc
        /// </summary>
        public string Direccion { get; set; }
    }
}