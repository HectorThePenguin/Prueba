USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ClonarParametroTrampa]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_ClonarParametroTrampa]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ClonarParametroTrampa]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/10/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Trampa_ClonarParametroTrampa 0,0,1
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_ClonarParametroTrampa] @TrampaOrigenID INT
	,@TrampaDestinoID INT
	,@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #PARAMETROSCLONAR (
		ParametroID INT
		,TrampaID INT
		,Valor VARCHAR(100)		
		)
	BEGIN TRY
		BEGIN TRANSACTION
		INSERT INTO #PARAMETROSCLONAR
		SELECT pt.ParametroID
			,pt.TrampaID
			,pt.Valor
		FROM ParametroTrampa pt
		WHERE pt.TrampaID = @TrampaOrigenID
		UPDATE pt
		SET pt.Valor = pc.Valor, pt.UsuarioModificacionID = @UsuarioCreacionID, pt.FechaModificacion = GETDATE()
		FROM ParametroTrampa pt
		INNER JOIN #PARAMETROSCLONAR pc ON pt.ParametroID = pc.ParametroID
		WHERE pt.TrampaID = @TrampaDestinoID
		INSERT INTO ParametroTrampa (
			ParametroID
			,TrampaID
			,Valor
			,Activo
			,FechaCreacion
			,UsuarioCreacionID
			)
		SELECT pt.ParametroID
			,@TrampaDestinoID
			,pt.Valor
			,1 -- Activo
			,getdate() --FechaCreacion
			,@UsuarioCreacionID
		FROM #PARAMETROSCLONAR pt
		WHERE NOT EXISTS (
				SELECT pc.ParametroID
				FROM ParametroTrampa pc
				WHERE pc.ParametroID = pt.ParametroID
					AND pc.TrampaID = @TrampaDestinoID
				)
		IF @@TRANCOUNT > 0
		BEGIN
			COMMIT TRANSACTION;
		END
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage VARCHAR(4000)
		SET @ErrorMessage = 'Error: ' + ISNULL(ERROR_PROCEDURE(), '') + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) + ' Mensaje: ' + ERROR_MESSAGE()
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION;
		END
		RAISERROR (@ErrorMessage,16,1)
	END CATCH
	SET NOCOUNT OFF;
END

GO
