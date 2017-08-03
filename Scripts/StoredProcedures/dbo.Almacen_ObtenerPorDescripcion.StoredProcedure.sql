USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Almacen_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorDescripcion]
@Descripcion varchar(50)
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
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
