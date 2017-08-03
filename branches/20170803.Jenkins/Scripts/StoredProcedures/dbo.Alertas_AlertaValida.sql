--=============================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 18/03/2016
-- Description: Obtiene todas las alertas previamente asignadas a un modulo y con la descripcion proporcionada
-- Alertas_AlertaValida
--=============================================
CREATE PROCEDURE [dbo].[Alertas_AlertaValida] 
@ModuloID int,
@Descripcion varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 1
  	FROM Alerta 
  	WHERE ModuloID=@ModuloID AND RTRIM(Descripcion)=RTRIM(@Descripcion)
	SET NOCOUNT OFF;
END