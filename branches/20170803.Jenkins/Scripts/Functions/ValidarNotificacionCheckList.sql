IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ValidarNotificacionCheckList]')
		)
	DROP FUNCTION [dbo].[ValidarNotificacionCheckList]
GO

CREATE FUNCTION dbo.ValidarNotificacionCheckList (
	@FechaCheckList DATETIME
	,@FechaActual DATETIME
	)
RETURNS BIT
AS
BEGIN

	declare @Encontrado BIT
		declare @Ciclos tinyint
		declare @DiferenciaMinutos INT
		
		SET @Encontrado = 0
		set @Ciclos = 1
		
		declare @Minutos int
		set @Minutos = 0
		
		while(@Ciclos < 8)
		begin
			set @DiferenciaMinutos = DATEDIFF(MI, @FechaCheckList, @FechaActual)
			set @Minutos = @Ciclos * 60
			set @Ciclos = @Ciclos + 1
			if(DATEADD(minute,@Minutos,@FechaCheckList) > @FechaActual)
			begin
				set @Ciclos = 9
			end			 
			
				if(@DiferenciaMinutos > (@Minutos -5) AND @DiferenciaMinutos <= @Minutos)
			begin
				set @Encontrado = 1
				set @Ciclos = 9
			end
			
			if(@DiferenciaMinutos < @Minutos)
			begin
				set @Ciclos = 9
			end
			
		end
		

	RETURN @Encontrado
END
