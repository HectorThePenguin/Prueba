USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarDisponibilidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarDisponibilidad]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarDisponibilidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 12/02/2014
-- Description:  Actualizala Fecha de Disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarDisponibilidad] 		
	@UsuarioID INT
	,@XmlLotes XML
AS
DECLARE @Lotes AS TABLE (
	LoteID INT
	,FechaDisponibilidad DATE
	,DisponibilidadManual BIT
	)
INSERT INTO @Lotes
SELECT t.item.value('./LoteID[1]', 'INT') AS LoteID	
	,t.item.value('./FechaDisponibilidad[1]', 'date') AS FechaDisponibilidad
	,t.item.value('./DisponibilidadManual[1]', 'BIT') AS DisponibilidadManual
FROM @XmlLotes.nodes('ROOT/Lotes') AS T(item)
BEGIN
	SET NOCOUNT ON;	
	update @Lotes set FechaDisponibilidad = NULL
	where  FechaDisponibilidad = '00010101'
	UPDATE lt
	SET FechaDisponibilidad = lo.FechaDisponibilidad
		,DisponibilidadManual = lo.DisponibilidadManual
		,UsuarioModificacionID = @UsuarioID
		,FechaModificacion = GETDATE()
	FROM Lote lt
	INNER JOIN @Lotes lo ON lt.LoteID = lo.LoteID
	SET NOCOUNT OFF;
END

GO
