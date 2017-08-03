using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
	public class AlmacenUsuarioInfo : BitacoraInfo
	{
		/// <summary> 
		///	AlmacenUsuarioID  
		/// </summary> 
		public int AlmacenUsuarioID { get; set; }

		/// <summary> 
		///	AlmacenID  
		/// </summary> 
		public AlmacenInfo Almacen { get; set; }

		/// <summary> 
		///	UsuarioID  
		/// </summary> 
		public UsuarioInfo Usuario { get; set; }

        /// <summary> 
        ///	Entidad Organización  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary> 
        ///	indica si la propiedad tiene cambios
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool TieneCambios { get; set; }

        public AlmacenUsuarioInfo Clone()
        {
            var usuarioClone = new AlmacenUsuarioInfo
            {
                AlmacenUsuarioID = AlmacenUsuarioID,
                Almacen = Almacen,
                Organizacion = Organizacion,
                Usuario = Usuario,
                Activo = Activo,
                UsuarioCreacionID = UsuarioCreacionID,
                UsuarioModificacionID = UsuarioModificacionID
            };
            return usuarioClone;
        }
	}
}
