USE [SIAP]
GO
/**** Object:  StoredProcedure [dbo].[AdministracionRuteo_ObtenerPorID]    Script Date: 19/05/2017 09:31:44 a.m. ****/
DROP PROCEDURE [dbo].[AdministracionRuteo_ObtenerPorID]
GO
/**** Object:  StoredProcedure [dbo].[AdministracionRuteo_ObtenerPorID]    Script Date: 19/05/2017 09:31:44 a.m. ****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Luis Manuel García López
-- Create date: 2017/05/19
-- Description: Obtiene la informacion de rute por id
-- AdministracionRuteo_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[AdministracionRuteo_ObtenerPorID]
@RuteoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		ce.RuteoID,
		ce.OrganizacionOrigenID,
		oo.Descripcion as [Origen],
		ce.OrganizacionDestinoID,
		od.Descripcion as [Destino],
		ce.NombreRuteo,
		ce.Activo
	FROM Ruteo ce
	INNER JOIN Organizacion oo on oo.OrganizacionID = ce.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = ce.OrganizacionDestinoID
	WHERE RuteoID = @RuteoID
	SET NOCOUNT OFF;
END
