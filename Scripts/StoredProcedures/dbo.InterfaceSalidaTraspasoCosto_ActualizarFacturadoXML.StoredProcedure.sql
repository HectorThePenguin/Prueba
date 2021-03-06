USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML
-- =============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML]
@AnimalesXML	XML
AS 
BEGIN	

	SET NOCOUNT ON

		UPDATE ISTC
		SET ISTC.Facturado = 1
			, ISTC.FechaModificacion = GETDATE()
		FROM InterfaceSalidaTraspasoCosto ISTC
		INNER JOIN	
		(
			SELECT T.N.value('./AnimalID[1]','BIGINT') AS AnimalID
			FROM @AnimalesXML.nodes('/ROOT/InterfaceSalidaTraspasoCosto') as T(N)
		) x ON (ISTC.AnimalID = x.AnimalID)

	SET NOCOUNT OFF
END

GO
