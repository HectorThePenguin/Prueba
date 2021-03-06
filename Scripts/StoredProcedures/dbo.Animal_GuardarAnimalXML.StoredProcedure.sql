USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_GuardarAnimalXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_GuardarAnimalXML]
GO
/****** Object:  StoredProcedure [dbo].[Animal_GuardarAnimalXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 16/01/2015
-- Description:  Obtiene animales por su disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Animal_GuardarAnimalXML]
@XmlAnimales XML
AS
BEGIN

		DECLARE @Fecha DATE
		SET @Fecha = GETDATE()

		INSERT INTO Animal
		(
				Arete
			,	AreteMetalico
			,	FechaCompra
			,	TipoGanadoID
			,	CalidadGanadoID
			,	ClasificacionGanadoID
			,	PesoCompra
			,	OrganizacionIDEntrada
			,	FolioEntrada
			,	PesoLlegada
			,	Paletas
			,	Venta
			,	Cronico
			,	CambioSexo
			,	Activo
			,   UsuarioCreacionID	
		)
		SELECT 
				  t.item.value('./Arete[1]', 'VARCHAR(100)') AS Arete
				, t.item.value('./AreteMetalico[1]', 'VARCHAR(100)') AS AreteMetalico
				, t.item.value('./FechaCompra[1]', 'DATETIME') AS FechaCompra
				, t.item.value('./TipoGanadoID[1]', 'INT') AS TipoGanadoID
				, t.item.value('./CalidadGanadoID[1]', 'INT') AS CalidadGanadoID
				, t.item.value('./ClasificacionGanadoID[1]', 'INT') AS ClasificacionGanadoID
				, t.item.value('./PesoCompra[1]', 'INT') AS PesoCompra
				, t.item.value('./OrganizacionIDEntrada[1]', 'INT') AS OrganizacionIDEntrada
				, t.item.value('./FolioEntrada[1]', 'BIGINT') AS FolioEntrada
				, t.item.value('./PesoLlegada[1]', 'INT') AS PesoLlegada
				, t.item.value('./Paletas[1]', 'INT') AS Paletas
				, t.item.value('./Venta[1]', 'BIT') AS Venta
				, t.item.value('./Cronico[1]', 'BIT') AS Cronico
				, t.item.value('./CambioSexo[1]', 'BIT') AS CambioSexo
				, t.item.value('./Activo[1]', 'BIT') AS Activo
				, t.item.value('./UsuarioCreacionID[1]', 'INT') AS UsuarioCreacionID
		FROM @XmlAnimales.nodes('ROOT/Animal') AS T(item)

END

GO
