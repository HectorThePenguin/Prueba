USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerParaCapturaCalidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerParaCapturaCalidad]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerParaCapturaCalidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  Jorge Luis Velazquez Araujo  
-- Create date: 29/01/2014
-- Description: Obtiene una entrada de ganado para la captura de sus calidades
-- EntradaGanado_ObtenerParaCapturaCalidad 1, 4  
-- =============================================  
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerParaCapturaCalidad] @FolioEntrada INT
	,@OrganizacionID INT
AS
BEGIN
	SELECT EntradaGanadoID
		,FolioEntrada
		,eg.OrganizacionID
		,o.Descripcion [Organizacion]
		,too.TipoOrganizacionID
		,too.Descripcion [TipoOrganizacion]
		,EG.OrganizacionOrigenID
		,FechaEntrada
		,EG.EmbarqueID
		,FolioOrigen
		,eg.FechaSalida
		,eg.CamionID
		,EG.ChoferID
		,EG.JaulaID
		,CabezasOrigen
		,CabezasRecibidas
		,EG.OperadorID
		,PesoBruto
		,PesoTara
		,EsRuteo
		,Fleje
		,CheckList
		,eg.CorralID
		,co.Codigo [Corral]
		,eg.LoteID
		,lo.Lote
		,Observacion
		,ImpresionTicket
		,Costeado
		,Manejado
		,eg.Activo
		,Op.Nombre + ' ' + Op.ApellidoPaterno + ' ' + Op.ApellidoMaterno AS NombreOperador
		,P.Descripcion AS Proveedor
		,P.ProveedorID
		,CASE 			
			WHEN (
					EXISTS (
						SELECT ''
						FROM EntradaGanadoCosteo egc
						WHERE egc.EntradaGanadoID = EG.EntradaGanadoID
							AND egc.Activo = 1
						)
					)
				THEN 2 --Mensaje que indica que la entrada de Ganado ya ha sido costeada
			WHEN (
					EXISTS (
						SELECT ''
						FROM EntradaGanadoCalidad egca
						WHERE egca.EntradaGanadoID = eg.EntradaGanadoID
						)
					)
				THEN 3 --Mensaje que indica que la calidad de ganado ya esta capturada
			ELSE 0 --Si cumple con todos regresa un 0 de Mensaje
			END AS MensajeRetorno
		,Em.Estatus
		,eg.CabezasMuertas
	FROM EntradaGanado eg
	INNER JOIN Organizacion o ON eg.OrganizacionOrigenID = o.OrganizacionID
	INNER JOIN TipoOrganizacion too ON o.TipoOrganizacionID = too.TipoOrganizacionID
	INNER JOIN Operador Op ON EG.OperadorID = Op.OperadorID
	LEFT JOIN Corral co ON eg.CorralID = co.CorralID
	LEFT JOIN Lote lo ON eg.LoteID = lo.LoteID
	INNER JOIN Embarque Em ON (EG.EmbarqueID = Em.EmbarqueID)
	INNER JOIN EmbarqueDetalle EmDet ON (
			Em.EmbarqueID = EmDet.EmbarqueID
			AND eg.OrganizacionID = EmDet.OrganizacionDestinoID
			)
	INNER JOIN Proveedor P ON (EmDet.ProveedorID = P.ProveedorID)
	WHERE FolioEntrada = @FolioEntrada
		AND eg.OrganizacionID = @OrganizacionID				
		AND eg.Activo = 1
	SELECT EC.EntradaCondicionID
		,EC.EntradaGanadoID
		,EC.CondicionID
		,EC.Cabezas
		,EC.Activo
		,C.Descripcion
	FROM EntradaCondicion EC
	INNER JOIN EntradaGanado EG ON EC.EntradaGanadoID = EG.EntradaGanadoID
	INNER JOIN Condicion C ON (EC.CondicionID = C.CondicionID)
	WHERE EG.FolioEntrada = @FolioEntrada
		AND EG.OrganizacionID = @OrganizacionID
		AND EC.Activo = 1
END

GO
