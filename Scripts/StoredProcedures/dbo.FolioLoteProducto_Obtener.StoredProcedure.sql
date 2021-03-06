USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FolioLoteProducto_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FolioLoteProducto_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[FolioLoteProducto_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 15/02/2014
-- Description:  Obtiene el folio del Lote de inventario que se genera por Almacen y productoID
-- Origen: APInterfaces
-- FolioLoteProducto_Obtener 1, 1, Folio 
-- =============================================
CREATE PROCEDURE [dbo].[FolioLoteProducto_Obtener]
	@AlmacenID INT,
	@ProductoID INT,
	@Folio INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;		
	IF NOT EXISTS(SELECT 1
					FROM FolioLoteProducto 
				   WHERE AlmacenID = @AlmacenID 	
					 AND ProductoID = @ProductoID
				 )
		BEGIN
			SET @Folio = 1
			INSERT INTO FolioLoteProducto(AlmacenID,ProductoID,Valor) 
			VALUES(@AlmacenID, @ProductoID, @Folio)
		END
	ELSE
		BEGIN	
			UPDATE f
			   SET Valor = Valor + 1 
			  FROM FolioLoteProducto f
			 WHERE AlmacenID = @AlmacenID 	
			   AND ProductoID = @ProductoID		
			SELECT @Folio = ISNULL(Valor, 1) 
			  FROM FolioLoteProducto
			 WHERE AlmacenID = @AlmacenID 	
			   AND ProductoID = @ProductoID
		END
	SET NOCOUNT OFF;
END	

GO
