USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VigilanciaProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VigilanciaProducto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[VigilanciaProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 16/05/2014
-- Description:  Obtener listado de Productos
-- VigilanciaProducto_ObtenerPorID 80
-- =============================================
CREATE PROCEDURE [dbo].[VigilanciaProducto_ObtenerPorID]
	@ProductoID INT,
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
		pr.ProductoID,
		pr.Descripcion,
		pr.Activo
      FROM Producto pr
	 INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	 INNER JOIN Familia f on sf.FamiliaID = f.FamiliaID
     WHERE pr.ProductoID = @ProductoID
	   AND ((sf.SubFamiliaID IN (SELECT SubFamiliaID FROM #SubFamiliasAceptadas) OR 
		    f.FamiliaID IN (SELECT FamiliaID FROM #FamiliasAceptadas)) 
			OR P.ProductoID IN (SELECT ProductoID FROM @SubProductos))
	   AND pr.Activo = 1
      SET NOCOUNT OFF;
  END

GO
