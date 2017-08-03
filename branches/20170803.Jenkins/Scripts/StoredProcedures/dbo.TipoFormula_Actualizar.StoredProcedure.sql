USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoFormula_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoFormula_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoFormula_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoFormula_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoFormula_Actualizar]
@TipoFormulaID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoFormula SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoFormulaID = @TipoFormulaID
	SET NOCOUNT OFF;
END

GO
