USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoMateriaPrima_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TraspasoMateriaPrima_Crear
--======================================================
CREATE PROCEDURE [dbo].[TraspasoMateriaPrima_Crear] @ContratoOrigenID INT
	,@ContratoDestinoID INT
	,@AlmacenOrigenID INT
	,@AlmacenDestinoID INT
	,@InventarioLoteOrigenID INT
	,@InventarioLoteDestinoID INT
	,@CuentaSAPID INT
	,@Justificacion VARCHAR(255)
	,@AlmacenMovimientoEntradaID BIGINT
	,@AlmacenMovimientoSalidaID BIGINT
	,@Activo BIT
	,@UsuarioCreacionID INT
	,@TipoFolio BIGINT
	,@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@CuentaSAPID = 0) BEGIN	SET @CuentaSAPID = NULL END
	IF (@ContratoOrigenID = 0) BEGIN SET @ContratoOrigenID = NULL END
	IF (@ContratoDestinoID = 0) BEGIN SET @ContratoDestinoID = NULL END
	IF (@InventarioLoteOrigenID = 0) BEGIN SET @InventarioLoteOrigenID = NULL END
	IF (@InventarioLoteDestinoID = 0) BEGIN SET @InventarioLoteDestinoID = NULL END
	DECLARE @FolioTrapaso INT
	EXEC Folio_Obtener @OrganizacionID
		,@TipoFolio
		,@Folio = @FolioTrapaso OUTPUT
	INSERT TraspasoMateriaPrima (
		OrganizacionID
		,ContratoOrigenID
		,ContratoDestinoID
		,FolioTraspaso
		,AlmacenOrigenID
		,AlmacenDestinoID
		,InventarioLoteOrigenID
		,InventarioLoteDestinoID
		,CuentaSAPID
		,Justificacion
		,AlmacenMovimientoEntradaID
		,AlmacenMovimientoSalidaID
		,FechaMovimiento
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	VALUES (
		@OrganizacionID
		,@ContratoOrigenID
		,@ContratoDestinoID
		,@FolioTrapaso
		,@AlmacenOrigenID
		,@AlmacenDestinoID
		,@InventarioLoteOrigenID
		,@InventarioLoteDestinoID
		,@CuentaSAPID
		,@Justificacion
		,@AlmacenMovimientoEntradaID
		,@AlmacenMovimientoSalidaID
		,GETDATE()
		,@Activo
		,GETDATE()
		,@UsuarioCreacionID
		)
	SELECT 
		TraspasoMateriaPrimaID
		,OrganizacionID
		,ContratoOrigenID
		,ContratoDestinoID
		,FolioTraspaso
		,AlmacenOrigenID
		,AlmacenDestinoID
		,InventarioLoteOrigenID
		,InventarioLoteDestinoID
		,CuentaSAPID
		,Justificacion
		,AlmacenMovimientoEntradaID
		,AlmacenMovimientoSalidaID
		,FechaMovimiento
		,Activo
	FROM TraspasoMateriaPrima
	WHERE TraspasoMateriaPrimaID = SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
