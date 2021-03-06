USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Folio_ObtenerPorOrganizacionTipoFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Folio_ObtenerPorOrganizacionTipoFolio]
GO
/****** Object:  StoredProcedure [dbo].[Folio_ObtenerPorOrganizacionTipoFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    José Gilberto Quintero López
-- Create date: 31/10/2013
-- Description:  Obtiene el folio y actualiza el consecutivo
-- Folio_ObtenerPorOrganizacionTipoFolio 1, 19
-- =============================================
CREATE PROCEDURE [dbo].[Folio_ObtenerPorOrganizacionTipoFolio]
@OrganizacionID INT,
@TipoFolioID	INT
AS
BEGIN
    SET NOCOUNT ON;		

		DECLARE @Folio BIGINT
    
		IF NOT EXISTS(Select '' 
			FROM Folio 
			WHERE OrganizacionId = @OrganizacionId 	
				AND TipoFolioID = @TipoFolioID)
			BEGIN
				SET @Folio = 1
				INSERT INTO Folio(OrganizacionID,TipoFolioID,Valor) values(@OrganizacionID, @TipoFolioID, @Folio)
			END
		ELSE
		BEGIN	
				
			UPDATE f
			SET Valor = Valor + 1 
			FROM Folio f
			WHERE OrganizacionId = @OrganizacionId 	
				AND TipoFolioID = @TipoFolioID		
				
			SELECT @Folio = isnull(Valor, 1) 
			FROM Folio
			WHERE OrganizacionId = @OrganizacionId 	
			AND TipoFolioID = @TipoFolioID
		END

		SELECT @Folio AS Folio
			 
	SET NOCOUNT OFF;
END	

GO
