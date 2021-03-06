USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizaFechaCierre]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizaFechaCierre]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizaFechaCierre]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Gilberto Carranza
Fecha	  : 28/11/2013
Proposito : Actualiza la Fecha de Cierre
Lote_ActualizaFechaCierre 1
-- 001 -- Gilberto Carranza -- Se agrega validacion para no poner fecha de cierre cuando el corral sea de enfermeria
************************************/
CREATE PROCEDURE [dbo].[Lote_ActualizaFechaCierre]
@LoteID INT
, @UsuarioModificacionID INT
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @FechaModificacion DATETIME
		SET @FechaModificacion = GETDATE()

		DECLARE @TipoCorralEnfermeria INT
		SET @TipoCorralEnfermeria = 3
		
		UPDATE Lote
		SET FechaCierre = CASE WHEN TipoCorralID = @TipoCorralEnfermeria THEN NULL ELSE @FechaModificacion END  --001
			, UsuarioModificacionID = @UsuarioModificacionID
			, FechaModificacion = @FechaModificacion
		WHERE LoteID = @LoteID

	SET NOCOUNT OFF

END

GO
