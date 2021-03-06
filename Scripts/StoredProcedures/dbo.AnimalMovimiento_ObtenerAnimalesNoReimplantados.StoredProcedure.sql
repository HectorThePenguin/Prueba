USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantados]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/07/21
-- Description: SP para Obtener Animales no reimplantados
-- Origen     : APInterfaces
-- EXEC AnimalMovimiento_ObtenerAnimalesNoReimplantados 2, 7992
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerAnimalesNoReimplantados]
	@OrganizacionID INT,
	@LoteID INT
AS
BEGIN
	DECLARE @CorralIDDestino INT;
	DECLARE @LoteIDDestino INT;
	
	/* Se obtiene el corral destino */
	if @OrganizacionID = 1
	SELECT @CorralIDDestino = C.CorralID, @LoteIDDestino = L.LoteID
	  FROM Corral C 
	 INNER JOIN Lote L ON L.CorralID = C.CorralID AND L.Activo = 1
	 WHERE C.Codigo = 'R99' 
	   AND C.OrganizacionID = @OrganizacionID

	   else
	   SELECT @CorralIDDestino = C.CorralID, @LoteIDDestino = L.LoteID
	  FROM Corral C 
	 INNER JOIN Lote L ON L.CorralID = C.CorralID AND L.Activo = 1
	 WHERE C.Codigo = 'R99' 
	   AND C.OrganizacionID = @OrganizacionID

	   if @OrganizacionID = 4
	SELECT @CorralIDDestino = C.CorralID, @LoteIDDestino = L.LoteID
	  FROM Corral C 
	 INNER JOIN Lote L ON L.CorralID = C.CorralID AND L.Activo = 1
	 WHERE C.Codigo = 'RR99' 
	   AND C.OrganizacionID = @OrganizacionID

	SELECT AM.AnimalID ,
		   AM.AnimalMovimientoID ,
		   AM.OrganizacionID ,
		   ISNULL(@CorralIDDestino,0) AS CorralID,
		   ISNULL(@LoteIDDestino,0) AS LoteID,
		   AM.FechaMovimiento ,
		   AM.Peso ,
		   AM.Temperatura ,
		   AM.TipoMovimientoID ,
		   AM.TrampaID ,
		   AM.OperadorID ,
		   AM.Observaciones ,
		   AM.Activo ,
		   AM.FechaCreacion ,
		   AM.UsuarioCreacionID ,
		   AM.FechaModificacion ,
		   AM.UsuarioModificacionID ,
		   AM.LoteID LoteIDOrigen,
		   AM.CorralID CorralIDOrigen,
		   AM.AnimalMovimientoIDAnterior
	  FROM Animal A(NOLOCK)
	 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	 WHERE A.Activo = 1
	   AND AM.Activo = 1
	   AND AM.LoteID = @LoteID 
	   AND AM.OrganizacionID = @OrganizacionID
	   AND AM.TipoMovimientoID != 6
	   
END
GO
