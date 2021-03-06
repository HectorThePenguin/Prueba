USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerCantidadBatch]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerCantidadBatch]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerCantidadBatch]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eduardo Cota
-- Create date: 24/11/2014
-- Description: 
-- SpName     : ProduccionFormula_ObtenerCantidadBatch 1, 1
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerCantidadBatch] 
@organizacionID int,
@rotoMix int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
		Batch
	FROM
		ProduccionFormulaBatch
	WHERE
		(organizacionID = @organizacionID) and 
		(RotoMixID = @rotoMix) and 
		(Cast(FechaCreacion as date) = cast (getdate() as date))
	ORDER BY 
		Batch DESC 
	SET NOCOUNT OFF;
END

GO
