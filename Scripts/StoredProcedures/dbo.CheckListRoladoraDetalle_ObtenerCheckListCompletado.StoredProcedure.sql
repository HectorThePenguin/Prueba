USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_ObtenerCheckListCompletado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraDetalle_ObtenerCheckListCompletado]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_ObtenerCheckListCompletado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 04/07/2014
-- Description:  Obtener CheckList 
-- CheckListRoladoraDetalle_ObtenerCheckListCompletado 4, 1, 4
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladoraDetalle_ObtenerCheckListCompletado]
@OrganizacionID INT
, @Turno		INT
, @RoladoraID	INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @Fecha CHAR(8)
		SET @Fecha = CONVERT(CHAR(8), GETDATE(), 112)
		SELECT CLRG.CheckListRoladoraGeneralID
			,  CLRG.Turno
			,  CLRG.FechaInicio
			,  CLRG.UsuarioIDSupervisor
			,  CLRG.Observaciones
			,  CLRG.SurfactanteInicio
			,  CLRG.SurfactanteFin
			,  CLRG.ContadorAguaInicio
			,  CLRG.ContadorAguaFin
			,  CLRG.GranoEnteroFinal
			,  CLR.CheckListRoladoraID
			,  CLR.RoladoraID
			,  CLR.UsuarioIDResponsable			
			,  CLR.FechaCheckList
			,  ISNULL(CLRD.CheckListRoladoraDetalleID, 0) AS CheckListRoladoraDetalleID
			,  ISNULL(CLRD.CheckListRoladoraRangoID, 0) AS CheckListRoladoraRangoID
			,  CLRD.CheckListRoladoraAccionID
			,  ISNULL(clrh.CheckListRoladoraHorometroID,0) AS CheckListRoladoraHorometroID
			,  clrh.HorometroInicial
			,  clrh.HorometroFinal
		FROM CheckListRoladoraGeneral CLRG
		INNER JOIN CheckListRoladora CLR
			ON (CLRG.CheckListRoladoraGeneralID = CLR.CheckListRoladoraGeneralID
				AND CLR.RoladoraID = @RoladoraID)
		INNER JOIN Roladora R
			ON (CLR.RoladoraID = R.RoladoraID
				AND R.OrganizacionID = @OrganizacionID)
		LEFT OUTER JOIN CheckListRoladoraDetalle CLRD
			ON (CLR.CheckListRoladoraID = CLRD.CheckListRoladoraID)
		left outer join CheckListRoladoraHorometro clrh on (CLRG.CheckListRoladoraGeneralID = clrh.CheckListRoladoraGeneralID AND clrh.RoladoraID = @RoladoraID)
		WHERE CLRG.Turno = @Turno
			AND CONVERT(CHAR(8), CLRG.FechaInicio, 112) = @Fecha
	SET NOCOUNT OFF;
END

GO
