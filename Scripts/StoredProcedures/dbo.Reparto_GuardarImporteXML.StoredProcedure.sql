USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarImporteXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_GuardarImporteXML]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarImporteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo	
-- Create date: 04/12/2014
-- Description:  Actualiza el importe de un reparto por XML
-- Reparto_GuardarImporteXML
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_GuardarImporteXML]
	@RepartosXML XML	
AS
BEGIN
	CREATE TABLE #REPARTOS (
		RepartoID BIGINT,
		LoteID INT,
		TipoServicio INT,
		Prorrateo BIT,
		PrecioPromedio DECIMAL(12,4),
		UsuarioModificacionID INT					
	)	
	INSERT #REPARTOS (
		RepartoID,
		LoteID,
		TipoServicio,
		Prorrateo,
		PrecioPromedio,
		UsuarioModificacionID)
	SELECT RepartoID = t.item.value('./RepartoID[1]', 'BIGINT'),
		LoteID = t.item.value('./LoteID[1]', 'INT'),
		TipoServicio = t.item.value('./TipoServicio[1]', 'INT'),
		Prorrateo = t.item.value('./Prorrateo[1]', 'BIT'),
		PrecioPromedio = t.item.value('./PrecioPromedio[1]', 'DECIMAL(12,4)'),
		UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @RepartosXML.nodes('ROOT/Repartos') AS T(item)
	update #REPARTOS set LoteID = NULL
	where LoteID = 0
	/* Se actualizan los repartos que se repartieron */
	UPDATE RD
	   SET Importe = CantidadServida * tmp.PrecioPromedio,
		   CostoPromedio = tmp.PrecioPromedio,
		   Prorrateo = tmp.Prorrateo,
		   UsuarioModificacionID = tmp.UsuarioModificacionID,
		   FechaModificacion = GETDATE()
	  FROM RepartoDetalle RD
	  INNER JOIN #REPARTOS tmp on rd.RepartoID = tmp.RepartoID and rd.TipoServicioID = tmp.TipoServicio
	/* Se actualiza el lote del reparto */
	UPDATE R
	   SET LoteID = tmp.LoteID,
		   UsuarioModificacionID = tmp.UsuarioModificacionID,
		   FechaModificacion = GETDATE()
	  FROM Reparto R	  
	  INNER JOIN #REPARTOS tmp on r.RepartoID = tmp.RepartoID
END 

GO
