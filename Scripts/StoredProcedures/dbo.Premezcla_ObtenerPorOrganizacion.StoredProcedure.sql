USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Premezcla_ObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 10-12-2014
-- Description:	Otiene un listado de premezclas de una organizacion
-- Premezcla_ObtenerPorOrganizacion 1
-- =============================================
CREATE PROCEDURE [dbo].[Premezcla_ObtenerPorOrganizacion]
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT 
			P.PremezclaID,
			P.OrganizacionID,
			P.Descripcion,
			P.ProductoID,
			P.Activo
		INTO #tPremezcla
		FROM Premezcla (NOLOCK) P
		INNER JOIN Organizacion (NOLOCK) O ON (O.OrganizacionID = P.OrganizacionID)
		WHERE O.OrganizacionID = @OrganizacionID
		SELECT PremezclaID,
			OrganizacionID,
			Descripcion,
			ProductoID,
			Activo
		FROM #tPremezcla
		SELECT PD.PremezclaDetalleID
			,  PD.PremezclaID
			,  PD.ProductoID
			,  PD.Porcentaje
		FROM #tPremezcla tP
		INNER JOIN PremezclaDetalle PD
			ON (tP.PremezclaID = PD.PremezclaID)
		DROP TABLE #tPremezcla
	SET NOCOUNT OFF
END

GO
