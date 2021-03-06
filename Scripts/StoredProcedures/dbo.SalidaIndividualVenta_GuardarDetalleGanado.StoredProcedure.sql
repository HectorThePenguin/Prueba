USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarDetalleGanado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_GuardarDetalleGanado]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarDetalleGanado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 04/03/2014
-- Description:	Guarda el detalle del ticket de venta
/*
	SalidaIndividualVenta_GuardarDetalleGanado '
	<ROOT>
		<GrabarVenta>
			<OrganizacionID>4</OrganizacionID>
			<FolioTicket>1</FolioTicket>
			<CodigoCorral>001</CodigoCorral>
			<UsuarioID>12</UsuarioID>
			<CausaPrecioID>1</CausaPrecioID>
			<Animal>
				<Arete>123456789</Arete>
				<FotoVenta>1212121212121.JPG</FotoVenta>
			</Animal>
			<Animal>
				<Arete>12345679</Arete>
				<FotoVenta>12121212132121.JPG</FotoVenta>
			</Animal>
		</GrabarVenta>
	</ROOT>'
*/
--======================================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_GuardarDetalleGanado]
@XmlVenta XML
AS
BEGIN
		DECLARE @OrganizacionID INT
		DECLARE @FolioTicket INT
		DECLARE @UsuarioCreacionID INT
		DECLARE @CausaPrecioID INT
		DECLARE @VentaGanadoID INT
		DECLARE @CodigoCorral VARCHAR(10)
		DECLARE @LoteID INT
		DECLARE @TipoVenta INT
		DECLARE @TotalCabezas INT
		
		
		SELECT TOP 1
				@OrganizacionID = T.item.value('./OrganizacionID[1]', 'INT'),
				@FolioTicket = T.item.value('./FolioTicket[1]', 'INT'),
				@UsuarioCreacionID = T.item.value('./UsuarioID[1]', 'INT'),
				@CodigoCorral = T.item.value('./CodigoCorral[1]', 'VARCHAR(10)'),
				@TipoVenta = T.item.value('./TipoVenta[1]', 'INT'),
				@TotalCabezas = T.item.value('./TotalCabezas[1]', 'INT'),
				@CausaPrecioID = T.item.value('./CausaPrecioID[1]', 'INT')
		FROM  @XmlVenta.nodes('ROOT/GrabarVenta') AS T(item)

		SELECT 
				@LoteID = LoteID
		FROM Lote (NOLOCK) L
		INNER JOIN Corral (NOLOCK) C
		ON (L.CorralID = C.CorralID)
		WHERE C.Codigo = @CodigoCorral
		and c.OrganizacionID = @OrganizacionID
		and l.Activo = 1
		
		IF @TipoVenta = 1 
		BEGIN 
			SELECT 
					@VentaGanadoID = VentaGanadoID
			FROM VentaGanado (NOLOCK)
			WHERE FolioTicket = @FolioTicket AND Activo = 1
			and OrganizacionID = @OrganizacionID
			
			DECLARE @TablaAretes TABLE(Arete VARCHAR(15),FotoVenta VARCHAR(250), CausaPrecioID INT)
			
			INSERT INTO @TablaAretes
			SELECT 
					FolioTicket = T.item.value('./Arete[1]', 'VARCHAR(15)'),
					UsuarioCreacionID = T.item.value('./FotoVenta[1]', 'VARCHAR(250)'),
					CausaPrecioID = T.item.value('./CausaPrecioID[1]', 'INT')
			FROM  @XmlVenta.nodes('ROOT/GrabarVenta') AS T(item)
			UPDATE 
				VentaGanado
			SET LoteID = @LoteID,FechaModificacion = GETDATE(),UsuarioModificacionID = @UsuarioCreacionID
			WHERE VentaGanadoID = @VentaGanadoID 
			INSERT INTO VentaGanadoDetalle (VentaGanadoID,Arete,AnimalID,FotoVenta,CausaPrecioID,Activo,FechaCreacion,UsuarioCreacion)
			SELECT @VentaGanadoID, A.Arete, A.AnimalID, FotoVenta, CausaPrecioID, 1, GETDATE(), @UsuarioCreacionID
			FROM @TablaAretes TA
			LEFT JOIN Animal A (NOLOCK) ON A.Arete = TA.Arete
		END 
		ELSE
		BEGIN
			SELECT @VentaGanadoID = SalidaGanadoIntensivoID
			FROM SalidaGanadoIntensivo (NOLOCK)
			WHERE FolioTicket = @FolioTicket AND Activo = 1
			and OrganizacionID = @OrganizacionID
			
			UPDATE SalidaGanadoIntensivo 
			SET LoteID = @LoteID,
			CausaPrecioID = @CausaPrecioID,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioCreacionID, 
			Cabezas = @TotalCabezas
			WHERE SalidaGanadoIntensivoID = @VentaGanadoID AND OrganizacionID = @OrganizacionID AND Activo = 1
			
			
			
			
		END
END

GO
