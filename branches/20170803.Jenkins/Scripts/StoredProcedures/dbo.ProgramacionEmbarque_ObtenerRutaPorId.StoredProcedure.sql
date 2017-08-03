USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRutaPorId]    Script Date: 27/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRutaPorId]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRutaPorId]    Script Date: 27/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 27-06-2017
-- Description: sp para regresar las Rutas dependiendo del Id seleccionado en la pestaña Transporte
-- SpName     : ProgramacionEmbarque_ObtenerRutaPorId 35,1,5, 1
--======================================================  
CREATE PROCEDURE ProgramacionEmbarque_ObtenerRutaPorId
@ConfiguracionEmbarqueDetalleID INT,
@OrigenID INT,
@DestinoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CED.ConfiguracionEmbarqueDetalleID,
		CED.Descripcion,
		CED.Kilometros,
		CED.Horas
	FROM ConfiguracionEmbarque AS CE (NOLOCK)
	INNER JOIN ConfiguracionEmbarqueDetalle AS CED (NOLOCK) ON CE.ConfiguracionEmbarqueID = CED.ConfiguracionEmbarqueId
	WHERE CE.Activo = @Activo AND CED.ConfiguracionEmbarqueDetalleID = @ConfiguracionEmbarqueDetalleID	AND (CE.OrganizacionOrigenID = @OrigenID AND CE.OrganizacionDestinoID = @DestinoID)
	SET NOCOUNT OFF;
END