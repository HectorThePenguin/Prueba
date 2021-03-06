USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacion_ExisteLoteAnimalSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaRecuperacion_ExisteLoteAnimalSalida]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacion_ExisteLoteAnimalSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-02-28
-- Descripci�n:	Existe el lote en animal salida
-- EXEC SalidaRecuperacion_ExisteLoteAnimalSalida '3',4,1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaRecuperacion_ExisteLoteAnimalSalida]
	@LoteID INT,
	@OrganizacionID INT,
	@ActivoID INT ,
	@CorralID INT
AS
BEGIN
	SET NOCOUNT ON
				SELECT TOP 1 ASalida.AnimalID,ASalida.AnimalSalidaID,ASalida.LoteID,ASalida.CorraletaID
				FROM AnimalSalida ASalida
				INNER JOIN Lote as L ON L.LoteID = ASalida.LoteID
				WHERE ASalida.LoteID = @LoteID
				AND ASalida.CorraletaID = @CorralID 
				AND L.OrganizacionID = @OrganizacionID
				AND ASalida.Activo = @ActivoID
	SET NOCOUNT OFF
END

GO
