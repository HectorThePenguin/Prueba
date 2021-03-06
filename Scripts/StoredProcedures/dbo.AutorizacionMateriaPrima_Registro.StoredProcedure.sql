USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_Registro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_Registro]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_Registro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 25/11/2014
-- Description: Crea registro AutorizacionMateriaPrima
-- AutorizacionMateriaPrima_Registro 1,1,106,'sidbfib',114,3.2449,60.00,100,6,13,5,1
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_Registro]
	@OrganizacionID INT,
	@TipoAutorizacionID INT,
  @Folio DECIMAL,
  @Justificacion VARCHAR(255),
  @Lote INT,
  @Precio DECIMAL,
  @Cantidad DECIMAL,
  @ProductoID INT,
  @AlmacenID INT,
	@EstatusID INT,
  @UsuarioCreacion INT,
  @Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT AutorizacionMateriaPrima(
		OrganizacionID,
		TipoAutorizacionID,
		Folio,
		Justificacion,
		Observaciones,
		Lote,
		Precio,
		Cantidad,
		ProductoID,
		EstatusID,
		AlmacenID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@OrganizacionID,
		@TipoAutorizacionID,
		@Folio,
		@Justificacion,
		NULL,
		@Lote,
		@Precio,
		@Cantidad,
		@ProductoID,
		@EstatusID,
		@AlmacenID,
		@Activo,
		GETDATE(),
		@UsuarioCreacion
	)
	SELECT @@IDENTITY AS 'AutorizacionMateriaPrimaID';
	SET NOCOUNT OFF;
END

GO
