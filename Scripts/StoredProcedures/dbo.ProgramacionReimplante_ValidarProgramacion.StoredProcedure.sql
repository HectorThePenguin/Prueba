USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_ValidarProgramacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionReimplante_ValidarProgramacion]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_ValidarProgramacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Description:  Especifica si existen programacion para la fecha especificada
-- ProgramacionReimplante_ValidarProgramacion 2 ,'20150210'
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionReimplante_ValidarProgramacion]
@OrganizacionID INT,
@Fecha smalldatetime
AS
  BEGIN
      SET NOCOUNT ON;
		SELECT FolioProgramacionId 
		  FROM ProgramacionReimplante 
		 WHERE CAST(Fecha AS DATE) = CAST(@Fecha AS DATE)
		 and OrganizacionID = @OrganizacionID
      SET NOCOUNT OFF;
  END

GO
