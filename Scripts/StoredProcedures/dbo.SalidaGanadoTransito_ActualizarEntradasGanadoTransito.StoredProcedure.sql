USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ActualizarEntradasGanadoTransito]    Script Date: 15/04/2016 4:55:00 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_ActualizarEntradasGanadoTransito]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ActualizarEntradasGanadoTransito]    Script Date: 15/04/2016 4:55:00 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 15/04/2016 4:55:00 a.m.
-- Description: SP para actualizar las entradas de ganado en transito por venta o muerte.
-- SpName     : SalidaGanadoTransito_ActualizarEntradasGanadoTransito
--======================================================
CREATE PROCEDURE [dbo].SalidaGanadoTransito_ActualizarEntradasGanadoTransito
@EntradaGanadoTransitoID INT,
@LoteId int,
@NumCabezas int,
@Fecha smalldatetime,
@Importe DECIMAL(10,4),
@Kilos int,
@UsuarioModificacionID int,
@Importes XML
AS

DECLARE @Activo BIT
BEGIN
	SET @Activo=1;
	SELECT @Activo=0 FROM EntradaGanadoTransito WHERE LoteID=@LoteID AND Cabezas=@NumCabezas;

	UPDATE EntradaGanadoTransito SET
		Cabezas=Cabezas-@NumCabezas,
		Peso=Peso-@Kilos,
		FechaModificacion=GETDATE(),
		UsuarioModificacionID=@UsuarioModificacionID,
		Activo= @Activo
	WHERE LoteID=@LoteID;

--reducir el importe de los costos del lote 
	UPDATE EntradaGanadoTransitoDetalle SET 
		UsuarioModificacionID=@UsuarioModificacionID,
		FechaModificacion=GETDATE()
	WHERE EntradaGanadoTransitoID=@EntradaGanadoTransitoID;
--
	UPDATE EntradaGanadoTransitoDetalle 
	   SET Importe= t.item.value('./Importe[1]', 'Decimal(10,2)')
	  FROM @Importes.nodes('ROOT/Importes') AS T(item)
	 WHERE EntradaGanadoTransitoID= @EntradaGanadoTransitoID 
	   AND CostoID=t.item.value('./CostoId[1]', 'INT')
			
--
	UPDATE EntradaGanadoTransitoDetalle 
	   SET Activo=0 
	 WHERE Importe<=0
--
	SET @Activo=1;
	UPDATE Lote 
	   SET Cabezas=Cabezas-@NumCabezas, FechaSalida=GETDATE(),FechaModificacion=GETDATE(),
				UsuarioModificacionID=@UsuarioModificacionID 
	 WHERE LoteID=@LoteID;
	 
	UPDATE Lote 
	   SET Activo=0 
	 WHERE LoteID=@LoteID And Cabezas=0;

END