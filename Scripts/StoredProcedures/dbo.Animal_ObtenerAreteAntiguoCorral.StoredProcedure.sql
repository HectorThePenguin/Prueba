USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAreteAntiguoCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerAreteAntiguoCorral]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerAreteAntiguoCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 17/02/2016
-- Description:  Obtiene el Arete con fecha de llegada mas antigua en el corral
-- Animal_ObtenerAreteAntiguoCorral 140
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerAreteAntiguoCorral]
@CorralID int
AS

declare @LoteID int = (select top 1 LoteID from Lote where CorralID = @CorralID and Activo = 1)

SELECT TOP 1
a.AnimalID
,a.Arete
,a.AreteMetalico
,a.FechaCompra
,a.TipoGanadoID
,a.CalidadGanadoID
,a.ClasificacionGanadoID
,a.PesoCompra
,a.OrganizacionIDEntrada
,a.FolioEntrada
,a.PesoLlegada
,a.Paletas
,a.CausaRechadoID
,a.Venta
,a.Cronico
,a.CambioSexo
 FROM Animal a
inner join AnimalMovimiento am on a.AnimalID = am.AnimalID
inner join EntradaGanado eg on a.FolioEntrada = eg.FolioEntrada and a.OrganizacionIDEntrada = eg.OrganizacionID
where am.Activo = 1
and am.LoteID = @LoteID
and a.Activo =1 
order by eg.FechaEntrada
