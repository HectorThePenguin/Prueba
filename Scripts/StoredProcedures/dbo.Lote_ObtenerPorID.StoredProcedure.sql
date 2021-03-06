USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Gilberto Carranza
Fecha	  : 15/11/2013
Proposito : Obtiene un Lote por ID
Lote_ObtenerPorID 1
************************************/
CREATE PROCEDURE [dbo].[Lote_ObtenerPorID]
@LoteID INT
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
	WHERE LoteID = @LoteID
	SET NOCOUNT OFF
END

GO
