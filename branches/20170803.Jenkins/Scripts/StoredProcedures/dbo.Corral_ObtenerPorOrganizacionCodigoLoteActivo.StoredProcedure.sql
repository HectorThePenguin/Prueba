USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorOrganizacionCodigoLoteActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorOrganizacionCodigoLoteActivo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorOrganizacionCodigoLoteActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Edgar Villarreal
-- Create date: 01/12/2015
-- Description:	Obtener Corral por codigo con lote activo
-- Corral_ObtenerPorOrganizacionCodigoLoteActivo '679', 1, 1
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorOrganizacionCodigoLoteActivo]
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
		INNER JOIN Lote L ON L.CorralID = C.CorralID AND L.Activo = @Activo
		INNER JOIN Organizacion O(NOLOCK)
			ON (C.OrganizacionID = O.OrganizacionID)
		WHERE (c.Codigo = @Codigo)
		  AND C.OrganizacionID =  @OrganizacionID
		  AND c.Activo = @Activo
END

GO
