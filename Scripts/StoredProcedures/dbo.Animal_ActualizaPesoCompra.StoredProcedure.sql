USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActualizaPesoCompra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ActualizaPesoCompra]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActualizaPesoCompra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar Valdez
-- Create date: 2014/12/16
-- Description: SP para Actualizar Peso Compra
-- Origen     : APInterfaces
-- EXEC Animal_ActualizaPesoCompra 40536,300,5
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ActualizaPesoCompra]
	@AnimalID INT,
	@PesoCompra INT,
	@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Animal 
	   SET PesoCompra = @PesoCompra,
		   UsuarioModificacionID = @UsuarioModificacionID, 
		   FechaModificacion = GETDATE()
	WHERE AnimalID = @AnimalID
END

GO
