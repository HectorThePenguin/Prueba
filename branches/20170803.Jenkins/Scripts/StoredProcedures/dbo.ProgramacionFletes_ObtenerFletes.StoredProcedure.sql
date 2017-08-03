USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerFletes]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_ObtenerFletes]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerFletes]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 22/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProgramacionFletes_ObtenerFletes 3,1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_ObtenerFletes]
@ContratoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT			F.ContratoID 			AS ContratoID,
					F.Observaciones			AS Observaciones,
					F.MermaPermitida		AS MermaPermitida,
					P.Descripcion			AS DescripcionProveedor,
					P.CodigoSap				AS CodigoSapProveedor,
					F.ProveedorID			AS ProveedorID,
					C.Activo 				AS Activo,
					F.FleteID				AS FleteId,
					TT.TipoTarifaID,
					TT.Descripcion
	FROM Flete (NOLOCK) AS F
	INNER JOIN TipoTarifa AS TT ON TT.TipoTarifaID = F.TipoTarifaID
	INNER JOIN Contrato (NOLOCK) AS C ON C.ContratoID = F.ContratoID
	INNER JOIN Proveedor (NOLOCK) AS P ON P.ProveedorID = F.ProveedorID
	WHERE C.ContratoID=@ContratoID
		AND C.Activo = @Activo 
		AND F.Activo =@Activo
	SET NOCOUNT OFF;
END

GO
