USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket]
@ProgramacionMateriaPrimaID INT
, @Ticket	INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT ProgMP.ProgramacionMateriaPrimaID
			,  ProgMP.PedidoDetalleID
			,  ProgMP.OrganizacionID
			,  ProgMP.AlmacenID
			,  ProgMP.InventarioLoteIDOrigen
			,  ProgMP.CantidadProgramada
			,  ProgMP.CantidadEntregada
			,  ProgMP.Observaciones
			,  ProgMP.Justificacion
			,  ProgMP.FechaProgramacion
			,  ProgMP.AlmacenMovimientoID
			,  ProgMP.Activo
		FROM ProgramacionMateriaPrima ProgMP
		INNER JOIN PesajeMateriaPrima PesajeMP
			ON (ProgMP.ProgramacionMateriaPrimaID = PesajeMP.ProgramacionMateriaPrimaID
				AND PesajeMP.Ticket = @Ticket
				AND PesajeMP.AlmacenMovimientoDestinoID > 0
				AND PesajeMP.AlmacenMovimientoOrigenID > 0)
		WHERE ProgMP.ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID
	SET NOCOUNT OFF;
END

GO
