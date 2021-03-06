USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiaria_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiaria_Crear
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiaria_Crear]
@Turno int,
@LitrosInicial decimal(14,2),
@LitrosFinal decimal(14,2),
@HorometroInicial int,
@HorometroFinal int,
@FechaProduccion smalldatetime,
@UsuarioIDAutorizo int,
@Observaciones varchar(255),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ProduccionDiaria (
		Turno,
		LitrosInicial,
		LitrosFinal,
		HorometroInicial,
		HorometroFinal,
		FechaProduccion,
		UsuarioIDAutorizo,
		Observaciones,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Turno,
		@LitrosInicial,
		@LitrosFinal,
		@HorometroInicial,
		@HorometroFinal,
		@FechaProduccion,
		@UsuarioIDAutorizo,
		@Observaciones,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
