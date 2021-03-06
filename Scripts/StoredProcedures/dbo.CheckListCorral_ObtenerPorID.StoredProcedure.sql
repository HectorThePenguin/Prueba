USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListCorral_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListCorral_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CheckListCorral_ObtenerPorID]
@CheckListCorralID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CheckListCorralID,
		OrganizacionID,
		LoteID,
		PDF,
		Activo
	FROM CheckListCorral
	WHERE CheckListCorralID = @CheckListCorralID
	SET NOCOUNT OFF;
END

GO
