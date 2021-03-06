USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorRolOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerPorRolOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorRolOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/02/11
-- Description: Obtiene un operador por ID, Rol y Organizacion
-- Operador_ObtenerPorRolOrganizacion 1, 1, 4
--=============================================
CREATE PROCEDURE [dbo].[Operador_ObtenerPorRolOrganizacion]
@OperadorID int
, @RolID INT
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		OperadorID,
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		CodigoSAP,
		RolID,
		UsuarioID,
		Activo,
		OrganizacionID
	FROM Operador
	WHERE OperadorID = @OperadorID
		AND RolID = @RolID
		AND OrganizacionID = @OrganizacionID
END

GO
