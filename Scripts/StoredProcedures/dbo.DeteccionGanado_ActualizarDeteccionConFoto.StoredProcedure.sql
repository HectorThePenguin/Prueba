USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ActualizarDeteccionConFoto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ActualizarDeteccionConFoto]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ActualizarDeteccionConFoto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Andres Vejar
-- Create date: 12/07/2014
-- Origen: APInterfaces
-- Description:	Actualiza el numero de arete detectado a la entrada en enfermeria por el numero de foto
-- execute DeteccionGanado_ActualizarDeteccionConFoto '1816457','foto'
-- =============================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ActualizarDeteccionConFoto]
	@DeteccionId INT,
	@Arete VARCHAR(15),
	@FotoDeteccion VARCHAR(250)
AS
BEGIN	
SET NOCOUNT ON;	
	UPDATE Deteccion 
	SET Arete = @Arete ,
	    FechaModificacion = GETDATE()
	WHERE DeteccionId = @DeteccionId
SET NOCOUNT OFF;	
END

GO
