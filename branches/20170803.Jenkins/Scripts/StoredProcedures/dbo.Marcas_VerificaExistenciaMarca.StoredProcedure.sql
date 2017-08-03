USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_VerificaExistenciaMarca]    Script Date: 22/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Marcas_VerificaExistenciaMarca]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_VerificaExistenciaMarca]    Script Date: 22/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 22-05-2017
-- Description: Procedimiento almacenado que obtiene por página las marcas registradas.
-- SpName     : Marcas_VerificaExistenciaMarca 'MARCA ACTUALIZADA', 1
--======================================================  
CREATE PROCEDURE [dbo].[Marcas_VerificaExistenciaMarca]
	@Descripcion VARCHAR(255),
	@Tracto BIT
AS
BEGIN
	SELECT MarcaID, Descripcion, Activo, Tracto, UsuarioCreacionId, FechaCreacion, UsuarioModificacionId, FechaModificacion
	FROM Marca WHERE Descripcion = @Descripcion  AND Tracto = @Tracto;
END