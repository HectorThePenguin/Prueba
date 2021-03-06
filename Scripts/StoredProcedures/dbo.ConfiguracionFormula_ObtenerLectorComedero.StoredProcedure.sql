USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_ObtenerLectorComedero]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jorge Luis Velazquez Araujo  
-- Create date: 02-04-2014  
-- Description:  Obtiene la configuraci�n de las formulas, para el sistema Lector de Comedero  
-- ConfiguracionFormula_ObtenerLectorComedero   
-- =============================================  
CREATE PROCEDURE [dbo].[ConfiguracionFormula_ObtenerLectorComedero]
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
	ORDER BY ConfiguracionFormulaID
END

GO
