USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferencia_ObtenerTipoProceso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferencia_ObtenerTipoProceso]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferencia_ObtenerTipoProceso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar.villarreal
-- Create date: 13/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CorteTransferencia_ObtenerTipoProceso 4,1
--======================================================
CREATE PROCEDURE [dbo].[CorteTransferencia_ObtenerTipoProceso]
@OrganizacionID INT,
@ActivoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 TOrg.TipoProcesoID from Organizacion O
	INNER JOIN TipoOrganizacion AS TOrg ON TOrg.TipoOrganizacionID = O.TipoOrganizacionID
	WHERE O.OrganizacionID = @OrganizacionID 
				AND TOrg.Activo = @ActivoID 
				AND O.Activo = @ActivoID
	SET NOCOUNT OFF;
END

GO
