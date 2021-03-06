USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[BitacoraSacrificios_Registrar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[BitacoraSacrificios_Registrar]
GO
/****** Object:  StoredProcedure [dbo].[BitacoraSacrificios_Registrar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[BitacoraSacrificios_Registrar]
	@loteid int
	, @fecha datetime
	, @loteSacrificioId int
	, @estatus int
	, @mensaje varchar(4000)
	, @uid uniqueidentifier
AS
BEGIN
	Insert into BitacoraSacrificios(LoteID, Fecha, LoteSacrificioId, Estatus, Mensaje, Transaccion)
	select @loteid, @fecha, @loteSacrificioId, @estatus, @mensaje, @uid
END

GO
