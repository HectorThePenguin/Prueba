USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ActualizaTraspasoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ActualizaTraspasoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[ActualizaTraspasoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author: Ernesto Cardenas LLanes      
-- Create date: 15 Abr 2015      
-- Description: Actualiza tabla trapaso detalle
-- =============================================  
Create Procedure [dbo].[ActualizaTraspasoDetalle]
	@prmTrapasoDetalleId Int,
	@prmCabezas Int
As
Begin

	Update InterfaceSalidaTraspasoDetalle
	Set CabezasPorSacrificar = IsNull(CabezasPorSacrificar,0) - @prmCabezas
	Where InterfaceSalidaTraspasoDetalleID = @prmTrapasoDetalleId

End

GO
