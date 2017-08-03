USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_ActualizaMarca]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Marcas_ActualizaMarca]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_ActualizaMarca]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 23-05-2017
-- Description: Procedimiento almacenado que actualiza la información de una marca.
-- SpName     : Marcas_ActualizaMarca 1, 'MARCA MODIFICADA', 1, 1, 1
--======================================================  
CREATE PROCEDURE [dbo].[Marcas_ActualizaMarca]
	@MarcaId INT,
	@Descripcion VARCHAR(255),
	@Activo BIT,
	@Tracto BIT,
	@UsuarioModificacionId INT
AS
BEGIN
	UPDATE Marca 
	SET	Descripcion = @Descripcion, 
		Activo = @Activo, 
		Tracto = @Tracto, 
		UsuarioModificacionId = @UsuarioModificacionId, 
		FechaModificacion = getdate()
	WHERE MarcaID = @MarcaId
	
	SELECT MarcaID, Descripcion, Activo, Tracto, UsuarioCreacionId, FechaCreacion, UsuarioModificacionId, FechaModificacion 
	FROM Marca
	WHERE MarcaID = @MarcaId
END