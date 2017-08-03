USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConceptoDeteccion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConceptoDeteccion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ConceptoDeteccion_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConceptoDeteccion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ConceptoDeteccion_ObtenerPorID]
@ConceptoDeteccionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ConceptoDeteccionID,
		Descripcion,
		Activo
	FROM ConceptoDeteccion
	WHERE ConceptoDeteccionID = @ConceptoDeteccionID
	SET NOCOUNT OFF;
END

GO
