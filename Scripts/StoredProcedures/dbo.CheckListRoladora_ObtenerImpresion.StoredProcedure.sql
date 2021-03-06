USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_ObtenerImpresion]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 19/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladora_ObtenerImpresion 1,'20141223'
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladora_ObtenerImpresion] @OrganizacionID INT
	,@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT crg.FechaInicio AS Fecha
		,CASE 
			WHEN crg.Turno = 1
				THEN 'Turno Uno'
			WHEN crg.Turno = 2
				THEN 'Turno Dos'
			WHEN crg.Turno = 3
				THEN 'Turno 3'
			END AS Turno
		,us.Nombre AS Responsable
		--,ro.Descripcion AS Roladora
		--,crh.HorometroInicial
		--,crh.HorometroFinal
		,crg.Observaciones
		,0 AS HumedadGranoRoladoBodega
		,0 AS HumedadGranoEnteroBodega
		,0 AS SuperavitAguaSurfactante
		,crg.SurfactanteInicio 
		,crg.SurfactanteFin
		,crg.ContadorAguaInicio
		,crg.ContadorAguaFin
		,ISNULL(crg.ContadorAguaFin,0) - crg.ContadorAguaInicio AS ConsumoAgua
		,0 AS TotalGranoEnteroPP
		,crg.GranoEnteroFinal		
		,0 AS TotalGranoProcesado
		,0 AS SuperavitGranoRolado
		,0 AS TotalGrandoRolado
		,0 AS ConsumoDieselCalderas
		,0 AS DieselToneladaGranoRolado		
	FROM CheckListRoladoraGeneral crg
	inner join Usuario us on crg.UsuarioCreacionID = us.UsuarioID
	--inner join CheckListRoladora clr on crg.CheckListRoladoraGeneralID = clr.CheckListRoladoraGeneralID
	--inner join Roladora ro on clr.RoladoraID = ro.RoladoraID
	--left join CheckListRoladoraHorometro crh on crh.CheckListRoladoraGeneralID = crg.CheckListRoladoraGeneralID
	where us.OrganizacionID = @OrganizacionID
	and CAST(FechaInicio AS DATE) = @Fecha
	SELECT 
		crg.CheckListRoladoraGeneralID
		,ro.Descripcion AS Roladora
		,crh.HorometroInicial
		,crh.HorometroFinal		
	FROM CheckListRoladoraGeneral crg	
	inner join Usuario us on crg.UsuarioCreacionID = us.UsuarioID
	left join CheckListRoladoraHorometro crh on crh.CheckListRoladoraGeneralID = crg.CheckListRoladoraGeneralID
	left join Roladora ro on crh.RoladoraID = ro.RoladoraID
	where us.OrganizacionID = @OrganizacionID
	and CAST(FechaInicio AS DATE) = @Fecha
	select 
	clr.CheckListRoladoraID
	,clr.RoladoraID
	,ro.Descripcion AS Roladora
	,crr.CheckListRoladoraRangoID
	,crr.PreguntaID
	,tp.TipoPreguntaID
	,tp.Descripcion AS TipoPregunta
	,pr.Descripcion AS Pregunta
	,crr.Descripcion AS DescripcionRango
	,cra.CheckListRoladoraAccionID
	,cra.Descripcion AS DescripcionAccion
	FROM CheckListRoladoraGeneral crg	
	inner join CheckListRoladora clr on crg.CheckListRoladoraGeneralID = clr.CheckListRoladoraGeneralID
	inner join Roladora ro on clr.RoladoraID = ro.RoladoraID
	inner join CheckListRoladoraDetalle crd on clr.CheckListRoladoraID = crd.CheckListRoladoraID
	inner join CheckListRoladoraRango crr on crd.CheckListRoladoraRangoID = crr.CheckListRoladoraRangoID
	inner join Pregunta pr on crr.PreguntaID = pr.PreguntaID
	inner join TipoPregunta tp on pr.TipoPreguntaID = tp.TipoPreguntaID
	left join CheckListRoladoraAccion cra on crd.CheckListRoladoraAccionID = cra.CheckListRoladoraAccionID
	where ro.OrganizacionID = @OrganizacionID
	and CAST(FechaInicio AS DATE) = @Fecha
	SET NOCOUNT OFF;
END

GO
