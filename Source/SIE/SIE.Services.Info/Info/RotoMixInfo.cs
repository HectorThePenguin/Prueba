namespace SIE.Services.Info.Info
{
    public class RotoMixInfo : BitacoraInfo
    {
        /// <summary>
        /// Se utiliza para guardar la organizacionID en la que está "logeado" el usuario
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Hace referencia al identificador Id de la mezclada de fórmulas de alimentos.        
        /// </summary>
        public int RotoMixId { get; set; }

        /// <summary>
        /// Hace referencia al identificador de la mezclada de fórmulas de alimentos.
        /// Tomar este dato de la tabla Rotomix del campo, de la organización del usuario logueao
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Se utiliza para guardar la cantidad de batch que se han realiado por dia y por RotoMix en especifico.
        /// </summary>
        public int Contador { get; set; }
    }
}
