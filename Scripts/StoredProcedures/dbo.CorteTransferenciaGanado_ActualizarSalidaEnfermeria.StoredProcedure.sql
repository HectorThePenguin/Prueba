USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ActualizarSalidaEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_ActualizarSalidaEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ActualizarSalidaEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: edgar.villarreal
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripci�n:	Actualiza el estatus de animal salida
-- EXEC CorteTransferenciaGanado_ActualizarSalidaEnfermeria 15,64,6
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_ActualizarSalidaEnfermeria]
@LoteID INT,
@AnimalID INT,
@UsuarioModificacion INT
AS
BEGIN
	UPDATE AnimalSalida SET Activo = 0 , 
							FechaModificacion = GETDATE(), 
							UsuarioModificacionID = @UsuarioModificacion
	WHERE LoteID = @LoteID 
	AND AnimalID = @AnimalID
END

GO
