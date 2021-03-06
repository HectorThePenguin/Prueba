USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerInformacionCheckList]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerInformacionCheckList]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerInformacionCheckList]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Lote_ObtenerInformacionCheckList 4,'20140128'
--======================================================
CREATE PROCEDURE [dbo].[Lote_ObtenerInformacionCheckList]
@OrganizacionID INT
,@FechaEjecucion DATE
AS
SELECT 
lo.LoteID
,co.Codigo [Corral]
,lo.Lote
,co.Capacidad [CapacidadCabezas]
,lo.Cabezas [CabezasActuales]
,(co.Capacidad - lo.Cabezas) [CabezasRestantes]
,lo.FechaInicio
,lo.FechaInicio + 7 [FechaFin] --La Fecha Fin debe ser en una semana de que se creo el lote
,lo.FechaCierre
FROM Lote lo
INNER JOIN Corral co ON lo.CorralID = co.CorralID
INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
WHERE lo.Activo = 1
and co.Activo = 1
and tc.TipoCorralID = 2 --Corrales de Producci�n
and lo.OrganizacionID = @OrganizacionID
AND ((CAST((lo.FechaInicio + 7) AS DATE) <= @FechaEjecucion) OR (co.Capacidad - lo.Cabezas = 0) )

GO
