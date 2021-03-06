USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerRelacionCorralSacrificioIntensivo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ObtenerRelacionCorralSacrificioIntensivo]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerRelacionCorralSacrificioIntensivo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ObtenerRelacionCorralSacrificioIntensivo]  
 @ordenSacrificioID int  
as  
begin  
 select  
  csac.Codigo as CorralSacrificio  
  , lsac.Lote as LoteSacrificio  
  , csac.CorralID as CorralIDSacrificio  
  , lsac.LoteID as LoteIDSacrificio  
  , csal.Codigo as CorralSalida  
  , lsal.Lote as LoteSalida  
  , csal.CorralID as CorralIDSalida   
  , lsal.LoteID as LoteIDSalida  
  , istd.InterfaceSalidaTraspasoDetalleID   
 -- , osd.CabezasSacrificio as Cabezas
 from  
  OrdenSacrificio os (nolock)  
  inner join OrdenSacrificioDetalle osd (nolock) on  
   os.OrdenSacrificioID = osd.OrdenSacrificioID  
  inner join Lote lsac (nolock) on  
   osd.LoteID = lsac.LoteID  
  inner join Corral csac (nolock) on  
   lsac.CorralID = csac.CorralID  
  inner join EntradaGanado eg (nolock) on  
   osd.LoteID = eg.LoteID  
  inner join InterfaceSalidaTraspaso ist (nolock) on  
   eg.FolioOrigen = ist.FolioTraspaso  
   and eg.OrganizacionID = ist.OrganizacionIDDestino  
  inner join InterfaceSalidaTraspasoDetalle istd (nolock) on  
   ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID  
  inner join Lote lsal (nolock) on  
   istd.LoteID = lsal.LoteID  
  inner join Corral csal (nolock) on  
   lsal.CorralID = csal.CorralID  
 where  
  os.OrdenSacrificioID = @ordenSacrificioID  
end


GO
