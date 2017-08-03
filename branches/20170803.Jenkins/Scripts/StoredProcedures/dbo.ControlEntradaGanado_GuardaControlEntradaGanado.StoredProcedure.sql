IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[ControlEntradaGanado_GuardaControlEntradaGanado]'))
  DROP PROCEDURE [dbo].[ControlEntradaGanado_GuardaControlEntradaGanado]
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 05/09/2015
-- Description:  Guarda Control Entrada Ganado
-- =============================================
CREATE PROCEDURE [dbo].[ControlEntradaGanado_GuardaControlEntradaGanado]
@EntradaGanadoID BIGINT,
@AnimalID BIGINT,
@PesoCompra INT,
@Activo BIT,
@UsuarioCreacionID INT,
@Xml XML
AS
BEGIN

		DECLARE @IdentityID INT;

		INSERT INTO ControlEntradaGanado
		(
				EntradaGanadoID,
				AnimalID,
				PesoCompra,
				Activo,
				UsuarioCreacionID,
				FechaCreacion
		)VALUES(@EntradaGanadoID,@AnimalID,@PesoCompra,@Activo,@UsuarioCreacionID,GETDATE())

		SET @IdentityID = (SELECT @@IDENTITY);
		
	
		INSERT INTO ControlEntradaGanadoDetalle
		(
			ControlEntradaGanadoID,
			CostoID,
			Importe,
			Activo,
			UsuarioCreacionID,	
			FechaCreacion
		)
		SELECT 
				@IdentityID AS ControlEntradaGanadoID
				, t.item.value('./CostoID[1]', 'INT') AS CostoID
				, t.item.value('./Importe[1]', 'DECIMAL(10,4)') AS Importe
				, t.item.value('./Activo[1]', 'BIT') AS Activo
				, t.item.value('./UsuarioCreacionID[1]', 'INT') AS UsuarioCreacionID,
				GETDATE()
		FROM @Xml.nodes('ROOT/ControlEntradaDetalle') AS T(item)

END
