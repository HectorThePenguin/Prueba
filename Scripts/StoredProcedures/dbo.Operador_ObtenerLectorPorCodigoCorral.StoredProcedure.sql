USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerLectorPorCodigoCorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerLectorPorCodigoCorral]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerLectorPorCodigoCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Ramses Santos
-- Create date: 2014-03-24
-- Origen: APInterfaces
-- Description:	Obtiene un el operador del corral
-- EXEC Operador_ObtenerLectorPorCodigoCorral 'ce1', 4
--=============================================
CREATE PROCEDURE [dbo].[Operador_ObtenerLectorPorCodigoCorral]
	@Codigo CHAR(10),
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
		Op.OperadorID, Op.Nombre, Op.ApellidoPaterno, Op.ApellidoMaterno, Op.CodigoSAP, Op.RolID, Op.OrganizacionID, Op.Activo
	FROM Corral (NOLOCK) AS C 
	INNER JOIN CorralLector (NOLOCK) AS CD ON (C.CorralID = CD.CorralID)
	INNER JOIN Operador (NOLOCK) AS Op ON (Op.OperadorID = CD.OperadorID)
	WHERE Codigo = @Codigo AND C.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
