namespace SIE.Services.Info.Info
{
    public class ElementoLista
    {
        /// <summary>
        ///     Identificador del elemento.
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        ///     Descripción del elemento de la lista.
        /// </summary>
        public string Descripcion { set; get; }

        /// <summary>
        ///      Indica si el registro se encuentra Activo
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }
    }
}
