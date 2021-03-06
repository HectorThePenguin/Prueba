USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CamionReparto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CamionReparto_Crear
--======================================================
CREATE PROCEDURE [dbo].[CamionReparto_Crear]
@CamionRepartoID int,
@OrganizacionID int,
@CentroCostoID int,
@NumeroEconomico varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CamionReparto (
		CamionRepartoID,
		OrganizacionID,
		CentroCostoID,
		NumeroEconomico,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@CamionRepartoID,
		@OrganizacionID,
		@CentroCostoID,
		@NumeroEconomico,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
