USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerDescripcionesPorIDs]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_ObtenerDescripcionesPorIDs]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerDescripcionesPorIDs]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: 25/04/2013
-- Description: Consulta para obtener el campo Descripcion de un grupo de tipos de ganado por ID
-- Empresa: SuKarne
-- =============================================
create procedure [dbo].[TipoGanado_ObtenerDescripcionesPorIDs]
	@tiposGanadoID varchar(max)
as
begin
	set NOCOUNT on
	declare @ids TABLE
	(
		id int
	)
	insert into @ids
		SELECT * from dbo.FuncionSplit(@tiposGanadoID, '|')
	select 
		t.TipoGanadoID
		, t.Descripcion
	from
		TipoGanado t
		inner join @ids i on t.TipoGanadoID = i.id
	set NOCOUNT OFF
end

GO
