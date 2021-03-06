USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para Actualizar un Costo Embarque Detalle
-- 
--=============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_Actualizar]
	@CostoEmbarqueDetalleID int,
	@EmbarqueDetalleID int,
	@CostoID int,
	@Importe decimal,
	@Activo bit,
	@Orden int,
	@FechaCreacion smalldatetime,
	@UsuarioCreacionID int,
	@FechaModificacion smalldatetime,
	@UsuarioModificacionID int	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE CostoEmbarqueDetalle SET 
			EmbarqueDetalleID = @EmbarqueDetalleID,
			CostoID = @CostoID,
			Importe = @Importe,
			Activo = @Activo,
			Orden = @Orden,
			FechaCreacion = @FechaCreacion,
			UsuarioCreacionID = @UsuarioCreacionID,
			FechaModificacion = @FechaModificacion,
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE CostoEmbarqueDetalleID = @CostoEmbarqueDetalleID
		SET NOCOUNT OFF;
END

GO
