USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Producto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_Producto]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Producto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eduardo Cota
-- Create date: 16-05-2014
-- Description:	Otiene listado de productos para el modulo -Materias Prima/Vigilancia-
-- Vigilancia_Producto 'FO', 1, 1, 15,'<ROOT><Familia>1</Familia><Familia>6</Familia></ROOT>',6
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_Producto]
	@Descripcion NVARCHAR(50),
	@Activo BIT,    
	@Inicio INT,
	@Limite INT,
	@ParametroDescripcion VARCHAR(50),
	@FamiliaXML XML,
	@SubFamiliaXML XML
AS    
BEGIN    
	SET NOCOUNT ON; 
    /* Familias aceptadas */
	CREATE TABLE #FamiliasAceptadas
	(
		FamiliaID INT
	)
	INSERT INTO #FamiliasAceptadas
	SELECT DISTINCT T.ITEM.value('./Familia[1]','INT') AS FamiliaID
	FROM @FamiliaXML.nodes('/ROOT/Familias') as T(ITEM)
	/* SubFamilias aceptadas */
	CREATE TABLE #SubFamiliasAceptadas
	(
		SubFamiliaID INT
	)
	INSERT INTO #SubFamiliasAceptadas
	SELECT DISTINCT T.ITEM.value('./SubFamilia[1]','INT') AS SubFamiliaID
	FROM @SubFamiliaXML.nodes('/ROOT/SubFamilias') as T(ITEM)
	DECLARE @SubProductos as TABLE
	(
		ProductoID INT primary key
	)
	DECLARE @Valor AS VARCHAR(255)
	SELECT @Valor = Valor 
	FROM ParametroGeneral PG
	INNER JOIN Parametro P
		ON P.ParametroID = PG.ParametroID
	WHERE P.Clave = @ParametroDescripcion
	INSERT INTO @SubProductos(ProductoID)
	SELECT * 
	FROM dbo.FuncionSplit(@Valor, '|')
	SELECT     
		ROW_NUMBER() OVER ( ORDER BY P.ProductoID ASC) AS RowNum,    
		P.ProductoID,
		P.Descripcion,
		P.Activo
	INTO #ProductoVigilancia
	FROM Producto P
	INNER JOIN SubFamilia SB ON SB.SubFamiliaID = P.SubFamiliaID AND SB.Activo = 1
	INNER JOIN Familia F ON F.FamiliaID = SB.FamiliaID AND F.Activo = 1
	WHERE P.descripcion LIKE '%'+@Descripcion+'%' 
	AND ((SB.SubFamiliaID IN (SELECT SubFamiliaID FROM #SubFamiliasAceptadas) OR 
		 F.FamiliaID IN (SELECT FamiliaID FROM #FamiliasAceptadas))
		 OR P.ProductoID IN (SELECT ProductoID FROM @SubProductos))
	AND P.Activo = @Activo;
	SELECT
		   P.ProductoID,
		   P.Descripcion,
		   P.Activo     
	  FROM #ProductoVigilancia P      
     WHERE RowNum BETWEEN @Inicio AND @Limite    
  ORDER BY P.ProductoID
 SELECT     
  COUNT(ProductoID)AS TotalReg     
 FROM #ProductoVigilancia
END

GO
