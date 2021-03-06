USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CalidadGanado_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CalidadGanado_Actualizar]
@CalidadGanadoID int,
@Descripcion varchar(50),
@Calidad varchar(10),
@Sexo char(1),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CalidadGanado SET
		Descripcion = @Descripcion,
		Calidad = @Calidad,
		Sexo = @Sexo,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CalidadGanadoID = @CalidadGanadoID
	SET NOCOUNT OFF;
END

GO
