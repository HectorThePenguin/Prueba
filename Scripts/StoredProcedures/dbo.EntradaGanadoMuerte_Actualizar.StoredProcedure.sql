USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoMuerte_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoMuerte_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoMuerte_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 11/05/2015
-- Description:  EntradaGanadoMuerte_Actualizar
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanadoMuerte_Actualizar]
@XmlMuertes		XML
, @XmlDetalle	XML
AS
BEGIN
		
		UPDATE EGM
		SET Peso = x.Peso
			, Arete = x.Arete
			, UsuarioModificacionID = x.UsuarioModificacion
		FROM EntradaGanadoMuerte EGM
		INNER JOIN
		(
			SELECT 
					  t.item.value('./EntradaGanadoMuerteID[1]', 'INT') AS EntradaGanadoMuerteID
					, t.item.value('./Peso[1]', 'INT') AS Peso
					, t.item.value('./Arete[1]', 'VARCHAR(100)') AS Arete
					, t.item.value('./EntradaGanadoID[1]', 'INT') AS EntradaGanadoID
					, t.item.value('./FolioMuerte[1]', 'INT') AS FolioMuerte
					, t.item.value('./Fecha[1]', 'DATETIME') AS Fecha
					, t.item.value('./UsuarioModificacion[1]', 'INT') AS UsuarioModificacion
			FROM @XmlMuertes.nodes('ROOT/Animal') AS T(item)
		) x ON (EGM.EntradaGanadoMuerteID = x.EntradaGanadoMuerteID)

		UPDATE EGM
		SET CostoID = x.CostoID
			, Importe = x.Importe
			, UsuarioModificacionID = x.UsuarioModificacion
		FROM EntradaGanadoMuerteDetalle EGM
		INNER JOIN
		(
			SELECT 
					  t.item.value('./EntradaGanadoMuerteID[1]', 'INT') AS EntradaGanadoMuerteID
					, t.item.value('./CostoID[1]', 'INT') AS CostoID
					, t.item.value('./Importe[1]', 'DECIMAL(18,2)') AS Importe
					, t.item.value('./UsuarioModificacion[1]', 'INT') AS UsuarioModificacion
			FROM @XmlDetalle.nodes('ROOT/Detalle') AS T(item)
		) x ON (EGM.EntradaGanadoMuerteID = x.EntradaGanadoMuerteID
				AND EGM.CostoID = x.CostoID)

		INSERT INTO EntradaGanadoMuerteDetalle(EntradaGanadoMuerteID, CostoID, Importe, UsuarioCreacionID, FechaCreacion, Activo)
		SELECT x.EntradaGanadoMuerteID
			,  x.CostoID
			,  x.Importe
			,  x.UsuarioModificacion
			,  GETDATE()
			,  1
		FROM
		(
			SELECT 
					  t.item.value('./EntradaGanadoMuerteID[1]', 'INT') AS EntradaGanadoMuerteID
					, t.item.value('./CostoID[1]', 'INT') AS CostoID
					, t.item.value('./Importe[1]', 'DECIMAL(18,2)') AS Importe
					, t.item.value('./UsuarioModificacionID[1]', 'INT') AS UsuarioModificacion
			FROM @XmlDetalle.nodes('ROOT/Detalle') AS T(item)
		) x 
		LEFT OUTER JOIN EntradaGanadoMuerteDetalle EGMD
			ON (EGMD.EntradaGanadoMuerteID = x.EntradaGanadoMuerteID
				AND EGMD.CostoID = x.CostoID)
		WHERE EGMD.CostoID IS NULL
			AND EGMD.EntradaGanadoMuerteID IS NULL

END

GO
