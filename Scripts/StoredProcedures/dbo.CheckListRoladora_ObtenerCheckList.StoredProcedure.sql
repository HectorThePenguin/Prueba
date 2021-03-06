USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerCheckList]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_ObtenerCheckList]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerCheckList]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladora_ObtenerCheckList 1, 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladora_ObtenerCheckList] @OrganizacionID INT
	,@Turno INT
	,@RoladoraID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Fecha CHAR(8)
	SET @Fecha = CONVERT(CHAR(8), GETDATE(), 112)
	DECLARE @TiempoTranscurido INT, @Supervisado INT
	SELECT @TiempoTranscurido = DATEDIFF(MI, CLR.FechaCheckList, GETDATE())
		,  @Supervisado = CLRG.UsuarioIDSupervisor
	FROM CheckListRoladoraGeneral CLRG
	INNER JOIN CheckListRoladora CLR ON (
			CLRG.CheckListRoladoraGeneralID = CLR.CheckListRoladoraGeneralID
			AND CLR.RoladoraID = @RoladoraID
			)
	INNER JOIN Roladora R ON (
			CLR.RoladoraID = R.RoladoraID
			AND R.OrganizacionID = @OrganizacionID
			)
	WHERE CLRG.Turno = @Turno
		AND CONVERT(CHAR(8), CLR.FechaCheckList, 112) = @Fecha
	/*IF (@TiempoTranscurido IS NULL)
		BEGIN
			SET @TiempoTranscurido = 0
		END
		ELSE
		BEGIN
			IF (@TiempoTranscurido < 60 AND @TiempoTranscurido < 30)
			BEGIN
				SET @TiempoTranscurido = 1
			END
			ELSE
			BEGIN
				IF (@TiempoTranscurido > 30 AND @TiempoTranscurido < 60)
				BEGIN
					SET @TiempoTranscurido = 2
				END
				ELSE
				BEGIN
					SET @TiempoTranscurido = 3
				END
			END
		END*/
	IF @TiempoTranscurido = 0
	BEGIN
		SET @TiempoTranscurido = 1
	END
	SELECT ISNULL(@TiempoTranscurido, -1) AS TiempoTranscurrido
		,  ISNULL(@Supervisado, 0) AS Supervisado
	SET NOCOUNT OFF;
END

GO
