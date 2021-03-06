USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDetectores_Crear]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 19/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SupervisionDetectores_Crear
--======================================================
CREATE PROCEDURE [dbo].[SupervisionDetectores_Crear]
@SupervisionDetectoresID int,
@OrganizacionID int,
@OperadorID int,
@FechaSupervision smalldatetime,
@CriterioSupervisionID int,
@Observaciones varchar(255),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT SupervisionDetectores (
		SupervisionDetectoresID,
		OrganizacionID,
		OperadorID,
		FechaSupervision,
		CriterioSupervisionID,
		Observaciones,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@SupervisionDetectoresID,
		@OrganizacionID,
		@OperadorID,
		@FechaSupervision,
		@CriterioSupervisionID,
		@Observaciones,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
