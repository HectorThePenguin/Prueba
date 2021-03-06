USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerHistorialClinico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerHistorialClinico]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerHistorialClinico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 18/02/2014
-- Origen: APInterfaces
-- Description:	Obtiene el historial 
-- Enfermeria_ObtenerHistorialClinico '223746', 7, 9, 10, 1;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerHistorialClinico]
	@Arete VARCHAR(15),
	@TipoMovEntradaEnfermeria INT,
	@TipoMovEntradaSalidaEnfermeria INT,
	@TipoMovSalidaRecuperacion INT,
	@OrganizacionID INT
AS

BEGIN	

SET NOCOUNT ON;

	DECLARE @TipoMovCortePorTransferencia INT = 13;
	DECLARE @TipoMovCorte INT = 5;
	
		SELECT * 
		  FROM(
			SELECT DT.DeteccionAnimalID,
				AM.AnimalMovimientoID,
				AM.FechaMovimiento [FechaEntrada],
				CASE AM.TipoMovimientoID WHEN @TipoMovEntradaSalidaEnfermeria THEN AM.FechaMovimiento 
				ELSE (SELECT TOP 1 AMT.FechaMovimiento 
						FROM AnimalMovimiento AMT (NOLOCK)
					   WHERE AMT.AnimalID = AM.AnimalID 
						 AND AMT.TipoMovimientoID = @TipoMovSalidaRecuperacion)
				END AS [FechaSalida],
				AM.Peso,
				AM.Temperatura,
				ISNULL(DA.GradoID, GR.GradoID) GradoID,
				GR.Descripcion [DescripcionGrado],
				GR.NivelGravedad,
				CASE WHEN AM.TipoMovimientoID IN( @TipoMovCortePorTransferencia,@TipoMovCorte) THEN
					(	SELECT DISTINCT STUFF(CAST(AMD.TratamientoID AS VARCHAR(20)) + ', ', 1, 0,'')
						  FROM AlmacenMovimientoDetalle AMD
				         INNER JOIN TratamientoProducto TP ON TP.TratamientoID = AMD.TratamientoID
						 WHERE AMD.AlmacenMovimientoID = AMM.AlmacenMovimientoID 
						   AND TP.ProductoID IN (22,52)
						   AND TP.Activo = 1
						   FOR XML PATH('') )
				ELSE (	SELECT DISTINCT STUFF(CAST(TratamientoID AS VARCHAR(20)) + ', ', 1, 0,'')
						  FROM AlmacenMovimientoDetalle 
					     WHERE AlmacenMovimientoID = AMM.AlmacenMovimientoID 
 						   FOR XML PATH('') )
				END AS Tratamientos,
				CASE WHEN AM.TipoMovimientoID IN( @TipoMovCortePorTransferencia,@TipoMovCorte) THEN
					(	SELECT DISTINCT STUFF(CAST(T.CodigoTratamiento AS VARCHAR(20)) + ', ', 1, 0,'')
						  FROM AlmacenMovimientoDetalle AMD
				         INNER JOIN TratamientoProducto TP ON TP.TratamientoID = AMD.TratamientoID
						 INNER JOIN Tratamiento T ON T.TratamientoID = AMD.TratamientoID
						 WHERE AMD.AlmacenMovimientoID = AMM.AlmacenMovimientoID 
						   AND TP.ProductoID IN (22,52)
						   AND TP.Activo = 1
						   FOR XML PATH('') )
				ELSE (	SELECT DISTINCT STUFF(CAST(T.CodigoTratamiento AS VARCHAR(20)) + ', ', 1, 0,'')
						  FROM AlmacenMovimientoDetalle AMD
						  INNER JOIN Tratamiento T ON T.TratamientoID = AMD.TratamientoID
					     WHERE AMD.AlmacenMovimientoID = AMM.AlmacenMovimientoID 
 						   FOR XML PATH('') )
				END AS CodigoTratamientos
			 FROM Animal A(NOLOCK)
			 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
			 INNER JOIN AlmacenMovimiento AMM ON AMM.AnimalMovimientoID = AM.AnimalMovimientoID
 		     LEFT JOIN DeteccionAnimal DT ON DT.Arete = A.Arete AND DT.AnimalMovimientoID = AM.AnimalMovimientoID
			 LEFT OUTER JOIN DiagnosticoAnalista DA
				ON (DT.DeteccionAnimalID = DA.DeteccionAnimalID)
		     LEFT JOIN Grado GR ON GR.GradoID = ISNULL(DA.GradoID, DT.GradoID) AND GR.Activo = 1
			 WHERE A.Arete = @Arete
			   AND AM.TipoMovimientoID IN( @TipoMovEntradaEnfermeria, 
			                               @TipoMovEntradaSalidaEnfermeria, 
										   @TipoMovCortePorTransferencia,
										   @TipoMovCorte)
		    ) AS TratamientosAplicados
		 WHERE TratamientosAplicados.Tratamientos IS NOT NULL

SET NOCOUNT OFF;	

END
GO
