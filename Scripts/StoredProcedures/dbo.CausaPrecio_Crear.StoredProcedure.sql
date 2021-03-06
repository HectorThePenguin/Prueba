USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaPrecio_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaPrecio_Crear
--======================================================
CREATE PROCEDURE [dbo].[CausaPrecio_Crear]
@CausaSalidaID int,
@OrganizacionID int,
@Precio decimal(18,2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CausaPrecio (
		OrganizacionID,
		CausaSalidaID,
		Precio,		
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@CausaSalidaID,
		@Precio,		
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
