/* Crear un almacen de prueba */
INSERT INTO Almacen ([OrganizacionID], [CodigoAlmacen], [Descripcion], [TipoAlmacenID], [CuentaInventario], [CuentaInventarioTransito], [CuentaDiferencias], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES ('4', 'CODIGO', 'Almacen de prueba', 5, 1, 1, 1, 1, GETDATE(), 1)

INSERT INTO Almacen ([OrganizacionID], [CodigoAlmacen], [Descripcion], [TipoAlmacenID], [CuentaInventario], [CuentaInventarioTransito], [CuentaDiferencias], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (1, 'CODIGO2', 'Almacén General 02', 1, '1', '1', '1', 1, GETDATE(), 1)

INSERT INTO Almacen ([OrganizacionID], [CodigoAlmacen], [Descripcion], [TipoAlmacenID], [CuentaInventario], [CuentaInventarioTransito], [CuentaDiferencias], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (3, 'CODIGO3', 'Almacen de prueba 03', 5, '1', '1', '1', 1, GETDATE(), 1)

INSERT INTO Almacen ([OrganizacionID], [CodigoAlmacen], [Descripcion], [TipoAlmacenID], [CuentaInventario], [CuentaInventarioTransito], [CuentaDiferencias], [Activo], [FechaCreacion], [UsuarioCreacionID]) 
VALUES (4, 'CODIGO4', 'Almacen de prueba 04', 5, '1', '1', '1', 1, GETDATE(), 1)

/* Se crea el inventario de prueba para el almacen de prueba */
INSERT INTO AlmacenInventario
SELECT 1,ProductoID,2,40,0,
	CAST (ABS(CAST (CAST (NEWID()
			AS varbinary(8))
			AS bigint)
		) / POWER(2.0, 57) AS decimal(18,2)) AS scary_random2,
	CAST (ABS(CAST (CAST (NEWID()
			AS varbinary(8))
			AS bigint)
		) / POWER(2.0, 52) AS decimal(24,2)) AS scary_random3,
		8, 10000, GETDATE(),1,GETDATE(),1
FROM Producto
GROUP BY ProductoID

UPDATE AlmacenInventario
SET Cantidad = Cantidad + 1

UPDATE AlmacenInventario
SET PrecioPromedio = ROUND(Importe/Cantidad,2)
