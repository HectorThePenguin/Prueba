USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerLectorComedero]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jorge Luis Velazquez Araujo  
-- Create date: 02-04-2014  
-- Description:  Obtiene las formulas, para el sistema Lector de Comedero  
-- Formula_ObtenerLectorComedero   
-- =============================================  
CREATE PROCEDURE [dbo].[Formula_ObtenerLectorComedero]
AS
BEGIN
	SELECT FormulaID AS IdFormula
		,Descripcion AS [DescripcionCorta]
		,Descripcion
		,Activo AS STATUS
	FROM Formula
	ORDER BY FormulaID
END

GO
