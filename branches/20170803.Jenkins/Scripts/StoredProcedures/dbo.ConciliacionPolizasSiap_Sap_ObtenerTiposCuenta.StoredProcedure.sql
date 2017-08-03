USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[[ConciliacionPolizasSiap_Sap_ObtenerTiposCuenta]]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ConciliacionPolizasSiap_Sap_ObtenerTiposCuenta]
GO
/****** Object:  StoredProcedure [dbo].[[ConciliacionPolizasSiap_Sap_ObtenerTiposCuenta]]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Daniel Benitez
-- Create date: 15/12/2015
-- Description:	Obtiene los tipos de cuenta a poder consultar en la conciliacion de polizas SIAP vs SAP
-- ConciliacionPolizasSiap_Sap_ObtenerTiposCuenta
--======================================================
CREATE PROCEDURE [dbo].[ConciliacionPolizasSiap_Sap_ObtenerTiposCuenta]
AS
BEGIN
	SELECT Descripcion, Prefijo FROM tipoCuentaConciliacion where Activo = 1
END

GO
