USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Estatus_ObtenerPorTipoEstatusID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Estatus_ObtenerPorTipoEstatusID]
GO
/****** Object:  StoredProcedure [dbo].[Estatus_ObtenerPorTipoEstatusID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 24/03/2014
-- Description: 
-- SpName     : exec Estatus_ObtenerPorTipoEstatusID 5
--======================================================
CREATE PROCEDURE [dbo].[Estatus_ObtenerPorTipoEstatusID]
@TipoEstatusID INT
,@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		E.EstatusID, E.Descripcion, E.DescripcionCorta
		FROM Estatus E
		INNER JOIN TipoEstatus TE ON TE.TipoEstatusID = E.TipoEstatus
	WHERE TE.TipoEstatusID = @TipoEstatusID
	AND E.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
