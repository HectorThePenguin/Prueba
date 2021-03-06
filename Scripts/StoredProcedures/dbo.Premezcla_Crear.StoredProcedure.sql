USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Premezcla_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/05/2014
-- Description: Crea una nueva premezcla
-- Premezcla_Crear 
--=============================================
CREATE PROCEDURE [dbo].[Premezcla_Crear]
	@OrganizacionID INT,
	@Descripcion VARCHAR(50),
	@ProductoID INT,
	@Activo INT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Premezcla(
		OrganizacionID,
		Descripcion,
		ProductoID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID	
	)
	VALUES(
		@OrganizacionID,
		@Descripcion,
		@ProductoID,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
