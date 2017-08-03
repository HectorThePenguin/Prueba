using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class MezcladoraInfo : BitacoraInfo
    {
        public MezcladoraInfo()
        {
            Activo = Enums.EstatusEnum.Activo;
        }
        /// <summary>
        /// Mezcladora id
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveCalidadMezcladoFormulasAlimento", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        public int mezcladoraID { get; set; }

        /// <summary>
        /// Organizacion id
        /// </summary>
        public int organizaionID { get; set; }

        /// <summary>
        /// Numero economico de la mezcladora
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionCalidadMezcladoFormulasAlimento", EncabezadoGrid = "Descripción",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        public string NumeroEconomico
        {
            get { return numeroEconomico != null ? numeroEconomico.Trim() : numeroEconomico; }
            set
            {
                if (value != numeroEconomico)
                {
                    string valor = value;
                    numeroEconomico = valor == null ? valor : valor.Trim();
                }
            }
        }

        public string numeroEconomico { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        
        public string Descripcion { get; set; }



        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificaciion{ get; set; }
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        public int UsuarioModificacion { get; set; } 
    }
}
