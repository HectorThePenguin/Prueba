USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerPagosConTarjeta]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ObtenerPagosConTarjeta]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerPagosConTarjeta]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerPagosConTarjeta]
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @Fecha DATETIME, @FormaPagoCredito INT, @FormaPagoDebito INT
		SET @Fecha = GETDATE()
		SET @FormaPagoCredito = dbo.ADSUM_ParametroAdministrado_Numerico('CXC_Folios_de_Tarjetas_de_Credito')
		SET @FormaPagoDebito = dbo.ADSUM_ParametroAdministrado_Numerico('CXC_Folios_de_Tarjetas_de_Debito')

			SELECT CPM.cReferencia AS [Factura]
				 , CPC.dFecha_Registro AS [Fecha]
				 , CTC.cRazonSocial AS [Cliente]
				 , sum(CPF.nImporte) AS [Importe Factura]
				 , CFP.cDescripcion AS [Forma de Pago]
				 , sum(CPF.nImporte) AS [Importe Pago]
				 , CPF.cNumeroCuenta AS [Número de Cuenta]
				 , CPF.cReferencia AS [Referencia]
				 , CTB.cDescripcion AS [Banco]
				 , CTC.nCliente AS [Código Cliente]  
			FROM CAJ_PagosCaja CPC (NOLOCK)
			INNER JOIN CAJ_PagosForma CPF (NOLOCK)
				ON (CPF.nCanalDistribucion = CPC.nCanalDistribucion
					AND CPF.nPagoCaja = CPC.nPagoCaja)
			INNER JOIN CAJ_PagosMovimientos CPM(NOLOCK)
				ON (CPM.nCanalDistribucion = CPF.nCanalDistribucion
					AND CPM.nPagoCaja = CPF.nPagoCaja
					AND CPM.nRenglon = CPF.nRenglon)
			INNER JOIN CTL_Clientes CTC(NOLOCK)
				ON (CTC.nCliente = CPF.nCliente)
			INNER JOIN CAJ_FormasPago CFP(NOLOCK)
				ON (CFP.nFormaPago = CPF.nFormaPago)
			INNER JOIN CTL_Bancos CTB(NOLOCK)
				ON (CTB.nBanco = CPF.nBanco)
			WHERE CONVERT(VARCHAR,CPC.dFecha_Registro,112) = CONVERT(VARCHAR,@Fecha,112)
				AND (CPF.nFormaPago = @FormaPagoCredito OR CPF.nFormaPago = @FormaPagoDebito)
			 GROUP BY
					CPC.dFecha_Registro, CPM.cReferencia, CTC.cRazonSocial,  CFP.cDescripcion, CPF.cNumeroCuenta, CPF.cReferencia, CTB.cDescripcion,CTC.nCliente
			ORDER BY CPC.dFecha_Registro DESC
 

	SET NOCOUNT OFF

END

GO
