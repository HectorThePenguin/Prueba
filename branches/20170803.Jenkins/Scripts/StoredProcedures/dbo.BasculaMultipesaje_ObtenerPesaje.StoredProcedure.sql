USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_ObtenerPesaje]     ******/
DROP PROCEDURE [dbo].[BasculaMultipesaje_ObtenerPesaje]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_ObtenerPesaje]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pablo Bórquez 
-- Create date: 02/12/2015
-- Description: Obtiene pesaje de Báscula Multipesajes
--  BasculaMultipesaje_ObtenerPesaje 198, 1
-- ============================================= 
CREATE PROCEDURE [dbo].[BasculaMultipesaje_ObtenerPesaje]
	@Folio INT,
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;  
	
	SELECT 
		Folio,
		Chofer,
		Placas,
		PesoBruto,
		PesoTara,
		Fecha,
		FechaPesoTara,
		FechaPesoBruto,
		FechaModificacion,
		UsuarioCreacionID,
		Producto,
		Observacion, 
		EnvioSAP, 
		OperadorID
	FROM
		BasculaMultipesaje
	WHERE 
		Folio = @Folio AND OrganizacionID = @OrganizacionID AND Activo = 1

	SET NOCOUNT OFF;
END

-- select * from BasculaMultipesaje