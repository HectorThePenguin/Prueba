USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014
-- Description: 
-- SpName     : Liquidacion_Crear
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_Crear]
@ContratoID INT,
@OrganizacionID INT,
@TipoCambio DECIMAL(10,4),
@Folio BIGINT,
@Fecha SMALLDATETIME,
@IPRM DECIMAL(10,4),
@CuotaEjidal decimal(10,4),
@ProEducacion decimal(10,4),
@PIEAFES decimal(10,4),
@Factura varchar(50),
@Cosecha char(3),
@FechaInicio smalldatetime,
@FechaFin smalldatetime,
@Activo bit,
@UsuarioCreacionID int,
@TipoFolio INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FolioEntrada INT
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @FolioEntrada OUTPUT
		INSERT Liquidacion (
			ContratoID,
			OrganizacionID,
			TipoCambio,
			Folio,
			Fecha,
			IPRM,
			CuotaEjidal,
			ProEducacion,
			PIEAFES,
			Factura,
			Cosecha,
			FechaInicio,
			FechaFin,
			Activo,
			UsuarioCreacionID,
			FechaCreacion
		)
		VALUES(
			@ContratoID,
			@OrganizacionID,
			@TipoCambio,
			@FolioEntrada,
			@Fecha,
			@IPRM,
			@CuotaEjidal,
			@ProEducacion,
			@PIEAFES,
			@Factura,
			@Cosecha,
			@FechaInicio,
			@FechaFin,
			@Activo,
			@UsuarioCreacionID,
			GETDATE()
		)
		SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
