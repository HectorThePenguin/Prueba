using System;

namespace SIE.Services.Info.Modelos
{
    public class ImpresionTarjetaRecepcionModel
    {
        /// <summary>
        /// id de la entrada de ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }

        /// <summary>
        /// folio de la entrada
        /// </summary>
        public string FolioEntrada { get; set; }

        /// <summary>
        /// Descripcion de la nomenclatura del formato
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Nombre del encargado de generar la entrada de ganado
        /// </summary>
        public string Vigilante { get; set; }
        /// <summary>
        /// Hora de entrada del ganado
        /// </summary>
        public string HoraVigilancia { get; set; }
        /// <summary>
        /// Organizacion de donde viene el ganado
        /// </summary>
        public string Origen { get; set; }
        /// <summary>
        /// Operador que trae el ganado
        /// </summary>
        public string Operador { get; set; }
        /// <summary>
        /// Placa de la jaula donde venia el ganado
        /// </summary>
        public string PlacaJaula { get; set; }
        /// <summary>
        /// Peso bruto de la entrada
        /// </summary>
        public decimal PesoBruto { get; set; }
        /// <summary>
        /// Peso tara de la entrada
        /// </summary>
        public decimal PesoTara { get; set; }
        /// <summary>
        /// Peso neto de la entrada
        /// </summary>
        public decimal PesoNeto { get; set; }
        /// <summary>
        /// Hora de pesaje de la entrada
        /// </summary>
        public string HoraBascula { get; set; }
        /// <summary>
        /// Usuario que recibio el ganado
        /// </summary>
        public string UsuarioRecibio { get; set; }
        /// <summary>
        /// Fecha de entrada
        /// </summary>
        public DateTime FechaEntrada { get; set; }
        /// <summary>
        /// Cabezas recibidas
        /// </summary>
        public int CabezasRecibidas { get; set; }
        /// <summary>
        /// Cabezas actuales del lote
        /// </summary>
        public int CabezasCorral { get; set; }
        /// <summary>
        /// Corral donde se realizo la entrada del ganado
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Indica si el ganado se manejo sin estres
        /// </summary>
        public bool ManejoSinEstres { get; set; }
        /// <summary>
        /// Imagen para manejo sin estres
        /// </summary>
        public byte[] ManejoSinEstresImg { get; set; }
        /// <summary>
        /// Indica si se recibio la guia
        /// </summary>
        public bool Guia { get; set; }
        /// <summary>
        /// Imagen para guia
        /// </summary>
        public byte[] GuiaImg { get; set; }
        /// <summary>
        /// Indica si se recibio la factura
        /// </summary>
        public bool Factura { get; set; }
        /// <summary>
        /// Imagen para factura
        /// </summary>
        public byte[] FacturaImg { get; set; }
        /// <summary>
        /// Indica si se recibio la poliza
        /// </summary>
        public bool Poliza { get; set; }
        /// <summary>
        /// Imagen para Poliza
        /// </summary>
        public byte[] PolizaImg { get; set; }
        /// <summary>
        /// Indica si se recibio la hoja de embarque
        /// </summary>
        public bool HojaEmbarque { get; set; }
        /// <summary>
        /// Imagen para hoja de embarque
        /// </summary>
        public byte[] HojaEmbarqueImg { get; set; }
        
    }
}
