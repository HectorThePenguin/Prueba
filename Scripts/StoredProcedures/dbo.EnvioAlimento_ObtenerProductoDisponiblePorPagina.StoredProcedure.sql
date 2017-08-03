USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[EnvioAlimento_ObtenerProductoDisponiblePorPagina]'))
 DROP PROCEDURE [dbo].[EnvioAlimento_ObtenerProductoDisponiblePorPagina]; 
GO

--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 10/11/2016 9:00:00 a.m.
-- Description: Consulta todos los productos activos con existencias en algun almacen de la organizacion del usuario indicado
-- SpName     : EnvioAlimento_ObtenerProductoDisponiblePorPagina '', 2, 5, 1, 15, 110
--======================================================
CREATE PROCEDURE [dbo].[EnvioAlimento_ObtenerProductoDisponiblePorPagina]
@Descripcion VARCHAR(100),
@SubFamiliaID INT,
@UsuarioID INT,
@Inicio INT,
@Limite INT,
@ProductoID INT = 0
AS
BEGIN

DECLARE @OrganizacionOrigen AS INT; 

SET @OrganizacionOrigen = (SELECT TOP 1 OrganizacionID FROM Usuario WHERE UsuarioID=@UsuarioID);

	SET NOCOUNT ON;
	SELECT 
		ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS RowNum,
		P.ProductoID,
		P.Descripcion,
		P.SubFamiliaID,
		P.UnidadID,
		P.Activo,
		UM.Descripcion AS DescripcionUnidad,
		p.ManejaLote,
	  p.MaterialSAP
	INTO #Datos
	FROM Producto P 
			INNER JOIN UnidadMedicion UM ON UM.UnidadID = P.UnidadID
	WHERE (P.Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '' )
			AND P.Activo = 1
			AND P.SubFamiliaID= @SubFamiliaID 
			AND @ProductoID IN (P.ProductoID, 0)
			AND P.ProductoID IN (
					SELECT AlmacenInventario.ProductoID 
					FROM AlmacenInventario
							INNER JOIN Almacen ON AlmacenInventario.AlmacenID = Almacen.AlmacenID 
					WHERE Almacen.OrganizacionID = @OrganizacionOrigen
					AND Almacen.Activo=1 
					AND AlmacenInventario.Cantidad>0)
SELECT
		ProductoID,
		Descripcion,
		SubFamiliaID,
		UnidadID,
		Activo,
		DescripcionUnidad,
		ManejaLote,
		MaterialSAP
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(ProductoID) AS [TotalReg]
	FROM #Datos

	DROP TABLE #Datos

	SET NOCOUNT OFF;
END
