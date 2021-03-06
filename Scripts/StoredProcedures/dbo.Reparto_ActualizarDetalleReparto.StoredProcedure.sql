USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ActualizarDetalleReparto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ActualizarDetalleReparto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ActualizarDetalleReparto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-04-01
-- Origen: APInterfaces
-- Description:	Obtiene un el detalle de un reparto
-- EXEC Reparto_ActualizarDetalleReparto 1, '<ROOT>
--  <RepartoDetalle>
--    <FormulaIDServida>1</FormulaIDServida>
--    <CantidadServida>0</CantidadServida>
--    <HoraReparto>05:57</HoraReparto>
--    <CamionRepartoID>      1 </CamionRepartoID>
--    <UsuarioModificacionID>5</UsuarioModificacionID>
--    <RepartoID>1</RepartoID>
--    <TipoServicioID>1</TipoServicioID>
--  </RepartoDetalle>
--</ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ActualizarDetalleReparto]
	@Servido BIT,
	@XmlRepartoDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TablaDetalleReparto TABLE (
		FormulaIDServida  INT,
		CantidadServida INT,
		HoraReparto VARCHAR(5),
		CamionRepartoID  INT,
		UsuarioModificacionID INT,
		RepartoID BIGINT,
		TipoServicioID INT
	)
	INSERT INTO @TablaDetalleReparto
	SELECT 
			FormulaIDServida  = T.item.value('./FormulaIDServida[1]', 'INT'),
			CantidadServida  = T.item.value('./CantidadServida[1]', 'INT'),
			HoraReparto    = T.item.value('./HoraReparto[1]', 'VARCHAR(5)'),
			CamionRepartoID  = T.item.value('./CamionRepartoID[1]', 'INT'),
			UsuarioModificacionID = T.item.value('./UsuarioModificacionID[1]','INT'),
			RepartoID    = T.item.value('./RepartoID[1]', 'BIGINT'),
			TipoServicioID = T.item.value('./TipoServicioID[1]', 'INT')
	FROM  @XmlRepartoDetalle.nodes('ROOT/RepartoDetalle') AS T(item)
	UPDATE RD
    SET RD.FormulaIDServida = Tmp.FormulaIDServida,
		RD.CantidadServida = Tmp.CantidadServida,
		RD.HoraReparto = Tmp.HoraReparto,
		RD.Servido = @Servido,
		RD.CamionRepartoID = Tmp.CamionRepartoID,
		RD.FechaModificacion = GETDATE(),
		RD.UsuarioModificacionID = Tmp.UsuarioModificacionID
	FROM RepartoDetalle RD
	INNER JOIN @TablaDetalleReparto Tmp ON (RD.RepartoID = Tmp.RepartoID AND RD.TipoServicioID = Tmp.TipoServicioID)
	WHERE RD.Servido <> @Servido
	SET NOCOUNT OFF;
END

GO
