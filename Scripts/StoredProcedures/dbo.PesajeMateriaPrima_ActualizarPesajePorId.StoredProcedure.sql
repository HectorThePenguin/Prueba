USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ActualizarPesajePorId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ActualizarPesajePorId]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ActualizarPesajePorId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 27/06/2014
-- Description: 
-- SpName     : PesajeMateriaPrima_ActualizarPesajePorId
--001 se modifica el SP' para que no modifique los campos de almacen movimiento, si ya tienen dato
--002 se modifica sp para que se actualize el campo HoraPesoBruto
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ActualizarPesajePorId]
@PesajeMateriaPrimaID INT,
@ProgramacionMateriaPrimaID INT,
@ProveedorChoferID INT,
@Ticket INT,
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
@UsuarioModificacionID INT,
@AlmacenMovimientoOrigenId BIGINT,
@AlmacenMovimientoDestinoId BIGINT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE PesajeMateriaPrima SET
		ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID,
		ProveedorChoferID = CASE WHEN @ProveedorChoferID > 0 THEN @ProveedorChoferID ELSE NULL END,
		Ticket = @Ticket,
		CamionID = CASE WHEN @CamionID > 0 THEN @CamionID ELSE NULL END,
		PesoBruto = @PesoBruto,
		PesoTara = @PesoTara,
		Piezas = @Piezas,
		TipoPesajeID = @TipoPesajeID,
		UsuarioIDSurtido = @UsuarioIDSurtido,
		FechaSurtido = @FechaSurtido,
		UsuarioIDRecibe = @UsuarioIDRecibe,
		FechaRecibe = @FechaRecibe,
		EstatusID = @EstatusID,
		--AlmacenMovimientoOrigenID = CASE WHEN @AlmacenMovimientoOrigenId = 0 THEN NULL ELSE @AlmacenMovimientoOrigenId END, 001
		--AlmacenMovimientoDestinoID = CASE WHEN @AlmacenMovimientoDestinoId = 0 THEN NULL ELSE @AlmacenMovimientoDestinoId END, 001
		Activo = @Activo,
		HoraPesoBruto = CURRENT_TIMESTAMP,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
		WHERE PesajeMateriaPrimaID = @PesajeMateriaPrimaID
		update PesajeMateriaPrima set --001
		AlmacenMovimientoOrigenID = CASE WHEN @AlmacenMovimientoOrigenId = 0 THEN NULL ELSE @AlmacenMovimientoOrigenId END,
		AlmacenMovimientoDestinoID = CASE WHEN @AlmacenMovimientoDestinoId = 0 THEN NULL ELSE @AlmacenMovimientoDestinoId END
		where PesajeMateriaPrimaID = @PesajeMateriaPrimaID
		 and AlmacenMovimientoOrigenID is null 
		 and AlmacenMovimientoDestinoID is null
	SET NOCOUNT OFF;
END

GO
