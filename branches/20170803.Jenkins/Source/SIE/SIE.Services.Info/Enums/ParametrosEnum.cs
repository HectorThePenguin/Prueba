namespace SIE.Services.Info.Enums
{
    public enum ParametrosEnum
    {
        diasZigma,
        diasMinimos,
        ubicacionFotosGuardar, //Ruta para almacenar fotografias
        ubicacionFotos, //ubicacion de las fotografias del sistema http
        diasPeriodo, //Identificador de los dias de periodo para la evaluacion de tecnica de deteccion
        porcentajeMatutino, //Porcentaje de alimentacion que se debe de aplicar en los servicios matutinos
        porcentajeVespertino, //Porcentaje de alimentacion que se debe de aplicar en los servicios verpertinos
        ajustePorRedondeo, //Valor para realizar los ajustes por redonde de los repartos de alimento
        DiasAtrasReporteInventario,
        rutaArchivoDatalink, //Ruta para obtener el archivo datalink
        nombreArchivoDatalink,
        rutaGenerarArchivoDatalink, //Ruta para generar el archivo datalink
        nombreGenerarArchivoDatalink, //Nombre del archivo que se genera para datalink
        correoOrigen, //Correo origen
        servidorSmtp, //Servidor smtp
        puerto, //Puerto del servidor smtp
        autentificacion, //password del de la cuenta smtp
        requiereSsl, //Indica si el servidor smtp requiere conexion segura
        correoDestino, //Correo donde se enviaran los correos
        mermapermitidaminima,//Merma minima de flete programado
        mermapermitidamaxima,//Merma maxima de flete programado
        mermapermitidaminimafletesint,//Merma minima permitida para programacion de fletes interna
        mermapermitidamaximafletesint,//Merma maxima permitida para programacion de fletes interna
        CuentaGastoEngordaFijo,
        CuentaGastoAlimentosFijo,
        CuentaGastoFinanciero,
        CuentaGastosCentrosFijos,
        TiposAlmacenProveedor,//contiene los tipos de almacenes que requieren capturarseles el Proveedor
        ProductosForraje,
        CuentaCostosProductosAlmacen,
        CuentaCostosDiesel,
        CTAMERMA,
        CTASUPERAVIT,
        CTASALIDAPRODUCTO,
        RUTAXMLPOLIZA,
        SerieFactura,
        FolioFactura,
        SELLER_ID,
        SHIP_FROM,
        RutaCFDI,
        SubProductosCierreDiaPA,
        CTACENTROBENEFICIOENG,
        CTACENTROBENEFICIOMP,
        CTOBENEFINT,
        CTACENTROCOSTOMP,
        CTACENTROCOSTOENG,
        CTACENTROCTOINT,
        SubProductosCrearContrato,
        CribaEntrada,
        CribaSalida,
        CuentaGastoMateriaPrima,
        PolizaSacrificio212,
        PolizaSacrificio300,
        SociedadPolizaSacrificio,
        CuentaMedicamentoTransito,
        PRODIVAALM,
        PROPESLLEGADA,
        PRODCTACOSTO,
        CORRALFALTPROPIO,
        CORRALFALTDIRECTA,
        CTAFALTANTESOBRANTE,
        GradosBrix,
        GradoPorcentualPerdidaHumedad,
        CorraletaSacrificioMacho, // Parametro para las corraletas dispobibles a sacrificio para machos
        CorraletaSacrificioHembra, // Parametro para las corraletas dispobibles a sacrificio para Hembras
        ProductoDiasTratamiento,
        CTAMERMAENG,
        CTASUPERAVITENG,
        PRODUCTOAJUSTAR,
        ENDPOINTWSSAP,
        USUARIOWSSAP,
        PASSWORDWSSAP,
        GANADERATRASPASAGANADO,
        DESCUENTOGANADOMUERTO,
        MuestraHumedad,
        ProductosMuestraHumedad,
        DiasRecuperacion,
        ScannerArete,
        EstandarTiempo,
        PorcMaximo,
        PorcMinimo,
        CapturaObligatoriaRFID,
	    RecProdAlmRepSAP,
        ApoyoSacrificio,
        MermaMaxima,
        RutaRespaldoDL,
        RecProdAlmRepSAPNac,
        AplicaSalidaSacrificioMarel,
        EJECORDENREP,
        ProducReportPvC, //Este enum sirve para los idproducto del reporte produccion vs consumo,
        TempMaxSilo,
        DIASFACTURACION,
        DigitosIniAreteSK,
        LongitudAreteSK,
        CUENTAPUENTETTO,
        FORMULAUE,
        CORRALSOBRANTE,
        URLvalidacionLimiteCredito, // Url del servicio de validacion de limite de credito en la salida por venta
        FlagValidarLimiteCredito,   // Valor que indica si se debe realizar la validacion de limite de credito en la salida por venta
        UsuarioWebServiceLimiteCredito, //Usuario utilizado para autentificarse con el webservice de validacion de limite de credito 
        PassWebServiceLimiteCredito, //Password utilizado para autentificarse con el webservice de validacion de limite de credito 
        AYUDAENVIOPRODUCTO, // Familias para envio de alimentos
        CuentaInventarioTransito,
        ProductoConRestriccionDescSK, //Descripcion del producto configurado con restriccion de descuento
        PorcentajeMaxDesctoProductoSK,// Porcentaje de descuento para un producto sukarne
        LECTURADOBLEARETE,
        CORRALDESTINOORGANIZACION
    }
}
