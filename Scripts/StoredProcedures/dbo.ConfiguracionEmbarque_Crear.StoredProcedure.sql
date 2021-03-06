USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionEmbarque_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/13
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionEmbarque_Crear]
	@OrganizacionOrigenID INT,
	@OrganizacionDestinoID INT,
	@Kilometros DECIMAL,
	@Horas INT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ConfiguracionEmbarque(
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		Kilometros,
		Horas,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@OrganizacionOrigenID,
		@OrganizacionDestinoID,
		@Kilometros,
		@Horas,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SET NOCOUNT OFF;
END

GO
