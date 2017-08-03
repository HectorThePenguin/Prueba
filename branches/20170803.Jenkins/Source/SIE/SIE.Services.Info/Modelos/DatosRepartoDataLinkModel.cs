using System;

namespace SIE.Services.Info.Modelos
{
    public class DatosRepartoDataLinkModel
    {
        [Atributos.AtributoDataLink(IndiceInicio = 1, Longitud = 6, TipoDato = TypeCode.String)]
        public string NumeroCamion { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 10, Longitud = 1, TipoDato = TypeCode.String)]
        public string IngredienteCorral { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 14, Longitud = 1, TipoDato = TypeCode.Int32)]
        public int TipoServicio { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 15, Longitud = 3, TipoDato = TypeCode.Int32)]
        public int NumeroReparto { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 19, Longitud = 6, TipoDato = TypeCode.String)]
        public string CodigoCorral { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 26, Longitud = 6, TipoDato = TypeCode.Int32)]
        public int RacionFormula { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 33, Longitud = 6, TipoDato = TypeCode.Int32)]
        public int KilosEmbarcados { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 33, Longitud = 6, TipoDato = TypeCode.Int32)]
        public int KilosProgramados { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 40, Longitud = 6, TipoDato = TypeCode.Int32)]
        public int KilosServidos { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 47, Longitud = 8, TipoDato = TypeCode.Int32)]
        public int Operador { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 56, Longitud = 5, TipoDato = TypeCode.String)]
        public string Hora { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 62, Longitud = 1, TipoDato = TypeCode.Int32)]
        public int FormatoFecha { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 64, Longitud = 8, TipoDato = TypeCode.String)]
        public string Fecha { get; set; }

        [Atributos.AtributoDataLink(IndiceInicio = 96, Longitud = 6, TipoDato = TypeCode.Int32)]
        public int PesoBruto { get; set; }

        public int OrdenRegistro { get; set; }
        
        public DateTime FechaReparto { get; set; }

        public string NumeroTolva { get; set; }
    }
}