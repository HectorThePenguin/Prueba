IF OBJECT_ID('dbo.CacRecepcionProducto_ValidarAretes', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.CacRecepcionProducto_ValidarAretes
END
GO
--==================================================================================  
-- Author     : Sergio Gamez
-- Create date: 26/01/2016
-- Description: Validar si los aretes relacionados ya han sido capturados previamente
-- SpName     : CacRecepcionProducto_ValidarAretes '',1
-- Notas	  : TipoArete: Sukarne/RFID = 1, Nacional/Siniga = 2
--================================================================================== 
CREATE PROCEDURE [dbo].[CacRecepcionProducto_ValidarAretes]  
@Aretes Xml,    
@OrganizacionId INT
AS      
BEGIN      
SET NOCOUNT ON
	
	SELECT      
		NumeroArete = t.item.value('./NumeroArete[1]', 'BIGINT'),  
		TipoArete = t.item.value('./TipoArete[1]', 'INT')      
	INTO #Aretes    
	FROM @Aretes.nodes('ROOT/DATOS') AS T(item) 
	
	CREATE TABLE #Arete(Arete bigint) 

	INSERT INTO #Arete(Arete)
	-- Validamos los aretes sukarne, estos se pueden repetir, pero no por organizacion ni aunque etsen inactivos
	SELECT NumeroAreteSukarne 
	FROM [Sukarne].[dbo].[CatAreteSukarne] A (NOLOCK)
	INNER JOIN #Aretes B
		ON A.NumeroAreteSukarne = B.NumeroArete 
	WHERE A.OrganizacionId = @OrganizacionId AND B.TipoArete = 1

	UNION
	
	-- Validamos los aretes sinigas, estos no se pueden repetir ni aunque sean organizaciones distintas o esten inactivos
	SELECT NumeroAreteNacional 
	FROM [Sukarne].[dbo].[CatAreteNacional] A (NOLOCK)
	INNER JOIN #Aretes B
		ON A.NumeroAreteNacional = B.NumeroArete
	WHERE B.TipoArete = 2

	SELECT TOP 1 Arete FROM #Arete

SET NOCOUNT OFF        
END