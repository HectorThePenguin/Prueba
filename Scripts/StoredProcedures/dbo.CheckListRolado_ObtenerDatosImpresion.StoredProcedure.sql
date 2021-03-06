USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRolado_ObtenerDatosImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRolado_ObtenerDatosImpresion]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRolado_ObtenerDatosImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/12/2014
-- Description:  Obtiene la cantidad de granos entregados a planta
-- CheckListRolado_ObtenerDatosImpresion '2014-12-23 11:33:34.927', 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRolado_ObtenerDatosImpresion]
@Fecha DATE
, @Turno INT
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT CLRG.Turno
			,  CLRG.FechaInicio
			,  CLRG.Observaciones
			,  CLRG.SurfactanteInicio
			,  CLRG.SurfactanteFin
			,  CLRG.ContadorAguaInicio
			,  CLRG.ContadorAguaFin
			,  CLRG.GranoEnteroFinal
			,  CLRG.CheckListRoladoraGeneralID
			,  U.Nombre AS NombreUsuario
		INTO #tRoladoraGeneral
		FROM CheckListRoladoraGeneral CLRG
		INNER JOIN Usuario U
			ON (CLRG.UsuarioCreacionID = U.UsuarioID
				AND U.OrganizacionID = @OrganizacionID)
		WHERE CAST(CLRG.FechaInicio AS DATE) = @Fecha
			AND Turno = @Turno
		SELECT R.RoladoraID
			,  R.Descripcion
			,  CLRH.HorometroInicial
			,  CLRH.HorometroFinal
			,  CLRH.CheckListRoladoraHorometroID
			,  CLRH.CheckListRoladoraGeneralID
		INTO #tRoladoraHorometro
		FROM #tRoladoraGeneral CLRG
		INNER JOIN CheckListRoladoraHorometro CLRH
			ON (CLRG.CheckListRoladoraGeneralID = CLRH.CheckListRoladoraGeneralID)
		INNER JOIN Roladora R
			ON (CLRH.RoladoraID = R.RoladoraID)
		GROUP BY R.RoladoraID
			,  R.Descripcion
			,  CLRH.HorometroInicial
			,  CLRH.HorometroFinal
			,  CLRH.CheckListRoladoraHorometroID
			,  CLRH.CheckListRoladoraGeneralID
		SELECT Turno
			,  FechaInicio
			,  Observaciones
			,  SurfactanteInicio
			,  SurfactanteFin
			,  ContadorAguaInicio
			,  ContadorAguaFin
			,  GranoEnteroFinal
			,  CheckListRoladoraGeneralID
			,  NombreUsuario
		FROM #tRoladoraGeneral
		SELECT RoladoraID
			,  Descripcion
			,  HorometroInicial
			,  HorometroFinal
			,  CheckListRoladoraHorometroID
			,  CheckListRoladoraGeneralID
		FROM #tRoladoraHorometro
		ORDER BY RoladoraID
		SELECT TP.Descripcion				AS TipoPregunta
			,  CLRR.Descripcion				AS Rango
			,  CLRR.CodigoColor
			,  CLRA.Descripcion				AS Accion
			,  CLRG.CheckListRoladoraGeneralID
			,  CLRA.CheckListRoladoraAccionID
			,  CLRR.CheckListRoladoraRangoID
			,  CLRD.CheckListRoladoraDetalleID
			,  CLR.FechaCheckList
			,  TP.TipoPreguntaID
			,  CLR.RoladoraID
			,  R.Descripcion AS Roladora	
		FROM #tRoladoraGeneral CLRG
		INNER JOIN CheckListRoladora CLR
			ON (CLRG.CheckListRoladoraGeneralID = CLR.CheckListRoladoraGeneralID)
		INNER JOIN CheckListRoladoraDetalle CLRD
			ON (CLR.CheckListRoladoraID = CLRD.CheckListRoladoraID)
		INNER JOIN CheckListRoladoraRango CLRR
			ON (CLRD.CheckListRoladoraRangoID = CLRR.CheckListRoladoraRangoID)
		INNER JOIN Pregunta P
			ON (CLRR.PreguntaID = P.PreguntaID)
		INNER JOIN TipoPregunta TP
			ON (P.TipoPreguntaID = TP.TipoPreguntaID)
		INNER JOIN Roladora R
			ON (CLR.RoladoraID = R.RoladoraID)
		LEFT JOIN CheckListRoladoraAccion CLRA
			ON (CLRD.CheckListRoladoraAccionID = CLRA.CheckListRoladoraAccionID
				AND CLRR.CheckListRoladoraRangoID = CLRA.CheckListRoladoraRangoID)
		ORDER BY CLR.RoladoraID, CLRD.CheckListRoladoraDetalleID
		DROP TABLE #tRoladoraGeneral
		DROP TABLE #tRoladoraHorometro
	SET NOCOUNT OFF
END

GO
