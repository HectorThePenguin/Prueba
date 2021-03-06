USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 16-10-2013
-- Description:	Actualiza una entrada de ganado
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_Actualizar]		
	@EntradaGanadoID INT, 
	@OrganizacionOrigenID INT,
	@FechaEntrada DATETIME,
	@EmbarqueID INT,
	@FolioOrigen INT,
	@FechaSalida DATETIME,
	@CamionID INT,
	@ChoferID INT,
	@JaulaID INT,
	@CabezasOrigen INT,
	@CabezasRecibidas INT,
	@OperadorID INT,
	@PesoBruto DECIMAL (18,2),
	@PesoTara DECIMAL (18,2),
	@EsRuteo BIT,
	@Fleje BIT,
	@CheckList CHAR(10), 
	@CorralID INT,
	@LoteID INT,
	@Observacion VARCHAR(400), 	
	@Activo BIT, 
	@Usuario INT ,
	@ImpresionTicket BIT,
	@Guia BIT,
	@Factura BIT,
	@Poliza BIT,
	@HojaEmbarque BIT,
	@ManejoSinEstres BIT
	, @CertificadoZoosanitario VARCHAR(15)
	, @PruebaTB	VARCHAR(15)
	, @PruebaTR	VARCHAR(15)
	, @CondicionJaulaID INT
AS 

DECLARE @PesoTaraGuardado INT
set @PesoTaraGuardado = (select PesoTara from EntradaGanado where EntradaGanadoID = @EntradaGanadoID)

if @PesoTaraGuardado = 0 and @PesoTara > 0
BEGIN
	update EntradaGanado set FechaPesoTara = GETDATE()
	where EntradaGanadoID = @EntradaGanadoID
end

BEGIN	
	SET NOCOUNT ON;
    Update EntradaGanado 
	SET OrganizacionOrigenID  =@OrganizacionOrigenID,
		FechaEntrada = @FechaEntrada,
		EmbarqueID = @EmbarqueID,
		FolioOrigen = @FolioOrigen,
		FechaSalida = @FechaSalida ,
		CamionID = @CamionID,
		ChoferID = @ChoferID,
		JaulaID = @JaulaID,
		CabezasOrigen = @CabezasOrigen,
		CabezasRecibidas = @CabezasRecibidas,
		OperadorID = @OperadorID,
		PesoBruto = @PesoBruto,
		PesoTara = @PesoTara,
		EsRuteo = @EsRuteo,
		Fleje = @Fleje,
		CheckList = @CheckList,
		CorralID = @CorralID,
		LoteID = @LoteID,
		Observacion = @Observacion,									
		Activo = @Activo, 
		FechaModificacion = Getdate(), 
		UsuarioModificacionID = @Usuario,
		ImpresionTicket = @ImpresionTicket,
		Guia = @Guia,
		Factura = @Factura,
		Poliza = @Poliza,
		HojaEmbarque = @HojaEmbarque,
		ManejoSinEstres = @ManejoSinEstres
		, CertificadoZoosanitario = @CertificadoZoosanitario
		, PruebaTB = @PruebaTB
		, PruebaTR = @PruebaTR
		, CondicionJaulaID = @CondicionJaulaID
	WHERE EntradaGanadoID = @EntradaGanadoID
END

GO
