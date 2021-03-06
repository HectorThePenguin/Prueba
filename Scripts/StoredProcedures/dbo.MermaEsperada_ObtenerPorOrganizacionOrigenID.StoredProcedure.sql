USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaEsperada_ObtenerPorOrganizacionOrigenID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaEsperada_ObtenerPorOrganizacionOrigenID]
GO
/****** Object:  StoredProcedure [dbo].[MermaEsperada_ObtenerPorOrganizacionOrigenID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Antonio Delgado Viedas
-- Create date: 30/06/2015
-- Description: 
-- SpName     : MermaEsperada_ObtenerPorOrganizacionOrigenID 27,1
--======================================================
CREATE PROCEDURE [dbo].[MermaEsperada_ObtenerPorOrganizacionOrigenID]
@OrganizacionOrigenID int,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ME.MermaEsperadaID,
		OrganizacionOrigenID,
		O.Descripcion AS OrganizacionOrigen,
		OrganizacionDestinoID,
		OD.Descripcion AS OrganizacionDestino,
		ME.Merma,
		ME.Activo
	FROM MermaEsperada ME(NOLOCK)
	INNER JOIN Organizacion O (NOLOCK) ON (ME.OrganizacionOrigenID = O.OrganizacionID)
	INNER JOIN Organizacion OD (NOLOCK) ON (ME.OrganizacionDestinoID = OD.OrganizacionID)
	WHERE @OrganizacionOrigenID in (OrganizacionOrigenID, 0)
	AND ME.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
