USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionGanado_GuardarSupervision]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionGanado_GuardarSupervision]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionGanado_GuardarSupervision]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Ramses Santos
-- Create date: 17/02/2014
-- Description:  Guardar el detalle del Ganado Detectado
-- SupervisionGanado_GuardarSupervision
-- ===============================================================
CREATE PROCEDURE [dbo].[SupervisionGanado_GuardarSupervision] @XmlSupervisionGanado XML
	,@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @LoteID AS INT
	DECLARE @SupervisionGanadoTemp AS TABLE (
		[SupervisionGanadoID] INT
		,[LoteID] INT
		,[CodigoCorral] VARCHAR(10)
		,[Arete] VARCHAR(15) DEFAULT ''
		,[AreteMetalico] VARCHAR(15) DEFAULT ''
		,[FechaDeteccion] DATETIME
		,[ConceptoDeteccionID] INT
		,[Acuerdo] VARCHAR(150)
		,[Notificacion] INT
		,[Activo] INT
		,[FotoSupervision] VARCHAR(250)
		)
	INSERT INTO @SupervisionGanadoTemp (
		[SupervisionGanadoID]
		,[LoteID]
		,[CodigoCorral]
		,[Arete]
		,[AreteMetalico]
		,[FechaDeteccion]
		,[ConceptoDeteccionID]
		,[Acuerdo]
		,[Notificacion]
		,[Activo]
		,[FotoSupervision]
		)
	SELECT SupervisionGanadoID = t.item.value('./SupervisionGanadoID[1]', 'INT')
		,LoteID = t.item.value('./LoteID[1]', 'INT')
		,CodigoCorral = t.item.value('./CodigoCorral[1]', 'VARCHAR(10)')
		,Arete = t.item.value('./Arete[1]', 'VARCHAR(15)')
		,AreteMetalico = t.item.value('./AreteMetalico[1]', 'VARCHAR(15)')
		,FechaDeteccion = t.item.value('./FechaDeteccion[1]', 'DATETIME')
		,ConceptoDeteccionID = t.item.value('./ConceptoDeteccionID[1]', 'INT')
		,Acuerdo = t.item.value('./Acuerdo[1]', 'VARCHAR(150)')
		,Notificacion = t.item.value('./Notificacion[1]', 'INT')
		,Activo = t.item.value('./Activo[1]', 'INT')
		,FotoSupervision = t.item.value('./FotoSupervision[1]', 'VARCHAR(250)')
	FROM @XmlSupervisionGanado.nodes('ROOT/Detectados') AS T(item)
	INSERT INTO SupervisionGanado (
		[LoteID]
		,[Arete]
		,[AreteMetalico]
		,[FechaDeteccion]
		,[ConceptoDeteccionID]
		,[Acuerdo]
		,[Notificacion]
		,[Activo]
		,[FechaCreacion]
		,[UsuarioCreacionID]
		,[FechaModificacion]
		,[UsuarioModificacionID]
		,[FotoSupervision]
		)
	SELECT L.LoteID
		,CASE 
			WHEN (
					SG.Arete = ''
					AND SG.AreteMetalico <> ''
					)
				THEN dbo.ObtenerAretePorTestigo(SG.AreteMetalico)
			ELSE SG.Arete
			END
		,CASE 
			WHEN (
					SG.AreteMetalico = ''
					AND SG.Arete <> ''
					)
				THEN dbo.ObtenerTestigoPorArete(SG.Arete)
			ELSE SG.AreteMetalico
			END
		,SG.FechaDeteccion
		,SG.ConceptoDeteccionID
		,SG.Acuerdo
		,1
		,1
		,GETDATE()
		,1
		,GETDATE()
		,1
		,SG.FotoSupervision
	FROM Lote AS L
	INNER JOIN Corral AS C ON (L.CorralID = C.CorralID)
	INNER JOIN @SupervisionGanadoTemp AS SG ON (C.Codigo = SG.CodigoCorral)
	WHERE C.OrganizacionID = @OrganizacionID
		AND L.Activo = 1
		AND C.Activo = 1
	SET NOCOUNT OFF;
END

GO
