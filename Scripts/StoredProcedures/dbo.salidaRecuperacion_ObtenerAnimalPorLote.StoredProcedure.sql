USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[salidaRecuperacion_ObtenerAnimalPorLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[salidaRecuperacion_ObtenerAnimalPorLote]
GO
/****** Object:  StoredProcedure [dbo].[salidaRecuperacion_ObtenerAnimalPorLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-02-28
-- Descripci�n:	Actualizar el numero de cabezas en lote origen
-- EXEC salidaRecuperacion_ObtenerAnimalPorLote '3',4,1
-- =============================================
CREATE PROCEDURE [dbo].[salidaRecuperacion_ObtenerAnimalPorLote]
	@LoteID INT,
	@OrganizacionID INT,
	@ActivoID INT 
AS
BEGIN
	SET NOCOUNT ON
				SELECT 	ASalida.AnimalID,
						ASalida.AnimalSalidaID,
						ASalida.LoteID,
						ASalida.CorraletaID,
						ASalida.TipoMovimientoID
				FROM AnimalSalida ASalida
				INNER JOIN Lote as L ON L.LoteID = ASalida.LoteID
				WHERE ASalida.LoteID=@LoteID
				AND L.OrganizacionID=@OrganizacionID
				AND ASalida.Activo=@ActivoID
	SET NOCOUNT OFF
END

GO
