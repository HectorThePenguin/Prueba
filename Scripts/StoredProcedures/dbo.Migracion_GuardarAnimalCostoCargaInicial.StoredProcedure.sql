USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarAnimalCostoCargaInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarAnimalCostoCargaInicial]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarAnimalCostoCargaInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 31/01/2015
-- Description: Guarda informacion en la tabla AnimalCostoCargaInicial
-- Origen: APInterfaces
-- Migracion_GuardarAnimalCostoCargaInicial 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarAnimalCostoCargaInicial]
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	DECLARE @CostoID INT;
	DECLARE @Descripcion VARCHAR(50);
	DECLARE @Ceros VARCHAR(3) = '000';
	DECLARE @sql NVarChar(2000)
	
	/* Cursor para los costos */
	DECLARE curCostos CURSOR FOR 		
	 SELECT CostoID, Descripcion
	   FROM Costo 
	  WHERE CostoID IN (1,2,3,4,5,6,7,8,9,10,12,14,15,16,17,18,19,20,21,22,24,31,32,28,29,38
						 ,11,13,23,25,26,27,30,33,34);
	
	/* Se crea la tabla de AnimalCostoCargaInicial */
	CREATE TABLE [dbo].[AnimalCostoCargaInicial] (
		[Arete] bigint NULL ,
		[FechaCosto] datetime NOT NULL ,
		[CostoID] int NOT NULL ,
		[FolioReferencia] int NOT NULL ,
		[Importe] float(53) NULL ,
		[FechaCreacion] datetime NOT NULL ,
		[UsuarioCreacionID] int NOT NULL ,
		[FechaModificacion] int NULL ,
		[UsuarioModificacionID] int NULL ,
		[TipoReferencia] int NOT NULL 
		) ON [PRIMARY];

	-- Apertura del cursor de los costos
	OPEN curCostos
	-- Lectura de la primera fila del cursor
		FETCH NEXT FROM curCostos INTO @CostoID, @Descripcion
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
			
			SET @sql= CONCAT(' INSERT INTO AnimalCostoCargaInicial ',
							' SELECT ',
								' am.Arete, ',
								' FechaCosto = GETDATE(), ',
								' CostoID = ', @CostoID ,', ',
								' FolioReferencia = 0, ',
								' Importe = ROUND(r1.[', @Descripcion ,'],2), ',
								' FechaCreacion = GETDATE(), ',
								' UsuarioCreacionID = 1, ',
								' FechaModificacion = null, ',
								' UsuarioModificacionID = null, ',
								' TipoReferencia = 0 ',
							' FROM AnimalCargaInicial a',
							' INNER JOIN AnimalMovimientoCargaInicial am ',
									' ON a.Arete = am.Arete ' ,
							' INNER JOIN Corral c ',
									' ON c.CorralID = am.CorralID AND c.OrganizacionID = ' , @OrganizacionID,
							' INNER JOIN RESUMEN r1 ',
								' ON r1.CORRAL = c.Codigo',
								' AND r1.Organizacion = 3 AND r1.[' , @Descripcion ,'] > 0 ',
								/*	' ON RIGHT(LTRIM(RTRIM(''',@Ceros,''' + r1.CORRAL)),3) = RIGHT(LTRIM(RTRIM(''',@Ceros,''' + c.Codigo)),3) ',
									' AND r1.Organizacion = ' , @OrganizacionID ,' AND r1.[' , @Descripcion ,'] > 0 ', */
							' WHERE am.TipoMovimientoID = 5; ');
			
				EXEC sp_executesql @sql

				FETCH NEXT FROM curCostos INTO @CostoID, @Descripcion
			END
		-- Cierre del cursor
		CLOSE curCostos
	-- Liberar los recursos
	DEALLOCATE curCostos

	SET NOCOUNT OFF
  END


GO
