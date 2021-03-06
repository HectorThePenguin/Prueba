USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 07/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Enfermeria_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Enfermeria_Actualizar]
@EnfermeriaID INT,
@OrganizacionID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE Enfermeria
		SET OrganizacionID = @OrganizacionID
			, Descripcion = @Descripcion
			, Activo = @Activo
			, FechaModificacion = GETDATE()
			, UsuarioModificacionID = @UsuarioModificacionID
		WHERE EnfermeriaID = @EnfermeriaID
	SET NOCOUNT OFF;
END

GO
