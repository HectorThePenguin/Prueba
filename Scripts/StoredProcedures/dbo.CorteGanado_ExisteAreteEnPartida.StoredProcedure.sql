USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ExisteAreteEnPartida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ExisteAreteEnPartida]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ExisteAreteEnPartida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cesar.valdez
-- Fecha: 2013-12-17
-- Origen: APInterfaces
-- Descripción:	Obtiene La info del animal en base al arete y a la organizacion
-- EXEC CorteGanado_ExisteAreteEnPartida '1234',1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ExisteAreteEnPartida]
	@Arete VARCHAR(15), 
	@OrganizacionIDEntrada INT
AS
BEGIN
	IF( LEN(@Arete) > 0)
	BEGIN
		SELECT TOP 1 
			A.AnimalID,
			A.Arete,
			A.AreteMetalico,
			A.FechaCompra,
			A.TipoGanadoID,
			A.CalidadGanadoID,
			A.ClasificacionGanadoID,
			A.PesoCompra,
			A.OrganizacionIDEntrada,
			A.FolioEntrada,
			A.PesoLlegada,
			Paletas,
			A.CausaRechadoID,
			A.Venta,
			A.Cronico,
			A.Activo,
			A.FechaCreacion,
			A.UsuarioCreacionID,
			A.FechaModificacion,
			A.UsuarioModificacionID,
			AM.CorralID,
			C.Codigo,
			AM.TipoMovimientoID
		FROM Animal AS A(NOLOCK)
		INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID AND AM.Activo = 1
		INNER JOIN Corral C ON AM.CorralID = C.CorralID AND C.Activo = 1
		WHERE A.Arete = @Arete
		AND A.OrganizacionIDEntrada = @OrganizacionIDEntrada	
END
END

GO
