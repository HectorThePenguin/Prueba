USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Familia_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/01/14
-- Description: Obtiene una Familia por ID
-- Familia_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[Familia_ObtenerPorID] @FamiliaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT FamiliaID
		,Descripcion
		,Activo
	FROM Familia
	WHERE FamiliaID = @FamiliaID
	SET NOCOUNT OFF;
END

GO
