USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezclado_ObtenerPorFormulaID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezclado_ObtenerPorFormulaID]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezclado_ObtenerPorFormulaID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Villarreal Medina Edgar E.
-- Create date: 31/10/2013
-- Description:  Obtener calidad mezclado por formula
-- CalidadMezclado_ObtenerPorFormulaID 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezclado_ObtenerPorFormulaID]
@OrganizacionID INT,
@TipoTecnicaID	INT,
@FormulaID INT
AS
BEGIN
    SET NOCOUNT ON;		
    SELECT 	CM.CalidadMezcladoID, 
						CM.OrganizacionID,
						O.Descripcion,
						CM.TipoTecnicaID,
						CM.Fecha,
						CM.UsuarioIDLaboratorio,
						CM.FormulaID,
						CM.FechaPremezcla,
						CM.FechaBatch,
						CM.TipoLugarMuestraID,
						COALESCE(CM.CamionRepartoID,0) AS CamionRepartoID,
						COALESCE(CM.ChoferID,0) AS ChoferID,
						COALESCE(CM.MezcladoraID,0) AS MezcladoraID,
						COALESCE(CM.OperadorIDMezclador,0) AS OperadorIDMezclador,
						COALESCE(CM.LoteID,0) AS LoteID,
						CM.Batch,
						CM.TiempoMezclado,
						CM.OperadorIDAnalista,
						CM.GramosMicrotoxina
			FROM CalidadMezclado CM
			INNER JOIN Organizacion O ON O.OrganizacionID = CM.OrganizacionID
		WHERE CM.OrganizacionID = @OrganizacionID
			AND CM.TipoTecnicaID = @TipoTecnicaID 
			AND CM.FormulaID = @FormulaID
			AND CAST(CM.Fecha as DATE) = CAST(GETDATE() as DATE)
		SET NOCOUNT OFF;
END	

GO
