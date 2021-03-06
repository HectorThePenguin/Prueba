USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClienteCreditoExcel_ObtenerSaldos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClienteCreditoExcel_ObtenerSaldos]
GO
/****** Object:  StoredProcedure [dbo].[ClienteCreditoExcel_ObtenerSaldos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Sergio A. Gamez Gomez
-- Fecha: 25-05-2015
-- Descripci�n:	Obtener saldos de la SOFOM de un cliente
-- ClienteCreditoExcel_ObtenerSaldos 868
-- =============================================
CREATE PROCEDURE [dbo].[ClienteCreditoExcel_ObtenerSaldos] @Cliente INT
AS
BEGIN
	SELECT 
		Prestado = ISNULL(SUM(Prestado),0), 
		SaldoVencido_Total = ISNULL(SUM(SaldoVencido + InteresVencido + InteresMoratorio),0), 
		Saldo = ISNULL(SUM(SaldoBanco+SaldoFondo+SaldoVencido+Interes+ InteresVencido+ InteresMoratorio),0)
	FROM ClienteCreditoExcel (NOLOCK) WHERE ClienteID = @Cliente
END

GO
