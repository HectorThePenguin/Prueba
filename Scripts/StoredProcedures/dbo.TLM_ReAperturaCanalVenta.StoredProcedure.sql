USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TLM_ReAperturaCanalVenta]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TLM_ReAperturaCanalVenta]
GO
/****** Object:  StoredProcedure [dbo].[TLM_ReAperturaCanalVenta]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TLM_ReAperturaCanalVenta](@Canal INT)          
AS          
BEGIN   
	SET NOCOUNT ON  
	-- Autor: Sergio Alberto Gámez Gómez [27/08/2014]  
	-- Aplicación: Abrir Canal Venta, (M05-Telemarketing)  
	-- Motivo: Reaperturar canal para poder realizar pedidos.  

	DECLARE @DiaCerrado BIT
	DECLARE @RespInt INT
	DECLARE @RespString VARCHAR(200)
	
	IF EXISTS(SELECT bDiaCerrado FROM TLV_CierreDeDia (NOLOCK) WHERE nCanalDistribucion = @Canal AND CONVERT(VARCHAR,dFecha,112) > CONVERT(VARCHAR,GETDATE(),112))
	BEGIN
		SELECT TOP 1 @DiaCerrado = bDiaCerrado 
		FROM TLV_CierreDeDia (NOLOCK) 
		WHERE nCanalDistribucion = @Canal AND CONVERT(VARCHAR,dFecha,112) > CONVERT(VARCHAR,GETDATE(),112)
		ORDER BY dFecha ASC
		
		IF @DiaCerrado = 1
		BEGIN
			UPDATE TLV_CierreDeDia 
			SET bDiaCerrado = 0 
			WHERE nCanalDistribucion = @Canal AND CONVERT(VARCHAR,dFecha,112) > CONVERT(VARCHAR,GETDATE(),112)
			SET @RespInt = 1
			SET @RespString = 'El canal ' + CAST(@Canal AS VARCHAR) + ' ha sido abierto satisfactoriamente.'
		END
		ELSE
		BEGIN
			SET @RespInt = 2
			SET @RespString = 'El canal ' + CAST(@Canal AS VARCHAR) + ' se encuentra abierto, favor de verificar.'
		END
	END
	ELSE
	BEGIN
		SET @RespInt = 0
		SET @RespString = 'No existe registro de cierre para el día siguiente, favor de verificar.'
	END

	SELECT @RespInt As Datos, RTRIM(LTRIM(@RespString)) As Mensaje
	 
	SET NOCOUNT OFF  
END

GO
