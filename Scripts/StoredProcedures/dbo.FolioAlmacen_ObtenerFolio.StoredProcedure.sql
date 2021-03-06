USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FolioAlmacen_ObtenerFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FolioAlmacen_ObtenerFolio]
GO
/****** Object:  StoredProcedure [dbo].[FolioAlmacen_ObtenerFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 30/06/2014
-- Description:  Obtiene el folio del almacen por tipo de movimiento y/o crea el Folio Almacen
-- FolioAlmacen_ObtenerFolio 2, 14, 0 
-- =============================================
CREATE PROCEDURE [dbo].[FolioAlmacen_ObtenerFolio]
	@AlmacenID INT,
	@TipoMovimientoID INT,
	@Folio BIGINT OUTPUT
AS
BEGIN
	IF NOT EXISTS(SELECT 1
					FROM FolioAlmacen 
				   WHERE AlmacenID = @AlmacenID 	
					 AND TipoMovimientoID = @TipoMovimientoID
				 )
		BEGIN
			SET @Folio = 1
			INSERT INTO FolioAlmacen(AlmacenID,TipoMovimientoID,Valor) 
			VALUES(@AlmacenID, @TipoMovimientoID, @Folio)
		END	
		ELSE
		begin 				
			SET @Folio = (Select ISNULL(Valor, 1) 
			  FROM FolioAlmacen
			 WHERE AlmacenID = @AlmacenID 	
			   AND TipoMovimientoID = @TipoMovimientoID)			   
		end
	select @Folio AS Folio	
END	

GO
