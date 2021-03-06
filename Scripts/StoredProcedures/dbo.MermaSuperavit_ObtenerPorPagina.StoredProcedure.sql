USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 13/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : MermaSuperavit_ObtenerPorPagina 1, 1, 1, 15
--001 Jorge Luis Velazquez Araujo 07/09/2015 **Se agregan los datos de la Organización 
--======================================================
CREATE PROCEDURE [dbo].[MermaSuperavit_ObtenerPorPagina]
@AlmacenID INT,
@Activo BIT,
@Inicio INT,
@Limite INT,
@OrganizacionID INT --001 

AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY A.Descripcion ASC) AS [RowNum],
		MS.MermaSuperavitID,
		MS.AlmacenID,
		MS.ProductoID,
		MS.Merma,
		MS.Superavit,
		MS.Activo
		, A.Descripcion		AS Almacen
		, P.Descripcion		AS Producto,
		o.OrganizacionID --001
		,o.Descripcion AS Organizacion --001
	INTO #MermaSuperavit
	FROM MermaSuperavit MS
	INNER JOIN Almacen A
		ON (MS.AlmacenID = A.AlmacenID
			AND (@AlmacenID IN (A.AlmacenID, 0)))
	INNER JOIN Producto P
		ON (MS.ProductoID = P.ProductoID)
	INNER JOIN Organizacion o on a.OrganizacionID = o.OrganizacionID --001
	WHERE  MS.Activo = @Activo
	and @OrganizacionID in (o.OrganizacionID, 0) --001

	SELECT
		MermaSuperavitID,
		AlmacenID,
		ProductoID,
		Merma,
		Superavit,
		Activo
		, Almacen
		, Producto
		,OrganizacionID --001
		,Organizacion --001
	FROM #MermaSuperavit
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(MermaSuperavitID) AS [TotalReg]
	FROM #MermaSuperavit

	DROP TABLE #MermaSuperavit

	SET NOCOUNT OFF;
END

GO
