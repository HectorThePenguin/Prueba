USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Concepto_ObtenerListadoDeConceptos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Concepto_ObtenerListadoDeConceptos]
GO
/****** Object:  StoredProcedure [dbo].[Concepto_ObtenerListadoDeConceptos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Ramses Santos
-- Create date: 2014-02-17
-- Origen: APInterfaces
-- Description:	Obtiene el listado de conceptos.
-- EXEC Concepto_ObtenerListadoDeConceptos
--=============================================
CREATE PROCEDURE [dbo].[Concepto_ObtenerListadoDeConceptos]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ConceptoDeteccionID, Descripcion FROM Conceptodeteccion WHERE Activo = 1 ORDER BY ConceptoDeteccionID
	SET NOCOUNT OFF;
END

GO
