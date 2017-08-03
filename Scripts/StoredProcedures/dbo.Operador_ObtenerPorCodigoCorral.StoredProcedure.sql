USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorCodigoCorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerPorCodigoCorral]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorCodigoCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Ramses Santos
-- Create date: 2014-02-17
-- Origen: APInterfaces
-- Description:	Obtiene un el operador del corral
-- EXEC Operador_ObtenerPorCodigoCorral 'ce1', 4
--=============================================
CREATE PROCEDURE [dbo].[Operador_ObtenerPorCodigoCorral]
	@Codigo CHAR(10),
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
		Op.OperadorID, Op.Nombre, Op.ApellidoPaterno, Op.ApellidoMaterno, Op.CodigoSAP, Op.RolID, Op.OrganizacionID, Op.Activo
	FROM Corral AS C 
	INNER JOIN Corraldetector AS CD ON (C.CorralID = CD.CorralID)
	INNER JOIN Operador AS Op ON (Op.OperadorID = CD.OperadorID)
	WHERE Codigo = @Codigo AND C.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
