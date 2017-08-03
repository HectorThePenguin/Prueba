USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_Guardar]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanado_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_Guardar]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Guardar pedido de ganado
-- Origen: APInterfaces
-- PedidoGanado_Guardar 1,1,1,1,1,1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanado_Guardar]
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
	@UsuarioCreacionID INT
AS
  BEGIN
      SET NOCOUNT ON
	INSERT INTO PedidoGanado (OrganizacionID, 
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
							  UsuarioCreacionID) 
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
			@UsuarioCreacionID)
			
	SELECT OrganizacionID, 
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
			PedidoGanadoID
	FROM PedidoGanado 
	WHERE PedidoGanadoID = @@IDENTITY
	SET NOCOUNT OFF
  END