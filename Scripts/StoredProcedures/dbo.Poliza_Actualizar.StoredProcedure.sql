USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author     : Audomar Piña
-- Create date: 29/05/2015  
-- Description:  Actualiza XML de Poliza  
-- =============================================  
CREATE PROCEDURE [dbo].[Poliza_Actualizar]  
@PolizaID   INT  
, @XmlPoliza  XML  
, @OrganizacionID INT  
, @Estatus   BIT  = NULL
AS  
BEGIN  
  
 UPDATE Poliza  
 SET XmlPoliza = @XmlPoliza  
  , FechaModificacion = GETDATE()  
 WHERE PolizaID = @PolizaID  
  
END  
GO
