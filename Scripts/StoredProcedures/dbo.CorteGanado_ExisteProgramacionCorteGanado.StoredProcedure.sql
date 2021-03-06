USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ExisteProgramacionCorteGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ExisteProgramacionCorteGanado]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ExisteProgramacionCorteGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cesar.valdez
-- Fecha: 2013-12-17
-- Origen: APInterfaces
-- Descripci�n:	Obtiene si existe programacion corte de ganagado 
-- EXEC CorteGanado_ExisteProgramacionCorteGanado
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ExisteProgramacionCorteGanado]
	@OrganizacionID INT
AS
BEGIN
	SELECT COUNT(1) 
	  FROM ProgramacionCorte 
	 WHERE Activo = 1
	   AND OrganizacionID = @OrganizacionID
	-- AND CAST(FechaInicioCorte AS DATE) = CAST(GETDATE() AS DATE)
END

GO
