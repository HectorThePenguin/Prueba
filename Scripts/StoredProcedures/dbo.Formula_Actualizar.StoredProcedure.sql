USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Formula_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Formula_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Formula_Actualizar]
@FormulaID int,
@Descripcion varchar(50),
@TipoFormulaID int,
@ProductoID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Formula SET
		Descripcion = @Descripcion,
		TipoFormulaID = @TipoFormulaID,
		ProductoID = @ProductoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE FormulaID = @FormulaID
	SET NOCOUNT OFF;
END

GO
