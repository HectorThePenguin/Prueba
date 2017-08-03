USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerCostoFlete]    Script Date: 27/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerCostoFlete]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerCostoFlete]    Script Date: 27/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 27-06-2017
-- Description: sp para obtener el costo flete de una ruta
-- SpName     : ProgramacionEmbarque_ObtenerCostoFlete 35, 376, 1
--======================================================  
CREATE PROCEDURE ProgramacionEmbarque_ObtenerCostoFlete
@ConfiguracionEmbarqueDetalleID INT,
@ProveedorID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		ET.EmbarqueTarifaID,
		ET.ImporteTarifa
	FROM EmbarqueTarifa AS ET (NOLOCK)
	WHERE ET.ConfiguracionEmbarqueDetalleID = @ConfiguracionEmbarqueDetalleID AND ET.ProveedorID = @ProveedorID AND Activo = @Activo
	SET NOCOUNT OFF;
END