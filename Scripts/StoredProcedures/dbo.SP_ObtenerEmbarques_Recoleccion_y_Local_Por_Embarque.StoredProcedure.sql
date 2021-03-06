USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local_Por_Embarque]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local_Por_Embarque]
GO
/****** Object:  StoredProcedure [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local_Por_Embarque]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_ObtenerEmbarques_Recoleccion_y_Local_Por_Embarque]
	@dFechaSurtido INT,
	@nEmbarque INT
AS
BEGIN		SELECT DISTINCT			nEmbarque, 			dFechaSurtido, 			nCanalOrigen, 			nOrdenCarga, 			nTurno, 			dTiempoInicio, 			dFechaCita, 			dHoraCita, 			nTipoEmbarque, 			cDestino, 			cChofer, 			cNumeroCaja, 			nCapacidad, 			nTemperatura,			nPresupuestado,			nCanalRecoleccion, 			bIntegrado,			DFechaSalida,			DFechaCitaCliente,		    proveedor = IsNull(pr.cRazonSocial,'no tiene'),		    ruta = IsNull(cDescripcion_Ruta,'sin ruta')		FROM TLM_Embarques e (NOLOCK) 		Left Outer Join CTL_Proveedores (Nolock) pr	    On   pr.nFolio = Case When ISNUMERIC(e.cchofer) = 1 Then e.cchofer Else 0 End	    Left Outer Join  dbo.Ctl_Distancias_Origen_Destino rd (NOLOCK) 	    On   rd.cClave_Ruta = e.cDestino		WHERE dbo.ADSUM_NumeroJuliano(dFechaSurtido) = @dFechaSurtido AND nEmbarque = @nEmbarque		 UNION All 		SELECT DISTINCT			nEmbarque, 			dFechaSurtido, 			nCanalOrigen, 			nOrdenCarga, 			nTurno, 			dTiempoInicio, 			dFechaCita, 			dHoraCita, 			nTipoEmbarque, 			cDestino, 			cChofer, 			cNumeroCaja, 			nCapacidad, 			nTemperatura,			nPresupuestado, 			nCanalRecoleccion, 			0 As bIntegrado,			DFechaSalida = Getdate(),			DFechaCitaCliente = Getdate(),		    proveedor = IsNull(pr.cRazonSocial,'no tiene'),		    ruta = IsNull(cDescripcion_Ruta,'sin ruta')		FROM TLM_EmbarquesRecoleccion e (NOLOCK)		Left Outer Join CTL_Proveedores (Nolock) pr	    On   pr.nFolio = Case When ISNUMERIC(e.cchofer) = 1 Then e.cchofer Else 0 End	    Left Outer Join  dbo.Ctl_Distancias_Origen_Destino rd (NOLOCK) 	    On   rd.cClave_Ruta = e.cDestino 		WHERE e.bActivo = 1 AND dbo.ADSUM_NumeroJuliano(dFechaSurtido) = @dFechaSurtido AND nEmbarque = @nEmbarque

END	

GO
