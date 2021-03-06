USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FolioAlmacen_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FolioAlmacen_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[FolioAlmacen_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 15/02/2014
-- Description:  Obtiene el folio del almacen por tipo de movimiento y actualiza el consecutivo
-- Origen: APInterfaces
-- FolioAlmacen_Obtener 1, 1, Folio 
-- =============================================
CREATE PROCEDURE [dbo].[FolioAlmacen_Obtener]
	@AlmacenID INT,
	@TipoMovimientoID INT,
	@Folio BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;		
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
		BEGIN	
			UPDATE f
			   SET Valor = Valor + 1 
			  FROM FolioAlmacen f
			 WHERE AlmacenID = @AlmacenID 	
			   AND TipoMovimientoID = @TipoMovimientoID		
			SELECT @Folio = ISNULL(Valor, 1) 
			  FROM FolioAlmacen
			 WHERE AlmacenID = @AlmacenID 	
			   AND TipoMovimientoID = @TipoMovimientoID
		END
	SET NOCOUNT OFF;
END	

GO
