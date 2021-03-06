USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInterno_Crear]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: Crea un nuevo flete interno
-- FleteInterno_Crear 
--=============================================
CREATE PROCEDURE [dbo].[FleteInterno_Crear]
	@OrganizacionID INT,
	@TipoMovimientoID INT,
	@AlmacenIDOrigen INT,
	@AlmacenIDDestino INT,
	@ProductoID INT,
	@Activo INT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT FleteInterno(
		OrganizacionID,
		TipoMovimientoID,
		AlmacenIDOrigen,
		AlmacenIDDestino,
		ProductoID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@OrganizacionID,
		@TipoMovimientoID,
		@AlmacenIDOrigen,
		@AlmacenIDDestino,
		@ProductoID,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
