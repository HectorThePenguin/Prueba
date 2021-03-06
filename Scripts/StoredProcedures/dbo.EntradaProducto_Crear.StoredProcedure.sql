USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <02/06/2014>
-- Description:	Guarda la entrada de un producto.
/*EntradaProducto_Crear'
	<ROOT>
		<EntradaProducto>
			<TipoFolio></TipoFolio>
			<ContratoID>12</ContratoID>
			<OrganizacionID>1</OrganizacionID>
			<TipoContratoID><TipoContratoID>
			<ProductoID>111</ProductoID>
			<RegistroVigilanciaID>1</RegistroVigilanciaID>
			<Observaciones><Observaciones>
			<OperadorIDAnalista><OperadorIDAnalista>
			<EstatusID></EstatusID>
			<UsuarioCreacionID></UsuarioCreacionID>
		</EntradaProducto>
	</ROOT>
'*/
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_Crear]
	@XMLEntrada XML 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @OrganizacionID INT;
	DECLARE @TipoFolio INT;
	DECLARE @EntradaProducto INT;
	DECLARE @Folio INT;
	SET @OrganizacionID = 0;
	SET @TipoFolio = 0;
	SET @Folio = 0;

	SELECT TOP 1 @TipoFolio = M.TipoFolio,
				 @OrganizacionID = M.OrganizacionID
	FROM (SELECT 
				TipoFolio = T.item.value('./TipoFolio[1]', 'INT'),
				OrganizacionID   = T.item.value('./OrganizacionID[1]','INT')
			FROM  @XMLEntrada.nodes('ROOT/EntradaProducto') AS T(item)) M;


	EXEC Folio_Obtener @OrganizacionID,@TipoFolio,@Folio OUTPUT;

   INSERT INTO EntradaProducto 
	(PesoBonificacion,ContratoID,TipoContratoID,OrganizacionID,ProductoID,RegistroVigilanciaID,
	Folio,Fecha,OperadorIDAnalista,EstatusID,Activo,FechaCreacion,UsuarioCreacionID,
	Observaciones,FechaEmbarque,
			FolioOrigen)
	(SELECT 0,CASE WHEN M.ContratoID > 0 THEN M.ContratoID ELSE NULL END ,
	        CASE WHEN M.TipoContratoID > 0 THEN M.TipoContratoID ELSE NULL END ,
			M.OrganizacionID,
			M.ProductoID,
			M.RegistroVigilanciaID,
			@Folio,
			GETDATE(),
			CASE WHEN M.OperadorIDAnalista > 0 THEN M.OperadorIDAnalista ELSE NULL END ,
			M.EstatusID,
			1,
			GETDATE(),
			M.UsuarioCreacionID,
			M.Observaciones,
			CASE  WHEN CAST(M.FechaEmbarque AS DATE) < CAST(GETDATE()-19999 AS DATE) THEN NULL   ELSE M.FechaEmbarque END,
			CASE  WHEN M.FolioOrigen = 0 THEN NULL   ELSE M.FolioOrigen END
	 FROM 
		(SELECT 
			ContratoID = T.item.value('./ContratoID[1]', 'INT'),
			OrganizacionID   = T.item.value('./OrganizacionID[1]','INT'),
			TipoContratoID   = T.item.value('./TipoContratoID[1]','INT'),
			ProductoID   = T.item.value('./ProductoID[1]','INT'),
			RegistroVigilanciaID   = T.item.value('./RegistroVigilanciaID[1]','INT'),
			Observaciones   = T.item.value('./Observaciones[1]','VARCHAR(255)'),
			OperadorIDAnalista   = T.item.value('./OperadorIDAnalista[1]','INT'),
			EstatusID   = T.item.value('./EstatusID[1]','INT'),
			UsuarioCreacionID    = T.item.value('./UsuarioCreacionID[1]', 'INT'),
			FechaEmbarque    = T.item.value('./FechaEmbarque[1]', 'DATETIME') ,
			FolioOrigen    = T.item.value('./FolioOrigen[1]', 'INT') 
		FROM  @XMLEntrada.nodes('ROOT/EntradaProducto') AS T(item)) M
        )

	SET @EntradaProducto = @@IDENTITY

	SELECT
		EntradaProductoID,
		ContratoID,
		OrganizacionID,
		ProductoID,
		Folio,
		Fecha,
		FechaDestara,
		Observaciones,
		OperadorIDAnalista,
		PesoOrigen,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoContratoID,
		EstatusID,
		Justificacion,
		OperadorIDBascula,
		OperadorIDAlmacen,
		OperadorIDAutoriza,
		FechaInicioDescarga,
		FechaFinDescarga,
		AlmacenInventarioLoteID,
		AlmacenMovimientoID,
		Activo,
		RegistroVigilanciaID,
		FechaCreacion,
		UsuarioCreacionID
	FROM EntradaProducto (NOLOCK)
	WHERE EntradaProductoID = @EntradaProducto
END

GO
