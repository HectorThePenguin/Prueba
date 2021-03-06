USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoCosto_CrearXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspasoCosto_CrearXML]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoCosto_CrearXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- InterfaceSalidaTraspasoCosto_CrearXML
-- 001 Jorge Luis Velazquez Araujo -- Se agrega flujo para Inactivar los Animales que se Traspasan
-- =============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspasoCosto_CrearXML]
@CostosXML	XML
AS 
BEGIN	

	SET NOCOUNT ON

		INSERT INTO InterfaceSalidaTraspasoCosto(InterfaceSalidaTraspasoDetalleID, AnimalID, CostoID, Importe, Activo, FechaCreacion, UsuarioCreacionID)
		SELECT x.InterfaceSalidaTraspasoDetalleID
			,  x.AnimalID
			,  x.CostoID
			,  x.Importe
			,  x.Activo
			,  x.FechaCreacion
			,  x.UsuarioCreacionID
		FROM
		(
			SELECT T.N.value('./InterfaceSalidaTraspasoDetalleID[1]','INT') AS InterfaceSalidaTraspasoDetalleID
				,  T.N.value('./AnimalID[1]','BIGINT') AS AnimalID
				,  T.N.value('./CostoID[1]','INT') AS CostoID
				,  T.N.value('./Importe[1]','DECIMAL(18,2)') AS Importe
				,  T.N.value('./Activo[1]','BIT') AS Activo
				,  GETDATE() AS FechaCreacion
				,  T.N.value('./UsuarioCreacionID[1]','INT') AS UsuarioCreacionID
			FROM @CostosXML.nodes('/ROOT/InterfaceSalidaTraspasoCosto') as T(N)
		) x

		UPDATE A
		SET Activo = 0
		  , FechaModificacion = GETDATE()
		  , UsuarioModificacionID = x.UsuarioCreacionID --001
		FROM
		(
			SELECT 
				  T.N.value('./AnimalID[1]','BIGINT') AS AnimalID				
				, T.N.value('./UsuarioCreacionID[1]','INT') AS UsuarioCreacionID
			FROM @CostosXML.nodes('/ROOT/InterfaceSalidaTraspasoCosto') as T(N)
		) x
		INNER JOIN Animal A
			ON (x.AnimalID = A.AnimalID)

	SET NOCOUNT OFF
END

GO
