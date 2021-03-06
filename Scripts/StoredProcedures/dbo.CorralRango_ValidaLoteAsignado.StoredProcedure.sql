USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_ValidaLoteAsignado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralRango_ValidaLoteAsignado]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_ValidaLoteAsignado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CorralRango_ValidaLoteAsignado]
	@OrganizacionID INT,
	@CorralID INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT DISTINCT c.CorralID
	FROM  Corral c
	INNER JOIN ServicioAlimento sa ON c.CorralID = sa.CorralID --validar que tenga servicio de alimento
	INNER  JOIN Lote l ON c.CorralID = l.CorralID	--validar que tenga lote asignado
	WHERE c.OrganizacionID = @OrganizacionID 
	AND sa.Activo = 1 
	AND l.Activo = 1 
	AND c.CorralID = @CorralID
SET NOCOUNT OFF;	
END

GO
