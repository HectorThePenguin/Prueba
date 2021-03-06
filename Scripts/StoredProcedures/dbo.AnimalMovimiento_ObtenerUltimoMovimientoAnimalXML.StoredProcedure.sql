USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimalXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimalXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimalXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 11/02/2015
-- Description: SP obtener los ultimos movimientos de los animales
-- EXEC [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimalXML] 1, 4
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoAnimalXML]
	@AnimalXml xml,
	--@AnimalID BIGINT,
	@OrganizacionID INT
AS
BEGIN


CREATE TABLE #tAnimal(AnimalID INT)

INSERT INTO #tAnimal
	SELECT
		t.item.value('./AnimalID[1]', 'BIGINT') AS AnimalID
	FROM @AnimalXml.nodes('ROOT/Animales') AS T (item)


	SELECT 
		am.AnimalID,
		AnimalMovimientoID,
		OrganizacionID,
		CorralID,
		LoteID,
		FechaMovimiento,
		Peso,
		Temperatura,
		TipoMovimientoID,
		TrampaID,
		OperadorID,
		Observaciones,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM AnimalMovimiento am(NOLOCK)
	inner join #tAnimal a on am.AnimalID = a.AnimalID
	WHERE OrganizacionID = @OrganizacionID
	AND Activo = 1
END

GO
