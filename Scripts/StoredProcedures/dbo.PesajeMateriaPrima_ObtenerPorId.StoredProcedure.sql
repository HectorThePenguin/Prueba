USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorId]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Ramses Santos
-- Create date: 22/08/2014
-- Description:  Obtener el pesaje por Id
-- PesajeMateriaPrima_ObtenerPorId 1
-- =============================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorId]
	@PesajeMateriaPrimaId INT
AS
  BEGIN
      SET NOCOUNT ON;

      SELECT
			[PesajeMateriaPrimaID] 
		   ,[ProgramacionMateriaPrimaID]
           ,[ProveedorChoferID]
           ,[Ticket]
           ,[CamionID]
           ,[PesoBruto]
           ,[PesoTara]
           ,[Piezas]
           ,[TipoPesajeID]
           ,[UsuarioIDSurtido]
           ,[FechaSurtido]
           ,[UsuarioIDRecibe]
           ,[FechaRecibe]
           ,[EstatusID]
           ,[Activo]
		   ,[FechaCreacion]
		   ,[UsuarioCreacionID]
		   ,AlmacenMovimientoOrigenID
		   ,AlmacenMovimientoDestinoID
      FROM PesajeMateriaPrima (NOLOCK)
      WHERE PesajeMateriaPrimaID = @PesajeMateriaPrimaId

      SET NOCOUNT OFF;
  END

GO
