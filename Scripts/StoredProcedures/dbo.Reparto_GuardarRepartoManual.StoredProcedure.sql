USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarRepartoManual]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_GuardarRepartoManual]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarRepartoManual]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Ramses Santos
-- Create date: 2014/04/07
-- Description: SP para Crear un registro en la tabla de Reparto
-- Origen     : APInterfaces
-- EXEC Reparto_GuardarRepartoManual 1,4, 1, 
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_GuardarRepartoManual]
	@RepartoID BIGINT,
	@OrganizacionID INT,
	@LoteID INT,
	@Fecha SMALLDATETIME,
	@PesoInicio INT,
	@PesoProyectado INT,
	@DiasEngorda INT,
	@PesoRepeso INT,
	@UsuarioCreacionID INT,
	@TipoServicioID INT,
	@FormulaIDProgramada INT,
	@FormulaIDServida INT,
	@CantidadProgramada INT,
	@CantidadServida INT,
	@HoraReparto CHAR(5),
	@Cabezas INT,
	@EstadoComederoID INT,
	@CorralID INT
AS
BEGIN
	DECLARE @RepartoDetalleID BIGINT
	IF (@RepartoID = 0)
		BEGIN
			--NUEVO
			INSERT INTO Reparto(
			OrganizacionID,
			LoteID,
			Fecha,
			PesoInicio,
			PesoProyectado,
			DiasEngorda,
			PesoRepeso,
			CorralID,
			Activo,
			FechaCreacion,
			UsuarioCreacionID)
			VALUES(@OrganizacionID,
				   @LoteID,
				   @Fecha,
				   @PesoInicio,
				   @PesoProyectado,
				   @DiasEngorda,
				   @PesoRepeso,
				   @CorralID,
				   1,
				   GETDATE(),
				   @UsuarioCreacionID);
			SET @RepartoID = (SELECT @@IDENTITY)
		END	
		INSERT INTO RepartoDetalle(
				RepartoID,
				TipoServicioID,
				FormulaIDProgramada,
				FormulaIDServida,
				CantidadProgramada,
				CantidadServida,
				HoraReparto,
				CostoPromedio,
				Importe,
				Servido,
				Cabezas,
				EstadoComederoID,
				Observaciones,
				Activo,
				FechaCreacion,
				UsuarioCreacionID)
		VALUES (@RepartoID,
				@TipoServicioID,
				@FormulaIDProgramada,
				@FormulaIDServida,
				@CantidadProgramada,
				@CantidadServida,
				@HoraReparto,
				0,
				0,
				1,
				@Cabezas,
				@EstadoComederoID,
				'',
				1,
				GETDATE(),
				@UsuarioCreacionID);
				SET @RepartoDetalleID = (SELECT @@IDENTITY)
	SELECT @RepartoID AS RepartoID, @RepartoDetalleID AS RepartoDetalleID;
	SET NOCOUNT OFF;
END

GO
