/* Tipo parametro */
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Dispositivo bascula', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Dispositivo termometro', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Imagenes', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('AlmacenID ligada a la trampa', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Tratamiento temperatura', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Orden sacrificio', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('CheckList de Tecnica Deteccion', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Orden de raparto alimentacion', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Intervalo Fecha Reporte Inventario', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Descargar archivo datalink', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Generar archivo datalink', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Configuracion smtp', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Dispositivo Lector RFID', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Merma Permitida Programacion de fletes', 1, GETDATE(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Gastos al inventario', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('InterfazComprasWEB', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Codigo Arete Color en Reimplante', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Parametros Generales', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Productos para Boleta de Recepcion Forraje', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('InterfazControlPiso', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Codigo Arete Blanco Entrada Enfermeria', 1, getdate(), 1);		
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Parametros Polizas', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Facturacion', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('AnalisisReporteDiaDiaCalidad', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('CONFIGURACION CENTRO COSTO', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('CONFIGURACION CENTRO BENEFICIO', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Criba Permitida', 1, getdate(), 1);
INSERT INTO [TipoParametro] ([Descripcion], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES ('Cálculo PNA Origen', 1, getdate(), 1);

/* Parametro */
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'puerto', 'puerto', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'baudrate', 'baudrate', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'databits', 'databits', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'paridad', 'paridad', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'bitstop', 'bitstop', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'espera', 'espera', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 'Captura Manual Bascula', 'CapturaManual', 1, getdate(), 1);
/* Parametro */
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'puerto', 'puerto', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'baudrate', 'baudrate', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'databits', 'databits', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'paridad', 'paridad', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'bitstop', 'bitstop', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'espera', 'espera', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 'Captura Manual Termometro', 'CapturaManual', 1, getdate(), 1);
/* Parametro */
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (3, 'Lectura de Imagenes', 'ubicacionFotos', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (3, 'Guardar Imagenes', 'ubicacionFotosGuardar', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (4, 'El ID del Almacen que esta ligada a la Trampa', 'AlmacenIDTrampa', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (5, 'Tratamiento que se debe aplicar por temperatura', 'codigoTratamientoTemperatura', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (5, 'Temperatura para activar el tratamiento', 'temperaturaAnimal', 1, getdate(), 1)

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (6, 'Dias de espera para sacrificio', 'diasZilmax', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (6, 'Rango permitido para sacrificio', 'diasMinimos', 1, getdate(), 1)

--Parameetros Tipo 7 de Checklist de tecnica de deteccion
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (7, 'Dias de periodo de evaluacion', 'diasPeriodo', 1, getdate(), 1)
--parametros orden reparto alimentacion
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (8, 'Porcentaje servicio matutino', 'porcentajeMatutino', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (8, 'Porcentaje servicio vespertino', 'porcentajeVespertino', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (8, 'Ajuste Por Redondeo', 'ajustePorRedondeo', 1, getdate(), 1)
--parametros reporte inventario
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (9, 'Dias hacia el pasado reporte de inventario', 'DiasAtrasReporteInventario', 1, getdate(), 1)
--Parametros Descargar archivo datalink
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (10, 'Ruta donde se encuentra el archivo datalink', 'rutaArchivoDatalink', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (10, 'Nombre del archivo datalink', 'nombreArchivoDatalink', 1, getdate(), 1)
--Parametros Generar archivo datalink
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (11, 'Ruta donde se generara el archivo datalink', 'rutaGenerarArchivoDatalink', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (11, 'Nombre del archivo datalink', 'nombreGenerarArchivoDatalink', 1, getdate(), 1)

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (12, 'Correo Origen', 'correoOrigen', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (12, 'Servidor Smtp', 'servidorSmtp', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (12, 'Puerto', 'puerto', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (12, 'Autentificacion', 'autentificacion', 1, getdate(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (12, 'Requiere ssl', 'requiereSsl', 1, getdate(), 1)

--Parametros para merma permitida de programaciond de fletes 
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (14, 'Merma minima', 'mermapermitidaminima', 1, GETDATE(), 1)
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (14, 'Merma maxima', 'mermapermitidamaxima', 1, GETDATE(), 1)
--Parametros para merma permitida de programaciond de fletes interna

/* Parametro de configuracion del lector RFID*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'puerto', 'puerto', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'baudrate', 'baudrate', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'databits', 'databits', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'paridad', 'paridad', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'bitstop', 'bitstop', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'espera', 'espera', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 'Captura Manual Termometro', 'CapturaManual', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (15, 'Cuenta Gasto', 'CuentaGasto', 1, getdate(), 1);

/* Parametro de configuracion de la conexion de compras de recepcion productos*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (16, 'ServidorBD', 'ServidorBD', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (16, 'Nombre de la BD', 'NombreBD', 1, getdate(), 1);

/* Parametro de configuracion del codigo de arete de color reimplante*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (17, 'Codigo Arete Color de Reimplante', 'codigoAreteColorReimplante', 1, getdate(), 1);

/* Parametros Generales*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (18, 'Tipos de Almacen que requieren Proveedor', 'TiposAlmacenProveedor', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (19, 'Productos para boleta de recepción forraje', 'ProductosForraje', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (20, 'Nombre del servidor de Control de piso', 'ServidorSPI', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (20, 'Nombre de la base de datos de Control de piso', 'BaseDatosSPI', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (21, 'Codigo Arete Blanco Entrada Enfermeria', 'CodigoAreteBlancoEntEnfermeria', 1, getdate(), 1);

/*Parametros Poliza*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (22, 'Cuenta de Costos de Cargo de Productos Almacen', 'CuentaCostosProductosAlmacen', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (22, 'Cuenta de Costos de Cargo para Diesel', 'CuentaCostosDiesel', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (22, 'CUENTA DE MERMA', 'CTAMERMA', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (22, 'CUENTA DE SUPERAVIT', 'CTASUPERAVIT', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (22, 'CUENTA POLIZA SALIDA PRODUCTO', 'CTASALIDAPRODUCTO', 1, getdate(), 1);

/* Parametros para el tipo Gastos al Inventario tipo 15 */
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (15, 'Cuenta de Gastos de Engorda Fijo', 'CuentaGastoEngordaFijo', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (15, 'Cuenta de Gastos de Alimento Fijo', 'CuentaGastoAlimentosFijo', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (15, 'Cuenta de Gastos de Financieros', 'CuentaGastoFinanciero', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (20, 'Porcentaje Ajuste de Peso de Salida', 'AjustePesoSalida', 1, getdate(), 1);

/* Facturacion */
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (23, 'SerieFactura', 'SerieFactura', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (23, 'FolioFactura', 'FolioFactura', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (23, 'SELLER_ID', 'SELLER_ID', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (23, 'SHIP_FROM', 'SHIP_FROM', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (23, 'RutaCFDI', 'RutaCFDI', 1, getdate(), 1);

/*Parametros Poliza*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (22, 'RUTA DEL ARCHIVO XML', 'RUTAXMLPOLIZA', 1, getdate(), 1);

/*Parametro Reporte Dia a Dia Calidad*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Almidon en heces', 'AnalisisAlmidonHeces', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Disponibilidad de almidón maiz blanco', 'AnalisisDispAlmidonMaizBlanco', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Disponibilidad de almidon maiz amarillo', 'AnalisisDispAlmidonMaizAma', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Gelatinizacion maiz blanco', 'AnalisisGelatinizacionMaizBco', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Gelatinizacion maiz amarillo', 'AnalisisGelatinizacionMaizAma', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Ard. maiz blanco', 'AnalisisArdMaizBlanco', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Ard. maíiz amarillo', 'AnalisisArdMaizAmarillo', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'At maiz blanco', 'AnalisisAtMaizBlanco', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'At maiz amarillo', 'AnalisisAtMaizAmarillo', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (24, 'Eficiencia mezclado', 'AnalisisEficienciaMezclado', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (25, 'CUENTA DEL CENTRO DE COSTO ENGORDA', 'CTACENTROCOSTOENG', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (25, 'CUENTA CENTRO COSTO MP', 'CTACENTROCOSTOMP', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (26, 'CUENTA CENTRO BENEFICIO ENGORDA', 'CTACENTROBENEFICIOENG', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (26, 'CUENTA CENTRO BENEFICIO MP', 'CTACENTROBENEFICIOMP', 1, getdate(), 1);

/*Parametro General*/
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID])
VALUES (18, 'SubProductos Validos, Cierre Dia PA', 'SubProductosCierreDiaPA', 1, getdate(), 1);
 
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (15, 'Cuenta de Gastos de Centros Fijos', 'CuentaGastosCentrosFijos', 1, getdate(), 1);
 
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID])
VALUES (18, 'SubProductos Validos, Crear Contrato', 'SubProductosCrearContrato', 1, getdate(), 1);

INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (27, 'CribaEntrada', 'CribaEntrada', 1, getdate(), 1);
INSERT INTO [Parametro] ([TipoParametroID], [Descripcion], [Clave], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (27, 'CribaSalida', 'CribaSalida', 1, getdate(), 1);

INSERT INTO Parametro(TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
VALUES	(15,'Cuenta de Gastos de Materia Prima','CuentaGastoMateriaPrima',1,getdate(),1)

INSERT INTO Parametro (TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
 VALUES (18,'Productos total cabezas enfermeria','ProductosEnfermeriaTotal',1,GETDATE(),1)

INSERT INTO Parametro (TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
 VALUES (28,'GradosBrix','GradosBrix',1,GETDATE(),1)
INSERT INTO Parametro (TipoParametroID,Descripcion,Clave,Activo,FechaCreacion,UsuarioCreacionID)
 VALUES (28,'GradoPorcentualPerdidaHumedad','GradoPorcentualPerdidaHumedad',1,GETDATE(),1)
 
/* Parametro Trampa*/
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (1, 1, 'COM3', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (2, 1, '9600', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (3, 1, '7', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (4, 1, 'None', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (5, 1, 'One', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (6, 1, '5', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (7, 1, 'false', 1, getdate(), 1);
/* Parametro Trampa*/
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (8, 1, 'COM4', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (9, 1, '9600', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (10, 1, '8', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (11, 1, 'None', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (12, 1, 'One', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (13, 1, '5', 1, getdate(), 1);
INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (14, 1, 'false', 1, getdate(), 1);
/* Parametro Organizacion*/
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (15, 4, 'http://localhost/SIAP_Fotos/MON/', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (16, 4, 'c:\\SIAP_Fotos\\MON\\', 1, getdate(), 1);

INSERT INTO [ParametroTrampa] ([ParametroID], [TrampaID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (17, 1, '1', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (18, 4, '26', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (19, 4, '37.5', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (20, 4, '30', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (21, 4, '4', 1, getdate(), 1);
--Parametro de dias del periodo de evaluacion de tecnica de deteccion para la organizacion
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (22, 4, '5', 1, getdate(), 1);

--Parametro de porcentaje de alimentacion para orden de reparto
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (23, 4, '40', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (24, 4, '60', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (25, 4, '5', 1, getdate(), 1);
------------ REPORTE DE INVENTARIO --------------
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (26, 4, '90', 1, getdate(), 1);
---Descargar archivo datalink
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (27, 4, 'c:\\SIAP\\datalink\\', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (28, 4, 't2w_fed.dat', 1, getdate(), 1);
---Generar descargar datalink
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (29, 4, 'c:\\SIAP\\generardatalink\\', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (30, 4, 'w2t_pen.dat', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (31, 4, 'notificaciones.siap@sukarne.com', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (32, 4, 'srv-adfsst.gviz.com', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (33, 4, '25', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (34, 4, '50P0Rt3', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (35, 4, 'false', 1, getdate(), 1);
--Parametros organizacion para mermas permitidas de programacion de fletes
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (36, 4, '0.10', 1, GETDATE(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (37, 4, '0.50', 1, GETDATE(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (45, 1, '002003', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (45, 4, '002003', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (46, 1, 'SRV-DBCLNP', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (47, 1, 'COMPRAS', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (48, 1, '142', 1, getdate(), 1);

/* Parametros Polizas */
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (54, 1, '5001015003', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (55, 1, '6103600400', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (56, 1, '5003007001', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (57, 1, '5003007002', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (58, 1, '5001008001', 1, getdate(), 1);
INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (59, 1, '', 1, getdate(), 1);

--Parametros Generales
INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (49, '7', 1, getdate(), 1);

INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (50, '109|110|111|112|113|114', 1, getdate(), 1);

INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (93, '24|29|41|52', 1, getdate(), 1);

INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (98, '85', 1, getdate(), 1);

INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (99, '0.0116', 1, getdate(), 1);

INSERT INTO [ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) VALUES (53, 1, '133', 1, getdate(), 1);

/*  Parámetros para la interfaz a SPI */ 
/*  Culiacán */ 
INSERT INTO [dbo].[ParametroOrganizacion] ( [ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (51, 1, 'SRV-ENGDQ'  , 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ( [ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (52, 1, 'SIAP', 1,GETDATE(),1)

/*  Monterrey */ 
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (51, 3, 'SRVPPISOMTY', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (52, 3, 'BDBasculas_SL', 1,GETDATE(),1)

/*  Monarca */ 
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (51, 4, 'SRVPPISOMON', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (51, 4, 'BDBasculas_SL', 1,GETDATE(),1)

/* Parametros Organizacion */
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (59, 4, '6108800100', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (60, 4, '6108800300', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (61, 4, '6108800500', 1,GETDATE(),1)

/* Parametros Interfaz de SPI */
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (62, 1, '3', 1,GETDATE(),1)

/* Parametros Facturacion */
/* CULIACAN */
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (63, 1, 'A', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (64, 1, '0', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (65, 1, '300001', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (66, 1, '50002', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (67, 1, 'I:\siStemas\cfds', 1,GETDATE(),1)
/* MEXICALI */
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (63, 2, 'B', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (64, 2, '0', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (67, 2, 'I:\siStemas\cfds', 1,GETDATE(),1)
/* MONTERREY */
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (63, 3, 'C', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (64, 3, '0', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (67, 3, 'I:\siStemas\cfds', 1,GETDATE(),1)
/* MONARCA MICHOACAN */
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (63, 4, 'D', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (64, 4, '0', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (67, 4, 'I:\siStemas\cfds', 1,GETDATE(),1)

-- Parametros para informacion de analisis reporte dia a dia de calidad
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (69, 1, 'Maiz Amarillo Max 3.5%', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (70, 1, '', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (71, 1, '68% MIN', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (72, 1, '', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (73, 1, '> 20%', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (74, 1, '35-55', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (75, 1, '35-55', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (76, 1, '> 60', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (77, 1, '> 60', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (78, 1, 'Min 90%', 1,GETDATE(),1)

INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (79, 1, '2121000902', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (80, 1, '2121000714', 1,GETDATE(),1)

INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (81, 1, '2121020008', 1,GETDATE(),1)
INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (82, 1, '2121010001', 1,GETDATE(),1)

/* Parametro General, SubProductos */
INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID])
VALUES (83, '201|209|210', 1, getdate(), 1);

INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (84, 1, '6108800200', 1,GETDATE(),1)

INSERT INTO [ParametroGeneral] ([ParametroID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID])
VALUES (85, '118|200|201|209|210', 1, getdate(), 1);

INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (86, 1, '100', 1,GETDATE(),1)

INSERT INTO [dbo].[ParametroOrganizacion] ([ParametroID], [OrganizacionID], [Valor], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (87, 1, '50', 1,GETDATE(),1)

insert into ParametroOrganizacion(ParametroID,OrganizacionID,Valor,Activo,FechaCreacion,UsuarioCreacionID)
VALUES (88,1,'6108800900',1,getdate(),1)