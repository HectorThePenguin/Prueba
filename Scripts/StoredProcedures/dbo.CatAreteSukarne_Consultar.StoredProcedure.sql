USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CatAreteSukarne_Consultar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CatAreteSukarne_Consultar]
GO
/****** Object:  StoredProcedure [dbo].[CatAreteSukarne_Consultar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sergio Gamez
-- Create date: 25/08/2015
-- Description: Consultar aretes sukarne por productos solicitados del almac�n
-- SpName     : CatAreteSukarne_Consultar ''
--====================================================== 
CREATE PROCEDURE [dbo].[CatAreteSukarne_Consultar]
	@XML Xml,
	@OrganizacionId INT
AS    
BEGIN    
	SET NOCOUNT ON
	SELECT		
		NumeroAreteSukarne = t.item.value('./NumeroAreteSukarne[1]', 'BIGINT'),
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	INTO #Aretes
	FROM @XML.nodes('ROOT/DATOS') AS T(item)
	SELECT NumeroAreteSukarne 
	FROM [Sukarne].[dbo].[CatAreteSukarne] (NOLOCK) 
	WHERE NumeroAreteSukarne IN (SELECT NumeroAreteSukarne FROM #Aretes (NOLOCK)) AND OrganizacionId = @OrganizacionId
	SET NOCOUNT OFF      
END

GO
