USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ValidaReimplante]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ValidaReimplante]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ValidaReimplante]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Fecha: 06-02-2014
-- Descripci�n:	Verifica si el arete ya fue reimplantado este dia
-- EXEC ReimplanteGanado_ValidaReimplante 4, '48400406522752',6
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ValidaReimplante]
	@OrganizacionID INT,
	@Arete VARCHAR(15),
	@MovimientoReimplante INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT COUNT(1) Reimplantado
	  FROM  Animal A(NOLOCK)
	 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID 
     /* LEFT JOIN ProgramacionReimplante PR(NOLOCK) ON PR.LoteID = AM.LoteID AND PR.Activo = 1*/
	 WHERE A.Arete = @Arete
	   AND A.OrganizacionIDEntrada = @OrganizacionID
	   AND AM.TipoMovimientoID = @MovimientoReimplante
	   AND CONVERT(CHAR(8), AM.FechaMovimiento, 112) = CONVERT(CHAR(8), GETDATE(), 112)
	   AND AM.Activo = 1
	   AND A.Activo = 1;
SET NOCOUNT OFF;	
END

GO
