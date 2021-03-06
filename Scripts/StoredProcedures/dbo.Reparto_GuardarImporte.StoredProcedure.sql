USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarImporte]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_GuardarImporte]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarImporte]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 10/05/2014
-- Description:  Actualiza el importe de un reparto
-- Origen: APInterfaces
-- Reparto_GuardarImporte 5,2,12.5
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_GuardarImporte]
	@RepartoID BIGINT,
	@LoteID INT,
	@TipoServicio INT,
	@Prorrateo BIT,
	@PrecioPromedio DECIMAL(12,4),
	@UsuarioModificacionID INT
AS
BEGIN
	/* Se actualizan los repartos que se repartieron */
	UPDATE RD
	   SET Importe = CantidadServida * @PrecioPromedio,
		   CostoPromedio = @PrecioPromedio,
		   Prorrateo = @Prorrateo,
		   UsuarioModificacionID = @UsuarioModificacionID,
		   FechaModificacion = GETDATE()
	  FROM RepartoDetalle RD
	 WHERE RepartoID = @RepartoID 
	   AND TipoServicioID = @TipoServicio;
	/* Se actualiza el lote del reparto */
	UPDATE R
	   SET LoteID = @LoteID,
		   UsuarioModificacionID = @UsuarioModificacionID,
		   FechaModificacion = GETDATE()
	  FROM Reparto R
	 WHERE RepartoID = @RepartoID;
END 

GO
