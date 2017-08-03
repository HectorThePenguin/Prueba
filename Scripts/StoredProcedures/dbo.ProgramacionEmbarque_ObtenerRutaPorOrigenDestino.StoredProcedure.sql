USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRutaPorOrigenDestino]    Script Date: 21/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRutaPorOrigenDestino]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRutaPorOrigenDestino]    Script Date: 21/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 21-06-2017
-- Description: sp para regresar las Rutas dependiendo del Origen y Destino seleccionado en la pestaña programación
-- SpName     : ProgramacionEmbarque_ObtenerRutaPorOrigenDestino 74, 82, 1
--======================================================  
CREATE PROCEDURE ProgramacionEmbarque_ObtenerRutaPorOrigenDestino
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
	WHERE CE.Activo = @Activo AND (CE.OrganizacionOrigenID = @OrigenID AND CE.OrganizacionDestinoID = @DestinoID)	
	SET NOCOUNT OFF;
END