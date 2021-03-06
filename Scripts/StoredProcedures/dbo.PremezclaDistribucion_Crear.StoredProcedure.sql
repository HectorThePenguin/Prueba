USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucion_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDistribucion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucion_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 17/07/2014 
-- Description: 
-- SpName     : PremezclaDistribucion_Crear 1, 100, '1.12', 1, 1
--======================================================
CREATE PROCEDURE [dbo].[PremezclaDistribucion_Crear]
	@ProductoId INT,
	@CantidadExistente INT,
	@CostoUnitario DECIMAL (18, 2),
	@ProveedorID INT,
	@Iva INT,
	@UsuarioCreacionId INT
AS
BEGIN
	DECLARE @PremezclaDistribucionId INT
	INSERT INTO PremezclaDistribucion (ProductoID, FechaEntrada, CantidadExistente, CostoUnitario, IVA, ProveedorID, Activo, FechaCreacion, UsuarioCreacionID)
	VALUES (@ProductoId, GETDATE(), @CantidadExistente, @CostoUnitario, @IVA, @ProveedorID, 1, GETDATE(), @UsuarioCreacionId)
	SET @PremezclaDistribucionId = @@IDENTITY
	SELECT PremezclaDistribucionID, ProductoID, FechaEntrada, CantidadExistente, CostoUnitario, IVA, ProveedorID, Activo, FechaCreacion, UsuarioCreacionID
	FROM PremezclaDistribucion (NOLOCK)
		WHERE PremezclaDistribucionID = @PremezclaDistribucionId
END

GO
