USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ValidarCorralDestinoPuntaChica]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ValidarCorralDestinoPuntaChica]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ValidarCorralDestinoPuntaChica]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Edgar Villarreal
-- Create date: 2014/12/08
-- Description: SP para validar el corral destino tiene punta chica
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ValidarCorralDestinoPuntaChica '521',2, 1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ValidarCorralDestinoPuntaChica]
	@CorralOrigen VARCHAR(10),
	@CorralDestino VARCHAR(10),
	@OrganizacionID INT
AS
BEGIN
	DECLARE @LoteIDOrigen INT;
	SELECT TOP 1 @LoteIDOrigen = LO.LoteID
	  FROM ProgramacionReimplante PROrigen
	 INNER JOIN ProgramacionReimplanteDetalle PRD ON PROrigen.FolioProgramacionID = PRD.FolioProgramacionID 
	 INNER JOIN Lote LO (NOLOCK) ON LO.LoteID = PROrigen.LoteID 
	 INNER JOIN Corral C (NOLOCK) ON LO.CorralID = C.CorralID
	 WHERE C.Codigo = @CorralOrigen
	   AND PROrigen.OrganizacionID = @OrganizacionID 
	   AND PROrigen.Activo = 1 AND C.Activo = 1
	   AND LO.Activo = 1
	   AND CONVERT(CHAR(8),PROrigen.Fecha,112) 
		   BETWEEN CONVERT(CHAR(8),GETDATE()-7,112) AND CONVERT(CHAR(8),GETDATE(),112);
	IF ( @LoteIDOrigen IS NOT NULL )
		BEGIN
			SELECT TOP 1 PRD.CorralDestinoID ,PRD.PuntaChica
			  FROM ProgramacionReimplante PR
			 INNER JOIN ProgramacionReimplanteDetalle PRD ON PR.FolioProgramacionID = PRD.FolioProgramacionID 
			 INNER JOIN Corral CRD (NOLOCK) ON PRD.CorralDestinoID = CRD.CorralID
			  LEFT JOIN Lote L (NOLOCK) ON L.CorralID = CRD.CorralID AND L.Activo = 1  /* AND L.FechaCierre IS NULL */
			 WHERE CRD.Codigo = @CorralDestino
			   AND PR.OrganizacionID = @OrganizacionID 
			   AND PR.Activo = 1 AND CRD.Activo = 1
			   AND COALESCE(L.Activo,1) = 1 AND COALESCE(L.Cabezas,0) <= CRD.Capacidad 
			   AND PR.LoteID = @LoteIDOrigen;
		END
	ELSE
		BEGIN
		    SELECT TOP 1 PRD.CorralDestinoID ,PRD.PuntaChica
			  FROM ProgramacionReimplante PR
			 INNER JOIN ProgramacionReimplanteDetalle PRD ON PR.FolioProgramacionID = PRD.FolioProgramacionID 
			 INNER JOIN Corral CRD (NOLOCK) ON PRD.CorralDestinoID = CRD.CorralID
			  LEFT JOIN Lote L (NOLOCK) ON L.CorralID = CRD.CorralID AND L.Activo = 1 /* AND L.FechaCierre IS NULL */
			 WHERE CRD.Codigo = @CorralDestino
			   AND PR.OrganizacionID = @OrganizacionID 
			   AND PR.Activo = 1 AND CRD.Activo = 1
			   AND COALESCE(L.Activo,1) = 1 AND COALESCE(L.Cabezas,0) <= CRD.Capacidad;
		END
END

GO
