USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Fecha_ObtenerActual]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Fecha_ObtenerActual]
GO
/****** Object:  StoredProcedure [dbo].[Fecha_ObtenerActual]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 02/06/2014
-- Description: 
-- SpName     : Fecha_ObtenerActual
--======================================================
CREATE PROCEDURE [dbo].[Fecha_ObtenerActual]
AS
BEGIN
	SELECT GETDATE() AS FechaActual
END

GO
