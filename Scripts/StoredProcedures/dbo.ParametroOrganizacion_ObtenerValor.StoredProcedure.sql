USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerValor]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_ObtenerValor]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerValor]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Emir Lezama
-- Create date: 07/11/2014
-- Description: 
-- SpName     : exec Silo_ObtenerPorOrganizacionID
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_ObtenerValor]
@OrganizacionID INT,
@Descripcion VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CAST(PO.valor AS INT) AS Valor
	FROM ParametroOrganizacion AS PO INNER JOIN Parametro AS P 
	ON PO.ParametroID = P.ParametroID 
	WHERE P.Descripcion=@Descripcion AND PO.OrganizacionID=@OrganizacionID
	SET NOCOUNT OFF;
END

GO
