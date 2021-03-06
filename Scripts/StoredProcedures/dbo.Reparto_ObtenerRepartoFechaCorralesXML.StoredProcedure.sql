USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCorralesXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCorralesXML]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCorralesXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 26/08/2015
-- Description: SP para consultar los repartos de los corrales en la fecha
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCorralesXML]
@OrganizacionID INT,
@CorralesXML XML,
@Fecha DATE
AS
CREATE TABLE #Corrales
(
	CorralID int 
)
	INSERT INTO #Corrales 
	            (CorralID)
	SELECT 
		CorralID  = T.item.value('./CorralID[1]', 'INT')		
	FROM  @CorralesXML.nodes('ROOT/Corrales') AS T(item)	
BEGIN
SET NOCOUNT ON;
	SELECT re.RepartoID,
		   re.OrganizacionID,
           re.CorralID,
           re.LoteID,
           re.Fecha,
		   re.PesoInicio,
		   re.PesoProyectado,
		   re.DiasEngorda,
		   re.PesoRepeso
	FROM Reparto re
	inner join #Corrales co on re.CorralID = co.CorralID
	WHERE OrganizacionID = @OrganizacionID
	AND CAST(Fecha AS DATE) = @Fecha	
	AND Activo= 1
	select 
	rd.RepartoDetalleID
	,rd.RepartoID
	,rd.TipoServicioID
	,rd.FormulaIDProgramada
	,rd.FormulaIDServida
	,rd.CantidadProgramada
	,rd.CantidadServida
	,rd.HoraReparto
	,rd.CostoPromedio
	,rd.Importe
	,rd.Servido
	,rd.Cabezas
	,rd.EstadoComederoID
	,rd.CamionRepartoID
	,rd.Observaciones
	from RepartoDetalle rd
	inner join Reparto re on rd.RepartoID = re.RepartoID
	inner join #Corrales co on re.CorralID = co.CorralID
	WHERE OrganizacionID = @OrganizacionID
	AND CAST(Fecha AS DATE) = @Fecha
	and rd.TipoServicioID in (1,2)	
	SET NOCOUNT OFF;
END

GO
