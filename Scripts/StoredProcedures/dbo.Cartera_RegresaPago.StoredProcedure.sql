USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cartera_RegresaPago]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cartera_RegresaPago]
GO
/****** Object:  StoredProcedure [dbo].[Cartera_RegresaPago]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Cartera_RegresaPago]
 @CanalDistribucion INT,    
 @Pago INT,
 @Cancelacion INT = 0  
AS              
BEGIN       
	SET NOCOUNT ON;

	-- [Autor]:[Sergio Gámez][21-07-2014]
	-- [Aplicación]:[NSC]
	-- [Motivo]:[Generar polizas de pagos]		

	DECLARE @TipoCanalCorporativo TINYINT  
	DECLARE @TipoCanal INT  
	DECLARE @CanalOperacion INT
	DECLARE @CXC_TM_PAGO INT
	DECLARE @CXC_TM_FACTURA INT
	DECLARE @CXC_TM_ANTICIPO INT
	DECLARE @CXC_FP_EFECTIVODLLS INT
	DECLARE @CLIENTE_CONTADO INT
	DECLARE @Cambio NUMERIC(18,2)
	DECLARE @SePagaCon NUMERIC(18,2)
	DECLARE @ToTalAPagar NUMERIC(18,2)
	DECLARE @ToTalAnticipos NUMERIC(18,2)

	SELECT @SePagaCon = ISNULL(SUM(nImporte),0) FROM CAJ_PagosForma (NOLOCK) WHERE  nPagoCaja = @Pago AND nCanalDistribucion = @CanalDistribucion AND nFormaPago = 5
	SELECT @ToTalAPagar = ISNULL(SUM(nImporte),0) FROM CAJ_PagosForma (NOLOCK) WHERE  nPagoCaja = @Pago AND nCanalDistribucion = @CanalDistribucion AND Abono_nAbono IS NOT NULL AND nFormaPago = 5
	SELECT @CXC_TM_FACTURA = cValor FROM ADSUM_ParametrosAdministrados WHERE  cContexto = 'CXC' AND cParametroAdministrado = 'CXC_TM_FACTURA'

	SET @TipoCanalCorporativo = dbo.ADSUM_ParametroAdministrado_Caracter('CTL', 'TIPOCANAL_CORPORATIVO')  
	SET @CanalOperacion = dbo.ADSUM_ParametroAdministrado_Caracter('PRMSUC', 'CANAL_DISTRIBUCION')  
	SELECT @TipoCanal = nTipoCanal FROM Ctl_CanalesDistribucion(NOLOCK) WHERE nCanalDistribucion = @CanalOperacion
	SELECT @CXC_TM_PAGO = cValor FROM ADSUM_ParametrosAdministrados (NOLOCK) WHERE cParametroAdministrado = 'CXC_TM_PAGO' AND cContexto = 'CXC'
	SELECT @CXC_TM_ANTICIPO = cValor FROM ADSUM_ParametrosAdministrados (NOLOCK) WHERE cParametroAdministrado = 'CXC_TM_ANTICIPO' AND cContexto = 'CXC'
	SELECT @CXC_FP_EFECTIVODLLS = cValor FROM ADSUM_ParametrosAdministrados (NOLOCK) WHERE cParametroAdministrado = 'FORMA_PAGO_PAGOS_EFECTIVO' AND cContexto = 'CAJ'
			
	CREATE TABLE #CON_POLIZAS_SAP
	(
		cMonedaContraria varchar(3),	  
		Cambio numeric(18,2),
		FacturaCambio varchar(50),
		Abono INT,
		nFormaPago tinyint,
		nMoneda tinyint,
		cCuenta_SAP varchar(10),
		bFija bit,
		cReferencia varchar(20),
		cDescripcionFormaPago  varchar(50),
		nReferencia INT,
		cCuenta_ValesCaja_SAP  varchar(10),
		cCuenta_Cambio_Sap  varchar(10),
		cCuentaEspecificaTombola_MBase  varchar(40),
		cCuentaEspecificaTombola_MSec  varchar(40),
		OIng_Sap  varchar(10),
		CanalOperacion Int,
		FechaFormato Varchar(8),
		Serie varchar(8),
		Factura Int,
		FichaDeposito  varchar(50),
		cCuenta_SAPCR varchar(10),
		cReferenciaBanco varchar(20),
		cCuenta	varchar(10),	     	     
		cProveedor	varchar(10),     	     
		cCliente	varchar(10),	     	     
		nImporte	numeric(16,2),    
		cTexto_Asignado	varchar(18),
		cConcepto	varchar(50),
		cRef2	varchar(12),	     	     
		cRef3	varchar(20),	     	     
		cCentro_Beneficio	varchar(10),
		cCargo_Abono	varchar(1),
		cTipo_Movimiento	varchar(3),	     	     
		cTipo_Poliza	varchar(2),
		cIndica_CME	varchar(1),   	     
		cRef1	varchar(12),	     	     
		cDivision	varchar(4),
		dFecha_Documento	smalldatetime,
		dFecha_Contabilidad	smalldatetime,
		cClase_Documento	varchar(2),
		cSociedad	varchar(4),
		cMoneda	varchar(3),	     
		nTipoCambio	numeric(16,4),    
		cTexto_Documento	varchar(25),	     	     
		cMes	varchar(2),     
		cBus_Act	varchar(4),	     	     
		cPeriodo	varchar(4),	     	     
		cFolio_Movimiento	varchar(10),
		nCanalPadre	int,
		nAnticipo int,
		Anticipo_nCanalDistribucion smallint,
		Anticipo_nAnticipo int
	)

	SELECT @CLIENTE_CONTADO = cValor FROM ADSUM_ParametrosAdministrados (NOLOCK) WHERE cParametroAdministrado = 'CLIENTE_CONTADO'

	IF (@TipoCanal = @TipoCanalCorporativo)  
	BEGIN
		SET @ToTalAnticipos = ISNULL((SELECT SUM(ISNULL(ANT.nImporte,0))
									   FROM CAJ_PagosForma PF (NOLOCK)
									   LEFT OUTER JOIN CXC_Anticipos ANT (NOLOCK)
							   				ON ANT.nCanalDistribucion = PF.Anticipo_nCanalDistribucion AND ANT.nAnticipo = PF.Anticipo_nAnticipo
									   INNER JOIN CTL_ConfiguracionCanalesDistribucion CCD (NOLOCK)
											ON CCD.nCanalDistribucion = PF.nCanalDistribucion
									   INNER JOIN CAJ_FormasPago FP (NOLOCK)
											ON FP.nFormaPago = PF.nFormaPago
									   WHERE  PF.nPagoCaja = @Pago AND PF.nCanalDistribucion = @CanalDistribucion AND FP.nMoneda = CCD.nMonedaSecundaria),0)
									   
		SET @Cambio = @SePagaCon - (@ToTalAPagar+@ToTalAnticipos)  
		INSERT INTO #CON_POLIZAS_SAP
		SELECT
			cMonedaContraria = CASE WHEN UPPER(ISNULL(M2.cAbreviacion,M.cAbreviacion)) = 'USD' THEN 'MXN' ELSE 'USD' END,
			Cambio = @Cambio,
			FacturaCambio = ISNULL(PM.cReferencia,''),
			Abono = ISNULL(A.nAbono,0),
			nFormaPago = PF.nFormaPago,
			nMoneda = ISNULL(M2.nMoneda,M.nMoneda),
			--cCuenta_SAP = ISNULL(NC.cCuenta_SAP,' '),
			cCuenta_SAP = ISNULL(CR.cCuenta_SAP,''),
			bFija = ISNULL(NC.bFija,0),
			cReferencia = ISNULL(CR.cReferencia,' '),
			cDescripcionFormaPago = ISNULL(FP.cDescripcion,' '),
			nReferencia = PF.nReferencia,
			cCuenta_ValesCaja_SAP = ISNULL(cCuenta_ValesCaja_Sap,' '),
			cCuenta_Cambio_Sap = ISNULL(cCuenta_Cambio_Sap,' '),
			cCuentaEspecificaTombola_MBase = ISNULL(CD.cCuentaEspecificaTombola_MBase,' '),
			cCuentaEspecificaTombola_MSec = ISNULL(CD.cCuentaEspecificaTombola_MSec,' '),
			OIng_Sap = ISNULL(CCD.OIng_Sap,' '),
			CanalOperacion = ISNULL(@CanalOperacion,' '),
			FechaFormato = RIGHT('00'+LTRIM(DAY(GETDATE())),2) + RIGHT('00'+LTRIM(MONTH(GETDATE())),2) + SUBSTRING(RIGHT('0000'+LTRIM(YEAR(GETDATE())),4),3,2),
			Serie = ISNULL(C.cSerieFactura,ISNULL(C2.cSerieFactura,' ')),
			Factura = ISNULL(C.nFolioFactura,ISNULL(C2.nFolioFactura,' ')),
			FichaDeposito = ' ',
			cCuenta_SAPCR = ISNULL(CR.cCuenta_SAP,' '),
			cReferenciaBanco = ISNULL(CR.cReferencia,' '),
		--- FIN CAMPOS CLAVE ****************

		--- INI CAMPOS VARIABLES ****************
			cCuenta = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN PF.nConcepto_Anticipo ELSE ' ' END,
			cProveedor = CASE WHEN PF.cRobo = 'R' THEN ISNULL(E.nCodigo_Proveedor_Sap, '0') ELSE '0' END,
			--cCliente = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN ISNULL(ANT.nCliente,PF.nCliente) ELSE ISNULL(A.nCliente,PF.nCliente) END,
			cCliente = CASE WHEN ISNULL(F2.bFacturaNoFiscal,0) = 1 THEN @CLIENTE_CONTADO ELSE CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN ISNULL(ANT.nCliente,PF.nCliente) ELSE ISNULL(A.nCliente,PF.nCliente) END END,
			nImporte = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN ISNULL(ANT.nImporte,PF.nImporte) ELSE ISNULL(A.nImporte,PF.nImporte) END,
			cTexto_Asignado = ' ',
			cConcepto = ' ',
			
			cRef2 = ' ',
			cRef3 = ' ',
			cCentro_Beneficio = ' ',
			cCargo_Abono = ' ',
		--- FIN CAMPOS VARIABLES ****************

		--- INI FIJOS ****************
			cTipo_Movimiento = @CXC_TM_PAGO,
			cTipo_Poliza = RIGHT('00'+LTRIM(TM.nTipoPoliza),2),
			cIndica_CME = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN 'A' ELSE CASE WHEN PF.cRobo = 'R' THEN 'Q' ELSE ' ' END END,				
			cRef1 = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN CAST(ISNULL(CA.cCve_SAP,'EXC-PAGO') AS VARCHAR(16)) ELSE CASE WHEN PF.cRobo = 'R' THEN 'ROBOS' ELSE ' ' END END,

			cDivision = D.CodDivision,
			dFecha_Documento = PC.dFecha_Registro,
			dFecha_Contabilidad = PC.dFecha_Registro,
			cClase_Documento = CCD.Cd_CC,
			cSociedad = CCD.cCodigo_Sociedad,
			cMoneda = UPPER(ISNULL(M2.cAbreviacion,M.cAbreviacion)), 
			nTipoCambio = CASE WHEN ISNULL(A.nTipoCambio,ISNULL(PF.nTipoCambio,0)) <= 1 THEN 0 ELSE ISNULL(A.nTipoCambio,ISNULL(PF.nTipoCambio,0)) END,
			cTexto_Documento = 'PAGOS',
			cMes = RIGHT('00'+LTRIM(MONTH(PC.dFecha_Registro)),2),
			cBus_Act = 'RFBU',
			cPeriodo = RIGHT('0000'+LTRIM(YEAR(PC.dFecha_Registro)),4),
			cFolio_Movimiento = PC.nPagoCaja,
			nCanalPadre = CD.nCanalPadre,
			nAnticipo = ISNULL(ANT.nAnticipo,' '),
			PF.Anticipo_nCanalDistribucion,
			PF.Anticipo_nAnticipo
		FROM CAJ_PagosCaja PC (NOLOCK)
		INNER JOIN CAJ_PagosMovimientos PM (NOLOCK)
			ON PM.nCanalDistribucion = PC.nCanalDistribucion and PM.nPagoCaja = PC.nPagoCaja
		INNER JOIN CAJ_PagosForma PF (NOLOCK)
			ON PF.nCanalDistribucion = PC.nCanalDistribucion and PF.nPagoCaja = PC.nPagoCaja AND PM.nRenglon = PF.nPagoMovimientoRenglon --And PF.nConsecutivoCierre = 0     
		INNER JOIN CAJ_FormasPago FP (NOLOCK)
			ON FP.nFormaPago = PF.nFormaPago
		INNER JOIN CTL_ConfiguracionCanalesDistribucion CCD (NOLOCK)
			ON CCD.nCanalDistribucion = PC.nCanalDistribucion
		INNER JOIN CTL_CanalesDistribucion CD (NOLOCK)
			ON CD.nCanalDistribucion = PC.nCanalDistribucion
		INNER JOIN CTL_Monedas M (NOLOCK)
			ON M.nMoneda = FP.nMoneda
		INNER JOIN SAPDivision D (NOLOCK) 
			ON D.IdDivision = CCD.cCodigo_Division
		LEFT OUTER JOIN CXC_ABONOS A (NOLOCK)
			ON A.nCanalDistribucion = PF.Abono_nCanalDistribucion and A.nAbono = PF.Abono_nAbono
		LEFT OUTER JOIN CXC_Cargos C (NOLOCK)
			ON C.nCanalDistribucion = A.Cargo_nCanalDistribucion and C.nCargo = A.Cargo_nCargo
		LEFT OUTER JOIN CXC_Cargos C2 (NOLOCK)
			ON C2.FacturaTicket_nCanalDistribucion = PM.FacturaTicket_nCanalDistribucion and C2.FacturaTicket_nFacturaTicket = PM.FacturaTicket_nFacturaTicket AND C2.nTipoMovimiento = @CXC_TM_FACTURA
		LEFT OUTER JOIN CTL_CuentasReceptoras CR (NOLOCK)
			ON CR.nCuentaReceptora = ISNULL(A.nBancoReceptor, PF.nBancoReceptor)
		LEFT OUTER JOIN CTL_Monedas M2 (NOLOCK)
			ON M2.nMoneda = A.nMoneda 
		LEFT OUTER JOIN CTL_CanalConfiguracionNivelCuenta NC (NOLOCK)
			ON NC.nFormaPago = PF.nFormaPago 
		INNER JOIN CXC_TiposMovimiento TM (NOLOCK)
			ON TM.nTipoMovimiento = @CXC_TM_PAGO
		LEFT OUTER JOIN Ctl_Empleados E (NOLOCK)
			ON E.nIdEmpleado = PF.nEmpleado
		LEFT OUTER JOIN CXC_Anticipos ANT (NOLOCK)
			ON ANT.nCanalDistribucion = PF.Anticipo_nCanalDistribucion AND ANT.nAnticipo = PF.Anticipo_nAnticipo
		LEFT OUTER JOIN VTA_FacturasTickets F (NOLOCK)
			ON F.nCanalDistribucion = PM.FacturaTicket_nCanalDistribucion AND F.nFacturaTicket = PM.FacturaTicket_nFacturaTicket
		LEFT OUTER JOIN VTA_FacturasTickets F2 (NOLOCK)
			ON F2.nCanalDistribucion = C.FacturaTicket_nCanalDistribucion AND F2.nFacturaTicket = C.FacturaTicket_nFacturaTicket
		LEFT OUTER JOIN VTA_TiposFactura TF (NOLOCK)
			ON TF.nTipoFactura = F.nTipoFactura
		LEFT OUTER JOIN CTL_ConceptosAnticipos CA (NOLOCK)
			ON CA.nConceptoAnticipo = PF.nConcepto_Anticipo
		WHERE PC.nCanalDistribucion = @CanalDistribucion AND PC.nPagoCaja = @Pago AND (ISNULL(TF.bEsFactura,1) = 1 OR (ISNULL(TF.bEsFactura,1) = 0 AND PF.nFormaPago = @CXC_FP_EFECTIVODLLS))

		UPDATE Cxc_Abonos SET nPoliza = @Pago WHERE nCanalDistribucion = @CanalDistribucion AND nAbono IN (SELECT Abono FROM #CON_POLIZAS_SAP)
		UPDATE CXC_Anticipos SET nPoliza = @Pago WHERE nCanalDistribucion = @CanalDistribucion AND nAnticipo IN(SELECT nAnticipo FROM #CON_POLIZAS_SAP)
	END  		 
	ELSE
	BEGIN
		SET @ToTalAnticipos = ISNULL((SELECT SUM(ISNULL(ANT.nImporte,0))
							   FROM CAJ_PagosForma PF (NOLOCK)
							   LEFT OUTER JOIN CXC_Anticipos_LOCAL ANT (NOLOCK)
							   		ON ANT.nCanalDistribucion = PF.Anticipo_nCanalDistribucion AND ANT.nAnticipo = PF.Anticipo_nAnticipo
							   INNER JOIN CTL_ConfiguracionCanalesDistribucion CCD (NOLOCK)
									ON CCD.nCanalDistribucion = PF.nCanalDistribucion
							   INNER JOIN CAJ_FormasPago FP (NOLOCK)
									ON FP.nFormaPago = PF.nFormaPago
							   WHERE  PF.nPagoCaja = @Pago AND PF.nCanalDistribucion = @CanalDistribucion AND FP.nMoneda = CCD.nMonedaSecundaria),0)
		SET @Cambio = @SePagaCon - (@ToTalAPagar+@ToTalAnticipos)
		INSERT INTO #CON_POLIZAS_SAP
		SELECT
			cMonedaContraria = CASE WHEN UPPER(ISNULL(M2.cAbreviacion,M.cAbreviacion)) = 'USD' THEN 'MXN' ELSE 'USD' END,
			Cambio = @Cambio,
			FacturaCambio = ISNULL(PM.cReferencia,''),
			Abono = ISNULL(A.nAbono,0),
			nFormaPago = PF.nFormaPago,
			nMoneda = ISNULL(M2.nMoneda,M.nMoneda),
			cCuenta_SAP = ISNULL(CR.cCuenta_SAP,''),
			bFija = ISNULL(NC.bFija,0),
			cReferencia = ISNULL(CR.cReferencia,' '),
			cDescripcionFormaPago = ISNULL(FP.cDescripcion,' '),
			nReferencia = PF.nReferencia,
			cCuenta_ValesCaja_SAP = ISNULL(cCuenta_ValesCaja_Sap,' '),
			cCuenta_Cambio_Sap = ISNULL(cCuenta_Cambio_Sap,' '),
			cCuentaEspecificaTombola_MBase = ISNULL(CD.cCuentaEspecificaTombola_MBase,' '),
			cCuentaEspecificaTombola_MSec = ISNULL(CD.cCuentaEspecificaTombola_MSec,' '),
			OIng_Sap = ISNULL(CCD.OIng_Sap,' '),
			CanalOperacion = ISNULL(@CanalOperacion,' '),
			FechaFormato = RIGHT('00'+LTRIM(DAY(GETDATE())),2) + RIGHT('00'+LTRIM(MONTH(GETDATE())),2) + SUBSTRING(RIGHT('0000'+LTRIM(YEAR(GETDATE())),4),3,2),
			Serie = ISNULL(C.cSerieFactura,ISNULL(C2.cSerieFactura,' ')),
			Factura = ISNULL(C.nFolioFactura,ISNULL(C2.nFolioFactura,' ')),
			FichaDeposito = ' ',
			cCuenta_SAPCR = ISNULL(CR.cCuenta_SAP,' '),
			cReferenciaBanco = ISNULL(CR.cReferencia,' '),
		--- FIN CAMPOS CLAVE ****************

		--- INI CAMPOS VARIABLES ****************
			cCuenta = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN PF.nConcepto_Anticipo ELSE ' ' END,
			cProveedor = CASE WHEN PF.cRobo = 'R' THEN ISNULL(E.nCodigo_Proveedor_Sap, '0') ELSE '0' END,
			--cCliente = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN ISNULL(ANT.nCliente,PF.nCliente) ELSE ISNULL(A.nCliente,PF.nCliente) END,
			cCliente = CASE WHEN ISNULL(F2.bFacturaNoFiscal,0) = 1 THEN @CLIENTE_CONTADO ELSE CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN ISNULL(ANT.nCliente,PF.nCliente) ELSE ISNULL(A.nCliente,PF.nCliente) END END,
			nImporte = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN ISNULL(ANT.nImporte,PF.nImporte) ELSE ISNULL(A.nImporte,PF.nImporte) END,
			cTexto_Asignado = ' ',
			cConcepto = ' ',
			
			cRef2 = ' ',
			cRef3 = ' ',
			cCentro_Beneficio = ' ',
			cCargo_Abono = ' ',
		--- FIN CAMPOS VARIABLES ****************

		--- INI FIJOS ****************
			cTipo_Movimiento = @CXC_TM_PAGO,
			cTipo_Poliza = RIGHT('00'+LTRIM(TM.nTipoPoliza),2),
			cIndica_CME = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN 'A' ELSE CASE WHEN PF.cRobo = 'R' THEN 'Q' ELSE ' ' END END,		
			cRef1 = CASE WHEN PM.nTipoMovimiento = @CXC_TM_ANTICIPO THEN CAST(ISNULL(CA.cCve_SAP,'EXC-PAGO') AS VARCHAR(16)) ELSE CASE WHEN PF.cRobo = 'R' THEN 'ROBOS' ELSE ' ' END END,

			cDivision = D.CodDivision,
			dFecha_Documento = PC.dFecha_Registro,
			dFecha_Contabilidad = PC.dFecha_Registro,
			cClase_Documento = CCD.Cd_CC,
			cSociedad = CCD.cCodigo_Sociedad,
			cMoneda = UPPER(ISNULL(M2.cAbreviacion,M.cAbreviacion)), 
			nTipoCambio = CASE WHEN ISNULL(A.nTipoCambio,ISNULL(PF.nTipoCambio,0)) <= 1 THEN 0 ELSE ISNULL(A.nTipoCambio,ISNULL(PF.nTipoCambio,0)) END,
			cTexto_Documento = 'PAGOS',
			cMes = RIGHT('00'+LTRIM(MONTH(PC.dFecha_Registro)),2),
			cBus_Act = 'RFBU',
			cPeriodo = RIGHT('0000'+LTRIM(YEAR(PC.dFecha_Registro)),4),
			cFolio_Movimiento = PC.nPagoCaja,
			nCanalPadre = CD.nCanalPadre,
			nAnticipo = ISNULL(ANT.nAnticipo,' '),
			PF.Anticipo_nCanalDistribucion,
			PF.Anticipo_nAnticipo
		FROM CAJ_PagosCaja PC (NOLOCK)
		INNER JOIN CAJ_PagosMovimientos PM (NOLOCK)
			ON PM.nCanalDistribucion = PC.nCanalDistribucion and PM.nPagoCaja = PC.nPagoCaja
		INNER JOIN CAJ_PagosForma PF (NOLOCK)
			ON PF.nCanalDistribucion = PC.nCanalDistribucion and PF.nPagoCaja = PC.nPagoCaja AND PM.nRenglon = PF.nPagoMovimientoRenglon --And PF.nConsecutivoCierre = 0     
		INNER JOIN CAJ_FormasPago FP (NOLOCK)
			ON FP.nFormaPago = PF.nFormaPago
		INNER JOIN CTL_ConfiguracionCanalesDistribucion CCD (NOLOCK)
			ON CCD.nCanalDistribucion = PC.nCanalDistribucion
		INNER JOIN CTL_CanalesDistribucion CD (NOLOCK)
			ON CD.nCanalDistribucion = PC.nCanalDistribucion
		INNER JOIN CTL_Monedas M (NOLOCK)
			ON M.nMoneda = FP.nMoneda
		INNER JOIN SAPDivision D (NOLOCK) 
			ON D.IdDivision = CCD.cCodigo_Division
		LEFT OUTER JOIN CXC_ABONOS_LOCAL A (NOLOCK)
			ON A.nCanalDistribucion = PF.Abono_nCanalDistribucion and A.nAbono = PF.Abono_nAbono
		LEFT OUTER JOIN CXC_Cargos_LOCAL C (NOLOCK)
			ON C.nCanalDistribucion = A.Cargo_nCanalDistribucion and C.nCargo = A.Cargo_nCargo
		LEFT OUTER JOIN CXC_Cargos_LOCAL C2 (NOLOCK)
			ON C2.FacturaTicket_nCanalDistribucion = PM.FacturaTicket_nCanalDistribucion and C2.FacturaTicket_nFacturaTicket = PM.FacturaTicket_nFacturaTicket AND C2.nTipoMovimiento = @CXC_TM_FACTURA
		LEFT OUTER JOIN CTL_CuentasReceptoras CR (NOLOCK)
			ON CR.nCuentaReceptora = ISNULL(A.nBancoReceptor, PF.nBancoReceptor)
		LEFT OUTER JOIN CTL_Monedas M2 (NOLOCK)
			ON M2.nMoneda = A.nMoneda 
		LEFT OUTER JOIN CTL_CanalConfiguracionNivelCuenta NC (NOLOCK)
			ON NC.nFormaPago = PF.nFormaPago 
		INNER JOIN CXC_TiposMovimiento TM (NOLOCK)
			ON TM.nTipoMovimiento = @CXC_TM_PAGO
		LEFT OUTER JOIN Ctl_Empleados E (NOLOCK)
			ON E.nIdEmpleado = PF.nEmpleado
		LEFT OUTER JOIN CXC_Anticipos_LOCAL ANT (NOLOCK)
			ON ANT.nCanalDistribucion = PF.Anticipo_nCanalDistribucion AND ANT.nAnticipo = PF.Anticipo_nAnticipo
		LEFT OUTER JOIN VTA_FacturasTickets F (NOLOCK)
			ON F.nCanalDistribucion = PM.FacturaTicket_nCanalDistribucion AND F.nFacturaTicket = PM.FacturaTicket_nFacturaTicket
		LEFT OUTER JOIN VTA_FacturasTickets F2 (NOLOCK)
			ON F2.nCanalDistribucion = C.FacturaTicket_nCanalDistribucion AND F2.nFacturaTicket = C.FacturaTicket_nFacturaTicket
		LEFT OUTER JOIN VTA_TiposFactura TF (NOLOCK)
			ON TF.nTipoFactura = F.nTipoFactura
		LEFT OUTER JOIN CTL_ConceptosAnticipos CA (NOLOCK)
			ON CA.nConceptoAnticipo = PF.nConcepto_Anticipo
		WHERE PC.nCanalDistribucion = @CanalDistribucion AND PC.nPagoCaja = @Pago AND (ISNULL(TF.bEsFactura,1) = 1 OR (ISNULL(TF.bEsFactura,1) = 0 AND PF.nFormaPago = @CXC_FP_EFECTIVODLLS))

		UPDATE Cxc_Abonos_Local SET nPoliza = @Pago WHERE nCanalDistribucion = @CanalDistribucion AND nAbono IN (SELECT Abono FROM #CON_POLIZAS_SAP)
		UPDATE CXC_Anticipos_Local SET nPoliza = @Pago WHERE nCanalDistribucion = @CanalDistribucion AND nAnticipo IN(SELECT nAnticipo FROM #CON_POLIZAS_SAP)
	END

	IF @Cancelacion = 0
	BEGIN
		DELETE FROM #CON_POLIZAS_SAP WHERE Abono = 0 AND RTRIM(LTRIM(cIndica_CME)) = '' AND nFormaPago <> 6
		UPDATE #CON_POLIZAS_SAP SET nIMporte = nIMporte * -1 WHERE Abono = 0 AND RTRIM(LTRIM(cIndica_CME)) = '' AND nFormaPago = 6
	END


	DELETE FROM #CON_POLIZAS_SAP WHERE RTRIM(LTRIM(cIndica_CME)) = 'A' AND Anticipo_nCanalDistribucion IS NULL AND Anticipo_nAnticipo IS NULL

	SELECT
		cMonedaContraria,	
		Cambio,
		FacturaCambio,
		Abono,
		nFormaPago,
		nMoneda,
		cCuenta_SAP,
		bFija,
		cReferencia,
		cDescripcionFormaPago,
		nReferencia,
		cCuenta_ValesCaja_SAP,
		cCuenta_Cambio_Sap,
		cCuentaEspecificaTombola_MBase,
		cCuentaEspecificaTombola_MSec,
		OIng_Sap,
		CanalOperacion,
		FechaFormato,
		Serie,
		Factura,
		FichaDeposito,
		cCuenta_SAPCR,
		cReferenciaBanco,
		cCuenta,	     	     
		cProveedor,     	     
		cCliente,	     	     
		nImporte,    
		cTexto_Asignado,
		cConcepto,
		cRef2,	     	     
		cRef3,	     	     
		cCentro_Beneficio,
		cCargo_Abono,
		cTipo_Movimiento,	     	     
		cTipo_Poliza,
		cIndica_CME,   	     
		cRef1,	     	     
		cDivision,
		dFecha_Documento,
		dFecha_Contabilidad,
		cClase_Documento,
		cSociedad,
		cMoneda,	     
		nTipoCambio,    
		cTexto_Documento,	     	     
		cMes,     
		cBus_Act,	     	     
		cPeriodo,	     	     
		cFolio_Movimiento,
		nCanalPadre
	FROM #CON_POLIZAS_SAP

	SET NOCOUNT OFF;  
END

GO
