USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorIDTiposAlmacen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAlmacen_ObtenerPorIDTiposAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorIDTiposAlmacen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/11/2014
-- Description: 
-- SpName     : TipoAlmacen_ObtenerPorIDTiposAlmacen 0, '',1,1,10
--======================================================
CREATE PROCEDURE [dbo].[TipoAlmacen_ObtenerPorIDTiposAlmacen]
@TipoAlmacenID int,
@Activo BIT,
@XmlTiposAlmacen XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TmpTiposAlmacen TABLE(TipoAlmacenID INT)
	INSERT INTO @TmpTiposAlmacen
	SELECT TipoAlmacenID  = T.item.value('./TipoAlmacenID[1]', 'INT')
	  FROM @XmlTiposAlmacen.nodes('ROOT/TiposAlmacen') AS T(item) 
	SELECT		
		ta.TipoAlmacenID,
		ta.Descripcion,
		ta.Activo	
	FROM TipoAlmacen ta
	INNER JOIN @TmpTiposAlmacen tmp ON ta.TipoAlmacenID = tmp.TipoAlmacenID
	WHERE ta.TipoAlmacenID = @TipoAlmacenID
	AND Activo = @Activo	
	SET NOCOUNT OFF;
END

GO
