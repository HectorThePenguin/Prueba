USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorAlmacen_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 26/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorAlmacen_Crear
--======================================================
CREATE PROCEDURE [dbo].[ProveedorAlmacen_Crear]
@ProveedorID int,
@AlmacenID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ProveedorAlmacen (		
		ProveedorID,
		AlmacenID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(		
		@ProveedorID,
		@AlmacenID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
