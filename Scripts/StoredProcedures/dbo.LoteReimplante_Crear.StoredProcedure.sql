USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_Crear]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteReimplante_Crear
--======================================================
CREATE PROCEDURE [dbo].[LoteReimplante_Crear]
@LoteProyeccionID int,
@NumeroReimplante int,
@FechaProyectada smalldatetime,
@PesoProyectado int,
@FechaReal smalldatetime,
@PesoReal int,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT LoteReimplante (		
		LoteProyeccionID,
		NumeroReimplante,
		FechaProyectada,
		PesoProyectado,
		FechaReal,
		PesoReal,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@LoteProyeccionID,		
		@NumeroReimplante,
		@FechaProyectada,
		@PesoProyectado,
		CASE WHEN CONVERT(CHAR(8), @FechaReal, 112) = '19000101'
			  THEN NULL
			  ELSE @FechaReal END,
		@PesoReal,
		@UsuarioCreacionID,
		GETDATE()
	)
	SET NOCOUNT OFF;
END

GO
