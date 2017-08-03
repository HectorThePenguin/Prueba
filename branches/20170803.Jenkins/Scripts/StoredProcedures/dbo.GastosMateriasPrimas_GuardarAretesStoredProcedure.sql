IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'GastosMateriasPrimas_GuardarAretes' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[GastosMateriasPrimas_GuardarAretes]
END 
GO
--==========================================================================================================
-- Author     : Sergio Gamez  
-- Create date: 04/08/2016  
-- Description: Guardar aretes en registro de gastos de materias primas [Entradas y Salidas]
-- SpName     : GastosMateriasPrimas_GuardarAretes 
--				'<ROOT><DATOS><Arete>484090100123456</Arete></DATOS></ROOT>', 85, 0, 0  
--==========================================================================================================
CREATE PROCEDURE dbo.GastosMateriasPrimas_GuardarAretes  
@XML XML,  
@OrganizacionId INT,
@ProductoId INT,
@EsAreteSukarne BIT,
@EsEntradaAlmacen BIT,
@Cantidad FLOAT,
@Importe FLOAT,
@UsuarioId INT
AS      
BEGIN       
SET NOCOUNT ON
	
	CREATE TABLE #Aretes(Arete BIGINT)
	
	IF @Cantidad > 0
	BEGIN
		INSERT INTO #Aretes(Arete)
		SELECT    
			Arete = t.item.value('./Arete[1]', 'BIGINT') 
		FROM @XML.nodes('ROOT/DATOS') AS T(item)	
	END
	
	
	IF @EsEntradaAlmacen = 1
	BEGIN
		--SE QUITA ESTA PARTE PORQUE SERA LA APLICACION EN EL IPAD QUIEN HAGA ESTA ACTUALIZACION AL SINCRONIZAR
		--UPDATE Sukarne.dbo.CatAlmacenInventario
		--SET Cantidad = Cantidad + @Cantidad,
		--	Importe = Importe + @Importe,
		--	FechaModificacion = GETDATE(),
		--	UsuarioModificacionID = @UsuarioId 
		--WHERE OrganizacionID = @OrganizacionId AND ProductoID = @ProductoId
	
		IF @EsAreteSukarne = 1
		BEGIN		
			INSERT INTO Sukarne.dbo.CatAreteSukarne(NumeroAreteSukarne,OrganizacionId,Activo,FechaCreacion,UsuarioCreacionID,Replicado)  
			SELECT DISTINCT A.Arete, @OrganizacionId, 1, GETDATE(), @UsuarioId, 0 
			FROM #Aretes A
			LEFT JOIN Sukarne.dbo.CatAreteSukarne CAS
			ON CAS.NumeroAreteSukarne = A.Arete AND CAS.OrganizacionId = @OrganizacionId
			WHERE CAS.NumeroAreteSukarne IS NULL
			
			UPDATE CAS
			SET Activo = 1
			FROM #Aretes A
			INNER JOIN Sukarne.dbo.CatAreteSukarne CAS
			ON CAS.NumeroAreteSukarne = A.Arete AND CAS.OrganizacionId = @OrganizacionId
		END
		ELSE
		BEGIN		
			INSERT INTO Sukarne.dbo.CatAreteNacional(NumeroAreteNacional,OrganizacionId,Activo,FechaCreacion,UsuarioCreacionID)
			SELECT DISTINCT A.Arete, @OrganizacionId, 1, GETDATE(), @UsuarioId 
			FROM #Aretes A
			LEFT JOIN Sukarne.dbo.CatAreteNacional CAN
			ON CAN.NumeroAreteNacional = A.Arete AND CAN.OrganizacionId = @OrganizacionId
			WHERE CAN.NumeroAreteNacional IS NULL
			
			UPDATE CAN
			SET Activo = 1
			FROM #Aretes A
			INNER JOIN Sukarne.dbo.CatAreteNacional CAN
			ON CAN.NumeroAreteNacional = A.Arete AND CAN.OrganizacionId = @OrganizacionId
		END
	END
	ELSE
	BEGIN
		--SE QUITA ESTA PARTE PORQUE SERA LA APLICACION EN EL IPAD QUIEN HAGA ESTA ACTUALIZACION AL SINCRONIZAR
		--UPDATE Sukarne.dbo.CatAlmacenInventario
		--SET Cantidad = Cantidad - @Cantidad,
		--	Importe = Importe - @Importe,
		--	FechaModificacion = GETDATE(),
		--	UsuarioModificacionID = @UsuarioId 
		--WHERE OrganizacionID = @OrganizacionId AND ProductoID = @ProductoId
	
		IF @EsAreteSukarne = 1
		BEGIN
			UPDATE CAS 
			SET Activo = 0
			FROM Sukarne.dbo.CatAreteSukarne CAS
			INNER JOIN #Aretes A
			ON A.Arete = CAS.NumeroAreteSukarne AND CAS.OrganizacionID = @OrganizacionId			
		END
		ELSE
		BEGIN	
			UPDATE CAN
			SET Activo = 0	
			FROM Sukarne.dbo.CatAreteNacional CAN
			INNER JOIN #Aretes A
			ON A.Arete = CAN.NumeroAreteNacional AND CAN.OrganizacionID = @OrganizacionId	
		END
	END
	
	DROP TABLE #Aretes
	  
 SET NOCOUNT OFF        
END