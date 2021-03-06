USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 20/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoGanado_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoGanado_Actualizar]
@TipoGanadoID int,
@Descripcion varchar(50),
@Sexo char(1),
@PesoMinimo int,
@PesoMaximo int,
@PesoSalida int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoGanado SET
		Descripcion = @Descripcion,
		Sexo = @Sexo,
		PesoMinimo = @PesoMinimo,
		PesoMaximo = @PesoMaximo,
		PesoSalida = @PesoSalida,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoGanadoID = @TipoGanadoID
	SET NOCOUNT OFF;
END

GO
