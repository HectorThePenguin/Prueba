USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerImpresionTarjetaRecepcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerImpresionTarjetaRecepcion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerImpresionTarjetaRecepcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 16/12/2014
-- Description:	Obtiene los datos para la impresion de la tarjeta de recepcion
-- EntradaGanado_ObtenerImpresionTarjetaRecepcion 1,0,'20141215'
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerImpresionTarjetaRecepcion] @OrganizacionID INT	
	,@FechaEntrada DATE
AS
BEGIN
	SELECT eg.EntradaGanadoID
	,CAST(eg.FolioEntrada AS VARCHAR(20)) AS FolioEntrada
	,us.Nombre AS Vigilante
		,CONVERT(VARCHAR(8), FechaEntrada, 108) AS HoraVigilancia
		,o.Descripcion AS Origen
		,op.Nombre + ' ' + op.ApellidoPaterno + ' ' + op.ApellidoMaterno AS Operador
		,ja.PlacaJaula
		,eg.PesoBruto
		,eg.PesoTara
		,eg.PesoBruto - eg.PesoTara AS PesoNeto
		,CONVERT(VARCHAR(8), FechaEntrada, 108) AS HoraBascula
		,us.Nombre AS UsuarioRecibio
		,eg.FechaEntrada
		,eg.CabezasRecibidas
		,lo.Cabezas AS CabezasCorral
		,co.Codigo AS Corral
		,eg.ManejoSinEstres
		,eg.Guia
		,eg.Factura
		,eg.Poliza
		,eg.HojaEmbarque
	FROM EntradaGanado eg
	INNER JOIN Usuario us ON eg.UsuarioCreacionID = us.UsuarioID
	INNER JOIN Organizacion o ON eg.OrganizacionOrigenID = o.OrganizacionID
	INNER JOIN Operador op ON eg.OperadorID = op.OperadorID
	INNER JOIN Jaula ja ON eg.JaulaID = ja.JaulaID
	INNER JOIN Corral co ON eg.CorralID = co.CorralID
	INNER JOIN Lote lo ON eg.LoteID = lo.LoteID
	WHERE eg.OrganizacionID = @OrganizacionID
		AND lo.Activo = 1
		AND CAST(eg.FechaEntrada AS DATE) = @FechaEntrada
END

GO
