USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Julian Carranza Castro
-- Create date: 2013/11/11
-- Description: 
-- Lote_ObtenerTodos
--=============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON
	SELECT LoteID
			,OrganizacionID
			,CorralID
			,TipoCorralID
			,TipoProcesoID
			,FechaInicio
			,CabezasInicio
			,FechaCierre
			,Cabezas
			,FechaDisponibilidad
			,DisponibilidadManual
			,Activo
			,FechaCreacion
			,UsuarioCreacionID
			,FechaModificacion
			,UsuarioModificacionID
	FROM Lote
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF
END

GO
