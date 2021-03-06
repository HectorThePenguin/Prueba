USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteID]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/03/2015
-- Description: Consulta el traspaso de ganado .
-- SpName     : InterfaceSalidaTraspaso_ObtenerPorLoteID 1,11755
--======================================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerPorLoteID]
@OrganizacionID INT
,@LoteID INT
AS
BEGIN
	declare @FolioTraspaso BIGINT 
	declare @OrganizacionOrigenID INT
	set @FolioTraspaso = (select top 1 FolioOrigen from EntradaGanado eg where eg.OrganizacionID = @OrganizacionID and eg.LoteID = @LoteID)
	set @OrganizacionOrigenID = (select top 1 eg.OrganizacionOrigenID from EntradaGanado eg where eg.OrganizacionID = @OrganizacionID and eg.LoteID = @LoteID)
	SELECT IST.InterfaceSalidaTraspasoID
			,IST.OrganizacionID
			,IST.OrganizacionIDDestino
			,o.Descripcion AS Organizacion
			,IST.FolioTraspaso
			,IST.FechaEnvio
			,IST.CabezasEnvio
			,IST.SacrificioGanado
			,IST.TraspasoGanado
			,IST.PesoTara
			,IST.PesoBruto
			,IST.Activo
	FROM InterfaceSalidaTraspaso IST
	INNER JOIN Organizacion o on IST.OrganizacionID = o.OrganizacionID
	where ist.OrganizacionID = @OrganizacionOrigenID
	and IST.FolioTraspaso = @FolioTraspaso
		SELECT istd.InterfaceSalidaTraspasoDetalleID
			,istd.InterfaceSalidaTraspasoID
			,istd.LoteID
			,lo.Lote
			,co.CorralID
			,co.Codigo
			,istd.TipoGanadoID
			,tg.Descripcion AS TipoGanado
			,tg.PesoSalida
			,istd.PesoProyectado
			,istd.GananciaDiaria
			,istd.DiasEngorda
			,istd.FormulaID
			,fo.Descripcion AS Formula
			,istd.DiasFormula
			,istd.Cabezas
	FROM InterfaceSalidaTraspaso IST
	INNER JOIN InterfaceSalidaTraspasoDetalle istd on  ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID
	INNER JOIN Lote lo on istd.LoteID = lo.LoteID
	INNER JOIN Corral co on lo.CorralID = co.CorralID
	INNER JOIN TipoGanado tg on istd.TipoGanadoID = tg.TipoGanadoID
	INNER JOIN Formula fo on istd.FormulaID = fo.FormulaID	
	where ist.OrganizacionID = @OrganizacionOrigenID
	and IST.FolioTraspaso = @FolioTraspaso
END

GO
