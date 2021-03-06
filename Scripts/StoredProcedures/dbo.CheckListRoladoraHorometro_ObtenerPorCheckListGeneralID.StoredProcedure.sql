USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_ObtenerPorCheckListGeneralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraHorometro_ObtenerPorCheckListGeneralID]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_ObtenerPorCheckListGeneralID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraHorometro_ObtenerPorCheckListGeneralID
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraHorometro_ObtenerPorCheckListGeneralID] @CheckListRoladoraGeneralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT clrh.CheckListRoladoraHorometroID
		,clrg.CheckListRoladoraGeneralID
		,clrg.FechaInicio
		,clrg.Turno
		,clrg.UsuarioIDSupervisor
		,clrg.Observaciones
		,clrg.SurfactanteInicio
		,clrg.SurfactanteFin
		,clrg.ContadorAguaInicio
		,clrg.ContadorAguaFin
		,clrg.GranoEnteroFinal
		,clrh.RoladoraID
		,clrh.HorometroInicial
		,clrh.HorometroFinal
		,clrh.Activo
		,GETDATE() AS FechaServidor
	FROM CheckListRoladoraHorometro clrh
	INNER JOIN CheckListRoladoraGeneral clrg ON clrh.CheckListRoladoraGeneralID = clrg.CheckListRoladoraGeneralID
	WHERE clrg.CheckListRoladoraGeneralID = @CheckListRoladoraGeneralID
	SET NOCOUNT OFF;
END

GO
