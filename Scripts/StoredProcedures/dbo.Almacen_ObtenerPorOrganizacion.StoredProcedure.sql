USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 25/03/2014 12:00:00 a.m.
-- Description: Obtiene todos los almacenes por Organizacion
-- SpName     : EXEC Almacen_ObtenerPorOrganizacion 1,4
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorOrganizacion]
@Activo INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		AlmacenID,
		OrganizacionID,
		CodigoAlmacen,
		Descripcion,
		TipoAlmacenID,
		CuentaInventario,
		CuentaInventarioTransito,
		CuentaDiferencias,
		Activo
	FROM Almacen
	WHERE Activo = @Activo
		AND OrganizacionID=@OrganizacionID
	SET NOCOUNT OFF;
END

GO
