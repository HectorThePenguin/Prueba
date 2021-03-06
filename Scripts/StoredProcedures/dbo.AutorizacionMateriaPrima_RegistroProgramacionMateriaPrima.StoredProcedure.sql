USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_RegistroProgramacionMateriaPrima]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_RegistroProgramacionMateriaPrima]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_RegistroProgramacionMateriaPrima]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 25/11/2014
-- Description: Crea registro AutorizacionMateriaPrima
-- AutorizacionMateriaPrima_RegistroProgramacionMateriaPrima 1,1,106,'sidbfib',114,3.2449,60.00,100,6,13,5,1
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_RegistroProgramacionMateriaPrima]
	@PedidoDetalleID INT,
  @OrganizacionID INT,
  @AlmacenID INT,
  @AlmacenInventarioLoteID INT,
  @CantidadProgramada DECIMAL(10,2),
  @UsuarioCreacionID INT,
  @Observaciones VARCHAR,
	@AutorizacionMateriaPrimaID INT
AS
BEGIN
	SET NOCOUNT ON;
  INSERT INTO ProgramacionMateriaPrima
				(PedidoDetalleID,
				OrganizacionID,
				AlmacenID,
				InventarioLoteIDOrigen,
				CantidadProgramada,
				Observaciones,
				FechaProgramacion,
				Activo,
				FechaCreacion,
				UsuarioCreacionID,
				AutorizacionMateriaPrimaID)
	VALUES(
		  @PedidoDetalleID,
			@OrganizacionID,
			@AlmacenID,
			@AlmacenInventarioLoteID,
			@CantidadProgramada,
			@Observaciones,
			GETDATE(),
			1,
			GETDATE(),
			@UsuarioCreacionID,
			@AutorizacionMateriaPrimaID	
	)
	SET NOCOUNT OFF;
END

GO
