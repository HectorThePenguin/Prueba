using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class SalidaGanadoMuertoInfo
    {
        /// <summary>
        /// Identificador de la muerte
        /// </summary>
        public int MuerteID { get; set; }
        /// <summary>
        /// Identifica el folio de la orden de salida por muerte
        /// </summary>
        public int FolioSalida { get; set; }
        /// <summary>
        /// Contiene el tipo de folio a conseguir
        /// </summary>
        public int TipoFolio { get; set; }
        /// <summary>
        /// Contiene el corral al que pertenecia
        /// </summary>
        public string CodigoCorral { get; set; }
        /// <summary>
        /// Contiene el arete del animal
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Contiene el arete testigo del animal
        /// </summary>
        public string AreteTestigo { get; set; }
        /// <summary>
        /// Contiene el sexo del animal
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Contiene el tipo de ganado del animal 
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Contiene el peso del animal
        /// </summary>
        public int Peso { get; set; }
        /// <summary>
        /// Contiene la causa de muerte para el animal
        /// </summary>
        public string Causa { get; set; }
        /// <summary>
        /// Contiene la Organizacion a consumir folio
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Contiene el usuario que esta haciendo la modificacion
        /// </summary>
        public int UsuarioModificacionID { get; set; }
    }

}
