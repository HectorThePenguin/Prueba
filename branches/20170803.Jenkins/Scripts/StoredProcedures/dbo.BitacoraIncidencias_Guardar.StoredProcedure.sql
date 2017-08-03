USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BitacoraIncidencias_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[BitacoraIncidencias_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[BitacoraIncidencias_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 30/03/2016
-- Origen     : Apinterfaces
-- Description: Inserta un registro en la tabla Bitacora incidencias
-- SpName     : BitacoraIncidencias_Guardar
--======================================================
CREATE PROCEDURE [dbo].[BitacoraIncidencias_Guardar]
@AlertaID INT,
@Folio  INT,
@OrganizacionID INT,
@Error VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO BitacoraIncidencia(
            AlertaID,
            Folio,
            Fecha,
            OrganizacionID,
						Error)
	VALUES(
		@AlertaID,
		@Folio,
		GETDATE(),
		@OrganizacionID,
		@Error
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END
