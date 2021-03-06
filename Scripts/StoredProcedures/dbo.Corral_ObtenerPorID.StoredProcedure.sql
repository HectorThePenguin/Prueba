USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 11/11/2013
-- Description:	Obtiene un Corral.
-- [Corral_ObtenerPorID] 1
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorID]
@CorralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	    
		C.CorralID
		,C.OrganizacionID
		,o.Descripcion as [Organizacion]
		,C.Codigo
		,C.TipoCorralID
		,tc.Descripcion as [TipoCorral]
		,C.Capacidad
		,C.MetrosLargo
		,C.MetrosAncho
		,C.Seccion
		,C.Orden
		,C.Activo
		,C.FechaCreacion
		,C.UsuarioCreacionID
		,tc.GrupoCorralID
		FROM Corral C
		inner join Organizacion o on o.OrganizacionID = c.OrganizacionID
		inner join TipoCorral tc on tc.TipoCorralID = c.TipoCorralID
		WHERE CorralID = @CorralID
	SET NOCOUNT OFF;
END

GO
