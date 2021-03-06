USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarFechaCerrado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarFechaCerrado]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarFechaCerrado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Lote_ActualizarFechaCerrado
-- 001 -- Gilberto Carranza -- Se agrega validacion para no poner fecha de cierre cuando el corral sea de enfermeria
--======================================================
CREATE PROCEDURE [dbo].[Lote_ActualizarFechaCerrado] @LoteID INT
	,@FechaCerrado DATETIME	
	,@UsuarioModificacionID INT
AS
BEGIN

	DECLARE @TipoCorralEnfermeria INT
	SET @TipoCorralEnfermeria = 3
	
	UPDATE Lote
	SET FechaCierre = CASE WHEN TipoCorralID = @TipoCorralEnfermeria THEN NULL ELSE @FechaCerrado END  --001
		,UsuarioModificacionID = @UsuarioModificacionID
		,FechaModificacion = GETDATE()	
	WHERE LoteID = @LoteID

END

GO
