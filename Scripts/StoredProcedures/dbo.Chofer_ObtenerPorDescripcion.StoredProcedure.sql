USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chofer_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 08/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Chofer_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Chofer_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ChoferID,
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		Activo
	FROM Chofer
	WHERE Nombre = @Descripcion
	SET NOCOUNT OFF;
END

GO
