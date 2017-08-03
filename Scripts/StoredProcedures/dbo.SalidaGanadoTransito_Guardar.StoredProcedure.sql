DROP PROCEDURE [dbo].[SalidaGanadoTransito_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_Guardar]    Script Date: 13/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 13/04/2016 4:55:00 a.m.
-- Description: SP para registrar una salida de ganado en transito por venta o muerte.
-- SpName     : SalidaGanadoTransito_Guardar
--======================================================
CREATE PROCEDURE [dbo].SalidaGanadoTransito_Guardar
@OrganizacionId int,
@LoteId int,
@NumCabezas int,
@Venta bit,
@Muerte BIT,
@Fecha smalldatetime,
@Importe DECIMAL(18,4),
@ClienteID int,
@Kilos int,
@Folio int,
@FolioFactura INT,
@PolizaId int,
@UsuarioCreacionId int,
@Observaciones VARCHAR(255),
@Detalles XML
AS
DECLARE @SalidaGanadoTransitoID INT
DECLARE @EntradaGanadoTransitoID INT
DECLARE @Activo BIT
DECLARE @Serie VARCHAR(100)
BEGIN
	SET NOCOUNT ON;
SET @Activo=1;
--
	/* Se obtiene la serie de la factura*/
	SELECT @Serie=Valor 
	  FROM ParametroOrganizacion 
	 INNER JOIN Parametro ON Parametro.ParametroID=ParametroOrganizacion.ParametroID
	 WHERE OrganizacionID=@OrganizacionId 
	   AND Parametro.Clave='SerieFactura' 
	   AND ParametroOrganizacion.ACTIVO=1; 
--
	INSERT SalidaGanadoTransito(
		OrganizacionID,Folio,LoteID,NumCabezas,Muerte,Venta,Fecha,Importe,ClienteID,Kilos,Serie,FolioFactura,PolizaID,
		Observaciones,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES(
		@OrganizacionId,@Folio,@LoteId,@NumCabezas,@Muerte,@Venta,@Fecha,@Importe,
		@ClienteID,@Kilos,@Serie,@FolioFactura,@PolizaId,
		@Observaciones,1,GETDATE(),@UsuarioCreacionId)

SET @SalidaGanadoTransitoID = SCOPE_IDENTITY();

INSERT SalidaGanadoTransitoDetalle(
		CostoID,ImporteCosto,Activo,FechaCreacion,
		SalidaGanadoTransitoID,UsuarioCreacionID)
SELECT
				t.item.value('./CostoID[1]', 'INT') AS CostoID
				,t.item.value('./ImporteCosto[1]', 'DECIMAL(10,2)') AS ImporteCosto
				,1 AS Activo
				,GETDATE() AS FechaCreacion
				,@SalidaGanadoTransitoID AS SalidaGanadoTransitoID
				,@UsuarioCreacionID AS UsuarioCreacionID
		FROM @Detalles.nodes('ROOT/SalidaGanadoTransitoDetalle') AS t(item)
	SET NOCOUNT OFF;
END