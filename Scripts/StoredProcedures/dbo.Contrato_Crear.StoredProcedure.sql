USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez 
-- Create date: 21/05/2014
-- Description: Crea un nuevo contrato
-- Modificado : Roque Solis
-- Fecha modificacion: 24/11/2014
-- Modificacion : Se agregaron los campos: Folio Aserca, Folio Cobertura, Calidad, Costo Secado y Aplica Descuento
-- Contrato_Crear 3, 80, 3, 1, 4842, 9999.999, 1, 99999999, 99999999, 'Origen', 1, 5, 8, '12/08/2014'
--=============================================
CREATE PROCEDURE [dbo].[Contrato_Crear]
	@OrganizacionID INT,
	@ProductoID INT,
	@TipoContratoID INT,
	@TipoFlete INT,
	@ProveedorID INT,
	@Precio DECIMAL(18,4),
	@TipoCambio INT,
	@Cantidad INT,
	@Merma DECIMAL(10,2),
	@PesoNegociar VARCHAR(10),
	@Tolerancia DECIMAL(10,2),
	@Parcial INT,
	@CuentaSAPID INT,
	@EstatusID INT,
	@Activo INT,
	@UsuarioCreacionID INT,
	@TipoFolio INT,
	@FechaVigencia DATETIME,
	@FolioAserca VARCHAR(15),
	@FolioCobertura INT,
	@CalidadOrigen INT,
	@CostoSecado INT,
	@AplicaDescuento INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorFolio int 
	
	IF @FolioCobertura < 0
	BEGIN 
		SET @FolioCobertura = NULL
	END 
	
	IF @CalidadOrigen < 0
	BEGIN
		SET @CalidadOrigen = NULL
	END

	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
	
	INSERT Contrato(
		OrganizacionID,
		Folio,
		ProductoID,
		TipoContratoID,
		TipoFleteID,
		ProveedorID,
		Precio,
		TipoCambioID,
		Cantidad,
		Merma,
		PesoNegociar,
		Fecha,
		FechaVigencia,
		Tolerancia,
		Parcial,
		CuentaSAPID,
		EstatusID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FolioAserca,
		FolioCobertura,
		CalidadOrigen,
		CostoSecado,
		AplicaDescuento		
	)
	VALUES(
		@OrganizacionID,
		@ValorFolio,
		@ProductoID,
		@TipoContratoID,
		@TipoFlete,
		@ProveedorID,
		@Precio,
		@TipoCambio,
		@Cantidad,
		@Merma,
		@PesoNegociar,
		GETDATE(),
		@FechaVigencia,
		@Tolerancia,
		@Parcial,
		@CuentaSAPID,
		@EstatusID,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID,
		@FolioAserca,
		@FolioCobertura,
		@CalidadOrigen,
		@CostoSecado,
		@AplicaDescuento
	)
	
	SELECT 
		C.ContratoID,
		C.OrganizacionID,
		C.Folio,
		C.ProductoID,
		C.TipoContratoID,
		C.TipoFleteID,
		C.ProveedorID,
		C.Precio,
		C.Precio / TC.Cambio AS PrecioConvertido,
		C.TipoCambioID,
		TC.Descripcion,
		C.Cantidad,
		C.Merma,
		C.PesoNegociar,
		C.Fecha,
		C.FechaVigencia,
		C.Tolerancia,
		C.Parcial,
		C.CuentaSAPID,
		C.EstatusID,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID,
		C.FolioAserca,
		C.FolioCobertura,
		C.CalidadOrigen,
		C.CostoSecado,
		C.AplicaDescuento		
	FROM Contrato (NOLOCK) C
	INNER JOIN TipoCambio TC ON(TC.TipoCambioID = C.TipoCambioID)
	WHERE ContratoID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF;
END

GO
