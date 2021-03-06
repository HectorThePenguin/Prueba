USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_InformacionDiaria]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ServicioAlimento_InformacionDiaria]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_InformacionDiaria]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Noel.Gerardo
-- Create date: 07-12-2013
-- Description:	Consulta informacion de alimento al dia de hoy GETDATE() 
-- Empresa: Apinterfaces
-- [dbo].[ServicioAlimento_InformacionDiaria] 2
-- =============================================
CREATE PROCEDURE [dbo].[ServicioAlimento_InformacionDiaria]
@OrganizacionId INT 
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
	FROM ServicioAlimento s(NOLOCK)
	INNER JOIN Corral c(NOLOCK) ON s.CorralID = c.CorralID
	INNER JOIN Formula f(NOLOCK) ON f.FormulaID = s.FormulaID
	WHERE Convert(varchar(10),s.Fecha,126)=Convert(varchar(10),GETDATE(),126)
	AND C.OrganizacionID = @OrganizacionID
	AND s.Activo = 1
SET NOCOUNT OFF;	
END

GO
