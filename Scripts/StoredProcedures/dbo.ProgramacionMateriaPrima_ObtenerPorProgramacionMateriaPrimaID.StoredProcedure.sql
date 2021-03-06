USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 12/08/2014
-- Description: Obtiene el detalle del pedido ingresado
-- SpName     : EXEC ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaID 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaID]
@ProgramacionMateriaPrimaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		ProgramacionMateriaPrimaID, 
		PedidoDetalleID, 
		OrganizacionID, 
		AlmacenID, 
		InventarioLoteIDOrigen,
		CantidadProgramada, 
		CantidadEntregada, 
		Observaciones, 
		Justificacion, 
		FechaProgramacion, 
		Activo
	FROM ProgramacionMateriaPrima WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID AND Activo = 1
	SET NOCOUNT OFF;
END

GO
