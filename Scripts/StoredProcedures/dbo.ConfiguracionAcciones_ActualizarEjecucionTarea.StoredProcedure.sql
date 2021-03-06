USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ActualizarEjecucionTarea]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAcciones_ActualizarEjecucionTarea]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ActualizarEjecucionTarea]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Andres Vejar
-- Create date: 07/04/2014
-- Description: 
-- ConfiguracionAcciones_ActualizarEjecucionTarea codigo fecha
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionAcciones_ActualizarEjecucionTarea]
@Codigo varchar(10),
@Fecha smalldatetime
AS
BEGIN
	SET NOCOUNT ON;
	update AccionesSIAP SET FechaUltimaEjecucion = @Fecha
	WHERE Codigo = @Codigo
	SET NOCOUNT OFF;
END

GO
