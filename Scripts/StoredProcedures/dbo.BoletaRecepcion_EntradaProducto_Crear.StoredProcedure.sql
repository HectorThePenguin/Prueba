USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BoletaRecepcion_EntradaProducto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[BoletaRecepcion_EntradaProducto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[BoletaRecepcion_EntradaProducto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 23/05/2014
-- Description:	Guarda una entrada producto con su detalle
/*BoletaRecepcion_EntradaProducto_Crear '
	<ROOT>
		<EntradaProducto>
			<TipoFolio>9</TipoFolio>
			<ContratoID>3</ContratoID>
			<TipoContratoID>1</TipoContratoID>
			<OrganizacionID>4</OrganizacionID>,
			<ProductoID>1</ProductoID>
			<RegistroVigilanciaID>1</RegistroVigilanciaID>
			<UsuarioCreacionID>1</UsuarioCreacionID>
			<EstatusID>1</EstatusID>
			<UsuarioCreacionID>1</UsuarioCreacionID>
			<IndicadorID>1</IndicadorID>
			<Porcentaje>12</Porcentaje>
			<Descuento>13</Descuento>
			<Rechazo>1</Rechazo>
			<FechaEmbarque>1</FechaEmbarque>
			<FolioOrigen>1</FolioOrigen>
			<EsOrigen>1</EsOrigen>
		</EntradaProducto>
	</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[BoletaRecepcion_EntradaProducto_Crear]
@XMLEntrada XML 
AS
BEGIN
	DECLARE @TablaTMP TABLE(
		TipoFolio INT,
		ContratoID INT,
		TipoContratoID INT,
		OrganizacionID INT,
		ProductoID INT,
		RegistroVigilanciaID INT,
		UsuarioCreacionID INT,
		OperadorIDAnalista INT,
		EstatusID INT,
		IndicadorID INT,
		Porcentaje DECIMAL(10,2),
		Descuento DECIMAL(10,2),
		Rechazo INT,
		FechaEmbarque DATE, 
		FolioOrigen INT,
		EsOrigen INT
	)

	INSERT INTO @TablaTMP 
	(TipoFolio,ContratoID,TipoContratoID,OrganizacionID,ProductoID,RegistroVigilanciaID,
		UsuarioCreacionID,OperadorIDAnalista,EstatusID,IndicadorID,Porcentaje,Descuento,Rechazo, FechaEmbarque, FolioOrigen, EsOrigen)
	SELECT 
		TipoFolio = T.item.value('./TipoFolio[1]', 'INT'),
		ContratoID  = T.item.value('./ContratoID[1]', 'INT'),
		TipoContratoID  = T.item.value('./TipoContratoID[1]', 'INT'),
		OrganizacionID    = T.item.value('./OrganizacionID[1]', 'INT'),
		ProductoID    = T.item.value('./ProductoID[1]', 'INT'),
		RegistroVigilanciaID  = T.item.value('./RegistroVigilanciaID[1]', 'INT'),
		UsuarioCreacionID   = T.item.value('./UsuarioCreacionID[1]','INT'),
		OperadorIDAnalista = T.item.value('./OperadorIDAnalista[1]','INT'),
		EstatusID = T.item.value('./EstatusID[1]','INT'),
		IndicadorID = T.item.value('./IndicadorID[1]','INT'),
		Porcentaje    = T.item.value('./Porcentaje[1]', 'DECIMAL(10,2)'),
		Descuento  = T.item.value('./Descuento[1]', 'DECIMAL(10,2)'),
		Rechazo = T.item.value('./Rechazo[1]', 'INT'),
		FechaEmbarque = T.item.value('./FechaEmbarque[1]', 'DATE'), 
		FolioOrigen = T.item.value('./FolioOrigen[1]', 'INT'),
		EsOrigen = T.item.value('./EsOrigen[1]', 'INT')
	FROM  @XMLEntrada.nodes('ROOT/EntradaProducto') AS T(item)

	DECLARE @OrganizacionID INT
	DECLARE @TipoFolio INT
	DECLARE @EntradaProducto INT
	DECLARE @Folio INT
	SET @OrganizacionID = 0
	SET @TipoFolio = 0
	SET @Folio = 0
	SET @EntradaProducto = 0


	SELECT TOP 1 @TipoFolio = TipoFolio,@OrganizacionID = OrganizacionID
	FROM @TablaTMP

	EXEC Folio_Obtener @OrganizacionID,@TipoFolio,@Folio OUTPUT

	--Inserta Entrada
	INSERT INTO EntradaProducto 
	(PesoBonificacion,ContratoID,TipoContratoID,OrganizacionID,ProductoID,RegistroVigilanciaID,Folio,Fecha,OperadorIDAnalista,EstatusID,
	Activo,FechaCreacion,UsuarioCreacionID, FechaEmbarque, FolioOrigen)
	SELECT TOP 1 0,ContratoID,TipoContratoID,OrganizacionID,ProductoID,RegistroVigilanciaID,@Folio,GETDATE(),OperadorIDAnalista,EstatusID,
	1,GETDATE(),UsuarioCreacionID, FechaEmbarque, FolioOrigen
	FROM @TablaTMP

	--Inserta Detalle
	SET @EntradaProducto = @@IDENTITY

	INSERT INTO EntradaProductoDetalle
	(EntradaProductoID,IndicadorID,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT @EntradaProducto,IndicadorID,1,GETDATE(),UsuarioCreacionID
	FROM @TablaTMP
	GROUP BY IndicadorID,UsuarioCreacionID

	--Inserta Muestra
	INSERT INTO EntradaProductoMuestra
	(EntradaProductoDetalleID,Porcentaje,Descuento,Rechazo,EsOrigen, Activo,FechaCreacion,UsuarioCreacionID)
	SELECT EntradaProductoDetalleID, Porcentaje, Descuento, Rechazo, EsOrigen, 1, GETDATE(),TMP.UsuarioCreacionID
	FROM @TablaTMP TMP
	INNER JOIN EntradaProductoDetalle (NOLOCK) EPD ON (TMP.IndicadorID = EPD.IndicadorID AND EPD.EntradaProductoID = @EntradaProducto) 
	
	SELECT
		EntradaProductoID,
		ContratoID,
		OrganizacionID,
		ProductoID,
		Folio,
		Fecha,
		FechaDestara,
		Observaciones,
		OperadorIDAnalista,
		PesoBonificacion,
		PesoOrigen,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoContratoID,
		EstatusID,
		Justificacion,
		OperadorIDBascula,
		OperadorIDAlmacen,
		OperadorIDAutoriza,
		FechaInicioDescarga,
		FechaFinDescarga,
		AlmacenInventarioLoteID,
		AlmacenMovimientoID,
		Activo,
		RegistroVigilanciaID,
		FechaCreacion,
		UsuarioCreacionID
	FROM EntradaProducto (NOLOCK)
	WHERE EntradaProductoID = @EntradaProducto
END

GO
