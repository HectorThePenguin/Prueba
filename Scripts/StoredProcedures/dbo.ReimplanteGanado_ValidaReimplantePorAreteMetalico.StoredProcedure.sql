USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ValidaReimplantePorAreteMetalico]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ValidaReimplantePorAreteMetalico]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ValidaReimplantePorAreteMetalico]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Alejandro Quiroz
-- Fecha: 29-07-2015
-- Descripción:	Verifica si el arete metalico ya fue reimplantado este dia
-- EXEC ReimplanteGanado_ValidaReimplantePorAreteMetalico 4, '48400406522752',6
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ValidaReimplantePorAreteMetalico]
	@OrganizacionID INT,
	@AreteMetalico VARCHAR(15),
	@MovimientoReimplante INT
AS
BEGIN	
SET NOCOUNT ON;	
	 SELECT COUNT(1) Reimplantado
	 FROM  Animal A(NOLOCK)
	 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	 WHERE A.AreteMetalico = @AreteMetalico
	   AND A.OrganizacionIDEntrada = @OrganizacionID
	   AND AM.TipoMovimientoID = @MovimientoReimplante
	   AND CONVERT(CHAR(8), AM.FechaMovimiento, 112) = CONVERT(CHAR(8), GETDATE(), 112)
	   AND AM.Activo = 1
	   AND A.Activo = 1;
SET NOCOUNT OFF;	
END

GO
