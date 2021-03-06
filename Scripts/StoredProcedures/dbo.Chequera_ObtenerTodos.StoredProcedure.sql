USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chequera_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Sergio Alberto Gamez Gomez
-- Create date: 17/06/2015
-- Description:  Obtener listado de Chequeras
-- Nota: Los estatus entre la chequera de SIAP y CENTROS son diferentes
-- Chequera_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[Chequera_ObtenerTodos]  
AS    
BEGIN    
	SET NOCOUNT ON;
	SELECT   
		ChequeraID = C.ChequeraID,  
		DivisionDescripcion = ISNULL(D.Descripcion,''),  
		CentroAcopioID = C.OrganizacionId,  
		CentroAcopioDescripcion = UPPER(O.Descripcion),  
		NumeroChequera = C.NumeroChequera,  
		BancoID = B.BancoID,  
		BancoDescripcion = UPPER(B.Descripcion),  
		EstatusID = CAST(C.Activo  AS INT),  
		EstatusDescripcion = CASE WHEN C.Activo = 0 THEN 'DISPONIBLE'   
			  WHEN C.Activo = 1 THEN 'ACTIVO'   
			  WHEN C.Activo = 2 THEN 'CERRADO'   
			  WHEN C.Activo = 3 THEN 'CANCELADO'   
			ELSE '' END,  
		DivisionID = ISNULL(D.OrganizacionId,0)    
	FROM [Sukarne].[dbo].[Chequera] C (NOLOCK)  
	INNER JOIN Organizacion O (NOLOCK)  
		ON C.OrganizacionId = O.OrganizacionId  
	INNER JOIN Banco B (NOLOCK)  
		ON B.BancoId = C.BancoId  
	LEFT OUTER JOIN Organizacion D (NOLOCK)  
		ON D.Division = O.Division AND D.TipoOrganizacionId = 2
	SET NOCOUNT OFF;    
END
GO