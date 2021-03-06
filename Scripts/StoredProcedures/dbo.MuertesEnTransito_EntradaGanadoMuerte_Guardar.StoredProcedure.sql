USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanadoMuerte_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuertesEnTransito_EntradaGanadoMuerte_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanadoMuerte_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 02/12/2014
-- Description: 
/* MuertesEnTransito_EntradaGanadoMuerte_Guardar '
	<ROOT>
	  <EntradaGanadoMuerte>
		<EntradaGanadoID>21804</EntradaGanadoID>
		<Arete>48403209083397</Arete>
		<FolioMuerte>17</FolioMuerte>
		<Activo>1</Activo>
		<Peso>320</Peso>
		<UsuarioCreacionID>2629</UsuarioCreacionID>
		<ClienteID>466</ClienteID>
		<AnimalID>1364416</AnimalID>
	  </EntradaGanadoMuerte>
	</ROOT>',
   '<ROOT>
  <EntradaGanadoMuerteDetalle>
    <EntradaGanadoID>21804</EntradaGanadoID>
    <CostoID>1</CostoID>
    <Importe>13440.0000</Importe>
    <UsuarioCreacionID>0</UsuarioCreacionID>
    <Arete>48403209083397</Arete>
  </EntradaGanadoMuerteDetalle>
  <EntradaGanadoMuerteDetalle>
    <EntradaGanadoID>21804</EntradaGanadoID>
    <CostoID>14</CostoID>
    <Importe>275.0400</Importe>
    <UsuarioCreacionID>0</UsuarioCreacionID>
    <Arete>48403209083397</Arete>
  </EntradaGanadoMuerteDetalle>
  <EntradaGanadoMuerteDetalle>
    <EntradaGanadoID>21804</EntradaGanadoID>
    <CostoID>15</CostoID>
    <Importe>7.0500</Importe>
    <UsuarioCreacionID>0</UsuarioCreacionID>
    <Arete>48403209083397</Arete>
  </EntradaGanadoMuerteDetalle>
</ROOT>'
*/

--=============================================
CREATE PROCEDURE [dbo].[MuertesEnTransito_EntradaGanadoMuerte_Guardar]
	@EntradaGanadoMuerteXML XML,
	@EntradaGanadoMuerteDetalleXML XML
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @FolioFactura VARCHAR(15)
	DECLARE @Serie VARCHAR(5)
	DECLARE @Folio VARCHAR(10)
	DECLARE @Fecha DATETIME
	DECLARE @OrganizacionID INT
	SET @Fecha = GETDATE()
	SET @OrganizacionID = 0

	CREATE TABLE #EntradaGanadoMuertesTmp (
		[EntradaGanadoID] INT
		,[Arete] VARCHAR(15)
		,[FolioMuerte] BIGINT
		,[Fecha] DATETIME
		,[Activo] BIT
		,[FechaCreacion] DATETIME
		,[UsuarioCreacionID] INT
		,Peso INT
	    ,AnimalID BIGINT
		,FolioFactura VARCHAR(15)
		,ClienteID INT
 		)

	CREATE TABLE #EntradaGanadoMuertesDetalleTmp (
		[EntradaGanadoID]	INT
		,Importe			DECIMAL(18, 2)
		,CostoID			INT
		,Arete				VARCHAR(100)
		)

	INSERT INTO #EntradaGanadoMuertesTmp
	SELECT T.ITEM.value('./EntradaGanadoID[1]', 'INT') AS [OrganizacionID]
		,T.ITEM.value('./Arete[1]', 'VARCHAR(15)') AS [ProductoID]
		,T.ITEM.value('./FolioMuerte[1]', 'BIGINT') AS [FolioMuerte]
		,@Fecha AS [Fecha]
		,T.ITEM.value('./Activo[1]', 'BIT') AS [Activo]
		,@Fecha AS [FechaCreacion]
		,T.ITEM.value('./UsuarioCreacionID[1]', 'INT') AS [UsuarioCreacionID]
		,T.ITEM.value('./Peso[1]', 'INT') AS [Peso]
		,T.ITEM.value('./AnimalID[1]', 'BIGINT') AS [AnimalID]
		,'' AS [FolioFactura]
		,T.ITEM.value('./ClienteID[1]', 'BIGINT') AS [Peso]
	FROM @EntradaGanadoMuerteXML.nodes('/ROOT/EntradaGanadoMuerte') AS T(ITEM)
	
	-- Si es de centros, obenemos su OrganizacionID diferente de 0 y preparamos el registro para facturar.
	SELECT @OrganizacionID = EG.OrganizacionID
	FROM EntradaGanado EG
	INNER JOIN Organizacion O on EG.OrganizacionOrigenID = O.OrganizacionID
	INNER JOIN TipoOrganizacion TOO on O.TipoOrganizacionID = TOO.TipoOrganizacionID
	WHERE EG.EntradaGanadoID = 2
	AND TOO.TipoOrganizacionID = (SELECT TipoOrganizacionID from TipoOrganizacion where Descripcion = 'Censtro')

	IF @OrganizacionID > 0
	BEGIN 
		-- Obtiene el numero que sigue de la factura segun el parametro configurado para cada organizacion.
		EXEC FolioFactura_Obtener @OrganizacionID, @FolioFactura OUTPUT, @Serie OUTPUT, @Folio OUTPUT
		-- Asignamos su nuevo folio de factura
		UPDATE #EntradaGanadoMuertesTmp
		SET FolioFactura = @FolioFactura
	END  
	
	

	INSERT INTO #EntradaGanadoMuertesDetalleTmp
	SELECT T.ITEM.value('./EntradaGanadoID[1]', 'INT') AS [EntradaGanadoID]
		,T.ITEM.value('./Importe[1]', 'DECIMAL(18,2)') AS [Importe]
		,T.ITEM.value('./CostoID[1]', 'INT') AS [CostoID]
		,T.ITEM.value('./Arete[1]', 'VARCHAR(100)') AS [Arete]
	FROM @EntradaGanadoMuerteDetalleXML.nodes('/ROOT/EntradaGanadoMuerteDetalle') AS T(ITEM)

	INSERT INTO [dbo].[EntradaGanadoMuerte] (
		[EntradaGanadoID]
		,[Arete]
		,[FolioMuerte]
		,[Fecha]
		,[Activo]
		,[FechaCreacion]
		,[UsuarioCreacionID]
		,Peso
		,AnimalID
		,FolioFactura
		,ClienteID
		)
	SELECT [EntradaGanadoID]
		,[Arete]
		,[FolioMuerte]
		,[Fecha]
		,[Activo]
		,[FechaCreacion]
		,[UsuarioCreacionID]
		,Peso
		,AnimalID
		,FolioFactura
		,ClienteID
	FROM #EntradaGanadoMuertesTmp

	SET NOCOUNT OFF;
END

GO
