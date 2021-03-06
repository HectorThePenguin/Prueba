USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ExisteProgramacionReimplante]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ExisteProgramacionReimplante]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ExisteProgramacionReimplante]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Ricardo.Lopez
-- Fecha: 26-12-2013
-- Descripci�n:	Verifica Existencia de Reimplantes
-- EXEC ReimplanteGanado_ExisteProgramacionReimplante 1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ExisteProgramacionReimplante]
	@OrganizacionID INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT COUNT(1) Reimplante 
	  FROM ProgramacionReimplante 
	 WHERE Activo = 1
	   AND OrganizacionID = @OrganizacionID
	   AND CONVERT(CHAR(8),Fecha,112) 
		   BETWEEN CONVERT(CHAR(8),GETDATE()-7,112) AND CONVERT(CHAR(8),GETDATE(),112)
SET NOCOUNT OFF;	
END

GO
