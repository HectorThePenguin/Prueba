USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/11/04
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[Embarque_Crear]
	@OrganizacionID INT,
	@TipoEmbarqueID TINYINT,
	@Estatus TINYINT,
	@UsuarioCreacionID INT,
	@TipoFolio INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorFolio int 
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
	INSERT Embarque(
		OrganizacionID,
		FolioEmbarque,
		TipoEmbarqueID,
		Estatus,
		FechaCreacion,
		UsuarioCreacionID		
	)
	VALUES(
		@OrganizacionID,
		@ValorFolio,
		@TipoEmbarqueID,
		@Estatus,
		GETDATE(),
		@UsuarioCreacionID	
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
