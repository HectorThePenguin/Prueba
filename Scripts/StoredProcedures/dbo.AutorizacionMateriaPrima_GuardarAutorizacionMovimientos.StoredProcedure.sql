USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_GuardarAutorizacionMovimientos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_GuardarAutorizacionMovimientos]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_GuardarAutorizacionMovimientos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author: Emir Lezama
-- Create date: 10/12/2014
-- Description:  Actualiza el estatus de las solicitudes Autorizada o Rechazada
-- Origen: APInterfaces
-- ================================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_GuardarAutorizacionMovimientos]
 @XmlSolicitudes XML,
 @OrganizacionID INT,
 @TipoAutorizacionID INT,
 @UsuarioModificaID INT,
 @EstatusPendienteID INT
AS
BEGIN
	SET NOCOUNT ON;
	/* Se crea tabla temporal para almacenar el XML */
	 DECLARE @AutorizacionMovimientosTemp AS TABLE
	(
		AutorizacionMateriaPrimaID INT,
		EstatusID INT,
		Observaciones VARCHAR(255)
	)
	/* Se llena tabla temporal con info del XML */
	INSERT @AutorizacionMovimientosTemp(
			AutorizacionMateriaPrimaID,
			EstatusID,
			Observaciones
		)
	SELECT 
		AutorizacionMateriaPrimaID    = T.item.value('./AutorizacionID[1]', 'INT'),
		EstatusID  = T.item.value('./EstatusID[1]', 'INT'),
		Observaciones  = T.item.value('./Observaciones[1]', 'VARCHAR(255)')
	FROM  @XmlSolicitudes.nodes('ROOT/AutorizacionMovimientos') AS T(item)
    /* Se actualiza el estatus de las solicitudes */
	UPDATE AMP 
	SET AMP.FechaModificacion = GETDATE(), AMP.UsuarioModificacionID = @UsuarioModificaID, 
	AMP.Observaciones=Temp.Observaciones, AMP.EstatusID=TEMP.EstatusID
	FROM AutorizacionMateriaPrima AS AMP INNER JOIN @AutorizacionMovimientosTemp AS Temp
	ON AMP.AutorizacionMateriaPrimaID = Temp.AutorizacionMateriaPrimaID AND AMP.OrganizacionID = @OrganizacionID 
	AND AMP.EstatusID = @EstatusPendienteID AND AMP.TipoAutorizacionID = @TipoAutorizacionID
	SET NOCOUNT OFF;
END

GO
