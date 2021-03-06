USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaSacrificio_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaSacrificio_Crear]
GO
/****** Object:  StoredProcedure [dbo].[SalidaSacrificio_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Jos� Gilberto Quintero L�pez  
-- Create date: 31/07/2014 12:00:00 a.m.  
-- Description:   
-- SpName     : SalidaSacrificio_Crear  
--======================================================  
CREATE PROCEDURE [dbo].[SalidaSacrificio_Crear]  
	@FEC_SACR varchar(30),  
	@NUM_SALI varchar(6),  
	@NUM_CORR varchar(3),  
	@NUM_PRO varchar(4),  
	@FEC_SALC varchar(30),  
	@HORA_SAL varchar(10),  
	@EDO_COME char(1),  
	@NUM_CAB int,  
	@TIP_ANI char(1),  
	@KGS_SAL int,  
	@PRECIO int,  
	@ORIGEN char(1),  
	@CTA_PROVIN varchar(13),  
	@PRE_EST int,  
	@ID_SALIDA_SACRIFICIO int,
	@VENTA_PARA varchar(50),  
	@COD_PROVEEDOR varchar(50),  
	@NOTAS varchar(1024),  
	@COSTO_CABEZA varchar(30),  
	@CABEZAS_PROCESADAS int,  
	@FICHA_INICIO int,  
	@COSTO_CORRAL varchar(30),  
	@UNI_ENT varchar(30),  
	@UNI_SAL varchar(30),  
	@SYNC char(1),  
	@ID_S int,  
	@SEXO int,  
	@DIAS_ENG varchar(30),  
	@FOLIO_ENTRADA_I varchar(30),  
	@ORIGEN_GANADO char(1),
	@OrdenSacrificioID int	
AS  
BEGIN  
	SET NOCOUNT ON;  
	INSERT SALIDA_SACRIFICIO (
		FEC_SACR,  
		NUM_SALI,  
		NUM_CORR,  
		NUM_PRO,  
		FEC_SALC,  
		HORA_SAL,  
		EDO_COME,  
		NUM_CAB,  
		TIP_ANI,  
		KGS_SAL,  
		PRECIO,  
		ORIGEN,  
		CTA_PROVIN,  
		PRE_EST,  
		ID_SALIDA_SACRIFICIO,
		VENTA_PARA,  
		COD_PROVEEDOR,  
		NOTAS,  
		COSTO_CABEZA,  
		CABEZAS_PROCESADAS,  
		FICHA_INICIO,  
		COSTO_CORRAL,  
		UNI_ENT,  
		UNI_SAL,  
		SYNC,  
		ID_S,  
		SEXO,  
		DIAS_ENG,  
		FOLIO_ENTRADA_I,  
		ORIGEN_GANADO,
		OrdenSacrificioID
	)  
	VALUES(  
		@FEC_SACR,  
		@NUM_SALI,  
		@NUM_CORR,  
		@NUM_PRO,  
		@FEC_SALC,  
		@HORA_SAL,  
		@EDO_COME,  
		@NUM_CAB,  
		@TIP_ANI,  
		@KGS_SAL,  
		@PRECIO,  
		@ORIGEN,  
		@CTA_PROVIN,  
		@PRE_EST,  
		@ID_SALIDA_SACRIFICIO,
		@VENTA_PARA,  
		@COD_PROVEEDOR,  
		@NOTAS,  
		@COSTO_CABEZA,  
		@CABEZAS_PROCESADAS,  
		@FICHA_INICIO,  
		@COSTO_CORRAL,  
		@UNI_ENT,  
		@UNI_SAL,  
		@SYNC,  
		@ID_S,  
		@SEXO,  
		@DIAS_ENG,  
		@FOLIO_ENTRADA_I,  
		@ORIGEN_GANADO,
		@OrdenSacrificioID
	)  
	SET NOCOUNT OFF;  
END


GO
