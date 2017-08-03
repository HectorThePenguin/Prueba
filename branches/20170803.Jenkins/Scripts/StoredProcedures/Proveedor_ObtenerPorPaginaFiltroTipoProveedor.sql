-- =========================================================================================
-- Author:  Edgar Villarreal															  --
-- Create date: 23/05/2014                                                                --
-- Description: Otiene un listado de Proveedores paginado con filtro de tipo proveedor    --
-- Proveedor_ObtenerPorPaginaFiltroTipoProveedor 0, '','', 1, 1,15 ,2,5					  --
-- =========================================================================================
IF EXISTS (SELECT  object_id FROM    sys.objects WHERE   object_id = OBJECT_ID(N'Proveedor_ObtenerPorPaginaFiltroTipoProveedor') AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE Proveedor_ObtenerPorPaginaFiltroTipoProveedor
END
GO
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaFiltroTipoProveedor]
 @ProveedorID INT,
 @CodigoSAP VARCHAR(10),
 @Descripcion NVARCHAR(50), 
 @Activo BIT,
 @Inicio INT, 
 @Limite INT,
 @TipoProveedorID INT,
 @TipoProveedorMateriaPrima INT = 0
AS
BEGIN
 SET NOCOUNT ON;
 SELECT 
     ROW_NUMBER() OVER ( ORDER BY p.Descripcion ASC) AS RowNum,
  p.ProveedorID,
  p.Descripcion,
  p.CodigoSAP,
  p.TipoProveedorID,
  p.ImporteComision,
  p.Activo  
  INTO #Proveedor
  FROM Proveedor p
  WHERE (p.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
  AND (p.CodigoSAP LIKE '%'+@CodigoSAP+'%' OR @CodigoSAP = '')
   AND p.Activo = @Activo
   AND @ProveedorID IN (p.ProveedorID, 0)
   AND (@TipoProveedorID IN (p.TipoProveedorID,0) OR @TipoProveedorMateriaPrima IN (p.TipoProveedorID))
 SELECT 
  p.ProveedorID, 
  p.Descripcion,
  p.CodigoSAP, 
  p.TipoProveedorID,
  p.ImporteComision,
  tp.Descripcion as  [TipoProveedor],
  p.Activo
 FROM #Proveedor p 
 INNER JOIN TipoProveedor tp on tp.TipoProveedorID = p.TipoProveedorID
 WHERE RowNum BETWEEN @Inicio AND @Limite
 SELECT 
  COUNT(ProveedorID)AS TotalReg 
 FROM #Proveedor 
 SET NOCOUNT OFF;
END