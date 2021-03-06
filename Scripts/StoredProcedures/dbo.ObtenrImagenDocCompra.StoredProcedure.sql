USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ObtenrImagenDocCompra]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ObtenrImagenDocCompra]
GO
/****** Object:  StoredProcedure [dbo].[ObtenrImagenDocCompra]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenrImagenDocCompra]
(
@OrganizacionId INT
,@FolioCompra INT
)  
AS
BEGIN 
SELECT 
IC.Imagen
FROM sukarne.dbo.CacImagenCompra AS  IC (NOLOCK)
INNER JOIN sukarne.dbo.CacImportarRecibosCompra AS IRC(NOLOCK)
ON IC.OrganizacionId = IRC.OrganizacionId
AND IC.FolioCompra = IRC.FolioCompra
WHERE	IRC.OrganizacionId = @OrganizacionId
	AND IRC.FolioCompra = @FolioCompra 
END

GO
