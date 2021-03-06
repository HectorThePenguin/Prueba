USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_GuardarXml]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_GuardarXml]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_GuardarXml]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 12/11/2014
-- Description:  Guardar la lista de Almacen Usuario
-- AlmacenUsuario_GuardarXml
-- ===============================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_GuardarXml] @AlmacenUsuarioXML XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @AlmacenUsuario AS TABLE (
		AlmacenUsuarioID INT
		,AlmacenID INT
		,UsuarioID INT
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT @AlmacenUsuario (
		AlmacenUsuarioID
		,AlmacenID
		,UsuarioID
		,Activo
		,UsuarioCreacionID 
		,UsuarioModificacionID
		)
	SELECT AlmacenUsuarioID = t.item.value('./AlmacenUsuarioID[1]', 'INT')
		,AlmacenID = t.item.value('./AlmacenID[1]', 'INT')
		,UsuarioID = t.item.value('./UsuarioID[1]', 'INT')		
		,Activo = t.item.value('./Activo[1]', 'bit')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'int')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'int')		
	FROM @AlmacenUsuarioXML.nodes('ROOT/AlmacenUsuario') AS T(item)
	INSERT AlmacenUsuario (		
		AlmacenID
		,UsuarioID
		,Activo
		,UsuarioCreacionID 		
		,FechaCreacion
		)
	SELECT 
		AlmacenID
		,UsuarioID
		,Activo
		,UsuarioCreacionID
		,GETDATE()
	FROM @AlmacenUsuario
	WHERE AlmacenUsuarioID = 0
	UPDATE au
	SET au.AlmacenID = aux.AlmacenID
		,au.UsuarioID = aux.UsuarioID		
		,Activo = aux.Activo
		,FechaModificacion = GETDATE()
		,UsuarioModificacionID = aux.UsuarioModificacionID
	FROM AlmacenUsuario au
	INNER JOIN @AlmacenUsuario aux ON au.AlmacenUsuarioID = aux.AlmacenUsuarioID
	SET NOCOUNT OFF;
END

GO
