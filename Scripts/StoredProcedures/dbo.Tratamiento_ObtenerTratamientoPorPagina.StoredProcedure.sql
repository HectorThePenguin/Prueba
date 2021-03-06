USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerTratamientoPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_ObtenerTratamientoPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerTratamientoPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014
-- Description: 
-- SpName     : Tratamiento_ObtenerTratamientoPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_ObtenerTratamientoPorPagina] @OrganizacionID INT
	,@CodigoTratamiento INT
	,@TipoTratamientoID INT
	,@Activo BIT
	,@Inicio INT
	,@Limite INT
AS
SET NOCOUNT ON
DECLARE @Tratamiento AS TABLE (
	TratamientoID INT
	,OrganizacionID INT
	,[Organizacion] VARCHAR(50)
	,TipoOrganizacionID INT
	,TipoOrganizacion VARCHAR(50)
	,CodigoTratamiento INT
	,TipoTratamientoID INT
	,[TipoTratamiento] VARCHAR(50)
	,Sexo CHAR
	,RangoInicial INT
	,RangoFinal INT
	,Activo BIT
	,RowNum INT IDENTITY
	)
DECLARE @TratamientoProducto AS TABLE (
	TratamientoProductoID INT
	,TratamientoID INT
	,ProductoID INT
	,Producto VARCHAR(50)
	,SubFamiliaID INT
	,SubFamilia VARCHAR(50)
	,FamiliaID INT
	,Familia VARCHAR(50)
	,Dosis INT
	,Activo BIT
	)
INSERT INTO @Tratamiento
SELECT tr.TratamientoID
	,o.OrganizacionID
	,o.Descripcion [Organizacion]
	,too.TipoOrganizacionID
	,too.Descripcion [TipoOrganizacion]
	,tr.CodigoTratamiento
	,tt.TipoTratamientoID
	,tt.Descripcion [TipoTratamiento]
	,tr.Sexo
	,tr.RangoInicial
	,tr.RangoFinal
	,tr.Activo
FROM Tratamiento tr
INNER JOIN Organizacion o ON tr.OrganizacionID = o.OrganizacionID
INNER JOIN TipoOrganizacion too ON o.TipoOrganizacionID = too.TipoOrganizacionID
INNER JOIN TipoTratamiento tt ON tr.TipoTratamientoID = tt.TipoTratamientoID
WHERE @OrganizacionID IN (
		tr.OrganizacionID
		,0
		)
	AND @CodigoTratamiento IN (
		tr.CodigoTratamiento
		,0
		)
	AND @TipoTratamientoID IN (
		tr.TipoTratamientoID
		,0
		)
	AND tr.Activo = @Activo
INSERT INTO @TratamientoProducto
SELECT tp.TratamientoProductoID
	,tp.TratamientoID
	,pr.ProductoID
	,pr.Descripcion [Producto]
	,sf.SubFamiliaID
	,sf.Descripcion [SubFamilia]
	,fa.FamiliaID
	,fa.Descripcion [Familia]
	,tp.Dosis
	,tp.Activo
FROM TratamientoProducto tp
INNER JOIN Producto pr ON tp.ProductoID = pr.ProductoID
INNER JOIN SubFamilia sf ON pr.SubFamiliaID = sf.SubFamiliaID
INNER JOIN Familia fa ON sf.FamiliaID = fa.FamiliaID
INNER JOIN @Tratamiento tr ON tp.TratamientoID = tr.TratamientoID
SELECT TratamientoID
	,OrganizacionID
	,Organizacion
	,TipoOrganizacionID
	,TipoOrganizacion
	,CodigoTratamiento
	,TipoTratamientoID
	,TipoTratamiento
	,Sexo
	,RangoInicial
	,RangoFinal
	,Activo
FROM @Tratamiento
WHERE RowNum BETWEEN @Inicio
		AND @Limite
SELECT COUNT(TratamientoID) AS TotalReg
FROM @Tratamiento
SELECT TratamientoProductoID
	,tp.TratamientoID
	,ProductoID
	,Producto
	,SubFamiliaID
	,SubFamilia
	,FamiliaID
	,Familia
	,Dosis
	,tp.Activo
FROM @TratamientoProducto tp
INNER JOIN @Tratamiento tr ON tp.TratamientoID = tr.TratamientoID
WHERE tr.RowNum BETWEEN @Inicio
		AND @Limite
SET NOCOUNT OFF

GO
