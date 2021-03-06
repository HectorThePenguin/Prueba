USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lotes_ObtenerLotesDescripcionPorIDs]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lotes_ObtenerLotesDescripcionPorIDs]
GO
/****** Object:  StoredProcedure [dbo].[Lotes_ObtenerLotesDescripcionPorIDs]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: 25/04/2013
-- Description: Consulta para obtener el campo lote de un grupo de lotes por ID
-- Empresa: SuKarne
-- =============================================
create procedure [dbo].[Lotes_ObtenerLotesDescripcionPorIDs]
	@lotesID varchar(max)
as
begin
	set NOCOUNT on
	declare @ids TABLE
	(
		id int
	)
	insert into @ids
		SELECT * from dbo.FuncionSplit(@lotesID, '|')
	select 
		LoteID
		, Lote	
	from
		Lote l
		inner join @ids i on l.LoteID = i.id
	set NOCOUNT OFF
end

GO
