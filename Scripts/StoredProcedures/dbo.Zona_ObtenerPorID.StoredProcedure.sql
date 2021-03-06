USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Zona_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Zona_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Zona_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Audomar Pi�a Aguilar
-- Create date: 2015/04/08  
-- Description: Zona_ObtenerPorID 1  
--=============================================  
CREATE PROCEDURE [dbo].[Zona_ObtenerPorID]  
@ZonaID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT 
		ZonaID = B.ZonaID,  
		Zona = B.Descripcion,
		PaisID = B.PaisID,
		Pais = P.Descripcion
	FROM Zona B
	INNER JOIN Pais P (NOLOCK)
		ON P.PaisID = B.PaisID
	WHERE B.ZonaID = @ZonaID  
	SET NOCOUNT OFF;  
END

GO
