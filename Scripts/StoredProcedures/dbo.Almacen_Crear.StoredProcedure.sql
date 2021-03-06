USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Almacen_Crear
--======================================================
CREATE PROCEDURE [dbo].[Almacen_Crear]
@OrganizacionID int,
@CodigoAlmacen varchar(10),
@Descripcion varchar(50),
@TipoAlmacenID int,
@CuentaInventario varchar(10),
@CuentaInventarioTransito varchar(10),
@CuentaDiferencias varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Almacen (
		OrganizacionID,
		CodigoAlmacen,
		Descripcion,
		TipoAlmacenID,
		CuentaInventario,
		CuentaInventarioTransito,
		CuentaDiferencias,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@CodigoAlmacen,
		@Descripcion,
		@TipoAlmacenID,
		@CuentaInventario,
		@CuentaInventarioTransito,
		@CuentaDiferencias,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
