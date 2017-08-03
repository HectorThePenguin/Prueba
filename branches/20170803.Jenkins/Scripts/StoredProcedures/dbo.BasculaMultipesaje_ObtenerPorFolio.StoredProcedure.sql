USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_ObtenerPorFolio]     ******/
DROP PROCEDURE [dbo].[BasculaMultipesaje_ObtenerPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_ObtenerPorFolio]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pablo Bórquez 
-- Create date: 01/12/2015
-- Description: Obtiene un registro de Báscula Multipesaje por Folio
-- =============================================  
CREATE PROCEDURE [dbo].[BasculaMultipesaje_ObtenerPorFolio]
	@Folio INT
AS
BEGIN
	SET NOCOUNT ON;  

	SELECT 
		Folio,
		Chofer,
		Producto
	FROM
		BasculaMultipesaje
	WHERE
		Folio = @Folio AND
		Activo = 1;

	SET NOCOUNT OFF;  
END