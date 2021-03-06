USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerLoteAnteriorGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerLoteAnteriorGanado]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerLoteAnteriorGanado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 18/09/2014 12:00:00 a.m.
-- Description: Obtiene los dias de aplicacion de los tipos de formula de PRODUCCION,FINALIZACION,RETIRO
-- SpName     : Lote_ObtenerLoteAnteriorGanado 1522
--======================================================
CREATE PROCEDURE [dbo].[Lote_ObtenerLoteAnteriorGanado]
	@LoteID INT
AS
BEGIN
	SET NOCOUNT ON;

	declare @PrimerAnimal INT
	set @PrimerAnimal = ( 
	select top 1 
	AnimalID
	FROM AnimalMovimiento am (NOLOCK)
	where LoteID = @LoteID )
	
	select 
	top 1
	LoteID
	from AnimalMovimiento(NOLOCK)
	where AnimalID = @PrimerAnimal
	and LoteID <> @LoteID	
	order by FechaMovimiento desc
	

	SET NOCOUNT OFF;
END

GO
