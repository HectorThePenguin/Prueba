USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Folio_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Folio_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[Folio_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Obtiene el folio y actualiza el consecutivo
-- Folio_Obtener 3,1 , Q
-- =============================================
CREATE PROCEDURE [dbo].[Folio_Obtener]
@OrganizacionID INT,
@TipoFolioID	INT,
@Folio BIGINT Output
AS
BEGIN
    SET NOCOUNT ON;		
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
	SET NOCOUNT OFF;
END	

GO
