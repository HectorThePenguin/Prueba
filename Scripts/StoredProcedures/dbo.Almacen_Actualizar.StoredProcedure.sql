USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Almacen_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Almacen_Actualizar]
@AlmacenID int,
@OrganizacionID int,
@CodigoAlmacen varchar(10),
@Descripcion varchar(50),
@TipoAlmacenID int,
@CuentaInventario varchar(10),
@CuentaInventarioTransito varchar(10),
@CuentaDiferencias varchar(10),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Almacen SET
		OrganizacionID = @OrganizacionID,
		CodigoAlmacen = @CodigoAlmacen,
		Descripcion = @Descripcion,
		TipoAlmacenID = @TipoAlmacenID,
		CuentaInventario = @CuentaInventario,
		CuentaInventarioTransito = @CuentaInventarioTransito,
		CuentaDiferencias = @CuentaDiferencias,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE AlmacenID = @AlmacenID
	SET NOCOUNT OFF;
END

GO
