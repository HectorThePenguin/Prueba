USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ValidarPreCondiciones]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ValidarPreCondiciones]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ValidarPreCondiciones]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author: Emir Lezama
-- Create date: 15/12/2014
-- Description:  Se valida que cumpla con las precondiciones necesarias
-- Origen: APInterfaces
-- ================================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ValidarPreCondiciones]
 @TipoGanaderaID INT,
 @PendienteID INT,
 @Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT TipoOrganizacionID FROM Organizacion (NOLOCK) WHERE TipoOrganizacionID = @TipoGanaderaID AND Activo = @Activo)
		IF EXISTS (SELECT TipoAutorizacionID FROM TipoAutorizacion (NOLOCK) WHERE Activo = @Activo)
			IF EXISTS (SELECT * FROM AutorizacionMateriaPrima (NOLOCK) WHERE EstatusID = @PendienteID AND Activo = @Activo)
				SELECT 1
			ELSE
				SELECT -3
		ELSE
			SELECT -2
	ELSE
		SELECT -1
	SET NOCOUNT OFF;
 END

GO
