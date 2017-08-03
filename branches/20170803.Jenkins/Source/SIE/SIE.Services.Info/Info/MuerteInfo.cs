using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class MuerteInfo
    {
        /// <summary>
        /// Identificador de la organizacion para la muerte
        /// </summary>
        public int OrganizacionId { get; set; }
        /// <summary>
        /// Codigo del Corral al que pertenece la cabeza
        /// </summary>
        public string CorralCodigo { get; set; }
        /// <summary>
        /// Codigo del lote
        /// </summary>
        public string LoteCodigo { get; set; }
        /// <summary>
        /// Id del Corral
        /// </summary>
        public int CorralId { get; set; }
        /// <summary>
        /// Id del registro de muerte
        /// </summary>
        public int MuerteId { get; set; }
        /// <summary>
        /// Numero de arete del animal
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Numero de arete metalico del animal
        /// </summary>
        public string AreteMetalico { get; set; }
        /// <summary>
        /// Observaciones a la muerte
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Identificador del lote
        /// </summary>
        public int LoteId { get; set; }
        /// <summary>
        /// Identificador que prepresenta al operador de deteccion
        /// </summary>
        public int OperadorDeteccionId { get; set; }
  		/// <summary>
        /// Datos del operador detector
        /// </summary>
        public OperadorInfo OperadorDeteccionInfo { get; set; }
        /// <summary>
        /// Fecha de deteccion de la muerte
        /// </summary>
        public DateTime FechaDeteccion { get; set; }
        /// <summary>
        /// Retorna la fecha deteccion como caena
        /// </summary>
        public string FechaDeteccionString {
            get
            {
                string regreso = String.Empty;
                if(FechaDeteccion.Year != 1900)
                    regreso = FechaDeteccion.ToShortDateString();
                return regreso;
            }
        }

        /// <summary>
        /// Retorna la fecha deteccion como caena
        /// </summary>
        public string HoraDeteccionString
        {
            get
            {
                string regreso = String.Empty;

                if (FechaDeteccion.Year != 1900)
                    regreso = FechaDeteccion.ToShortTimeString();
                return regreso;
                
            }
        }

        /// <summary>
        /// Foto de deteccion de la muerte
        /// </summary>
        public string FotoDeteccion { get; set; }
        /// <summary>
        /// Identificador del operador que recolector
        /// </summary>
        public int OperadorRecoleccionId { get; set; }

        /// <summary>
        /// Identificador del operador que recolector
        /// </summary>
        public OperadorInfo OperadorRecoleccionInfo { get; set; }

        /// <summary>
        /// Fecha de Recoleccion de la muerte
        /// </summary>
        public DateTime FechaRecoleccion { get; set; }
        /// <summary>
        /// Retorna la fecha Recoleccion como caena
        /// </summary>
        public string FechaRecoleccionString
        {
            get
            {
                string regreso = String.Empty;

                if (FechaRecoleccion.Year != 1900)
                    regreso = FechaRecoleccion.ToShortDateString();
                return regreso;
            }
        }
        /// <summary>
        /// Hora de recoleccion en formato cadena
        /// </summary>
        public string HoraRecoleccionString
        {
            get
            {
                string regreso = String.Empty;

                if (FechaRecoleccion.Year != 1900)
                    regreso = FechaRecoleccion.ToShortTimeString();
                return regreso;

            }
        }
        /// <summary>
        /// Identificador del operador que realiza la salida de necropsia
        /// </summary>
        public int OperadorNecropsiaId { get; set; }
        /// <summary>
        /// Fecha de la necropsia
        /// </summary>
        public DateTime FechaNecropsia { get; set; }
        /// <summary>
        /// Retorna la fecha necropsia como caena
        /// </summary>
        public string FechaNecropsiaString
        {
            get
            {
                string regreso = String.Empty;

                if (FechaNecropsia.Year != 1900)
                    regreso = FechaNecropsia.ToShortDateString();
                
                return regreso;
            }
        }
        /// <summary>
        /// Foto de la necropsia
        /// </summary>
        public string FotoNecropsia { get; set; }
        /// <summary>
        /// Identificador del operador que realiza la cancelacion
        /// </summary>
        public int OperadorCancelacion { get; set; }
        /// <summary>
        /// Identificador del operador que realiza la cancelacion
        /// </summary>
        public OperadorInfo OperadorCancelacionInfo { get; set; }
        /// <summary>
        /// Fecha de la cancelacion
        /// </summary>
        public DateTime FechaCancelacion { get; set; }
        /// <summary>
        /// Retorna la fecha Cancelacion como caena
        /// </summary>
        public string FechaCancelacionString
        {
            get { return FechaCancelacion.ToShortDateString(); }
        }
        /// <summary>
        /// Motivo de la cancelacion de la muerte
        /// </summary>
        public string MotivoCancelacion { get; set; }
        /// <summary>
        /// Identificador del estado del registro
        /// </summary>
        public int EstatusId { get; set; }
        /// <summary>
        /// Identificador del problema de la muerte
        /// </summary>
        public int ProblemaId { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Peso registrado del ganado
        /// </summary>
        public int Peso { get; set; }

        /// <summary>
        /// Identificador del Animal
        /// </summary>
        public long AnimalId { get; set; }

        /// <summary>
        /// Identificador del Usuario
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        
        /// <summary>
        /// Comentarios de la suerte
        /// </summary>
        public string Comentarios { get; set; }

        /// <summary>
        /// Objeto del Corral al que pertenece la cabeza
        /// </summary>
        public CorralInfo Corral { get; set; }

        /// <summary>
        /// Folio de Salida
        /// </summary>
        public int FolioSalida { get; set; }
    }
}
