USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chequera_ObtenerDetalle]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Sergio Alberto Gamez Gomez
-- Fecha: 17-06-2015
-- Descripcion:	Obtener el detalle de una chequera
-- Chequera_ObtenerDetalle 4,24
-- =============================================
CREATE PROCEDURE [dbo].[Chequera_ObtenerDetalle]   
	@ChequeraId INT,  
	@OrganizacionId INT  
AS  
BEGIN  
	SET NOCOUNT ON;

	SELECT       
		C.ChequeraId,  
		C.NumeroChequera,  
		ChequeIDInicial = C.ChequeraIDInicial,  
		ChequeIDFinal = C.ChequeraIDFinal,  
		C.OrganizacionId,  
		C.BancoId,  
		EstatusId = CAST(C.Activo AS INT),  
		C.FechaCreacion,  
		Disponibles = ((ChequeraIDFinal-ChequeraIDInicial)+1) - ISNULL(COUNT(CC.NumeroChequera), 0),  
		Girados = ISNULL(CASE WHEN CC.Estatus = 1 THEN COUNT(CC.NumeroChequera) END, 0),  
		Cancelados = ISNULL(CASE WHEN CC.Estatus = 0 THEN COUNT(CC.NumeroChequera) END, 0)
	FROM [Sukarne].[dbo].[Chequera] C (NOLOCK)
	LEFT OUTER JOIN [Sukarne].[dbo].[CacCheque] CC (NOLOCK)
		ON CAST(CC.NumeroChequera AS INT) = C.ChequeraId AND CC.OrganizacionId = C.OrganizacionId
	WHERE C.ChequeraId = @ChequeraId AND C.OrganizacionId = @OrganizacionId
	GROUP BY C.ChequeraId,  
		C.NumeroChequera,  
		C.ChequeraIDInicial,  
		C.ChequeraIDFinal,  
		C.OrganizacionId,  
		C.BancoId,  
		C.Activo,
		C.FechaCreacion,
		CC.Estatus

	SET NOCOUNT OFF;    
END
GO