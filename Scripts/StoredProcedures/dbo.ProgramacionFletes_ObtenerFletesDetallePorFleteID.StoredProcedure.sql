USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerFletesDetallePorFleteID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionFletes_ObtenerFletesDetallePorFleteID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionFletes_ObtenerFletesDetallePorFleteID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 13/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProgramacionFletes_ObtenerFletesDetallePorFleteID 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionFletes_ObtenerFletesDetallePorFleteID]
@Activo BIT,
@FleteID INT
AS
BEGIN
	SET NOCOUNT ON;
			SELECT	F.FleteID,
				F.ContratoID,
				F.Activo,
				FD.FleteDetalleID,
				FD.CostoID,
				FD.Tarifa,
				C.Descripcion
		FROM Flete F
		INNER JOIN FleteDetalle AS FD ON FD.FleteID=F.FleteID
		INNER JOIN Costo AS C ON C.CostoID=FD.CostoID
		WHERE F.FleteID=@FleteID
		AND F.Activo=@Activo
		AND FD.FleteID=@FleteID
		AND FD.Activo=@Activo
	SET NOCOUNT OFF;
END

GO
