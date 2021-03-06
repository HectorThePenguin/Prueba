USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_GuardarEntradaDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_GuardarEntradaDetalle]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_GuardarEntradaDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    José Gilberto Quintero López
-- Create date: 31/10/2013
-- Description:  Guardar el detalle de la entrada de ganado (EntradaDetalle)
-- 
-- ===============================================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_GuardarEntradaDetalle]		
	@XmlDetalle XML 
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Detalle AS TABLE
	  (
		[EntradaDetalleID] INT,
		[EntradaGanadoCosteoID] INT,
		[TipoGanadoID] INT,
		[Cabezas] INT,
		[PesoOrigen] DECIMAL(18,2),
		[PesoLlegada] DECIMAL(18,2),
		[PrecioKilo] DECIMAL(18,2),
		[Importe] DECIMAL(18,2),
		[ImporteOrigen] DECIMAL(18,2),
		[Activo] BIT,
		[FechaCreacion] SMALLDATETIME,
		[UsuarioCreacionID] INT,
		[FechaModificacion] SMALLDATETIME,
		[UsuarioModificacionID] INT      
	  )

	INSERT @Detalle
		   (
			[EntradaDetalleID],
			[EntradaGanadoCosteoID],
			[TipoGanadoID],
			[Cabezas],
			[PesoOrigen],
			[PesoLlegada],
			[PrecioKilo],
			[Importe],
			[ImporteOrigen],
			[Activo],
			[UsuarioCreacionID],
			[UsuarioModificacionID]
			)

		SELECT 
			[EntradaDetalleID]     = t.item.value('./EntradaDetalleID[1]', 'INT'),
			[EntradaGanadoCosteoID]= t.item.value('./EntradaGanadoCosteoID[1]', 'INT'),
			[TipoGanadoID]         = t.item.value('./TipoGanadoID[1]', 'INT'),
			[Cabezas]              = t.item.value('./Cabezas[1]', 'INT'),
			[PesoOrigen]           = t.item.value('./PesoOrigen[1]', 'DECIMAL(18,2)'),
			[PesoLlegada]          = t.item.value('./PesoLlegada[1]', 'DECIMAL(18,2)'),
			[PrecioKilo]           = t.item.value('./PrecioKilo[1]', 'DECIMAL(18,2)'),
			[Importe]              = t.item.value('./Importe[1]', 'DECIMAL(18,2)'),
			[ImporteOrigen]        = t.item.value('./ImporteOrigen[1]', 'DECIMAL(18,2)'),
			[Activo]               = t.item.value('./Activo[1]', 'BIT'),
			[UsuarioCreacionID]    = t.item.value('./UsuarioCreacionID[1]', 'INT'),
			[UsuarioModificacionID]= t.item.value('./UsuarioModificacionID[1]', 'BIT')
			FROM @XmlDetalle.nodes('ROOT/EntradaDetalle') AS T(item)

	UPDATE ed
	SET    
		[EntradaGanadoCosteoID]= dt.[EntradaGanadoCosteoID],
		[TipoGanadoID]         = dt.[TipoGanadoID],
		[Cabezas]              = dt.[Cabezas],
		[PesoOrigen]           = dt.[PesoOrigen],
		[PesoLlegada]          = dt.[PesoLlegada],
		[PrecioKilo]           = dt.[PrecioKilo],
		[Importe]              = dt.[Importe],
		[ImporteOrigen]        = dt.[ImporteOrigen],
		[Activo]               = dt.[Activo],
		[FechaModificacion]    = GETDATE(),    
		[UsuarioModificacionID]= NULLIF(dt.[UsuarioModificacionID], 0)
	FROM  EntradaDetalle ed
	INNER JOIN @Detalle dt
		ON dt.EntradaDetalleID =  ed.EntradaDetalleID

	INSERT EntradaDetalle
		   (
			[EntradaGanadoCosteoID],
			[TipoGanadoID],
			[Cabezas],
			[PesoOrigen],
			[PesoLlegada],
			[PrecioKilo],
			[Importe],
			[ImporteOrigen],
			[Activo],
			[FechaCreacion],    
			[UsuarioCreacionID] 
			) 
	SELECT 
		[EntradaGanadoCosteoID],
		[TipoGanadoID],
		[Cabezas],
		[PesoOrigen],
		[PesoLlegada],
		[PrecioKilo],
		[Importe],
		[ImporteOrigen],
		[Activo],
		GETDATE(),    
		[UsuarioCreacionID] 
	FROM   @Detalle
	WHERE  EntradaDetalleID = 0

	SET NOCOUNT OFF;
END

GO
