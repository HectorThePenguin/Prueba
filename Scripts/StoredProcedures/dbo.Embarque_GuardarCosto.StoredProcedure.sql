USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_GuardarCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_GuardarCosto]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_GuardarCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar el detalle de las escalas del embarque
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_GuardarCosto]		
	@XmlCosto XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Detalle AS TABLE
	  (
		[CostoEmbarqueDetalleID]INT,
		[EmbarqueDetalleID]		INT,
		[CostoID]				INT,
		[Orden]			INT,
		[Importe]				DECIMAL(18,4),
		[Activo]				INT,
		[UsuarioCreacionID]		INT,
		[UsuarioModificacionID] INT
	  )
	INSERT @Detalle(
		[CostoEmbarqueDetalleID],
		[EmbarqueDetalleID],
		[CostoID],
		[Orden],
		[Importe],
		[Activo],
		[UsuarioCreacionID],
		[UsuarioModificacionID])
	SELECT 
		[CostoEmbarqueDetalleID] = t.item.value('./CostoEmbarqueDetalleID[1]', 'INT'),
		[EmbarqueDetalleID]		 = t.item.value('./EmbarqueDetalleID[1]', 'INT'),
		[CostoID]				 = t.item.value('./CostoID[1]', 'INT'),
		[Orden]					 = t.item.value('./Orden[1]', 'INT'),
		[Importe]                = t.item.value('./Importe[1]', 'DECIMAL(18,4)'),
		[Activo]                 = t.item.value('./Activo[1]', 'BIT'),
		[UsuarioCreacionID]      = t.item.value('./UsuarioCreacionID[1]', 'INT'),
		[UsuarioModificacionID]  = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @XmlCosto.nodes('ROOT/CostoEmbarqueDetalle') AS T(item)
	UPDATE ce
	SET CostoID               = dt.CostoID,
		Orden         = dt.Orden,
		Importe               = dt.Importe,
		Activo                = dt.Activo,		
		FechaModificacion	  = GETDATE(),
		UsuarioModificacionID = dt.UsuarioModificacionID
	FROM CostoEmbarqueDetalle ce
	INNER JOIN @Detalle dt
	ON dt.CostoEmbarqueDetalleID = ce.CostoEmbarqueDetalleID
	INSERT CostoEmbarqueDetalle
		   ([EmbarqueDetalleID],
			[CostoID],
			[Orden],
			[Importe],
			[Activo],
			[FechaCreacion],
			[UsuarioCreacionID])
	SELECT 
		[EmbarqueDetalleID],
		[CostoID],
		[Orden],
		[Importe],
		[Activo],
		GETDATE(),
		[UsuarioCreacionID]
	FROM @Detalle
	WHERE CostoEmbarqueDetalleID = 0
	SET NOCOUNT OFF;
END

GO
