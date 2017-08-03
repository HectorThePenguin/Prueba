USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_Actualizar]    Script Date: 06/07/2017 06:22:39 p.m. ******/
DROP PROCEDURE [dbo].[AdministracionDeGastosFijos_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_Actualizar]    Script Date: 06/07/2017 06:22:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Murillo Mayorquín Héctor Adrián
-- Create date: 06-07-2017
-- Description: Actualiza un gasto fijo
-- SpName     : AdministracionDeGastosFijos_Actualizar 1, Prueba edicion, 1, 500, 1
--======================================================  
CREATE PROCEDURE [dbo].[AdministracionDeGastosFijos_Actualizar]
		@GastoFijoID INT,
		@Descripcion VARCHAR(250),
		@Activo BIT,
		@Importe FLOAT,
		@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE GastosFijos
	SET Descripcion = @Descripcion,
		Activo = @Activo,
		Importe = @Importe,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE GastoFijoID = @GastoFijoID
	
	SET NOCOUNT OFF;
END
GO