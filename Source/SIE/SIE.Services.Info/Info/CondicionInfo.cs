namespace SIE.Services.Info.Info
{
    public class CondicionInfo :BitacoraInfo
    {
        private string descripcion;
        /// <summary>
        /// Identificador de la Condicion de ganado 
        /// </summary>
        public int CondicionID { get; set; }

        /// <summary>
        /// Descripcion de la condicion de ganado
        /// </summary>
        public string Descripcion
        {
            get { return descripcion != null ? descripcion.Trim() : descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ?  value.Trim() : null;
                }
            }
        }
    }
}