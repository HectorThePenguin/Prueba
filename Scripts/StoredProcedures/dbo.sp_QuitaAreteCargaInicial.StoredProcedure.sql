USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_QuitaAreteCargaInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_QuitaAreteCargaInicial]
GO
/****** Object:  StoredProcedure [dbo].[sp_QuitaAreteCargaInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_QuitaAreteCargaInicial] (@Arete varchar(15))
AS
BEGIN

DECLARE @AreteConversion varchar(15)
--SET @Arete = '1982089'

SELECT @AreteConversion = a.Arete
FROM Animal a
WHERE a.Arete = @Arete
AND a.UsuarioCreacionID = 1

SET @AreteConversion = '9'+@AreteConversion

IF (SELECT a.Arete FROM Animal a WHERE a.Arete = @AreteConversion) IS NULL
    UPDATE Animal SET Arete = @AreteConversion WHERE Arete = @Arete
ELSE
    PRINT 'El arete ' + @AreteConversion + ' ya existe en el inventario, ya fue reemplazado una vez'
END   







GO
