USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerCheckListCompleto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerCheckListCompleto]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerCheckListCompleto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 07/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Lote_ObtenerCheckListCompleto 4,3
--======================================================
CREATE PROCEDURE [dbo].[Lote_ObtenerCheckListCompleto] @OrganizacionID INT
	,@LoteID INT
AS
SET NOCOUNT ON;

DECLARE @CHECKLISTCORRAL AS TABLE (
	LoteID INT
	,Corral CHAR(10)
	,CapacidadCorral INT
	,Lote VARCHAR(20)
	,PesoCorte int
	,CabezasSistema INT
	,FechaSacrificio DATETIME
	,FechaAbierto DATETIME
	,Fecha1Reimplante DATETIME
	,FechaCerrado DATETIME
	,Fecha2Reimplante DATETIME	
	,TipoGanado VARCHAR(50)
	)

INSERT INTO @CHECKLISTCORRAL (
	LoteID
	,Corral
	,CapacidadCorral
	,Lote
	,CabezasSistema
	,FechaAbierto
	,FechaCerrado	
	)
SELECT lo.LoteID
	,co.Codigo [Corral]
	,co.Capacidad
	,lo.Lote
	,lo.Cabezas [CabezasSistema]
	,lo.FechaInicio [FechaAbierto]
	,lo.FechaInicio + 7 [FechaCerrado] --La Fecha Fin debe ser en una semana de que se creo el lote	
FROM Lote lo
INNER JOIN Corral co ON lo.CorralID = co.CorralID
INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
WHERE lo.Activo = 1
	AND co.Activo = 1
	AND tc.TipoCorralID = 2 --Corrales de Producción
	AND lo.OrganizacionID = @OrganizacionID
	AND lo.LoteID = @LoteID

DECLARE @PesoCorte int

SET @PesoCorte = (
		SELECT SUM(Peso)
		FROM AnimalMovimiento(NOLOCK)
		WHERE LoteID = @LoteID
			AND OrganizacionID = @OrganizacionID
			AND Activo = 1
		GROUP BY LoteID
		)

UPDATE @CHECKLISTCORRAL
SET PesoCorte = @PesoCorte

SELECT LoteID
	,Corral
	,CapacidadCorral
	,Lote
	,PesoCorte
	,CabezasSistema
	,FechaSacrificio
	,FechaAbierto
	,Fecha1Reimplante
	,FechaCerrado
	,Fecha2Reimplante	
	,TipoGanado
FROM @CHECKLISTCORRAL
WHERE PesoCorte IS NOT NULL AND PesoCorte > 0

SET NOCOUNT OFF;

GO
