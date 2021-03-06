USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarDistribucionAlimento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_GuardarDistribucionAlimento]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_GuardarDistribucionAlimento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Ramses Santos
-- Create date: 25/03/2014
-- Description:  Guardar el estatus con el que se detecto la distribucion de alimento
-- Reparto_GuardarDistribucionAlimento
-- ===============================================================
CREATE PROCEDURE [dbo].[Reparto_GuardarDistribucionAlimento] 
	@CodigoCorral CHAR(10), 
	@EstatusDistribucion INT,
	@OrganizacionId INT,
	@UsuarioID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TipoServicio INT
	SELECT @TipoServicio = TipoServicioID FROM TipoServicio (NOLOCK)
	WHERE CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR)+':'+CAST(DATEPART(MINUTE, GETDATE()) AS VARCHAR) 
	BETWEEN HoraInicio AND HoraFin
	INSERT INTO LoteDistribucionAlimento (LoteID, TipoServicioID, EstatusDistribucionID, Fecha, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
	SELECT L.LoteID, @TipoServicio, @EstatusDistribucion, GETDATE(), 1, GETDATE(), @UsuarioID, GETDATE(), @UsuarioID 
	FROM Lote (NOLOCK) AS L INNER JOIN Corral (NOLOCK) AS C ON (C.CorralID = L.CorralID AND C.OrganizacionID = L.OrganizacionID)
	WHERE L.Activo = 1 AND C.Activo = 1 AND C.Codigo = @CodigoCorral AND L.OrganizacionID = @OrganizacionId
	SELECT @@IDENTITY AS LoteDistribucionID
	SET NOCOUNT OFF;
END

GO
