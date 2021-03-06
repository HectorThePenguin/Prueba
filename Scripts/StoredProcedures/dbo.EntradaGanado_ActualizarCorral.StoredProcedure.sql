USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ActualizarCorral]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Pedro.Delgado
-- Fecha: 08/09/2014
-- Origen: APInterfaces
-- Descripci�n:	Actualiza el corralid de una entrada de ganado por lote
-- EXEC EntradaGanado_ActualizarCorral 1, 1,1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ActualizarCorral]
@LoteID INT,
@CorralID INT,
@UsuarioModificacionID INT
AS
BEGIN
	UPDATE EntradaGanado 
		SET CorralID = @CorralID,
				UsuarioModificacionID = @UsuarioModificacionID,
				FechaModificacion = GETDATE()
	WHERE LoteID = @LoteID AND Activo = 1
	SELECT 
		EntradaGanadoID,
		FolioEntrada,
		EG.OrganizacionID,
		OrganizacionOrigenID,
		FechaEntrada,
		EmbarqueID,
		FolioOrigen,
		FechaSalida,
		CamionID,
		ChoferID,
		JaulaID,
		CabezasOrigen,
		CabezasRecibidas,
		OperadorID,
		PesoBruto,
		PesoTara,
		EsRuteo,
		Fleje,
		CheckList,
		CorralID,
		LoteID,
		Observacion,
		ImpresionTicket,
		Costeado,
		Manejado,
		Guia,
		Factura,
		Poliza,
		HojaEmbarque,
		ManejoSinEstres,
		CabezasMuertas,
		EG.Activo,
		EG.FechaCreacion,
		EG.UsuarioCreacionID,
		EG.FechaModificacion,
		EG.UsuarioModificacionID,
		O.TipoOrganizacionID
	FROM EntradaGanado EG
	INNER JOIN Organizacion O ON (EG.OrganizacionID = O.OrganizacionID)
	WHERE LoteID = @LoteID AND CorralID = @CorralID AND EG.Activo = 1
END

GO
