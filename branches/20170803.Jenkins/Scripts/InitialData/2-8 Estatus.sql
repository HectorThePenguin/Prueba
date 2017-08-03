/* Estatus Embarques */		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Pendiente', 1,'EmbPendien', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Recibido', 1,'EmRecibido', 1, GETDATE(), 1) 
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cancelado', 1,'EmCancelad', 1, GETDATE(), 1) 

/* Estatus para las detecciones de Muertes */			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('Detectado', 2,'MuDetectad', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta,  Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('Recolectado', 2,'MuRecolect', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('Necropsia', 2,'MuNecropsi', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('Cancelado', 2,'MuCancelad',  1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Recepcion Necropsia', 2,'MuRecNecro', 1, GETDATE(), 1)

/* Estatus para las Salidas por Venta(VentaGanado) */			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Generado', 3, 'VGGenerado', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Recolectado', 3,'VGRecolect', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Aplicado', 3,'VGAplicado', 1, GETDATE(), 1)

/* Estatus para la orden de sacrificio(*/			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Pendiente', 4,'OSPendient', 1, GETDATE(), 1)

/* Tipo Estatus para Distribución de alimentos */			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Correcto', 5,'OK', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Monton', 5,'M', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Espacio', 5,'E', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('No Existe', 5,'N/A', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Separacion', 5,'S', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Desperdicio', 5,'D', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Barrido', 5,'B', 1, GETDATE(), 1)

/* Tipo Estatus para Inventario de Almacen */	
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Pendiente', 6,'InvPendien', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Aplicado', 6,'InvAplicad', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cancelado', 6,'InvCancela', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Nuevo', 6,'InvNuevo', 1, GETDATE(), 1)

/* Tipo Estatus para Entrada Producto */			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Autorizado', 7,'EPAutoriza', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Rechazado', 7,'EPRechazad', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('Aprobado', 7, 'EPAprobado', 1, GETDATE(), 1);
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('Pendiente por Autorizar', 7, 'EPPendient', 1, GETDATE(), 1);


/* Tipo Estatus para Pedidos de materia primas*/			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Solicitado', 8,'PMPSolicit', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Programado', 8,'PMPProgram', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Parcial', 8,'PMPParcial', 1, GETDATE(), 1)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Recibido', 8,'PMPRecibid', 1, GETDATE(), 1)

/* Tipo Estatus para Pesajes de materia primas*/			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Pendiente', 9,'PMPPendien', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Completado', 9,'PMPComplet', 1, GETDATE(), 1)		

/* Tipo Estatus para Solicitud de productos al almacen*/

INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
VALUES ('Pendiente', 10, 'Pendiente', 1, GETDATE(), 1, NULL, NULL)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
VALUES ('Autorizado', 10, 'Autorizado', 1, GETDATE(), 1, NULL, NULL)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
VALUES ('Cancelado', 10, 'Cancelado', 1, GETDATE(), 1, NULL, NULL)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
VALUES ('Entregado', 10, 'Entregado', 1, GETDATE(), 1, NULL, NULL)
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
VALUES ('Recibido', 10, 'Recibido', 1, GETDATE(), 1, NULL, NULL)

/* Tipo Estatus para Diferencias de inventarios*/			
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Aplicado', 11,'DINAplicad', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Autorizado', 11,'DINAutoriz', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Pendiente', 11,'DINPendien', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Rechazado', 11,'DINRechaza', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cancelado', 11,'DINCancela', 1, GETDATE(), 1)		

/* Continuacion Tipo Estatus para Inventario de Almacen */	
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Autorizado', 6,'InvAutori', 1, GETDATE(), 1)

/*Tipo Estatus para Contratos*/
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Activo', 12,'CONactivo', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cerrado', 12,'CONcerrado', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cancelado', 12,'CONCancela', 1, GETDATE(), 1)

/*Tipo Estatus para Autorización Materia Prima*/
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Autorizado', 13,'AMPAutoriz', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Rechazado', 13,'AMPRechaza', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Pendiente', 13,'AMPPendien', 1, GETDATE(), 1)

/*Tipo Estatus para Cancelaciones de Pesaje y de Entrada Producto*/
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cancelado', 7,'EPCancelad', 1, GETDATE(), 1)		
INSERT INTO Estatus (Descripcion, TipoEstatus, DescripcionCorta, Activo, FechaCreacion, UsuarioCreacionID)
VALUES ('Cancelado', 9,'PMPCancela', 1, GETDATE(), 1)		



	
	
	
	
	