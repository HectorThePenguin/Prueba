USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetalleLectorRegistro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerDetalleLectorRegistro]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetalleLectorRegistro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-03-27
-- Origen: APInterfaces
-- Description:	Obtiene el detalle de un  lector registro
-- EXEC Reparto_ObtenerDetalleLectorRegistro 1, 1
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerDetalleLectorRegistro]
	@LectorRegistroID INT,
	@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LectorRegistroDetalleID,
        LectorRegistroID,
        TipoServicioID,
        FormulaIDAnterior,
        FormulaIDProgramada
	FROM LectorRegistroDetalle
	WHERE LectorRegistroID = @LectorRegistroID
	AND Activo = @Activo
	SET NOCOUNT OFF;
END

GO
