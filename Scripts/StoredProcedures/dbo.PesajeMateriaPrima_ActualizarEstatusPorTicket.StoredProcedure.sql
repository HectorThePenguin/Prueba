USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ActualizarEstatusPorTicket]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ActualizarEstatusPorTicket]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ActualizarEstatusPorTicket]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 23/06/2014
-- Description: 
-- SpName     : PesajeMateriaPrima_ActualizarEstatusPorTicket
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ActualizarEstatusPorTicket]
@Ticket INT,
@UsuarioModificacionID INT,
@EstatusID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE PesajeMateriaPrima SET
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE(),
		EstatusID = @EstatusID
	WHERE Ticket = @Ticket
	SET NOCOUNT OFF;
END



GO
