IF EXISTS (SELECT '' FROM sysobjects WHERE ID = OBJECT_ID('[dbo].[Lote_ConAnimalesObtenerPorOrganizacionID]') AND xtype = 'P')
BEGIN
	DROP PROCEDURE [dbo].[Lote_ConAnimalesObtenerPorOrganizacionID]
END
GO 
CREATE PROCEDURE [dbo].[Lote_ConAnimalesObtenerPorOrganizacionID]
@OrganizacionId INT
AS          
BEGIN          
	SET NOCOUNT ON;     

	-- Autor: Sergio Gamez
	-- Fecha: 2015/09/26
	-- Descipción:  Consultar loteid y codigo de corral por organización, debe tener animales disponibles y estar activos.
	-- Lote_ConAnimalesObtenerPorOrganizacionID 1

	SELECT 
		L.LoteID, 
		C.Codigo
	FROM Lote L (NOLOCK) 
	INNER JOIN Corral C (NOLOCK)
		ON C.CorralId = L.CorralID AND C.Activo = 1
	INNER JOIN AnimalMovimiento AM (NOLOCK)
		ON AM.LoteId = L.LoteId AND AM.Activo = 1 AND TipoMovimientoID NOT IN(8,11,16)
	WHERE L.OrganizacionId = @OrganizacionId AND L.Activo = 1
	GROUP BY L.LoteID, C.Codigo

	SET NOCOUNT OFF;          
END
GO