USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerPorTurno]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_ObtenerPorTurno]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_ObtenerPorTurno]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladora_ObtenerPorTurno 4,2
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladora_ObtenerPorTurno]
@OrganizacionID INT
, @Turno		INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @Fecha CHAR(8), @Registros INT
		SET @Fecha = CONVERT(CHAR(8), GETDATE(), 112)
		DECLARE @tFecha TABLE
		(
			FechaInicio		DATETIME
			, HoraInicio	CHAR(5)
			, UsuarioIDSupervisor INT
		)
		INSERT INTO @tFecha
		SELECT TOP 1 ISNULL(CLRG.FechaInicio, GETDATE()) AS FechaInicio
			,  CONVERT(CHAR(5), GETDATE(), 108) AS HoraInicio
			,CLRG.UsuarioIDSupervisor
		FROM CheckListRoladoraGeneral CLRG
		WHERE CLRG.Turno = @Turno
		ORDER BY CLRG.FechaInicio DESC
			--AND CONVERT(CHAR(8), CLRG.FechaInicio, 112) = @Fecha
		SET @Registros = @@ROWCOUNT
		IF (@Registros = 0)
		BEGIN
			INSERT INTO @tFecha
			SELECT GETDATE() AS FechaInicio
				,  CONVERT(CHAR(5), GETDATE(), 108) AS HoraInicio
				,0
		END		
		SELECT FechaInicio
			,  HoraInicio
			,UsuarioIDSupervisor
		FROM @tFecha
		SELECT R.RoladoraID
			,  R.Descripcion
		FROM Roladora R
		WHERE R.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
