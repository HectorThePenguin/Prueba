IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[ControlEntradaGanado_ObtenerPorAnimalIDOEntradaGanadoID]'))
  DROP PROCEDURE [dbo].[ControlEntradaGanado_ObtenerPorAnimalIDOEntradaGanadoID]

GO
-- =============================================
-- Author:		Edgar Villarreal
-- Create date: 05-09-2015
-- Description:	Obtiene control entrada de ganado por animalID o entradaGanadoID
-- EXEC ControlEntradaGanado_ObtenerPorAnimalIDOEntradaGanadoID 0, 0
-- =============================================
CREATE PROCEDURE dbo.ControlEntradaGanado_ObtenerPorAnimalIDOEntradaGanadoID
@AnimalID	int,
@EntradaGanadoID int
AS 
BEGIN	

	SET NOCOUNT ON
	DECLARE @ControlEntradaGanadoID bigint;

			CREATE Table #ControlEntrada (
						ControlEntradaGanadoID BIGINT
						, EntradaGanadoID int
						, AnimalID BIGINT
						, PesoCompra INT
						, Activo BIT
						,UsuarioCreacionID INT
						,FechaCreacion datetime
						
					);


				INSERT INTO #ControlEntrada
				SELECT 	ControlEntradaGanadoID, 
								EntradaGanadoID, 
								AnimalID ,
								PesoCompra,
								Activo, 
								UsuarioCreacionID, 
								FechaCreacion 
					FROM ControlEntradaGanado (NOLOCK)
						WHERE  
								(
											(@AnimalID > 0 AND @EntradaGanadoID = 0 AND AnimalID = @AnimalID ) 
									OR 	
											(@EntradaGanadoID > 0 AND @AnimalID = 0 AND EntradaGanadoID = @EntradaGanadoID)
									OR 	
											(@EntradaGanadoID > 0 AND @AnimalID > 0 AND EntradaGanadoID = @EntradaGanadoID AND AnimalID = @AnimalID )
								)

			SELECT CEG.ControlEntradaGanadoDetalleID, 
								CEG.ControlEntradaGanadoID,
								CEG.CostoID, 
								CEG.Importe ,
								CEG.Activo, 
								CEG.UsuarioCreacionID, 
								CEG.FechaCreacion FROM ControlEntradaGanadoDetalle CEG
			INNER JOIN #ControlEntrada CETem ON CETem.ControlEntradaGanadoID = CEG.ControlEntradaGanadoID
			
			SELECT 	ControlEntradaGanadoID, 
								EntradaGanadoID, 
								AnimalID ,
								PesoCompra,
								Activo, 
								UsuarioCreacionID, 
								FechaCreacion 
					FROM #ControlEntrada (NOLOCK)

			DROP TABLE #ControlEntrada

	SET NOCOUNT OFF
END




