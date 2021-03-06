USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/25
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_ObtenerPorID]
@EntradaGanadoCosteoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		egc.EntradaGanadoCosteoID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		egc.EntradaGanadoID,
		egc.Activo,
		egc.FechaCreacion,
		egc.UsuarioCreacion,
		egc.FechaModificacion,
		egc.UsuarioModificacion,
		egc.Observacion	
	FROM EntradaGanadoCosteo egc
	INNER JOIN Organizacion o on egc.OrganizacionID = o.OrganizacionID
	WHERE egc.EntradaGanadoCosteoID = @EntradaGanadoCosteoID
	AND egc.Activo = 1
	SET NOCOUNT OFF;
END

GO
