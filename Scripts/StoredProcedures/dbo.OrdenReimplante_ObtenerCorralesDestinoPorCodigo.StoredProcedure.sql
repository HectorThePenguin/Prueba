USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenReimplante_ObtenerCorralesDestinoPorCodigo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenReimplante_ObtenerCorralesDestinoPorCodigo]
GO
/****** Object:  StoredProcedure [dbo].[OrdenReimplante_ObtenerCorralesDestinoPorCodigo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Noel.Gerardo
-- Fecha: 23-12-2013
-- Descripcion:	Obtiene la lista de corrales destino para orden de reimplante
-- OrdenReimplante_ObtenerCorralesDestinoPorCodigo 1
-- =============================================
CREATE PROCEDURE [dbo].[OrdenReimplante_ObtenerCorralesDestinoPorCodigo]
	@OrganizacionID INT,
	@Codigo VARCHAR(10)
AS
BEGIN	
	DECLARE @DIASRANGO INT;
	SET @DIASRANGO = 7;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY CR.Codigo ASC) AS RowNum,
		CR.CorralID,
		CR.OrganizacionID,
		o.Descripcion as [Organizacion],
		CR.Codigo,
		CR.TipoCorralID,
		t.Descripcion as [TipoCorral],
		CR.Capacidad,
		CR.MetrosLargo,
		CR.MetrosAncho,
		CR.Seccion,
		CR.Orden,
		CR.Activo,
		CR.FechaCreacion,
		CR.UsuarioCreacionID,
		CR.FechaModificacion,
		CR.UsuarioModificacionID
	-- INTO #Corral 
	 FROM Corral CR
	INNER JOIN Organizacion o on o.OrganizacionID = cr.OrganizacionId
	INNER JOIN TipoCorral t on t.TipoCorralID = cr.TipoCorralID
    WHERE t.GrupoCorralID = 2 
      AND CR.OrganizacionID = @OrganizacionID 
 	  AND CR.Codigo = @Codigo
      AND CR.Activo = 1
      AND NOT EXISTS(SELECT CorralID 
					   FROM CorralRango 
				      WHERE CorralID = CR.CorralID 
					    AND OrganizacionID = CR.OrganizacionID
  					    AND Activo = 1)
	 AND (NOT EXISTS(SELECT CorralID 
					   FROM Lote 
					  WHERE CorralID = CR.CorralID 
					    AND Cabezas >=  CR.Capacidad
					    AND OrganizacionID = CR.OrganizacionID
					    AND Activo = 1) 
					OR 
		      EXISTS(SELECT C.CorralID
					   FROM LoteProyeccion (NOLOCK) LP
					  INNER JOIN Lote (NOLOCK) LT ON (LP.LoteID = LT.LoteID AND LP.OrganizacionId = LT.OrganizacionId)
					  INNER JOIN Corral (NOLOCK) C ON (LT.CorralId = C.CorralId AND LT.OrganizacionId = C.OrganizacionId)
					  INNER JOIN LoteReimplante (NOLOCK) LR ON LR.LoteProyeccionID = LP.LoteProyeccionID
					  WHERE LT.FechaCierre IS NOT NULL AND LT.Activo = 1 --validar que el lote este cerrado, teniendo fecha de cierre y este activo
					    AND LT.OrganizacionID = CR.OrganizacionID
					    AND ( LR.FechaProyectada > GETDATE() AND 
								LR.FechaProyectada <= DATEADD(DAY, @DIASRANGO, GETDATE()))
					    AND NOT EXISTS(SELECT LoteId 
										 FROM ProgramacionReimplante 
									    WHERE LP.LoteId = LoteId 
										  AND OrganizacionID = LT.OrganizacionID
										  AND Activo = 1)
					    AND C.Codigo = @Codigo
					    AND C.CorralID = CR.CorralID
					)
			)
END

GO
