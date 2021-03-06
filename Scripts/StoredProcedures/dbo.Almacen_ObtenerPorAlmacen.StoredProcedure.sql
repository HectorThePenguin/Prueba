USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 15/07/2014
-- Description: 
-- SpName     : Almacen_ObtenerPorAlmacen 0,'',0,0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorAlmacen]
@AlmacenID INT,
@OrganizacionID INT,
@TipoAlmacenID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		A.AlmacenID,
		A.OrganizacionID,
		A.CodigoAlmacen,
		A.Descripcion,
		A.TipoAlmacenID,
		A.CuentaInventario,
		A.CuentaInventarioTransito,
		A.CuentaDiferencias,
		A.Activo
	FROM Almacen A
	INNER JOIN AlmacenMovimiento AM
		ON (A.AlmacenID = AM.AlmacenID
			AND AM.PolizaGenerada = 1)
	WHERE A.AlmacenID = A.TipoAlmacenID
		AND A.OrganizacionID = @OrganizacionID
		AND A.TipoAlmacenID = @TipoAlmacenID
	SET NOCOUNT OFF;
END

GO
