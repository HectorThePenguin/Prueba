USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarEntradaGanadoCargaInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarEntradaGanadoCargaInicial]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarEntradaGanadoCargaInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 30/01/2015
-- Description: Guarda informacion en la tabla EntradaGanadoCargaInicial
-- Origen: APInterfaces
-- Migracion_GuardarEntradaGanadoCargaInicial 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarEntradaGanadoCargaInicial]
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	/* Se crean las entradas de ganado carga inicial */
	SELECT 
		FolioEntrada = ROW_NUMBER() OVER (ORDER BY R.[FECHA INICIO]), 
		OrganizacionID = @OrganizacionID, 
		FechaEntrada = (GETDATE() - [DIASENG]), -- [FECHA INICIO],
		EmbarqueID = 0,
		FolioOrigen = 0,
		FechaSalida = DATEADD(DAY, -1, [FECHA INICIO]),
		ChoferID = 1, 
		JaulaID = 1,
		CabezasOrigen = CABEZAS,
		CabezasRecibidas = CABEZAS,
		OperadorID = 6,
		PesoBruto = ROUND([PESO ORIGEN] * CABEZAS,0) + 1000,
		PesoTara = 1000,
		EsRuteo = 0,
		Fleje = 0,
		CheckList = CORRAL,
		CorralID = null,
		LoteID = null,
		Observaciones = 'Carga Inicial',
		ImpresionTicket = 1,
		Costeado = 1,
		Manejado = 1,
		Guia = 1,
		Factura = 1,
		Poliza = 1,
		HojaEmbarque = 1,
		ManejoSinEstres = 1,
		CabezasMuertas = 0,
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioModificaconID = null,
		FechaModificacion = null
	INTO EntradaGanadoCargaInicial
	FROM RESUMEN R
	WHERE R.ORGANIZACION = 3;

	SET NOCOUNT OFF
  END


GO
