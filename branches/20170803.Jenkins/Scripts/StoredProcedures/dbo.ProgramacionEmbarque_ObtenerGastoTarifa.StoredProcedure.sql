USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerGastoTarifa]    Script Date: 27/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerGastoTarifa]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerGastoTarifa]    Script Date: 27/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro
-- Create date: 28-06-2017
-- Description: sp para obtener el gasto fijo de acuerdo a la ruta
-- SpName     : ProgramacionEmbarque_ObtenerGastoTarifa 36, 1
--======================================================  
CREATE PROCEDURE ProgramacionEmbarque_ObtenerGastoTarifa
@ConfiguracionEmbarqueDetalleID INT,
@ProveedorID INT = 0,
@Activo BIT
AS
BEGIN	
	SELECT SUM (gafi.Importe) AS Importe
	FROM EmbarqueTarifa embTar (NOLOCK)
	INNER JOIN EmbarqueGastoTarifa egt (NOLOCK) ON (egt.EmbarqueTarifaID = embTar.EmbarqueTarifaID)
	INNER JOIN GastosFijos gafi (NOLOCK) ON (egt.GastoFijoID = gafi.GastoFijoID)
	WHERE embTar.ConfiguracionEmbarqueDetalleID = @ConfiguracionEmbarqueDetalleID
	AND embTar.ProveedorID = @ProveedorID
	AND embTar.Activo = @Activo
	AND egt.Activo = @Activo
	AND gafi.Activo = @Activo
END