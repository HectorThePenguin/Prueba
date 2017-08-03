IF object_id('dbo.TratamientoCentros_ObtenerTratamientoPorPagina', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TratamientoCentros_ObtenerTratamientoPorPagina
END
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 10/11/2015
-- Description:   
-- SpName     : TratamientoCentros_ObtenerTratamientoPorPagina 0,0,1,1,15   
--======================================================  
CREATE PROCEDURE [dbo].[TratamientoCentros_ObtenerTratamientoPorPagina] 
@OrganizacionID INT,  
@CodigoTratamiento INT,  
@Activo BIT, 
@Inicio INT,
@Limite INT  
AS  
SET NOCOUNT ON  
	DECLARE @Tratamiento AS TABLE (  
		TratamientoID INT,  
		OrganizacionID INT,  
		Organizacion VARCHAR(50),  
		TipoOrganizacionID INT,  
		TipoOrganizacion VARCHAR(50),  
		CodigoTratamiento INT,  
		Descripcion VARCHAR(100),
		Activo BIT,  
		RowNum INT IDENTITY  
	)
  
	DECLARE @TratamientoProducto AS TABLE (  
		TratamientoProductoID INT,
		TratamientoID INT,  
		OrganizacionID INT,
		ProductoID INT,  
		Producto VARCHAR(50),  
		SubFamiliaID INT,  
		SubFamilia VARCHAR(50),  
		FamiliaID INT,  
		Familia VARCHAR(50),  
		Dosis INT,  
		Activo BIT,
		Factor BIT,
		FactorMacho NUMERIC(8,4),
		FactorHembra NUMERIC(8,4)
	)
  
	INSERT INTO @Tratamiento  
	SELECT tr.TratamientoID  
		,o.OrganizacionID  
		,o.Descripcion [Organizacion]  
		,too.TipoOrganizacionID  
		,too.Descripcion [TipoOrganizacion]  
		,tr.CodigoTratamiento
		,tr.Descripcion  
		,tr.Activo
	FROM Sukarne.dbo.CatTratamiento tr  
	INNER JOIN Organizacion o ON tr.OrganizacionID = o.OrganizacionID  
	INNER JOIN TipoOrganizacion too ON o.TipoOrganizacionID = too.TipoOrganizacionID  
	WHERE @OrganizacionID IN (tr.OrganizacionID,0) 
	AND @CodigoTratamiento IN (tr.CodigoTratamiento,0) 
	AND tr.Activo = @Activo
  
	INSERT INTO @TratamientoProducto  
	SELECT 
		tp.TratamientoProductoID,  
		tp.TratamientoID,  
		tp.OrganizacionID,
		pr.ProductoID,  
		pr.Descripcion [Producto],  
		sf.SubFamiliaID,
		sf.Descripcion [SubFamilia],
		fa.FamiliaID,  
		fa.Descripcion [Familia],  
		tp.Dosis,  
		tp.Activo,
		tp.Factor,
		ISNULL(tp.FactorMacho,0),
		ISNULL(tp.FactorHembra,0)  
	FROM Sukarne.dbo.CatTratamientoProducto tp  
	INNER JOIN Sukarne.dbo.CatProducto pr ON pr.ProductoID = tp.ProductoID AND pr.OrganizacionID = tp.OrganizacionID    
	INNER JOIN Sukarne.dbo.CatSubFamilia sf ON sf.SubFamiliaID = pr.SubFamiliaID  
	INNER JOIN Sukarne.dbo.CatFamilia fa ON fa.FamiliaID = sf.FamiliaID  
	INNER JOIN @Tratamiento tr ON tr.TratamientoID = tp.TratamientoID AND tr.OrganizacionID = tp.OrganizacionID
  
	SELECT 
		TratamientoID,  
		OrganizacionID,  
		Organizacion,  
		TipoOrganizacionID,  
		TipoOrganizacion,  
		CodigoTratamiento,
		Descripcion,  
		Activo  
	FROM @Tratamiento  
	WHERE RowNum BETWEEN @Inicio AND @Limite
  
	SELECT COUNT(TratamientoID) AS TotalReg FROM @Tratamiento
  
	SELECT 
		TratamientoProductoID,  
		tp.TratamientoID,  
		ProductoID,  
		Producto,  
		SubFamiliaID,  
		SubFamilia,  
		FamiliaID,  
		Familia,  
		Dosis,  
		tp.Activo,
		Factor,
		FactorMacho,
		FactorHembra,
		tp.OrganizacionID
	FROM @TratamientoProducto tp  
	INNER JOIN @Tratamiento tr ON tp.TratamientoID = tr.TratamientoID AND tp.OrganizacionID = tr.OrganizacionID
	WHERE tr.RowNum BETWEEN @Inicio AND @Limite
  
SET NOCOUNT OFF