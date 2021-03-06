USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarEntradaLlegadaPorEntradaId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizarEntradaLlegadaPorEntradaId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarEntradaLlegadaPorEntradaId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 20/05/2014
-- Description: Actualiza la entrada de producto cuando se llega con el producto antes de descargarlo.
-- SpName     : exec EntradaProducto_ActualizarEntradaLlegadaPorEntradaId 1, 0, 0, 0, 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizarEntradaLlegadaPorEntradaId] @EntradaProductoId INT
	,@PesoOrigen INT
	,@PesoBonificacion INT
	,@PesoBruto INT
	,@PesoTara INT
	,@OperadorIdBascula INT
	,@PesoDescuento INT
	,@NotaVenta VARCHAR(10) = ''
AS
BEGIN
	IF (@PesoDescuento = NULL)
	BEGIN
		SET @PesoDescuento = 0
	END

	UPDATE EntradaProducto
	SET PesoOrigen = @PesoOrigen
		,PesoBonificacion = @PesoBonificacion
		,PesoBruto = @PesoBruto
		,PesoTara = @PesoTara
		,FechaDestara = CASE 
			WHEN @PesoTara = 0
				THEN NULL
			ELSE GETDATE()
			END
		,OperadorIDBascula = CASE 
			WHEN @OperadorIdBascula = 0
				THEN OperadorIDBascula
			ELSE @OperadorIdBascula
			END
		,PesoDescuento = @PesoDescuento
		,NotaVenta = @NotaVenta		
	WHERE EntradaProductoID = @EntradaProductoId

	update EntradaProducto SET
	FechaPesaje = CASE 
			WHEN @PesoBruto > 0
				AND @PesoTara = 0
				THEN GETDATE()
			ELSE NULL
			END
	WHERE EntradaProductoID = @EntradaProductoId
		AND FechaPesaje IS NULL
END

GO
