USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaValor_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaValor_Crear
--======================================================
CREATE PROCEDURE [dbo].[CuentaValor_Crear]
@CuentaID int,
@OrganizacionID int,
@Valor varchar(20),
@Activo int,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CuentaValor (
		CuentaID,
		OrganizacionID,
		Valor,
		Activo,
		UsuarioCreacionID,		
		FechaCreacion
	)
	VALUES(
		@CuentaID,
		@OrganizacionID,
		@Valor,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
