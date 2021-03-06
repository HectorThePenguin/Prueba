USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPartidaCompraDirecta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPartidaCompraDirecta]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPartidaCompraDirecta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cesar.valdez
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripción:	Metodo para obtener una partida de compra directa de una lista de partidas para un lote
-- EXEC EntradaGanado_ObtenerPartidaCompraDirecta 
-- '<ROOT><Partidas><NoPartida>1237 </NoPartida></Partidas><Partidas><NoPartida> 1240</NoPartida></Partidas></ROOT>', 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPartidaCompraDirecta]
	@NoPartida XML,
	@LoteID INT
AS
BEGIN

	DECLARE @Partidas AS TABLE ([NoPartida] INT)
	DECLARE @FolioEntrada INT, @OrganizacionID INT
		  , @CabezasOrigen INT, @CabezasAnimal INT

	INSERT @Partidas ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
		FROM @NoPartida.nodes('ROOT/Partidas') AS T(item)

		SELECT @FolioEntrada	= EG.FolioEntrada
			 , @OrganizacionID	= EG.OrganizacionID
			 , @CabezasOrigen	= EG.CabezasOrigen
		FROM EntradaGanado EG
		INNER JOIN Organizacion OO 
			ON EG.OrganizacionOrigenID = OO.OrganizacionID
		INNER JOIN TipoOrganizacion TP 
			ON TP.TipoOrganizacionID = OO.TipoOrganizacionID
				AND TP.TipoOrganizacionID = 3
		INNER JOIN @Partidas P
				ON (EG.FolioEntrada = P.NoPartida)
		WHERE EG.LoteID = @LoteID

		SET @CabezasAnimal = (SELECT COUNT(AnimalID) FROM Animal(NOLOCK) WHERE FolioEntrada = @FolioEntrada AND OrganizacionIDEntrada = @OrganizacionID)

		SET @CabezasAnimal += (SELECT COUNT(AnimalID) FROM AnimalHistorico (NOLOCK) WHERE FolioEntrada = @FolioEntrada AND OrganizacionIDEntrada = @OrganizacionID)

		IF (@CabezasOrigen = @CabezasAnimal)
		BEGIN
			SET @FolioEntrada = 0
		END
		
	SELECT COALESCE(@FolioEntrada,0)	
END

GO
