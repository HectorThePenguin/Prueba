USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 18/11/2013
-- Description:	Obtiene interface salida animal
-- InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino 8, 1
--=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino]
@SalidaXML	XML
AS
BEGIN
	SET NOCOUNT ON
		SELECT ISA.OrganizacionID
			,  ISA.SalidaID
			,  ISA.Arete
			,  ISA.FechaCompra
			,  ISA.PesoCompra
			,  ISA.TipoGanadoID
			,  ISA.PesoOrigen
			,  ISalida.FechaSalida
			,  ISalida.Cabezas
			,  ISalida.EsRuteo
			,  ISalida.SalidaID
		FROM InterfaceSalidaAnimal ISA
		INNER JOIN InterfaceSalida ISalida
			ON (ISA.OrganizacionID = ISalida.OrganizacionID
				AND ISA.SalidaID = ISalida.SalidaID)
		INNER JOIN 
		(
			SELECT SalidaID = t.item.value('./SalidaID[1]', 'INT')
				,  OrganizacionDestinoID = t.item.value('./OrganizacionDestinoID[1]', 'INT')
				,  OrganizacionOrigenID = t.item.value('./OrganizacionOrigenID[1]', 'INT')
			FROM @SalidaXML.nodes('ROOT/Salidas') AS T(item)
		) S ON (ISalida.SalidaID = S.SalidaID
				AND ISalida.OrganizacionDestinoID = S.OrganizacionDestinoID
				AND ISalida.OrganizacionID = S.OrganizacionOrigenID)
	SET NOCOUNT OFF
END

GO
