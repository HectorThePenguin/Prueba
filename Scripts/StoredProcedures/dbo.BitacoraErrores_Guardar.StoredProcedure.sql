USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BitacoraErrores_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[BitacoraErrores_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[BitacoraErrores_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 04/04/2014
-- Origen     : Apinterfaces
-- Description: Inserta un registro en la tabla BitacoraErrores
-- SpName     : BitacoraErrores_Guardar
--======================================================
CREATE PROCEDURE [dbo].[BitacoraErrores_Guardar]
@AccionesSiapID INT,
@Mensaje  VARCHAR(500),
@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO BitacoraErrores(
            AccionesSiapID,
            Mensaje,
            FechaCreacion,
            UsuarioCreacionID)
	VALUES(
		@AccionesSiapID,
		@Mensaje,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
