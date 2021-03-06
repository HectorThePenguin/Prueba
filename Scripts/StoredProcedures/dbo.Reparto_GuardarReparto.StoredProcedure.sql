USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarReparto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_GuardarReparto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarReparto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/26
-- Description: SP para Crear un registro en la tabla de Reparto
-- Origen     : APInterfaces
-- EXEC Reparto_GuardarReparto 1,4, 1, 
-- 001 Jorge Luis Velazquez Araujo 27/06/2015 **Se quita para que no guarde el Peso de Repeso, ya que este va a ser actualizado por el Cierre de Reimplante
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_GuardarReparto]
	@RepartoID BIGINT,
	@OrganizacionID INT,
	@CorralID INT,
	@LoteID INT,
	@Fecha DATE,
	@PesoInicio INT,
	@PesoProyectado INT,
	@DiasEngorda INT,
	@PesoRepeso INT,
	@UsuarioCreacionID INT,
	@RepartoDetalleID BIGINT,
	@TipoServicioID INT,
	@FormulaIDProgramada INT,
	@CantidadProgramada INT,
	@Cabezas INT,
	@EstadoComederoID INT
	
AS
BEGIN
	
	IF(@LoteID = 0)
		BEGIN 
			SET @LoteID = NULL;
		END
			
	IF (@RepartoID=0)
		BEGIN
			
			--NUEVO
			INSERT INTO Reparto(
			OrganizacionID,
			CorralID,
			LoteID,
			Fecha,
			PesoInicio,
			PesoProyectado,
			DiasEngorda,
			PesoRepeso,
			Activo,
			FechaCreacion,
			UsuarioCreacionID)
			VALUES(@OrganizacionID,
			       @CorralID,
				   @LoteID,
				   @Fecha,
				   @PesoInicio,
				   @PesoProyectado,
				   @DiasEngorda,
				   0,--001
				   1,
				   GETDATE(),
				   @UsuarioCreacionID);
				   
			SET @RepartoID = (SELECT @@IDENTITY)
			
		END
	ELSE
		BEGIN
			UPDATE Reparto
			SET PesoInicio = @PesoInicio,
				PesoProyectado = @PesoProyectado,
				DiasEngorda = @DiasEngorda,
				--PesoRepeso = @PesoRepeso,--001
				FechaModificacion = GETDATE(),
				UsuarioModificacionID = @UsuarioCreacionID
			WHERE RepartoID = @RepartoID 
				
		END
		
	
	IF (@RepartoDetalleID=0)
		BEGIN
	
			INSERT INTO RepartoDetalle(
					RepartoID,
					TipoServicioID,
					FormulaIDProgramada,
					CantidadProgramada,
					CantidadServida,
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
					@CantidadProgramada,
					0,
					0,
					0,
					0,
					@Cabezas,
					@EstadoComederoID,
					'',
					1,
					GETDATE(),
					@UsuarioCreacionID);
					
					SET @RepartoDetalleID = (SELECT @@IDENTITY)
		END
	ELSE
		BEGIN
			UPDATE RepartoDetalle
			   SET FormulaIDProgramada = @FormulaIDProgramada,
				   CantidadProgramada = @CantidadProgramada,
				   Cabezas = @Cabezas,
				   EstadoComederoID = @EstadoComederoID,
				   FechaModificacion = GETDATE(),
				   UsuarioModificacionID = @UsuarioCreacionID
			 WHERE RepartoDetalleID = @RepartoDetalleID

		END
	
	SELECT @RepartoID AS RepartoID, @RepartoDetalleID AS RepartoDetalleID;

	SET NOCOUNT OFF;
END

GO
