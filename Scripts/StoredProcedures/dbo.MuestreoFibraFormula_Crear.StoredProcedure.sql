USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuestreoFibraFormula_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuestreoFibraFormula_Crear]
GO
/****** Object:  StoredProcedure [dbo].[MuestreoFibraFormula_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Alejandro Quiroz
-- Create date: 27/08/2014
-- Description: Inserta una lista de Muestras Fribra Formulas.
/* MuestreoFibraFormula_Crear '
	<ROOT>
		<Muestra>
			<OrganizacionID>1</OrganizacionID>
			<FormulaID>1</FormulaID>
			<PesoInicial>1</PesoInicial>
			<Grande>1</Grande>
			<PesoFibraGrande>1</PesoFibraGrande>
			<Mediana>1</Mediana>
			<PesoFibraMediana>1</PesoFibraMediana>
			<FinoTamiz>1</FinoTamiz>
			<PesoFinoTamiz>1</PesoFinoTamiz>
			<FinoBase>1</FinoBase>
			<PesoFinoBase>1</PesoFinoBase>
			<Origen>origen</Origen>
			<Activo>1</Activo>
			<UsuarioID>1</UsuarioID>
		</Muestra>
	</ROOT>'
*/
-- =============================================
CREATE PROCEDURE [dbo].[MuestreoFibraFormula_Crear]
	@MuestrasXML XML
AS
  BEGIN
	CREATE TABLE #MuestrasFormulas (	
		[OrganizacionID] [int],
		[FechaMuestreo] [smalldatetime],
		[FormulaID] [int],
		[PesoInicial] [decimal](15, 2),
		[Grande] [bit],
		[PesoFibraGrande] [decimal](15, 2),
		[Mediana] [bit],
		[PesoFibraMediano] [decimal](15, 2),
		[FinoTamiz] [bit],
		[PesoFinoTamiz] [decimal](15, 2),
		[FinoBase] [bit],
		[PesoFinoBase] [decimal](15, 2),
		[Origen] [varchar](100),
		[Activo] [bit],
		[FechaCreacion] [datetime],
		[UsuarioCreacionID] [int]
	)
	INSERT INTO #MuestrasFormulas
	SELECT T.ITEM.value('./OrganizacionID[1]','INT') AS [OrganizacionID]
		  ,GETDATE() AS [FechaMuestreo]
		  ,T.ITEM.value('./FormulaID[1]','INT') AS [FormulaID]
		  ,T.ITEM.value('./PesoInicial[1]','DECIMAL(15,2)') AS [PesoInicial]
		  ,T.ITEM.value('./Grande[1]','BIT') AS [Grande]
		  ,T.ITEM.value('./PesoFibraGrande[1]','DECIMAL(15,2)') AS [PesoFibraGrande]
		  ,T.ITEM.value('./Mediana[1]','BIT') AS [Mediana]
		  ,T.ITEM.value('./PesoFibraMediana[1]','DECIMAL(15,2)') AS [PesoFibraMediano]
		  ,T.ITEM.value('./FinoTamiz[1]','BIT') AS [FinoTamiz]
		  ,T.ITEM.value('./PesoFinoTamiz[1]','DECIMAL(15,2)') AS [PesoFinoTamiz]
		  ,T.ITEM.value('./FinoBase[1]','BIT') AS [FinoBase]
		  ,T.ITEM.value('./PesoFinoBase[1]','DECIMAL(15,2)') AS [PesoFinoBase]
		  ,T.ITEM.value('./Origen[1]','VARCHAR(100)') AS [Origen]
		  ,1 AS [Activo]
		  ,GETDATE() AS [FechaCreacion]
		  ,T.ITEM.value('./UsuarioID[1]','INT') AS [UsuarioCreacionID]
	FROM @MuestrasXML.nodes('/ROOT/Muestra') as T(ITEM)
	INSERT INTO [dbo].[MuestreoFibraFormula]
           ([OrganizacionID]
           ,[FechaMuestreo]
           ,[FormulaID]
           ,[PesoInicial]
           ,[Grande]
           ,[PesoFibraGrande]
           ,[Mediana]
           ,[PesoFibraMediano]
           ,[FinoTamiz]
           ,[PesoFinoTamiz]
           ,[FinoBase]
           ,[PesoFinoBase]
           ,[Origen]
           ,[Activo]
           ,[FechaCreacion]
           ,[UsuarioCreacionID])
	SELECT  [OrganizacionID]
           ,[FechaMuestreo]
           ,[FormulaID]
           ,[PesoInicial]
           ,[Grande]
           ,[PesoFibraGrande]
           ,[Mediana]
           ,[PesoFibraMediano]
           ,[FinoTamiz]
           ,[PesoFinoTamiz]
           ,[FinoBase]
           ,[PesoFinoBase]
           ,[Origen]
           ,[Activo]
           ,[FechaCreacion]
           ,[UsuarioCreacionID]
	FROM #MuestrasFormulas
  END

GO
