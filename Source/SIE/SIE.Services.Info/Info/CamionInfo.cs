using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class CamionInfo : BitacoraInfo
    {
        private string placaCamion;
        /// <summary> 
        ///	CamionID  
        /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarque", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveBasculaMateriaPrima", MetodoInvocacion = "ObtenerPorProveedorIdCamionId", PopUp = false, EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveSalidaVentaTraspaso", MetodoInvocacion = "ObtenerPorId", PopUp = false, EncabezadoGrid = "Clave")]
        public int CamionID { get; set; }

        /// <summary> 
        ///	ProveedorID  
        /// </summary> 
        public ProveedorInfo Proveedor { get; set; }

        /// <summary> 
        ///	PlacaCamion  
        /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", EncabezadoGrid = "Descripcion", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", MetodoInvocacion = "ObtenerPorInfo", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EncabezadoGrid = "Placa Camión")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", MetodoInvocacion = "ObtenerPorCamion", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBasculaMateriaPrima", MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EncabezadoGrid = "Placa Camión")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaVentaTraspaso", MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EncabezadoGrid = "Descripción")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaVentaTraspaso", MetodoInvocacion = "ObtenerPorCamion", PopUp = false, EncabezadoGrid = "Descripción")]
        public string PlacaCamion
        {
            get { return placaCamion != null ? placaCamion.Trim() : placaCamion; }
            set
            {
                if (value != placaCamion)
                {
                    placaCamion = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary> 
        ///	atributo privado economico  
        /// </summary> 
        private string economico;

        /// <summary> 
        ///	atributo publico economico  
        /// </summary> 
        public string Economico
        {
            get { return economico != null ? economico.Trim() : economico; }
            set
            {
                if (value != economico)
                {
                    economico = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary> 
        ///	atributo que almacena todas las marcas que estan en el catalogo 
        /// </summary> 
        public List<MarcasInfo> MarcasIniciales;

        /// <summary> 
        ///	descripcion de la marca que almacena la marca del registro seleccionado en el grid
        /// </summary> 
        private string marcaDescripcion;

        /// <summary> 
        ///	id de la marca que almacena el id de la marca del registro seleccionado en el grid
        /// </summary> 
        public int? MarcaID { get; set; }

        /// <summary> 
        ///	id de la marca que almacena el id de la marca del registro seleccionado en el grid 
        /// </summary> 
        public string MarcaDescripcion
        {
            get { return marcaDescripcion != null ? marcaDescripcion.Trim() : marcaDescripcion; }
            set
            {
                if (value != marcaDescripcion)
                {
                    marcaDescripcion = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary> 
        ///	almacena el modelo del camion 
        /// </summary> 
        public int? Modelo { get; set; }

        /// <summary> 
        ///	atributo privado para almacenar el color del camion 
        /// </summary> 
        private string color;

        /// <summary> 
        ///	atributo publico para almacenar el color del camion 
        /// </summary>  
        public string Color
        {
            get { return color != null ? color.Trim() : color; }
            set
            {
                if (value != color)
                {
                    color = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary> 
        ///	almacena el check boletinado 
        /// </summary>  
        public bool Boletinado { get; set; }

        /// <summary> 
        ///	atributo privado que almacena el campo de observaciones
        /// </summary>  
        private string observacionesEnviar;

        /// <summary> 
        ///	atributo privado que almacena el campo de observaciones
        /// </summary>  
        private string observacionesObtener;

        /// <summary> 
        ///	atributo publico que almacena el campo de observaciones
        /// </summary>  
        public string ObservacionesEnviar
        {
            get { return observacionesEnviar != null ? observacionesEnviar.Trim() : observacionesEnviar; }
            set
            {
                if (value != observacionesEnviar)
                {
                    observacionesEnviar = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary> 
        ///	atributo publico que almacena el campo de observaciones
        /// </summary>  
        public string ObservacionesObtener
        {
            get { return observacionesObtener != null ? observacionesObtener.Trim() : observacionesObtener; }
            set
            {
                if (value != observacionesObtener)
                {
                    observacionesObtener = value != null ? value.Trim() : null;
                }
            }
        }
    }
}
