USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 20/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoGanado_Crear
--======================================================
CREATE PROCEDURE [dbo].[TipoGanado_Crear]
@Descripcion varchar(50),
@Sexo char(1),
@PesoMinimo int,
@PesoMaximo int,
@PesoSalida int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoGanado (
		Descripcion,
		Sexo,
		PesoMinimo,
		PesoMaximo,
		PesoSalida,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@Sexo,
		@PesoMinimo,
		@PesoMaximo,
		@PesoSalida,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
