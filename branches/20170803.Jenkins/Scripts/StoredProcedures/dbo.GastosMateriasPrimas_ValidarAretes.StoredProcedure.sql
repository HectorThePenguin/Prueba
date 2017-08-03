IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'GastosMateriasPrimas_ValidarAretes' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[GastosMateriasPrimas_ValidarAretes]
END 
GO
--==========================================================================================================
-- Author     : Sergio Gamez  
-- Create date: 25/08/2015  
-- Description: Consultar aretes por productos en registro de gastos de materias primas [Entradas y Salidas]
-- SpName     : GastosMateriasPrimas_ValidarAretes 
--				'<ROOT><DATOS><Arete>484090100123456</Arete></DATOS></ROOT>', 85, 0, 0  
--==========================================================================================================
CREATE PROCEDURE dbo.GastosMateriasPrimas_ValidarAretes  
@XML XML,  
@OrganizacionId INT,
@EsAreteSukarne BIT,
@EsEntradaAlmacen BIT
AS      
BEGIN       
SET NOCOUNT ON

	SELECT    
		Arete = t.item.value('./Arete[1]', 'BIGINT') 
	INTO #Aretes  
	FROM @XML.nodes('ROOT/DATOS') AS T(item)
	
	IF @EsEntradaAlmacen = 1
	BEGIN
		IF @EsAreteSukarne = 1
		BEGIN		
			SELECT CAST(NumeroAreteSukarne AS VARCHAR(20)) AS Arete
			FROM [Sukarne].[dbo].[CatAreteSukarne] (NOLOCK)   
			WHERE NumeroAreteSukarne IN (SELECT Arete FROM #Aretes (NOLOCK)) AND OrganizacionId = @OrganizacionId AND Activo = 1	
		END
		ELSE
		BEGIN		
			SELECT CAST(NumeroAreteNacional AS VARCHAR(20)) AS Arete     
			FROM [Sukarne].[dbo].[CatAreteNacional] (NOLOCK)   
			WHERE NumeroAreteNacional IN (SELECT Arete FROM #Aretes (NOLOCK)) AND OrganizacionId = @OrganizacionId AND Activo = 1
		END
	END
	ELSE
	BEGIN
		IF @EsAreteSukarne = 1
		BEGIN
			SELECT CAST(Arete AS VARCHAR(20)) AS Arete
			FROM #Aretes A
			LEFT JOIN [Sukarne].[dbo].[CatAreteSukarne] S (NOLOCK)
			ON S.OrganizacionId = @OrganizacionId AND S.NumeroAreteSukarne = A.Arete AND S.Activo = 1
			WHERE S.NumeroAreteSukarne IS NULL					
		END
		ELSE
		BEGIN		
			SELECT CAST(Arete AS VARCHAR(20)) AS Arete
			FROM #Aretes A
			LEFT JOIN [Sukarne].[dbo].[CatAreteNacional] S (NOLOCK)
			ON S.OrganizacionId = @OrganizacionId AND S.NumeroAreteNacional = A.Arete AND S.Activo = 1 
			WHERE S.NumeroAreteNacional IS NULL		
		END
	END
	
	DROP TABLE #Aretes
	  
 SET NOCOUNT OFF        
END