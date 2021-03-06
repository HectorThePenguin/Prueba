USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ExisteChequera]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chequera_ExisteChequera]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ExisteChequera]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================================================
-- Autor: Sergio Alberto Gamez Gomez
-- Fecha: 17-06-2015
-- Descripcion:	Consultar cuantas chequereas con determinado estatis tiene cada centro de acopio
-- Chequera_ExisteChequera 17,1
-- =============================================================================================
CREATE PROCEDURE [dbo].[Chequera_ExisteChequera]
	@CentroAcopio INT,
	@Estatus INT 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 ChequeraId
	FROM [Sukarne].[dbo].[Chequera] (NOLOCK) 
	WHERE Activo = @Estatus AND OrganizacionId = @CentroAcopio

	SET NOCOUNT OFF;  
END
GO