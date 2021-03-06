USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerPorPaginaFiltroDescripcionOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInterno_ObtenerPorPaginaFiltroDescripcionOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerPorPaginaFiltroDescripcionOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: Obtiene fletes internos por pagina
-- FleteInterno_ObtenerPorPaginaFiltroDescripcionOrganizacion 0, 1, 'DORADO', 0, 0, 0, 0, 0, 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[FleteInterno_ObtenerPorPaginaFiltroDescripcionOrganizacion] 
@FleteInternoID INT,
@OrganizacionID INT,
@DescripcionOrganizacion VARCHAR(50),
@ParametroTipoMovimientoID INT,
@TipoMovimientoID INT,
@AlmacenIDOrigen INT,
@AlmacenIDDestino INT,
@ProductoID INT,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		 FT.FleteInternoID,
		 FT.OrganizacionID AS OrganizacionIDFleteInterno,
		 O2.Descripcion AS OrganizacionDescripcion,
		 O.OrganizacionID AS OrganizacionIDDestino,
		 CASE WHEN FT.TipoMovimientoID = @ParametroTipoMovimientoID THEN 'Movimiento Interno'
         ELSE O.Descripcion
		 END AS OrganizacionDescripcionDestino,
		 FT.TipoMovimientoID,
		 TM.Descripcion,
		 FT.AlmacenIDOrigen,
		 AO.CodigoAlmacen AS CodigoAlmacenOrigen,
		 AO.Descripcion AS DescripcionAlmacenOrigen,
		 FT.AlmacenIDDestino,
		 AD.CodigoAlmacen AS CodigoAlmacenDestino,
		 AD.Descripcion AS DescripcionAlmacenDestino,
		 FT.ProductoID,
		 P.Descripcion AS DescripcionProducto,
		 P.SubFamiliaID,
		 FT.Activo,
		 FT.FechaCreacion,
		 FT.UsuarioCreacionID
	INTO #Datos
	FROM FleteInterno FT
	INNER JOIN TipoMovimiento TM
		ON (TM.TipoMovimientoID = FT.TipoMovimientoID)
	INNER JOIN Almacen AO
		ON (AO.AlmacenID = FT.AlmacenIDOrigen)
	INNER JOIN Almacen AD
		ON (AD.AlmacenID = FT.AlmacenIDDestino)
	INNER JOIN Organizacion O
        ON (O.OrganizacionID = AD.OrganizacionID)
	INNER JOIN Organizacion O2
        ON (O2.OrganizacionID = FT.OrganizacionID)
	INNER JOIN Producto P
		ON (P.ProductoID = FT.ProductoID)
	WHERE @FleteInternoID IN (FT.FleteInternoID, 0)
		AND @TipoMovimientoID IN (FT.TipoMovimientoID, 0)
		AND @OrganizacionID IN (FT.OrganizacionID, 0)
		AND @AlmacenIDOrigen IN (FT.AlmacenIDOrigen, 0)
		AND @AlmacenIDDestino IN (FT.AlmacenIDDestino, 0)
		AND @ProductoID IN (FT.ProductoID, 0)
		AND FT.Activo = @Activo
	SELECT ROW_NUMBER() OVER (
			ORDER BY D.OrganizacionDescripcion ASC
			) AS RowNum,
		 FleteInternoID,
		 OrganizacionIDFleteInterno,
		 OrganizacionDescripcion,
		 OrganizacionIDDestino,
		 OrganizacionDescripcionDestino,
		 TipoMovimientoID,
		 Descripcion,
		 AlmacenIDOrigen,
		 CodigoAlmacenOrigen,
		 DescripcionAlmacenOrigen,
		 AlmacenIDDestino,
		 CodigoAlmacenDestino,
		 DescripcionAlmacenDestino,
		 ProductoID,
		 DescripcionProducto,
		 SubFamiliaID,
		 Activo,
		 FechaCreacion,
		 UsuarioCreacionID
	INTO #Datos2
	FROM #Datos D
	WHERE (OrganizacionDescripcionDestino like '%' + @DescripcionOrganizacion + '%' OR @DescripcionOrganizacion = '') 
	SELECT 		 
		 FleteInternoID,
		 OrganizacionIDFleteInterno,
		 OrganizacionDescripcion,
		 OrganizacionIDDestino,
		 OrganizacionDescripcionDestino,
		 TipoMovimientoID,
		 Descripcion,
		 AlmacenIDOrigen,
		 CodigoAlmacenOrigen,
		 DescripcionAlmacenOrigen,
		 AlmacenIDDestino,
		 CodigoAlmacenDestino,
		 DescripcionAlmacenDestino,
		 ProductoID,
		 DescripcionProducto,
		 SubFamiliaID,
		 Activo,
		 FechaCreacion,
		 UsuarioCreacionID
	FROM #Datos2
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(FleteInternoID) AS TotalReg
	FROM #Datos2
	DROP TABLE #Datos
	DROP TABLE #Datos2
	SET NOCOUNT OFF;
END

GO
