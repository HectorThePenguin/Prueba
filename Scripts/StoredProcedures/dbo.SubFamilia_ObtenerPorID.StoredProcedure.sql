USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/01/14
-- Description: Obtiene una Subfamilia
-- SubFamilia_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerPorID]
@SubFamiliaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT SB.FamiliaID
		,  SB.SubFamiliaID
		,  SB.Descripcion AS DescripcionSubFamilia
		,  SB.Activo
		,  F.Descripcion AS DescripcionFamilia
	FROM SubFamilia SB
	INNER JOIN Familia F
		ON (SB.FamiliaID = F.FamiliaID)
	WHERE SB.SubFamiliaID = @SubFamiliaID
	SET NOCOUNT OFF;
END

GO
