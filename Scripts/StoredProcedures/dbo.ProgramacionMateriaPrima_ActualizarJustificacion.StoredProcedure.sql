USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ActualizarJustificacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ActualizarJustificacion]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ActualizarJustificacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 27/06/2014
-- Description: 
-- SpName     : ProgramacionMateriaPrima_ActualizarJustificacion
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ActualizarJustificacion]
@ProgramacionMateriaPrimaID INT,
@Justificacion VARCHAR(255),
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProgramacionMateriaPrima SET
		Justificacion = @Justificacion,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID
	SET NOCOUNT OFF;
END

GO
