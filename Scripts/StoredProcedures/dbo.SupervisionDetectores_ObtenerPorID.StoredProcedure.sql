USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDetectores_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDetectores_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 19/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SupervisionDetectores_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[SupervisionDetectores_ObtenerPorID]
@SupervisionDetectoresID int
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
	WHERE SupervisionDetectoresID = @SupervisionDetectoresID
	SET NOCOUNT OFF;
END

GO
