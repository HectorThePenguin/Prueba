USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Zona_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Zona_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Zona_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- SpName     : Zona_ObtenerPorDescripcion 'OESTE'  
--=============================================  
CREATE PROCEDURE [dbo].[Zona_ObtenerPorDescripcion]  
@Descripcion VARCHAR(100)  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT b.ZonaID,  
		   b.Descripcion,  
		   b.PaisID,
		   p.Descripcion AS Pais,
		   b.Activo  
	  FROM Zona b
	 INNER JOIN Pais p 
	    ON b.PaisID = p.PaisID
	 WHERE b.Descripcion = @Descripcion  
	SET NOCOUNT OFF;  
END  
GO
