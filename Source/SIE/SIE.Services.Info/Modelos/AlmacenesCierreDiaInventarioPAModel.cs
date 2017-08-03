namespace SIE.Services.Info.Modelos
{
    public class AlmacenesCierreDiaInventarioPAModel
    { 
        /// <summary>
        /// Id del Almacen
        /// </summary>
        public int AlmacenID { set; get; }

        /// <summary>
        /// Id del Tipo del Movimiento
        /// </summary>
        public int TipoAlmacenID { set; get; }

        /// <summary>
        /// Descripcion del Almacen
        /// </summary>
        public string Almacen { set; get; }
    }
}
