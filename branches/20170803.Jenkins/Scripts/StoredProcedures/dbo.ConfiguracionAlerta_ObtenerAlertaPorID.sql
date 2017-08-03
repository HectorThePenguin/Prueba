USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlerta_ObtenerAlertaPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAlerta_ObtenerAlertaPorID]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlerta_ObtenerAlertaPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Valenzuela Rivera Juan Diego 
-- Create date: 15/03/2016
-- Description: Obtiene un registro de Alerta por medio de su ID
-- =============================================  
CREATE PROCEDURE [dbo].[ConfiguracionAlerta_ObtenerAlertaPorID]
	@IDAlerta INT
AS
BEGIN
	SET NOCOUNT ON;  
	SELECT 
		Descripcion,
		A.AlertaID
	FROM
		Alerta A
INNER JOIN AlertaConfiguracion AC 	ON A.AlertaID = AC.AlertaID 
	WHERE
		A.AlertaID = @IDAlerta AND
		A.Activo = 1;
	SET NOCOUNT OFF;
END