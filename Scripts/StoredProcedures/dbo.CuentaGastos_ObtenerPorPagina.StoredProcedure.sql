IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[CuentaGastos_ObtenerPorPagina]'))
BEGIN
 DROP PROCEDURE [dbo].[CuentaGastos_ObtenerPorPagina]
END
GO

--=============================================
-- Author     : Edgar Villarreal
-- Create date: 2015/10/09
-- Description: 
-- Name : EXEC CuentaGastos_ObtenerPorPagina 0,'SUKARNE AGROINDUSTRIAL SA DE CV (CLN)',1,1,5
--=============================================
CREATE PROCEDURE [dbo].[CuentaGastos_ObtenerPorPagina]
@OrganizacionID INT,
@Descripcion Varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY O.Descripcion ASC) AS RowNum,
				CGG.CuentaGastoID,
				CGG.OrganizacionID,
				O.Descripcion AS DescripcionOrganizacion,
				Cta.CuentaSAPID,
				Cta.CuentaSAP,
				Cta.Descripcion AS DescripcionCuenta,
				CGG.CostoID,
				C.Descripcion AS DescripcionCosto,
				C.ClaveContable,
				CGG.Activo,
				CGG.UsuarioCreacionID,
				CGG.FechaCreacion 
		INTO #Datos
		FROM CatCuentaGasto CGG
		INNER JOIN Organizacion O ON O.OrganizacionID=CGG.OrganizacionID
		INNER JOIN Costo C ON C.CostoID = CGG.CostoID AND C.Activo = 1
		INNER JOIN CuentaSAP Cta ON Cta.CuentaSAP COLLATE Modern_Spanish_CI_AS =CGG.CuentaSAP COLLATE Modern_Spanish_CI_AS AND Cta.Activo = 1
			WHERE (O.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
			AND CGG.Activo = @Activo
	SELECT
				CuentaGastoID,
				OrganizacionID,
				DescripcionOrganizacion,
				CuentaSAPID,
				CuentaSAP,
				DescripcionCuenta,
				CostoID,
				DescripcionCosto,
				ClaveContable,
				Activo,
				UsuarioCreacionID,
				FechaCreacion 
	FROM #Datos 
	WHERE RowNum BETWEEN @Inicio AND @Limite


	SELECT 
		COUNT(CuentaGastoID)AS TotalReg
	From #Datos


	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

