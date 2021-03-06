USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudPremezcla_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudPremezcla_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudPremezcla_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 16/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudPremezcla_Guardar 4,'2014-07-14 09:33:00','2014-07-14 09:33:00',1,1
--======================================================
CREATE PROCEDURE [dbo].[SolicitudPremezcla_Guardar]
@OrganizacionID INT,
@FechaInicio SMALLDATETIME,
@FechaFin SMALLDATETIME,
@UsuarioCreacionID INT,
@Activo BIT
AS
BEGIN
	DECLARE @SolicitudPremezclaID INT
	INSERT INTO SolicitudPremezcla 
	(OrganizacionID,Fecha,FechaInicio,FechaFin,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES
	(@OrganizacionID,GETDATE(),@FechaInicio,@FechaFin,@Activo,GETDATE(),@UsuarioCreacionID)
	SET @SolicitudPremezclaID = @@IDENTITY
	SELECT 
		SolicitudPremezclaID,
		OrganizacionID,
		Fecha,
		FechaInicio,
		FechaFin,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM SolicitudPremezcla (NOLOCK)
	WHERE SolicitudPremezclaID = @SolicitudPremezclaID
END

GO
