USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_EliminarProgramacionReimplanteXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionReimplante_EliminarProgramacionReimplanteXML]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_EliminarProgramacionReimplanteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Ricardo.lopez
-- Fecha: 27-12-2013
-- Descripci?n:	Eliminar Reimplante
-- ProgramacionReimplante_EliminarProgramacionReimplanteXML
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionReimplante_EliminarProgramacionReimplanteXML] @XmlReimplante XML
AS
BEGIN
	CREATE TABLE #tProgramacionReimplante (
		FolioProgramacionID INT
		,UsuarioModificacionID INT
		)

	INSERT INTO #tProgramacionReimplante
	SELECT t.item.value('./FolioProgramacionID[1]', 'INT') AS FolioProgramacionID
		,t.item.value('./UsuarioModificacionID[1]', 'INT') AS UsuarioModificacionID
	FROM @XmlReimplante.nodes('ROOT/Reimplante') AS T(item)

	UPDATE PR
	SET Activo = 1
		,FechaModificacion = GETDATE()
		,UsuarioModificacionID = tPR.UsuarioModificacionID
	FROM ProgramacionReimplante PR
	INNER JOIN #tProgramacionReimplante tPR ON (PR.FolioProgramacionID = tPR.FolioProgramacionID)
END

GO
