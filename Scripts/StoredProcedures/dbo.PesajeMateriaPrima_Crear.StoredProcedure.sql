USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 20/06/2014
-- Description: 
-- SpName     : PesajeMateriaPrima_Crear
-- Modificacion: 
-- 001 Se modifica sp para que se registre valor en el campo HoraPesoBruto, el cual registra la fecha y hora de la primer ocasion en que se pesa el camion
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_Crear]
@ProgramacionMateriaPrimaID INT,
@ProveedorChoferID INT,
@CamionID INT,
@PesoBruto INT,
@PesoTara INT,
@Piezas INT,
@TipoPesajeID INT,
@UsuarioIDSurtido INT,
@FechaSurtido DATETIME,
@UsuarioIDRecibe INT,
@FechaRecibe DATETIME,
@EstatusID INT,
@Activo INT,
@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Ticket INT
	SELECT @Ticket = ISNULL(MAX(PMP.Ticket) + 1,1) 
	FROM ProgramacionMateriaPrima (NOLOCK) PROMP
	INNER JOIN PedidoDetalle (NOLOCK) PD ON PROMP.PedidoDetalleID = PD.PedidoDetalleID
	INNER JOIN Pedido P (NOLOCK) ON PD.PedidoID = P.PedidoID
	INNER JOIN PesajeMateriaPrima (NOLOCK) PMP ON PROMP.ProgramacionMateriaPrimaID = PMP.ProgramacionMateriaPrimaID  
	WHERE P.PedidoID = (SELECT P.PedidoID
						FROM ProgramacionMateriaPrima (NOLOCK) PROMP
						INNER JOIN PedidoDetalle (NOLOCK) PD ON PROMP.PedidoDetalleID = PD.PedidoDetalleID
						INNER JOIN Pedido (NOLOCK) P ON PD.PedidoID = P.PedidoID
						WHERE PROMP.ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID)
	INSERT PesajeMateriaPrima (
		ProgramacionMateriaPrimaID,
		ProveedorChoferID,
		Ticket,
		CamionID,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoPesajeID,
		UsuarioIDSurtido,
		FechaSurtido,
		UsuarioIDRecibe,
		FechaRecibe,
		EstatusID,
		Activo,
		HoraPesoTara,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@ProgramacionMateriaPrimaID,
		CASE WHEN @ProveedorChoferID > 0 
		THEN @ProveedorChoferID
		ELSE NULL END,
		@Ticket,
		CASE WHEN @CamionID > 0 
		THEN @CamionID
		ELSE NULL END,
		@PesoBruto,
		@PesoTara,
		@Piezas,
		@TipoPesajeID,
		@UsuarioIDSurtido,
		@FechaSurtido,
		@UsuarioIDRecibe,
		@FechaRecibe,
		@EstatusID,
		@Activo,
		CURRENT_TIMESTAMP,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT 
	    PesajeMateriaPrimaID,
		ProgramacionMateriaPrimaID,
		ProveedorChoferID,
		Ticket,
		CamionID,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoPesajeID,
		UsuarioIDSurtido,
		FechaSurtido,
		UsuarioIDRecibe,
		FechaRecibe,
		EstatusID,
		AlmacenMovimientoOrigenID,
		AlmacenMovimientoDestinoID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM PesajeMateriaPrima WHERE PesajeMateriaPrimaID = SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
