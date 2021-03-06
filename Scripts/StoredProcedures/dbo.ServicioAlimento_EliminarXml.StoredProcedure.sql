USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_EliminarXml]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ServicioAlimento_EliminarXml]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_EliminarXml]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014
-- Description: Eliminar ServicioAlimento 
-- ServicioAlimento_EliminarXml
-- =============================================
CREATE PROCEDURE [dbo].[ServicioAlimento_EliminarXml] @XmlServicioAlimento XML
AS
BEGIN
	CREATE TABLE #ServicioAlimento (
		CorralID INT
		,UsuarioModificacionID INT
		)
	INSERT INTO #ServicioAlimento
	SELECT CorralID = t.item.value('./CorralID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @XmlServicioAlimento.nodes('ROOT/ServicioAlimento') AS T(item)
	UPDATE sa
	SET sa.Activo = 0
		,sa.FechaModificacion = GETDATE()
		,UsuarioModificacionID = satmp.UsuarioModificacionID
	FROM ServicioAlimento sa
	INNER JOIN #ServicioAlimento satmp ON sa.CorralID = satmp.CorralID
	WHERE sa.Activo = 1
END

GO
