USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraAccion_ObtenerParametros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraAccion_ObtenerParametros]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraAccion_ObtenerParametros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 24/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladoraAccion_ObtenerParametros
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladoraAccion_ObtenerParametros]
AS
BEGIN
	SET NOCOUNT ON
		DECLARE @TipoPreguntaCocedor INT
			  , @TipoPreguntaAmperaje INT
			  , @TipoPreguntaCalidadRolado INT
		SELECT @TipoPreguntaAmperaje = 5
			,  @TipoPreguntaCalidadRolado = 6
			,  @TipoPreguntaCocedor = 4
		SELECT ROW_NUMBER()	 OVER (ORDER BY TP.TipoPreguntaID ASC)		AS Indice
			,  TP.TipoPreguntaID
			,  TP.Descripcion								AS TipoPregunta
			,  P.PreguntaID
			,  P.Descripcion								AS Pregunta
			,  P.Orden
			,  CLRR.CodigoColor
			,  CLRR.CheckListRoladoraRangoID
			,  CLRR.Descripcion								AS DescripcionRango
			,  ISNULL(CLRA.CheckListRoladoraAccionID, 0)	AS CheckListRoladoraAccionID
			,  ISNULL(CLRA.Descripcion, '')					AS DescripcionAccion
		FROM TipoPregunta TP
		INNER JOIN Pregunta P
			ON (TP.TipoPreguntaID = P.TipoPreguntaID)
		INNER JOIN CheckListRoladoraRango CLRR
			ON (P.PreguntaID = CLRR.PreguntaID)
		LEFT OUTER JOIN CheckListRoladoraAccion CLRA
			ON (CLRR.CheckListRoladoraRangoID = CLRA.CheckListRoladoraRangoID)
		WHERE TP.TipoPreguntaID IN (@TipoPreguntaCocedor, @TipoPreguntaAmperaje, @TipoPreguntaCalidadRolado)
		ORDER BY TP.TipoPreguntaID, P.Orden
	SET NOCOUNT OFF
END

GO
