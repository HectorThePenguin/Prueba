USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalCabezasRecuperacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerTotalCabezasRecuperacion]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalCabezasRecuperacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: edgar.villarreal
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripción:	Obtiene el numero de cabezas que estan el corral de enfermeria
-- EXEC CorteGanado_ObtenerTotalCabezasRecuperacion 3156,13,1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerTotalCabezasRecuperacion]
@LoteID INT,
@TipoMovimiento INT,
@Activo INT


AS
BEGIN

	
	declare @Total INT,
	@TotalCortadas INT,
	@TotalPorCortar INT
	
	
	set @Total = (select Cabezas from Lote where LoteID = @LoteID)
	
	set @TotalCortadas = (select count(AnimalID) from AnimalMovimiento (NOLOCK)
	where LoteIDOrigen = @LoteID 
	and TipoMovimientoID = @TipoMovimiento 
	and Activo = @Activo
	and CAST(FechaMovimiento AS DATE) = CAST(GETDATE() AS DATE))
	
	set @TotalPorCortar = @Total + @TotalCortadas
	
	select 
	@Total AS Total,
	@TotalCortadas AS TotalCortadas,
	@TotalPorCortar AS TotalPorCortar
	
END


GO
