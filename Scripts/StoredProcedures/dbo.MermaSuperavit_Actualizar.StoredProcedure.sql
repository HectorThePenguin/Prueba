USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MermaSuperavit_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[MermaSuperavit_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 13/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : MermaSuperavit_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[MermaSuperavit_Actualizar]
@MermaSuperavitID int,
@AlmacenID int,
@ProductoID int,
@Merma decimal(12,2),
@Superavit decimal(12,2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE MermaSuperavit SET
		AlmacenID = @AlmacenID,
		ProductoID = @ProductoID,
		Merma = @Merma,
		Superavit = @Superavit,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE MermaSuperavitID = @MermaSuperavitID
	SET NOCOUNT OFF;
END

GO
