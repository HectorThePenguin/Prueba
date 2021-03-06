USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorOrganizacionCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorOrganizacionCodigo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorOrganizacionCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Gilberto Carranza
-- Create date: 24/10/2013
-- Description:	Obtener listado de Corrales Por Organizacion.
-- Corral_ObtenerPorOrganizacionCodigo '001', 4, 1
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorOrganizacionCodigo]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
			C.CorralID
			,C.OrganizacionID
			,C.Codigo
			,C.TipoCorralID
			,C.Capacidad
			,C.MetrosLargo
			,C.MetrosAncho
			,C.Seccion
			,C.Orden
			,C.Activo
			,C.FechaCreacion
			,C.UsuarioCreacionID
			,C.FechaModificacion
			,C.UsuarioModificacionID
		FROM Corral C(NOLOCK)
		INNER JOIN Organizacion O(NOLOCK)
			ON (C.OrganizacionID = O.OrganizacionID)
		WHERE (c.Codigo = @Codigo)
		  AND C.OrganizacionID =  @OrganizacionID
		  AND c.Activo = @Activo
END

GO
