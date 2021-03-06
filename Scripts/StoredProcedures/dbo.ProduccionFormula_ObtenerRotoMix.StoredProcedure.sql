USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerRotoMix]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerRotoMix]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerRotoMix]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eduardo Cota
-- Create date: 21/11/2014
-- Description: 
-- SpName     : ProduccionFormula_ObtenerRotoMix 1
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerRotoMix] 
@organizacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		Descripcion,
		RotoMixID
	FROM
		Rotomix
	WHERE
		OrganizacionID = @organizacionID
	SET NOCOUNT OFF;
END

GO
