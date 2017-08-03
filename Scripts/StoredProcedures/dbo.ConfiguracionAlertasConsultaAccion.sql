USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertasConsultaAccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAlertasConsultaAccion]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertasConsultaAccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author			: Torres Lugo Manuel E.
-- Create date: 18/03/2016
-- Description: Genera la lista de Acciones segun la AlertaConfiguracion
-- Origen			: APInterfaces
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionAlertasConsultaAccion]
		@ID INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT
			CASE WHEN ACC.AccionID IS NULL THEN 0 ELSE ACC.AccionID END AS AccionID,
			ACC.Descripcion,
			AA.AlertaAccionID,
			AA.AlertaID
		FROM AlertaAccion AS AA
		INNER JOIN Accion AS ACC ON AA.AccionID = ACC.AccionID

		WHERE AA.AlertaID = @ID
		AND AA.Activo = 1

	SET NOCOUNT OFF;
END