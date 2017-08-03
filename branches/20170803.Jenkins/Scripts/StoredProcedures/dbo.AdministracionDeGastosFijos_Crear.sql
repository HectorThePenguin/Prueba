USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_Crear]    Script Date: 06/07/2017 06:22:39 p.m. ******/
DROP PROCEDURE [dbo].[AdministracionDeGastosFijos_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_Crear]    Script Date: 06/07/2017 06:22:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Murillo Mayorquín Héctor Adrián
-- Create date: 06-07-2017
-- Description: Guarda un nuevo gasto fijo
-- SpName     : AdministracionDeGastosFijos_Crear 'Prueba', 1, 200, 1
--======================================================  
CREATE PROCEDURE [dbo].[AdministracionDeGastosFijos_Crear]
		@Descripcion VARCHAR(250),
		@Activo BIT,
		@Importe INT,
		@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO GastosFijos (Descripcion, Activo, Importe, FechaCreacion, UsuarioCreacionID)
	VALUES (@Descripcion, @Activo, @Importe, GETDATE(), @UsuarioCreacionID)
	
	SET NOCOUNT OFF;
END
GO