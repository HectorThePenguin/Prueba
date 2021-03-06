USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerLectorComederoPorOrganizacionId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_ObtenerLectorComederoPorOrganizacionId]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerLectorComederoPorOrganizacionId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jorge Luis Velazquez Araujo  
-- Create date: 02-04-2014  
-- Description:  Obtiene la configuraci�n de las formulas, para el sistema Lector de Comedero  
-- ConfiguracionFormula_ObtenerLectorComederoPorOrganizacionId 1   
-- =============================================  
CREATE PROCEDURE [dbo].[ConfiguracionFormula_ObtenerLectorComederoPorOrganizacionId]
@OrganizacionID int
AS
BEGIN
	SELECT ConfiguracionFormulaID
		,OrganizacionID
		,FormulaID
		,PesoInicioMinimo
		,PesoInicioMaximo
		,TipoGanado
		,PesoSalida
		,FormulaSiguienteID
		,DiasEstanciaMinimo
		,DiasEstanciaMaximo
		,DiasTransicionMinimo
		,DiasTransicionMaximo
		,Disponibilidad
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		,FechaModificacion
		,UsuarioModificacionID
	FROM ConfiguracionFormula
	WHERE OrganizacionID = @OrganizacionID
	ORDER BY ConfiguracionFormulaID
END

GO
