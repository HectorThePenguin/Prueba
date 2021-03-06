USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionCorral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EvaluacionCorral_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionCorral_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Marco Zamora
-- Origen:	  APInterfaces
-- Create date: 21/11/2013
-- Description:  Metodo para guardar la Evaluacion de las Partidas de un Corral
-- EvaluacionCorral_Crear 1,1,1,60,0,12,3,0,'Justificacion',4
-- =============================================
CREATE PROCEDURE [dbo].[EvaluacionCorral_Crear]
 @OrganizacionID INT,
 @CorralID INT,
 @LoteID INT,
 @Cabezas  INT,
 @EsMetafilaxia  BIT,
 @OperadorID  INT,
 @NivelGarrapata  INT,
 @Autorizado BIT,
 @Justificacion VARCHAR(255),
 @UsuarioCreacion INT,
 @TipoFolio INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FolioResp INT;
	/* Se obtiene el folio para evaluacion Partida */
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @FolioResp output
	 INSERT INTO EvaluacionCorral(FolioEvaluacion,
	 OrganizacionID,
	 LoteID,
	 FechaEvaluacion, 
	 Cabezas,
	 EsMetafilaxia,
	 OperadorID,
	 NivelGarrapata,
	 MetafilaxiaAutorizada,
	 Justificacion,
	 Activo,
	 FechaCreacion,
	 UsuarioCreacionID)
	 VALUES(@FolioResp,
	 @OrganizacionID,
	 @LoteID,
	 GETDATE(),
	 @Cabezas,
	 @EsMetafilaxia,
	 @OperadorID,
	 @NivelGarrapata,
	 @Autorizado,
	 @Justificacion,
	 1,
	GETDATE(),
	@UsuarioCreacion)
	SELECT @@IDENTITY
END

GO
