USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Lote_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Gilberto Carranza
Fecha	  : 27/11/2013
Proposito : Crear un Lote
************************************/
CREATE PROCEDURE [dbo].[Lote_Crear]
 @Activo BIT,
 @Cabezas INT,
 @CabezasInicio INT,
 @CorralID INT,
 @DisponibilidadManual BIT, 
 @OrganizacionID INT,
 @TipoCorralID INT,
 @TipoProcesoID INT,
 @UsuarioCreacionID INT,
 @TipoFolioID INT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Lote INT 
	EXEC Folio_Obtener @OrganizacionID, @TipoFolioID, @Folio = @Lote output
	INSERT INTO Lote
	(
		 Activo,
		 Cabezas,
		 CabezasInicio,
		 CorralID,
		 DisponibilidadManual,
		 Lote,
		 OrganizacionID,
		 TipoCorralID,
		 TipoProcesoID,
		 UsuarioCreacionID,
		 FechaCreacion,
		 FechaInicio
	)
	VALUES
	(
		 @Activo,
		 @Cabezas,
		 @CabezasInicio,
		 @CorralID,
		 @DisponibilidadManual,
		 @Lote,
		 @OrganizacionID,
		 @TipoCorralID,
		 @TipoProcesoID,
		 @UsuarioCreacionID,
		 GETDATE(),
		 GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF
END

GO
