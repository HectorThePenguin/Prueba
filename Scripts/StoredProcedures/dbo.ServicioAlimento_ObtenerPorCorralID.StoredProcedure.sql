USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_ObtenerPorCorralID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ServicioAlimento_ObtenerPorCorralID]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_ObtenerPorCorralID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo	
-- Create date: 02-09-2014
-- Description:	Consulta informacion de alimento del Corral
-- [ServicioAlimento_ObtenerPorCorralID] 
-- =============================================
CREATE PROCEDURE [dbo].[ServicioAlimento_ObtenerPorCorralID]
@OrganizacionId INT 
,@CorralID INT
AS
BEGIN 
SET NOCOUNT ON;
	SELECT 		
		c.Codigo,
		s.KilosProgramados,
		f.Descripcion AS Formula,
		s.Comentarios,
		s.Fecha,
		c.CorralID,
		s.FormulaID,
		s.OrganizacionID,
		s.ServicioID
	FROM ServicioAlimento s
	INNER JOIN Corral c ON s.CorralID = c.CorralID
	INNER JOIN Formula f ON f.FormulaID = s.FormulaID
	WHERE C.OrganizacionID = @OrganizacionID
	AND C.CorralID = @CorralID
	AND s.Activo = 1
SET NOCOUNT OFF;	
END

GO
