USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SIAP_InicializarTablasTransaccionales]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SIAP_InicializarTablasTransaccionales]
GO
/****** Object:  StoredProcedure [dbo].[SIAP_InicializarTablasTransaccionales]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez
-- Create date: 04/07/2014
-- Description:  Procedimiento para Inicializar Tablas Transaccionales
-- SIAP_InicializarTablasTransaccionales 1
-- =============================================
CREATE PROCEDURE [dbo].[SIAP_InicializarTablasTransaccionales] 
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	/*  Inicializar Muertes */
	-- TRUNCATE TABLE Muertes;
	-- DBCC CHECKIDENT(N'[dbo].[Muertes]', RESEED, 0);
	/*  Inicializar Proyeccion */
	-- TRUNCATE TABLE LoteReimplante;
	-- DELETE FROM LoteProyeccion;
	-- DBCC CHECKIDENT(N'[dbo].[LoteReimplante]', RESEED, 0);
	-- DBCC CHECKIDENT(N'[dbo].[LoteProyeccion]', RESEED, 0);
	/*  Inicializar ChecklistCorral */
	-- TRUNCATE TABLE CheckListCorral;
	-- DBCC CHECKIDENT(N'[dbo].[CheckListCorral]', RESEED, 0);
	/*  Inicializar Deteccion */
	 TRUNCATE TABLE DeteccionSintoma;
	 DELETE FROM Deteccion;
	 DBCC CHECKIDENT(N'[dbo].[DeteccionSintoma]', RESEED, 1);
	 DBCC CHECKIDENT(N'[dbo].[Deteccion]', RESEED, 1);
	 TRUNCATE TABLE DeteccionSintomaAnimal;
	 DELETE FROM DeteccionAnimal;
	 DBCC CHECKIDENT(N'[dbo].[DeteccionSintomaAnimal]', RESEED, 1);
	 DBCC CHECKIDENT(N'[dbo].[DeteccionAnimal]', RESEED, 1);
	TRUNCATE TABLE VentaGanadoDetalle;
	DELETE FROM VentaGanado;
	DBCC CHECKIDENT(N'[dbo].[VentaGanadoDetalle]', RESEED, 1);
	DBCC CHECKIDENT(N'[dbo].[VentaGanado]', RESEED, 1);
	/*  Inicializar Evaluaciones */
	-- TRUNCATE TABLE EvaluacionCorralDetalle;
	-- DELETE FROM EvaluacionCorral;
	-- DBCC CHECKIDENT(N'[dbo].[EvaluacionCorralDetalle]', RESEED, 0);
	-- DBCC CHECKIDENT(N'[dbo].[EvaluacionCorral]', RESEED, 0);
	/*  Inicializar Reparto */
	TRUNCATE TABLE RepartoDetalle;
	DELETE FROM Reparto;
	DBCC CHECKIDENT(N'[dbo].[RepartoDetalle]', RESEED, 1);
	DBCC CHECKIDENT(N'[dbo].[Reparto]', RESEED, 1);
	/* Inicializar Entrada de Ganado */
	/*
	TRUNCATE TABLE EntradaGanadoCosto;
	TRUNCATE TABLE EntradaGanadoCalidad;
	TRUNCATE TABLE EntradaCondicion;
	TRUNCATE TABLE EntradaDetalle;
	-- TRUNCATE TABLE EntradaGanadoCosteo;  -- **
	DELETE FROM EntradaGanadoCosteo;
	TRUNCATE TABLE ProgramacionCorte;
	-- TRUNCATE TABLE EntradaGanado; -- **
	DELETE FROM EntradaGanado; -- **
	DBCC CHECKIDENT(N'[dbo].[EntradaGanadoCosto]', RESEED, 0);
	DBCC CHECKIDENT(N'[dbo].[EntradaGanadoCalidad]', RESEED, 0);
	DBCC CHECKIDENT(N'[dbo].[EntradaCondicion]', RESEED, 0);
	DBCC CHECKIDENT(N'[dbo].[EntradaDetalle]', RESEED, 0);
	DBCC CHECKIDENT(N'[dbo].[EntradaGanadoCosteo]', RESEED, 0);
	DBCC CHECKIDENT(N'[dbo].[ProgramacionCorte]', RESEED, 0);
	DBCC CHECKIDENT(N'[dbo].[EntradaGanado]', RESEED, 0);
	*/
	/* Inicializar tablas del Inventario */
	TRUNCATE TABLE AnimalCosto;
	TRUNCATE TABLE AnimalMovimiento;
	DELETE FROM Animal;
	DBCC CHECKIDENT(N'[dbo].[Animal]', RESEED, 1);
	DBCC CHECKIDENT(N'[dbo].[AnimalMovimiento]', RESEED, 1);
	DBCC CHECKIDENT(N'[dbo].[AnimalCosto]', RESEED, 1);
	/*  Inicializar Lote */
	UPDATE L
	SET L.Activo = 0, 
	    UsuarioModificacionID = 1, 
	    FechaModificacion = GETDATE()
	  FROM Lote L
	 WHERE NOT EXISTS(
				SELECT EG.LoteID
				  FROM EntradaGanado EG
				 WHERE EG.LoteID = L.LoteID
			)
	AND Activo = 1
	AND OrganizacionID = @OrganizacionID;
	-- DBCC CHECKIDENT(N'[dbo].[Lote]', RESEED, 0);
END

GO
