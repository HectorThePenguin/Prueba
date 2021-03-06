USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaAreteRecepcionSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ConsultaAreteRecepcionSalida]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaAreteRecepcionSalida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pedro Delgado
-- Create date: 13/03/2014
-- Description: Consulta un arete en la tabla inserfacesalidaanimal
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ConsultaAreteRecepcionSalida]
@SalidaID INT,
@Arete VARCHAR(15)
AS
BEGIN
	SELECT 
		Arete
	FROM InterfaceSalida (NOLOCK) ISA
	INNER JOIN InterfaceSalidaDetalle (NOLOCK) ISD
	ON (ISA.SalidaID = ISD.SalidaID)
	INNER JOIN InterfaceSalidaAnimal (NOLOCK) ISAN
	ON (ISD.SalidaID = ISAN.SalidaID)
	WHERE ISD.SalidaID = @SalidaID AND Arete = @Arete
END

GO
