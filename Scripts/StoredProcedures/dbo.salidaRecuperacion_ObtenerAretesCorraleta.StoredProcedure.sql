USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[salidaRecuperacion_ObtenerAretesCorraleta]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[salidaRecuperacion_ObtenerAretesCorraleta]
GO
/****** Object:  StoredProcedure [dbo].[salidaRecuperacion_ObtenerAretesCorraleta]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-02-28
-- Descripción:	Actualizar el numero de cabezas en lote origen
-- EXEC salidaRecuperacion_ObtenerAretesCorraleta '001',4,1
-- =============================================
CREATE PROCEDURE [dbo].[salidaRecuperacion_ObtenerAretesCorraleta] @Codigo CHAR(10)
	,@LoteID INT
	,@OrganizacionID INT
	,@Activo INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT A.AnimalID
		,A.Arete
	FROM Corral C
	INNER JOIN AnimalSalida AS ASalida ON C.CorralID = ASalida.CorraletaID
	INNER JOIN Animal AS A(NOLOCK) ON A.AnimalID = ASalida.AnimalID
	WHERE C.Codigo = @Codigo
		AND C.Activo = @Activo
		AND C.OrganizacionID = @OrganizacionID
		AND ASalida.Activo = @Activo
		AND A.Activo = @Activo
		AND ASalida.LoteID = @LoteID

	SET NOCOUNT OFF
END

GO
