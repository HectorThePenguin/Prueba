USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ValidarCorralPorEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ValidarCorralPorEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ValidarCorralPorEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Ramses Santos
-- Create date: 2014-02-17
-- Origen: APInterfaces
-- Description:	Valida si el corral pertenece a la enfermeria
-- EXEC Corral_ValidarCorralPorEnfermeria '010', 1, 4
--=============================================
CREATE PROCEDURE [dbo].[Corral_ValidarCorralPorEnfermeria]
	@Codigo CHAR(10),
	@EnfermeriaID INT,
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*)
	FROM Corral AS C 
	INNER JOIN EnfermeriaCorral AS EC ON (C.CorralID = EC.CorralID)
	WHERE Codigo = @Codigo AND EC.EnfermeriaID = @EnfermeriaID AND C.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
