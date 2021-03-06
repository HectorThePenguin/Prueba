USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCorralIDXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorCorralIDXML]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCorralIDXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 22/08/2014
-- Description: 
-- SpName     : Corral_ObtenerPorCorralIDXML '<ROOT><Corrales><CorralID>1</CorralID></Corrales></ROOT>'
--======================================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorCorralIDXML]
@XmlCorral XML
AS
BEGIN
	SET NOCOUNT ON
		SELECT C.CorralID
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
		FROM
		(
			SELECT T.N.value('./CorralID[1]','INT') AS CorralID
			FROM @XmlCorral.nodes('/ROOT/Corrales') as T(N)
		) A
		INNER JOIN Corral C
			ON (A.CorralID = C.CorralID
				AND C.Activo = 1)
		INNER JOIN Organizacion O
			ON (C.OrganizacionID = O.OrganizacionID)
		INNER JOIN TipoCorral TC
			ON (C.TipoCorralID = TC.TipoCorralID)
	SET NOCOUNT OFF
END

GO
