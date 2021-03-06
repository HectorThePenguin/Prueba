USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Jorge Luis Velazquez Araujo
Fecha	  : 17/02/2015
Proposito : Obtiene los lotes por Organizacion
Lote_ObtenerPorOrganizacionID 1
************************************/
CREATE PROCEDURE [dbo].[Lote_ObtenerPorOrganizacionID]
@OrganizacionID INT
,@Activo  BIT
AS
BEGIN
	SET NOCOUNT ON
	SELECT lo.LoteID
			,lo.OrganizacionID
			,co.CorralID
			,co.Codigo
			,lo.Lote
			,lo.TipoCorralID
			,lo.TipoProcesoID
			,lo.FechaInicio
			,lo.CabezasInicio
			,lo.FechaCierre
			,lo.Cabezas
			,lo.FechaDisponibilidad
			,lo.DisponibilidadManual
			,lo.Activo
			,lo.FechaCreacion
			,lo.UsuarioCreacionID
			,lo.FechaModificacion
			,lo.UsuarioModificacionID
	FROM Lote lo 
	inner join Corral co on lo.CorralID = co.CorralID
	WHERE lo.OrganizacionID = @OrganizacionID
	and lo.Activo = @Activo
	SET NOCOUNT OFF
END

GO
