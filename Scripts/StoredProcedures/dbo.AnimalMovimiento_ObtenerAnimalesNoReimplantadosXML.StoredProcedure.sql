USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Gilberto Carranza
-- Create date: 2015/05/18
-- Description: SP para Obtener Animales no reimplantados
-- AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML] @OrganizacionID INT
	,@LoteXML XML
AS
BEGIN
	DECLARE @CorralIDDestino INT;
	DECLARE @LoteIDDestino INT;

	/* Se obtiene el corral destino */
	IF @OrganizacionID = 1
		SELECT @CorralIDDestino = C.CorralID
			,@LoteIDDestino = L.LoteID
		FROM Corral C
		INNER JOIN Lote L ON L.CorralID = C.CorralID
			AND L.Activo = 1
		WHERE C.Codigo = 'R99'
			AND C.OrganizacionID = @OrganizacionID
	ELSE
		SELECT @CorralIDDestino = C.CorralID
			,@LoteIDDestino = L.LoteID
		FROM Corral C
		INNER JOIN Lote L ON L.CorralID = C.CorralID
			AND L.Activo = 1
		WHERE C.Codigo = 'R99'
			AND C.OrganizacionID = @OrganizacionID

	IF @OrganizacionID = 4
		SELECT @CorralIDDestino = C.CorralID
			,@LoteIDDestino = L.LoteID
		FROM Corral C
		INNER JOIN Lote L ON L.CorralID = C.CorralID
			AND L.Activo = 1
		WHERE C.Codigo = 'RR99'
			AND C.OrganizacionID = @OrganizacionID

	CREATE TABLE #tLote (LoteID INT)

	INSERT INTO #tLote
	SELECT t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @LoteXML.nodes('ROOT/Lotes') AS T(item)

	SELECT AM.AnimalID
		,AM.AnimalMovimientoID
		,AM.OrganizacionID
		,ISNULL(@CorralIDDestino, 0) AS CorralID
		,ISNULL(@LoteIDDestino, 0) AS LoteID
		,AM.FechaMovimiento
		,AM.Peso
		,AM.Temperatura
		,AM.TipoMovimientoID
		,AM.TrampaID
		,AM.OperadorID
		,AM.Observaciones
		,AM.Activo
		,AM.FechaCreacion
		,AM.UsuarioCreacionID
		,AM.FechaModificacion
		,AM.UsuarioModificacionID
		,AM.LoteID LoteIDOrigen
		,AM.CorralID CorralIDOrigen
		,AM.AnimalMovimientoIDAnterior
	FROM Animal A(NOLOCK)
	INNER JOIN AnimalMovimiento AM(NOLOCK) ON (
			A.AnimalID = AM.AnimalID
			AND AM.Activo = 1
			AND AM.OrganizacionID = @OrganizacionID
			AND AM.TipoMovimientoID != 6
			)
	INNER JOIN #tLote tL
		ON (AM.LoteID = tL.LoteID)
	WHERE A.Activo = 1

	DROP TABLE #tLote
END

GO
