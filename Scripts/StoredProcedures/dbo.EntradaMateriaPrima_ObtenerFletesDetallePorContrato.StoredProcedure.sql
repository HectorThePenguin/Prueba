USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerFletesDetallePorContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerFletesDetallePorContrato]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerFletesDetallePorContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 27/05/2014
-- Description: Obtiene el detalle del flete por contrato
-- SpName     : EXEC EntradaMateriaPrima_ObtenerFletesDetallePorContrato 1,1,4,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerFletesDetallePorContrato]
@ContratoID INT,
@RegistroVigilanciaID INT,
@OrganizacionID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT			FD.FleteDetalleID,
					FD.CostoID,
					FD.Tarifa,
					F.ContratoID,
					F.Observaciones,
					F.MermaPermitida,
				    F.ProveedorID,
					C.Activo,
					F.FleteID AS FleteId
	FROM Flete (NOLOCK) AS F
	INNER JOIN FleteDetalle (NOLOCK) AS FD ON F.FleteID = FD.FleteID AND FD.Activo = @Activo
	INNER JOIN Contrato (NOLOCK) AS C ON C.ContratoID = F.ContratoID 
	INNER JOIN Proveedor (NOLOCK) AS P ON P.ProveedorID = F.ProveedorID AND P.Activo = @Activo
	INNER JOIN ProveedorChofer (NOLOCK) AS PC ON PC.ProveedorID = P.ProveedorID AND PC.Activo = @Activo
	INNER JOIN RegistroVigilancia (NOLOCK) AS RV ON RV.ProveedorChoferID = PC.ProveedorChoferID --AND RV.Activo = @Activo
	WHERE C.ContratoID=@ContratoID
	  AND RV.RegistroVigilanciaID = @RegistroVigilanciaID
	  AND RV.OrganizacionID = @OrganizacionID
	  AND F.OrganizacionID = @OrganizacionID
	  AND F.Activo = @Activo
	  AND C.Activo = @Activo 
	SET NOCOUNT OFF;
END

GO
