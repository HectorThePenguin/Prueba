USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Mezcladora_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Mezcladora_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Mezcladora_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Villarreal Medina Edgar Esteban
-- Create date: 05/11/2014
-- Description:  Mezcladora Obtener Por ID.
-- Mezcladora_ObtenerPorID 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[Mezcladora_ObtenerPorID]	
@MezcladoraID INT,
@Activo INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT mezcladoraID,
				 NumeroEconomico,
				 Descripcion,
				 Activo
	FROM Mezcladora (NOLOCK)
 WHERE mezcladoraID = 	@MezcladoraID
	AND  Activo = @Activo
	AND OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
