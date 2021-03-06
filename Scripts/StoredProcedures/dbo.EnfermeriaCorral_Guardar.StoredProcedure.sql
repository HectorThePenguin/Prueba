USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EnfermeriaCorral_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EnfermeriaCorral_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[EnfermeriaCorral_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 06/06/2014
-- Description:  Guardar el detalle de Enfermeria Corral
-- EnfermeriaCorral_Guardar
-- ===============================================================
CREATE PROCEDURE [dbo].[EnfermeriaCorral_Guardar] @XmlEnfermeriaCorral XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @EnfermeriaCorral AS TABLE (
		EnfermeriaCorralID INT
		,EnfermeriaID INT
		,CorralID INT
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT @EnfermeriaCorral (
		EnfermeriaCorralID
		,EnfermeriaID
		,CorralID
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT EnfermeriaCorralID = t.item.value('./EnfermeriaCorralID[1]', 'INT')
		,EnfermeriaID = t.item.value('./EnfermeriaID[1]', 'INT')
		,CorralID = t.item.value('./CorralID[1]', 'INT')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @XmlEnfermeriaCorral.nodes('ROOT/EnfermeriaCorral') AS T(item)
	UPDATE ec
	SET EnfermeriaID = dt.EnfermeriaID
		,CorralID = dt.CorralID
		,Activo = dt.Activo
		,[FechaModificacion] = GETDATE()
		,UsuarioModificacionID = dt.UsuarioModificacionID
	FROM EnfermeriaCorral ec
	INNER JOIN @EnfermeriaCorral dt ON dt.EnfermeriaCorralID = ec.EnfermeriaCorralID
	INSERT EnfermeriaCorral (
		EnfermeriaID
		,CorralID
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT EnfermeriaID
		,CorralID
		,Activo
		,GETDATE()
		,UsuarioCreacionID
	FROM @EnfermeriaCorral
	WHERE EnfermeriaCorralID = 0
	SET NOCOUNT OFF;
END

GO
