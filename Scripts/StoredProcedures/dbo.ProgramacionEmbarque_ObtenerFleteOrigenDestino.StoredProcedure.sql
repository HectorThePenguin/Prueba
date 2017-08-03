USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerFleteOrigenDestino]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerFleteOrigenDestino]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerFleteOrigenDestino]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 14-06-2017
-- Description: Procedimiento que valida que exista tarifa para un origen y un destino
-- SpName     : ProgramacionEmbarque_ObtenerFleteOrigenDestino 14853
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerFleteOrigenDestino]
@OrganizacionOrigenID INT,
@OrganizacionDestinoID INT
AS
BEGIN
	SELECT cemb.ConfiguracionEmbarqueID
	FROM ConfiguracionEmbarque cemb (NOLOCK)
	INNER JOIN ConfiguracionEmbarqueDetalle cembdet (NOLOCK) ON (cemb.ConfiguracionEmbarqueID = cembdet.ConfiguracionEmbarqueID)
	INNER JOIN EmbarqueTarifa emtar (NOLOCK) ON (cembdet.ConfiguracionEmbarqueDetalleID = emtar.ConfiguracionEmbarqueDetalleID)
	WHERE cemb.OrganizacionOrigenID = @OrganizacionOrigenID AND cemb.OrganizacionDestinoID = @OrganizacionDestinoID
	AND cemb.Activo = 1 AND cembdet.Activo = 1 AND emtar.Activo = 1
END