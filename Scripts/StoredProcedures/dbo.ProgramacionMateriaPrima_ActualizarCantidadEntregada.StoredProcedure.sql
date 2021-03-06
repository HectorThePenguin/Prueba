USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ActualizarCantidadEntregada]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ActualizarCantidadEntregada]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ActualizarCantidadEntregada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 21/06/2014
-- Description: 
-- SpName     : ProgramacionMateriaPrima_ActualizarCantidadEntregada
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ActualizarCantidadEntregada]
@ProgramacionMateriaPrimaID INT,
@CantidadEntregada DECIMAL(14,2),
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProgramacionMateriaPrima SET
		CantidadEntregada = COALESCE(CantidadEntregada, 0) + @CantidadEntregada,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID
	SET NOCOUNT OFF;
END

GO
