USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ConsultaAreteVentaDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_ConsultaAreteVentaDetalle]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ConsultaAreteVentaDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 04/03/2014
-- Description:	Obtiene si existe el arete en ventaganadodetalle
/*
	SalidaIndividualVenta_ConsultaAreteVentaDetalle '48400406522752'
*/
--======================================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_ConsultaAreteVentaDetalle]
@Arete VARCHAR(15),
@AreteMetalico VARCHAR(15)
AS
BEGIN
	DECLARE @AnimalID INT
	IF (LEN(@Arete) > 0)
		BEGIN 
			SELECT TOP 1 Arete
			FROM VentaGanadoDetalle (NOLOCK)
			WHERE Arete = @Arete AND Activo = 1
		END
	ELSE
		BEGIN
			IF ( LEN(@AreteMetalico) > 0) 
				BEGIN
					SET @AnimalID = (SELECT TOP 1 AnimalID FROM animal WHERE areteMetalico = @AreteMetalico and Activo = 1)
					IF(@AnimalID IS NOT NULL AND LEN(@AnimalID) > 0)
					BEGIN
						SELECT TOP 1 Arete
						FROM VentaGanadoDetalle (NOLOCK)
						WHERE AnimalID = @AnimalID AND Activo = 1
					END
				END
		END
END

GO
