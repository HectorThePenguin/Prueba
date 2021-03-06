USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteLlegadaLogistica_Obtener]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteLlegadaLogistica_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[ReporteLlegadaLogistica_Obtener]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- =============================================
 -- Author: Raúl Alberto Vega Ruelas
 -- Create date: 26/06/2015
 -- Description: Obtiene los datos para reporte de Llegada Logistica
 -- propuesta
 -- Exec ReporteLlegadaLogistica_Obtener 4,'20150401', '20150728'
 -- =============================================
CREATE PROCEDURE [dbo].[ReporteLlegadaLogistica_Obtener]
 @OrganizacionID INT
 ,@FechaInicial DATE
 ,@FechaFinal DATE
AS
BEGIN
 DECLARE @Organizacion INT
 DECLARE @FechaInicio DATE
 DECLARE @FechaFin  DATE
 SET @Organizacion = @OrganizacionID
 SET @FechaInicio = @FechaInicial
 SET @FechaFin = @FechaFinal
 SELECT e.FolioEmbarque
 , UPPER(te.Descripcion) AS TipoEmbarque
 , p.Descripcion AS Proveedor
 , c.Nombre + ' ' + c.ApellidoPaterno + ' ' + c.ApellidoMaterno AS Chofer
 , o.Descripcion AS Origen
 , od.Descripcion AS Destino
 , UPPER(es.Descripcion) AS Estatus
 , co.Descripcion
 , ced.Importe
 , ISNULL(eg.FolioEntrada,0) AS FolioEntrada
 , ISNULL(eg.FechaEntrada, CAST('1900-01-01' AS DATE)) AS FechaEntrada
 , ISNULL(ed.FechaSalida, CAST('1900-01-01' AS DATE)) AS FechaEmbarque
 FROM Embarque e
 INNER JOIN EmbarqueDetalle ed ON ed.EmbarqueID = e.EmbarqueID
 INNER JOIN CostoEmbarqueDetalle ced ON ced.EmbarqueDetalleID = ed.EmbarqueDetalleID
 INNER JOIN TipoEmbarque te ON te.TipoEmbarqueID = e.TipoEmbarqueID
 INNER JOIN Estatus es ON es.EstatusID = e.Estatus
 INNER JOIN Proveedor p ON p.ProveedorID = ed.ProveedorID
 INNER JOIN Chofer c ON c.ChoferID = ed.ChoferID
 INNER JOIN Organizacion o ON o.OrganizacionID = ed.OrganizacionOrigenID
 INNER JOIN Organizacion od ON od.OrganizacionID = ed.OrganizacionDestinoID
 INNER JOIN Costo co ON co.CostoID = ced.CostoID
 LEFT JOIN EntradaGanado eg ON eg.EmbarqueID = e.EmbarqueID
 WHERE e.FechaCreacion
 BETWEEN @FechaInicio AND @FechaFin 
 AND ed.Activo = 1 
 AND od.OrganizacionID = @Organizacion
END

GO
