USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GanadoIntesivo_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GanadoIntesivo_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[GanadoIntesivo_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 03/12/2015
-- Description:  Guarda Ganado Intensivo
--
/* Exec GanadoIntesivo_Guardar 
'<ROOT>
  <Costos>
    <CostoID>1</CostoID>
    <Importe>46754.55</Importe>
  </Costos>
  <Costos>
    <CostoID>4</CostoID>
    <Importe>757.01</Importe>
  </Costos>
  <Costos>
    <CostoID>8</CostoID>
    <Importe>22.43</Importe>
  </Costos>
  <Costos>
    <CostoID>20</CostoID>
    <Importe>97.89</Importe>
  </Costos>
</ROOT>'*/
-- =============================================
CREATE PROCEDURE [dbo].[GanadoIntesivo_Guardar]
@XMLCostos XML,
@TipoMovimientoID INT,
@LoteID INT,
@Cabezas INT,
@CabezasAnterior INT,
@Importe DECIMAL(10,2),
@Observaciones VARCHAR(255),
@OrganizacionID INT,
@TipoFolio INT,
@Activo BIT,
@UsuarioID INT,
@FolioTicket INT,
@PesoBruto decimal(10, 2)
AS
BEGIN

		DECLARE @IdentityID INT;
		DECLARE @ValorFolio INT
		DECLARE @Serie VARCHAR(5)
		DECLARE @FolioOutput VARCHAR(10)
		DECLARE @VentaGanadoID INT
		DECLARE @FolioFactura VARCHAR(15)
		
		IF @TipoMovimientoID = 44 BEGIN	
		EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @ValorFolio output
			
			INSERT INTO SalidaGanadoIntensivo
			(
					TipoMovimientoID,
					FolioTicket,
					Fecha,
					LoteID,
					Cabezas,
					CabezasAnterior,
					Importe,
					Observaciones,
					Activo,
					UsuarioCreacionID,
					FechaCreacion
			)VALUES(@TipoMovimientoID,
							@ValorFolio,
							GETDATE(),
							@LoteID,
							@Cabezas,
							@CabezasAnterior,
							@Importe,
							@Observaciones,
							@Activo,
							@UsuarioID,
							GETDATE())

			SET @IdentityID = (SELECT @@IDENTITY);
		END	
	
		--Actualiza en SalidaGanadoIntensivoPesaje y SalidaGanadoIntensivo si el tipo de movimiento es salida por venta de ganado intensiva--
		IF @TipoMovimientoID = 43 BEGIN		
		select 1
			EXEC FolioFactura_Obtener @OrganizacionID, @FolioFactura OUTPUT, @Serie OUTPUT, @FolioOutput OUTPUT
			
			SELECT @IdentityID = SG.SalidaGanadoIntensivoID
				FROM SalidaGanadoIntensivo SG(NOLOCK) 
				WHERE SG.FolioTicket = @FolioTicket	AND SG.OrganizacionID = @OrganizacionID
						
			UPDATE SalidaGanadoIntensivo SET FolioFactura = @FolioFactura, Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioID 
			WHERE FolioTicket = @FolioTicket AND OrganizacionID = @OrganizacionID
			
			UPDATE SalidaGanadoIntensivoPesaje SET PesoBruto = @PesoBruto, Activo = 0, FechaModificacion = GETDATE(), FechaPesoBruto = GETDATE(), UsuarioModificacionID = @UsuarioID 
			WHERE SalidaGanadoIntensivoID = @IdentityID
			SET @ValorFolio = @IdentityID
		END
		
		
		INSERT INTO GanadoIntensivoCosto
		(
			SalidaGanadoIntensivoID,
			CostoID,
			Importe,
			Activo,
			UsuarioCreacionID,	
			FechaCreacion
		)
		SELECT 
				@IdentityID AS ControlEntradaGanadoID
				, t.item.value('./CostoID[1]', 'INT') AS CostoID
				, t.item.value('./Importe[1]', 'DECIMAL(18,2)') AS Importe
				,@Activo
				,@UsuarioID
				,GETDATE()
		FROM @XMLCostos.nodes('ROOT/Costos') AS T(item)
			
		
		
		SELECT @ValorFolio

END