USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorOrganizacionTipoParametroClave]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_ObtenerPorOrganizacionTipoParametroClave]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorOrganizacionTipoParametroClave]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: C�sar Valdez
-- Create date: 12/02/2013
-- Description: Obtiene la lista de Parametros por organizacion, tipo de parametro y clave del parametro en especifico
-- Empresa: Apinterfaces
-- EXEC Parametro_ObtenerPorOrganizacionTipoParametroClave 4,1,'ubicacionFotos'
-- =============================================
CREATE PROCEDURE [dbo].[Parametro_ObtenerPorOrganizacionTipoParametroClave]
	@OrganizacionID INT,
	@TipoParametroID INT,
	@Clave VARCHAR(30)
AS
BEGIN
	SELECT TOP 1 PO.ParametroID, 
		   PO.Valor,
		   P.Clave, 
		   P.Descripcion, 
		   PO.OrganizacionID
	  FROM ParametroOrganizacion PO (NOLOCK) 
	 INNER JOIN Parametro P (NOLOCK)  ON P.ParametroID = PO.ParametroID 
	 INNER JOIN TipoParametro TP(NOLOCK)  ON TP.TipoParametroID = P.TipoParametroID
	 WHERE PO.OrganizacionID = @OrganizacionID
	   AND P.Clave = @Clave
	   AND P.TipoParametroID = @TipoParametroID
	   AND PO.Activo = 1
END

GO
