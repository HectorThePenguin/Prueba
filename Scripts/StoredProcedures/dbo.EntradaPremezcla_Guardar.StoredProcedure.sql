USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaPremezcla_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaPremezcla_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaPremezcla_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : C�sar Valdez
-- Create date: 2014/10/06
-- Description: Almacena un registro en la tabla EntradaPremezcla
-- SpName     : EntradaPremezcla_Guardar 4,'2014-07-14 09:33:00','2014-07-14 09:33:00',1,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaPremezcla_Guardar]
	@AlmacenMovimientoIDEntrada BIGINT,
	@AlmacenMovimientoIDSalida BIGINT,
	@UsuarioCreacionID INT
AS
BEGIN
	INSERT INTO EntradaPremezcla(AlmacenMovimientoIDEntrada,AlmacenMovimientoIDSalida,Activo,FechaCreacion,UsuarioCreacionID) 
	VALUES (@AlmacenMovimientoIDEntrada,@AlmacenMovimientoIDSalida,1,GETDATE(),@UsuarioCreacionID)
END

GO
