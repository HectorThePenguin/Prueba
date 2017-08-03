USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerInterfazSalidaAnimalPorEntradaGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerInterfazSalidaAnimalPorEntradaGanado]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerInterfazSalidaAnimalPorEntradaGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Edgar Villarreal
-- Create date: 20/10/2015
-- Description:	Obtiene interface salida animal
-- InterfaceSalidaAnimal_ObtenerInterfazSalidaAnimalPorEntradaGanado 8, 1
--=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerInterfazSalidaAnimalPorEntradaGanado]
@SalidaID INT,
@OrganizacionID INT

AS
BEGIN
	SET NOCOUNT ON
		SELECT OrganizacionID
			,  SalidaID
			,  Arete
			,  FechaCompra
			,  PesoCompra
			,  TipoGanadoID
			,  PesoOrigen
			,  FechaRegistro
			,  UsuarioRegistro
			,  AreteMetalico
			,  AnimalID
		FROM InterfaceSalidaAnimal
		WHERE OrganizacionID = @OrganizacionID AND SalidaID = @SalidaID
			
	SET NOCOUNT OFF
END

GO
