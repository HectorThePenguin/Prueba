USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_ObtenerPorDescripcion]    Script Date: 10/07/2017 10:06:39 a.m. ******/
DROP PROCEDURE [dbo].[AdministracionDeGastosFijos_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_ObtenerPorDescripcion]    Script Date: 10/07/2017 10:06:39 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Murillo Mayorquín Héctor Adrián
-- Create date: 10-07-2017
-- Description: Obtiene un gasto fijo por descripción
-- SpName     : AdministracionDeGastosFijos_ObtenerPorDescripcion 'Prueba'
--======================================================  
CREATE PROCEDURE [dbo].[AdministracionDeGastosFijos_ObtenerPorDescripcion]
		@Descripcion VARCHAR(250)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT GastoFijoID, Descripcion, Importe, Activo, UsuarioCreacionID
	FROM GastosFijos
	WHERE Descripcion = @Descripcion
	
	SET NOCOUNT OFF;
END
GO