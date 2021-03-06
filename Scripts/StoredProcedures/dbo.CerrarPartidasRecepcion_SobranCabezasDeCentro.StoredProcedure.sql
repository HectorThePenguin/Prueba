USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CerrarPartidasRecepcion_SobranCabezasDeCentro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CerrarPartidasRecepcion_SobranCabezasDeCentro]
GO
/****** Object:  StoredProcedure [dbo].[CerrarPartidasRecepcion_SobranCabezasDeCentro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Fecha: 2014-10-21
-- Descripción:	Se cierran partidas donde las cabezas de centro son mayores a las q traemos en el inventario
-- EXEC CerrarPartidasRecepcion_SobranCabezasDeCentro 
-- =============================================
CREATE PROCEDURE [dbo].[CerrarPartidasRecepcion_SobranCabezasDeCentro]
AS
BEGIN	

	DECLARE @FolioEntrada INT, @CabezasRecibidas INT, @AnimalesIventario INT, @AnimalesCentro INT;
	DECLARE @AnimalMovimientoID BIGINT, @Peso INT, @Sexo CHAR(1), @CabezasRequeridas INT;
	DECLARE @FolioAlmacen BIGINT, @AlmacenID INT, @TipoMovimientoID INT, @AlmacenMovimientoID BIGINT, @AnimalID BIGINT;
	DECLARE @sql NVarChar(1500)
	
	SET @AlmacenID = 1;
	SET @TipoMovimientoID = 12;	
	
	-- Declaración del cursor   /* Validar partidas que no se encentran completas con tra la informacion de centros(interface)  */
	DECLARE curFolioEntrada1 CURSOR FOR 		
		SELECT Inventario.FolioEntrada, Inventario.CabezasRecibidas, Inventario.AnimalesIventario, Centro.AnimalesCentro  
		FROM (
			SELECT EI.FolioEntrada, EI.CabezasRecibidas, COUNT(*) AS AnimalesIventario
			FROM EntradasIncompletas EI
			INNER JOIN EntradaGanado EG ON EG.FolioEntrada = EI.FolioEntrada
			INNER JOIN Animal A(NOLOCK) ON A.FolioEntrada = EG.FolioEntrada
			WHERE Cerrado = 0
			-- AND EI.FolioEntrada IN (SELECT FolioEntrada FROM EntradasIncompletas)  --  (1019)
			GROUP BY EI.FolioEntrada, EI.CabezasRecibidas) AS Inventario
		INNER JOIN 
		 (SELECT EI.FolioEntrada, COUNT(*) AS AnimalesCentro
			FROM EntradasIncompletas EI
			INNER JOIN EntradaGanado EG ON EG.FolioEntrada = EI.FolioEntrada
			INNER JOIN InterfaceSalida ISs ON ISs.SalidaID = EG.FolioOrigen 
										AND ISs.OrganizacionID = EG.OrganizacionOrigenID
									--	AND ISs.OrganizacionDestinoID = 1
			INNER JOIN InterfaceSalidaAnimal ISA ON ISA.SalidaID = EG.FolioOrigen 
										AND ISA.OrganizacionID = EG.OrganizacionOrigenID
			WHERE  Cerrado = 0
		    -- AND EI.FolioEntrada IN (SELECT FolioEntrada FROM EntradasIncompletas)  --  (1019)
				AND ISA.Arete NOT IN ( SELECT Arete FROM Animal(NOLOCK) )
			GROUP BY EI.FolioEntrada, EI.CabezasRecibidas) AS Centro ON Centro.FolioEntrada = Inventario.FolioEntrada
		WHERE Inventario.CabezasRecibidas <= (Inventario.AnimalesIventario+Centro.AnimalesCentro);
		  -- AND Inventario.FolioEntrada = 1049;
		
	CREATE TABLE #tmpAnimalesNuevos (Arete VARCHAR(15));
	
	-- Apertura del cursor
	OPEN curFolioEntrada1
	-- Lectura de la primera fila del cursor
		FETCH NEXT FROM curFolioEntrada1 INTO @FolioEntrada, @CabezasRecibidas, @AnimalesIventario, @AnimalesCentro
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
			
				SET @CabezasRequeridas = @CabezasRecibidas-@AnimalesIventario;
				
				-- SELECT @FolioEntrada, @CabezasRecibidas, @AnimalesIventario, @AnimalesCentro;
				DELETE FROM #tmpAnimalesNuevos;
				/* Aretes nuevos a insertar */
				/* Aretes nuevos a insertar */
				SET @sql= CONCAT(' INSERT INTO #tmpAnimalesNuevos ',
						         ' SELECT TOP ' , @CabezasRequeridas ,' Arete = ISA.Arete',
						' FROM EntradasIncompletas EI',
						' INNER JOIN EntradaGanado EG ON EG.FolioEntrada = EI.FolioEntrada',
						' INNER JOIN InterfaceSalida ISs ON ISs.SalidaID = EG.FolioOrigen ',
													' AND ISs.OrganizacionID = EG.OrganizacionOrigenID',
													' AND ISs.OrganizacionDestinoID = 1',
						' INNER JOIN InterfaceSalidaAnimal ISA ON ISA.SalidaID = EG.FolioOrigen', 
															' AND ISA.OrganizacionID = EG.OrganizacionOrigenID',
						' WHERE EI.FolioEntrada IN (' , @FolioEntrada , ')',  --  (1019)
						' AND ISA.Arete NOT IN ( SELECT Arete FROM Animal(NOLOCK) )');
			
				EXEC sp_executesql @sql
				
				/*
				INSERT INTO @tmpAnimalesNuevos
				SELECT Arete = ISA.Arete
				FROM EntradasIncompletas EI
				INNER JOIN EntradaGanado EG ON EG.FolioEntrada = EI.FolioEntrada
				INNER JOIN InterfaceSalida ISs ON ISs.SalidaID = EG.FolioOrigen 
											AND ISs.OrganizacionID = EG.OrganizacionOrigenID
											AND ISs.OrganizacionDestinoID = 1
				INNER JOIN InterfaceSalidaAnimal ISA ON ISA.SalidaID = EG.FolioOrigen 
													AND ISA.OrganizacionID = EG.OrganizacionOrigenID
				WHERE EI.FolioEntrada IN (@FolioEntrada)  --  (1019)
				AND ISA.Arete NOT IN ( SELECT Arete FROM Animal );*/
				
				
					
			    /* Insert animales Animal */
				 INSERT INTO Animal
				SELECT Arete = ISA.Arete,
							AreteMetalico = '',
							FechaCompra = ISA.FechaCompra,
							TipoGanadoID = ISA.TipoGanadoID,
							CalidadGanadoID = 3 , -- CEBU
							ClasificacionGanadoID = 4, -- Normal
							PesoCompra = ISA.PesoCompra,
							OrganizacionIDEntrada = EG.OrganizacionID ,
							FolioEntrada = EI.FolioEntrada,
							PesoLlegada = 0,
							Paletas = 0,
							CausaRechadoID = NULL,
							Venta = 0,
							Cronico = 0,
							CambioSexo = 0,
							Activo = 1,
							FechaCreacion = GETDATE(),
							UsuarioCreacionID = 1,
							FechaModificacion = NULL,
							UsuarioModificacionID = NULL
					FROM EntradasIncompletas EI
					INNER JOIN EntradaGanado EG ON EG.FolioEntrada = EI.FolioEntrada
					INNER JOIN InterfaceSalida ISs ON ISs.SalidaID = EG.FolioOrigen 
												AND ISs.OrganizacionID = EG.OrganizacionOrigenID
												AND ISs.OrganizacionDestinoID = 1
					INNER JOIN InterfaceSalidaAnimal ISA ON ISA.SalidaID = EG.FolioOrigen 
														AND ISA.OrganizacionID = EG.OrganizacionOrigenID
					WHERE EI.FolioEntrada IN (@FolioEntrada)  --  (1019)
					AND ISA.Arete IN ( SELECT Arete FROM #tmpAnimalesNuevos );
			
				/* Insert animales AnimalMovimiento */
				 INSERT INTO AnimalMovimiento
				SELECT AnimalID = A.AnimalID,
							OrganizacionID = EG.OrganizacionID ,
							CorralID = 1424,
							LoteID = 2748,
							FechaMovimiento = GETDATE(),
							Peso = ISA.PesoCompra,
							Temperatura = 39,
							TipoMovimientoID = 5,
							TrampaID = 4,
							OperadorID = 6,
							Observaciones = '',
							LoteIDOrigen = NULL,
							AnimalMovimientoIDAnterior = NULL,
							Activo = 1,
							FechaCreacion = GETDATE(),
							UsuarioCreacionID = 1,
							FechaModificacion = NULL,
							UsuarioModificacionID = NULL
					FROM EntradasIncompletas EI
					INNER JOIN EntradaGanado EG ON EG.FolioEntrada = EI.FolioEntrada
					INNER JOIN InterfaceSalida ISs ON ISs.SalidaID = EG.FolioOrigen 
													AND ISs.OrganizacionID = EG.OrganizacionOrigenID
													AND ISs.OrganizacionDestinoID = 1
					INNER JOIN InterfaceSalidaAnimal ISA ON ISA.SalidaID = EG.FolioOrigen 
														AND ISA.OrganizacionID = EG.OrganizacionOrigenID
					INNER JOIN Animal A(NOLOCK) ON A.Arete = ISA.Arete
					WHERE EI.FolioEntrada IN (@FolioEntrada)  --  (1019);
					AND ISA.Arete IN ( SELECT Arete FROM #tmpAnimalesNuevos );
					
					
					/* Cursor para los movimientos de almacen */
					DECLARE curMovimientos CURSOR FOR 		
						SELECT AM.AnimalMovimientoID, A.AnimalID, AM.Peso, TG.Sexo 
						FROM Animal A(NOLOCK)
						INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
						INNER JOIN TipoGanado TG ON TG.TipoGanadoID = A.TipoGanadoID
						WHERE A.Arete IN ( SELECT Arete FROM #tmpAnimalesNuevos )
						AND A.FolioEntrada IN (@FolioEntrada)
						AND AM.Activo = 1;
			
					/* Insertar los movimientos de almacen y los tratamientos */
					-- Apertura del cursor
					OPEN curMovimientos
					-- Lectura de la primera fila del cursor
						FETCH NEXT FROM curMovimientos INTO @AnimalMovimientoID, @AnimalID, @Peso, @Sexo
							WHILE (@@FETCH_STATUS = 0 )
							BEGIN
								-- SELECT @AnimalMovimientoID, @AnimalID, @Peso, @Sexo;
								/* Generamos folio del movimiento del almacen */		
								EXEC FolioAlmacen_Obtener @AlmacenID, @TipoMovimientoID, @Folio = @FolioAlmacen OUTPUT
								-- SELECT @FolioAlmacen;
								 INSERT INTO AlmacenMovimiento
								SELECT TOP 1 
									AlmacenID = @AlmacenID,
									TipoMovimientoID = @TipoMovimientoID,
									ProveedorID = NULL,
									FolioMovimiento = @FolioAlmacen,
									Observaciones = '',
									FechaMovimiento = GETDATE(),
									Status = 21,
									AnimalMovimientoID = @AnimalMovimientoID,
									PolizaGenerada = 0,
									FechaCreacion = GETDATE(),
									UsuarioCreacionID = 1,
									FechaModificacion = NULL,
									UsuarioModificacionID = NULL
								FROM AlmacenMovimiento 
								WHERE AnimalMovimientoID IS NOT NULL;

								/* Se obtiene el id Insertado */
								SET @AlmacenMovimientoID = (SELECT @@IDENTITY);
	
								/* Se genera el detalle del almacen */
								-- INSERT INTO AlmacenMovimientoDetalle
								SELECT DISTINCT
										AlmacenMovimientoID = @AlmacenMovimientoID,
										AlmacenInventarioLoteID = NULL,
										ContratoID = NULL,
										Piezas = 0,
										TratamientoID = t.TratamientoID,
										ProductoID = p.ProductoID, 
										Precio = AI.PrecioPromedio,
										Cantidad = tp.Dosis,
										Importe = AI.PrecioPromedio * tp.Dosis,
										FechaCreacion = GETDATE(),
										UsuarioCreacionID = 1,
										FechaModificacion = NULL,
										UsuarioModificacionID = NULL     
								INTO #tmpAlmacenMovimientoDetalle
								-- SELECT DISTINCT t.TratamientoID, p.ProductoID, AI.PrecioPromedio, tp.Dosis, AI.PrecioPromedio * tp.Dosis
								FROM Tratamiento t(NOLOCK)
								INNER JOIN TratamientoProducto tp(NOLOCK) ON tp.TratamientoID = t.TratamientoID
								INNER JOIN Producto p(NOLOCK) ON p.ProductoID = tp.ProductoID
								INNER JOIN AlmacenInventario AI ON AI.ProductoID = p.ProductoID AND AlmacenID = @AlmacenID
								WHERE  t.OrganizacionID = 1 
								AND t.sexo = @Sexo
								AND @Peso BETWEEN t.RangoInicial AND t.RangoFinal
								-- AND t.TipoTratamientoID in(1)
								AND (  
									   (t.TipoTratamientoID in(1)) OR 
									   (t.TipoTratamientoID in(5) AND p.ProductoID IN (52))/* OR
									   (t.TipoTratamientoID in(3) AND p.ProductoID IN (133))*/
									 )
								AND t.Activo = 1
								AND p.Activo = 1
								AND tp.Activo = 1;
								
								INSERT INTO AlmacenMovimientoDetalle
								SELECT AlmacenMovimientoID,
										AlmacenInventarioLoteID,
										ContratoID,
										Piezas,
										TratamientoID,
										ProductoID, 
										Precio,
										Cantidad,
										Importe,
										FechaCreacion,
										UsuarioCreacionID,
										FechaModificacion,
										UsuarioModificacionID
								FROM #tmpAlmacenMovimientoDetalle;
								
								INSERT INTO AnimalCosto
								SELECT 
									AnimalID = @AnimalID,
									FechaCosto = GETDATE(),
									CostoID = 16,   /* Costo de Medicamentos Corte */
									TipoReferencia = 1, /* 1.- Manejo --> AlmacenMovimientoID */
									FolioReferencia = @AlmacenMovimientoID,
									Importe = SUM(Importe),
									FechaCreacion = GETDATE(),
									UsuarioCreacionID = 1,
									FechaModificacion = NULL,
									UsuarioModificacionID = NULL
								FROM #tmpAlmacenMovimientoDetalle;
								
								/* Se decrementan las existencias del inventario*/
								UPDATE AI SET AI.Cantidad=AI.Cantidad-temp.Cantidad,
											  AI.Importe=AI.PrecioPromedio*(AI.Cantidad-temp.Cantidad),
											  AI.FechaModificacion = GETDATE(),
											  AI.UsuarioModificacionID = 1
								FROM AlmacenInventario AI(NOLOCK)
								INNER JOIN #tmpAlmacenMovimientoDetalle temp ON @AlmacenID = AI.AlmacenID AND temp.ProductoID = AI.ProductoID
		
								DROP TABLE #tmpAlmacenMovimientoDetalle;
								
								FETCH NEXT FROM curMovimientos INTO @AnimalMovimientoID, @AnimalID, @Peso, @Sexo
							END
						-- Cierre del cursor
						CLOSE curMovimientos
					-- Liberar los recursos
					DEALLOCATE curMovimientos
					
					/* Se decrementa las cabezas del lote origen */
					UPDATE L
					SET L.Cabezas = LoteEntrada.Cabezas - AniInsertados.CabezasInsertadas,
						L.Activo = CASE WHEN (LoteEntrada.Cabezas  - AniInsertados.CabezasInsertadas)<= 0 THEN 0 ELSE 1 END,
						L.UsuarioModificacionID = 1,
						L.FechaModificacion = GETDATE(),
						L.FechaSalida = GETDATE()
					FROM(
						SELECT A.FolioEntrada, COUNT(*) CabezasInsertadas
						  FROM Animal A(NOLOCK)
						 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
						 WHERE AM.Activo = 1
						   AND A.UsuarioCreacionID = 1
						   AND CONVERT(CHAR(8),AM.FechaMovimiento,112) = CONVERT(CHAR(8),GETDATE(),112)
						   AND A.FolioEntrada IN (@FolioEntrada)
						 GROUP BY A.FolioEntrada
					) AS AniInsertados
					INNER JOIN(SELECT EG.FolioEntrada, L.Cabezas, L.LoteID
								 FROM EntradaGanado EG 
								INNER JOIN Lote L on L.LoteID = EG.LoteID
								  AND EG.FolioEntrada IN (@FolioEntrada)
							  ) AS LoteEntrada on LoteEntrada.FolioEntrada = AniInsertados.FolioEntrada
					INNER JOIN Lote L on L.LoteID = LoteEntrada.LoteID;
								
					/* Elimina la programacionCorte Ganado */
					UPDATE ProgramacionCorte 
					   SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = 1 
					 WHERE FolioEntradaID = @FolioEntrada;
					
					/* Se actualiza las partidas procesadas*/
					UPDATE EntradasIncompletas 
					SET Cerrado = 1,
						FechaModificacion = GETDATE()
					WHERE FolioEntrada = @FolioEntrada;
					
				FETCH NEXT FROM curFolioEntrada1 INTO @FolioEntrada, @CabezasRecibidas, @AnimalesIventario, @AnimalesCentro
			END
		-- Cierre del cursor
		CLOSE curFolioEntrada1
	-- Liberar los recursos
	DEALLOCATE curFolioEntrada1
	
	/* Se actualizan las cabezas del lote del corral XXX */
	-- SELECT L.LoteID, L.Cabezasinicio, L.Cabezas, COUNT(AM.AnimalID) TotalCabezas
	UPDATE L
	SET Cabezas = Animales.TotalCabezas, 
		L.Cabezasinicio = Animales.TotalCabezas, 
		FechaModificacion = GETDATE(), 
		UsuarioModificacionID = 1
	FROM Corral C
	INNER JOIN Lote L ON L.CorralID = C.CorralID
	INNER JOIN (
				SELECT LoteID, COUNT(*) TotalCabezas
				  FROM AnimalMovimiento(NOLOCK)
				 WHERE Activo = 1
				  -- AND LoteID = L.LoteID
				 GROUP BY LoteID
				) AS Animales ON Animales.LoteID = L.LoteID
	WHERE L.Activo = 1
	AND C.Codigo = 'XXX';
	-- GROUP BY L.LoteID, L.Cabezasinicio, L.Cabezas

END

GO
