USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaLoteAnimalSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ConsultaLoteAnimalSalida]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaLoteAnimalSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/************************************
Autor	  : Pedro Delgado
Fecha	  : 20/03/2014
Proposito : Obtiene lote por el arete
************************************/
CREATE PROCEDURE [dbo].[DeteccionGanado_ConsultaLoteAnimalSalida]
@Arete VARCHAR(15),
@AreteTestigo VARCHAR(15),
@OrganizacionID INT
AS
BEGIN
		SELECT
			ASA.AnimalSalidaID,
			ASA.AnimalID,LoteID,
			ASA.CorraletaID,
			ASA.TipoMovimientoID,
			ASA.FechaSalida,
			ASA.OrdenSacrificioID,
			ASA.Activo,
			ASA.FechaCreacion,
			ASA.UsuarioCreacionID,
			ASA.FechaModificacion,
			ASA.UsuarioModificacionID
		FROM AnimalSalida (NOLOCK) ASA
		INNER JOIN Animal (NOLOCK) A
		ON (ASA.AnimalID = A.AnimalID)
		WHERE (A.Arete = @Arete OR @Arete = '') AND (A.AreteMetalico= @AreteTestigo OR @AreteTestigo = '') AND A.OrganizacionIDEntrada = @OrganizacionID AND ASA.Activo = 1
END

GO
