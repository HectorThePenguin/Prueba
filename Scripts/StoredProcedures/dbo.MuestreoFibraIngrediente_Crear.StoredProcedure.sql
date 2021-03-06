USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuestreoFibraIngrediente_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuestreoFibraIngrediente_Crear]
GO
/****** Object:  StoredProcedure [dbo].[MuestreoFibraIngrediente_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Alejandro Quiroz
-- Create date: 27/08/2014
-- Description: Inserta una lista de Muestras Fribra Ingredientes.
/* MuestreoFibraIngrediente_Crear '
	<ROOT>
		<Muestra>
			<OrganizacionID>1</OrganizacionID>
			<ProductoID>10</ProductoID>
			<PesoMuestra>1</PesoMuestra>
			<Grueso>1</Grueso>
			<PesoGranoGrueso>1</PesoGranoGrueso>
			<Mediano>1</Mediano>
			<PesoGranoMediano>1</PesoGranoMediano>
			<Fino>1</Fino>
			<PesoGranoFino>1</PesoGranoFino>
			<CribaEntrada>1</CribaEntrada>
			<CribaSalida>1</CribaSalida>
			<Observaciones>obs</Observaciones>
			<UsuarioID>1</UsuarioID>
		</Muestra>
	</ROOT>'
*/
-- =============================================
CREATE PROCEDURE [dbo].[MuestreoFibraIngrediente_Crear]
	@MuestrasXML XML
AS
  BEGIN
	CREATE TABLE #MuestrasIngredientes (		
			[OrganizacionID] INT
           ,[FechaMuestreo] SMALLDATETIME
           ,[ProductoID] INT
           ,[PesoMuestra] DECIMAL(14,2)
           ,[Grueso] BIT
           ,[PesoGranoGrueso] DECIMAL(15,2)
           ,[Mediano] BIT
           ,[PesoGranoMediano]  DECIMAL(15,2)
           ,[Fino] BIT
           ,[PesoGranoFino]  DECIMAL(15,2)
           ,[CribaEntrada]  INT
           ,[CribaSalida] INT
           ,[Observaciones] VARCHAR(255)
           ,[Activo] BIT
           ,[FechaCreacion] DATETIME
           ,[UsuarioCreacionID] INT
	)
	INSERT INTO #MuestrasIngredientes
	SELECT T.ITEM.value('./OrganizacionID[1]','INT') AS [OrganizacionID]
		  ,GETDATE() AS [FechaMuestreo]
		  ,T.ITEM.value('./ProductoID[1]','INT') AS [ProductoID]
		  ,T.ITEM.value('./PesoMuestra[1]','DECIMAL(14,2)') AS [PesoMuestra]
		  ,T.ITEM.value('./Grueso[1]','BIT') AS [Grueso]
		  ,T.ITEM.value('./PesoGranoGrueso[1]','DECIMAL(15,2)') AS [PesoGranoGrueso]
		  ,T.ITEM.value('./Mediano[1]','BIT') AS [Mediano]
		  ,T.ITEM.value('./PesoGranoMediano[1]','DECIMAL(15,2)') AS [PesoGranoMediano]
		  ,T.ITEM.value('./Fino[1]','BIT') AS [Fino]
		  ,T.ITEM.value('./PesoGranoFino[1]','DECIMAL(15,2)') AS [PesoGranoFino]
		  ,T.ITEM.value('./CribaEntrada[1]','INT') AS [CribaEntrada]
		  ,T.ITEM.value('./CribaSalida[1]','INT') AS [CribaSalida]
		  ,T.ITEM.value('./Observaciones[1]','VARCHAR(255)') AS [Observaciones]
		  ,1 AS [Activo]
		  ,GETDATE() AS [FechaCreacion]
		  ,T.ITEM.value('./UsuarioID[1]','INT') AS [UsuarioCreacionID]
	FROM @MuestrasXML.nodes('/ROOT/Muestra') as T(ITEM)
	INSERT INTO [dbo].[MuestreoFibraIngrediente]
           ([OrganizacionID]
           ,[FechaMuestreo]
           ,[ProductoID]
           ,[PesoMuestra]
           ,[Grueso]
           ,[PesoGranoGrueso]
           ,[Mediano]
           ,[PesoGranoMediano]
           ,[Fino]
           ,[PesoGranoFino]
           ,[CribaEntrada]
           ,[CribaSalida]
           ,[Observaciones]
           ,[Activo]
           ,[FechaCreacion]
           ,[UsuarioCreacionID])
     SELECT [OrganizacionID]
           ,[FechaMuestreo]
           ,[ProductoID]
           ,[PesoMuestra]
           ,[Grueso]
           ,[PesoGranoGrueso]
           ,[Mediano]
           ,[PesoGranoMediano]
           ,[Fino]
           ,[PesoGranoFino]
           ,[CribaEntrada]
           ,[CribaSalida]
           ,[Observaciones]
           ,[Activo]
           ,[FechaCreacion]
           ,[UsuarioCreacionID]
	FROM #MuestrasIngredientes
  END

GO
