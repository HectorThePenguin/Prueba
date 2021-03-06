USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ActualizarFacturacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ActualizarFacturacion]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ActualizarFacturacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 17/08/2014
-- Description: Actualiza los datos de la facturacion de sacrificio
-- SpName     : LoteSacrificio_ActualizarFacturacion 1, 1, 3, 'rrrrr', 1
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ActualizarFacturacion] @OrdenSacrificioId INT
	,@OrganizacionId INT
	,@ClienteId INT
	,@Observaciones VARCHAR(255)
	,@UsuarioModificacionId INT
AS
BEGIN
	DECLARE @FolioFactura VARCHAR(15)
	DECLARE @Serie VARCHAR(5)
	DECLARE @Folio VARCHAR(15)
	DECLARE @LoteSacrificioId INT
 	DECLARE @Fecha SMALLDATETIME  

	BEGIN TRY
		IF @ClienteId > 0
		BEGIN
				-- Obtiene el numero que sigue de la factura segun el parametro configurado para cada organizacion.
				EXEC FolioFactura_Obtener @OrganizacionId
					,@FolioFactura OUTPUT
					,@Serie OUTPUT
					,@Folio OUTPUT

				SELECT TOP 1 @Fecha = Fecha
				FROM LoteSacrificio
				WHERE OrdenSacrificioID = @OrdenSacrificioId
					AND Serie IS NULL

    			UPDATE A
				SET ClienteID = @ClienteId
					,Serie = @Serie
					,Folio = @Folio
					,FechaModificacion = GETDATE()
					,UsuarioModificacionID = @UsuarioModificacionId
			    FROM LoteSacrificio A
			    INNER JOIN Lote B 
			      On A.LoteID = B.LoteID  
			    WHERE A.Fecha = @Fecha    
			      AND B.OrganizacionID = @OrganizacionId
     
		END

		IF @ClienteId = 0
		BEGIN
			UPDATE LoteSacrificio
			SET ClienteID = NULL
				,SerieFolio = CONCAT(Serie, '-', Folio)
				,Serie = NULL
				,Folio = NULL
				,Observaciones = @Observaciones
				,FechaModificacion = GETDATE()
				,UsuarioModificacionID = @UsuarioModificacionId
			WHERE OrdenSacrificioID = @OrdenSacrificioId
		END
	END TRY

	BEGIN CATCH
		THROW;
	END CATCH
END

GO
