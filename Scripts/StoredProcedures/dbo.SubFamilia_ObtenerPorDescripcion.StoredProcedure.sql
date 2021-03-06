USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SubFamilia_ObtenerPorDescripcion 'Granos', 1
--======================================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerPorDescripcion]
@Descripcion varchar(50),
@FamiliaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FamiliaID,
		SubFamiliaID,
		Descripcion,
		Activo
	FROM SubFamilia
	WHERE Descripcion = @Descripcion
	AND FamiliaID = @FamiliaID
	SET NOCOUNT OFF;
END

GO
