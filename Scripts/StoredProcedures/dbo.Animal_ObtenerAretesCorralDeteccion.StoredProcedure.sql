USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAretesCorralDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerAretesCorralDeteccion]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAretesCorralDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 04/02/2016
-- Description:  Obtiene el listado de aretes
-- Animal_ObtenerAretesCorralDeteccion 14,0
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerAretesCorralDeteccion]
@CorralID int,
@EsPartida bit
AS

declare @LoteID int = (select top 1 LoteID from Lote where CorralID = @CorralID and Activo = 1)

if @EsPartida = 1
begin

	
	SELECT 
	isa.Arete
	,isa.AreteMetalico
	FROM InterfaceSalidaAnimal isa
	inner join EntradaGanado eg on isa.SalidaID = eg.FolioOrigen and isa.OrganizacionID = eg.OrganizacionOrigenID
	where eg.LoteID = @LoteID
	and eg.Activo = 1
	and (isa.AnimalID is null or isa.AnimalID = 0)
end
else
SELECT 
a.Arete
,a.AreteMetalico
 FROM Animal a
inner join AnimalMovimiento am on a.AnimalID = am.AnimalID
where am.Activo = 1
and am.LoteID = @LoteID
and a.Activo =1 
