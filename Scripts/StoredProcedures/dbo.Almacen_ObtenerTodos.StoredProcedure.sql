USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Almacen_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerTodos]
@Activo BIT = NULL
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
		, O.Descripcion AS Organizacion
		, TA.Descripcion AS TipoAlmacen
	FROM Almacen A
	INNER JOIN Organizacion O
		ON (A.OrganizacionID = O.OrganizacionID)
	INNER JOIN TipoAlmacen TA
		ON (A.TipoAlmacenID = TA.TipoAlmacenID)
	WHERE A.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
