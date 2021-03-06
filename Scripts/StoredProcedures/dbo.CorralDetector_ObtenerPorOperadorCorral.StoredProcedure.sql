USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerPorOperadorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralDetector_ObtenerPorOperadorCorral]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerPorOperadorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CorralDetector_ObtenerPorOperadorCorral 1,1
--======================================================
CREATE PROCEDURE [dbo].[CorralDetector_ObtenerPorOperadorCorral]
@OperadorID INT,
@CorralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CD.CorralDetectorID,
		CD.OperadorID,
		CD.CorralID,
		CD.Activo,
		C.Codigo AS CodigoCorral,
		O.Nombre AS NombreOperador,
		O.ApellidoPaterno AS ApellidoPaternoOperador,
		O.ApellidoMaterno AS ApellidoMaternoOperador,
		O.CodigoSAP,
		Org.OrganizacionID,
		Org.Descripcion AS Organizacion
	FROM CorralDetector CD
	INNER JOIN Corral C
		ON (CD.CorralID = C.CorralID
			AND C.CorralID = @CorralID)
	INNER JOIN Operador O
		ON (CD.OperadorID = O.OperadorID
			AND C.OrganizacionID = O.OrganizacionID
			AND O.OperadorID = @OperadorID)
	INNER JOIN Organizacion Org
		ON (O.OrganizacionID = Org.OrganizacionID
			AND C.OrganizacionID = Org.OrganizacionID)
	SET NOCOUNT OFF;
END

GO
