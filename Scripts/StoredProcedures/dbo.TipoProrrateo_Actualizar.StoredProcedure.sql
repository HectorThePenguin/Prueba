USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProrrateo_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para actualzar un Tipo de Prorrateo
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoProrrateo_Actualizar]
	@TipoProrrateoID int,
	@Descripcion varchar(50),
	@Activo bit,
	@FechaCreacion smalldatetime,
	@UsuarioCreacionID int,
	@FechaModificacion smalldatetime,
	@UsuarioModificacionID int	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE TipoProrrateo SET 
			Descripcion = @Descripcion,
			Activo = @Activo,
			FechaCreacion = @FechaCreacion,
			UsuarioCreacionID = @UsuarioCreacionID,
			FechaModificacion = @FechaModificacion,
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE TipoProrrateoID = @TipoProrrateoID
		SET NOCOUNT OFF;
END

GO
