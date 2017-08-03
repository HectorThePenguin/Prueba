USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Marcas_ObtenerMarcas]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Guillermo Osuna Covarrubias
-- Create date: 22 may 2017
-- Description: Obtiene todas las marcas de los camiones
-- SpName     : usp_ObtenerMarcas
--======================================================
CREATE PROCEDURE [dbo].[Marcas_ObtenerMarcas] 
@Tipo INT,
@Activo INT
AS
BEGIN
	SELECT 
		   MarcaID AS MarcaID,
		   Descripcion AS Descripcion
	FROM Marca 
	WHERE Activo = @Activo AND Tracto = @Tipo;
END	

GO