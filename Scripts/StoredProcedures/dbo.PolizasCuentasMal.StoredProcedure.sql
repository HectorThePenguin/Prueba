USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PolizasCuentasMal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PolizasCuentasMal]
GO
/****** Object:  StoredProcedure [dbo].[PolizasCuentasMal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PolizasCuentasMal]
AS
BEGIN

	SET NOCOUNT ON

		SELECT P.PolizaID
			,  P.FechaCreacion
			,  P.Mensaje
			,  P.TipoPolizaID
		FROM Poliza P
		WHERE  
			Procesada = 0
			AND LEN(Mensaje) > 0
			AND Mensaje LIKE '%cuenta%mayor%'
			AND CAST(FechaCreacion AS DATE) >= CAST('20150607' AS DATE)
			AND Estatus = 1			
		ORDER BY PolizaID

	SET NOCOUNT OFF

END

GO
