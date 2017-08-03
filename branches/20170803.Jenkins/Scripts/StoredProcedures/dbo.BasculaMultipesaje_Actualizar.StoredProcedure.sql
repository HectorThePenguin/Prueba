USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_Actualizar]     ******/
DROP PROCEDURE [dbo].[BasculaMultipesaje_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[BasculaMultipesaje_Actualizar]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pablo Bórquez 
-- Create date: 01/12/2015
-- Description: Actualizar un registro de Báscula multipesaje
--=============================================
CREATE PROCEDURE [dbo].[BasculaMultipesaje_Actualizar]
	@Folio int,
	@PesoBruto int,
	@PesoTara int,
	@FechaPesoBruto DATETIME,
	@FechaPesoTara DATETIME,
	@UsuarioModificacionID int,
	@Observacion VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE BasculaMultipesaje SET
		PesoBruto = @PesoBruto,
		PesoTara = @PesoTara,
		FechaPesoBruto = @FechaPesoBruto,
		FechaPesoTara = @FechaPesoTara,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = CURRENT_TIMESTAMP,
		Observacion = @Observacion,
		Activo = 0
	WHERE Folio = @Folio

	SET NOCOUNT OFF;
END