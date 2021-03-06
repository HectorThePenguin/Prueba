USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCabezasMuertas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ActualizarCabezasMuertas]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCabezasMuertas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 04/02/2014
-- Description:  Actualiza el N�mero de Cabezas Muertas
-- EntradaGanado_ActualizarCabezasMuertas
-- ===============================================================
CREATE PROCEDURE [dbo].[EntradaGanado_ActualizarCabezasMuertas]
@EntradaGanadoID INT
,@CabezasMuertas INT
,@UsuarioID INT
AS
update EntradaGanado SET CabezasMuertas = @CabezasMuertas, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioID
where EntradaGanadoID = @EntradaGanadoID
AND Activo = 1

GO
