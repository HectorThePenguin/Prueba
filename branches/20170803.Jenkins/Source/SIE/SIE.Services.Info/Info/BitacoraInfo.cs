using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class BitacoraInfo
    {
        public BitacoraInfo()
        {
            Activo = EstatusEnum.Activo;
            UsuarioModificacionID = null;
        }

        /// <summary>
        ///      Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        ///     Usario que creo el registro.
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        ///     Usario que modifica el registro .
        /// </summary>
        public int? UsuarioModificacionID { get; set; }
    }
}
