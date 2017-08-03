USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ActualizaFechaEjecucion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAcciones_ActualizaFechaEjecucion]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAcciones_ActualizaFechaEjecucion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 04/04/2016
-- Description: 
-- ConfiguracionAcciones_ActualizaFechaEjecucion SerAlerta 
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionAcciones_ActualizaFechaEjecucion]
@Codigo varchar(10),
@FechaEjecucion smalldatetime
AS
BEGIN
	SET NOCOUNT ON;
	update AccionesSIAP SET FechaEjecucion = @FechaEjecucion
	WHERE Codigo = @Codigo
	SET NOCOUNT OFF;
END

GO

