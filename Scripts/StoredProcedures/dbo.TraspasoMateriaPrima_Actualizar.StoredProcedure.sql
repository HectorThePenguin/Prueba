USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoMateriaPrima_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TraspasoMateriaPrima_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TraspasoMateriaPrima_Actualizar]
@TraspasoMateriaPrimaID int,
@ContratoOrigenID int,
@ContratoDestinoID int,
@FolioTraspaso bigint,
@AlmacenOrigenID int,
@AlmacenDestinoID int,
@InventarioLoteOrigenID int,
@InventarioLoteDestinoID int,
@CuentaSAPID int,
@Justificacion varchar(255),
@AlmacenMovimientoEntradaID bigint,
@AlmacenMovimientoSalidaID bigint,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	IF (@CuentaSAPID = 0) BEGIN	SET @CuentaSAPID = NULL END
	IF (@ContratoOrigenID = 0) BEGIN SET @ContratoOrigenID = NULL END
	IF (@ContratoDestinoID = 0) BEGIN SET @ContratoDestinoID = NULL END
	IF (@InventarioLoteOrigenID = 0) BEGIN SET @InventarioLoteOrigenID = NULL END
	IF (@InventarioLoteDestinoID = 0) BEGIN SET @InventarioLoteDestinoID = NULL END
	SET NOCOUNT ON;
	UPDATE TraspasoMateriaPrima SET
		ContratoOrigenID = @ContratoOrigenID,
		ContratoDestinoID = @ContratoDestinoID,
		FolioTraspaso = @FolioTraspaso,
		AlmacenOrigenID = @AlmacenOrigenID,
		AlmacenDestinoID = @AlmacenDestinoID,
		InventarioLoteOrigenID = @InventarioLoteOrigenID,
		InventarioLoteDestinoID = @InventarioLoteDestinoID,
		CuentaSAPID = @CuentaSAPID,
		Justificacion = @Justificacion,
		AlmacenMovimientoEntradaID = @AlmacenMovimientoEntradaID,
		AlmacenMovimientoSalidaID = @AlmacenMovimientoSalidaID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TraspasoMateriaPrimaID = @TraspasoMateriaPrimaID
	SET NOCOUNT OFF;
END

GO
