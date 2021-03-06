USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chofer_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar un Chofer.
-- Chofer_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[Chofer_ObtenerPorID]	
@ChoferID INT	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ChoferID,
		  Nombre,
		  ApellidoPaterno,
		  ApellidoMaterno,
		  Activo
	FROM Chofer(NOLOCK)
	WHERE ChoferID = @ChoferID
	SET NOCOUNT OFF;
END

GO
