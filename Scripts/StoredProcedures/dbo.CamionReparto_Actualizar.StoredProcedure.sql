USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CamionReparto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CamionReparto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CamionReparto_Actualizar]
@CamionRepartoID int,
@OrganizacionID int,
@CentroCostoID int,
@NumeroEconomico varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CamionReparto SET
		OrganizacionID = @OrganizacionID,
		CentroCostoID = @CentroCostoID,
		NumeroEconomico = @NumeroEconomico,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE CamionRepartoID = @CamionRepartoID
	SET NOCOUNT OFF;
END

GO
