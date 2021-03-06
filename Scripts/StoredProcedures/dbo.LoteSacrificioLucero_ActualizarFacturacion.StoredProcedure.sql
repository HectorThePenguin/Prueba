USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ActualizarFacturacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_ActualizarFacturacion]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ActualizarFacturacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 17/08/2014
-- Description: Actualiza los datos de la facturacion de sacrificio
-- SpName     : LoteSacrificioLucero_ActualizarFacturacion 5, 92, '', 1
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificioLucero_ActualizarFacturacion] @OrganizacionId INT
	,@ClienteId INT
	,@Observaciones VARCHAR(255)
	,@UsuarioModificacionId INT
	,@Fecha DATE
AS
BEGIN
	DECLARE @FolioFactura VARCHAR(15)
	DECLARE @Serie VARCHAR(5)
	DECLARE @Folio VARCHAR(15)
	DECLARE @LoteID INT
	DECLARE @InterfaceSalidaTraspasoDetalleID INT

	BEGIN TRY
		IF @ClienteId > 0
		BEGIN
				-- Obtiene el numero que sigue de la factura segun el parametro configurado para cada organizacion.
				EXEC FolioFactura_Obtener @OrganizacionId
					,@FolioFactura OUTPUT
					,@Serie OUTPUT
					,@Folio OUTPUT


				UPDATE LoteSacrificioLucero
				SET ClienteID = @ClienteId
					,Serie = @Serie
					,Folio = @Folio
					,FechaModificacion = GETDATE()
					,UsuarioModificacionID = @UsuarioModificacionId
				WHERE CAST(Fecha AS DATE) = @Fecha  
					AND Serie IS NULL
		END

		IF @ClienteId = 0
		BEGIN
			UPDATE LSL
			SET ClienteID = NULL
				,SerieFolio = CONCAT(Serie, '-', Folio)
				,Serie = NULL
				,Folio = NULL
				,Observaciones = @Observaciones
				,FechaModificacion = GETDATE()
				,UsuarioModificacionID = @UsuarioModificacionId
			FROM LoteSacrificioLucero LSL
			WHERE CAST(Fecha AS DATE) = @Fecha
		END
	END TRY

	BEGIN CATCH
		THROW;
	END CATCH
END

GO
