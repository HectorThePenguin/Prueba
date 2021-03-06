USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorHostName]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_ObtenerPorHostName]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorHostName]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : C�sar Valdez
-- Create date: 22/05/2014
-- Description: Obtener trampa por HostName
-- Origen     : APInterfaces
-- SpName     : Trampa_ObtenerPorHostName ''
--======================================================
CREATE PROCEDURE [dbo].[Trampa_ObtenerPorHostName]
	@HostName varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TrampaID,
		Descripcion,
		OrganizacionID,
		TipoTrampa,
		HostName,
		Activo
	FROM Trampa
	WHERE HostName = @HostName
	SET NOCOUNT OFF;
END

GO
