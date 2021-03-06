USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chequera_ObtenerPorFiltro]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================================================================
-- Author: Sergio Alberto Gamez Gomez
-- Create date: 17/06/2015
-- Description:  Obtener listado de Chequeras filtrado por n�mero chequera, estatus, centro de acopio
-- Chequera_ObtenerPorFiltro 'CLN','A00000000','0',1
-- ===================================================================================================
CREATE  PROCEDURE [dbo].[Chequera_ObtenerPorFiltro]  
	@Division VARCHAR(5),  
	@NumeroChequera Varchar(50),  
	@Estatus VARCHAR(10),  
	@CentroAcopio INT  
AS    
BEGIN    
	SET NOCOUNT ON;  
	IF @CentroAcopio = 0  
	BEGIN  
		SELECT   
			ChequeraID = C.ChequeraID,  
			DivisionDescripcion = D.Descripcion,  
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
			DivisionID = D.OrganizacionId  
		FROM [Sukarne].[dbo].[Chequera] C (NOLOCK)  
		INNER JOIN Organizacion O (NOLOCK)  
			ON C.OrganizacionId = O.OrganizacionId  
		INNER JOIN Banco B (NOLOCK)  
			ON B.BancoId = C.BancoId  
		LEFT OUTER JOIN Organizacion D (NOLOCK)  
			ON D.Division = O.Division AND D.TipoOrganizacionId = 2  
		WHERE O.Division LIKE '%'+@Division+'%'  
		AND C.NumeroChequera LIKE '%'+@NumeroChequera+'%'  
		AND CAST(C.Activo AS VARCHAR) LIKE '%'+@Estatus+'%'    
	END  
	ELSE  
	BEGIN  
		SELECT   
			ChequeraID = C.ChequeraID,  
			DivisionDescripcion = D.Descripcion,  
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
			DivisionID = D.OrganizacionId   
		FROM [Sukarne].[dbo].[Chequera] C (NOLOCK)  
		INNER JOIN Organizacion O (NOLOCK)  
			ON C.OrganizacionId = O.OrganizacionId  
		INNER JOIN Banco B (NOLOCK)  
			ON B.BancoId = C.BancoId  
		LEFT OUTER JOIN Organizacion D (NOLOCK)  
			ON D.Division = O.Division AND D.TipoOrganizacionId = 2  
		WHERE O.Division LIKE '%'+@Division+'%'  
		AND C.NumeroChequera LIKE '%'+@NumeroChequera+'%'  
		AND CAST(C.Activo AS VARCHAR) LIKE '%'+@Estatus+'%'  
		AND C.OrganizacionId = @CentroAcopio  
	END  
	SET NOCOUNT OFF;    
END

GO
