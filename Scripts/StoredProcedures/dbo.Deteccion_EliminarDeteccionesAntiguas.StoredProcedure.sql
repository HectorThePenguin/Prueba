USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Deteccion_EliminarDeteccionesAntiguas]    Script Date: 11/11/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Deteccion_EliminarDeteccionesAntiguas]
GO
/****** Object:  StoredProcedure [dbo].[Deteccion_EliminarDeteccionesAntiguas]    Script Date: 11/11/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jorge Luis Velazquez Araujo
-- Fecha: 11-11-2015
-- Descripcion:	Elimina las detecciones mayores a 3 dias que sigan activas
-- EXEC Deteccion_EliminarDeteccionesAntiguas
-- =============================================
CREATE PROCEDURE [dbo].[Deteccion_EliminarDeteccionesAntiguas]	
AS
BEGIN
	
	BEGIN
	UPDATE Deteccion
		   SET Activo = 0,
			   FechaModificacion = GETDATE(),
			   UsuarioModificacionID = 1
		 WHERE Activo = 1
		 and CAST(FechaDeteccion AS DATE) < CAST(GETDATE() -3 AS DATE)

		 UPDATE Deteccion
		   SET Activo = 0,
			   FechaModificacion = GETDATE(),
			   UsuarioModificacionID = 1
		 WHERE Activo = 1
		 and FotoDeteccion = ''
		 and EXISTS (select 1 from  AnimalMovimiento am
							inner join Animal a on am.AnimalID = a.AnimalID
							inner join Lote lo on am.LoteID = lo.LoteID
							where a.Arete = Deteccion.Arete
							and am.LoteID <> Deteccion.LoteID
							and am.Activo = 1
							and lo.TipoCorralID <> 1)
	END
	
END

GO
