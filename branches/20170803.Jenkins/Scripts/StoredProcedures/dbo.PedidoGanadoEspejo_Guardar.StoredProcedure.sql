USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanadoEspejo_Guardar]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanadoEspejo_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanadoEspejo_Guardar]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Guardar pedido de ganado ESPEJO
-- Origen: APInterfaces
-- PedidoGanadoEspejo_Guardar 1,1,1,1,1,1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanadoEspejo_Guardar]
	@OrganizacionID INT, 
	@CabezasPromedio INT,
	@FechaInicio SMALLDATETIME,
	@Lunes INT,
	@Martes INT,
	@Miercoles INT,
	@Jueves INT,
	@Viernes INT,
	@Sabado INT,
	@Domingo INT,
	@UsuarioCreacionID INT,
	@UsuarioSolicitanteID INT,
	@Justificacion VARCHAR(255),
	@Estatus BIT,
	@PedidoGanadoID INT
AS
  BEGIN
      SET NOCOUNT ON
    IF NOT EXISTS( SELECT PedidoGanadoEspejoID FROM PedidoGanadoEspejo WHERE OrganizacionID = @OrganizacionID AND Activo = 1)
	BEGIN
	INSERT INTO PedidoGanadoEspejo (OrganizacionID, 
							  CabezasPromedio,
							  FechaInicio,
							  Lunes,
							  Martes,
							  Miercoles,
							  Jueves,
							  Viernes,
							  Sabado,
							  Domingo,
							  FechaCreacion, 
							  UsuarioCreacionID,
							  UsuarioSolicitanteID,
							  Justificacion,
							  Estatus,
							  PedidoGanadoID) 
	VALUES (@OrganizacionID, 
			@CabezasPromedio,
			@FechaInicio,
			@Lunes,
			@Martes,
			@Miercoles,
			@Jueves,
			@Viernes,
			@Sabado,
			@Domingo,
			GETDATE(), 
			@UsuarioCreacionID,
			@UsuarioSolicitanteID,
			@Justificacion,
			@Estatus,
			@PedidoGanadoID)
			
			SELECT PedidoGanadoEspejoID,
			OrganizacionID, 
			CabezasPromedio,
			FechaInicio,
			Lunes,
			Martes,
			Miercoles,
			Jueves,
			Viernes,
			Sabado,
			Domingo,
			FechaCreacion, 
			UsuarioCreacionID,
			UsuarioSolicitanteID,
			UsuarioAproboID,
			Justificacion,
			Estatus,
			Activo,
			PedidoGanadoID
	FROM PedidoGanadoEspejo 
	WHERE PedidoGanadoEspejoID = @@IDENTITY
	END
	ELSE
	BEGIN
		SELECT 0 PedidoGanadoEspejoID,
			0 OrganizacionID, 
			0 CabezasPromedio,
			GETDATE() AS FechaInicio,
			0 Lunes,
			0 Martes,
			0 Miercoles,
			0 Jueves,
			0 Viernes,
			0 Sabado,
			0 Domingo,
			0 UsuarioAproboID,
			'0' Justificacion,
			CAST(0 AS BIT) Estatus,
			CAST(0 AS BIT) Activo,
			0 PedidoGanadoID,
			0 UsuarioSolicitanteID
	END
			
	
	SET NOCOUNT OFF
  END