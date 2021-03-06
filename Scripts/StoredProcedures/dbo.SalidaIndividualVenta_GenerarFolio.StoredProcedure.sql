USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GenerarFolio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_GenerarFolio]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GenerarFolio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
--  exec SalidaIndividualVenta_GenerarFolio 4, 40.86, '1234', 6, 1
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_GenerarFolio]
	@OrganizacionID INT,
	@PesoTara DECIMAL(10, 2),
	@CodigoSAP VARCHAR(10),
	@TipoFolio INT,
	@Usuario INT,
	@TipoVenta INT = 1 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorFolio INT
	DECLARE @ClienteID INT
	DECLARE @SalidaGanadoID INT
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
	SELECT @ClienteID = ClienteID FROM Cliente WHERE CodigoSAP = @CodigoSAP
	IF @TipoVenta = 1 BEGIN
	    INSERT INTO VentaGanado ( FolioTicket, PesoTara, PesoBruto, ClienteID, FechaVenta, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID, OrganizacionID ) 
	    VALUES (@ValorFolio, @PesoTara, 0, @ClienteID, GETDATE(), 1, GETDATE(), @Usuario, NULL, NULL, @OrganizacionID)
	END
	ELSE BEGIN
	    INSERT INTO SalidaGanadoIntensivo ( TipoMovimientoID, FolioTicket, ClienteID, Fecha, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID, OrganizacionID)
	    VALUES (43, @ValorFolio, @ClienteID, GETDATE(), 1, GETDATE(), @Usuario, NULL, NULL, @OrganizacionID)
	    SELECT  @SalidaGanadoID = SalidaGanadoIntensivoID from SalidaGanadoIntensivo where FolioTicket = @ValorFolio AND OrganizacionID = @OrganizacionID
		INSERT INTO SalidaGanadoIntensivoPesaje (SalidaGanadoIntensivoID, PesoTara, PesoBruto, Activo, FechaCreacion, FechaPesoTara, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
		VALUES (@SalidaGanadoID, @PesoTara, 0, 1, GETDATE(), GETDATE(), @Usuario, NULL, NULL)
	END
	SELECT @ValorFolio
	SET NOCOUNT OFF;
END

GO
