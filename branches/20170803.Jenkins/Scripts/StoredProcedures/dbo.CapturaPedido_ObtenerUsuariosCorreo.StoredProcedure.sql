USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CapturaPedido_ObtenerUsuariosCorreo]    Script Date: 02/06/2017 03:36:45 p.m. ******/
DROP PROCEDURE [dbo].[CapturaPedido_ObtenerUsuariosCorreo]
GO
/****** Object:  StoredProcedure [dbo].[CapturaPedido_ObtenerUsuariosCorreo]    Script Date: 02/06/2017 03:36:45 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Murillo Mayorquín Héctor Adrián
-- Create date: 02-06-2017
-- Description: Procedimiento almacenado que obtiene los usuarios a los que se les enviara un correo.
-- SpName     : CapturaPedido_ObtenerUsuariosCorreo '<IntList><Int>4</Int><Int>6</Int></IntList>'
--======================================================  
CREATE PROCEDURE [dbo].[CapturaPedido_ObtenerUsuariosCorreo]
		@XmlRoles XML
AS
BEGIN
	SET NOCOUNT ON;
	
	CREATE TABLE ##tRol(RolID INT)
	
	INSERT INTO ##tRol(RolID)
		SELECT T.item.value('.', 'INT')
		FROM  @XmlRoles.nodes('IntList/Int') AS T(item);

	SELECT u.UsuarioID,
		   u.Nombre,
		   u.OrganizacionID,
		   u.UsuarioActiveDirectory,
		   u.Corporativo,
		   u.Activo
	FROM Usuario u
	INNER JOIN Operador o ON (o.UsuarioID = u.UsuarioID)
	WHERE o.RolID = ANY(SELECT RolID FROM ##tRol) AND u.Activo = 1 AND o.Activo = 1;
	
	DROP TABLE ##tRol;
	
	SET NOCOUNT OFF;
END
GO