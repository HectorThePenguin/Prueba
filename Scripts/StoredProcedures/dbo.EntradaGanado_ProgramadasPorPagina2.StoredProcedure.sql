USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ProgramadasPorPagina2]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ProgramadasPorPagina2]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ProgramadasPorPagina2]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Roque.Solis
-- Fecha: 29-01-2014
-- Descripción:	Obtiene entradas de ganado evaluadas por paginas 
-- EntradaGanado_ProgramadasPorPagina2 1, 1, 0, 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ProgramadasPorPagina2]
	@FolioEntrada INT, 
	@OrganizacionID INT,
	@Inicio INT,
	@Limite INT
AS
BEGIN
	if @FolioEntrada < 0
	BEGIN
		SET @FolioEntrada = NULL
	END

	DECLARE @tmpEntradaGanado AS TABLE (
		RowNum INT IDENTITY,
		EntradaGanadoID INT,
		FolioEntrada INT,
		OrganizacionID INT,
		Organizacion VARCHAR(50),
		TipoOrganizacionID INT, 
		TipoOrganizacion VARCHAR(50),
		OrganizacionOrigenID INT,
		FechaEntrada SMALLDATETIME,
		EmbarqueID INT,
		FolioOrigen INT,
		FechaSalida SMALLDATETIME,
		CamionID INT,
		ChoferID INT,
		JaulaID INT,
		CabezasOrigen INT,
		CabezasRecibidas INT,
		OperadorID INT,
		PesoBruto DECIMAL(18,2),
		PesoTara DECIMAL(18,2),
		EsRuteo BIT,
		Fleje BIT,
		CheckList CHAR(10),
		CorralID INT,
		Corral CHAR(10),
		LoteID INT,
		Lote VARCHAR(20),
		Observacion VARCHAR(400),
		ImpresionTicket BIT,
		Costeado BIT,
		Manejado BIT,
		Activo BIT,
		FechaEvaluacion SMALLDATETIME,
		EsMetafilaxia BIT,
		PesoOrigen INT,
		PesoLlegada INT,
		NivelGarrapata INT)

	INSERT INTO @tmpEntradaGanado (EntradaGanadoID,FolioEntrada,OrganizacionID,Organizacion,TipoOrganizacionID, 
				TipoOrganizacion,OrganizacionOrigenID,FechaEntrada,EmbarqueID,FolioOrigen,FechaSalida,CamionID,
				ChoferID,JaulaID,CabezasOrigen,CabezasRecibidas,OperadorID,PesoBruto,PesoTara,EsRuteo,
				Fleje,CheckList,CorralID,Corral,LoteID,Lote,Observacion,ImpresionTicket,Costeado,
				Manejado,Activo,FechaEvaluacion,EsMetafilaxia,PesoOrigen,PesoLlegada,NivelGarrapata)
			EXEC EntradaGanado_Programadas2 @FolioEntrada, @OrganizacionID;

	SELECT RowNum, EntradaGanadoID,FolioEntrada,OrganizacionID,Organizacion,TipoOrganizacionID, 
			TipoOrganizacion,OrganizacionOrigenID,FechaEntrada,EmbarqueID,FolioOrigen,FechaSalida,CamionID,
			ChoferID,JaulaID,CabezasOrigen,CabezasRecibidas,OperadorID,PesoBruto,PesoTara,EsRuteo,
			Fleje,CheckList,CorralID,Corral,LoteID,Lote,Observacion,ImpresionTicket,Costeado,
			Manejado,Activo,FechaEvaluacion,EsMetafilaxia,PesoOrigen,PesoLlegada,NivelGarrapata
	  FROM @tmpEntradaGanado
	 WHERE RowNum BETWEEN @Inicio AND @Limite
	
	SELECT COUNT(RowNum) AS TotalReg
	  FROM @tmpEntradaGanado;
		
		 	
END

GO
