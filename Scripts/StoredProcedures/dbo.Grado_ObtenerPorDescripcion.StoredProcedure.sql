USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grado_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grado_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Grado_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Grado_ObtenerPorDescripcion '2'
--======================================================
CREATE PROCEDURE [dbo].[Grado_ObtenerPorDescripcion]
@Descripcion VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		GradoID
		, Descripcion
		, NivelGravedad		
		, Activo
	FROM Grado
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
