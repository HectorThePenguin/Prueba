IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ProveedorRetencion_ObtenerPorPagina]'))
	DROP PROCEDURE [dbo].[ProveedorRetencion_ObtenerPorPagina]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/03/2016 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorRetencion_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ProveedorRetencion_ObtenerPorPagina]
@ProveedorRetencionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 

AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		ProveedorRetencionID,
		ProveedorID,
		RetencionID,
		IvaID,
		Activo
	INTO #ProveedorRetencion
	FROM ProveedorRetencion
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo

	SELECT
		ProveedorRetencionID,
		ProveedorID,
		RetencionID,
		IvaID,
		Activo
	FROM #ProveedorRetencion
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(ProveedorRetencionID) AS [TotalReg]
	FROM #ProveedorRetencion

	DROP TABLE #ProveedorRetencion

	SET NOCOUNT OFF;
END
GO

