USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDetectores_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 19/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SupervisionDetectores_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[SupervisionDetectores_Actualizar]
@SupervisionDetectoresID int,
@OrganizacionID int,
@OperadorID int,
@FechaSupervision smalldatetime,
@CriterioSupervisionID int,
@Observaciones varchar(255),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE SupervisionDetectores SET
		OrganizacionID = @OrganizacionID,
		OperadorID = @OperadorID,
		FechaSupervision = @FechaSupervision,
		CriterioSupervisionID = @CriterioSupervisionID,
		Observaciones = @Observaciones,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE SupervisionDetectoresID = @SupervisionDetectoresID
	SET NOCOUNT OFF;
END

GO
