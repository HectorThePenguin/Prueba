IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MuertesEnTransitoVenta_ValidarFolio]') AND type in (N'P', N'PC'))
BEGIN
    DROP PROCEDURE MuertesEnTransitoVenta_ValidarFolio;
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================s
-- Author     : Daniel Ricardo Benitez Carrillo
-- Create date: 2016/12/20
-- Description: Valida si existen folios registrados con muertes en transito
-- MuertesEnTransitoVenta_ValidarFolio 4148, 1, '<ROOT><Aretes><Arete><ID>48405000617885</ID></Arete><Arete><ID>48402508151193</ID></Arete></Aretes></ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[MuertesEnTransitoVenta_ValidarFolio]
@FolioEntrada INT,
@OrganizacionID INT,
@AretesXML XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Total INT
	DECLARE @RegistroCondicionMuerte BIT
	DECLARE @FolioConMuertesRegistradas BIT
	DECLARE @EstatusLote BIT
	DECLARE @Cabezas INT
	DECLARE @EntradaGanadoID INT
	DECLARE @AretesRegistrados VARCHAR(300)
	DECLARE @AretesActivos VARCHAR(300)
	
	---------------
	DECLARE @Aretes AS TABLE (
		Arete VARCHAR(20)
	)
	INSERT @Aretes (Arete)
	SELECT Arete = t.item.value('ID[1]', 'VARCHAR(20)')
	FROM @AretesXML.nodes('ROOT/Aretes/Arete') AS T(item)
	-------------
	
	SELECT @EntradaGanadoID = EG.EntradaGanadoID 
	FROM EntradaGanado EG
	WHERE EG.FolioEntrada = @FolioEntrada AND EG.OrganizacionID = @OrganizacionID
	
	SELECT @Total = COUNT(ec.EntradaCondicionID)
	FROM EntradaGanado eg
	INNER JOIN EntradaCondicion ec ON eg.EntradaGanadoID = ec.EntradaGanadoID
	WHERE eg.FolioEntrada = @FolioEntrada AND eg.OrganizacionID = @OrganizacionID and ec.Cabezas > 0 and ec.CondicionId NOT IN (1, 5)
	
	IF @Total > 0 set @RegistroCondicionMuerte = 1
	
	SELECT @Total = count(EG.EntradaGanadoID) FROM EntradaGanado EG INNER JOIN EntradaGanadoMuerte EGM ON EG.EntradaGanadoID = EGM.EntradaGanadoID
	WHERE EG.FolioEntrada = @FolioEntrada AND EG.OrganizacionID = @OrganizacionID
	
	IF @Total > 0 SET @FolioConMuertesRegistradas = 1
	
	SELECT @EstatusLote = L.Activo, @Cabezas = L.Cabezas 
	FROM EntradaGanado EG INNER JOIN Lote L ON EG.LoteID = L.LoteID
	WHERE EG.FolioEntrada = @FolioEntrada AND EG.OrganizacionID = @OrganizacionID
	
	set @AretesRegistrados = ( 
	SELECT CAST(','+EGM.Arete as VARCHAR(300)) FROM EntradaGanadoMuerte EGM INNER JOIN @Aretes AR ON EGM.Arete = AR.Arete
	WHERE EGM.EntradaGanadoID = @EntradaGanadoID
	FOR XML PATH(''))
	
	set @AretesActivos = ( 
	SELECT CAST(','+A.Arete as VARCHAR(300)) FROM Animal A INNER JOIN @Aretes AR ON A.Arete = AR.Arete 
	WHERE A.Activo = 1
	FOR XML PATH(''))
	
	SELECT COALESCE(@RegistroCondicionMuerte,0) RegistroCondicionMuerte,
	COALESCE(@FolioConMuertesRegistradas,0) FolioConMuertesRegistradas,
	COALESCE(@EstatusLote,0) EstatusLote,
	COALESCE(@Cabezas,0) Cabezas,
	COALESCE(@AretesRegistrados,'') AretesRegistrados,
	COALESCE(@AretesActivos,'') AretesActivos

	SET NOCOUNT OFF;
END

