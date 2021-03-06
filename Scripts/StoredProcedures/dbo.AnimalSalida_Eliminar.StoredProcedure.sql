USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalSalida_Eliminar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalSalida_Eliminar]
GO
/****** Object:  StoredProcedure [dbo].[AnimalSalida_Eliminar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: C�sar Valdez
-- Fecha: 2014-05-22
-- Origen: APInterfaces
-- Descripci�n:	Elimana el animal de Animal Salida
-- EXEC AnimalSalida_Eliminar 15,64
-- =============================================
CREATE PROCEDURE [dbo].[AnimalSalida_Eliminar]
	@AnimalID INT,
	@LoteID INT
AS
BEGIN
	DELETE
	  FROM AnimalSalida 
	 WHERE AnimalID = @AnimalID
	   AND LoteID = @LoteID 
END

GO
