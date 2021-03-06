USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerAnimalSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerAnimalSalida]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerAnimalSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-01-13
-- Origen: APInterfaces
-- Descripci�n:	Se consulta si el animal esta en AnimalSalida
-- EXEC CorteTransferenciaGanado_ObtenerAnimalSalida 11
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerAnimalSalida]
@AnimalID INT 
AS
BEGIN
	SELECT TOP 1
		ASalida.AnimalID,
	    ASalida.LoteID,
		C.CorralID,
		ASalida.CorraletaID
	FROM Animal AS A (NOLOCK) 
	INNER JOIN AnimalSalida (NOLOCK) ASalida ON ASalida.AnimalID = A.AnimalID 
	INNER JOIN Lote (NOLOCK) L ON L.LoteID = ASalida.LoteID 
	INNER JOIN Corral (NOLOCK) C ON C.CorralID = L.CorralID
	WHERE A.AnimalID=@AnimalID
	AND A.Activo = 1
   AND A.Activo = ASalida.Activo
END

GO
