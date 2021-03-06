USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_RegistrarBitacora]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_RegistrarBitacora]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_RegistrarBitacora]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_RegistrarBitacora 1013758
create procedure [dbo].[LoteSacrificioLucero_RegistrarBitacora]
	@animalID bigint
	, @loteSacrificioId int
	, @intensivo bit
	, @mensaje varchar(8000)
	, @pila varchar(8000)
	, @conExito bit = 0
as
begin
	
	insert into LoteSacrificioBitacora (
		LoteSacrificioID
		, AnimalID
		, Intensivo
		, Mensaje
		, Pila
		, ConExito
	)
	select 
		@loteSacrificioId
		, @animalID
		, @intensivo
		, @mensaje
		, @pila
		, @conExito

end


/*

create table LoteSacrificioBitacora (
	LoteSacrificioBitacoraID bigint identity(1,1) constraint PK_LoteSacrificioBitacora_LoteSacrificioBitacoraID primary key
	, LoteSacrificioID int not null
	, AnimalID bigint not null
	, Intensivo bit not null
	, Mensaje varchar(8000) not null
	, Pila varchar(8000) not null
	, ConExito bit not null
	, FechaRegistro DateTime not null constraint DF_LoteSacrificioBitacora_FechaRegistro default GETDATE()
)

*/
GO
