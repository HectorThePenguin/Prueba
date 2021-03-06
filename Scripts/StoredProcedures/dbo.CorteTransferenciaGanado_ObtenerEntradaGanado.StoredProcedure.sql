USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerEntradaGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerEntradaGanado]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerEntradaGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesus Alvarez
-- Create date: 17-02-2014
-- Description:	Obtiene una entrada de ganado por folio entrada
-- Exec CorteTransferenciaGanado_ObtenerEntradaGanado 11, 4
-- Modificado
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerEntradaGanado]
	@FolioEntrada INT, 
	@OrganizacionID INT
AS
BEGIN	
	SELECT TOP 1
		EG.EntradaGanadoID,
		EG.FolioEntrada,
		EG.OrganizacionID,
		EG.OrganizacionOrigenID,
		EG.FechaEntrada,
		EG.EmbarqueID,
		EG.FolioOrigen,
		EG.FechaSalida,
		EG.ChoferID,
		EG.JaulaID,
		EG.CabezasOrigen,
		EG.CabezasRecibidas,
		EG.OperadorID,
		EG.PesoBruto,
		EG.PesoTara,
		EG.EsRuteo,
		EG.Fleje,
		EG.CheckList,
		EG.CorralID,
		EG.LoteID,
		EG.Observacion,
		EG.ImpresionTicket,
		EG.Costeado,
		EG.Manejado,
		EG.Activo,
		EG.Guia,
		EG.Factura,
		EG.Poliza,
		EG.HojaEmbarque,
		EG.ManejoSinEstres,
		EG.CabezasMuertas,
		EG.Activo, 
		EG.FechaCreacion
	 FROM EntradaGanado EG
		WHERE EG.Activo = 1	
		AND EG.OrganizacionID = @OrganizacionID
		AND EG.FolioEntrada = @FolioEntrada
END

GO
