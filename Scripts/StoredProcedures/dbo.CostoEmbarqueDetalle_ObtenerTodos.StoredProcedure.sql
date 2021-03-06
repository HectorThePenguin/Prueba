USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener todos los Costos Embarques Detalle
-- CostoEmbarqueDetalle_ObtenerTodos 1
--=============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerTodos]
@Activo BIT = NULL	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CostoEmbarqueDetalleID
		,EmbarqueDetalleID
		,CostoID
		,Importe
		,Activo
	FROM CostoEmbarqueDetalle
		WHERE (
			Activo = @Activo
			OR @Activo IS NULL
			)	
	SET NOCOUNT OFF;
END

GO
