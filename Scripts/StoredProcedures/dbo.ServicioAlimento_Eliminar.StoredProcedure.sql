USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_Eliminar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ServicioAlimento_Eliminar]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_Eliminar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: C�sar Valdez
-- Create date: 21/20/2013
-- Description: Eliminar ServicioAlimento 
-- Empresa:Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[ServicioAlimento_Eliminar]
	@CorralID INT,
	@UsuarioModificacionID INT
AS
BEGIN
 UPDATE ServicioAlimento SET Activo = 0,
	    FechaModificacion=GETDATE(),
	    UsuarioModificacionID=@UsuarioModificacionID
 WHERE CorralID = @CorralID 
   AND Activo = 1;
END

GO
