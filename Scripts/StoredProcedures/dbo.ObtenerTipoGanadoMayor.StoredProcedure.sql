USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTipoGanadoMayor]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ObtenerTipoGanadoMayor]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTipoGanadoMayor]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 08/08/2015 12:00:00 a.m.
-- Description: 
-- SpName     : ObtenerTipoGanadoMayor 5
--======================================================
CREATE PROCEDURE [dbo].[ObtenerTipoGanadoMayor] @OrganizacionID INT
AS
create table #LOTES
(
	TipoGanadoID INT
			,TipoGanado VARCHAR(50)
			,LoteID INT	
			,Corral varchar(10)		
			,Sexo char(1)
)
create table #TipoGanadoLote
(
	LoteID int,
	TipoGanadoID int,
	CabezasTipo int
)
insert into #TipoGanadoLote
select 
am.LoteID
,a.TipoGanadoID
,count(a.AnimalID) as CabezasTipo
from AnimalMovimiento am 
inner join Animal a on am.AnimalID = a.AnimalID
where am.OrganizacionID = @OrganizacionID
and am.Activo = 1
and a.Activo = 1
group by am.LoteID,a.TipoGanadoID
insert into #LOTES
select 
tg.TipoGanadoID
,tg.Descripcion AS TipoGanado
,lo.LoteID
,co.Codigo AS Corral
,tg.Sexo
from Lote lo
inner join #TipoGanadoLote tgl on tgl.LoteID = lo.LoteID
inner join Corral co on lo.CorralID = co.CorralID
inner join TipoGanado tg on tg.TipoGanadoID = (select top 1 tgl.TipoGanadoID from #TipoGanadoLote tgl where tgl.LoteID = lo.LoteID order by tgl.CabezasTipo desc)
WHERE LO.OrganizacionID = @OrganizacionID
select DISTINCT * from #LOTES

GO
