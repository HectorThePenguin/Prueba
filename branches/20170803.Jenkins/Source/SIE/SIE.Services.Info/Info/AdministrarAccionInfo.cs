using System;

namespace SIE.Services.Info.Info
{
   public class AdministrarAccionInfo : BitacoraInfo
    {
       // se hereda de la clase BitacoraInfo 
       //Activo, UsuarioCreacionID, UsuarioModificacionID

       // ID de la acción
        public int AccionID { get; set; }
       // Descripcion de la acción.
        public String Descripcion { get; set; }
    }
}
