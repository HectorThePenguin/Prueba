USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Familia_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Familia_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Familia_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FamiliaID,
		Descripcion,
		Activo
	FROM Familia
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
