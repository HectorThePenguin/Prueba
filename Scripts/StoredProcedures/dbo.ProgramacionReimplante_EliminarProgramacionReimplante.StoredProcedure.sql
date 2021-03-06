USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_EliminarProgramacionReimplante]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionReimplante_EliminarProgramacionReimplante]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_EliminarProgramacionReimplante]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Ricardo.lopez
-- Fecha: 27-12-2013
-- Descripci?n:	Eliminar Reimplante
-- EXEC ProgramacionReimplante_EliminarProgramacionReimplante 24
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionReimplante_EliminarProgramacionReimplante]
	@FolioProgReimplante INT
AS
BEGIN
	UPDATE ProgramacionReimplante
	   SET Activo = 1,
		   FechaModificacion = GETDATE()
	 WHERE FolioProgramacionID = @FolioProgReimplante
END

GO
