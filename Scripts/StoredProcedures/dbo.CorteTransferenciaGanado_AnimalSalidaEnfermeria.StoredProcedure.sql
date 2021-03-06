USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_AnimalSalidaEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_AnimalSalidaEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_AnimalSalidaEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-01-13
-- Origen: APInterfaces
-- Descripci�n:	Se consulta si el animal esta en AnimalSalida
-- EXEC CorteTransferenciaGanado_AnimalSalidaEnfermeria 11
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_AnimalSalidaEnfermeria]
@AnimalID INT ,
@OrganizacionID INT,
@TipoMovimientoID INT,
@Activo INT
AS
BEGIN
	SELECT TOP 1
		ASalida.AnimalID,
		ASalida.LoteID,
		ASalida.CorraletaID
	FROM Animal AS A (NOLOCK) 
	INNER JOIN AnimalSalida (NOLOCK) ASalida ON ASalida.AnimalID = A.AnimalID 
	WHERE A.AnimalID=@AnimalID
	AND ASalida.AnimalID=@AnimalID
	AND ASalida.TipoMovimientoID= @TipoMovimientoID
	AND A.OrganizacionIDEntrada=@OrganizacionID
	AND ASalida.Activo = @Activo
END

GO
