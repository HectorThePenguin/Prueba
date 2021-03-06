USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CatAreteSukarne_Insertar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CatAreteSukarne_Insertar]
GO
/****** Object:  StoredProcedure [dbo].[CatAreteSukarne_Insertar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sergio Gamez
-- Create date: 30/07/2015
-- Description: Insertar aretes sukarne por productos solicitados del almac�n
-- SpName     : CatAreteSukarne_Insertar ''
--====================================================== 
CREATE PROCEDURE [dbo].[CatAreteSukarne_Insertar]  
	@XML Xml,  
	@OrganizacionId INT,  
	@UsuarioId INT  
AS      
BEGIN      
	SET NOCOUNT ON  
	SELECT    
		NumeroAreteSukarne = t.item.value('./NumeroAreteSukarne[1]', 'BIGINT')  
	INTO #Aretes  
	FROM @XML.nodes('ROOT/DATOS') AS T(item)  
	SELECT NumeroAreteSukarne   
	INTO #AretesExistentes  
	FROM [Sukarne].[dbo].[CatAreteSukarne] (NOLOCK)   
	WHERE NumeroAreteSukarne IN (SELECT NumeroAreteSukarne FROM #Aretes (NOLOCK)) AND OrganizacionId = @OrganizacionId  
	IF EXISTS(SELECT '' FROM #AretesExistentes (NOLOCK))  
	BEGIN  
		SELECT NumeroAreteSukarne FROM #AretesExistentes (NOLOCK)  
	END  
	ELSE  
	BEGIN  
		INSERT INTO [Sukarne].[dbo].[CatAreteSukarne](NumeroAreteSukarne,OrganizacionId,Activo,FechaCreacion,UsuarioCreacionID)  
		SELECT NumeroAreteSukarne, @OrganizacionId, 0, GETDATE(), @UsuarioId FROM #Aretes  
	END  
	SET NOCOUNT OFF        
END

GO
