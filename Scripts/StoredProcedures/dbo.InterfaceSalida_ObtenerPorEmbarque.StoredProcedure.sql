USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalida_ObtenerPorEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalida_ObtenerPorEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalida_ObtenerPorEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================================
-- Author:		Raul Esquer	
-- Create date: 02-01-14
-- Description:	Valida si una salida se encuentra en una entrada
-- InterfaceSalida_ObtenerPorEmbarque 4, 81, 101
-- ================================================================
CREATE PROCEDURE [dbo].[InterfaceSalida_ObtenerPorEmbarque]
	@OrganizacionID INT, 
	@OrganizacionOrigenID INT, 
	@Salida INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		count(EntradaGanadoId) 
	FROM entradaganado 
	WHERE FolioOrigen = @Salida
	AND OrganizacionOrigenID = @OrganizacionOrigenID
	AND OrganizacionID = @OrganizacionID
	AND Activo = 1
END

GO
