USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para insertar un Cost Embarque Detalle
-- 
--=============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_Crear]
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
	INSERT CostoEmbarqueDetalle(
		EmbarqueDetalleID,
		CostoID,
		Importe,
		Activo,
		Orden,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	)
	VALUES(
		@EmbarqueDetalleID,
		@CostoID,
		@Importe,
		@Activo,
		@Orden,
		@FechaCreacion,
		@UsuarioCreacionID,
		@FechaModificacion,
		@UsuarioModificacionID	
	)
	SET NOCOUNT OFF;
END

GO
