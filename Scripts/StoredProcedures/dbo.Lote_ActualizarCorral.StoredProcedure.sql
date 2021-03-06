USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarCorral]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Pedro.Delgado
-- Fecha: 08/09/2014
-- Origen: APInterfaces
-- Descripci�n:	Actualiza el corralid de un lote
-- EXEC Lote_ActualizarCorral 1, 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarCorral]
@LoteID INT,
@CorralID INT,
@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Lote
		SET CorralID = @CorralID,
				UsuarioModificacionID = @UsuarioModificacionID,
				FechaModificacion = GETDATE()
	WHERE LoteID = @LoteID
	SELECT 
		LoteID,
		OrganizacionID,
		CorralID,
		Lote,
		TipoCorralID,
		TipoProcesoID,
		FechaInicio,
		CabezasInicio,
		FechaCierre,
		Cabezas,
		FechaDisponibilidad,
		DisponibilidadManual,
		Activo,
		FechaSalida,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM Lote
	WHERE LoteID = @LoteID AND
				CorralID = @CorralID
END

GO
