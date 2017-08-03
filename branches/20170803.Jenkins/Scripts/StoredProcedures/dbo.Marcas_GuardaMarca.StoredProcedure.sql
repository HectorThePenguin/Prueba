USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_GuardaMarca]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Marcas_GuardaMarca]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_GuardaMarca]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 23-05-2017
-- Description: Procedimiento almacenado que guarda una nueva marca.
-- SpName     : Marcas_GuardaMarca 'Nueva Marca', 1, 1, 1
--======================================================  
CREATE PROCEDURE [dbo].[Marcas_GuardaMarca]
	@Descripcion VARCHAR(255),
	@Activo BIT,
	@Tracto BIT,
	@UsuarioCreacionId INT
AS
BEGIN
	INSERT INTO Marca (Descripcion, Activo, Tracto, UsuarioCreacionId, FechaCreacion)
	VALUES (@Descripcion, @Activo, @Tracto, @UsuarioCreacionId, getdate())

	SELECT MarcaID, Descripcion, Activo, Tracto, UsuarioCreacionId, FechaCreacion
	FROM Marca
	WHERE MarcaID = @@IDENTITY
END