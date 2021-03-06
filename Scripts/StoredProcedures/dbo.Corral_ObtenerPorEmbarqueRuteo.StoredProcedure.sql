USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorEmbarqueRuteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorEmbarqueRuteo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorEmbarqueRuteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer	
-- Create date: 24-12-2013
-- Description:	Obtiene el corral de un embarque de ruteo 
-- Corral_ObtenerPorEmbarqueRuteo 2, 4
-- =============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorEmbarqueRuteo]
	@FolioEmbarque INT, 
	@OrganizacionID INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		EG.CorralID, 
		C.Codigo
		Into #Datos
	FROM Embarque E
	INNER JOIN EntradaGanado EG 
		ON E.EmbarqueID = EG.EmbarqueID 
	INNER JOIN Corral C	
		ON C.CorralID = EG.CorralID 
	WHERE E.FolioEmbarque = @FolioEmbarque
		AND E.OrganizacionID = @OrganizacionID
	GROUP BY E.EmbarqueID, 
			 E.OrganizacionID,
			 EG.CorralID, 
			 C.Codigo	
	select 
		a.CorralID,
		a.Codigo,
		c.Capacidad		
	From #Datos a
	inner join Corral c	on c.CorralID = a.CorralID
	Drop table #Datos
END

GO
