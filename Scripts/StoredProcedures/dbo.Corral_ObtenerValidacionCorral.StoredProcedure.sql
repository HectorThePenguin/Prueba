USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerValidacionCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerValidacionCorral]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerValidacionCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Francisco Alejo Pacheco
-- Create date: 22/11/2013
-- Origen: APInterfaces
-- Description: Validacion de la tabla  corral, Recibir como parametro OrganizacionID,Codigo Corral
-- Corral_ObtenerValidacionCorral
-- EXEC Corral_ObtenerValidacionCorral 1,'crl01'
-- =============================================
 CREATE PROCEDURE [dbo].[Corral_ObtenerValidacionCorral]
  @OrganizacionID AS INT,
  @Codigo AS VARCHAR(20)  
 AS
 BEGIN 
 SET NOCOUNT ON;
 DECLARE @CorralID AS INTEGER
 DECLARE @TipoCorral AS INTEGER
 DECLARE @Retorno AS INTEGER
 --ESTE VALOR ES CUANDO NO ENCUENTRA EL CORRAL
 SET @Retorno = -2
 set @CorralID = -2
 SELECT @CorralID = c.CorralID , @TipoCorral = c.TipoCorralID
 FROM Corral c 
 WHERE LTRIM (RTRIM (c.Codigo)) LIKE @Codigo 
 AND C.OrganizacionID = @OrganizacionID
 AND C.Activo = 1
 IF (@CorralID > -2)
 BEGIN
	 /* Se valida que el corral sea de tipo produccion */
	 IF (@TipoCorral = 2)
	 BEGIN
		--ESTE VALOR ES CUANDO TIENE UN LOTE ASIGNADO
		set @Retorno = -1
		SELECT @Retorno = c.CorralID
		FROM Lote l RIGHT JOIN Corral c 
		ON l.CorralID = c.CorralID
		AND l.Activo = 1
		WHERE l.LoteID IS NULL
		AND c.CorralID = @CorralID
		IF @Retorno > -1
		BEGIN
			--ESTE VALOR ES CUANDO YA HAY UN SERVICIO DE ALIMENTOS
			set @Retorno = 0
			SELECT @Retorno = c.CorralID
			FROM Corral c LEFT JOIN ServicioAlimento s ON c.CorralID = s.CorralID AND s.Activo = 1
			WHERE s.ServicioID IS NULL
			AND c.CorralID = @CorralID
		END
	END
	ELSE
		BEGIN
		/* El corral no es de tipo Produccion */
		  SET @Retorno = -3
		  SET @CorralID = -3
		END
 END
 SELECT @Retorno CorralID
 SET NOCOUNT OFF;
END

GO
