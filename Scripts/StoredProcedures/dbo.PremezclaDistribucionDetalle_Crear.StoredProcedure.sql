USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucionDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDistribucionDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucionDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 17/07/2014 
-- Description: 
-- SpName     : PremezclaDistribucionDetalle_Crear 1, 1, 100, 1, 1
--======================================================
CREATE PROCEDURE [dbo].[PremezclaDistribucionDetalle_Crear]
	@PremezclaDistribucionId INT,
	@OrganizacionId INT,
	@AlmacenMovimientoId INT,
	@CantidadASurtir BIGINT,
	@UsuarioCreacionId INT
AS
BEGIN
	DECLARE @PremezclaDistribucionDetalleId INT
	INSERT INTO PremezclaDistribucionDetalle ( PremezclaDistribucionID, OrganizacionID, CantidadASurtir, Activo, FechaCreacion, UsuarioCreacionID, AlmacenMovimientoID)
	VALUES (@PremezclaDistribucionId, @OrganizacionId, @CantidadASurtir, 1, GETDATE(), @UsuarioCreacionId, @AlmacenMovimientoId)
	SET @PremezclaDistribucionDetalleId = @@IDENTITY
	SELECT PremezclaDistribucionDetalleID, PremezclaDistribucionID, OrganizacionID, CantidadASurtir, Activo, FechaCreacion, UsuarioCreacionID, AlmacenMovimientoID
	FROM PremezclaDistribucionDetalle (NOLOCK) WHERE PremezclaDistribucionDetalleID = @PremezclaDistribucionDetalleId
END
