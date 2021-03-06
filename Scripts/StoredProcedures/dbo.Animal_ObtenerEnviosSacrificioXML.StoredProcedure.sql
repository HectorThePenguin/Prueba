USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerEnviosSacrificioXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerEnviosSacrificioXML]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerEnviosSacrificioXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 13/04/2015
-- Description:  Obtiene animales por su disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerEnviosSacrificioXML]
@XmlLote XML
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tLote
	(
		LoteID INT
	)

	INSERT INTO #tLote
	SELECT 
		t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T(item)

	DECLARE @Animal AS TABLE 
	(
		AnimalID BIGINT
		,LoteID INT
	)

	INSERT INTO @Animal (
		AnimalID
		,LoteID
		)
	SELECT A.AnimalID
		,  tL.LoteID
	FROM #tLote tL
	INNER JOIN InterfaceSalidaTraspasoDetalle ISTD
		ON (tL.LoteID = ISTD.LoteID)
	INNER JOIN InterfaceSalidaTraspasoCosto ISTC
		ON (ISTD.InterfaceSalidaTraspasoDetalleID = ISTC.InterfaceSalidaTraspasoDetalleID)
	INNER JOIN Animal A(NOLOCK)
		ON (ISTC.AnimalID = A.AnimalID
			AND A.Activo = 1)

	SELECT AnimalID
		,LoteID
	FROM @Animal	
	GROUP BY AnimalID
			,LoteID

	SET NOCOUNT OFF;
END

GO
