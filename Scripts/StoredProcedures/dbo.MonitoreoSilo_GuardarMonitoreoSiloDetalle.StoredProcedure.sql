USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MonitoreoSilo_GuardarMonitoreoSiloDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MonitoreoSilo_GuardarMonitoreoSiloDetalle]
GO
/****** Object:  StoredProcedure [dbo].[MonitoreoSilo_GuardarMonitoreoSiloDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author: Emir Lezama
-- Create date: 13/11/2014
-- Description:  Almacena el detalle de la captura de temperaturas en el Monitoreo de Silos
-- Origen: APInterfaces
-- ================================================
CREATE PROCEDURE [dbo].[MonitoreoSilo_GuardarMonitoreoSiloDetalle]
 @XmlGuardarDetalleMonitoreoSilo XML,
 @Temperatura DECIMAL(10,2),
 @SiloDescripcion VARCHAR(255),
 @ProductoID INT,
 @HR DECIMAL(10,2),
 @Observaciones VARCHAR(255),
 @UsuarioCreacionID INT,
 @OrganizacionID INT,
 @Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IdentityID BIGINT
	DECLARE @SiloID INT
	SELECT @SiloID = SiloID FROM Silo WHERE Descripcion = @SiloDescripcion;
	INSERT INTO MonitoreoSilo
	(
		FechaMonitoreo,
		TemperaturaAmbiente,
		SiloID,
		ProductoID,
		HR,
		Observaciones,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES
	(
		GETDATE(),
		@Temperatura,
		@SiloID,
		@ProductoID,
		@HR,
		@Observaciones,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	);
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY)
	/* Se crea tabla temporal para almacenar el XML */
	 DECLARE @MonitoreoSiloDetalleTemp AS TABLE
	(
		TemperaturaCelda INT,
		AlturaSilo INT,
		UbicacionSensor INT
	)
	/* Se llena tabla temporal con info del XML */
	INSERT @MonitoreoSiloDetalleTemp(
			TemperaturaCelda,
			AlturaSilo,
			UbicacionSensor
		)
	SELECT 
		TemperaturaCelda    = T.item.value('./TemperaturaCelda[1]', 'INT'),
		AlturaSilo  = T.item.value('./AlturaSilo[1]', 'INT'),
		UbicacionSensor  = T.item.value('./UbicacionSensor[1]', 'INT')
	FROM  @XmlGuardarDetalleMonitoreoSilo.nodes('ROOT/MonitoreoSilos') AS T(item)
    /* Se insertan los costos en el AlmacenMovimientoDetalle */
	INSERT INTO MonitoreoSiloDetalle
	(
		MonitoreoSiloID,
		MonitoreoSiloIndicadorID,
		Temperatura,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	SELECT
		@IdentityID,
		ms.MonitoreoSiloIndicadorID,/*MonitoreoSiloIndicadorID, NO DEFINIERON DE DONDE SE VA A OBTENER*/
		m.TemperaturaCelda,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	FROM  @MonitoreoSiloDetalleTemp AS m
	INNER JOIN MonitoreoSiloIndicador AS ms
	ON ms.OrganizacionID=@OrganizacionID AND m.AlturaSilo=ms.AlturaSilo AND m.UbicacionSensor=ms.UbicacionSensor 
	WHERE ms.Activo = @Activo;
	SET NOCOUNT OFF;
END

GO
