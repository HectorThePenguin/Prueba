USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 16-10-2013
-- Description:	Crea una entrada de ganado
-- 001 Jorge Luis Velazquez Araujo 04/12/2015 **Se agrega que guarde la columna de FechaPesoBruto
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_Crear]	
	@OrganizacionID INT,
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
	@ImpresionTicket BIT,
	@Costeado BIT,
	@Manejado BIT,
	@Activo BIT, 
	@Usuario INT, 
	@TipoFolio INT,
	@Guia BIT,
	@Factura BIT,
	@Poliza BIT,
	@HojaEmbarque BIT,
	@ManejoSinEstres BIT
	, @CertificadoZoosanitario VARCHAR(15)
    , @PruebasTB VARCHAR(15)
    , @PruebasTR VARCHAR(15)
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @FolioEntrada int 
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @FolioEntrada output
    INSERT INTO EntradaGanado 
		   (FolioEntrada,
			OrganizacionID,
			OrganizacionOrigenID,
			FechaEntrada,
			EmbarqueID,
			FolioOrigen,
			FechaSalida,
			CamionID,
			ChoferID,
			JaulaID,
			CabezasOrigen,
			CabezasRecibidas,
			OperadorID,
			PesoBruto,
			PesoTara,
			EsRuteo,
			Fleje,
			CheckList,
			CorralID,
			LoteID,
			Observacion,
			ImpresionTicket,
			Costeado,
			Manejado,
			Activo, 
			Guia,
			Factura,
			Poliza,
			HojaEmbarque,
			FechaCreacion, 
			UsuarioCreacionID,
			ManejoSinEstres,
			CabezasMuertas
			, CertificadoZoosanitario
			, PruebaTB
			, PruebaTR
			, FechaPesoBruto --001
			)
	VALUES(	@FolioEntrada, 
			@OrganizacionID,
			@OrganizacionOrigenID,
			@FechaEntrada,
			@EmbarqueID,
			@FolioOrigen,
			@FechaSalida,
			@CamionID,
			@ChoferID,
			@JaulaID,
			@CabezasOrigen,
			@CabezasRecibidas,
			@OperadorID,
			@PesoBruto,
			@PesoTara,
			@EsRuteo,
			@Fleje,
			@CheckList, 
			@CorralID,
			@LoteID,
			@Observacion, 
			@ImpresionTicket,
			@Costeado,
			@Manejado,
			@Activo, 
			@Guia,
			@Factura,
			@Poliza,
			@HojaEmbarque,
			Getdate(), 
			@Usuario,
			@ManejoSinEstres,
			0
			, @CertificadoZoosanitario
			, @PruebasTB
			, @PruebasTR
			, GETDATE()--001
			)
	Select SCOPE_IDENTITY()
END

GO
