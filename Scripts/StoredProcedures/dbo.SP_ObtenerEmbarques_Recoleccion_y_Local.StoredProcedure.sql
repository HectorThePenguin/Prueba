USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local]
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local]
	@dFechaSurtido INT
AS
BEGIN
	SET NOCOUNT ON;	SELECT	    nEmbarque,		dFechaSurtido,		nCanalOrigen,		nOrdenCarga,		nTurno,		dTiempoInicio,		dFechaCita,		dHoraCita,		nTipoEmbarque, 		cDestino, 		cChofer, 		cNumeroCaja, 		nCapacidad, 		nTemperatura,		nPresupuestado,		nCanalRecoleccion, 		bIntegrado,		DFechaSalida,		DFechaCitaCliente,		proveedor = pr.cRazonSocial,		ruta = cDescripcion_Ruta	FROM TLM_Embarques (NOLOCK) e	Left Outer Join CTL_Proveedores (Nolock) pr	On   pr.nFolio = Case When ISNUMERIC(e.cchofer) = 1 Then e.cchofer Else 0 End	Left Outer Join  dbo.Ctl_Distancias_Origen_Destino rd (NOLOCK) 	On   rd.cClave_Ruta = e.cDestino	WHERE dbo.ADSUM_NumeroJuliano(dFechaSurtido) = @dFechaSurtido	UNION	SELECT		nEmbarque, 		dFechaSurtido, 		nCanalOrigen, 		nOrdenCarga, 		nTurno, 		dTiempoInicio, 		dFechaCita, 		dHoraCita, 		nTipoEmbarque, 		cDestino, 		cChofer, 		cNumeroCaja, 		nCapacidad, 		nTemperatura,		nPresupuestado, 		nCanalRecoleccion, 		0 As bIntegrado,		DFechaSalida = Null,		DFechaCitaCliente = Null,		proveedor = pr.cRazonSocial,		ruta = cDescripcion_Ruta	FROM TLM_EmbarquesRecoleccion ER (NOLOCK)	Left Outer Join CTL_Proveedores (Nolock) pr	On   pr.nFolio = Case When ISNUMERIC(er.cchofer) = 1 Then er.cchofer Else 0 End	Left Outer Join  dbo.Ctl_Distancias_Origen_Destino rd (NOLOCK) 	On   rd.cClave_Ruta = er.cDestino	WHERE er.bActivo = 1 AND  dbo.ADSUM_NumeroJuliano(dFechaSurtido) = @dFechaSurtido AND	NOT EXISTS (SELECT * FROM TLM_Embarques E (NOLOCK)
	            WHERE ER.nEmbarque = E.nEmbarque AND dbo.ADSUM_NumeroJuliano(E.dFechaSurtido) = @dFechaSurtido)		SET NOCOUNT OFF;

END	

GO
