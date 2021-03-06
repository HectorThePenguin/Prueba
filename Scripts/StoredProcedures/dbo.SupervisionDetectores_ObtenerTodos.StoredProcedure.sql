USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDetectores_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 19/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SupervisionDetectores_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[SupervisionDetectores_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		SupervisionDetectoresID,
		OrganizacionID,
		OperadorID,
		FechaSupervision,
		CriterioSupervisionID,
		Observaciones,
		Activo
	FROM SupervisionDetectores
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
