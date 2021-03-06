USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EstadoComedero_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EstadoComedero_Crear
--======================================================
CREATE PROCEDURE [dbo].[EstadoComedero_Crear]
@Descripcion varchar(100),
@DescripcionCorta varchar(50),
@NoServir bit,
@AjusteBase decimal(10,2),
@Tendencia char(1),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT EstadoComedero (
		Descripcion,
		DescripcionCorta,
		NoServir,
		AjusteBase,
		Tendencia,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@DescripcionCorta,
		@NoServir,
		@AjusteBase,
		@Tendencia,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
