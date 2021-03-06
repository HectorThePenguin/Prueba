USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pais_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pais_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Pais_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================    
-- Author     : Audomar Piña Aguilar  
-- Create date: 2015/04/08    
-- Description: Pais_ObtenerPorID 1  
--=============================================    
CREATE PROCEDURE [dbo].[Pais_ObtenerPorID]    
@PaisID INT    
AS    
BEGIN    
	SET NOCOUNT ON;    
  
	SELECT B.PaisID,    
		   B.Descripcion,  
	       B.DescripcionCorta  
	  FROM Pais B    
	 WHERE B.PaisID = @PaisID    
  
	SET NOCOUNT OFF;    
END 
GO
