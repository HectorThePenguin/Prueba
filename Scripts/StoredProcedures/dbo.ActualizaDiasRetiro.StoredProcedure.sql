USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ActualizaDiasRetiro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ActualizaDiasRetiro]
GO
/****** Object:  StoredProcedure [dbo].[ActualizaDiasRetiro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=================================================================
-- Author     : Bladimir Garcia
-- Create date: 16/09/2014 
-- Origen     : AP Interfaces
-- Description: Actualiza Dias de Retiro para Corral a Sacrifico
-- SpName     : EXEC ActualizaDiasRetiro '20',6,9,0
--=================================================================
CREATE PROCEDURE [dbo].[ActualizaDiasRetiro]
 @Codigo Varchar(3),
 @DiasActual Int,
 @DiasRetiro Int,
 @RepartoId Int
AS
BEGIN

	 -- Obtiene el Reparto para Moidicar Fecha
	 SELECT @RepartoId = RepartoID FROM Reparto 
	 WHERE LoteID IN
		   (Select LoteID from lote where corralid IN (Select CorralID from corral where codigo = @Codigo)) And 
		   OrganizacionID = 1 AND Activo = 1 And Convert(char(10),Fecha,103) = CONVERT (char(10), getdate()- @DiasActual, 103) 
	 
	 -- Verificar si obtuvo Resultados      
	 IF @@ROWCOUNT > 0 
		-- Actualizar Fecha de Reparto para Agregar Dias de Retiro
		   UPDATE Reparto SET fecha = getdate()- @DiasRetiro --> MODIFICAR SEGUN LOS DIAS DE RETIRO
		   WHERE repartoId = @RepartoId  --> MODIFICAR EL ULTIMO REPARTO
	 ELSE
		 Select 'No Hay Reparto para Modificar para ese Dia'  
	 	 
END  
GO
