USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_Cancelar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_Cancelar]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_Cancelar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pedro Delgado
-- Create date: 22/12/2014
-- Description:	Cancela una programacion
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_Cancelar]
@ProgramacionMateriaPrimaID BIGINT,
@UsuarioID INT
AS
BEGIN
	UPDATE ProgramacionMateriaPrima
	SET UsuarioModificacionID = @UsuarioID,
			FechaModificacion = GETDATE(),
			Activo = 0
	WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID
END

GO
