IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[FechaAgregarHoraMaxima]')
		)
	DROP FUNCTION [dbo].[FechaAgregarHoraMaxima]
GO

-- =============================================  
-- Author:    José Gilberto Quintero López 
-- Create date: 02-04-2014  
-- Description:  Agrega la ultima hora a la fecha
-- select dbo.FechaAgregarHoraMaxima(getdate()) 
-- =============================================  
CREATE FUNCTION [dbo].[FechaAgregarHoraMaxima] (
	@Fecha datetime
	)
RETURNS datetime
AS
BEGIN
	DECLARE @resultado datetime
	set @resultado = dateadd(MILLISECOND, - 2, dateadd(hh, 24, convert(VARCHAR(10), @Fecha, 112)))

	RETURN isnull(@resultado, '19000101')
END
