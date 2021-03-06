USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProrrateo_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para insertar un Tipo de Prorrateo
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoProrrateo_Crear]
@Descripcion varchar(50),
	@Activo bit,
	@FechaCreacion smalldatetime,
	@UsuarioCreacionID int,
	@FechaModificacion smalldatetime,
	@UsuarioModificacionID int	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoProrrateo(
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	)
	VALUES(
		@Descripcion,
		@Activo,
		@FechaCreacion,
		@UsuarioCreacionID,
		@FechaModificacion,
		@UsuarioModificacionID	
	)
	SET NOCOUNT OFF;
END

GO
