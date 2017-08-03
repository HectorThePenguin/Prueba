IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AccionesSIAP_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AccionesSIAP] DROP CONSTRAINT [FK_AccionesSIAP_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AccionesSIAP_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AccionesSIAP] DROP CONSTRAINT [FK_AccionesSIAP_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Almacen_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Almacen] DROP CONSTRAINT [FK_Almacen_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Almacen_TipoAlmacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Almacen] DROP CONSTRAINT [FK_Almacen_TipoAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Almacen_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Almacen] DROP CONSTRAINT [FK_Almacen_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Almacen_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Almacen] DROP CONSTRAINT [FK_Almacen_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventario_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventario] DROP CONSTRAINT [FK_AlmacenInventario_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventario_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventario] DROP CONSTRAINT [FK_AlmacenInventario_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventario_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventario] DROP CONSTRAINT [FK_AlmacenInventario_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventario_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventario] DROP CONSTRAINT [FK_AlmacenInventario_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventarioLote_AlmacenInventario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventarioLote] DROP CONSTRAINT [FK_AlmacenInventarioLote_AlmacenInventario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventarioLote_AlmacenInventario_02]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventarioLote] DROP CONSTRAINT [FK_AlmacenInventarioLote_AlmacenInventario_02]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventarioLote_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventarioLote] DROP CONSTRAINT [FK_AlmacenInventarioLote_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenInventarioLote_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenInventarioLote] DROP CONSTRAINT [FK_AlmacenInventarioLote_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimiento_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimiento] DROP CONSTRAINT [FK_AlmacenMovimiento_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimiento_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimiento] DROP CONSTRAINT [FK_AlmacenMovimiento_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimiento_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimiento] DROP CONSTRAINT [FK_AlmacenMovimiento_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimiento_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimiento] DROP CONSTRAINT [FK_AlmacenMovimiento_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimiento] DROP CONSTRAINT [FK_AlmacenMovimiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoCosto_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoCosto] DROP CONSTRAINT [FK_AlmacenMovimientoCosto_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoCosto] DROP CONSTRAINT [FK_AlmacenMovimientoCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoCosto_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoCosto] DROP CONSTRAINT [FK_AlmacenMovimientoCosto_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoCosto_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoCosto] DROP CONSTRAINT [FK_AlmacenMovimientoCosto_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoCosto] DROP CONSTRAINT [FK_AlmacenMovimientoCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoCosto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoCosto] DROP CONSTRAINT [FK_AlmacenMovimientoCosto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoDetalle_AlmacenInventarioLote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoDetalle] DROP CONSTRAINT [FK_AlmacenMovimientoDetalle_AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoDetalle_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoDetalle] DROP CONSTRAINT [FK_AlmacenMovimientoDetalle_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoDetalle] DROP CONSTRAINT [FK_AlmacenMovimientoDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoDetalle_Tratamiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoDetalle] DROP CONSTRAINT [FK_AlmacenMovimientoDetalle_Tratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoDetalle] DROP CONSTRAINT [FK_AlmacenMovimientoDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenMovimientoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenMovimientoDetalle] DROP CONSTRAINT [FK_AlmacenMovimientoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenUsuario_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenUsuario] DROP CONSTRAINT [FK_AlmacenUsuario_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenUsuario_UsuarioAlmacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenUsuario] DROP CONSTRAINT [FK_AlmacenUsuario_UsuarioAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenUsuario_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenUsuario] DROP CONSTRAINT [FK_AlmacenUsuario_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmacenUsuario_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmacenUsuario] DROP CONSTRAINT [FK_AlmacenUsuario_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonFactor_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonFactor] DROP CONSTRAINT [FK_AlmidonFactor_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonFactor_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonFactor] DROP CONSTRAINT [FK_AlmidonFactor_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonFactor_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonFactor] DROP CONSTRAINT [FK_AlmidonFactor_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_UsuarioIDLaboratorio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_UsuarioIDLaboratorio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHeces_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHeces] DROP CONSTRAINT [FK_AlmidonHeces_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHecesDetalle_AlmidonHeces]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHecesDetalle] DROP CONSTRAINT [FK_AlmidonHecesDetalle_AlmidonHeces]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHecesDetalle_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHecesDetalle] DROP CONSTRAINT [FK_AlmidonHecesDetalle_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHecesDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHecesDetalle] DROP CONSTRAINT [FK_AlmidonHecesDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AlmidonHecesDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AlmidonHecesDetalle] DROP CONSTRAINT [FK_AlmidonHecesDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnalisisGrasa_EntradaProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnalisisGrasa] DROP CONSTRAINT [FK_AnalisisGrasa_EntradaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnalisisGrasa_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnalisisGrasa] DROP CONSTRAINT [FK_AnalisisGrasa_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnalisisGrasa_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnalisisGrasa] DROP CONSTRAINT [FK_AnalisisGrasa_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnalisisGrasa_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnalisisGrasa] DROP CONSTRAINT [FK_AnalisisGrasa_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Animal_CalidadGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Animal] DROP CONSTRAINT [FK_Animal_CalidadGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Animal_CausaRechazo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Animal] DROP CONSTRAINT [FK_Animal_CausaRechazo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Animal_ClasificacionGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Animal] DROP CONSTRAINT [FK_Animal_ClasificacionGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Animal_TipoGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Animal] DROP CONSTRAINT [FK_Animal_TipoGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Animal_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Animal] DROP CONSTRAINT [FK_Animal_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Animal_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Animal] DROP CONSTRAINT [FK_Animal_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumo_Animal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalConsumo] DROP CONSTRAINT [FK_AnimalConsumo_Animal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumo_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalConsumo] DROP CONSTRAINT [FK_AnimalConsumo_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumo_Reparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalConsumo] DROP CONSTRAINT [FK_AnimalConsumo_Reparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumo_TipoServicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalConsumo] DROP CONSTRAINT [FK_AnimalConsumo_TipoServicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalConsumo] DROP CONSTRAINT [FK_AnimalConsumo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalConsumo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalConsumo] DROP CONSTRAINT [FK_AnimalConsumo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCosto_Animal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalCosto] DROP CONSTRAINT [FK_AnimalCosto_Animal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalCosto] DROP CONSTRAINT [FK_AnimalCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCosto_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalCosto] DROP CONSTRAINT [FK_AnimalCosto_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalCosto] DROP CONSTRAINT [FK_AnimalCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_Animal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_Animal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_LoteIDAnterior]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_LoteIDAnterior]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_Trampa]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_Trampa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalMovimiento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalMovimiento] DROP CONSTRAINT [FK_AnimalMovimiento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_Animal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_Animal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_OrdenSacrificio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_OrdenSacrificio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AnimalSalida_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AnimalSalida] DROP CONSTRAINT [FK_AnimalSalida_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AreaRevision_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AreaRevision] DROP CONSTRAINT [FK_AreaRevision_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AreaRevision_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AreaRevision] DROP CONSTRAINT [FK_AreaRevision_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_TipoAutorizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_TipoAutorizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_AutorizacionMateriaPrima_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AutorizacionMateriaPrima] DROP CONSTRAINT [FK_AutorizacionMateriaPrima_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_BitacoraErrores_AccionesSIAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [BitacoraErrores] DROP CONSTRAINT [FK_BitacoraErrores_AccionesSIAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_BitacoraErrores_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [BitacoraErrores] DROP CONSTRAINT [FK_BitacoraErrores_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Calculo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Calculo] DROP CONSTRAINT [FK_Calculo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Calculo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Calculo] DROP CONSTRAINT [FK_Calculo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMateriaPrima_ColorObjetivo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMateriaPrima] DROP CONSTRAINT [FK_CalidadMateriaPrima_ColorObjetivo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMateriaPrima_IndicadorObjetivo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMateriaPrima] DROP CONSTRAINT [FK_CalidadMateriaPrima_IndicadorObjetivo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMateriaPrima_PedidoDetalle]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMateriaPrima] DROP CONSTRAINT [FK_CalidadMateriaPrima_PedidoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMateriaPrima_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMateriaPrima] DROP CONSTRAINT [FK_CalidadMateriaPrima_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMateriaPrima_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMateriaPrima] DROP CONSTRAINT [FK_CalidadMateriaPrima_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_CamionReparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_CamionReparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_Chofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_Chofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_Mezcladora]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_Mezcladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_OperadorIDAnalista]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_OperadorIDAnalista]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_OperadorIDMezclador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_OperadorIDMezclador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_TipoLugarMuestra]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_TipoLugarMuestra]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_TipoTecnica]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_TipoTecnica]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_UsuarioLaboratorio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_UsuarioLaboratorio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezclado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezclado] DROP CONSTRAINT [FK_CalidadMezclado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoDetalle_CalidadMezclado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoDetalle] DROP CONSTRAINT [FK_CalidadMezcladoDetalle_CalidadMezclado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoDetalle_TipoMuestra]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoDetalle] DROP CONSTRAINT [FK_CalidadMezcladoDetalle_TipoMuestra]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoDetalle] DROP CONSTRAINT [FK_CalidadMezcladoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoDetalle] DROP CONSTRAINT [FK_CalidadMezcladoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoFactor_TipoMuestra]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoFactor] DROP CONSTRAINT [FK_CalidadMezcladoFactor_TipoMuestra]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoFactor_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoFactor] DROP CONSTRAINT [FK_CalidadMezcladoFactor_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CalidadMezcladoFactor_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CalidadMezcladoFactor] DROP CONSTRAINT [FK_CalidadMezcladoFactor_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Camion_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Camion] DROP CONSTRAINT [FK_Camion_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Camion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Camion] DROP CONSTRAINT [FK_Camion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Camion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Camion] DROP CONSTRAINT [FK_Camion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CamionReparto_CentroCosto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CamionReparto] DROP CONSTRAINT [FK_CamionReparto_CentroCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CamionReparto_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CamionReparto] DROP CONSTRAINT [FK_CamionReparto_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CamionReparto_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CamionReparto] DROP CONSTRAINT [FK_CamionReparto_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CamionReparto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CamionReparto] DROP CONSTRAINT [FK_CamionReparto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CancelacionMovimiento_Pedido]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CancelacionMovimiento] DROP CONSTRAINT [FK_CancelacionMovimiento_Pedido]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CancelacionMovimiento_TipoCancelacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CancelacionMovimiento] DROP CONSTRAINT [FK_CancelacionMovimiento_TipoCancelacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CancelacionMovimiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CancelacionMovimiento] DROP CONSTRAINT [FK_CancelacionMovimiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CancelacionMovimiento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CancelacionMovimiento] DROP CONSTRAINT [FK_CancelacionMovimiento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CancelacionMovimiento_AlmacenMovimientoIDOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CancelacionMovimiento] DROP CONSTRAINT [FK_CancelacionMovimiento_AlmacenMovimientoIDOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CancelacionMovimiento_AlmacenMovimientoIDCancelado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CancelacionMovimiento] DROP CONSTRAINT [FK_CancelacionMovimiento_AlmacenMovimientoIDCancelado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaPrecio_CausaSalida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaPrecio] DROP CONSTRAINT [FK_CausaPrecio_CausaSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaPrecio_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaPrecio] DROP CONSTRAINT [FK_CausaPrecio_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaPrecio_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaPrecio] DROP CONSTRAINT [FK_CausaPrecio_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaPrecio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaPrecio] DROP CONSTRAINT [FK_CausaPrecio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaRechazo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaRechazo] DROP CONSTRAINT [FK_CausaRechazo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaRechazo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaRechazo] DROP CONSTRAINT [FK_CausaRechazo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaSalida_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaSalida] DROP CONSTRAINT [FK_CausaSalida_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaSalida_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaSalida] DROP CONSTRAINT [FK_CausaSalida_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaSalida_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaSalida] DROP CONSTRAINT [FK_CausaSalida_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaTiempoMuerto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaTiempoMuerto] DROP CONSTRAINT [FK_CausaTiempoMuerto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CausaTiempoMuerto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CausaTiempoMuerto] DROP CONSTRAINT [FK_CausaTiempoMuerto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroBeneficio_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroBeneficio] DROP CONSTRAINT [FK_CentroBeneficio_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroBeneficio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroBeneficio] DROP CONSTRAINT [FK_CentroBeneficio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroCosto_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroCosto] DROP CONSTRAINT [FK_CentroCosto_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroCosto] DROP CONSTRAINT [FK_CentroCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroCostoUsuario_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroCostoUsuario] DROP CONSTRAINT [FK_CentroCostoUsuario_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroCostoUsuario_UsuarioCreacionID]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroCostoUsuario] DROP CONSTRAINT [FK_CentroCostoUsuario_UsuarioCreacionID]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroCostoUsuario_UsuarioModificacionID]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroCostoUsuario] DROP CONSTRAINT [FK_CentroCostoUsuario_UsuarioModificacionID]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CentroCostoUsuario_CentroCosto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CentroCostoUsuario] DROP CONSTRAINT [FK_CentroCostoUsuario_CentroCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListCorral_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListCorral] DROP CONSTRAINT [FK_CheckListCorral_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListCorral_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListCorral] DROP CONSTRAINT [FK_CheckListCorral_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListCorral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListCorral] DROP CONSTRAINT [FK_CheckListCorral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListCorral_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListCorral] DROP CONSTRAINT [FK_CheckListCorral_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladora_CheckListRoladoraGeneral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladora] DROP CONSTRAINT [FK_CheckListRoladora_CheckListRoladoraGeneral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladora_Roladora]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladora] DROP CONSTRAINT [FK_CheckListRoladora_Roladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladora_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladora] DROP CONSTRAINT [FK_CheckListRoladora_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladora_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladora] DROP CONSTRAINT [FK_CheckListRoladora_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladora_UsuarioResponsable]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladora] DROP CONSTRAINT [FK_CheckListRoladora_UsuarioResponsable]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraAccion_CheckListRoladoraRango]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraAccion] DROP CONSTRAINT [FK_CheckListRoladoraAccion_CheckListRoladoraRango]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraAccion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraAccion] DROP CONSTRAINT [FK_CheckListRoladoraAccion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraAccion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraAccion] DROP CONSTRAINT [FK_CheckListRoladoraAccion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraDetalle_CheckListRoladora]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraDetalle] DROP CONSTRAINT [FK_CheckListRoladoraDetalle_CheckListRoladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraDetalle_CheckListRoladoraAccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraDetalle] DROP CONSTRAINT [FK_CheckListRoladoraDetalle_CheckListRoladoraAccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraDetalle_CheckListRoladoraRango]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraDetalle] DROP CONSTRAINT [FK_CheckListRoladoraDetalle_CheckListRoladoraRango]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraDetalle] DROP CONSTRAINT [FK_CheckListRoladoraDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraDetalle] DROP CONSTRAINT [FK_CheckListRoladoraDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraGeneral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraGeneral] DROP CONSTRAINT [FK_CheckListRoladoraGeneral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraGeneral_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraGeneral] DROP CONSTRAINT [FK_CheckListRoladoraGeneral_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraGeneral_UsuarioSupervisor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraGeneral] DROP CONSTRAINT [FK_CheckListRoladoraGeneral_UsuarioSupervisor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraHorometro_CheckListRoladoraGeneral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraHorometro] DROP CONSTRAINT [FK_CheckListRoladoraHorometro_CheckListRoladoraGeneral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraHorometro_Roladora]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraHorometro] DROP CONSTRAINT [FK_CheckListRoladoraHorometro_Roladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraHorometro_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraHorometro] DROP CONSTRAINT [FK_CheckListRoladoraHorometro_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraHorometro_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraHorometro] DROP CONSTRAINT [FK_CheckListRoladoraHorometro_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraRango_Pregunta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraRango] DROP CONSTRAINT [FK_CheckListRoladoraRango_Pregunta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraRango_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraRango] DROP CONSTRAINT [FK_CheckListRoladoraRango_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CheckListRoladoraRango_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CheckListRoladoraRango] DROP CONSTRAINT [FK_CheckListRoladoraRango_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Chofer_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Chofer] DROP CONSTRAINT [FK_Chofer_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Chofer_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Chofer] DROP CONSTRAINT [FK_Chofer_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClaseCostoProducto_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClaseCostoProducto] DROP CONSTRAINT [FK_ClaseCostoProducto_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClaseCostoProducto_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClaseCostoProducto] DROP CONSTRAINT [FK_ClaseCostoProducto_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClaseCostoProducto_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClaseCostoProducto] DROP CONSTRAINT [FK_ClaseCostoProducto_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClaseCostoProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClaseCostoProducto] DROP CONSTRAINT [FK_ClaseCostoProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClaseCostoProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClaseCostoProducto] DROP CONSTRAINT [FK_ClaseCostoProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClasificacionGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClasificacionGanado] DROP CONSTRAINT [FK_ClasificacionGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ClasificacionGanado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ClasificacionGanado] DROP CONSTRAINT [FK_ClasificacionGanado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Cliente_MetodoPago]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Cliente] DROP CONSTRAINT [FK_Cliente_MetodoPago]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Cliente_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Cliente] DROP CONSTRAINT [FK_Cliente_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Cliente_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Cliente] DROP CONSTRAINT [FK_Cliente_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ColorObjetivo_TipoObjetivoCalidad]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ColorObjetivo] DROP CONSTRAINT [FK_ColorObjetivo_TipoObjetivoCalidad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ColorObjetivo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ColorObjetivo] DROP CONSTRAINT [FK_ColorObjetivo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ColorObjetivo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ColorObjetivo] DROP CONSTRAINT [FK_ColorObjetivo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConceptoDeteccion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConceptoDeteccion] DROP CONSTRAINT [FK_ConceptoDeteccion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConceptoDeteccion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConceptoDeteccion] DROP CONSTRAINT [FK_ConceptoDeteccion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Condicion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Condicion] DROP CONSTRAINT [FK_Condicion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Condicion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Condicion] DROP CONSTRAINT [FK_Condicion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CondicionJaula_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CondicionJaula] DROP CONSTRAINT [FK_CondicionJaula_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CondicionJaula_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CondicionJaula] DROP CONSTRAINT [FK_CondicionJaula_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionEmbarque_OrganizacionDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionEmbarque] DROP CONSTRAINT [FK_ConfiguracionEmbarque_OrganizacionDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionEmbarque_OrganizacionOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionEmbarque] DROP CONSTRAINT [FK_ConfiguracionEmbarque_OrganizacionOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionEmbarque_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionEmbarque] DROP CONSTRAINT [FK_ConfiguracionEmbarque_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionEmbarque_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionEmbarque] DROP CONSTRAINT [FK_ConfiguracionEmbarque_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionFormula_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionFormula] DROP CONSTRAINT [FK_ConfiguracionFormula_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionFormula_FormulaSiguiente]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionFormula] DROP CONSTRAINT [FK_ConfiguracionFormula_FormulaSiguiente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionFormula_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionFormula] DROP CONSTRAINT [FK_ConfiguracionFormula_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionFormula_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionFormula] DROP CONSTRAINT [FK_ConfiguracionFormula_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionFormula_UsuarioCrecion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionFormula] DROP CONSTRAINT [FK_ConfiguracionFormula_UsuarioCrecion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionImplante_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionImplante] DROP CONSTRAINT [FK_ConfiguracionImplante_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionImplante_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionImplante] DROP CONSTRAINT [FK_ConfiguracionImplante_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionSemana_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionSemana] DROP CONSTRAINT [FK_ConfiguracionSemana_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionSemana_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionSemana] DROP CONSTRAINT [FK_ConfiguracionSemana_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionSemana_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionSemana] DROP CONSTRAINT [FK_ConfiguracionSemana_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionTraspasoAlmacen_TipoAlmacenOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionTraspasoAlmacen] DROP CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_TipoAlmacenOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionTraspasoAlmacen_TipoAlmacenDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionTraspasoAlmacen] DROP CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_TipoAlmacenDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionTraspasoAlmacen_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionTraspasoAlmacen] DROP CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ConfiguracionTraspasoAlmacen_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ConfiguracionTraspasoAlmacen] DROP CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Constantes_Calculo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Constantes] DROP CONSTRAINT [FK_Constantes_Calculo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Constantes_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Constantes] DROP CONSTRAINT [FK_Constantes_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Constantes_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Constantes] DROP CONSTRAINT [FK_Constantes_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_TipoCambio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_TipoCambio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_TipoContrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_TipoContrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_TipoFlete]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_TipoFlete]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Contrato_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Contrato] DROP CONSTRAINT [FK_Contrato_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoDetalle_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoDetalle] DROP CONSTRAINT [FK_ContratoDetalle_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoDetalle_Indicador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoDetalle] DROP CONSTRAINT [FK_ContratoDetalle_Indicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoDetalle] DROP CONSTRAINT [FK_ContratoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoDetalle] DROP CONSTRAINT [FK_ContratoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoHumedad_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoHumedad] DROP CONSTRAINT [FK_ContratoHumedad_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoHumedad_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoHumedad] DROP CONSTRAINT [FK_ContratoHumedad_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoHumedad_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoHumedad] DROP CONSTRAINT [FK_ContratoHumedad_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoParcial_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoParcial] DROP CONSTRAINT [FK_ContratoParcial_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoParcial_TipoCambio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoParcial] DROP CONSTRAINT [FK_ContratoParcial_TipoCambio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoParcial_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoParcial] DROP CONSTRAINT [FK_ContratoParcial_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ContratoParcial_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ContratoParcial] DROP CONSTRAINT [FK_ContratoParcial_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Corral_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Corral] DROP CONSTRAINT [FK_Corral_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Corral_TipoCorral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Corral] DROP CONSTRAINT [FK_Corral_TipoCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Corral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Corral] DROP CONSTRAINT [FK_Corral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Corral_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Corral] DROP CONSTRAINT [FK_Corral_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralDetector_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralDetector] DROP CONSTRAINT [FK_CorralDetector_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralDetector_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralDetector] DROP CONSTRAINT [FK_CorralDetector_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralDetector_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralDetector] DROP CONSTRAINT [FK_CorralDetector_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralDetector_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralDetector] DROP CONSTRAINT [FK_CorralDetector_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralLector_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralLector] DROP CONSTRAINT [FK_CorralLector_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralLector_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralLector] DROP CONSTRAINT [FK_CorralLector_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralLector_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralLector] DROP CONSTRAINT [FK_CorralLector_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralLector_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralLector] DROP CONSTRAINT [FK_CorralLector_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralRango_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralRango] DROP CONSTRAINT [FK_CorralRango_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralRango_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralRango] DROP CONSTRAINT [FK_CorralRango_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralRango_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralRango] DROP CONSTRAINT [FK_CorralRango_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CorralRango_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CorralRango] DROP CONSTRAINT [FK_CorralRango_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Costo_Retencion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Costo] DROP CONSTRAINT [FK_Costo_Retencion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Costo_TipoCosto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Costo] DROP CONSTRAINT [FK_Costo_TipoCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Costo_TipoProrrateo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Costo] DROP CONSTRAINT [FK_Costo_TipoProrrateo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Costo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Costo] DROP CONSTRAINT [FK_Costo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Costo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Costo] DROP CONSTRAINT [FK_Costo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoEmbarqueDetalle_EmbarqueDetalle]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoEmbarqueDetalle] DROP CONSTRAINT [FK_CostoEmbarqueDetalle_EmbarqueDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoEmbarqueDetalle_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoEmbarqueDetalle] DROP CONSTRAINT [FK_CostoEmbarqueDetalle_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoEmbarqueDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoEmbarqueDetalle] DROP CONSTRAINT [FK_CostoEmbarqueDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoEmbarqueDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoEmbarqueDetalle] DROP CONSTRAINT [FK_CostoEmbarqueDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoOrganizacion_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoOrganizacion] DROP CONSTRAINT [FK_CostoOrganizacion_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoOrganizacion_TipoOrganizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoOrganizacion] DROP CONSTRAINT [FK_CostoOrganizacion_TipoOrganizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoOrganizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoOrganizacion] DROP CONSTRAINT [FK_CostoOrganizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoOrganizacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoOrganizacion] DROP CONSTRAINT [FK_CostoOrganizacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoPromedio_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoPromedio] DROP CONSTRAINT [FK_CostoPromedio_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoPromedio_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoPromedio] DROP CONSTRAINT [FK_CostoPromedio_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoPromedio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoPromedio] DROP CONSTRAINT [FK_CostoPromedio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CostoPromedio_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CostoPromedio] DROP CONSTRAINT [FK_CostoPromedio_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CriterioSupervision_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CriterioSupervision] DROP CONSTRAINT [FK_CriterioSupervision_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CriterioSupervision_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CriterioSupervision] DROP CONSTRAINT [FK_CriterioSupervision_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Cuenta_TipoCuenta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Cuenta] DROP CONSTRAINT [FK_Cuenta_TipoCuenta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Cuenta_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Cuenta] DROP CONSTRAINT [FK_Cuenta_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Cuenta_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Cuenta] DROP CONSTRAINT [FK_Cuenta_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaAlmacenSubFamilia_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaAlmacenSubFamilia] DROP CONSTRAINT [FK_CuentaAlmacenSubFamilia_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaAlmacenSubFamilia_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaAlmacenSubFamilia] DROP CONSTRAINT [FK_CuentaAlmacenSubFamilia_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaAlmacenSubFamilia_SubFamilia]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaAlmacenSubFamilia] DROP CONSTRAINT [FK_CuentaAlmacenSubFamilia_SubFamilia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaAlmacenSubFamilia_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaAlmacenSubFamilia] DROP CONSTRAINT [FK_CuentaAlmacenSubFamilia_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaAlmacenSubFamilia_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaAlmacenSubFamilia] DROP CONSTRAINT [FK_CuentaAlmacenSubFamilia_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaSAP_TipoCuenta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaSAP] DROP CONSTRAINT [FK_CuentaSAP_TipoCuenta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaSAP_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaSAP] DROP CONSTRAINT [FK_CuentaSAP_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaSAP_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaSAP] DROP CONSTRAINT [FK_CuentaSAP_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaValor_Cuenta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaValor] DROP CONSTRAINT [FK_CuentaValor_Cuenta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaValor_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaValor] DROP CONSTRAINT [FK_CuentaValor_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaValor_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaValor] DROP CONSTRAINT [FK_CuentaValor_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_CuentaValor_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [CuentaValor] DROP CONSTRAINT [FK_CuentaValor_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DescripcionGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DescripcionGanado] DROP CONSTRAINT [FK_DescripcionGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DescripcionGanado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DescripcionGanado] DROP CONSTRAINT [FK_DescripcionGanado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_DescripcionGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_DescripcionGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_Grado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_Grado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_TipoDeteccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_TipoDeteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Deteccion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Deteccion] DROP CONSTRAINT [FK_Deteccion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_DescripcionGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_DescripcionGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_Grado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_Grado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_TipoDeteccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_TipoDeteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionAnimal_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionAnimal] DROP CONSTRAINT [FK_DeteccionAnimal_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintoma_Deteccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintoma] DROP CONSTRAINT [FK_DeteccionSintoma_Deteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintoma_Sintoma]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintoma] DROP CONSTRAINT [FK_DeteccionSintoma_Sintoma]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintoma_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintoma] DROP CONSTRAINT [FK_DeteccionSintoma_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintoma_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintoma] DROP CONSTRAINT [FK_DeteccionSintoma_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintomaAnimal_DeteccionAnimal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintomaAnimal] DROP CONSTRAINT [FK_DeteccionSintomaAnimal_DeteccionAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintomaAnimal_Sintoma]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintomaAnimal] DROP CONSTRAINT [FK_DeteccionSintomaAnimal_Sintoma]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintomaAnimal_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintomaAnimal] DROP CONSTRAINT [FK_DeteccionSintomaAnimal_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DeteccionSintomaAnimal_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DeteccionSintomaAnimal] DROP CONSTRAINT [FK_DeteccionSintomaAnimal_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalista_DeteccionAnimal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalista] DROP CONSTRAINT [FK_DiagnosticoAnalista_DeteccionAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalista_Grado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalista] DROP CONSTRAINT [FK_DiagnosticoAnalista_Grado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalista_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalista] DROP CONSTRAINT [FK_DiagnosticoAnalista_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalista_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalista] DROP CONSTRAINT [FK_DiagnosticoAnalista_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalistaDetalle_DiagnosticoAnalista]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalistaDetalle] DROP CONSTRAINT [FK_DiagnosticoAnalistaDetalle_DiagnosticoAnalista]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalistaDetalle_Problema]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalistaDetalle] DROP CONSTRAINT [FK_DiagnosticoAnalistaDetalle_Problema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalistaDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalistaDetalle] DROP CONSTRAINT [FK_DiagnosticoAnalistaDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_DiagnosticoAnalistaDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [DiagnosticoAnalistaDetalle] DROP CONSTRAINT [FK_DiagnosticoAnalistaDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Embarque_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Embarque] DROP CONSTRAINT [FK_Embarque_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Embarque_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Embarque] DROP CONSTRAINT [FK_Embarque_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Embarque_TipoEmbarque]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Embarque] DROP CONSTRAINT [FK_Embarque_TipoEmbarque]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Embarque_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Embarque] DROP CONSTRAINT [FK_Embarque_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Embarque_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Embarque] DROP CONSTRAINT [FK_Embarque_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_Camion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_Camion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_Chofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_Chofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_Jaula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_Jaula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_OrganizacionDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_OrganizacionDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_OrganizacionOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_OrganizacionOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_Embarque]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_Embarque]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EmbarqueDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EmbarqueDetalle] DROP CONSTRAINT [FK_EmbarqueDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Enfermeria_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Enfermeria] DROP CONSTRAINT [FK_Enfermeria_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Enfermeria_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Enfermeria] DROP CONSTRAINT [FK_Enfermeria_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Enfermeria_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Enfermeria] DROP CONSTRAINT [FK_Enfermeria_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EnfermeriaCorral_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EnfermeriaCorral] DROP CONSTRAINT [FK_EnfermeriaCorral_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EnfermeriaCorral_Enfermeria]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EnfermeriaCorral] DROP CONSTRAINT [FK_EnfermeriaCorral_Enfermeria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EnfermeriaCorral_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EnfermeriaCorral] DROP CONSTRAINT [FK_EnfermeriaCorral_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EnfermeriaCorral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EnfermeriaCorral] DROP CONSTRAINT [FK_EnfermeriaCorral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaCondicion_Condicion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaCondicion] DROP CONSTRAINT [FK_EntradaCondicion_Condicion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaCondicion_EntradaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaCondicion] DROP CONSTRAINT [FK_EntradaCondicion_EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaCondicion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaCondicion] DROP CONSTRAINT [FK_EntradaCondicion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaCondicion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaCondicion] DROP CONSTRAINT [FK_EntradaCondicion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaDetalle_EntradaGanadoCosteo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaDetalle] DROP CONSTRAINT [FK_EntradaDetalle_EntradaGanadoCosteo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaDetalle] DROP CONSTRAINT [FK_EntradaDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoDetalle_TipoGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaDetalle] DROP CONSTRAINT [FK_EntradaGanadoDetalle_TipoGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaDetalle] DROP CONSTRAINT [FK_EntradaDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Camion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Camion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Chofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Chofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Embarque]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Embarque]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Jaula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Jaula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanado_OrganizacionOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanado] DROP CONSTRAINT [FK_EntradaGanado_OrganizacionOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCalidad_CalidadGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCalidad] DROP CONSTRAINT [FK_EntradaGanadoCalidad_CalidadGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCalidad_EntradaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCalidad] DROP CONSTRAINT [FK_EntradaGanadoCalidad_EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCalidad_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCalidad] DROP CONSTRAINT [FK_EntradaGanadoCalidad_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCalidad_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCalidad] DROP CONSTRAINT [FK_EntradaGanadoCalidad_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosteo_EntradaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosteo] DROP CONSTRAINT [FK_EntradaGanadoCosteo_EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosteo_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosteo] DROP CONSTRAINT [FK_EntradaGanadoCosteo_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosteo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosteo] DROP CONSTRAINT [FK_EntradaGanadoCosteo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosteo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosteo] DROP CONSTRAINT [FK_EntradaGanadoCosteo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCostoEntrada_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosto] DROP CONSTRAINT [FK_EntradaGanadoCostoEntrada_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosto] DROP CONSTRAINT [FK_EntradaGanadoCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosto] DROP CONSTRAINT [FK_EntradaGanadoCosto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoCosto_EntradaGanadoCosteo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoCosto] DROP CONSTRAINT [FK_EntradaGanadoCosto_EntradaGanadoCosteo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoMuerte_EntradaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoMuerte] DROP CONSTRAINT [FK_EntradaGanadoMuerte_EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoMuerte_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoMuerte] DROP CONSTRAINT [FK_EntradaGanadoMuerte_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoMuerte_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoMuerte] DROP CONSTRAINT [FK_EntradaGanadoMuerte_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoSobrante_EntradaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoSobrante] DROP CONSTRAINT [FK_EntradaGanadoSobrante_EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoSobrante_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoSobrante] DROP CONSTRAINT [FK_EntradaGanadoSobrante_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoSobrante_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoSobrante] DROP CONSTRAINT [FK_EntradaGanadoSobrante_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoTransito_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoTransito] DROP CONSTRAINT [FK_EntradaGanadoTransito_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoTransito_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoTransito] DROP CONSTRAINT [FK_EntradaGanadoTransito_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaGanadoTransito_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaGanadoTransito] DROP CONSTRAINT [FK_EntradaGanadoTransito_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaPremezcla_AlmacenMovimientoIDEntrada]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaPremezcla] DROP CONSTRAINT [FK_EntradaPremezcla_AlmacenMovimientoIDEntrada]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaPremezcla_AlmacenMovimientoIDSalida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaPremezcla] DROP CONSTRAINT [FK_EntradaPremezcla_AlmacenMovimientoIDSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaPremezcla_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaPremezcla] DROP CONSTRAINT [FK_EntradaPremezcla_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaPremezcla_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaPremezcla] DROP CONSTRAINT [FK_EntradaPremezcla_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_AlmacenInventarioLote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_AlmacenMovimientoSalida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_AlmacenMovimientoSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_OperadorAlmacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_OperadorAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_OperadorAnalista]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_OperadorAnalista]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_OperadorAutoriza]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_OperadorAutoriza]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_OperadorBascula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_OperadorBascula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_RegistroVigilancia]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_RegistroVigilancia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_TipoContrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_TipoContrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProducto] DROP CONSTRAINT [FK_EntradaProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoCosto] DROP CONSTRAINT [FK_EntradaProductoCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoCosto_EntradaProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoCosto] DROP CONSTRAINT [FK_EntradaProductoCosto_EntradaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoCosto_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoCosto] DROP CONSTRAINT [FK_EntradaProductoCosto_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoCosto] DROP CONSTRAINT [FK_EntradaProductoCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoCosto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoCosto] DROP CONSTRAINT [FK_EntradaProductoCosto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoDetalle_EntradaProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoDetalle] DROP CONSTRAINT [FK_EntradaProductoDetalle_EntradaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoDetalle_Indicador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoDetalle] DROP CONSTRAINT [FK_EntradaProductoDetalle_Indicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoDetalle] DROP CONSTRAINT [FK_EntradaProductoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoDetalle] DROP CONSTRAINT [FK_EntradaProductoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoMuestra_EntradaProductoDetalle]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoMuestra] DROP CONSTRAINT [FK_EntradaProductoMuestra_EntradaProductoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoMuestra_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoMuestra] DROP CONSTRAINT [FK_EntradaProductoMuestra_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoMuestra_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoMuestra] DROP CONSTRAINT [FK_EntradaProductoMuestra_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoParcial_ContratoParcial]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoParcial] DROP CONSTRAINT [FK_EntradaProductoParcial_ContratoParcial]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoParcial_EntradaProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoParcial] DROP CONSTRAINT [FK_EntradaProductoParcial_EntradaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoParcial_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoParcial] DROP CONSTRAINT [FK_EntradaProductoParcial_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EntradaProductoParcial_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EntradaProductoParcial] DROP CONSTRAINT [FK_EntradaProductoParcial_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstadoComedero_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstadoComedero] DROP CONSTRAINT [FK_EstadoComedero_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstadoComedero_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstadoComedero] DROP CONSTRAINT [FK_EstadoComedero_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstadoImplante_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstadoImplante] DROP CONSTRAINT [FK_EstadoImplante_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstadoImplante_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstadoImplante] DROP CONSTRAINT [FK_EstadoImplante_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstandarDescarga_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstandarDescarga] DROP CONSTRAINT [FK_EstandarDescarga_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstandarDescarga_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstandarDescarga] DROP CONSTRAINT [FK_EstandarDescarga_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstandarDescarga_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstandarDescarga] DROP CONSTRAINT [FK_EstandarDescarga_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EstandarDescarga_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EstandarDescarga] DROP CONSTRAINT [FK_EstandarDescarga_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Estatus_TipoEstatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Estatus] DROP CONSTRAINT [FK_Estatus_TipoEstatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Estatus_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Estatus] DROP CONSTRAINT [FK_Estatus_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Estatus_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Estatus] DROP CONSTRAINT [FK_Estatus_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorral_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorral] DROP CONSTRAINT [FK_EvaluacionCorral_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorral_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorral] DROP CONSTRAINT [FK_EvaluacionCorral_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorral_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorral] DROP CONSTRAINT [FK_EvaluacionCorral_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorral] DROP CONSTRAINT [FK_EvaluacionCorral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorral_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorral] DROP CONSTRAINT [FK_EvaluacionCorral_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorralDetalle_EvaluacionCorral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorralDetalle] DROP CONSTRAINT [FK_EvaluacionCorralDetalle_EvaluacionCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorralDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorralDetalle] DROP CONSTRAINT [FK_EvaluacionCorralDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorralDetalle_Pregunta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorralDetalle] DROP CONSTRAINT [FK_EvaluacionCorralDetalle_Pregunta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_EvaluacionCorralDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [EvaluacionCorralDetalle] DROP CONSTRAINT [FK_EvaluacionCorralDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Familia_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Familia] DROP CONSTRAINT [FK_Familia_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Familia_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Familia] DROP CONSTRAINT [FK_Familia_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Flete_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Flete] DROP CONSTRAINT [FK_Flete_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Flete_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Flete] DROP CONSTRAINT [FK_Flete_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Flete_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Flete] DROP CONSTRAINT [FK_Flete_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Flete_TipoTarifa]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Flete] DROP CONSTRAINT [FK_Flete_TipoTarifa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Flete_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Flete] DROP CONSTRAINT [FK_Flete_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Flete_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Flete] DROP CONSTRAINT [FK_Flete_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteDetalle_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteDetalle] DROP CONSTRAINT [FK_FleteDetalle_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteDetalle_Flete]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteDetalle] DROP CONSTRAINT [FK_FleteDetalle_Flete]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteDetalle_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteDetalle] DROP CONSTRAINT [FK_FleteDetalle_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteDetalle] DROP CONSTRAINT [FK_FleteDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteDetalle] DROP CONSTRAINT [FK_FleteDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_AlmacenIDDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_AlmacenIDDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_AlmacenIDOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_AlmacenIDOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInterno_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInterno] DROP CONSTRAINT [FK_FleteInterno_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoCosto] DROP CONSTRAINT [FK_FleteInternoCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoCosto_FleteInternoDetalle]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoCosto] DROP CONSTRAINT [FK_FleteInternoCosto_FleteInternoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoCosto] DROP CONSTRAINT [FK_FleteInternoCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoCosto_UsuarioModificar]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoCosto] DROP CONSTRAINT [FK_FleteInternoCosto_UsuarioModificar]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoDetalle_FleteInterno]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoDetalle] DROP CONSTRAINT [FK_FleteInternoDetalle_FleteInterno]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoDetalle_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoDetalle] DROP CONSTRAINT [FK_FleteInternoDetalle_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoDetalle_TipoTarifa]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoDetalle] DROP CONSTRAINT [FK_FleteInternoDetalle_TipoTarifa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoDetalle] DROP CONSTRAINT [FK_FleteInternoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteInternoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteInternoDetalle] DROP CONSTRAINT [FK_FleteInternoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteMermaPermitida_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteMermaPermitida] DROP CONSTRAINT [FK_FleteMermaPermitida_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteMermaPermitida_SubFamilia]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteMermaPermitida] DROP CONSTRAINT [FK_FleteMermaPermitida_SubFamilia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteMermaPermitida_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteMermaPermitida] DROP CONSTRAINT [FK_FleteMermaPermitida_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FleteMermaPermitida_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FleteMermaPermitida] DROP CONSTRAINT [FK_FleteMermaPermitida_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Folio_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Folio] DROP CONSTRAINT [FK_Folio_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Folio_TipoFolio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Folio] DROP CONSTRAINT [FK_Folio_TipoFolio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FolioAlmacen_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FolioAlmacen] DROP CONSTRAINT [FK_FolioAlmacen_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FolioAlmacen_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FolioAlmacen] DROP CONSTRAINT [FK_FolioAlmacen_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FolioLoteProducto_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FolioLoteProducto] DROP CONSTRAINT [FK_FolioLoteProducto_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_FolioLoteProducto_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [FolioLoteProducto] DROP CONSTRAINT [FK_FolioLoteProducto_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Formula_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Formula] DROP CONSTRAINT [FK_Formula_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Formula_TipoFormula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Formula] DROP CONSTRAINT [FK_Formula_TipoFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Formula_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Formula] DROP CONSTRAINT [FK_Formula_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Formula_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Formula] DROP CONSTRAINT [FK_Formula_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Formulario_Modulo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Formulario] DROP CONSTRAINT [FK_Formulario_Modulo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Formulario_Padre]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Formulario] DROP CONSTRAINT [FK_Formulario_Padre]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoCentroCosto_CentroCosto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoCentroCosto] DROP CONSTRAINT [FK_GastoCentroCosto_CentroCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoCentroCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoCentroCosto] DROP CONSTRAINT [FK_GastoCentroCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoCentroCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoCentroCosto] DROP CONSTRAINT [FK_GastoCentroCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoCentroCosto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoCentroCosto] DROP CONSTRAINT [FK_GastoCentroCosto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoInventario_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoInventario] DROP CONSTRAINT [FK_GastoInventario_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoInventario_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoInventario] DROP CONSTRAINT [FK_GastoInventario_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoInventario_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoInventario] DROP CONSTRAINT [FK_GastoInventario_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoInventario_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoInventario] DROP CONSTRAINT [FK_GastoInventario_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoInventario_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoInventario] DROP CONSTRAINT [FK_GastoInventario_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoInventario_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoInventario] DROP CONSTRAINT [FK_GastoInventario_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_AlmacenInventarioLote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GastoMateriaPrima_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GastoMateriaPrima] DROP CONSTRAINT [FK_GastoMateriaPrima_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Gelatinizacion_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Gelatinizacion] DROP CONSTRAINT [FK_Gelatinizacion_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Gelatinizacion_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Gelatinizacion] DROP CONSTRAINT [FK_Gelatinizacion_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Gelatinizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Gelatinizacion] DROP CONSTRAINT [FK_Gelatinizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Gelatinizacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Gelatinizacion] DROP CONSTRAINT [FK_Gelatinizacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GelatinizacionDetalle_Gelatinizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GelatinizacionDetalle] DROP CONSTRAINT [FK_GelatinizacionDetalle_Gelatinizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GelatinizacionDetalle_Roladora]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GelatinizacionDetalle] DROP CONSTRAINT [FK_GelatinizacionDetalle_Roladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GelatinizacionDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GelatinizacionDetalle] DROP CONSTRAINT [FK_GelatinizacionDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GelatinizacionDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GelatinizacionDetalle] DROP CONSTRAINT [FK_GelatinizacionDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Grado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Grado] DROP CONSTRAINT [FK_Grado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Grado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Grado] DROP CONSTRAINT [FK_Grado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GrupoCorral_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GrupoCorral] DROP CONSTRAINT [FK_GrupoCorral_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GrupoCorral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GrupoCorral] DROP CONSTRAINT [FK_GrupoCorral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GrupoFormulario_Acceso]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GrupoFormulario] DROP CONSTRAINT [FK_GrupoFormulario_Acceso]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GrupoFormulario_Formulario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GrupoFormulario] DROP CONSTRAINT [FK_GrupoFormulario_Formulario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_GrupoFormulario_Grupo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [GrupoFormulario] DROP CONSTRAINT [FK_GrupoFormulario_Grupo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Indicador_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Indicador] DROP CONSTRAINT [FK_Indicador_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Indicador_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Indicador] DROP CONSTRAINT [FK_Indicador_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorObjetivo_IndicadorProductoCalidad]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorObjetivo] DROP CONSTRAINT [FK_IndicadorObjetivo_IndicadorProductoCalidad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorObjetivo_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorObjetivo] DROP CONSTRAINT [FK_IndicadorObjetivo_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorObjetivo_TipoObjetivoCalidad]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorObjetivo] DROP CONSTRAINT [FK_IndicadorObjetivo_TipoObjetivoCalidad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorObjetivo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorObjetivo] DROP CONSTRAINT [FK_IndicadorObjetivo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorObjetivo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorObjetivo] DROP CONSTRAINT [FK_IndicadorObjetivo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProducto_Indicador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProducto] DROP CONSTRAINT [FK_IndicadorProducto_Indicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProducto_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProducto] DROP CONSTRAINT [FK_IndicadorProducto_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProducto] DROP CONSTRAINT [FK_IndicadorProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProducto] DROP CONSTRAINT [FK_IndicadorProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoBoleta_IndicadorProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoBoleta] DROP CONSTRAINT [FK_IndicadorProductoBoleta_IndicadorProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoBoleta_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoBoleta] DROP CONSTRAINT [FK_IndicadorProductoBoleta_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoBoleta_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoBoleta] DROP CONSTRAINT [FK_IndicadorProductoBoleta_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoBoleta_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoBoleta] DROP CONSTRAINT [FK_IndicadorProductoBoleta_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoCalidad_Indicador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoCalidad] DROP CONSTRAINT [FK_IndicadorProductoCalidad_Indicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoCalidad_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoCalidad] DROP CONSTRAINT [FK_IndicadorProductoCalidad_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoCalidad_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoCalidad] DROP CONSTRAINT [FK_IndicadorProductoCalidad_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_IndicadorProductoCalidad_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [IndicadorProductoCalidad] DROP CONSTRAINT [FK_IndicadorProductoCalidad_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Ingrediente_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Ingrediente] DROP CONSTRAINT [FK_Ingrediente_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Ingrediente_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Ingrediente] DROP CONSTRAINT [FK_Ingrediente_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Ingrediente_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Ingrediente] DROP CONSTRAINT [FK_Ingrediente_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Ingrediente_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Ingrediente] DROP CONSTRAINT [FK_Ingrediente_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Ingrediente_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Ingrediente] DROP CONSTRAINT [FK_Ingrediente_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceCosto] DROP CONSTRAINT [FK_InterfaceCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceCosto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceCosto] DROP CONSTRAINT [FK_InterfaceCosto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceCosto] DROP CONSTRAINT [FK_InterfaceCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceOrganizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceOrganizacion] DROP CONSTRAINT [FK_InterfaceOrganizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceOrganizacion_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceOrganizacion] DROP CONSTRAINT [FK_InterfaceOrganizacion_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceOrganizacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceOrganizacion] DROP CONSTRAINT [FK_InterfaceOrganizacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalida_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalida] DROP CONSTRAINT [FK_InterfaceSalida_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalida_OrganizacionOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalida] DROP CONSTRAINT [FK_InterfaceSalida_OrganizacionOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaAnimal_InterfaceSalidaDetalle]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaAnimal] DROP CONSTRAINT [FK_InterfaceSalidaAnimal_InterfaceSalidaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaAnimal_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaAnimal] DROP CONSTRAINT [FK_InterfaceSalidaAnimal_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaCosto_Costo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaCosto] DROP CONSTRAINT [FK_InterfaceSalidaCosto_Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaCosto_InterfaceSalidaAnimal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaCosto] DROP CONSTRAINT [FK_InterfaceSalidaCosto_InterfaceSalidaAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaDetalle_InterfaceSalida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaDetalle] DROP CONSTRAINT [FK_InterfaceSalidaDetalle_InterfaceSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaDetalle_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaDetalle] DROP CONSTRAINT [FK_InterfaceSalidaDetalle_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaTraspaso_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaTraspaso] DROP CONSTRAINT [FK_InterfaceSalidaTraspaso_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaTraspaso_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaTraspaso] DROP CONSTRAINT [FK_InterfaceSalidaTraspaso_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaTraspaso_OrganizacionIDDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaTraspaso] DROP CONSTRAINT [FK_InterfaceSalidaTraspaso_OrganizacionIDDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaTraspaso_TipoGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaTraspaso] DROP CONSTRAINT [FK_InterfaceSalidaTraspaso_TipoGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaTraspaso_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaTraspaso] DROP CONSTRAINT [FK_InterfaceSalidaTraspaso_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_InterfaceSalidaTraspaso_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [InterfaceSalidaTraspaso] DROP CONSTRAINT [FK_InterfaceSalidaTraspaso_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Iva_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Iva] DROP CONSTRAINT [FK_Iva_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Jaula_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Jaula] DROP CONSTRAINT [FK_Jaula_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Jaula_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Jaula] DROP CONSTRAINT [FK_Jaula_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Jaula_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Jaula] DROP CONSTRAINT [FK_Jaula_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistro_EstadoComedero]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistro] DROP CONSTRAINT [FK_LectorRegistro_EstadoComedero]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistro_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistro] DROP CONSTRAINT [FK_LectorRegistro_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistro_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistro] DROP CONSTRAINT [FK_LectorRegistro_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistro_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistro] DROP CONSTRAINT [FK_LectorRegistro_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistro_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistro] DROP CONSTRAINT [FK_LectorRegistro_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistroDetalle_FormulaAnterior]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistroDetalle] DROP CONSTRAINT [FK_LectorRegistroDetalle_FormulaAnterior]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistroDetalle_FormulaProgramada]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistroDetalle] DROP CONSTRAINT [FK_LectorRegistroDetalle_FormulaProgramada]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistroDetalle_LectorRegistro]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistroDetalle] DROP CONSTRAINT [FK_LectorRegistroDetalle_LectorRegistro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistroDetalle_TipoServicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistroDetalle] DROP CONSTRAINT [FK_LectorRegistroDetalle_TipoServicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistroDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistroDetalle] DROP CONSTRAINT [FK_LectorRegistroDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LectorRegistroDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LectorRegistroDetalle] DROP CONSTRAINT [FK_LectorRegistroDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Liquidacion_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Liquidacion] DROP CONSTRAINT [FK_Liquidacion_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Liquidacion_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Liquidacion] DROP CONSTRAINT [FK_Liquidacion_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Liquidacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Liquidacion] DROP CONSTRAINT [FK_Liquidacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Liquidacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Liquidacion] DROP CONSTRAINT [FK_Liquidacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LiquidacionDetalle_EntradaProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LiquidacionDetalle] DROP CONSTRAINT [FK_LiquidacionDetalle_EntradaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LiquidacionDetalle_Liquidacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LiquidacionDetalle] DROP CONSTRAINT [FK_LiquidacionDetalle_Liquidacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LiquidacionDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LiquidacionDetalle] DROP CONSTRAINT [FK_LiquidacionDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LiquidacionDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LiquidacionDetalle] DROP CONSTRAINT [FK_LiquidacionDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Lote_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Lote] DROP CONSTRAINT [FK_Lote_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Lote_TipoCorral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Lote] DROP CONSTRAINT [FK_Lote_TipoCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Lote_TipoProceso]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Lote] DROP CONSTRAINT [FK_Lote_TipoProceso]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteDistribucionAlimento_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteDistribucionAlimento] DROP CONSTRAINT [FK_LoteDistribucionAlimento_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteDistribucionAlimento_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteDistribucionAlimento] DROP CONSTRAINT [FK_LoteDistribucionAlimento_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteDistribucionAlimento_TipoServicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteDistribucionAlimento] DROP CONSTRAINT [FK_LoteDistribucionAlimento_TipoServicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteDistribucionAlimento_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteDistribucionAlimento] DROP CONSTRAINT [FK_LoteDistribucionAlimento_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteDistribucionAlimento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteDistribucionAlimento] DROP CONSTRAINT [FK_LoteDistribucionAlimento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteProyeccion_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteProyeccion] DROP CONSTRAINT [FK_LoteProyeccion_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteProyeccion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteProyeccion] DROP CONSTRAINT [FK_LoteProyeccion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteProyeccion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteProyeccion] DROP CONSTRAINT [FK_LoteProyeccion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteReimplante_LoteProyeccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteReimplante] DROP CONSTRAINT [FK_LoteReimplante_LoteProyeccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteReimplante_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteReimplante] DROP CONSTRAINT [FK_LoteReimplante_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteReimplante_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteReimplante] DROP CONSTRAINT [FK_LoteReimplante_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteSacrificio_Cliente]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteSacrificio] DROP CONSTRAINT [FK_LoteSacrificio_Cliente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteSacrificio_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteSacrificio] DROP CONSTRAINT [FK_LoteSacrificio_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteSacrificio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteSacrificio] DROP CONSTRAINT [FK_LoteSacrificio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteSacrificio_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteSacrificio] DROP CONSTRAINT [FK_LoteSacrificio_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_LoteSacrificioDetalle_LoteSacrificio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [LoteSacrificioDetalle] DROP CONSTRAINT [FK_LoteSacrificioDetalle_LoteSacrificio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MermasSuperavit_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MermaSuperavit] DROP CONSTRAINT [FK_MermasSuperavit_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MermasSuperavit_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MermaSuperavit] DROP CONSTRAINT [FK_MermasSuperavit_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MermasSuperavit_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MermaSuperavit] DROP CONSTRAINT [FK_MermasSuperavit_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Mezcladora_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Mezcladora] DROP CONSTRAINT [FK_Mezcladora_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Mezcladora_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Mezcladora] DROP CONSTRAINT [FK_Mezcladora_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Mezcladora_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Mezcladora] DROP CONSTRAINT [FK_Mezcladora_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Moneda_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Moneda] DROP CONSTRAINT [FK_Moneda_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Moneda_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Moneda] DROP CONSTRAINT [FK_Moneda_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSilo_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSilo] DROP CONSTRAINT [FK_MonitoreoSilo_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSilo_Silo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSilo] DROP CONSTRAINT [FK_MonitoreoSilo_Silo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSilo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSilo] DROP CONSTRAINT [FK_MonitoreoSilo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSilo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSilo] DROP CONSTRAINT [FK_MonitoreoSilo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloDetalle_MonitoreoSilo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloDetalle] DROP CONSTRAINT [FK_MonitoreoSiloDetalle_MonitoreoSilo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloDetalle_MonitoreoSiloIndicador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloDetalle] DROP CONSTRAINT [FK_MonitoreoSiloDetalle_MonitoreoSiloIndicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloDetalle] DROP CONSTRAINT [FK_MonitoreoSiloDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloDetalle] DROP CONSTRAINT [FK_MonitoreoSiloDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloIndicador_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloIndicador] DROP CONSTRAINT [FK_MonitoreoSiloIndicador_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloIndicador_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloIndicador] DROP CONSTRAINT [FK_MonitoreoSiloIndicador_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MonitoreoSiloIndicador_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MonitoreoSiloIndicador] DROP CONSTRAINT [FK_MonitoreoSiloIndicador_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_OperadorDeteccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_OperadorDeteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_OperadorNecropsia]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_OperadorNecropsia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_OperadorRecoleccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_OperadorRecoleccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_Problema]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_Problema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Muertes_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Muertes] DROP CONSTRAINT [FK_Muertes_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraFormula_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraFormula] DROP CONSTRAINT [FK_MuestreoFibraFormula_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraFormula_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraFormula] DROP CONSTRAINT [FK_MuestreoFibraFormula_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraFormula_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraFormula] DROP CONSTRAINT [FK_MuestreoFibraFormula_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraFormula_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraFormula] DROP CONSTRAINT [FK_MuestreoFibraFormula_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraIngrediente_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraIngrediente] DROP CONSTRAINT [FK_MuestreoFibraIngrediente_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraIngrediente_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraIngrediente] DROP CONSTRAINT [FK_MuestreoFibraIngrediente_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraIngrediente_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraIngrediente] DROP CONSTRAINT [FK_MuestreoFibraIngrediente_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_MuestreoFibraIngrediente_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [MuestreoFibraIngrediente] DROP CONSTRAINT [FK_MuestreoFibraIngrediente_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Notificaciones_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Notificaciones] DROP CONSTRAINT [FK_Notificaciones_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Notificaciones_AccionesSIAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Notificaciones] DROP CONSTRAINT [FK_Notificaciones_AccionesSIAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Notificaciones_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Notificaciones] DROP CONSTRAINT [FK_Notificaciones_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Observacion_TipoObservacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Observacion] DROP CONSTRAINT [FK_Observacion_TipoObservacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Observacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Observacion] DROP CONSTRAINT [FK_Observacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Observacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Observacion] DROP CONSTRAINT [FK_Observacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Operador_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Operador] DROP CONSTRAINT [FK_Operador_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Operador_Rol]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Operador] DROP CONSTRAINT [FK_Operador_Rol]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Operador_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Operador] DROP CONSTRAINT [FK_Operador_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Operador_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Operador] DROP CONSTRAINT [FK_Operador_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Operador_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Operador] DROP CONSTRAINT [FK_Operador_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificio_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificio] DROP CONSTRAINT [FK_OrdenSacrificio_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificio_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificio] DROP CONSTRAINT [FK_OrdenSacrificio_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificio_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificio] DROP CONSTRAINT [FK_OrdenSacrificio_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificio] DROP CONSTRAINT [FK_OrdenSacrificio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificioDetalle_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificioDetalle] DROP CONSTRAINT [FK_OrdenSacrificioDetalle_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificioDetalle_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificioDetalle] DROP CONSTRAINT [FK_OrdenSacrificioDetalle_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificioDetalle_OrdenSacrificio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificioDetalle] DROP CONSTRAINT [FK_OrdenSacrificioDetalle_OrdenSacrificio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificioDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificioDetalle] DROP CONSTRAINT [FK_OrdenSacrificioDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_OrdenSacrificioDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [OrdenSacrificioDetalle] DROP CONSTRAINT [FK_OrdenSacrificioDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Organizacion_Iva]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Organizacion] DROP CONSTRAINT [FK_Organizacion_Iva]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Organizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Organizacion] DROP CONSTRAINT [FK_Organizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Organizacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Organizacion] DROP CONSTRAINT [FK_Organizacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Parametro_TipoParametro]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Parametro] DROP CONSTRAINT [FK_Parametro_TipoParametro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Parametro_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Parametro] DROP CONSTRAINT [FK_Parametro_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Parametro_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Parametro] DROP CONSTRAINT [FK_Parametro_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroGeneral_Parametro]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroGeneral] DROP CONSTRAINT [FK_ParametroGeneral_Parametro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroGeneral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroGeneral] DROP CONSTRAINT [FK_ParametroGeneral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroGeneral_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroGeneral] DROP CONSTRAINT [FK_ParametroGeneral_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroOrganizacion_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroOrganizacion] DROP CONSTRAINT [FK_ParametroOrganizacion_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroOrganizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroOrganizacion] DROP CONSTRAINT [FK_ParametroOrganizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroOrganizacion_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroOrganizacion] DROP CONSTRAINT [FK_ParametroOrganizacion_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroOrganizacion_Parametro]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroOrganizacion] DROP CONSTRAINT [FK_ParametroOrganizacion_Parametro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroTrampa_Parametro]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroTrampa] DROP CONSTRAINT [FK_ParametroTrampa_Parametro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroTrampa_Trampa]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroTrampa] DROP CONSTRAINT [FK_ParametroTrampa_Trampa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroTrampa_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroTrampa] DROP CONSTRAINT [FK_ParametroTrampa_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ParametroTrampa_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ParametroTrampa] DROP CONSTRAINT [FK_ParametroTrampa_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pedido_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pedido] DROP CONSTRAINT [FK_Pedido_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pedido_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pedido] DROP CONSTRAINT [FK_Pedido_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pedido_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pedido] DROP CONSTRAINT [FK_Pedido_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pedido_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pedido] DROP CONSTRAINT [FK_Pedido_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pedido_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pedido] DROP CONSTRAINT [FK_Pedido_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PedidoDetalle_AlmacenInventarioLote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PedidoDetalle] DROP CONSTRAINT [FK_PedidoDetalle_AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PedidoDetalle_Pedido]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PedidoDetalle] DROP CONSTRAINT [FK_PedidoDetalle_Pedido]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PedidoDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PedidoDetalle] DROP CONSTRAINT [FK_PedidoDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PedidoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PedidoDetalle] DROP CONSTRAINT [FK_PedidoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PedidoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PedidoDetalle] DROP CONSTRAINT [FK_PedidoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_AlmacenMovimientoDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_AlmacenMovimientoDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_AlmacenMovimientoOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_AlmacenMovimientoOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_Camion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_Camion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_ProgramacionMateriaPrima]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_ProgramacionMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_ProveedorChofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_ProveedorChofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_TipoPesaje]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_TipoPesaje]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PesajeMateriaPrima_UsuarioSurtido]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PesajeMateriaPrima] DROP CONSTRAINT [FK_PesajeMateriaPrima_UsuarioSurtido]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioGanado_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioGanado] DROP CONSTRAINT [FK_PrecioGanado_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioGanado_TipoGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioGanado] DROP CONSTRAINT [FK_PrecioGanado_TipoGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioGanado] DROP CONSTRAINT [FK_PrecioGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioGanado_UsuarioModifficacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioGanado] DROP CONSTRAINT [FK_PrecioGanado_UsuarioModifficacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioPAC_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioPAC] DROP CONSTRAINT [FK_PrecioPAC_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioPAC_TipoPAC]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioPAC] DROP CONSTRAINT [FK_PrecioPAC_TipoPAC]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioPAC_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioPAC] DROP CONSTRAINT [FK_PrecioPAC_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PrecioPAC_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PrecioPAC] DROP CONSTRAINT [FK_PrecioPAC_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pregunta_TipoPregunta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pregunta] DROP CONSTRAINT [FK_Pregunta_TipoPregunta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pregunta_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pregunta] DROP CONSTRAINT [FK_Pregunta_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Pregunta_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Pregunta] DROP CONSTRAINT [FK_Pregunta_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Premezcla_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Premezcla] DROP CONSTRAINT [FK_Premezcla_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Premezcla_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Premezcla] DROP CONSTRAINT [FK_Premezcla_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Premezcla_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Premezcla] DROP CONSTRAINT [FK_Premezcla_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Premezcla_UsuarioModificar]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Premezcla] DROP CONSTRAINT [FK_Premezcla_UsuarioModificar]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDetalle_Premezcla]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDetalle] DROP CONSTRAINT [FK_PremezclaDetalle_Premezcla]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDetalle] DROP CONSTRAINT [FK_PremezclaDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDetalle] DROP CONSTRAINT [FK_PremezclaDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDetalle] DROP CONSTRAINT [FK_PremezclaDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucion_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucion] DROP CONSTRAINT [FK_PremezclaDistribucion_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucion_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucion] DROP CONSTRAINT [FK_PremezclaDistribucion_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucion] DROP CONSTRAINT [FK_PremezclaDistribucion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucion_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucion] DROP CONSTRAINT [FK_PremezclaDistribucion_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucion] DROP CONSTRAINT [FK_PremezclaDistribucion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucionDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucionDetalle] DROP CONSTRAINT [FK_PremezclaDistribucionDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucionDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucionDetalle] DROP CONSTRAINT [FK_PremezclaDistribucionDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucionDetalle_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucionDetalle] DROP CONSTRAINT [FK_PremezclaDistribucionDetalle_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_PremezclaDistribucionDetalle_PremezclaDistribucion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [PremezclaDistribucionDetalle] DROP CONSTRAINT [FK_PremezclaDistribucionDetalle_PremezclaDistribucion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Problema_TipoProblema]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Problema] DROP CONSTRAINT [FK_Problema_TipoProblema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Problema_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Problema] DROP CONSTRAINT [FK_Problema_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Problema_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Problema] DROP CONSTRAINT [FK_Problema_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaSintoma_Problema]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaSintoma] DROP CONSTRAINT [FK_ProblemaSintoma_Problema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaSintoma_Sintoma]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaSintoma] DROP CONSTRAINT [FK_ProblemaSintoma_Sintoma]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaSintoma_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaSintoma] DROP CONSTRAINT [FK_ProblemaSintoma_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaTratamiento_Problema]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaTratamiento] DROP CONSTRAINT [FK_ProblemaTratamiento_Problema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaTratamiento_Tratamiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaTratamiento] DROP CONSTRAINT [FK_ProblemaTratamiento_Tratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaTratamiento_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaTratamiento] DROP CONSTRAINT [FK_ProblemaTratamiento_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProblemaTratamiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProblemaTratamiento] DROP CONSTRAINT [FK_ProblemaTratamiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiaria_UsuarioAutorizo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiaria] DROP CONSTRAINT [FK_ProduccionDiaria_UsuarioAutorizo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiaria_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiaria] DROP CONSTRAINT [FK_ProduccionDiaria_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiaria_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiaria] DROP CONSTRAINT [FK_ProduccionDiaria_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiariaDetalle_PesajeMateriaPrima]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiariaDetalle] DROP CONSTRAINT [FK_ProduccionDiariaDetalle_PesajeMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiariaDetalle_ProduccionDiaria]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiariaDetalle] DROP CONSTRAINT [FK_ProduccionDiariaDetalle_ProduccionDiaria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiariaDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiariaDetalle] DROP CONSTRAINT [FK_ProduccionDiariaDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiariaDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiariaDetalle] DROP CONSTRAINT [FK_ProduccionDiariaDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionDiariaDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionDiariaDetalle] DROP CONSTRAINT [FK_ProduccionDiariaDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormula_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormula] DROP CONSTRAINT [FK_ProduccionFormula_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormula_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormula] DROP CONSTRAINT [FK_ProduccionFormula_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormula_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormula] DROP CONSTRAINT [FK_ProduccionFormula_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormula_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormula] DROP CONSTRAINT [FK_ProduccionFormula_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormula_AlmacenMovimientoEntrada]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormula] DROP CONSTRAINT [FK_ProduccionFormula_AlmacenMovimientoEntrada]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormula_AlmacenMovimientoSalida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormula] DROP CONSTRAINT [FK_ProduccionFormula_AlmacenMovimientoSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_ProduccionFormula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_ProduccionFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_Rotomix]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_Rotomix]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaBatch_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaBatch] DROP CONSTRAINT [FK_ProduccionFormulaBatch_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaDetalle] DROP CONSTRAINT [FK_ProduccionFormulaDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaDetalle] DROP CONSTRAINT [FK_ProduccionFormulaDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaDetalle_ProgramacionFormula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaDetalle] DROP CONSTRAINT [FK_ProduccionFormulaDetalle_ProgramacionFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaDetalle_Ingrediente]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaDetalle] DROP CONSTRAINT [FK_ProduccionFormulaDetalle_Ingrediente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProduccionFormulaDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProduccionFormulaDetalle] DROP CONSTRAINT [FK_ProduccionFormulaDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Producto_SubFamilia]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Producto] DROP CONSTRAINT [FK_Producto_SubFamilia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Producto_UnidadMedicion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Producto] DROP CONSTRAINT [FK_Producto_UnidadMedicion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Producto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Producto] DROP CONSTRAINT [FK_Producto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Producto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Producto] DROP CONSTRAINT [FK_Producto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProductoCodigoParte_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProductoCodigoParte] DROP CONSTRAINT [FK_ProductoCodigoParte_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProductoCodigoParte_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProductoCodigoParte] DROP CONSTRAINT [FK_ProductoCodigoParte_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProductoCodigoParte_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProductoCodigoParte] DROP CONSTRAINT [FK_ProductoCodigoParte_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionCorte_EntradaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionCorte] DROP CONSTRAINT [FK_ProgramacionCorte_EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionCorte_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionCorte] DROP CONSTRAINT [FK_ProgramacionCorte_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionCorte_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionCorte] DROP CONSTRAINT [FK_ProgramacionCorte_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionCorte_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionCorte] DROP CONSTRAINT [FK_ProgramacionCorte_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_AutorizacionMateriaPrima]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_AutorizacionMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_AlmacenInventarioLote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_PedidoDetalle]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_PedidoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionMateriaPrima_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionMateriaPrima] DROP CONSTRAINT [FK_ProgramacionMateriaPrima_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplante_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplante] DROP CONSTRAINT [FK_ProgramacionReimplante_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplante_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplante] DROP CONSTRAINT [FK_ProgramacionReimplante_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplante_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplante] DROP CONSTRAINT [FK_ProgramacionReimplante_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplante_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplante] DROP CONSTRAINT [FK_ProgramacionReimplante_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplante_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplante] DROP CONSTRAINT [FK_ProgramacionReimplante_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplanteDetalle_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplanteDetalle] DROP CONSTRAINT [FK_ProgramacionReimplanteDetalle_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplanteDetalle_ProgramacionReimplante]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplanteDetalle] DROP CONSTRAINT [FK_ProgramacionReimplanteDetalle_ProgramacionReimplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplanteDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplanteDetalle] DROP CONSTRAINT [FK_ProgramacionReimplanteDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProgramacionReimplanteDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProgramacionReimplanteDetalle] DROP CONSTRAINT [FK_ProgramacionReimplanteDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Proveedor_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Proveedor] DROP CONSTRAINT [FK_Proveedor_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Proveedor_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Proveedor] DROP CONSTRAINT [FK_Proveedor_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProveedorID]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Proveedor] DROP CONSTRAINT [FK_TipoProveedorID]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProveedorAlmacen_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProveedorAlmacen] DROP CONSTRAINT [FK_ProveedorAlmacen_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProveedorAlmacen_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProveedorAlmacen] DROP CONSTRAINT [FK_ProveedorAlmacen_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProveedorAlmacen_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProveedorAlmacen] DROP CONSTRAINT [FK_ProveedorAlmacen_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProveedorAlmacen_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProveedorAlmacen] DROP CONSTRAINT [FK_ProveedorAlmacen_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProveedorChofer_Chofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProveedorChofer] DROP CONSTRAINT [FK_ProveedorChofer_Chofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProveedorChofer_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ProveedorChofer] DROP CONSTRAINT [FK_ProveedorChofer_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProducto_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProducto] DROP CONSTRAINT [FK_RecepcionProducto_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProducto_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProducto] DROP CONSTRAINT [FK_RecepcionProducto_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProducto_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProducto] DROP CONSTRAINT [FK_RecepcionProducto_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProducto] DROP CONSTRAINT [FK_RecepcionProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProducto] DROP CONSTRAINT [FK_RecepcionProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProductoDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProductoDetalle] DROP CONSTRAINT [FK_RecepcionProductoDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProductoDetalle_RecepcionProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProductoDetalle] DROP CONSTRAINT [FK_RecepcionProductoDetalle_RecepcionProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProductoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProductoDetalle] DROP CONSTRAINT [FK_RecepcionProductoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RecepcionProductoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RecepcionProductoDetalle] DROP CONSTRAINT [FK_RecepcionProductoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_Camion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_Camion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_Contrato]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_Proveedor]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_ProveedorChofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_ProveedorChofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RegistroVigilancia_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RegistroVigilancia] DROP CONSTRAINT [FK_RegistroVigilancia_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Reparto_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Reparto] DROP CONSTRAINT [FK_Reparto_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Reparto_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Reparto] DROP CONSTRAINT [FK_Reparto_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Reparto_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Reparto] DROP CONSTRAINT [FK_Reparto_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Reparto_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Reparto] DROP CONSTRAINT [FK_Reparto_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Reparto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Reparto] DROP CONSTRAINT [FK_Reparto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimento_CamionReparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimento] DROP CONSTRAINT [FK_RepartoAlimento_CamionReparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimento_TipoServicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimento] DROP CONSTRAINT [FK_RepartoAlimento_TipoServicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimento] DROP CONSTRAINT [FK_RepartoAlimento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimento] DROP CONSTRAINT [FK_RepartoAlimento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimento_UsuarioReparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimento] DROP CONSTRAINT [FK_RepartoAlimento_UsuarioReparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimentoDetalle_CorralFinal]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimentoDetalle] DROP CONSTRAINT [FK_RepartoAlimentoDetalle_CorralFinal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimentoDetalle_CorralInicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimentoDetalle] DROP CONSTRAINT [FK_RepartoAlimentoDetalle_CorralInicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimentoDetalle_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimentoDetalle] DROP CONSTRAINT [FK_RepartoAlimentoDetalle_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimentoDetalle_RepartoAlimento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimentoDetalle] DROP CONSTRAINT [FK_RepartoAlimentoDetalle_RepartoAlimento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimentoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimentoDetalle] DROP CONSTRAINT [FK_RepartoAlimentoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAlimentoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAlimentoDetalle] DROP CONSTRAINT [FK_RepartoAlimentoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoAvance_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoAvance] DROP CONSTRAINT [FK_RepartoAvance_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_CamionReparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_CamionReparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_EstadoComedero]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_EstadoComedero]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_FormulaProgramada]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_FormulaProgramada]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_FormulaServida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_FormulaServida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_Reparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_Reparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_TipoServicio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_TipoServicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RepartoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RepartoDetalle] DROP CONSTRAINT [FK_RepartoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Retencion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Retencion] DROP CONSTRAINT [FK_Retencion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Retencion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Retencion] DROP CONSTRAINT [FK_Retencion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RevisionImplante_AreaRevision]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RevisionImplante] DROP CONSTRAINT [FK_RevisionImplante_AreaRevision]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RevisionImplante_EstadoImplante]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RevisionImplante] DROP CONSTRAINT [FK_RevisionImplante_EstadoImplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RevisionImplante_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RevisionImplante] DROP CONSTRAINT [FK_RevisionImplante_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RevisionImplante_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RevisionImplante] DROP CONSTRAINT [FK_RevisionImplante_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_RevisionImplante_UsuarioModificcion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [RevisionImplante] DROP CONSTRAINT [FK_RevisionImplante_UsuarioModificcion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Rol_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Rol] DROP CONSTRAINT [FK_Rol_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Rol_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Rol] DROP CONSTRAINT [FK_Rol_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Roladora_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Roladora] DROP CONSTRAINT [FK_Roladora_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Roladora_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Roladora] DROP CONSTRAINT [FK_Roladora_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Roladora_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Roladora] DROP CONSTRAINT [FK_Roladora_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Rotomix_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Rotomix] DROP CONSTRAINT [FK_Rotomix_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Rotomix_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Rotomix] DROP CONSTRAINT [FK_Rotomix_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Rotomix_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Rotomix] DROP CONSTRAINT [FK_Rotomix_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaAnimal_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaAnimal] DROP CONSTRAINT [FK_SalidaAnimal_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaAnimal_SalidaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaAnimal] DROP CONSTRAINT [FK_SalidaAnimal_SalidaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaAnimal_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaAnimal] DROP CONSTRAINT [FK_SalidaAnimal_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaAnimal_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaAnimal] DROP CONSTRAINT [FK_SalidaAnimal_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaGanado_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaGanado] DROP CONSTRAINT [FK_SalidaGanado_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaGanado_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaGanado] DROP CONSTRAINT [FK_SalidaGanado_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaGanado_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaGanado] DROP CONSTRAINT [FK_SalidaGanado_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaGanado] DROP CONSTRAINT [FK_SalidaGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaGanado_VentaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaGanado] DROP CONSTRAINT [FK_SalidaGanado_VentaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_AutorizacionMateriaPrima]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_AutorizacionMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_Camion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_Camion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_Chofer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_Chofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_Cliente]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_Cliente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_OrganizacionDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_OrganizacionDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_TipoMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SalidaProducto_AlmacenInventarioLote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SalidaProducto] DROP CONSTRAINT [FK_SalidaProducto_AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ServicioAlimento_Corral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ServicioAlimento] DROP CONSTRAINT [FK_ServicioAlimento_Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ServicioAlimento_Formula]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ServicioAlimento] DROP CONSTRAINT [FK_ServicioAlimento_Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ServicioAlimento_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ServicioAlimento] DROP CONSTRAINT [FK_ServicioAlimento_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ServicioAlimento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ServicioAlimento] DROP CONSTRAINT [FK_ServicioAlimento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ServicioAlimento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [ServicioAlimento] DROP CONSTRAINT [FK_ServicioAlimento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Silo_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Silo] DROP CONSTRAINT [FK_Silo_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Silo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Silo] DROP CONSTRAINT [FK_Silo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Silo_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Silo] DROP CONSTRAINT [FK_Silo_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Sintoma_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Sintoma] DROP CONSTRAINT [FK_Sintoma_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Sintoma_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Sintoma] DROP CONSTRAINT [FK_Sintoma_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezcla_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezcla] DROP CONSTRAINT [FK_SolicitudPremezcla_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezcla_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezcla] DROP CONSTRAINT [FK_SolicitudPremezcla_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezcla_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezcla] DROP CONSTRAINT [FK_SolicitudPremezcla_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezclaDetalle_Premezcla]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezclaDetalle] DROP CONSTRAINT [FK_SolicitudPremezclaDetalle_Premezcla]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezclaDetalle_SolicitudPremezcla]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezclaDetalle] DROP CONSTRAINT [FK_SolicitudPremezclaDetalle_SolicitudPremezcla]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezclaDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezclaDetalle] DROP CONSTRAINT [FK_SolicitudPremezclaDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudPremezclaDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudPremezclaDetalle] DROP CONSTRAINT [FK_SolicitudPremezclaDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_Almacen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_AlmacenMovimiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_CentroCosto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_CentroCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_UsuarioAutoriza]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_UsuarioAutoriza]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_UsuarioEntrega]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_UsuarioEntrega]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProducto_UsuarioSolicita]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProducto] DROP CONSTRAINT [FK_SolicitudProducto_UsuarioSolicita]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProductoDetalle_CamionReparto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProductoDetalle] DROP CONSTRAINT [FK_SolicitudProductoDetalle_CamionReparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProductoDetalle_Estatus]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProductoDetalle] DROP CONSTRAINT [FK_SolicitudProductoDetalle_Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProductoDetalle_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProductoDetalle] DROP CONSTRAINT [FK_SolicitudProductoDetalle_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProductoDetalle_SolicitudProducto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProductoDetalle] DROP CONSTRAINT [FK_SolicitudProductoDetalle_SolicitudProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProductoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProductoDetalle] DROP CONSTRAINT [FK_SolicitudProductoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SolicitudProductoDetalle_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SolicitudProductoDetalle] DROP CONSTRAINT [FK_SolicitudProductoDetalle_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SubFamilia_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SubFamilia] DROP CONSTRAINT [FK_SubFamilia_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SubFamilia_Familia]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SubFamilia] DROP CONSTRAINT [FK_SubFamilia_Familia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SubFamilia_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SubFamilia] DROP CONSTRAINT [FK_SubFamilia_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SubFamilia_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SubFamilia] DROP CONSTRAINT [FK_SubFamilia_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectores_CriterioSupervision]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectores] DROP CONSTRAINT [FK_SupervisionDetectores_CriterioSupervision]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectores_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectores] DROP CONSTRAINT [FK_SupervisionDetectores_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectores_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectores] DROP CONSTRAINT [FK_SupervisionDetectores_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectores_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectores] DROP CONSTRAINT [FK_SupervisionDetectores_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectores_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectores] DROP CONSTRAINT [FK_SupervisionDetectores_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectoresDetalle_Pregunta]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectoresDetalle] DROP CONSTRAINT [FK_SupervisionDetectoresDetalle_Pregunta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectoresDetalle_SupervisionDetectores]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectoresDetalle] DROP CONSTRAINT [FK_SupervisionDetectoresDetalle_SupervisionDetectores]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectoresDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectoresDetalle] DROP CONSTRAINT [FK_SupervisionDetectoresDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionDetectoresDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionDetectoresDetalle] DROP CONSTRAINT [FK_SupervisionDetectoresDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionGanado_ConceptoDeteccion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionGanado] DROP CONSTRAINT [FK_SupervisionGanado_ConceptoDeteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionGanado_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionGanado] DROP CONSTRAINT [FK_SupervisionGanado_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionGanado] DROP CONSTRAINT [FK_SupervisionGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisionGanado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisionGanado] DROP CONSTRAINT [FK_SupervisionGanado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisorEnfermeria_Enfermeria]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisorEnfermeria] DROP CONSTRAINT [FK_SupervisorEnfermeria_Enfermeria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisorEnfermeria_Operador]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisorEnfermeria] DROP CONSTRAINT [FK_SupervisorEnfermeria_Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisorEnfermeria_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisorEnfermeria] DROP CONSTRAINT [FK_SupervisorEnfermeria_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_SupervisorEnfermeria_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [SupervisorEnfermeria] DROP CONSTRAINT [FK_SupervisorEnfermeria_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TarifaSecado_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TarifaSecado] DROP CONSTRAINT [FK_TarifaSecado_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TarifaSecado_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TarifaSecado] DROP CONSTRAINT [FK_TarifaSecado_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TarifaSecado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TarifaSecado] DROP CONSTRAINT [FK_TarifaSecado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TarifaSecado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TarifaSecado] DROP CONSTRAINT [FK_TarifaSecado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TiempoMuerto_CausaTiempoMuerto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TiempoMuerto] DROP CONSTRAINT [FK_TiempoMuerto_CausaTiempoMuerto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TiempoMuerto_ProduccionDiaria]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TiempoMuerto] DROP CONSTRAINT [FK_TiempoMuerto_ProduccionDiaria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TiempoMuerto_RepartoAlimento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TiempoMuerto] DROP CONSTRAINT [FK_TiempoMuerto_RepartoAlimento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TiempoMuerto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TiempoMuerto] DROP CONSTRAINT [FK_TiempoMuerto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TiempoMuerto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TiempoMuerto] DROP CONSTRAINT [FK_TiempoMuerto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoAlmacen_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoAlmacen] DROP CONSTRAINT [FK_TipoAlmacen_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoAlmacen_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoAlmacen] DROP CONSTRAINT [FK_TipoAlmacen_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoAutorizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoAutorizacion] DROP CONSTRAINT [FK_TipoAutorizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoAutorizacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoAutorizacion] DROP CONSTRAINT [FK_TipoAutorizacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCambio_Moneda]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCambio] DROP CONSTRAINT [FK_TipoCambio_Moneda]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCambio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCambio] DROP CONSTRAINT [FK_TipoCambio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCambio_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCambio] DROP CONSTRAINT [FK_TipoCambio_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCancelacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCancelacion] DROP CONSTRAINT [FK_TipoCancelacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCancelacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCancelacion] DROP CONSTRAINT [FK_TipoCancelacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoContrato_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoContrato] DROP CONSTRAINT [FK_TipoContrato_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCorral_GrupoCorral]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCorral] DROP CONSTRAINT [FK_TipoCorral_GrupoCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCorral_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCorral] DROP CONSTRAINT [FK_TipoCorral_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCorral_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCorral] DROP CONSTRAINT [FK_TipoCorral_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCosto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCosto] DROP CONSTRAINT [FK_TipoCosto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCosto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCosto] DROP CONSTRAINT [FK_TipoCosto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCuenta_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCuenta] DROP CONSTRAINT [FK_TipoCuenta_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoCuenta_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoCuenta] DROP CONSTRAINT [FK_TipoCuenta_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoDeteccion_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoDeteccion] DROP CONSTRAINT [FK_TipoDeteccion_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoDeteccion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoDeteccion] DROP CONSTRAINT [FK_TipoDeteccion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoEmbarque_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoEmbarque] DROP CONSTRAINT [FK_TipoEmbarque_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoEmbarque_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoEmbarque] DROP CONSTRAINT [FK_TipoEmbarque_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoEstatus_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoEstatus] DROP CONSTRAINT [FK_TipoEstatus_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoEstatus_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoEstatus] DROP CONSTRAINT [FK_TipoEstatus_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoFlete_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoFlete] DROP CONSTRAINT [FK_TipoFlete_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoFlete_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoFlete] DROP CONSTRAINT [FK_TipoFlete_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoFormula_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoFormula] DROP CONSTRAINT [FK_TipoFormula_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoFormula_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoFormula] DROP CONSTRAINT [FK_TipoFormula_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoGanado_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoGanado] DROP CONSTRAINT [FK_TipoGanado_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoGanado] DROP CONSTRAINT [FK_TipoGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoGanadoCorrales_TipoGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoGanadoCorrales] DROP CONSTRAINT [FK_TipoGanadoCorrales_TipoGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoGanadoCorrales_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoGanadoCorrales] DROP CONSTRAINT [FK_TipoGanadoCorrales_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoGanadoCorrales_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoGanadoCorrales] DROP CONSTRAINT [FK_TipoGanadoCorrales_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoLugarMuestra_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoLugarMuestra] DROP CONSTRAINT [FK_TipoLugarMuestra_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoLugarMuestra_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoLugarMuestra] DROP CONSTRAINT [FK_TipoLugarMuestra_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoMovimiento_TipoPoliza]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoMovimiento] DROP CONSTRAINT [FK_TipoMovimiento_TipoPoliza]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoMovimiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoMovimiento] DROP CONSTRAINT [FK_TipoMovimiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoMovimiento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoMovimiento] DROP CONSTRAINT [FK_TipoMovimiento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoMuestra_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoMuestra] DROP CONSTRAINT [FK_TipoMuestra_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoMuestra_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoMuestra] DROP CONSTRAINT [FK_TipoMuestra_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoObjetivoCalidad_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoObjetivoCalidad] DROP CONSTRAINT [FK_TipoObjetivoCalidad_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoObjetivoCalidad_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoObjetivoCalidad] DROP CONSTRAINT [FK_TipoObjetivoCalidad_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoObservacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoObservacion] DROP CONSTRAINT [FK_TipoObservacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoObservacion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoObservacion] DROP CONSTRAINT [FK_TipoObservacion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoOrganizacion_TipoProceso]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoOrganizacion] DROP CONSTRAINT [FK_TipoOrganizacion_TipoProceso]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoOrganizacion_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoOrganizacion] DROP CONSTRAINT [FK_TipoOrganizacion_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoOrganizacion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoOrganizacion] DROP CONSTRAINT [FK_TipoOrganizacion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPAC_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPAC] DROP CONSTRAINT [FK_TipoPAC_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPAC_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPAC] DROP CONSTRAINT [FK_TipoPAC_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoParametro_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoParametro] DROP CONSTRAINT [FK_TipoParametro_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoParametro_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoParametro] DROP CONSTRAINT [FK_TipoParametro_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPesaje_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPesaje] DROP CONSTRAINT [FK_TipoPesaje_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPesaje_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPesaje] DROP CONSTRAINT [FK_TipoPesaje_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPoliza_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPoliza] DROP CONSTRAINT [FK_TipoPoliza_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPoliza_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPoliza] DROP CONSTRAINT [FK_TipoPoliza_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPregunta_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPregunta] DROP CONSTRAINT [FK_TipoPregunta_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoPregunta_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoPregunta] DROP CONSTRAINT [FK_TipoPregunta_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProblema_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProblema] DROP CONSTRAINT [FK_TipoProblema_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProblema_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProblema] DROP CONSTRAINT [FK_TipoProblema_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProceso_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProceso] DROP CONSTRAINT [FK_TipoProceso_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProceso_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProceso] DROP CONSTRAINT [FK_TipoProceso_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProrrateo_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProrrateo] DROP CONSTRAINT [FK_TipoProrrateo_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProveedor_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProveedor] DROP CONSTRAINT [FK_TipoProveedor_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoProveedor_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoProveedor] DROP CONSTRAINT [FK_TipoProveedor_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoServicio_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoServicio] DROP CONSTRAINT [FK_TipoServicio_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoServicio_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoServicio] DROP CONSTRAINT [FK_TipoServicio_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoTarifa_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoTarifa] DROP CONSTRAINT [FK_TipoTarifa_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoTarifa_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoTarifa] DROP CONSTRAINT [FK_TipoTarifa_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoTecnica_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoTecnica] DROP CONSTRAINT [FK_TipoTecnica_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoTecnica_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoTecnica] DROP CONSTRAINT [FK_TipoTecnica_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoTratamiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoTratamiento] DROP CONSTRAINT [FK_TipoTratamiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TipoTratamiento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TipoTratamiento] DROP CONSTRAINT [FK_TipoTratamiento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Trampa_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Trampa] DROP CONSTRAINT [FK_Trampa_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Trampa_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Trampa] DROP CONSTRAINT [FK_Trampa_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Trampa_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Trampa] DROP CONSTRAINT [FK_Trampa_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_AlmacenInventarioLoteDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenInventarioLoteDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_AlmacenInventarioLoteOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenInventarioLoteOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_AlmacenMovimientoEntrada]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenMovimientoEntrada]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_AlmacenMovimientoSalida]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenMovimientoSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_CuentaSAP]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_ContratoOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_ContratoOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_ContratoDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_ContratoDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_AlmacenOrigen]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenOrigen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TraspasoMateriaPrima_AlmacenDestino]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TraspasoMateriaPrima] DROP CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenDestino]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Tratamiento_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Tratamiento] DROP CONSTRAINT [FK_Tratamiento_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Tratamiento_TipoTratamiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Tratamiento] DROP CONSTRAINT [FK_Tratamiento_TipoTratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Tratamiento_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Tratamiento] DROP CONSTRAINT [FK_Tratamiento_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Tratamiento_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Tratamiento] DROP CONSTRAINT [FK_Tratamiento_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TratamientoProducto_Producto]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TratamientoProducto] DROP CONSTRAINT [FK_TratamientoProducto_Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TratamientoProducto_Tratamiento]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TratamientoProducto] DROP CONSTRAINT [FK_TratamientoProducto_Tratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TratamientoProducto_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TratamientoProducto] DROP CONSTRAINT [FK_TratamientoProducto_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_TratamientoProducto_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [TratamientoProducto] DROP CONSTRAINT [FK_TratamientoProducto_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_UnidadMedicion_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [UnidadMedicion] DROP CONSTRAINT [FK_UnidadMedicion_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_UnidadMedicion_UsuarioModificacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [UnidadMedicion] DROP CONSTRAINT [FK_UnidadMedicion_UsuarioModificacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Usuario_Organizacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Usuario] DROP CONSTRAINT [FK_Usuario_Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_UsuarioGrupo_Grupo]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [UsuarioGrupo] DROP CONSTRAINT [FK_UsuarioGrupo_Grupo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_UsuarioGrupo_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [UsuarioGrupo] DROP CONSTRAINT [FK_UsuarioGrupo_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanado_Cliente]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanado] DROP CONSTRAINT [FK_VentaGanado_Cliente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanado_Lote]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanado] DROP CONSTRAINT [FK_VentaGanado_Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanado_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanado] DROP CONSTRAINT [FK_VentaGanado_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanado_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanado] DROP CONSTRAINT [FK_VentaGanado_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanadoDetalle_CausaPrecio]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanadoDetalle] DROP CONSTRAINT [FK_VentaGanadoDetalle_CausaPrecio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanadoDetalle_Usuario]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanadoDetalle] DROP CONSTRAINT [FK_VentaGanadoDetalle_Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanadoDetalle_UsuarioCreacion]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanadoDetalle] DROP CONSTRAINT [FK_VentaGanadoDetalle_UsuarioCreacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_VentaGanadoDetalle_VentaGanado]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [VentaGanadoDetalle] DROP CONSTRAINT [FK_VentaGanadoDetalle_VentaGanado]
; GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Acceso]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Acceso]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AccionesSIAP]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AccionesSIAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Almacen]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Almacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmacenInventario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmacenInventario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmacenInventarioLote]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmacenInventarioLote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmacenMovimiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmacenMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmacenMovimientoCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmacenMovimientoCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmacenMovimientoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmacenMovimientoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmacenUsuario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmacenUsuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmidonFactor]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmidonFactor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmidonHeces]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmidonHeces]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AlmidonHecesDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AlmidonHecesDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnalisisGrasa]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnalisisGrasa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Animal]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Animal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalConsumo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalConsumo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalConsumoHistorico]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalConsumoHistorico]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalCostoHistorico]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalCostoHistorico]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalHistorico]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalHistorico]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalMovimiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalMovimientoHistorico]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalMovimientoHistorico]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AnimalSalida]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AnimalSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AreaRevision]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AreaRevision]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AutorizacionMateriaPrima]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AutorizacionMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[BitacoraErrores]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [BitacoraErrores]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Calculo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Calculo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CalidadGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CalidadGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CalidadMateriaPrima]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CalidadMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CalidadMezclado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CalidadMezclado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CalidadMezcladoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CalidadMezcladoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CalidadMezcladoFactor]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CalidadMezcladoFactor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Camion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Camion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CamionReparto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CamionReparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CancelacionMovimiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CancelacionMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CausaPrecio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CausaPrecio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CausaRechazo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CausaRechazo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CausaSalida]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CausaSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CausaTiempoMuerto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CausaTiempoMuerto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CentroBeneficio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CentroBeneficio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CentroCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CentroCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CentroCostoUsuario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CentroCostoUsuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListCorral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListRoladora]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListRoladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListRoladoraAccion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListRoladoraAccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListRoladoraDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListRoladoraDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListRoladoraGeneral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListRoladoraGeneral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListRoladoraHorometro]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListRoladoraHorometro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CheckListRoladoraRango]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CheckListRoladoraRango]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Chofer]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Chofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ClaseCostoProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ClaseCostoProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ClasificacionGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ClasificacionGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Cliente]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Cliente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ColorObjetivo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ColorObjetivo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ConceptoDeteccion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ConceptoDeteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Condicion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Condicion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CondicionJaula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CondicionJaula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ConfiguracionEmbarque]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ConfiguracionEmbarque]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ConfiguracionFormula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ConfiguracionFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ConfiguracionImplante]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ConfiguracionImplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ConfiguracionSemana]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ConfiguracionSemana]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ConfiguracionTraspasoAlmacen]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ConfiguracionTraspasoAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Constantes]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Constantes]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Contrato]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Contrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ContratoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ContratoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ContratoHumedad]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ContratoHumedad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ContratoParcial]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ContratoParcial]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Corral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Corral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CorralDetector]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CorralDetector]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CorralLector]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CorralLector]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CorralRango]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CorralRango]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Costo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Costo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CostoEmbarqueDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CostoEmbarqueDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CostoOrganizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CostoOrganizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CostoPromedio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CostoPromedio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CriterioSupervision]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CriterioSupervision]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Cuenta]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Cuenta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CuentaAlmacenSubFamilia]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CuentaAlmacenSubFamilia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CuentaSAP]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CuentaSAP]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[CuentaValor]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [CuentaValor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[DescripcionGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [DescripcionGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Deteccion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Deteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[DeteccionAnimal]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [DeteccionAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[DeteccionSintoma]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [DeteccionSintoma]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[DeteccionSintomaAnimal]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [DeteccionSintomaAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[DiagnosticoAnalista]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [DiagnosticoAnalista]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[DiagnosticoAnalistaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [DiagnosticoAnalistaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Embarque]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Embarque]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EmbarqueDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EmbarqueDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Enfermeria]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Enfermeria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EnfermeriaCorral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EnfermeriaCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaCondicion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaCondicion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanadoCalidad]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanadoCalidad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanadoCosteo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanadoCosteo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanadoCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanadoCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanadoMuerte]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanadoMuerte]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanadoSobrante]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanadoSobrante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaGanadoTransito]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaGanadoTransito]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaPremezcla]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaPremezcla]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaProductoCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaProductoCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaProductoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaProductoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaProductoMuestra]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaProductoMuestra]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EntradaProductoParcial]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EntradaProductoParcial]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EstadoComedero]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EstadoComedero]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EstadoImplante]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EstadoImplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EstandarDescarga]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EstandarDescarga]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Estatus]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Estatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EvaluacionCorral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EvaluacionCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[EvaluacionCorralDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [EvaluacionCorralDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Familia]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Familia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Flete]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Flete]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FleteDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FleteDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FleteInterno]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FleteInterno]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FleteInternoCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FleteInternoCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FleteInternoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FleteInternoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FleteMermaPermitida]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FleteMermaPermitida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Folio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Folio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FolioAlmacen]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FolioAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FolioLoteProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [FolioLoteProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Formula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Formula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Formulario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Formulario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[GastoCentroCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GastoCentroCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[GastoInventario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GastoInventario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[GastoMateriaPrima]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GastoMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Gelatinizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Gelatinizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[GelatinizacionDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GelatinizacionDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Grado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Grado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Grupo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Grupo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[GrupoCorral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GrupoCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[GrupoFormulario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [GrupoFormulario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Indicador]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Indicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[IndicadorObjetivo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [IndicadorObjetivo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[IndicadorProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [IndicadorProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[IndicadorProductoBoleta]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [IndicadorProductoBoleta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[IndicadorProductoCalidad]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [IndicadorProductoCalidad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Ingrediente]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Ingrediente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceOrganizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceOrganizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceSalida]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceSalida]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceSalidaAnimal]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceSalidaAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceSalidaCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceSalidaCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceSalidaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceSalidaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[InterfaceSalidaTraspaso]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [InterfaceSalidaTraspaso]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Iva]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Iva]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Jaula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Jaula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LectorRegistro]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LectorRegistro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LectorRegistroDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LectorRegistroDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Liquidacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Liquidacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LiquidacionDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LiquidacionDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Lote]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Lote]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LoteDistribucionAlimento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LoteDistribucionAlimento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LoteProyeccion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LoteProyeccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LoteReimplante]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LoteReimplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LoteSacrificio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LoteSacrificio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[LoteSacrificioDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [LoteSacrificioDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MermaSuperavit]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MermaSuperavit]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MetodoPago]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MetodoPago]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Mezcladora]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Mezcladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Modulo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Modulo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Moneda]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Moneda]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MonitoreoSilo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MonitoreoSilo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MonitoreoSiloDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MonitoreoSiloDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MonitoreoSiloIndicador]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MonitoreoSiloIndicador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Muertes]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Muertes]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MuestreoFibraFormula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MuestreoFibraFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[MuestreoFibraIngrediente]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [MuestreoFibraIngrediente]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Notificaciones]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Notificaciones]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Observacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Observacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Operador]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Operador]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[OrdenSacrificio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [OrdenSacrificio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[OrdenSacrificioDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [OrdenSacrificioDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Organizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Organizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Padre]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Padre]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Parametro]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Parametro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ParametroGeneral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ParametroGeneral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ParametroOrganizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ParametroOrganizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ParametroTrampa]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ParametroTrampa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Pedido]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Pedido]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PedidoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PedidoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PesajeMateriaPrima]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PesajeMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Poliza]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Poliza]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PrecioGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PrecioGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PrecioPAC]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PrecioPAC]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Pregunta]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Pregunta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Premezcla]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Premezcla]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PremezclaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PremezclaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PremezclaDistribucion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PremezclaDistribucion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[PremezclaDistribucionDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [PremezclaDistribucionDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Problema]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Problema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProblemaSintoma]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProblemaSintoma]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProblemaTratamiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProblemaTratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProduccionDiaria]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProduccionDiaria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProduccionDiariaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProduccionDiariaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProduccionFormula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProduccionFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProduccionFormulaBatch]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProduccionFormulaBatch]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProduccionFormulaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProduccionFormulaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Producto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Producto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProductoCodigoParte]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProductoCodigoParte]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProgramacionCorte]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProgramacionCorte]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProgramacionMateriaPrima]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProgramacionMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProgramacionReimplante]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProgramacionReimplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProgramacionReimplanteDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProgramacionReimplanteDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Proveedor]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Proveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProveedorAlmacen]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProveedorAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ProveedorChofer]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ProveedorChofer]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RecepcionProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RecepcionProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RecepcionProductoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RecepcionProductoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RegistroVigilancia]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RegistroVigilancia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Reparto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Reparto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RepartoAlimento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RepartoAlimento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RepartoAlimentoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RepartoAlimentoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RepartoAvance]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RepartoAvance]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RepartoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RepartoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Retencion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Retencion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[RevisionImplante]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [RevisionImplante]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Rol]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Rol]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Roladora]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Roladora]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Rotomix]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Rotomix]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SalidaAnimal]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SalidaAnimal]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SalidaGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SalidaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SalidaProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SalidaProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[ServicioAlimento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [ServicioAlimento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Silo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Silo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Sintoma]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Sintoma]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SolicitudPremezcla]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SolicitudPremezcla]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SolicitudPremezclaDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SolicitudPremezclaDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SolicitudProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SolicitudProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SolicitudProductoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SolicitudProductoDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SubFamilia]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SubFamilia]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SupervisionDetectores]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SupervisionDetectores]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SupervisionDetectoresDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SupervisionDetectoresDetalle]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SupervisionGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SupervisionGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[SupervisorEnfermeria]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [SupervisorEnfermeria]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TarifaSecado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TarifaSecado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TiempoMuerto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TiempoMuerto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoAlmacen]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoAlmacen]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoAutorizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoAutorizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoCambio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoCambio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoCancelacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoCancelacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoContrato]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoContrato]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoCorral]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoCorral]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoCosto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoCosto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoCuenta]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoCuenta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoDeteccion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoDeteccion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoEmbarque]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoEmbarque]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoEstatus]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoEstatus]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoFlete]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoFlete]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoFolio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoFolio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoFormula]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoFormula]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoGanadoCorrales]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoGanadoCorrales]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoLugarMuestra]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoLugarMuestra]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoMovimiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoMovimiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoMuestra]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoMuestra]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoObjetivoCalidad]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoObjetivoCalidad]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoObservacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoObservacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoOrganizacion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoOrganizacion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoPAC]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoPAC]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoParametro]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoParametro]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoPesaje]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoPesaje]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoPoliza]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoPoliza]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoPregunta]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoPregunta]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoProblema]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoProblema]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoProceso]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoProceso]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoProrrateo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoProrrateo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoProveedor]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoProveedor]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoServicio]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoServicio]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoTarifa]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoTarifa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoTecnica]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoTecnica]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TipoTratamiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TipoTratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Trampa]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Trampa]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TraspasoMateriaPrima]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TraspasoMateriaPrima]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Tratamiento]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Tratamiento]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[TratamientoProducto]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [TratamientoProducto]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[UnidadMedicion]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [UnidadMedicion]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Usuario]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Usuario]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[UsuarioGrupo]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [UsuarioGrupo]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[VentaGanado]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [VentaGanado]
; GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[VentaGanadoDetalle]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [VentaGanadoDetalle]
; GO


CREATE TABLE [Acceso] ( 
	[AccesoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL
)
; GO

CREATE TABLE [AccionesSIAP] ( 
	[AccionesSiapID] int identity(1,1)  NOT NULL,
	[Proceso] varchar(150) NOT NULL,
	[Descripcion] varchar(255) NOT NULL,
	[Codigo] varchar(10) NOT NULL,
	[FechaEjecucion] smalldatetime NOT NULL,
	[Lunes] bit DEFAULT 0,
	[Martes] bit DEFAULT 0,
	[Miercoles] bit DEFAULT 0,
	[Jueves] bit DEFAULT 0,
	[Viernes] bit DEFAULT 0,
	[Sabado] bit DEFAULT 0,
	[Domingo] bit DEFAULT 0,
	[Repetir] bit DEFAULT 0 NOT NULL,
	[FechaUltimaEjecucion] smalldatetime,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Almacen] ( 
	[AlmacenID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CodigoAlmacen] varchar(10) NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoAlmacenID] int NOT NULL,
	[CuentaInventario] varchar(10),
	[CuentaInventarioTransito] varchar(10),
	[CuentaDiferencias] varchar(10),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmacenInventario] ( 
	[AlmacenInventarioID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Minimo] int NOT NULL,
	[Maximo] int NOT NULL,
	[PrecioPromedio] decimal(18,4) NOT NULL,
	[Cantidad] decimal(18,2) NOT NULL,
	[Importe] decimal(24,2) NOT NULL,
	[DiasReorden] int DEFAULT 0 NOT NULL,
	[CapacidadAlmacenaje] int DEFAULT 0 NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmacenInventarioLote] ( 
	[AlmacenInventarioLoteID] int identity(1,1)  NOT NULL,
	[AlmacenInventarioID] int NOT NULL,
	[Lote] int NOT NULL,
	[Cantidad] decimal(18,2) NOT NULL,
	[PrecioPromedio] decimal(18,4) NOT NULL,
	[Piezas] int DEFAULT 0 NOT NULL,
	[Importe] decimal(24,2) NOT NULL,
	[FechaInicio] smalldatetime NOT NULL,
	[FechaFin] smalldatetime,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmacenMovimiento] ( 
	[AlmacenMovimientoID] bigint identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[ProveedorID] int,
	[FolioMovimiento] bigint NOT NULL,
	[Observaciones] varchar(255),
	[FechaMovimiento] smalldatetime NOT NULL,
	[Status] int NOT NULL,
	[AnimalMovimientoID] bigint,
	[PolizaGenerada] bit DEFAULT 0 NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmacenMovimientoCosto] ( 
	[AlmacenMovimientoCostoID] int identity(1,1)  NOT NULL,
	[AlmacenMovimientoID] bigint NOT NULL,
	[TieneCuenta] bit DEFAULT 0 NOT NULL,
	[ProveedorID] int,
	[CuentaSAPID] int,
	[CostoID] int NOT NULL,
	[Cantidad] decimal(18,2) DEFAULT 0 NOT NULL,
	[Importe] decimal(24,2) NOT NULL,
	[Iva] bit DEFAULT 0 NOT NULL,
	[Retencion] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmacenMovimientoDetalle] ( 
	[AlmacenMovimientoDetalleID] bigint identity(1,1)  NOT NULL,
	[AlmacenMovimientoID] bigint NOT NULL,
	[AlmacenInventarioLoteID] int,
	[ContratoID] int,
	[Piezas] int DEFAULT 0 NOT NULL,
	[TratamientoID] int,
	[ProductoID] int NOT NULL,
	[Precio] decimal(18,4) NOT NULL,
	[Cantidad] decimal(18,2) NOT NULL,
	[Importe] decimal(24,2) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmacenUsuario] ( 
	[AlmacenUsuarioID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[UsuarioID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmidonFactor] ( 
	[AlmidonFactorID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Descripcion] varchar(10) NOT NULL,
	[Lectura1] decimal(18,4) NOT NULL,
	[Lectura2] decimal(18,4) NOT NULL,
	[Peso] decimal(18,4) NOT NULL,
	[MateriaSeca] decimal(18,4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmidonHeces] ( 
	[AlmidonHecesID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[OperdorIDCalidad] int NOT NULL,
	[UsuarioIDLaboratorio] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[FechaRecepcion] smalldatetime NOT NULL,
	[FechaMuestra] smalldatetime NOT NULL,
	[FechaAnalisis] smalldatetime,
	[FormulaID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[EstatusID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AlmidonHecesDetalle] ( 
	[AlmidonHecesDetalleID] int identity(1,1)  NOT NULL,
	[AlmidonHecesID] int NOT NULL,
	[LoteID] int NOT NULL,
	[Peso] decimal(18,4) NOT NULL,
	[Lectura1] decimal(18,4) NOT NULL,
	[Lectura2] decimal(18,4) NOT NULL,
	[Observaciones] varchar(255) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnalisisGrasa] ( 
	[AnalisisGrasaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[EntradaProductoID] int NOT NULL,
	[TipoMuestra] varchar(10) NOT NULL,
	[PesoMuestra] decimal(15,2) NOT NULL,
	[PesoTuboSeco] decimal(15,2) NOT NULL,
	[PesoTuboMuestra] decimal(15,2) NOT NULL,
	[Impurezas] decimal(15,2) NOT NULL,
	[Observaciones] varchar(255) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Animal] ( 
	[AnimalID] bigint identity(1,1)  NOT NULL,
	[Arete] varchar(15) NOT NULL,
	[AreteMetalico] varchar(15),
	[FechaCompra] smalldatetime NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[CalidadGanadoID] int NOT NULL,
	[ClasificacionGanadoID] int NOT NULL,
	[PesoCompra] int NOT NULL,
	[OrganizacionIDEntrada] int NOT NULL,
	[FolioEntrada] bigint NOT NULL,
	[PesoLlegada] int NOT NULL,
	[Paletas] int NOT NULL,
	[CausaRechadoID] int,
	[Venta] bit NOT NULL,
	[Cronico] bit NOT NULL,
	[CambioSexo] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalConsumo] ( 
	[AnimalConsumoID] bigint identity(1,1)  NOT NULL,
	[AnimalID] bigint NOT NULL,
	[RepartoID] bigint NOT NULL,
	[FormulaIDServida] int NOT NULL,
	[Cantidad] decimal(12,3) NOT NULL,
	[TipoServicioID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalConsumoHistorico] ( 
	[AnimalConsumoID] bigint NOT NULL,
	[AnimalID] bigint NOT NULL,
	[RepartoID] bigint NOT NULL,
	[FormulaIDServida] int NOT NULL,
	[Cantidad] decimal(12,3) NOT NULL,
	[TipoServicioID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalCosto] ( 
	[AnimalCostoID] bigint identity(1,1)  NOT NULL,
	[AnimalID] bigint NOT NULL,
	[FechaCosto] smalldatetime NOT NULL,
	[CostoID] int NOT NULL,
	[TipoReferencia] int DEFAULT 0 NOT NULL,
	[FolioReferencia] bigint NOT NULL,
	[Importe] decimal(17,2) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalCostoHistorico] ( 
	[AnimalCostoID] bigint NOT NULL,
	[AnimalID] bigint NOT NULL,
	[FechaCosto] smalldatetime NOT NULL,
	[CostoID] int NOT NULL,
	[TipoReferencia] int DEFAULT 0 NOT NULL,
	[FolioReferencia] bigint NOT NULL,
	[Importe] decimal(17,2) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalHistorico] ( 
	[AnimalID] bigint NOT NULL,
	[Arete] varchar(15) NOT NULL,
	[AreteMetalico] varchar(15),
	[FechaCompra] smalldatetime NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[CalidadGanadoID] int NOT NULL,
	[ClasificacionGanadoID] int NOT NULL,
	[PesoCompra] int NOT NULL,
	[OrganizacionIDEntrada] int NOT NULL,
	[FolioEntrada] bigint NOT NULL,
	[PesoLlegada] int NOT NULL,
	[Paletas] int NOT NULL,
	[CausaRechadoID] int,
	[Venta] bit NOT NULL,
	[Cronico] bit NOT NULL,
	[PesoNoqueo] int,
	[PesoCanal] int,
	[PesoPiel] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalMovimiento] ( 
	[AnimalID] bigint NOT NULL,
	[AnimalMovimientoID] bigint identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CorralID] int NOT NULL,
	[LoteID] int NOT NULL,
	[FechaMovimiento] smalldatetime NOT NULL,
	[Peso] int NOT NULL,
	[Temperatura] decimal(5,1) NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[TrampaID] int NOT NULL,
	[OperadorID] int NOT NULL,
	[Observaciones] varchar(255),
	[LoteIDOrigen] int,
	[AnimalMovimientoIDAnterior] bigint,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalMovimientoHistorico] ( 
	[AnimalID] bigint NOT NULL,
	[AnimalMovimientoID] bigint NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CorralID] int NOT NULL,
	[LoteID] int NOT NULL,
	[FechaMovimiento] smalldatetime NOT NULL,
	[Peso] int NOT NULL,
	[Temperatura] decimal(5,1) NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[TrampaID] int NOT NULL,
	[OperadorID] int NOT NULL,
	[Observaciones] varchar(255),
	[LoteIDOrigen] int,
	[AnimalMovimientoIDAnterior] bigint,
	[Activo] bit NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AnimalSalida] ( 
	[AnimalSalidaID] int identity(1,1)  NOT NULL,
	[AnimalID] bigint NOT NULL,
	[LoteID] int NOT NULL,
	[CorraletaID] int NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[FechaSalida] smalldatetime NOT NULL,
	[OrdenSacrificioID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AreaRevision] ( 
	[AreaRevisionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [AutorizacionMateriaPrima] ( 
	[AutorizacionMateriaPrimaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoAutorizacionID] int NOT NULL,
	[Folio] bigint NOT NULL,
	[Justificacion] varchar(250) NOT NULL,
	[Observaciones] varchar(255),
	[Lote] int NOT NULL,
	[Precio] decimal(18,4) NOT NULL,
	[Cantidad] decimal(18,2) NOT NULL,
	[ProductoID] int NOT NULL,
	[EstatusID] int NOT NULL,
	[AlmacenID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [BitacoraErrores] ( 
	[BitacoraErroresID] int identity(1,1)  NOT NULL,
	[AccionesSiapID] int NOT NULL,
	[Mensaje] varchar(500) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL
)
; GO

CREATE TABLE [Calculo] ( 
	[CalculoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CalidadGanado] ( 
	[CalidadGanadoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Calidad] varchar(10) NOT NULL,
	[Sexo] char(1) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CalidadMateriaPrima] ( 
	[CalidadMateriaPrimaID] int identity(1,1)  NOT NULL,
	[PedidoDetalleID] int,
	[Movimiento] int NOT NULL,
	[IndicadorObjetivoID] int NOT NULL,
	[Resultado] decimal(10,2) NOT NULL,
	[ColorObjetivoID] int NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CalidadMezclado] ( 
	[CalidadMezcladoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoTecnicaID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[UsuarioIDLaboratorio] int NOT NULL,
	[FormulaID] int NOT NULL,
	[FechaPremezcla] smalldatetime NOT NULL,
	[FechaBatch] smalldatetime NOT NULL,
	[TipoLugarMuestraID] int NOT NULL,
	[CamionRepartoID] int,
	[ChoferID] int,
	[MezcladoraID] int,
	[OperadorIDMezclador] int,
	[LoteID] int,
	[Batch] int NOT NULL,
	[TiempoMezclado] int NOT NULL,
	[OperadorIDAnalista] int NOT NULL,
	[GramosMicrotoxina] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CalidadMezcladoDetalle] ( 
	[CalidadMezcladoDetalleID] int identity(1,1)  NOT NULL,
	[CalidadMezcladoID] int NOT NULL,
	[TipoMuestraID] int NOT NULL,
	[NumeroMuestra] varchar(2) NOT NULL,
	[Peso] int NOT NULL,
	[Particulas] decimal(10,3) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CalidadMezcladoFactor] ( 
	[CalidadMezcladoFactorID] int identity(1,1)  NOT NULL,
	[TipoMuestraID] int NOT NULL,
	[PesoBaseHumeda] int NOT NULL,
	[PesoBaseSeca] int NOT NULL,
	[Factor] decimal(14,4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Camion] ( 
	[CamionID] int identity(1,1)  NOT NULL,
	[ProveedorID] int NOT NULL,
	[PlacaCamion] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CamionReparto] ( 
	[CamionRepartoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CentroCostoID] int NOT NULL,
	[NumeroEconomico] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CancelacionMovimiento] ( 
	[CancelacionMovimientoID] int identity(1,1)  NOT NULL,
	[TipoCancelacionID] int NOT NULL,
	[PedidoID] int,
	[Ticket] int,
	[AlmacenMovimientoIDOrigen] bigint,
	[AlmacenMovimientoIDCancelado] bigint,
	[FechaCancelacion] smalldatetime NOT NULL,
	[Justificacion] varchar(255) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CausaPrecio] ( 
	[CausaPrecioID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CausaSalidaID] int NOT NULL,
	[Precio] decimal(10,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CausaRechazo] ( 
	[CausaRechazoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CausaSalida] ( 
	[CausaSalidaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CausaTiempoMuerto] ( 
	[CausaTiempoMuertoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(255) NOT NULL,
	[TipoCausa] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CentroBeneficio] ( 
	[CentroBeneficioID] int identity(1,1)  NOT NULL,
	[CentroBeneficioSAP] char(6),
	[Descripcion] varchar(100) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CentroCosto] ( 
	[CentroCostoID] int identity(1,1)  NOT NULL,
	[CentroCostoSAP] char(6) NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[AreaDepartamento] char(6),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CentroCostoUsuario] ( 
	[CentroCostoUsuarioID] int identity(1,1)  NOT NULL,
	[UsuarioID] int NOT NULL,
	[CentroCostoID] int NOT NULL,
	[Autoriza] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListCorral] ( 
	[CheckListCorralID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[LoteID] int NOT NULL,
	[PDF] varbinary(max) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListRoladora] ( 
	[CheckListRoladoraID] int identity(1,1)  NOT NULL,
	[CheckListRoladoraGeneralID] int NOT NULL,
	[RoladoraID] int NOT NULL,
	[UsuarioIDResponsable] int NOT NULL,
	[FechaCheckList] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListRoladoraAccion] ( 
	[CheckListRoladoraAccionID] int identity(1,1)  NOT NULL,
	[CheckListRoladoraRangoID] int NOT NULL,
	[Descripcion] varchar(255) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListRoladoraDetalle] ( 
	[CheckListRoladoraDetalleID] int identity(1,1)  NOT NULL,
	[CheckListRoladoraID] int NOT NULL,
	[CheckListRoladoraRangoID] int NOT NULL,
	[CheckListRoladoraAccionID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListRoladoraGeneral] ( 
	[CheckListRoladoraGeneralID] int identity(1,1)  NOT NULL,
	[Turno] int NOT NULL,
	[FechaInicio] datetime NOT NULL,
	[UsuarioIDSupervisor] int,
	[Observaciones] varchar(255),
	[SurfactanteInicio] decimal(14,2),
	[SurfactanteFin] decimal(14,2),
	[ContadorAguaInicio] decimal(14,2),
	[ContadorAguaFin] decimal(14,2),
	[GranoEnteroFinal] decimal(14,2),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListRoladoraHorometro] ( 
	[CheckListRoladoraHorometroID] int identity(1,1)  NOT NULL,
	[CheckListRoladoraGeneralID] int NOT NULL,
	[RoladoraID] int NOT NULL,
	[HorometroInicial] varchar(5) NOT NULL,
	[HorometroFinal] varchar(5),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CheckListRoladoraRango] ( 
	[CheckListRoladoraRangoID] int identity(1,1)  NOT NULL,
	[PreguntaID] int NOT NULL,
	[Descripcion] varchar(255) NOT NULL,
	[CodigoColor] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Chofer] ( 
	[ChoferID] int identity(1,1)  NOT NULL,
	[Nombre] varchar(50) NOT NULL,
	[ApellidoPaterno] varchar(50) NOT NULL,
	[ApellidoMaterno] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ClaseCostoProducto] ( 
	[ClaseCostoProductoID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[CuentaSAPID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ClasificacionGanado] ( 
	[ClasificacionGanadoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Cliente] ( 
	[ClienteID] int identity(1,1)  NOT NULL,
	[CodigoSAP] varchar(10) NOT NULL,
	[Descripcion] varchar(150) NOT NULL,
	[Poblacion] varchar(50),
	[Estado] varchar(50),
	[Pais] varchar(50),
	[Calle] varchar(255),
	[CodigoPostal] varchar(10),
	[RFC] varchar(13),
	[MetodoPagoID] int,
	[CondicionPago] int NOT NULL,
	[DiasPago] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ColorObjetivo] ( 
	[ColorObjetivoID] int identity(1,1)  NOT NULL,
	[TipoObjetivoCalidadID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Tendencia] varchar(10) NOT NULL,
	[CodigoColor] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ConceptoDeteccion] ( 
	[ConceptoDeteccionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Condicion] ( 
	[CondicionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CondicionJaula] ( 
	[CondicionJaulaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(255) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ConfiguracionEmbarque] ( 
	[ConfiguracionEmbarqueID] int identity(1,1)  NOT NULL,
	[OrganizacionOrigenID] int NOT NULL,
	[OrganizacionDestinoID] int NOT NULL,
	[Kilometros] decimal(10,2) NOT NULL,
	[Horas] decimal(10,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ConfiguracionFormula] ( 
	[ConfiguracionFormulaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FormulaID] int NOT NULL,
	[PesoInicioMinimo] int NOT NULL,
	[PesoInicioMaximo] int NOT NULL,
	[TipoGanado] varchar(100) NOT NULL,
	[PesoSalida] int NOT NULL,
	[FormulaSiguienteID] int NOT NULL,
	[DiasEstanciaMinimo] int NOT NULL,
	[DiasEstanciaMaximo] int NOT NULL,
	[DiasTransicionMinimo] int NOT NULL,
	[DiasTransicionMaximo] int NOT NULL,
	[Disponibilidad] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ConfiguracionImplante] ( 
	[ConfiguracionImplanteID] int identity(1,1)  NOT NULL,
	[DiasMin] int NOT NULL,
	[DiasMax] int NOT NULL,
	[DiasImplante1] int NOT NULL,
	[DiasImplante2] int NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ConfiguracionSemana] ( 
	[ConfiguracionSemanaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[InicioSemana] varchar(50) NOT NULL,
	[FinSemana] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ConfiguracionTraspasoAlmacen] ( 
	[ConfiguracionTraspasoAlmacenID] int identity(1,1)  NOT NULL,
	[TipoAlmacenOrigenID] int NOT NULL,
	[TipoAlmacenDestinoID] int NOT NULL,
	[DiferenteOrganizacion] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Constantes] ( 
	[CalculoID] int NOT NULL,
	[ConstanteID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Codigo] int NOT NULL,
	[Sexo] char(1) NOT NULL,
	[Valor] varchar(20) NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Contrato] ( 
	[ContratoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Folio] int NOT NULL,
	[ProductoID] int NOT NULL,
	[TipoContratoID] int NOT NULL,
	[TipoFleteID] int,
	[ProveedorID] int NOT NULL,
	[Precio] decimal(18,4) NOT NULL,
	[TipoCambioID] int,
	[Cantidad] int NOT NULL,
	[Merma] decimal(14,2) NOT NULL,
	[PesoNegociar] varchar(10) NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[FechaVigencia] smalldatetime NOT NULL,
	[Tolerancia] decimal(10,2) NOT NULL,
	[Parcial] bit NOT NULL,
	[CuentaSAPID] int,
	[EstatusID] int NOT NULL,
	[FolioAserca] varchar(15),
	[FolioCobertura] int,
	[CalidadOrigen] bit NOT NULL,
	[CostoSecado] bit NOT NULL,
	[AplicaDescuento] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ContratoDetalle] ( 
	[ContratoDetalleID] int identity(1,1)  NOT NULL,
	[ContratoID] int NOT NULL,
	[IndicadorID] int NOT NULL,
	[PorcentajePermitido] decimal(10,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ContratoHumedad] ( 
	[ContratoHumedadID] int identity(1,1)  NOT NULL,
	[ContratoID] int NOT NULL,
	[FechaInicio] smalldatetime NOT NULL,
	[PorcentajeHumedad] decimal(10,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ContratoParcial] ( 
	[ContratoParcialID] int identity(1,1)  NOT NULL,
	[ContratoID] int NOT NULL,
	[Cantidad] int NOT NULL,
	[Importe] decimal(18,4) NOT NULL,
	[TipoCambioID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Corral] ( 
	[CorralID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Codigo] varchar(10) NOT NULL,
	[TipoCorralID] int NOT NULL,
	[Capacidad] int NOT NULL,
	[MetrosLargo] int NOT NULL,
	[MetrosAncho] bigint NOT NULL,
	[Seccion] int NOT NULL,
	[Orden] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CorralDetector] ( 
	[CorralDetectorID] int identity(1,1)  NOT NULL,
	[OperadorID] int NOT NULL,
	[CorralID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CorralLector] ( 
	[CorralLectorID] int identity(1,1)  NOT NULL,
	[OperadorID] int NOT NULL,
	[CorralID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CorralRango] ( 
	[OrganizacionID] int NOT NULL,
	[CorralID] int NOT NULL,
	[Sexo] char(1) NOT NULL,
	[RangoInicial] int NOT NULL,
	[RangoFinal] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Costo] ( 
	[CostoID] int identity(1,1)  NOT NULL,
	[ClaveContable] char(3) NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoCostoID] int NOT NULL,
	[TipoProrrateoID] int NOT NULL,
	[RetencionID] int,
	[AbonoA] char(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CostoEmbarqueDetalle] ( 
	[CostoEmbarqueDetalleID] int identity(1,1)  NOT NULL,
	[EmbarqueDetalleID] int NOT NULL,
	[CostoID] int NOT NULL,
	[Orden] int NOT NULL,
	[Importe] decimal(18,4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CostoOrganizacion] ( 
	[CostoOrganizacionID] int identity(1,1)  NOT NULL,
	[TipoOrganizacionID] int NOT NULL,
	[CostoID] int NOT NULL,
	[Automatico] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CostoPromedio] ( 
	[CostoPromedioID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CostoID] int NOT NULL,
	[Importe] decimal(19,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CriterioSupervision] ( 
	[CriterioSupervisionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[ValorInicial] int NOT NULL,
	[ValorFinal] int NOT NULL,
	[CodigoColor] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Cuenta] ( 
	[CuentaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoCuentaID] int NOT NULL,
	[ClaveCuenta] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CuentaAlmacenSubFamilia] ( 
	[CuentaAlmacenSubFamiliaID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[SubFamiliaID] int NOT NULL,
	[CuentaSAPID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CuentaSAP] ( 
	[CuentaSAPID] int identity(1,1)  NOT NULL,
	[CuentaSAP] varchar(10) NOT NULL,
	[Descripcion] varchar(60) NOT NULL,
	[TipoCuentaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [CuentaValor] ( 
	[CuentaValorID] int identity(1,1)  NOT NULL,
	[CuentaID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Valor] varchar(20) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [DescripcionGanado] ( 
	[DescripcionGanadoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(255) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Deteccion] ( 
	[DeteccionID] int identity(1,1)  NOT NULL,
	[Arete] varchar(15),
	[AreteMetalico] varchar(15),
	[FotoDeteccion] varchar(250),
	[LoteID] int NOT NULL,
	[OperadorID] int NOT NULL,
	[TipoDeteccionID] int NOT NULL,
	[GradoID] int NOT NULL,
	[Observaciones] varchar(255),
	[DescripcionGanadoID] int NOT NULL,
	[NoFierro] varchar(10),
	[FechaDeteccion] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [DeteccionAnimal] ( 
	[DeteccionAnimalID] int identity(1,1)  NOT NULL,
	[AnimalMovimientoID] bigint NOT NULL,
	[Arete] varchar(15),
	[AreteMetalico] varchar(15),
	[FotoDeteccion] varchar(250),
	[LoteID] int NOT NULL,
	[OperadorID] int NOT NULL,
	[TipoDeteccionID] int NOT NULL,
	[GradoID] int NOT NULL,
	[Observaciones] varchar(255),
	[DescripcionGanadoID] int NOT NULL,
	[NoFierro] varchar(10),
	[FechaDeteccion] smalldatetime NOT NULL,
	[DeteccionAnalista] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [DeteccionSintoma] ( 
	[DeteccionSintomaID] int identity(1,1)  NOT NULL,
	[DeteccionID] int NOT NULL,
	[SintomaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [DeteccionSintomaAnimal] ( 
	[DeteccionAnimalSintomaID] int identity(1,1)  NOT NULL,
	[DeteccionAnimalID] int NOT NULL,
	[SintomaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [DiagnosticoAnalista] ( 
	[DiagnosticoAnalistaID] int identity(1,1)  NOT NULL,
	[DeteccionAnimalID] int NOT NULL,
	[GradoID] int NOT NULL,
	[Justificacion] varchar(150),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [DiagnosticoAnalistaDetalle] ( 
	[DiagnosticoAnalistaDetalleID] int identity(1,1)  NOT NULL,
	[DiagnosticoAnalistaID] int NOT NULL,
	[ProblemaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Embarque] ( 
	[EmbarqueID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FolioEmbarque] int NOT NULL,
	[TipoEmbarqueID] int NOT NULL,
	[Estatus] int NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EmbarqueDetalle] ( 
	[EmbarqueDetalleID] int identity(1,1)  NOT NULL,
	[EmbarqueID] int NOT NULL,
	[ProveedorID] int NOT NULL,
	[ChoferID] int NOT NULL,
	[JaulaID] int NOT NULL,
	[CamionID] int NOT NULL,
	[OrganizacionOrigenID] int NOT NULL,
	[OrganizacionDestinoID] int NOT NULL,
	[FechaSalida] smalldatetime NOT NULL,
	[FechaLlegada] smalldatetime NOT NULL,
	[Orden] int NOT NULL,
	[Kilometros] decimal(10,2) NOT NULL,
	[Horas] decimal(6,2) NOT NULL,
	[Recibido] bit NOT NULL,
	[Comentarios] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Enfermeria] ( 
	[EnfermeriaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EnfermeriaCorral] ( 
	[EnfermeriaCorralID] int identity(1,1)  NOT NULL,
	[EnfermeriaID] int NOT NULL,
	[CorralID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaCondicion] ( 
	[EntradaCondicionID] int identity(1,1)  NOT NULL,
	[EntradaGanadoID] int NOT NULL,
	[CondicionID] int NOT NULL,
	[Cabezas] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaDetalle] ( 
	[EntradaDetalleID] int identity(1,1)  NOT NULL,
	[EntradaGanadoCosteoID] int NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[Cabezas] int NOT NULL,
	[PesoOrigen] decimal(18,2) NOT NULL,
	[PesoLlegada] decimal(18,2) NOT NULL,
	[PrecioKilo] decimal(18,2) NOT NULL,
	[Importe] decimal(18,2) NOT NULL,
	[ImporteOrigen] decimal(18,2) DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaGanado] ( 
	[EntradaGanadoID] int identity(1,1)  NOT NULL,
	[FolioEntrada] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[OrganizacionOrigenID] int NOT NULL,
	[FechaEntrada] smalldatetime NOT NULL,
	[EmbarqueID] int NOT NULL,
	[FolioOrigen] int NOT NULL,
	[FechaSalida] smalldatetime NOT NULL,
	[CamionID] int NOT NULL,
	[ChoferID] int NOT NULL,
	[JaulaID] int NOT NULL,
	[CabezasOrigen] int NOT NULL,
	[CabezasRecibidas] int NOT NULL,
	[OperadorID] int NOT NULL,
	[PesoBruto] decimal(18,2) NOT NULL,
	[PesoTara] decimal(18,2) NOT NULL,
	[EsRuteo] bit NOT NULL,
	[Fleje] bit NOT NULL,
	[CheckList] char(10) NOT NULL,
	[CorralID] int,
	[LoteID] int,
	[Observacion] varchar(400) NOT NULL,
	[ImpresionTicket] bit NOT NULL,
	[Costeado] bit NOT NULL,
	[Manejado] bit NOT NULL,
	[Guia] bit NOT NULL,
	[Factura] bit NOT NULL,
	[Poliza] bit NOT NULL,
	[HojaEmbarque] bit NOT NULL,
	[ManejoSinEstres] bit NOT NULL,
	[CabezasMuertas] int NOT NULL,
	[CertificadoZoosanitario] varchar(15),
	[PruebaTR] varchar(15),
	[PruebaTB] varchar(15),
	[CondicionJaulaID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaGanadoCalidad] ( 
	[EntradaGanadoCalidadID] int identity(1,1)  NOT NULL,
	[EntradaGanadoID] int NOT NULL,
	[CalidadGanadoID] int DEFAULT 0 NOT NULL,
	[Valor] int DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaGanadoCosteo] ( 
	[EntradaGanadoCosteoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[EntradaGanadoID] int NOT NULL,
	[Observacion] varchar(255),
	[Prorrateado] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacion] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacion] int
)
; GO

CREATE TABLE [EntradaGanadoCosto] ( 
	[EntradaGanadoCostoID] int identity(1,1)  NOT NULL,
	[EntradaGanadoCosteoID] int NOT NULL,
	[CostoID] int NOT NULL,
	[TieneCuenta] bit NOT NULL,
	[ProveedorID] int,
	[CuentaProvision] char(10),
	[NumeroDocumento] varchar(20) DEFAULT 0 NOT NULL,
	[Importe] decimal(10,2) NOT NULL,
	[Iva] bit NOT NULL,
	[Retencion] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacion] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacion] int
)
; GO

CREATE TABLE [EntradaGanadoMuerte] ( 
	[EntradaGanadoMuerteID] int identity(1,1)  NOT NULL,
	[EntradaGanadoID] int NOT NULL,
	[Arete] varchar(15) NOT NULL,
	[FolioMuerte] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioCreacionID] int NOT NULL,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaGanadoSobrante] ( 
	[EntradaGanadoSobranteID] int identity(1,1)  NOT NULL,
	[EntradaGanadoID] int NOT NULL,
	[AnimalID] bigint,
	[Importe] decimal(18,2) NOT NULL,
	[Costeado] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaGanadoTransito] ( 
	[EntradaGanadoTransitoID] int identity(1,1)  NOT NULL,
	[LoteID] int NOT NULL,
	[Cabezas] int NOT NULL,
	[Importe] decimal(18,2) NOT NULL,
	[Peso] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaPremezcla] ( 
	[EntradaPremezclaID] bigint identity(1,1)  NOT NULL,
	[AlmacenMovimientoIDEntrada] bigint NOT NULL,
	[AlmacenMovimientoIDSalida] bigint NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaProducto] ( 
	[EntradaProductoID] int identity(1,1)  NOT NULL,
	[ContratoID] int,
	[OrganizacionID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[RegistroVigilanciaID] int NOT NULL,
	[Folio] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[FechaDestara] smalldatetime,
	[Observaciones] varchar(255),
	[OperadorIDAnalista] int,
	[PesoBonificacion] int NOT NULL,
	[PesoOrigen] int DEFAULT 0 NOT NULL,
	[PesoBruto] int DEFAULT 0 NOT NULL,
	[PesoTara] int DEFAULT 0 NOT NULL,
	[PesoDescuento] int DEFAULT 0 NOT NULL,
	[Piezas] int DEFAULT 0 NOT NULL,
	[TipoContratoID] int,
	[EstatusID] int NOT NULL,
	[Justificacion] varchar(255),
	[OperadorIDBascula] int,
	[OperadorIDAlmacen] int,
	[OperadorIDAutoriza] int,
	[FechaInicioDescarga] smalldatetime,
	[FechaFinDescarga] smalldatetime,
	[AlmacenInventarioLoteID] int,
	[AlmacenMovimientoID] bigint,
	[FechaEmbarque] smalldatetime,
	[AlmacenMovimientoSalidaID] bigint,
	[FolioOrigen] int,
	[Revisado] bit DEFAULT 0 NOT NULL,
	[FechaPesaje] smalldatetime,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaProductoCosto] ( 
	[EntradaProductoCostoID] int identity(1,1)  NOT NULL,
	[EntradaProductoID] int NOT NULL,
	[CostoID] int NOT NULL,
	[TieneCuenta] bit NOT NULL,
	[ProveedorID] int,
	[CuentaProvision] varchar(10),
	[Importe] decimal(10,2) NOT NULL,
	[Iva] bit NOT NULL,
	[Retencion] bit NOT NULL,
	[TipoEntrada] varchar(10) NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaProductoDetalle] ( 
	[EntradaProductoDetalleID] int identity(1,1)  NOT NULL,
	[EntradaProductoID] int NOT NULL,
	[IndicadorID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaProductoMuestra] ( 
	[EntradaProductoMuestraID] int identity(1,1)  NOT NULL,
	[EntradaProductoDetalleID] int NOT NULL,
	[Porcentaje] decimal(10,2) DEFAULT 0 NOT NULL,
	[Descuento] decimal(10,2) DEFAULT 0 NOT NULL,
	[Rechazo] bit DEFAULT 0 NOT NULL,
	[EsOrigen] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EntradaProductoParcial] ( 
	[EntradaProductoParcialID] int identity(1,1)  NOT NULL,
	[EntradaProductoID] int NOT NULL,
	[ContratoParcialID] int NOT NULL,
	[CantidadEntrante] decimal(10,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EstadoComedero] ( 
	[EstadoComederoID] int identity(0,1)  NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[DescripcionCorta] varchar(50) NOT NULL,
	[NoServir] bit NOT NULL,
	[AjusteBase] decimal(10,2) NOT NULL,
	[Tendencia] char(1) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EstadoImplante] ( 
	[EstadoImplanteID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Correcto] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EstandarDescarga] ( 
	[EstandarDescargaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Tiempo] char(5) NOT NULL,
	[Activo] bit NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Estatus] ( 
	[EstatusID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoEstatus] int NOT NULL,
	[DescripcionCorta] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EvaluacionCorral] ( 
	[EvaluacionID] int identity(1,1)  NOT NULL,
	[FolioEvaluacion] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[LoteID] int NOT NULL,
	[FechaEvaluacion] smalldatetime NOT NULL,
	[Cabezas] int NOT NULL,
	[EsMetafilaxia] bit NOT NULL,
	[OperadorID] int NOT NULL,
	[NivelGarrapata] int DEFAULT 0 NOT NULL,
	[MetafilaxiaAutorizada] bit NOT NULL,
	[Justificacion] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [EvaluacionCorralDetalle] ( 
	[EvaluacionCorralDetalleID] int identity(1,1)  NOT NULL,
	[EvaluacionCorralID] int NOT NULL,
	[PreguntaID] int NOT NULL,
	[Respuesta] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacion] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacion] int
)
; GO

CREATE TABLE [Familia] ( 
	[FamiliaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Flete] ( 
	[FleteID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ContratoID] int NOT NULL,
	[ProveedorID] int NOT NULL,
	[MermaPermitida] decimal(10,3) NOT NULL,
	[TipoTarifaID] int NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [FleteDetalle] ( 
	[FleteDetalleID] int identity(1,1)  NOT NULL,
	[FleteID] int NOT NULL,
	[CostoID] int NOT NULL,
	[Tarifa] decimal(17,4) NOT NULL,
	[EsTotal] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [FleteInterno] ( 
	[FleteInternoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[AlmacenIDOrigen] int NOT NULL,
	[AlmacenIDDestino] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [FleteInternoCosto] ( 
	[FleteInternoCostoID] int identity(1,1)  NOT NULL,
	[FleteInternoDetalleID] int NOT NULL,
	[CostoID] int NOT NULL,
	[Tarifa] decimal(18,4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [FleteInternoDetalle] ( 
	[FleteInternoDetalleID] int identity(1,1)  NOT NULL,
	[FleteInternoID] int NOT NULL,
	[ProveedorID] int NOT NULL,
	[MermaPermitida] decimal(12,2) NOT NULL,
	[Observaciones] varchar(255),
	[TipoTarifaID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [FleteMermaPermitida] ( 
	[FleteMermaPermitidaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[SubFamiliaID] int NOT NULL,
	[MermaPermitida] decimal(10,3) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Folio] ( 
	[FolioID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoFolioID] tinyint NOT NULL,
	[Valor] bigint NOT NULL
)
; GO

CREATE TABLE [FolioAlmacen] ( 
	[FolioAlmacenID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[Valor] int NOT NULL
)
; GO

CREATE TABLE [FolioLoteProducto] ( 
	[FolioLoteProductoID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Valor] int NOT NULL
)
; GO

CREATE TABLE [Formula] ( 
	[FormulaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoFormulaID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Formulario] ( 
	[FormularioID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[WinForm] varchar(100),
	[Activo] bit NOT NULL,
	[Orden] int,
	[PadreID] int,
	[ModuloID] int,
	[Imagen] varchar(100),
	[Web] bit NOT NULL
)
; GO

CREATE TABLE [GastoCentroCosto] ( 
	[GastoCentroCostoID] int identity(1,1)  NOT NULL,
	[CostoID] int NOT NULL,
	[CentroCostoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [GastoInventario] ( 
	[GastoInventarioID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoGasto] varchar(10) NOT NULL,
	[FolioGasto] bigint NOT NULL,
	[FechaGasto] smalldatetime NOT NULL,
	[CostoID] int NOT NULL,
	[TieneCuenta] bit DEFAULT 0 NOT NULL,
	[CuentaSAPID] int,
	[ProveedorID] int,
	[Factura] varchar(50),
	[Importe] decimal(18,2) NOT NULL,
	[IVA] bit DEFAULT 0 NOT NULL,
	[Observaciones] varchar(255),
	[Retencion] bit DEFAULT 0 NOT NULL,
	[CuentaGasto] varchar(50),
	[CentroCosto] varchar(50),
	[TotalCorrales] int,
	[CorralID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [GastoMateriaPrima] ( 
	[GastoMateriaPrimaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FolioGasto] bigint NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[ProductoID] int NOT NULL,
	[TieneCuenta] bit DEFAULT 0 NOT NULL,
	[CuentaSAPID] int,
	[ProveedorID] int,
	[AlmacenMovimientoID] bigint NOT NULL,
	[AlmacenInventarioLoteID] int,
	[Importe] decimal(18,2) NOT NULL,
	[IVA] bit DEFAULT 0 NOT NULL,
	[Observaciones] varchar(255),
	[Cantidad] decimal(18,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Gelatinizacion] ( 
	[GelatinizacionID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FechaMuestra] smalldatetime NOT NULL,
	[FechaAnalisis] smalldatetime NOT NULL,
	[ProductoID] int NOT NULL,
	[Dextrosa1A] decimal(15,4) NOT NULL,
	[Dextrosa1B] decimal(15,4) NOT NULL,
	[Dextrosa2A] decimal(15,4) NOT NULL,
	[Dextrosa2B] decimal(15,4) NOT NULL,
	[PesoDextrosa1] decimal(15,4) NOT NULL,
	[PesoDextrosa2] decimal(15,4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [GelatinizacionDetalle] ( 
	[GelatinizacionDetalleID] int identity(1,1)  NOT NULL,
	[GelatinizacionID] int NOT NULL,
	[RoladoraID] int NOT NULL,
	[PesoMuestra] decimal(15,4) NOT NULL,
	[Lectura1] decimal(15,4) NOT NULL,
	[Lectura2] decimal(15,4) NOT NULL,
	[Humedad] decimal(15,2) NOT NULL,
	[Densidad] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Grado] ( 
	[GradoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[NivelGravedad] char(1) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Grupo] ( 
	[GrupoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL
)
; GO

CREATE TABLE [GrupoCorral] ( 
	[GrupoCorralID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime DEFAULT GETDATE() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [GrupoFormulario] ( 
	[GrupoID] int NOT NULL,
	[FormularioID] int NOT NULL,
	[AccesoID] int NOT NULL
)
; GO

CREATE TABLE [Indicador] ( 
	[IndicadorID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [IndicadorObjetivo] ( 
	[IndicadorObjetivoID] int identity(1,1)  NOT NULL,
	[IndicadorProductoCalidadID] int NOT NULL,
	[TipoObjetivoCalidadID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ObjetivoMinimo] decimal(10,2) DEFAULT 0 NOT NULL,
	[ObjetivoMaximo] decimal(10,2) DEFAULT 0 NOT NULL,
	[Tolerancia] decimal(10,2) DEFAULT 0 NOT NULL,
	[Medicion] varchar(10),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [IndicadorProducto] ( 
	[IndicadorProductoID] int identity(1,1)  NOT NULL,
	[IndicadorID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Minimo] decimal(10,3) DEFAULT 0 NOT NULL,
	[Maximo] decimal(10,3) DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [IndicadorProductoBoleta] ( 
	[IndicadorProductoBoletaID] int identity(1,1)  NOT NULL,
	[IndicadorProductoID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[RangoMinimo] decimal(10,3) DEFAULT 0 NOT NULL,
	[RangoMaximo] decimal(10,3) DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [IndicadorProductoCalidad] ( 
	[IndicadorProductoCalidadID] int identity(1,1)  NOT NULL,
	[IndicadorID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Ingrediente] ( 
	[IngredienteID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FormulaID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[PorcentajeProgramado] decimal(10,3) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [InterfaceCosto] ( 
	[CostoID] int NOT NULL,
	[CodigoRelacion] int NOT NULL,
	[TipoSistema] char(1) NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [InterfaceOrganizacion] ( 
	[OrganizacionID] int NOT NULL,
	[CodigoRelacion] int NOT NULL,
	[TipoSistema] char(1) NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [InterfaceSalida] ( 
	[OrganizacionID] int NOT NULL,
	[SalidaID] int NOT NULL,
	[OrganizacionDestinoID] int NOT NULL,
	[FechaSalida] smalldatetime NOT NULL,
	[EsRuteo] bit NOT NULL,
	[Cabezas] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaRegistro] datetime NOT NULL,
	[UsuarioRegistro] varchar(50) NOT NULL
)
; GO

CREATE TABLE [InterfaceSalidaAnimal] ( 
	[OrganizacionID] int NOT NULL,
	[SalidaID] int NOT NULL,
	[Arete] varchar(15) NOT NULL,
	[FechaCompra] smalldatetime NOT NULL,
	[PesoCompra] decimal(10,2) NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[PesoOrigen] decimal(10,2) NOT NULL,
	[FechaRegistro] smalldatetime NOT NULL,
	[UsuarioRegistro] varchar(50) NOT NULL
)
; GO

CREATE TABLE [InterfaceSalidaCosto] ( 
	[OrganizacionID] int NOT NULL,
	[SalidaID] int NOT NULL,
	[Arete] varchar(15) NOT NULL,
	[FechaCompra] smalldatetime NOT NULL,
	[CostoID] int NOT NULL,
	[Importe] money NOT NULL,
	[FechaRegistro] smalldatetime NOT NULL,
	[UsuarioRegistro] varchar(50) NOT NULL
)
; GO

CREATE TABLE [InterfaceSalidaDetalle] ( 
	[OrganizacionID] int NOT NULL,
	[SalidaID] int NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[Cabezas] int NOT NULL,
	[PrecioKg] money NOT NULL,
	[Importe] money NOT NULL,
	[FechaRegistro] smalldatetime NOT NULL,
	[UsuarioRegistro] varchar(50) NOT NULL
)
; GO

CREATE TABLE [InterfaceSalidaTraspaso] ( 
	[InterfaceSalidaTraspasoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[OrganizacionIDDestino] int NOT NULL,
	[FolioTraspaso] int NOT NULL,
	[Corral] varchar(10) NOT NULL,
	[Lote] varchar(10) NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[PesoProyectado] int NOT NULL,
	[GananciaDiaria] decimal(6,2) NOT NULL,
	[DiasEngorda] int NOT NULL,
	[FormulaID] int NOT NULL,
	[DiasFormula] int NOT NULL,
	[Cabezas] int NOT NULL,
	[FechaEnvio] smalldatetime NOT NULL,
	[CabezasEnvio] int DEFAULT 0 NOT NULL,
	[SacrificioGanado] bit DEFAULT 0 NOT NULL,
	[TraspasoGanado] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Iva] ( 
	[IvaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TasaIva] decimal(5,2) NOT NULL,
	[IndicadorIvaPagar] char(2) NOT NULL,
	[CuentaPagar] char(10) NOT NULL,
	[IndicadorIvaRecuperar] char(2) NOT NULL,
	[CuentaRecuperar] char(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Jaula] ( 
	[JaulaID] int identity(1,1)  NOT NULL,
	[ProveedorID] int NOT NULL,
	[PlacaJaula] varchar(10),
	[Capacidad] int NOT NULL,
	[Secciones] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LectorRegistro] ( 
	[LectorRegistroID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Seccion] int NOT NULL,
	[LoteID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[CambioFormula] bit NOT NULL,
	[Cabezas] int NOT NULL,
	[EstadoComederoID] int NOT NULL,
	[CantidadOriginal] decimal(16,4) NOT NULL,
	[CantidadPedido] decimal(16,4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LectorRegistroDetalle] ( 
	[LectorRegistroDetalleID] int identity(1,1)  NOT NULL,
	[LectorRegistroID] int,
	[TipoServicioID] int NOT NULL,
	[FormulaIDAnterior] int NOT NULL,
	[FormulaIDProgramada] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Liquidacion] ( 
	[LiquidacionID] int identity(1,1)  NOT NULL,
	[ContratoID] int NOT NULL,
	[OrganizacionID] int,
	[TipoCambio] decimal(10,4) NOT NULL,
	[Folio] bigint NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[IPRM] decimal(10,4),
	[CuotaEjidal] decimal(10,4),
	[ProEducacion] decimal(10,4),
	[PIEAFES] decimal(10,4),
	[Factura] varchar(50) NOT NULL,
	[Cosecha] char(3) NOT NULL,
	[FechaInicio] smalldatetime NOT NULL,
	[FechaFin] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LiquidacionDetalle] ( 
	[LiquidacionDetalleID] int identity(1,1)  NOT NULL,
	[LiquidacionID] int NOT NULL,
	[EntradaProductoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Lote] ( 
	[LoteID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CorralID] int NOT NULL,
	[Lote] varchar(20) NOT NULL,
	[TipoCorralID] int NOT NULL,
	[TipoProcesoID] int NOT NULL,
	[FechaInicio] smalldatetime NOT NULL,
	[CabezasInicio] int NOT NULL,
	[FechaCierre] smalldatetime,
	[Cabezas] int NOT NULL,
	[FechaDisponibilidad] smalldatetime,
	[DisponibilidadManual] bit NOT NULL,
	[FechaSalida] datetime,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LoteDistribucionAlimento] ( 
	[LoteDistribucionAlimentoID] int identity(1,1)  NOT NULL,
	[LoteID] int NOT NULL,
	[TipoServicioID] int NOT NULL,
	[EstatusDistribucionID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LoteProyeccion] ( 
	[LoteProyeccionID] int identity(1,1)  NOT NULL,
	[LoteID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Frame] decimal(10,2) NOT NULL,
	[GananciaDiaria] decimal(10,2) NOT NULL,
	[ConsumoBaseHumeda] decimal(10,2) NOT NULL,
	[Conversion] decimal(10,2) NOT NULL,
	[PesoMaduro] int NOT NULL,
	[PesoSacrificio] int NOT NULL,
	[DiasEngorda] int NOT NULL,
	[FechaEntradaZilmax] smalldatetime NOT NULL,
	[Revision] bit DEFAULT 0 NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LoteReimplante] ( 
	[LoteReimplanteID] int identity(1,1)  NOT NULL,
	[LoteProyeccionID] int NOT NULL,
	[NumeroReimplante] int NOT NULL,
	[FechaProyectada] smalldatetime NOT NULL,
	[PesoProyectado] int NOT NULL,
	[FechaReal] smalldatetime,
	[PesoReal] int,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LoteSacrificio] ( 
	[LoteSacrificioID] int identity(1,1)  NOT NULL,
	[LoteID] int NOT NULL,
	[OrdenSacrificioID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[ImporteCanal] decimal(18,2) NOT NULL,
	[ImportePiel] decimal(18,2) NOT NULL,
	[ImporteVisera] decimal(18,2) NOT NULL,
	[Serie] varchar(5),
	[Folio] varchar(10),
	[Observaciones] varchar(255),
	[ClienteID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [LoteSacrificioDetalle] ( 
	[LoteSacrificioDetalleID] int identity(1,1)  NOT NULL,
	[LoteSacrificioID] int NOT NULL,
	[AnimalID] int NOT NULL
)
; GO

CREATE TABLE [MermaSuperavit] ( 
	[MermaSuperavitID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Merma] decimal(12,2) DEFAULT 0 NOT NULL,
	[Superavit] decimal(12,2) DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [MetodoPago] ( 
	[MetodoPagoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50),
	[Activo] bit,
	[FechaCreacion] datetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Mezcladora] ( 
	[MezcladoraID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[NumeroEconomico] varchar(10) NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Modulo] ( 
	[ModuloID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[Orden] int NOT NULL,
	[Control] varchar(50)
)
; GO

CREATE TABLE [Moneda] ( 
	[MonedaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Abreviatura] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [MonitoreoSilo] ( 
	[MonitoreoSiloID] int identity(1,1)  NOT NULL,
	[FechaMonitoreo] smalldatetime NOT NULL,
	[TemperaturaAmbiente] decimal(5,2) NOT NULL,
	[SiloID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[HR] decimal(15,2) NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [MonitoreoSiloDetalle] ( 
	[MonitoreoSiloDetalleID] int identity(1,1)  NOT NULL,
	[MonitoreoSiloID] int NOT NULL,
	[MonitoreoSiloIndicadorID] int NOT NULL,
	[Temperatura] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [MonitoreoSiloIndicador] ( 
	[MonitoreoSiloIndicadorID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[AlturaSilo] int NOT NULL,
	[UbicacionSensor] int NOT NULL,
	[OrdenSensor] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Muertes] ( 
	[MuerteID] int identity(1,1)  NOT NULL,
	[Arete] varchar(15),
	[AreteMetalico] varchar(15),
	[Observaciones] varchar(255),
	[LoteID] int NOT NULL,
	[OperadorDeteccion] int NOT NULL,
	[FechaDeteccion] smalldatetime NOT NULL,
	[FotoDeteccion] varchar(250),
	[OperadorRecoleccion] int,
	[FechaRecoleccion] smalldatetime,
	[OperadorNecropsia] int,
	[FechaNecropsia] smalldatetime,
	[FotoNecropsia] varchar(250),
	[FolioSalida] int,
	[OperadorCancelacion] int,
	[FechaCancelacion] smalldatetime,
	[MotivoCancelacion] varchar(255),
	[EstatusID] int NOT NULL,
	[ProblemaID] int,
	[Comentarios] varchar(255) DEFAULT (('')) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [MuestreoFibraFormula] ( 
	[MuestreoFibraFormulaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FechaMuestreo] smalldatetime NOT NULL,
	[FormulaID] int NOT NULL,
	[PesoInicial] decimal(15,2) NOT NULL,
	[Grande] bit DEFAULT 0 NOT NULL,
	[PesoFibraGrande] decimal(15,2),
	[Mediana] bit DEFAULT 0 NOT NULL,
	[PesoFibraMediano] decimal(15,2),
	[FinoTamiz] bit DEFAULT 0 NOT NULL,
	[PesoFinoTamiz] decimal(15,2),
	[FinoBase] bit DEFAULT 0 NOT NULL,
	[PesoFinoBase] decimal(15,2),
	[Origen] varchar(100) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [MuestreoFibraIngrediente] ( 
	[MuestreoFibraIngredienteID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FechaMuestreo] smalldatetime NOT NULL,
	[ProductoID] int NOT NULL,
	[PesoMuestra] decimal(14,2) NOT NULL,
	[Grueso] bit DEFAULT 0 NOT NULL,
	[PesoGranoGrueso] decimal(15,2),
	[Mediano] bit DEFAULT 0 NOT NULL,
	[PesoGranoMediano] decimal(15,2),
	[Fino] bit DEFAULT 0 NOT NULL,
	[PesoGranoFino] decimal(15,2),
	[CribaEntrada] int NOT NULL,
	[CribaSalida] int NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Notificaciones] ( 
	[NotificacionID] int identity(1,1)  NOT NULL,
	[AccionesSiapID] int NOT NULL,
	[UsuarioDestino] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Observacion] ( 
	[ObservacionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoObservacionID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Operador] ( 
	[OperadorID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Nombre] varchar(50) NOT NULL,
	[ApellidoPaterno] varchar(50) NOT NULL,
	[ApellidoMaterno] varchar(50) NOT NULL,
	[CodigoSAP] char(8) NOT NULL,
	[RolID] int NOT NULL,
	[UsuarioID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [OrdenSacrificio] ( 
	[OrdenSacrificioID] int identity(1,1)  NOT NULL,
	[FolioOrdenSacrificio] int,
	[OrganizacionID] int NOT NULL,
	[Observaciones] varchar(300),
	[FechaOrden] smalldatetime NOT NULL,
	[EstatusID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [OrdenSacrificioDetalle] ( 
	[OrdenSacrificioDetalleID] int identity(1,1)  NOT NULL,
	[OrdenSacrificioID] int NOT NULL,
	[FolioSalida] int NOT NULL,
	[CorraletaID] int,
	[LoteID] int NOT NULL,
	[CorraletaCodigo] varchar(10) NOT NULL,
	[CabezasLote] int NOT NULL,
	[DiasEngordaGrano] int NOT NULL,
	[DiasRetiro] int NOT NULL,
	[CabezasSacrificio] int NOT NULL,
	[Turno] int DEFAULT 1 NOT NULL,
	[Proveedor] varchar(100) NOT NULL,
	[Clasificacion] varchar(100) NOT NULL,
	[Orden] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacion] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacion] int
)
; GO

CREATE TABLE [Organizacion] ( 
	[OrganizacionID] int identity(1,1)  NOT NULL,
	[TipoOrganizacionID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Direccion] varchar(255),
	[RFC] varchar(13) NOT NULL,
	[IvaID] int NOT NULL,
	[Division] varchar(4) DEFAULT (('')) NOT NULL,
	[Sociedad] varchar(3) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Padre] ( 
	[PadreID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Imagen] varchar(100) NOT NULL,
	[Activo] bit NOT NULL
)
; GO

CREATE TABLE [Parametro] ( 
	[ParametroID] int identity(1,1)  NOT NULL,
	[TipoParametroID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Clave] varchar(30) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ParametroGeneral] ( 
	[ParametroGeneralID] int identity(1,1)  NOT NULL,
	[ParametroID] int NOT NULL,
	[Valor] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ParametroOrganizacion] ( 
	[ParametroOrganizacionID] int identity(1,1)  NOT NULL,
	[ParametroID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Valor] varchar(100) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ParametroTrampa] ( 
	[ParametroTrampaID] int identity(1,1)  NOT NULL,
	[ParametroID] int NOT NULL,
	[TrampaID] int NOT NULL,
	[Valor] varchar(100) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Pedido] ( 
	[PedidoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[AlmacenID] int NOT NULL,
	[FolioPedido] int NOT NULL,
	[FechaPedido] smalldatetime NOT NULL,
	[Observaciones] varchar(255),
	[EstatusID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [PedidoDetalle] ( 
	[PedidoDetalleID] int identity(1,1)  NOT NULL,
	[PedidoID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[InventarioLoteIDDestino] int NOT NULL,
	[CantidadSolicitada] decimal(14,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [PesajeMateriaPrima] ( 
	[PesajeMateriaPrimaID] int identity(1,1)  NOT NULL,
	[ProgramacionMateriaPrimaID] int NOT NULL,
	[ProveedorChoferID] int,
	[Ticket] int NOT NULL,
	[CamionID] int,
	[PesoBruto] int DEFAULT 0 NOT NULL,
	[PesoTara] int DEFAULT 0 NOT NULL,
	[Piezas] int DEFAULT 0 NOT NULL,
	[TipoPesajeID] int NOT NULL,
	[UsuarioIDSurtido] int,
	[FechaSurtido] smalldatetime,
	[UsuarioIDRecibe] int,
	[FechaRecibe] smalldatetime,
	[EstatusID] int NOT NULL,
	[AlmacenMovimientoOrigenID] bigint,
	[AlmacenMovimientoDestinoID] bigint,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Poliza] ( 
	[PolizaID] int identity(1,1)  NOT NULL,
	[TipoPolizaID] int NOT NULL,
	[XmlPoliza] xml NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ArchivoEnviadoServidor] bit DEFAULT 0 NOT NULL,
	[Conciliada] bit DEFAULT 0 NOT NULL,
	[Estatus] bigint NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioModificacionID] int,
	[FechaModificacion] datetime
)
; GO

CREATE TABLE [PrecioGanado] ( 
	[PrecioGanadoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[Precio] decimal(10,2) DEFAULT 0 NOT NULL,
	[FechaVigencia] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [PrecioPAC] ( 
	[PrecioPACID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoPACID] int NOT NULL,
	[Precio] decimal(12,2) NOT NULL,
	[FechaInicio] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Pregunta] ( 
	[PreguntaID] int identity(1,1)  NOT NULL,
	[TipoPreguntaID] int,
	[Descripcion] varchar(255) NOT NULL,
	[Valor] varchar(50),
	[Orden] int DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT GETDATE() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Premezcla] ( 
	[PremezclaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[ProductoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [PremezclaDetalle] ( 
	[PremezclaDetalleID] int identity(1,1)  NOT NULL,
	[PremezclaID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Porcentaje] decimal(10,3) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [PremezclaDistribucion] ( 
	[PremezclaDistribucionID] int identity(1,1)  NOT NULL,
	[ProductoID] int NOT NULL,
	[FechaEntrada] smalldatetime NOT NULL,
	[CantidadExistente] bigint NOT NULL,
	[CostoUnitario] decimal(18,2),
	[AlmacenMovimientoID] bigint,
	[ProveedorID] int NOT NULL,
	[IVA] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [PremezclaDistribucionDetalle] ( 
	[PremezclaDistribucionDetalleID] int identity(1,1)  NOT NULL,
	[PremezclaDistribucionID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CantidadASurtir] bigint NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Problema] ( 
	[ProblemaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoProblemaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProblemaSintoma] ( 
	[ProblemaSintomaID] int identity(1,1)  NOT NULL,
	[ProblemaID] int NOT NULL,
	[SintomaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT GETDATE() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProblemaTratamiento] ( 
	[ProblemaTratamientoID] int identity(1,1)  NOT NULL,
	[ProblemaID] int NOT NULL,
	[TratamientoID] int NOT NULL,
	[Dias] int,
	[Orden] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT GETDATE() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProduccionDiaria] ( 
	[ProduccionDiariaID] int identity(1,1)  NOT NULL,
	[Turno] int NOT NULL,
	[LitrosInicial] decimal(14,2) NOT NULL,
	[LitrosFinal] decimal(14,2),
	[HorometroInicial] int,
	[HorometroFinal] int,
	[FechaProduccion] smalldatetime NOT NULL,
	[UsuarioIDAutorizo] int,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProduccionDiariaDetalle] ( 
	[ProduccionDiariaDetalleID] int identity(1,1)  NOT NULL,
	[ProduccionDiariaID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[PesajeMateriaPrimaID] int NOT NULL,
	[EspecificacionForraje] int NOT NULL,
	[HoraInicial] varchar(5) NOT NULL,
	[HoraFinal] varchar(5),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProduccionFormula] ( 
	[ProduccionFormulaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FolioFormula] int NOT NULL,
	[FormulaID] int NOT NULL,
	[CantidadProducida] decimal(14,2) DEFAULT 0 NOT NULL,
	[FechaProduccion] smalldatetime NOT NULL,
	[AlmacenMovimientoEntradaID] bigint,
	[AlmacenMovimientoSalidaID] bigint,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProduccionFormulaBatch] ( 
	[ProduccionFormulaBatchID] int identity(1,1)  NOT NULL,
	[ProduccionFormulaID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[FormulaID] int NOT NULL,
	[RotomixID] int NOT NULL,
	[Batch] int NOT NULL,
	[CantidadProgramada] int NOT NULL,
	[CantidadReal] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProduccionFormulaDetalle] ( 
	[ProduccionFormulaDetalleID] int identity(1,1)  NOT NULL,
	[ProduccionFormulaID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[CantidadProducto] decimal(14,2) NOT NULL,
	[IngredienteID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Producto] ( 
	[ProductoID] int NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[SubFamiliaID] int NOT NULL,
	[UnidadID] int NOT NULL,
	[ManejaLote] bit DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProductoCodigoParte] ( 
	[ProductoCodigoParteID] int identity(1,1)  NOT NULL,
	[ProductoID] int NOT NULL,
	[CodigoParte] varchar(30) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProgramacionCorte] ( 
	[FolioProgramacionID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FolioEntradaID] int NOT NULL,
	[FechaProgramacion] smalldatetime NOT NULL,
	[FechaInicioCorte] smalldatetime,
	[FechaFinCorte] smalldatetime,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProgramacionMateriaPrima] ( 
	[ProgramacionMateriaPrimaID] int identity(1,1)  NOT NULL,
	[PedidoDetalleID] int NOT NULL,
	[OrganizacionID] int NOT NULL,
	[AlmacenID] int NOT NULL,
	[InventarioLoteIDOrigen] int NOT NULL,
	[CantidadProgramada] decimal(14,2) NOT NULL,
	[CantidadEntregada] decimal(14,2),
	[Observaciones] varchar(255),
	[Justificacion] varchar(255),
	[FechaProgramacion] smalldatetime NOT NULL,
	[AlmacenMovimientoID] bigint,
	[AutorizacionMateriaPrimaID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProgramacionReimplante] ( 
	[FolioProgramacionID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[LoteID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Activo] bit,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProgramacionReimplanteDetalle] ( 
	[ProgramacionReimplanteDetalleID] int identity(1,1)  NOT NULL,
	[FolioProgramacionID] int NOT NULL,
	[CorralDestinoID] int NOT NULL,
	[PuntaChica] bit DEFAULT 0 NOT NULL,
	[Activo] bit,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Proveedor] ( 
	[ProveedorID] int identity(1,1)  NOT NULL,
	[CodigoSAP] char(10) NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[TipoProveedorID] int NOT NULL,
	[ImporteComision] decimal(10,2),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProveedorAlmacen] ( 
	[ProveedorAlmacenID] int identity(1,1)  NOT NULL,
	[ProveedorID] int NOT NULL,
	[AlmacenID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ProveedorChofer] ( 
	[ProveedorChoferID] int identity(1,1)  NOT NULL,
	[ProveedorID] int NOT NULL,
	[ChoferID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RecepcionProducto] ( 
	[RecepcionProductoID] int identity(1,1)  NOT NULL,
	[AlmacenID] int NOT NULL,
	[FolioRecepcion] int NOT NULL,
	[FolioOrdenCompra] varchar(6) NOT NULL,
	[FechaSolicitud] smalldatetime NOT NULL,
	[ProveedorID] int NOT NULL,
	[FechaRecepcion] smalldatetime NOT NULL,
	[AlmacenMovimientoID] bigint NOT NULL,
	[Factura] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RecepcionProductoDetalle] ( 
	[RecepcionProductoDetalleID] int identity(1,1)  NOT NULL,
	[RecepcionProductoID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Cantidad] decimal(18,2) NOT NULL,
	[PrecioPromedio] decimal(18,4) NOT NULL,
	[Importe] decimal(24,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RegistroVigilancia] ( 
	[RegistroVigilanciaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ProveedorIDMateriasPrimas] int NOT NULL,
	[ProveedorChoferID] int,
	[Transportista] varchar(50),
	[Chofer] varchar(100),
	[ProductoID] int NOT NULL,
	[CamionID] int,
	[Camion] varchar(10),
	[Marca] varchar(50) NOT NULL,
	[Color] varchar(50) NOT NULL,
	[FolioTurno] int NOT NULL,
	[FechaLlegada] smalldatetime NOT NULL,
	[FechaSalida] smalldatetime,
	[ContratoID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Reparto] ( 
	[RepartoID] bigint identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CorralID] int NOT NULL,
	[LoteID] int,
	[Fecha] smalldatetime NOT NULL,
	[PesoInicio] int NOT NULL,
	[PesoProyectado] int NOT NULL,
	[DiasEngorda] int NOT NULL,
	[PesoRepeso] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RepartoAlimento] ( 
	[RepartoAlimentoID] int identity(1,1)  NOT NULL,
	[TipoServicioID] int NOT NULL,
	[CamionRepartoID] int NOT NULL,
	[UsuarioIDReparto] int NOT NULL,
	[HorometroInicial] int NOT NULL,
	[HorometroFinal] int,
	[OdometroInicial] int NOT NULL,
	[OdometroFinal] int,
	[LitrosDiesel] int,
	[FechaReparto] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RepartoAlimentoDetalle] ( 
	[RepartoAlimentoDetalleID] int identity(1,1)  NOT NULL,
	[RepartoAlimentoID] int NOT NULL,
	[FolioReparto] int NOT NULL,
	[FormulaIDRacion] int NOT NULL,
	[Tolva] varchar(10) DEFAULT 0 NOT NULL,
	[KilosEmbarcados] int NOT NULL,
	[KilosRepartidos] int NOT NULL,
	[Sobrante] int NOT NULL,
	[PesoFinal] int NOT NULL,
	[CorralIDInicio] int NOT NULL,
	[CorralIDFinal] int NOT NULL,
	[HoraRepartoInicio] varchar(5) NOT NULL,
	[HoraRepartoFinal] varchar(5) NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RepartoAvance] ( 
	[UsuarioID] int NOT NULL,
	[Seccion] varchar(50) NOT NULL,
	[TotalCorrales] int NOT NULL,
	[TotalCorralesSeccion] int NOT NULL,
	[TotalCorralesProcesados] int NOT NULL,
	[TotalCorralesProcesadosSeccion] int,
	[PorcentajeSeccion] int NOT NULL,
	[PorcentajeTotal] int NOT NULL,
	[EstatusError] bit DEFAULT 0 NOT NULL
)
; GO

CREATE TABLE [RepartoDetalle] ( 
	[RepartoDetalleID] bigint identity(1,1)  NOT NULL,
	[RepartoID] bigint NOT NULL,
	[TipoServicioID] int NOT NULL,
	[FormulaIDProgramada] int NOT NULL,
	[FormulaIDServida] int,
	[CantidadProgramada] int NOT NULL,
	[CantidadServida] int DEFAULT 0,
	[HoraReparto] char(5) DEFAULT (('')),
	[CostoPromedio] decimal(12,4) DEFAULT 0 NOT NULL,
	[Importe] decimal(18,2) DEFAULT 0 NOT NULL,
	[Servido] bit DEFAULT 0 NOT NULL,
	[Cabezas] int NOT NULL,
	[EstadoComederoID] int NOT NULL,
	[CamionRepartoID] int,
	[Observaciones] varchar(255),
	[Ajuste] bit DEFAULT 0 NOT NULL,
	[Prorrateo] bit DEFAULT 0 NOT NULL,
	[AlmacenMovimientoID] bigint,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Retencion] ( 
	[RetencionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[TipoRetencion] char(3) NOT NULL,
	[IndicadorRetencion] char(2) NOT NULL,
	[IndicadorImpuesto] char(2) NOT NULL,
	[Tasa] decimal(5,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [RevisionImplante] ( 
	[RevisionImplanteID] int identity(1,1)  NOT NULL,
	[LoteID] int NOT NULL,
	[AnimalID] bigint NOT NULL,
	[AreaRevisionID] int NOT NULL,
	[EstadoImplanteID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Rol] ( 
	[RolID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Roladora] ( 
	[RoladoraID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Rotomix] ( 
	[RotomixID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SalidaAnimal] ( 
	[SalidaAnimalID] int identity(1,1)  NOT NULL,
	[SalidaGanadoID] int NOT NULL,
	[AnimalID] bigint NOT NULL,
	[LoteID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SalidaGanado] ( 
	[SalidaGanadoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoMovimientoID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[Folio] int NOT NULL,
	[VentaGanadoID] int,
	[Activo] bit NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioModificacionID] int,
	[FechaModificacion] smalldatetime
)
; GO

CREATE TABLE [SalidaProducto] ( 
	[SalidaProductoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[OrganizacionIDDestino ] int,
	[TipoMovimientoID] int NOT NULL,
	[FolioSalida] int NOT NULL,
	[FolioFactura] varchar(15),
	[AlmacenID] int,
	[AlmacenInventarioLoteID] int,
	[ClienteID] int,
	[CuentaSAPID] int,
	[Observaciones] varchar(255),
	[Precio] decimal(18,2),
	[Importe] decimal(18,2),
	[AlmacenMovimientoID] bigint,
	[PesoTara] int DEFAULT 0 NOT NULL,
	[PesoBruto] int DEFAULT 0 NOT NULL,
	[Piezas] int DEFAULT 0 NOT NULL,
	[FechaSalida] smalldatetime NOT NULL,
	[ChoferID] int,
	[CamionID] int,
	[AutorizacionMateriaPrimaID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [ServicioAlimento] ( 
	[ServicioID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CorralID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[FormulaID] int NOT NULL,
	[KilosProgramados] int NOT NULL,
	[Comentarios] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Silo] ( 
	[SiloID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Descripcion] varchar(10) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Sintoma] ( 
	[SintomaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT GetDate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SolicitudPremezcla] ( 
	[SolicitudPremezclaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[Fecha] smalldatetime NOT NULL,
	[FechaInicio] smalldatetime NOT NULL,
	[FechaFin] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SolicitudPremezclaDetalle] ( 
	[SolicitudPremezclaDetalleID] int identity(1,1)  NOT NULL,
	[SolicitudPremezclaID] int NOT NULL,
	[FechaLlegada] smalldatetime NOT NULL,
	[PremezclaID] int NOT NULL,
	[CantidadSolicitada] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SolicitudProducto] ( 
	[SolicitudProductoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[FolioSolicitud] int NOT NULL,
	[FechaSolicitud] smalldatetime NOT NULL,
	[UsuarioIDSolicita] int NOT NULL,
	[EstatusID] int NOT NULL,
	[UsuarioIDAutoriza] int,
	[FechaAutorizado] smalldatetime,
	[UsuarioIDEntrega] int,
	[FechaEntrega] smalldatetime,
	[CentroCostoID] int NOT NULL,
	[AlmacenID] int,
	[AlmacenMovimientoID] bigint,
	[ObservacionUsuarioEntrega] varchar(255),
	[ObservacionUsuarioAutoriza] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SolicitudProductoDetalle] ( 
	[SolicitudProductoDetalleID] int identity(1,1)  NOT NULL,
	[SolicitudProductoID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Cantidad] decimal(18,2) DEFAULT 0 NOT NULL,
	[CamionRepartoID] int,
	[EstatusID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SubFamilia] ( 
	[FamiliaID] int NOT NULL,
	[SubFamiliaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[CuentaSAPID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SupervisionDetectores] ( 
	[SupervisionDetectoresID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[OperadorID] int NOT NULL,
	[FechaSupervision] smalldatetime NOT NULL,
	[CriterioSupervisionID] int NOT NULL,
	[Observaciones] varchar(255),
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SupervisionDetectoresDetalle] ( 
	[SupervisionDetectoresDetalleID] int identity(1,1)  NOT NULL,
	[SupervisionDetectoresID] int NOT NULL,
	[PreguntaID] int NOT NULL,
	[Respuesta] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacion] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacion] int
)
; GO

CREATE TABLE [SupervisionGanado] ( 
	[SupervisionGanadoID] int identity(1,1)  NOT NULL,
	[LoteID] int NOT NULL,
	[Arete] varchar(15),
	[AreteMetalico] varchar(15),
	[FotoSupervision] varchar(250),
	[FechaDeteccion] datetime NOT NULL,
	[ConceptoDeteccionID] int NOT NULL,
	[Acuerdo] varchar(150),
	[Notificacion] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [SupervisorEnfermeria] ( 
	[SupervisorEnfermeriaID] int identity(1,1)  NOT NULL,
	[OperadorID] int NOT NULL,
	[EnfermeriaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TarifaSecado] ( 
	[TarifaSecadoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[RangoMinimo] decimal(10,2) NOT NULL,
	[RangoMaximo] decimal(10,2) NOT NULL,
	[Tarifa] decimal(10,2) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TiempoMuerto] ( 
	[TiempoMuertoID] int identity(1,1)  NOT NULL,
	[ProduccionDiariaID] int,
	[RepartoAlimentoID] int,
	[HoraInicio] varchar(5) NOT NULL,
	[HoraFin] varchar(5) NOT NULL,
	[CausaTiempoMuertoID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoAlmacen] ( 
	[TipoAlmacenID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoAutorizacion] ( 
	[TipoAutorizacionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoCambio] ( 
	[TipoCambioID] int identity(1,1)  NOT NULL,
	[MonedaID] int,
	[Descripcion] varchar(50) NOT NULL,
	[Cambio] decimal(10,4) NOT NULL,
	[Fecha] datetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoCancelacion] ( 
	[TipoCancelacionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[DiasPermitidos] int DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoContrato] ( 
	[TipoContratoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoCorral] ( 
	[TipoCorralID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[GrupoCorralID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoCosto] ( 
	[TipoCostoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoCuenta] ( 
	[TipoCuentaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoDeteccion] ( 
	[TipoDeteccionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoEmbarque] ( 
	[TipoEmbarqueID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoEstatus] ( 
	[TipoEstatusID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoFlete] ( 
	[TipoFleteID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoFolio] ( 
	[TipoFolioID] tinyint identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL
)
; GO

CREATE TABLE [TipoFormula] ( 
	[TipoFormulaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoGanado] ( 
	[TipoGanadoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Sexo] char(1) NOT NULL,
	[PesoMinimo] int NOT NULL,
	[PesoMaximo] int NOT NULL,
	[PesoSalida] int DEFAULT 0 NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoGanadoCorrales] ( 
	[TipoGanadoCorralID] int identity(1,1)  NOT NULL,
	[TipoGanadoID] int NOT NULL,
	[RangoInicial] int NOT NULL,
	[RangoFinal] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoLugarMuestra] ( 
	[TipoLugarMuestraID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoMovimiento] ( 
	[TipoMovimientoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[EsGanado] bit NOT NULL,
	[EsProducto] bit NOT NULL,
	[EsEntrada] bit NOT NULL,
	[EsSalida] bit NOT NULL,
	[ClaveCodigo] char(2) NOT NULL,
	[TipoPolizaID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoMuestra] ( 
	[TipoMuestraID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoObjetivoCalidad] ( 
	[TipoObjetivoCalidadID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoObservacion] ( 
	[TipoObservacionID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoOrganizacion] ( 
	[TipoOrganizacionID] int identity(1,1)  NOT NULL,
	[TipoProcesoID] int NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoPAC] ( 
	[TipoPACID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoParametro] ( 
	[TipoParametroID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoPesaje] ( 
	[TipoPesajeID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoPoliza] ( 
	[TipoPolizaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[ClavePoliza] char(2) NOT NULL,
	[PostFijoRef3] char(2) NOT NULL,
	[TextoDocumento] varchar(50) NOT NULL,
	[ImprimePoliza] bit NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoPregunta] ( 
	[TipoPreguntaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoProblema] ( 
	[TipoProblemaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoProceso] ( 
	[TipoProcesoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime,
	[UsuarioCreacionID] int,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoProrrateo] ( 
	[TipoProrrateoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoProveedor] ( 
	[TipoProveedorID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[CodigoGrupoSAP] char(4) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoServicio] ( 
	[TipoServicioID] int identity(1,1)  NOT NULL,
	[HoraInicio] char(5) NOT NULL,
	[HoraFin] char(5) NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime DEFAULT getdate() NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoTarifa] ( 
	[TipoTarifaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoTecnica] ( 
	[TipoTecnicaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] datetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] datetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TipoTratamiento] ( 
	[TipoTratamientoID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Trampa] ( 
	[TrampaID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[OrganizacionID] int NOT NULL,
	[TipoTrampa] char(1) NOT NULL,
	[HostName] varchar(50) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TraspasoMateriaPrima] ( 
	[TraspasoMateriaPrimaID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[ContratoOrigenID] int,
	[ContratoDestinoID] int,
	[FolioTraspaso] bigint NOT NULL,
	[AlmacenOrigenID] int NOT NULL,
	[AlmacenDestinoID] int NOT NULL,
	[InventarioLoteOrigenID] int,
	[InventarioLoteDestinoID] int,
	[CuentaSAPID] int,
	[Justificacion] varchar(255) NOT NULL,
	[AlmacenMovimientoEntradaID] bigint,
	[AlmacenMovimientoSalidaID] bigint,
	[FechaMovimiento] smalldatetime NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Tratamiento] ( 
	[TratamientoID] int identity(1,1)  NOT NULL,
	[OrganizacionID] int NOT NULL,
	[CodigoTratamiento] int NOT NULL,
	[TipoTratamientoID] int NOT NULL,
	[Sexo] char(1) NOT NULL,
	[RangoInicial] int NOT NULL,
	[RangoFinal] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [TratamientoProducto] ( 
	[TratamientoProductoID] int identity(1,1)  NOT NULL,
	[TratamientoID] int NOT NULL,
	[ProductoID] int NOT NULL,
	[Dosis] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [UnidadMedicion] ( 
	[UnidadID] int identity(1,1)  NOT NULL,
	[Descripcion] varchar(50) NOT NULL,
	[ClaveUnidad] char(3) NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [Usuario] ( 
	[UsuarioID] int identity(1,1)  NOT NULL,
	[Nombre] varchar(200) NOT NULL,
	[OrganizacionID] int NOT NULL,
	[UsuarioActiveDirectory] varchar(100) NOT NULL,
	[Activo] bit
)
; GO

CREATE TABLE [UsuarioGrupo] ( 
	[UsuarioGrupoID] int identity(1,1)  NOT NULL,
	[UsuarioID] int NOT NULL,
	[GrupoID] int NOT NULL
)
; GO

CREATE TABLE [VentaGanado] ( 
	[VentaGanadoID] int identity(1,1)  NOT NULL,
	[FolioTicket] int NOT NULL,
	[FolioFactura] varchar(15),
	[ClienteID] int NOT NULL,
	[FechaVenta] smalldatetime NOT NULL,
	[PesoTara] decimal(18,2) NOT NULL,
	[PesoBruto] decimal(18,2) NOT NULL,
	[LoteID] int,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacionID] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacionID] int
)
; GO

CREATE TABLE [VentaGanadoDetalle] ( 
	[VentaGanadoDetalleID] int identity(1,1)  NOT NULL,
	[VentaGanadoID] int NOT NULL,
	[AnimalID] bigint,
	[Arete] varchar(16) NOT NULL,
	[FotoVenta] varchar(250),
	[CausaPrecioID] int NOT NULL,
	[Activo] bit NOT NULL,
	[FechaCreacion] smalldatetime NOT NULL,
	[UsuarioCreacion] int NOT NULL,
	[FechaModificacion] smalldatetime,
	[UsuarioModificacion] int
)
; GO


ALTER TABLE [Acceso]
	ADD CONSTRAINT [UQ_Acceso_AccesoID] UNIQUE ([AccesoID])
; GO

ALTER TABLE [Acceso]
	ADD CONSTRAINT [UQ_Acceso_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Almacen]
	ADD CONSTRAINT [UQ_Almacen_AlmacenID] UNIQUE ([AlmacenID])
; GO

ALTER TABLE [Almacen]
	ADD CONSTRAINT [UQ_Almacen_CodigoAlmacen] UNIQUE ([CodigoAlmacen])
; GO

ALTER TABLE [AlmacenInventario]
	ADD CONSTRAINT [UQ_AlmacenInventario_AlmacenInventarioID] UNIQUE ([AlmacenInventarioID])
; GO

ALTER TABLE [AlmacenInventario]
	ADD CONSTRAINT [UQ_AlmacenInventario_AlmacenIDProductoID] UNIQUE ([AlmacenID], [ProductoID])
; GO

CREATE INDEX [IXFK_AlmacenInventarioLote_AlmacenInventario_02]
ON [AlmacenInventarioLote] ([AlmacenInventarioID] ASC)
; GO

ALTER TABLE [AlmacenMovimiento]
	ADD CONSTRAINT [UQ_AlmacenMovimientos_AlmacenMovimientoID] UNIQUE ([AlmacenMovimientoID])
; GO

CREATE INDEX [IXFK_AlmacenMovimiento_AnimalMovimiento]
ON [AlmacenMovimiento] ([AnimalMovimientoID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonFactor_Organizacion]
ON [AlmidonFactor] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonFactor_UsuarioCreacion]
ON [AlmidonFactor] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonFactor_UsuarioModificacion]
ON [AlmidonFactor] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_Estatus]
ON [AlmidonHeces] ([EstatusID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_Formula]
ON [AlmidonHeces] ([FormulaID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_Operador]
ON [AlmidonHeces] ([OperdorIDCalidad] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_Organizacion]
ON [AlmidonHeces] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_Producto]
ON [AlmidonHeces] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_UsuarioCreacion]
ON [AlmidonHeces] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_UsuarioIDLaboratorio]
ON [AlmidonHeces] ([UsuarioIDLaboratorio] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHeces_UsuarioModificacion]
ON [AlmidonHeces] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHecesDetalle_AlmidonHeces]
ON [AlmidonHecesDetalle] ([AlmidonHecesID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHecesDetalle_Lote]
ON [AlmidonHecesDetalle] ([LoteID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHecesDetalle_UsuarioCreacion]
ON [AlmidonHecesDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_AlmidonHecesDetalle_UsuarioModificacion]
ON [AlmidonHecesDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_AnalisisGrasa_EntradaProducto]
ON [AnalisisGrasa] ([EntradaProductoID] ASC)
; GO

CREATE INDEX [IXFK_AnalisisGrasa_Organizacion]
ON [AnalisisGrasa] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_AnalisisGrasa_UsuarioCreacion]
ON [AnalisisGrasa] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_AnalisisGrasa_UsuarioModificacion]
ON [AnalisisGrasa] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [AnalisisGrasa]
	ADD CONSTRAINT [CK_AnalisisGrasa_TipoMuestra] CHECK (TipoMuestra IN ('Grasa','Sebo'))
; GO

ALTER TABLE [Animal]
	ADD CONSTRAINT [UQ_Animal_AnimalID] UNIQUE ([AnimalID])
; GO

ALTER TABLE [AnimalMovimiento]
	ADD CONSTRAINT [UQ_AnimalMovimiento_AnimalMovimientoID] UNIQUE ([AnimalMovimientoID])
; GO

ALTER TABLE [Calculo]
	ADD CONSTRAINT [UQ_Calculo_CalculoID] UNIQUE ([CalculoID])
; GO

ALTER TABLE [Calculo]
	ADD CONSTRAINT [UQ_Calculo_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [CalidadGanado]
	ADD CONSTRAINT [UQ_CalidadGanado_Descripcion] UNIQUE ([Descripcion], [Sexo])
; GO

ALTER TABLE [CalidadGanado]
	ADD CONSTRAINT [CK_Calidad_Sexo] CHECK (Sexo IN ('M','H'))
; GO

CREATE INDEX [IXFK_CalidadMateriaPrima_ColorObjetivo]
ON [CalidadMateriaPrima] ([ColorObjetivoID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMateriaPrima_IndicadorObjetivo]
ON [CalidadMateriaPrima] ([IndicadorObjetivoID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMateriaPrima_PedidoDetalle]
ON [CalidadMateriaPrima] ([PedidoDetalleID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMateriaPrima_UsuarioCreacion]
ON [CalidadMateriaPrima] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMateriaPrima_UsuarioModificacion]
ON [CalidadMateriaPrima] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [CalidadMateriaPrima]
	ADD CONSTRAINT [CK_CalidadMateriaPrima_Movimiento] CHECK (Movimiento IN (1,2))
; GO

CREATE INDEX [IXFK_CalidadMezclado_CamionReparto]
ON [CalidadMezclado] ([CamionRepartoID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_Chofer]
ON [CalidadMezclado] ([ChoferID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_Formula]
ON [CalidadMezclado] ([FormulaID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_Lote]
ON [CalidadMezclado] ([LoteID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_Mezcladora]
ON [CalidadMezclado] ([MezcladoraID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_OperadorIDAnalista]
ON [CalidadMezclado] ([OperadorIDAnalista] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_OperadorIDMezclador]
ON [CalidadMezclado] ([OperadorIDMezclador] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_Organizacion]
ON [CalidadMezclado] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_TipoLugarMuestra]
ON [CalidadMezclado] ([TipoLugarMuestraID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_TipoTecnica]
ON [CalidadMezclado] ([TipoTecnicaID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_UsuarioCreacion]
ON [CalidadMezclado] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_UsuarioLaboratorio]
ON [CalidadMezclado] ([UsuarioIDLaboratorio] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezclado_UsuarioModificacion]
ON [CalidadMezclado] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezcladoDetalle_CalidadMezclado]
ON [CalidadMezcladoDetalle] ([CalidadMezcladoID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezcladoDetalle_TipoMuestra]
ON [CalidadMezcladoDetalle] ([TipoMuestraID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezcladoDetalle_UsuarioCreacion]
ON [CalidadMezcladoDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezcladoDetalle_UsuarioModificacion]
ON [CalidadMezcladoDetalle] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [CalidadMezcladoDetalle]
	ADD CONSTRAINT [CK_CalidadMezcladoDetalle_NumeroMuestra] CHECK (NumeroMuestra IN ('M1','M2','M3','M4','M5','M6'))
; GO

ALTER TABLE [CalidadMezcladoFactor]
	ADD CONSTRAINT [UQ_CalidadMezcladoFactor_TipoMuestraID] UNIQUE ([TipoMuestraID])
; GO

CREATE INDEX [IXFK_CalidadMezcladoFactor_TipoMuestra]
ON [CalidadMezcladoFactor] ([TipoMuestraID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezcladoFactor_UsuarioCreacion]
ON [CalidadMezcladoFactor] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CalidadMezcladoFactor_UsuarioModificacion]
ON [CalidadMezcladoFactor] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Camion]
	ADD CONSTRAINT [UQ_Camion_CamionID] UNIQUE ([CamionID])
; GO

ALTER TABLE [CamionReparto]
	ADD CONSTRAINT [UK_CamionReparto_OrganizacionNumeroEconomico] UNIQUE ([OrganizacionID], [NumeroEconomico])
; GO

ALTER TABLE [CausaRechazo]
	ADD CONSTRAINT [UQ_CausaRechazo_CausaRechazoID] UNIQUE ([CausaRechazoID])
; GO

CREATE INDEX [IXFK_CausaTiempoMuerto_UsuarioCreacion]
ON [CausaTiempoMuerto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CausaTiempoMuerto_UsuarioModificacion]
ON [CausaTiempoMuerto] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [CausaTiempoMuerto]
	ADD CONSTRAINT [CK_CausaTiempoMuerto_TipoCausa] CHECK (TipoCausa IN (1,2))
; GO

ALTER TABLE [CentroBeneficio]
	ADD CONSTRAINT [UQ_CentroBeneficio_CentroBeneficioSAP] UNIQUE ([CentroBeneficioSAP])
; GO

ALTER TABLE [CentroCosto]
	ADD CONSTRAINT [UQ_CentroCosto_CentroCostoSAP] UNIQUE ([CentroCostoSAP])
; GO

ALTER TABLE [CentroCostoUsuario]
	ADD CONSTRAINT [UQ_CentroCostoUsuario_UsuarioID] UNIQUE ([UsuarioID], [CentroCostoID], [Autoriza])
; GO

CREATE INDEX [IXFK_CentroCostoUsuario_CentroCosto]
ON [CentroCostoUsuario] ([CentroCostoID] ASC)
; GO

CREATE INDEX [IXFK_CentroCostoUsuario_UsuarioAutorizador]
ON [CentroCostoUsuario] ([UsuarioID] ASC)
; GO

CREATE INDEX [IXFK_CentroCostoUsuario_UsuarioCreacion]
ON [CentroCostoUsuario] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CentroCostoUsuario_UsuarioModificacion]
ON [CentroCostoUsuario] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladora_CheckListRoladoraGeneral]
ON [CheckListRoladora] ([CheckListRoladoraGeneralID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladora_Roladora]
ON [CheckListRoladora] ([RoladoraID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladora_UsuarioCreacion]
ON [CheckListRoladora] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladora_UsuarioModificacion]
ON [CheckListRoladora] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladora_UsuarioResponsable]
ON [CheckListRoladora] ([UsuarioIDResponsable] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraAccion_CheckListRoladoraRango]
ON [CheckListRoladoraAccion] ([CheckListRoladoraRangoID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraAccion_UsuarioCreacion]
ON [CheckListRoladoraAccion] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraAccion_UsuarioModificacion]
ON [CheckListRoladoraAccion] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraDetalle_CheckListRoladora]
ON [CheckListRoladoraDetalle] ([CheckListRoladoraID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraDetalle_CheckListRoladoraAccion]
ON [CheckListRoladoraDetalle] ([CheckListRoladoraAccionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraDetalle_CheckListRoladoraRango]
ON [CheckListRoladoraDetalle] ([CheckListRoladoraRangoID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraDetalle_UsuarioCreacion]
ON [CheckListRoladoraDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraDetalle_UsuarioModificacion]
ON [CheckListRoladoraDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraGeneral_UsuarioCreacion]
ON [CheckListRoladoraGeneral] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraGeneral_UsuarioModificacion]
ON [CheckListRoladoraGeneral] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraGeneral_UsuarioSupervisor]
ON [CheckListRoladoraGeneral] ([UsuarioIDSupervisor] ASC)
; GO

ALTER TABLE [CheckListRoladoraGeneral]
	ADD CONSTRAINT [CK_CheckListRoladoraGeneral_Turno] CHECK (Turno in (1,2,3))
; GO

ALTER TABLE [CheckListRoladoraGeneral]
	ADD CONSTRAINT [UQ_CheckListRoladoraGeneral_Turno_Fecha] UNIQUE ([Turno], [FechaInicio])
; GO

CREATE INDEX [IXFK_CheckListRoladoraRango_Pregunta]
ON [CheckListRoladoraRango] ([PreguntaID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraRango_UsuarioCreacion]
ON [CheckListRoladoraRango] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_CheckListRoladoraRango_UsuarioModificacion]
ON [CheckListRoladoraRango] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Chofer]
	ADD CONSTRAINT [UQ_Chofer_ChoferID] UNIQUE ([ChoferID])
; GO

CREATE INDEX [IXFK_ClaseCostoProducto_Almacen]
ON [ClaseCostoProducto] ([AlmacenID] ASC)
; GO

CREATE INDEX [IXFK_ClaseCostoProducto_CuentaSAP]
ON [ClaseCostoProducto] ([CuentaSAPID] ASC)
; GO

CREATE INDEX [IXFK_ClaseCostoProducto_Producto]
ON [ClaseCostoProducto] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_ClaseCostoProducto_UsuarioCreacion]
ON [ClaseCostoProducto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ClaseCostoProducto_UsuarioModificacion]
ON [ClaseCostoProducto] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [ClasificacionGanado]
	ADD CONSTRAINT [UQ_ClasificacionGanado_ClasificacionGanadoID] UNIQUE ([ClasificacionGanadoID])
; GO

ALTER TABLE [Cliente]
	ADD CONSTRAINT [CK_Cliente_CondicionPago] CHECK (CondicionPago IN (1,2))
; GO

CREATE INDEX [IXFK_ColorObjetivo_TipoObjetivoCalidad]
ON [ColorObjetivo] ([TipoObjetivoCalidadID] ASC)
; GO

CREATE INDEX [IXFK_ColorObjetivo_UsuarioCreacion]
ON [ColorObjetivo] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ColorObjetivo_UsuarioModificacion]
ON [ColorObjetivo] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Condicion]
	ADD CONSTRAINT [UQ_Condicion_CondicionID] UNIQUE ([CondicionID])
; GO

ALTER TABLE [CondicionJaula]
	ADD CONSTRAINT [UQ_CondicionJaula_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [ConfiguracionImplante]
	ADD CONSTRAINT [UQ_ConfiguracionImplante_ConfiguracionImplanteID] UNIQUE ([ConfiguracionImplanteID])
; GO

ALTER TABLE [ConfiguracionSemana]
	ADD CONSTRAINT [CK_ConfiguracionSemana_InicioSemana] CHECK (InicioSemana in ('lunes','martes','miercoles','jueves','viernes','sabado','domingo'))
; GO

ALTER TABLE [ConfiguracionSemana]
	ADD CONSTRAINT [CK_ConfiguracionSemana_FinSemana] CHECK (FinSemana in ('lunes','martes','miercoles','jueves','viernes','sabado','domingo'))
; GO

ALTER TABLE [Constantes]
	ADD CONSTRAINT [UQ_Constantes_OrganizacionCodigo] UNIQUE ([CalculoID], [OrganizacionID], [Codigo], [Sexo])
; GO

ALTER TABLE [Contrato]
	ADD CONSTRAINT [UQ_Contrato_ContratoID] UNIQUE ([ContratoID])
; GO

ALTER TABLE [Contrato]
	ADD CONSTRAINT [CK_Contrato_PesoNegociar] CHECK ([PesoNegociar]='Origen' OR [PesoNegociar]='Destino')
; GO

ALTER TABLE [ContratoDetalle]
	ADD CONSTRAINT [UQ_ContratoDetalle_ContratoDetalleID] UNIQUE ([ContratoDetalleID])
; GO

ALTER TABLE [Corral]
	ADD CONSTRAINT [UQ_Corral_CorralID] UNIQUE ([CorralID])
; GO

ALTER TABLE [Corral]
	ADD CONSTRAINT [UQ_Corral_OrganizacionIDCodigo] UNIQUE ([OrganizacionID], [Codigo])
; GO

ALTER TABLE [CorralRango]
	ADD CONSTRAINT [CK_CorralRango_Sexo] CHECK (Sexo IN ('M','H'))
; GO

ALTER TABLE [Costo]
	ADD CONSTRAINT [CK_Costo_AbonoA] CHECK (AbonoA in ('AMBOS','PROVEEDOR','CUENTA'))
; GO

ALTER TABLE [CostoOrganizacion]
	ADD CONSTRAINT [UQ_CostoOrganizacion_TipoOrganizacionCostoID] UNIQUE ([CostoID], [TipoOrganizacionID])
; GO

ALTER TABLE [CostoPromedio]
	ADD CONSTRAINT [UQ_CostoPromedio_OrganizacionCosto] UNIQUE ([OrganizacionID], [CostoID])
; GO

ALTER TABLE [CuentaSAP]
	ADD CONSTRAINT [UQ_CuentaSAP_CuentaSAP] UNIQUE ([CuentaSAP])
; GO

ALTER TABLE [CuentaSAP]
	ADD CONSTRAINT [UQ_CuentaSAP_CuentaSAPID] UNIQUE ([CuentaSAPID])
; GO

ALTER TABLE [CuentaValor]
	ADD CONSTRAINT [UQ_CuentaValor_CuentaOrganizacion] UNIQUE ([CuentaID], [OrganizacionID])
; GO

CREATE INDEX [IXFK_DeteccionAnimal_AnimalMovimiento]
ON [DeteccionAnimal] ([AnimalMovimientoID] ASC)
; GO

ALTER TABLE [EmbarqueDetalle]
	ADD CONSTRAINT [UQ_EmbarqueDetalle_EmbarqueDetalleID] UNIQUE ([EmbarqueDetalleID])
; GO

ALTER TABLE [EntradaCondicion]
	ADD CONSTRAINT [UQ_EntradaCondicion_EntradaCondicionID] UNIQUE ([EntradaCondicionID])
; GO

ALTER TABLE [EntradaDetalle]
	ADD CONSTRAINT [UQ_EntradaDetalle_EntradaDetalleID] UNIQUE ([EntradaDetalleID])
; GO

ALTER TABLE [EntradaGanado]
	ADD CONSTRAINT [UQ_EntradaGanado_EntradaGanadoID] UNIQUE ([EntradaGanadoID])
; GO

CREATE UNIQUE INDEX [UQ_EntraGanado_FolioEntrada]
ON [EntradaGanado] ([OrganizacionID] ASC, [FolioEntrada] ASC)
; GO

ALTER TABLE [EntradaProducto]
	ADD CONSTRAINT [UQ_EntradaProducto_EntradaProductoID] UNIQUE ([EntradaProductoID])
; GO

CREATE INDEX [IXFK_EntradaProducto_TipoContrato]
ON [EntradaProducto] ([TipoContratoID] ASC)
; GO

CREATE INDEX [IXFK_EntradaProductoCosto_Costo]
ON [EntradaProductoCosto] ([CostoID] ASC)
; GO

CREATE INDEX [IXFK_EntradaProductoCosto_EntradaProducto]
ON [EntradaProductoCosto] ([EntradaProductoID] ASC)
; GO

CREATE INDEX [IXFK_EntradaProductoCosto_Proveedor]
ON [EntradaProductoCosto] ([ProveedorID] ASC)
; GO

CREATE INDEX [IXFK_EntradaProductoCosto_UsuarioCreacion]
ON [EntradaProductoCosto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_EntradaProductoCosto_UsuarioModificacion]
ON [EntradaProductoCosto] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [EntradaProductoCosto]
	ADD CONSTRAINT [CK_EstradaProductoCosto_TipoEntrada] CHECK ([TipoEntrada]='Compra' OR [TipoEntrada]='Traspaso')
; GO

ALTER TABLE [EntradaProductoDetalle]
	ADD CONSTRAINT [UQ_EntradaProductoDetalle_EntradaProductoDetalleID] UNIQUE ([EntradaProductoDetalleID])
; GO

ALTER TABLE [EntradaProductoMuestra]
	ADD CONSTRAINT [UQ_EntradaProductoMuestra_EntradaProductoMuestraID] UNIQUE ([EntradaProductoMuestraID])
; GO

ALTER TABLE [EstadoComedero]
	ADD CONSTRAINT [UQ_EstadoComedero_EstadoComederoID] UNIQUE ([EstadoComederoID])
; GO

ALTER TABLE [Estatus]
	ADD CONSTRAINT [UQ_Estatus_EstatusID] UNIQUE ([EstatusID])
; GO

CREATE UNIQUE INDEX [IU_EvaluacionCorral_EvaluacionID]
ON [EvaluacionCorral] ([FolioEvaluacion] ASC, [OrganizacionID] ASC)
; GO

ALTER TABLE [EvaluacionCorral]
	ADD CONSTRAINT [UQ_EvaluacionCorral_LoteID] UNIQUE ([LoteID])
; GO

ALTER TABLE [EvaluacionCorralDetalle]
	ADD CONSTRAINT [UQ_EvaluacionCorralDetalle_EvaluacionCorralDetalleID] UNIQUE ([EvaluacionCorralDetalleID])
; GO

ALTER TABLE [Familia]
	ADD CONSTRAINT [UQ_Familia_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Familia]
	ADD CONSTRAINT [UQ_Familia_FamiliaID] UNIQUE ([FamiliaID])
; GO

CREATE INDEX [IXFK_Flete_Contrato]
ON [Flete] ([ContratoID] ASC)
; GO

CREATE INDEX [IXFK_Flete_Organizacion]
ON [Flete] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Flete_Proveedor]
ON [Flete] ([ProveedorID] ASC)
; GO

CREATE INDEX [IXFK_Flete_UsuarioCreacion]
ON [Flete] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_Flete_UsuarioModificacion]
ON [Flete] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteDetalle_Costo]
ON [FleteDetalle] ([CostoID] ASC)
; GO

CREATE INDEX [IXFK_FleteDetalle_Flete]
ON [FleteDetalle] ([FleteID] ASC)
; GO

CREATE INDEX [IXFK_FleteDetalle_Usuario]
ON [FleteDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteDetalle_Usuario_02]
ON [FleteDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_AlmacenIDDestino]
ON [FleteInterno] ([AlmacenIDDestino] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_AlmacenIDOrigen]
ON [FleteInterno] ([AlmacenIDOrigen] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_Organizacion]
ON [FleteInterno] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_Producto]
ON [FleteInterno] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_TipoMovimiento]
ON [FleteInterno] ([TipoMovimientoID] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_UsuarioCreacion]
ON [FleteInterno] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInterno_UsuarioModificacion]
ON [FleteInterno] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoCosto_Costo]
ON [FleteInternoCosto] ([CostoID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoCosto_FleteInternoDetalle]
ON [FleteInternoCosto] ([FleteInternoDetalleID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoCosto_UsuarioCreacion]
ON [FleteInternoCosto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoCosto_UsuarioModificar]
ON [FleteInternoCosto] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoDetalle_FleteInterno]
ON [FleteInternoDetalle] ([FleteInternoID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoDetalle_Proveedor]
ON [FleteInternoDetalle] ([ProveedorID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoDetalle_UsuarioCreacion]
ON [FleteInternoDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_FleteInternoDetalle_UsuarioModificacion]
ON [FleteInternoDetalle] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [FleteMermaPermitida]
	ADD CONSTRAINT [UQ_FleteMermaPermitida] UNIQUE ([OrganizacionID], [SubFamiliaID])
; GO

ALTER TABLE [Folio]
	ADD CONSTRAINT [UQ_Folio_FolioOrganizacion] UNIQUE ([FolioID], [OrganizacionID])
; GO

ALTER TABLE [FolioAlmacen]
	ADD CONSTRAINT [UQ_FolioAlmacen_AlmacenID] UNIQUE ([AlmacenID], [TipoMovimientoID])
; GO

ALTER TABLE [FolioLoteProducto]
	ADD CONSTRAINT [UQ_FolioLoteProducto_ProductoID_AlmacenID] UNIQUE ([AlmacenID], [ProductoID])
; GO

ALTER TABLE [Formula]
	ADD CONSTRAINT [UQ_Formula_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Formula]
	ADD CONSTRAINT [UQ_Formula_FormulaID] UNIQUE ([FormulaID])
; GO

ALTER TABLE [Formulario]
	ADD CONSTRAINT [UQ_Formulario_FormularioID] UNIQUE ([FormularioID])
; GO

ALTER TABLE [Formulario]
	ADD CONSTRAINT [UQ_Formulario_WinForm_Web] UNIQUE ([WinForm], [Web])
; GO

CREATE INDEX [IXFK_GastoCentroCosto_CentroCosto]
ON [GastoCentroCosto] ([CentroCostoID] ASC)
; GO

CREATE INDEX [IXFK_GastoCentroCosto_Costo]
ON [GastoCentroCosto] ([CostoID] ASC)
; GO

CREATE INDEX [IXFK_GastoCentroCosto_UsuarioCreacion]
ON [GastoCentroCosto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_GastoCentroCosto_UsuarioModificacion]
ON [GastoCentroCosto] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_GastoInventario_Costo]
ON [GastoInventario] ([CostoID] ASC)
; GO

CREATE INDEX [IXFK_GastoInventario_Organizacion]
ON [GastoInventario] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_GastoInventario_UsuarioCreacion]
ON [GastoInventario] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_GastoInventario_UsuarioModificacion]
ON [GastoInventario] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [GastoInventario]
	ADD CONSTRAINT [CK_GastoInventario_TipoGasto] CHECK (TipoGasto IN ('Parcial','Total'))
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_AlmacenInventarioLote]
ON [GastoMateriaPrima] ([AlmacenInventarioLoteID] ASC)
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_Organizacion]
ON [GastoMateriaPrima] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_Producto]
ON [GastoMateriaPrima] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_Proveedor]
ON [GastoMateriaPrima] ([ProveedorID] ASC)
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_TipoMovimiento]
ON [GastoMateriaPrima] ([TipoMovimientoID] ASC)
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_UsuarioCreacion]
ON [GastoMateriaPrima] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_GastoMateriaPrima_UsuarioModificacion]
ON [GastoMateriaPrima] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_Gelatinizacion_Organizacion]
ON [Gelatinizacion] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Gelatinizacion_Producto]
ON [Gelatinizacion] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_Gelatinizacion_UsuarioCreacion]
ON [Gelatinizacion] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_Gelatinizacion_UsuarioModificacion]
ON [Gelatinizacion] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_GelatinizacionDetalle_Gelatinizacion]
ON [GelatinizacionDetalle] ([GelatinizacionID] ASC)
; GO

CREATE INDEX [IXFK_GelatinizacionDetalle_Roladora]
ON [GelatinizacionDetalle] ([RoladoraID] ASC)
; GO

CREATE INDEX [IXFK_GelatinizacionDetalle_UsuarioCreacion]
ON [GelatinizacionDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_GelatinizacionDetalle_UsuarioModificacion]
ON [GelatinizacionDetalle] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Grado]
	ADD CONSTRAINT [CK_Grado_NivelGravedad] CHECK (NivelGravedad IN ('L','G'))
; GO

ALTER TABLE [Grupo]
	ADD CONSTRAINT [UQ_Grupo_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Grupo]
	ADD CONSTRAINT [UQ_Grupo_GrupoID] UNIQUE ([GrupoID])
; GO

ALTER TABLE [Indicador]
	ADD CONSTRAINT [UQ_Indicador_IndicadorID] UNIQUE ([IndicadorID])
; GO

CREATE INDEX [IXFK_IndicadorObjetivo_Indicador]
ON [IndicadorObjetivo] ([IndicadorProductoCalidadID] ASC)
; GO

CREATE INDEX [IXFK_IndicadorObjetivo_Organizacion]
ON [IndicadorObjetivo] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_IndicadorObjetivo_TipoObjetivoCalidad]
ON [IndicadorObjetivo] ([TipoObjetivoCalidadID] ASC)
; GO

CREATE INDEX [IXFK_IndicadorObjetivo_UsuarioCreacion]
ON [IndicadorObjetivo] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_IndicadorObjetivo_UsuarioModificacion]
ON [IndicadorObjetivo] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [IndicadorProducto]
	ADD CONSTRAINT [UQ_IndicadorProducto_IndicadorProductoID] UNIQUE ([IndicadorProductoID])
; GO

CREATE INDEX [IXFK_Ingrediente_Formula]
ON [Ingrediente] ([FormulaID] ASC)
; GO

CREATE INDEX [IXFK_Ingrediente_Organizacion]
ON [Ingrediente] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Ingrediente_Producto]
ON [Ingrediente] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_Ingrediente_Usuario]
ON [Ingrediente] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_Ingrediente_UsuarioCreacion]
ON [Ingrediente] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_InterfaceSalidaTraspaso_Formula]
ON [InterfaceSalidaTraspaso] ([FormulaID] ASC)
; GO

CREATE INDEX [IXFK_InterfaceSalidaTraspaso_Organizacion]
ON [InterfaceSalidaTraspaso] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_InterfaceSalidaTraspaso_OrganizacionIDDestino]
ON [InterfaceSalidaTraspaso] ([OrganizacionIDDestino] ASC)
; GO

CREATE INDEX [IXFK_InterfaceSalidaTraspaso_TipoGanado]
ON [InterfaceSalidaTraspaso] ([TipoGanadoID] ASC)
; GO

CREATE INDEX [IXFK_InterfaceSalidaTraspaso_UsuarioCreacion]
ON [InterfaceSalidaTraspaso] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_InterfaceSalidaTraspaso_UsuarioModificacion]
ON [InterfaceSalidaTraspaso] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Iva]
	ADD CONSTRAINT [UQ_Iva_IvaID] UNIQUE ([IvaID])
; GO

ALTER TABLE [Jaula]
	ADD CONSTRAINT [UQ_Jaula_JaulaID] UNIQUE ([JaulaID])
; GO

ALTER TABLE [LectorRegistro]
	ADD CONSTRAINT [UQ_LectorRegistro_LectorRegistroID] UNIQUE ([LectorRegistroID])
; GO

ALTER TABLE [Liquidacion]
	ADD CONSTRAINT [CK_Liquidacion_Cosecha] CHECK (Cosecha IN ('O-I','P-V'))
; GO

CREATE INDEX [IXFK_Lote_UsuarioModificacion]
ON [Lote] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_Lote_Corral]
ON [Lote] ([CorralID] ASC)
; GO

CREATE INDEX [IXFK_Lote_UsuarioCreacion]
ON [Lote] ([UsuarioCreacionID] ASC)
; GO

ALTER TABLE [Lote]
	ADD CONSTRAINT [UQ_Lote_LoteID] UNIQUE ([LoteID])
; GO

ALTER TABLE [LoteProyeccion]
	ADD CONSTRAINT [UQ_LoteProyeccion_LoteProyeccionID] UNIQUE ([LoteProyeccionID])
; GO

ALTER TABLE [LoteReimplante]
	ADD CONSTRAINT [UQ_LoteReimplante_LoteReimplanteID] UNIQUE ([LoteReimplanteID])
; GO

CREATE INDEX [IXFK_MermasSuperavit_Almacen]
ON [MermaSuperavit] ([AlmacenID] ASC)
; GO

CREATE INDEX [IXFK_MermasSuperavit_Producto]
ON [MermaSuperavit] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_MermasSuperavit_UsuarioCreacion]
ON [MermaSuperavit] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_MermasSuperavit_UsuarioModificacion]
ON [MermaSuperavit] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_Mezcladora_Organizacion]
ON [Mezcladora] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Mezcladora_UsuarioCreacion]
ON [Mezcladora] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_Mezcladora_UsuarioModificacion]
ON [Mezcladora] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Modulo]
	ADD CONSTRAINT [UQ_Modulo_ModuloID] UNIQUE ([ModuloID])
; GO

ALTER TABLE [Modulo]
	ADD CONSTRAINT [UQ_OpcionMenu_Descripcion] UNIQUE ([Descripcion])
; GO

CREATE INDEX [IXFK_MonitoreoSilo_Producto]
ON [MonitoreoSilo] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSilo_Silo]
ON [MonitoreoSilo] ([SiloID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSilo_UsuarioCreacion]
ON [MonitoreoSilo] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSilo_UsuarioModificacion]
ON [MonitoreoSilo] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloDetalle_MonitoreoSilo]
ON [MonitoreoSiloDetalle] ([MonitoreoSiloID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloDetalle_MonitoreoSiloIndicador]
ON [MonitoreoSiloDetalle] ([MonitoreoSiloIndicadorID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloDetalle_UsuarioCreacion]
ON [MonitoreoSiloDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloDetalle_UsuarioModificacion]
ON [MonitoreoSiloDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloIndicador_Organizacion]
ON [MonitoreoSiloIndicador] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloIndicador_UsuarioCreacion]
ON [MonitoreoSiloIndicador] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_MonitoreoSiloIndicador_UsuarioModificacion]
ON [MonitoreoSiloIndicador] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraFormula_Formula]
ON [MuestreoFibraFormula] ([FormulaID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraFormula_Organizacion]
ON [MuestreoFibraFormula] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraFormula_UsuarioCreacion]
ON [MuestreoFibraFormula] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraFormula_UsuarioModificacion]
ON [MuestreoFibraFormula] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraIngrediente_Ingrediente]
ON [MuestreoFibraIngrediente] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraIngrediente_Organizacion]
ON [MuestreoFibraIngrediente] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraIngrediente_UsuarioCreacion]
ON [MuestreoFibraIngrediente] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_MuestreoFibraIngrediente_UsuarioModificacion]
ON [MuestreoFibraIngrediente] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [Observacion]
	ADD CONSTRAINT [UQ_Observacion_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Operador]
	ADD CONSTRAINT [UQ_Operador_OperadorID] UNIQUE ([OperadorID])
; GO

CREATE INDEX [IXFK_Organizacion_Iva]
ON [Organizacion] ([IvaID] ASC)
; GO

ALTER TABLE [Organizacion]
	ADD CONSTRAINT [UQ_Organizacion_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Organizacion]
	ADD CONSTRAINT [UQ_Organizacion_OrganizacionID] UNIQUE ([OrganizacionID])
; GO

ALTER TABLE [Parametro]
	ADD CONSTRAINT [UQ_Parametro_ClaveTipoParametroID] UNIQUE ([Clave], [TipoParametroID])
; GO

ALTER TABLE [ParametroGeneral]
	ADD CONSTRAINT [UQ_ParametroGeneral_ParametroID] UNIQUE ([ParametroID])
; GO

CREATE INDEX [IXFK_Pedido_Almacen]
ON [Pedido] ([AlmacenID] ASC)
; GO

CREATE INDEX [IXFK_Pedido_Estatus]
ON [Pedido] ([EstatusID] ASC)
; GO

CREATE INDEX [IXFK_Pedido_Organizacion]
ON [Pedido] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Pedido_UsuarioModificacion]
ON [Pedido] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_Pedido_UsuarioCreacion]
ON [Pedido] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_PedidoDetalle_AlmacenInventarioLote]
ON [PedidoDetalle] ([InventarioLoteIDDestino] ASC)
; GO

CREATE INDEX [IXFK_PedidoDetalle_Pedido]
ON [PedidoDetalle] ([PedidoID] ASC)
; GO

CREATE INDEX [IXFK_PedidoDetalle_Producto]
ON [PedidoDetalle] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_PedidoDetalle_UsuarioCreacion]
ON [PedidoDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_PedidoDetalle_UsuarioModificacion]
ON [PedidoDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_Camion]
ON [PesajeMateriaPrima] ([CamionID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_Estatus]
ON [PesajeMateriaPrima] ([EstatusID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_ProgramacionMateriaPrima]
ON [PesajeMateriaPrima] ([ProgramacionMateriaPrimaID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_ProveedorChofer]
ON [PesajeMateriaPrima] ([ProveedorChoferID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_TipoPesaje]
ON [PesajeMateriaPrima] ([TipoPesajeID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_UsuarioSurtido]
ON [PesajeMateriaPrima] ([UsuarioIDSurtido] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_UsuarioCreacion]
ON [PesajeMateriaPrima] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_PesajeMateriaPrima_UsuarioModificacion]
ON [PesajeMateriaPrima] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [PrecioGanado]
	ADD CONSTRAINT [UQ_PrecioGanado_OrganizacionTipoGanado] UNIQUE ([OrganizacionID], [TipoGanadoID])
; GO

ALTER TABLE [Pregunta]
	ADD CONSTRAINT [UQ_Pregunta_PreguntaID] UNIQUE ([PreguntaID])
; GO

CREATE INDEX [IXFK_Premezcla_Organizacion]
ON [Premezcla] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Premezcla_Producto]
ON [Premezcla] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_Premezcla_UsuarioCreacion]
ON [Premezcla] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_Premezcla_UsuarioModificar]
ON [Premezcla] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDetalle_Premezcla]
ON [PremezclaDetalle] ([PremezclaID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDetalle_Producto]
ON [PremezclaDetalle] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDetalle_UsuarioCreacion]
ON [PremezclaDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDetalle_UsuarioModificacion]
ON [PremezclaDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucion_Producto]
ON [PremezclaDistribucion] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucion_UsuarioCreacion]
ON [PremezclaDistribucion] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucion_UsuarioModificacion]
ON [PremezclaDistribucion] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucionDetalle_Organizacion]
ON [PremezclaDistribucionDetalle] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucionDetalle_PremezclaDistribucion]
ON [PremezclaDistribucionDetalle] ([PremezclaDistribucionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucionDetalle_UsuarioCreacion]
ON [PremezclaDistribucionDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_PremezclaDistribucionDetalle_UsuarioModificacion]
ON [PremezclaDistribucionDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiaria_UsuarioAutorizo]
ON [ProduccionDiaria] ([UsuarioIDAutorizo] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiaria_UsuarioCreacion]
ON [ProduccionDiaria] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiaria_UsuarioModificaciokn]
ON [ProduccionDiaria] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [ProduccionDiaria]
	ADD CONSTRAINT [CK_ProduccionDiaria_Turno] CHECK (Turno IN (1,2,3))
; GO

ALTER TABLE [ProduccionDiaria]
	ADD CONSTRAINT [UQ_ProduccionDiaria_Turno_Fecha] UNIQUE ([FechaProduccion], [Turno])
; GO

CREATE INDEX [IXFK_ProduccionDiariaDetalle_PesajeMateriaPrima]
ON [ProduccionDiariaDetalle] ([PesajeMateriaPrimaID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiariaDetalle_ProduccionDiaria]
ON [ProduccionDiariaDetalle] ([ProduccionDiariaID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiariaDetalle_Producto]
ON [ProduccionDiariaDetalle] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiariaDetalle_UsuarioCreacion]
ON [ProduccionDiariaDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionDiariaDetalle_UsuarioModificacion]
ON [ProduccionDiariaDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormula_UsuarioCreacion]
ON [ProduccionFormula] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormula_UsuarioModificacion]
ON [ProduccionFormula] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormula_Formula]
ON [ProduccionFormula] ([FormulaID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormula_Organizacion]
ON [ProduccionFormula] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormulaDetalle_UsuarioCreacion]
ON [ProduccionFormulaDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormulaDetalle_UsuarioModificacion]
ON [ProduccionFormulaDetalle] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormulaDetalle_Ingrediente]
ON [ProduccionFormulaDetalle] ([IngredienteID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormulaDetalle_Producto]
ON [ProduccionFormulaDetalle] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_ProduccionFormulaDetalle_ProgramacionFormula]
ON [ProduccionFormulaDetalle] ([ProduccionFormulaID] ASC)
; GO

ALTER TABLE [ProgramacionCorte]
	ADD CONSTRAINT [UQ_ProgramacionCorte_FolioProgramacionID] UNIQUE ([FolioProgramacionID])
; GO

CREATE INDEX [IXFK_ProgramacionMateriaPrima_Almacen]
ON [ProgramacionMateriaPrima] ([AlmacenID] ASC)
; GO

CREATE INDEX [IXFK_ProgramacionMateriaPrima_AlmacenInventarioLote]
ON [ProgramacionMateriaPrima] ([InventarioLoteIDOrigen] ASC)
; GO

CREATE INDEX [IXFK_ProgramacionMateriaPrima_Organizacion]
ON [ProgramacionMateriaPrima] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_ProgramacionMateriaPrima_PedidoDetalle]
ON [ProgramacionMateriaPrima] ([PedidoDetalleID] ASC)
; GO

CREATE INDEX [IXFK_ProgramacionMateriaPrima_UsuarioCreacion]
ON [ProgramacionMateriaPrima] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_ProgramacionMateriaPrima_UsuarioModificacion]
ON [ProgramacionMateriaPrima] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [ProgramacionReimplante]
	ADD CONSTRAINT [UQ_ProgramacionReimplante_FolioProgramacionID] UNIQUE ([FolioProgramacionID])
; GO

ALTER TABLE [Proveedor]
	ADD CONSTRAINT [UQ_Proveedor_CodigoSAP] UNIQUE ([CodigoSAP])
; GO

ALTER TABLE [Proveedor]
	ADD CONSTRAINT [UQ_Proveedor_ProveedorID] UNIQUE ([ProveedorID])
; GO

ALTER TABLE [ProveedorAlmacen]
	ADD CONSTRAINT [UQ_ProveedorAlmacen_ProveedorID] UNIQUE ([ProveedorID])
; GO

CREATE INDEX [IXFK_ProveedorChofer_Chofer]
ON [ProveedorChofer] ([ChoferID] ASC)
; GO

CREATE INDEX [IXFK_ProveedorChofer_Proveedor]
ON [ProveedorChofer] ([ProveedorChoferID] ASC)
; GO

ALTER TABLE [RegistroVigilancia]
	ADD CONSTRAINT [UQ_RegistroVigilancia_RegistroVigilanciaID] UNIQUE ([RegistroVigilanciaID])
; GO

CREATE INDEX [IXFK_RegistroVigilancia_Producto]
ON [RegistroVigilancia] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_RegistroVigilancia_ProveedorChofer]
ON [RegistroVigilancia] ([ProveedorChoferID] ASC)
; GO

ALTER TABLE [Reparto]
	ADD CONSTRAINT [UQ_Reparto_Corral] UNIQUE ([CorralID], [Fecha])
; GO

CREATE INDEX [IXFK_RepartoAlimento_CamionReparto]
ON [RepartoAlimento] ([CamionRepartoID] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimento_TipoServicio]
ON [RepartoAlimento] ([TipoServicioID] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimento_UsuarioCreacion]
ON [RepartoAlimento] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimento_UsuarioModificacion]
ON [RepartoAlimento] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimento_UsuarioReparto]
ON [RepartoAlimento] ([UsuarioIDReparto] ASC)
; GO

ALTER TABLE [RepartoAlimento]
	ADD CONSTRAINT [UQ_RepartoAlimento_Fecha_Turno_Usuario] UNIQUE ([FechaReparto], [TipoServicioID], [UsuarioIDReparto], [CamionRepartoID])
; GO

CREATE INDEX [IXFK_RepartoAlimentoDetalle_CorralFinal]
ON [RepartoAlimentoDetalle] ([CorralIDFinal] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimentoDetalle_CorralInicio]
ON [RepartoAlimentoDetalle] ([CorralIDInicio] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimentoDetalle_Formula]
ON [RepartoAlimentoDetalle] ([FormulaIDRacion] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimentoDetalle_RepartoAlimento]
ON [RepartoAlimentoDetalle] ([RepartoAlimentoID] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimentoDetalle_UsuarioCreacion]
ON [RepartoAlimentoDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_RepartoAlimentoDetalle_UsuarioMOdificacion]
ON [RepartoAlimentoDetalle] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [RepartoDetalle]
	ADD CONSTRAINT [UQ_RepartoDetalle_RepartoID_TipoServicioID] UNIQUE ([RepartoID], [TipoServicioID])
; GO

ALTER TABLE [Retencion]
	ADD CONSTRAINT [UQ_Retencion_RetencionID] UNIQUE ([RetencionID])
; GO

ALTER TABLE [Rol]
	ADD CONSTRAINT [UQ_Rol_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [Rol]
	ADD CONSTRAINT [UQ_Rol_RolID] UNIQUE ([RolID])
; GO

CREATE INDEX [IXFK_Roladora_Organizacion]
ON [Roladora] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Roladora_UsuarioCreacion]
ON [Roladora] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_Roladora_UsuarioModificacion]
ON [Roladora] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_AlmacenMovimiento]
ON [SalidaProducto] ([AlmacenMovimientoID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_Camion]
ON [SalidaProducto] ([CamionID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_Chofer]
ON [SalidaProducto] ([ChoferID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_Almacen]
ON [SalidaProducto] ([AlmacenID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_AlmacenInventarioLote]
ON [SalidaProducto] ([AlmacenInventarioLoteID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_Cliente]
ON [SalidaProducto] ([ClienteID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_CuentaSAP]
ON [SalidaProducto] ([CuentaSAPID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_Organizacion]
ON [SalidaProducto] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_OrganizacionDestino]
ON [SalidaProducto] ([OrganizacionIDDestino ] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_TipoMovimiento]
ON [SalidaProducto] ([TipoMovimientoID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_UsuarioCreacion]
ON [SalidaProducto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_SalidaProducto_UsuarioModificacion]
ON [SalidaProducto] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [ServicioAlimento]
	ADD CONSTRAINT [UQ_ServicioAlimento_ServicioID] UNIQUE ([ServicioID])
; GO

CREATE INDEX [IXFK_Silo_Organizacion]
ON [Silo] ([OrganizacionID] ASC)
; GO

CREATE INDEX [IXFK_Silo_UsuarioCreacion]
ON [Silo] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_Silo_UsuarioModificacion]
ON [Silo] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProducto_Estatus]
ON [SolicitudProducto] ([EstatusID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProducto_UsuarioAutoriza]
ON [SolicitudProducto] ([UsuarioIDAutoriza] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProducto_UsuarioCreacion]
ON [SolicitudProducto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProducto_UsuarioModificacion]
ON [SolicitudProducto] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProducto_UsuarioSolicita]
ON [SolicitudProducto] ([UsuarioIDSolicita] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProductoDetalle_Estatus]
ON [SolicitudProductoDetalle] ([EstatusID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProductoDetalle_Producto]
ON [SolicitudProductoDetalle] ([ProductoID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProductoDetalle_SolicitudProducto]
ON [SolicitudProductoDetalle] ([SolicitudProductoID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProductoDetalle_UsuarioCreacion]
ON [SolicitudProductoDetalle] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_SolicitudProductoDetalle_UsuarioModificacion]
ON [SolicitudProductoDetalle] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [SubFamilia]
	ADD CONSTRAINT [UQ_SubFamilia_SubFamiliaID] UNIQUE ([SubFamiliaID])
; GO

CREATE INDEX [IXFK_TiempoMuerto_CausaTiempoMuerto]
ON [TiempoMuerto] ([CausaTiempoMuertoID] ASC)
; GO

CREATE INDEX [IXFK_TiempoMuerto_ProduccionDiaria]
ON [TiempoMuerto] ([ProduccionDiariaID] ASC)
; GO

CREATE INDEX [IXFK_TiempoMuerto_RepartoAlimento]
ON [TiempoMuerto] ([RepartoAlimentoID] ASC)
; GO

CREATE INDEX [IXFK_TiempoMuerto_UsuarioCreacion]
ON [TiempoMuerto] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_TiempoMuerto_UsuarioModificacion]
ON [TiempoMuerto] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [TipoAlmacen]
	ADD CONSTRAINT [UQ_TipoAlmacen_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [TipoContrato]
	ADD CONSTRAINT [UQ_TipoContrato_TipoContratoID] UNIQUE ([TipoContratoID])
; GO

ALTER TABLE [TipoCorral]
	ADD CONSTRAINT [UQ_TipoCorral_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [TipoCorral]
	ADD CONSTRAINT [UQ_TipoCorral_TipoCorralID] UNIQUE ([TipoCorralID])
; GO

ALTER TABLE [TipoCuenta]
	ADD CONSTRAINT [UQ_TipoCuenta_Descripcion] UNIQUE ([Descripcion])
; GO

CREATE INDEX [IXFK_TipoEstatus_UsuarioModificacion]
ON [TipoEstatus] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [TipoEstatus]
	ADD CONSTRAINT [UQ_TipoEstatus_TipoEstatusID] UNIQUE ([TipoEstatusID])
; GO

ALTER TABLE [TipoEstatus]
	ADD CONSTRAINT [UQ_TipoEstatus_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [TipoFlete]
	ADD CONSTRAINT [UQ_TipoFlete_TipoFleteID] UNIQUE ([TipoFleteID])
; GO

ALTER TABLE [TipoFormula]
	ADD CONSTRAINT [UQ_TipoFormula_TipoFormulaID] UNIQUE ([TipoFormulaID])
; GO

ALTER TABLE [TipoGanado]
	ADD CONSTRAINT [UQ_TipoGanado_TipoGanadoID] UNIQUE ([TipoGanadoID])
; GO

ALTER TABLE [TipoGanado]
	ADD CONSTRAINT [CK_TipoAnimal_Sexo] CHECK (Sexo IN ('M','H'))
; GO

ALTER TABLE [TipoGanadoCorrales]
	ADD CONSTRAINT [UQ_TipoGanadoCorrales_TipoGanadoCorralID] UNIQUE ([TipoGanadoCorralID])
; GO

ALTER TABLE [TipoGanadoCorrales]
	ADD CONSTRAINT [UQ_TipoGanadoCorrales_RangoInicial] UNIQUE ([TipoGanadoID], [RangoInicial])
; GO

ALTER TABLE [TipoGanadoCorrales]
	ADD CONSTRAINT [UQ_TipoGanadoCorrales_RangoFinal] UNIQUE ([RangoFinal], [TipoGanadoID])
; GO

ALTER TABLE [TipoGanadoCorrales]
	ADD CONSTRAINT [CK_TipoGanadoCorrales_RangoInicial] CHECK (RangoInicial > 0
)
; GO

ALTER TABLE [TipoGanadoCorrales]
	ADD CONSTRAINT [CK_TipoGanadoCorrales_RangoFinal] CHECK (RangoFinal > 0)
; GO

CREATE INDEX [IXFK_TipoLugarMuestra_UsuarioCreacion]
ON [TipoLugarMuestra] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_TipoLugarMuestra_UsuarioModificacion]
ON [TipoLugarMuestra] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [TipoMovimiento]
	ADD CONSTRAINT [UQ_TipoMovimiento_TipoMovimientoID] UNIQUE ([TipoMovimientoID])
; GO

CREATE INDEX [IXFK_TipoMuestra_UsuarioCreacion]
ON [TipoMuestra] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_TipoMuestra_UsuarioModificacion]
ON [TipoMuestra] ([UsuarioModificacionID] ASC)
; GO

CREATE INDEX [IXFK_TipoObjetivoCalidad_UsuarioCreacion]
ON [TipoObjetivoCalidad] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_TipoObjetivoCalidad_UsuarioModificacion]
ON [TipoObjetivoCalidad] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [TipoObservacion]
	ADD CONSTRAINT [UQ_TipoObservacion_Descripcion] UNIQUE ([Descripcion])
; GO

ALTER TABLE [TipoOrganizacion]
	ADD CONSTRAINT [UQ_TipoOrganizacion_TipoOrganizacionID] UNIQUE ([TipoOrganizacionID])
; GO

CREATE INDEX [IXFK_TipoPesaje_UsuarioCreacion]
ON [TipoPesaje] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_TipoPesaje_UsuarioModificacion]
ON [TipoPesaje] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [TipoProceso]
	ADD CONSTRAINT [UQ_TipoProceso_TipoProcesoID] UNIQUE ([TipoProcesoID])
; GO

ALTER TABLE [TipoProveedor]
	ADD CONSTRAINT [UQ_TipoProveedor_Descripcion] UNIQUE ([Descripcion])
; GO

CREATE INDEX [IXFK_TipoTecnica_UsuarioCreacion]
ON [TipoTecnica] ([UsuarioCreacionID] ASC)
; GO

CREATE INDEX [IXFK_TipoTecnica_UsuarioModificacion]
ON [TipoTecnica] ([UsuarioModificacionID] ASC)
; GO

ALTER TABLE [TipoTratamiento]
	ADD CONSTRAINT [UQ_TipoTratamiento_TipoTratamientoID] UNIQUE ([TipoTratamientoID])
; GO

ALTER TABLE [Trampa]
	ADD CONSTRAINT [UQ_Trampa_TrampaID] UNIQUE ([TrampaID])
; GO

ALTER TABLE [Trampa]
	ADD CONSTRAINT [CK_Trampa_TipoTrampa] CHECK (TipoTrampa IN ('M','E'))
; GO

ALTER TABLE [Tratamiento]
	ADD CONSTRAINT [UQ_Tratamiento_TratamientoID] UNIQUE ([TratamientoID])
; GO

ALTER TABLE [Tratamiento]
	ADD CONSTRAINT [CK_Tratamiento_Sexo] CHECK (Sexo IN ('M','H'))
; GO

ALTER TABLE [TratamientoProducto]
	ADD CONSTRAINT [UQ_TratamientoProducto_TratamientoProductoID] UNIQUE ([TratamientoProductoID])
; GO

ALTER TABLE [UnidadMedicion]
	ADD CONSTRAINT [UQ_UnidadMedicion_UnidadID] UNIQUE ([UnidadID])
; GO

ALTER TABLE [Usuario]
	ADD CONSTRAINT [UQ_Usuario_UsuarioActiveDirectory] UNIQUE ([UsuarioActiveDirectory])
; GO

ALTER TABLE [Usuario]
	ADD CONSTRAINT [UQ_Usuario_UsuarioID] UNIQUE ([UsuarioID])
; GO

ALTER TABLE [Acceso] ADD CONSTRAINT [PK_Acceso] 
	PRIMARY KEY CLUSTERED ([AccesoID])
; GO

ALTER TABLE [AccionesSIAP] ADD CONSTRAINT [PK_AccionesSIAP] 
	PRIMARY KEY CLUSTERED ([AccionesSiapID])
; GO

ALTER TABLE [Almacen] ADD CONSTRAINT [PK_Almacen] 
	PRIMARY KEY CLUSTERED ([AlmacenID])
; GO

ALTER TABLE [AlmacenInventario] ADD CONSTRAINT [PK_AlmacenInventario] 
	PRIMARY KEY CLUSTERED ([AlmacenInventarioID])
; GO

ALTER TABLE [AlmacenInventarioLote] ADD CONSTRAINT [PK_AlmacenInventarioLote] 
	PRIMARY KEY CLUSTERED ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [AlmacenMovimiento] ADD CONSTRAINT [PK_AlmacenMovimientos] 
	PRIMARY KEY CLUSTERED ([AlmacenMovimientoID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [PK_AlmacenMovimientoCosto] 
	PRIMARY KEY CLUSTERED ([AlmacenMovimientoCostoID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [PK_AlmacenMovimientoDetalle] 
	PRIMARY KEY CLUSTERED ([AlmacenMovimientoDetalleID])
; GO

ALTER TABLE [AlmacenUsuario] ADD CONSTRAINT [PK_AlmacenUsuario] 
	PRIMARY KEY CLUSTERED ([AlmacenUsuarioID])
; GO

ALTER TABLE [AlmidonFactor] ADD CONSTRAINT [PK_AlmidonFactor] 
	PRIMARY KEY CLUSTERED ([AlmidonFactorID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [PK_AlmidonHeces] 
	PRIMARY KEY CLUSTERED ([AlmidonHecesID])
; GO

ALTER TABLE [AlmidonHecesDetalle] ADD CONSTRAINT [PK_AlmidonHecesDetalle] 
	PRIMARY KEY CLUSTERED ([AlmidonHecesDetalleID])
; GO

ALTER TABLE [AnalisisGrasa] ADD CONSTRAINT [PK_AnalisisGrasa] 
	PRIMARY KEY CLUSTERED ([AnalisisGrasaID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [PK_Animal] 
	PRIMARY KEY CLUSTERED ([AnimalID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [PK_AnimalConsumo] 
	PRIMARY KEY CLUSTERED ([AnimalConsumoID])
; GO

ALTER TABLE [AnimalCosto] ADD CONSTRAINT [PK_AnimalCosto] 
	PRIMARY KEY CLUSTERED ([AnimalCostoID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [PK_AnimalMovimiento] 
	PRIMARY KEY NONCLUSTERED ([AnimalID], [AnimalMovimientoID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [PK_AnimalSalida] 
	PRIMARY KEY CLUSTERED ([AnimalSalidaID])
; GO

ALTER TABLE [AreaRevision] ADD CONSTRAINT [PK_AreaRevision] 
	PRIMARY KEY CLUSTERED ([AreaRevisionID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [PK_AutorizacionMateriaPrima] 
	PRIMARY KEY CLUSTERED ([AutorizacionMateriaPrimaID])
; GO

ALTER TABLE [BitacoraErrores] ADD CONSTRAINT [PK_BitacoraErrores] 
	PRIMARY KEY CLUSTERED ([BitacoraErroresID])
; GO

ALTER TABLE [Calculo] ADD CONSTRAINT [PK_Calculo] 
	PRIMARY KEY CLUSTERED ([CalculoID])
; GO

ALTER TABLE [CalidadGanado] ADD CONSTRAINT [PK_CalidadGanado] 
	PRIMARY KEY CLUSTERED ([CalidadGanadoID])
; GO

ALTER TABLE [CalidadMateriaPrima] ADD CONSTRAINT [PK_CalidadMateriaPrima] 
	PRIMARY KEY CLUSTERED ([CalidadMateriaPrimaID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [PK_CalidadMezclado] 
	PRIMARY KEY CLUSTERED ([CalidadMezcladoID])
; GO

ALTER TABLE [CalidadMezcladoDetalle] ADD CONSTRAINT [PK_CalidadMezcladoDetalle] 
	PRIMARY KEY CLUSTERED ([CalidadMezcladoDetalleID])
; GO

ALTER TABLE [CalidadMezcladoFactor] ADD CONSTRAINT [PK_CalidadMezcladoFactor] 
	PRIMARY KEY CLUSTERED ([CalidadMezcladoFactorID])
; GO

ALTER TABLE [Camion] ADD CONSTRAINT [PK_PlacaCamion] 
	PRIMARY KEY CLUSTERED ([CamionID])
; GO

ALTER TABLE [CamionReparto] ADD CONSTRAINT [PK_CamionReparto] 
	PRIMARY KEY CLUSTERED ([CamionRepartoID])
; GO

ALTER TABLE [CancelacionMovimiento] ADD CONSTRAINT [PK_CancelacionMovimiento] 
	PRIMARY KEY CLUSTERED ([CancelacionMovimientoID])
; GO

ALTER TABLE [CausaPrecio] ADD CONSTRAINT [PK_CausaPrecio] 
	PRIMARY KEY CLUSTERED ([CausaPrecioID])
; GO

ALTER TABLE [CausaRechazo] ADD CONSTRAINT [PK_CausaRechazo] 
	PRIMARY KEY CLUSTERED ([CausaRechazoID])
; GO

ALTER TABLE [CausaSalida] ADD CONSTRAINT [PK_CausaSalida] 
	PRIMARY KEY CLUSTERED ([CausaSalidaID])
; GO

ALTER TABLE [CausaTiempoMuerto] ADD CONSTRAINT [PK_CausaTiempoMuerto] 
	PRIMARY KEY CLUSTERED ([CausaTiempoMuertoID])
; GO

ALTER TABLE [CentroBeneficio] ADD CONSTRAINT [PK_CentroBeneficio] 
	PRIMARY KEY CLUSTERED ([CentroBeneficioID])
; GO

ALTER TABLE [CentroCosto] ADD CONSTRAINT [PK_CentroCosto] 
	PRIMARY KEY CLUSTERED ([CentroCostoID])
; GO

ALTER TABLE [CentroCostoUsuario] ADD CONSTRAINT [PK_CentroCostoUsuario] 
	PRIMARY KEY CLUSTERED ([CentroCostoUsuarioID])
; GO

ALTER TABLE [CheckListCorral] ADD CONSTRAINT [PK_CheckListCorral] 
	PRIMARY KEY CLUSTERED ([CheckListCorralID])
; GO

ALTER TABLE [CheckListRoladora] ADD CONSTRAINT [PK_CheckListRoladora] 
	PRIMARY KEY CLUSTERED ([CheckListRoladoraID])
; GO

ALTER TABLE [CheckListRoladoraAccion] ADD CONSTRAINT [PK_CheckListRoladoraAccion] 
	PRIMARY KEY CLUSTERED ([CheckListRoladoraAccionID])
; GO

ALTER TABLE [CheckListRoladoraDetalle] ADD CONSTRAINT [PK_CheckListRoladoraDetalle] 
	PRIMARY KEY CLUSTERED ([CheckListRoladoraDetalleID])
; GO

ALTER TABLE [CheckListRoladoraGeneral] ADD CONSTRAINT [PK_CheckListRoladoraGeneral] 
	PRIMARY KEY CLUSTERED ([CheckListRoladoraGeneralID])
; GO

ALTER TABLE [CheckListRoladoraHorometro] ADD CONSTRAINT [PK_CheckListRoladoraHorometro] 
	PRIMARY KEY CLUSTERED ([CheckListRoladoraHorometroID])
; GO

ALTER TABLE [CheckListRoladoraRango] ADD CONSTRAINT [PK_CheckListRoladoraRango] 
	PRIMARY KEY CLUSTERED ([CheckListRoladoraRangoID])
; GO

ALTER TABLE [Chofer] ADD CONSTRAINT [PK_Chofer] 
	PRIMARY KEY CLUSTERED ([ChoferID])
; GO

ALTER TABLE [ClaseCostoProducto] ADD CONSTRAINT [PK_ClaseCostoProducto] 
	PRIMARY KEY CLUSTERED ([ClaseCostoProductoID])
; GO

ALTER TABLE [ClasificacionGanado] ADD CONSTRAINT [PK_ClasificacionGanado] 
	PRIMARY KEY CLUSTERED ([ClasificacionGanadoID])
; GO

ALTER TABLE [Cliente] ADD CONSTRAINT [PK_Cliente] 
	PRIMARY KEY CLUSTERED ([ClienteID])
; GO

ALTER TABLE [ColorObjetivo] ADD CONSTRAINT [PK_ColorObjetivo] 
	PRIMARY KEY CLUSTERED ([ColorObjetivoID])
; GO

ALTER TABLE [ConceptoDeteccion] ADD CONSTRAINT [PK_ConceptoDeteccion] 
	PRIMARY KEY CLUSTERED ([ConceptoDeteccionID])
; GO

ALTER TABLE [Condicion] ADD CONSTRAINT [PK_CondicionesGanado] 
	PRIMARY KEY CLUSTERED ([CondicionID])
; GO

ALTER TABLE [CondicionJaula] ADD CONSTRAINT [PK_CondicionJaula] 
	PRIMARY KEY CLUSTERED ([CondicionJaulaID])
; GO

ALTER TABLE [ConfiguracionEmbarque] ADD CONSTRAINT [PK_ConfiguracionEmbarques] 
	PRIMARY KEY CLUSTERED ([ConfiguracionEmbarqueID])
; GO

ALTER TABLE [ConfiguracionFormula] ADD CONSTRAINT [PK_ConfiguracionFormula] 
	PRIMARY KEY CLUSTERED ([ConfiguracionFormulaID])
; GO

ALTER TABLE [ConfiguracionImplante] ADD CONSTRAINT [PK_ConfiguracionImplante] 
	PRIMARY KEY CLUSTERED ([ConfiguracionImplanteID])
; GO

ALTER TABLE [ConfiguracionSemana] ADD CONSTRAINT [PK_ConfiguracionSemana] 
	PRIMARY KEY CLUSTERED ([ConfiguracionSemanaID])
; GO

ALTER TABLE [ConfiguracionTraspasoAlmacen] ADD CONSTRAINT [PK_ConfiguracionTraspasoAlmacen] 
	PRIMARY KEY CLUSTERED ([ConfiguracionTraspasoAlmacenID])
; GO

ALTER TABLE [Constantes] ADD CONSTRAINT [PK_Constantes] 
	PRIMARY KEY CLUSTERED ([CalculoID], [ConstanteID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [PK_Contrato] 
	PRIMARY KEY CLUSTERED ([ContratoID])
; GO

ALTER TABLE [ContratoDetalle] ADD CONSTRAINT [PK_ContratoDetalle] 
	PRIMARY KEY CLUSTERED ([ContratoDetalleID])
; GO

ALTER TABLE [ContratoHumedad] ADD CONSTRAINT [PK_ContratoHumedad] 
	PRIMARY KEY CLUSTERED ([ContratoHumedadID])
; GO

ALTER TABLE [ContratoParcial] ADD CONSTRAINT [PK_ContratoParcial] 
	PRIMARY KEY CLUSTERED ([ContratoParcialID])
; GO

ALTER TABLE [Corral] ADD CONSTRAINT [PK_Corral] 
	PRIMARY KEY CLUSTERED ([CorralID])
; GO

ALTER TABLE [CorralDetector] ADD CONSTRAINT [PK_CorralDetector] 
	PRIMARY KEY CLUSTERED ([CorralDetectorID])
; GO

ALTER TABLE [CorralLector] ADD CONSTRAINT [PK_CorralLector] 
	PRIMARY KEY CLUSTERED ([CorralLectorID])
; GO

ALTER TABLE [CorralRango] ADD CONSTRAINT [PK_CorralRango] 
	PRIMARY KEY CLUSTERED ([OrganizacionID], [CorralID])
; GO

ALTER TABLE [Costo] ADD CONSTRAINT [PK_Costo] 
	PRIMARY KEY CLUSTERED ([CostoID])
; GO

ALTER TABLE [CostoEmbarqueDetalle] ADD CONSTRAINT [PK_CostoEmbarqueDetalle] 
	PRIMARY KEY CLUSTERED ([CostoEmbarqueDetalleID])
; GO

ALTER TABLE [CostoOrganizacion] ADD CONSTRAINT [PK_CostoOrganizacion] 
	PRIMARY KEY CLUSTERED ([CostoOrganizacionID])
; GO

ALTER TABLE [CostoPromedio] ADD CONSTRAINT [PK_CostoPromedio] 
	PRIMARY KEY CLUSTERED ([CostoPromedioID])
; GO

ALTER TABLE [CriterioSupervision] ADD CONSTRAINT [PK_CriterioSupervision] 
	PRIMARY KEY CLUSTERED ([CriterioSupervisionID])
; GO

ALTER TABLE [Cuenta] ADD CONSTRAINT [PK_Cuenta] 
	PRIMARY KEY CLUSTERED ([CuentaID])
; GO

ALTER TABLE [CuentaAlmacenSubFamilia] ADD CONSTRAINT [PK_CuentaAlmacenSubFamilia] 
	PRIMARY KEY CLUSTERED ([CuentaAlmacenSubFamiliaID])
; GO

ALTER TABLE [CuentaSAP] ADD CONSTRAINT [PK_CuentaSAP] 
	PRIMARY KEY CLUSTERED ([CuentaSAPID])
; GO

ALTER TABLE [CuentaValor] ADD CONSTRAINT [PK_CuentaValor] 
	PRIMARY KEY CLUSTERED ([CuentaValorID])
; GO

ALTER TABLE [DescripcionGanado] ADD CONSTRAINT [PK_DescripcionGanado] 
	PRIMARY KEY CLUSTERED ([DescripcionGanadoID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [PK_Deteccion] 
	PRIMARY KEY CLUSTERED ([DeteccionID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [PK_DeteccionAnimal] 
	PRIMARY KEY CLUSTERED ([DeteccionAnimalID])
; GO

ALTER TABLE [DeteccionSintoma] ADD CONSTRAINT [PK_DeteccionSintoma] 
	PRIMARY KEY CLUSTERED ([DeteccionSintomaID])
; GO

ALTER TABLE [DeteccionSintomaAnimal] ADD CONSTRAINT [PK_DeteccionSintomaAnimal] 
	PRIMARY KEY CLUSTERED ([DeteccionAnimalSintomaID])
; GO

ALTER TABLE [DiagnosticoAnalista] ADD CONSTRAINT [PK_DiagnosticoAnalista] 
	PRIMARY KEY CLUSTERED ([DiagnosticoAnalistaID])
; GO

ALTER TABLE [DiagnosticoAnalistaDetalle] ADD CONSTRAINT [PK_DeteccionAnalistaDetalle] 
	PRIMARY KEY CLUSTERED ([DiagnosticoAnalistaDetalleID])
; GO

ALTER TABLE [Embarque] ADD CONSTRAINT [PK_Embarque] 
	PRIMARY KEY CLUSTERED ([EmbarqueID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [PK_EmbarqueDetalle] 
	PRIMARY KEY CLUSTERED ([EmbarqueDetalleID])
; GO

ALTER TABLE [Enfermeria] ADD CONSTRAINT [PK_Enfermeria] 
	PRIMARY KEY CLUSTERED ([EnfermeriaID])
; GO

ALTER TABLE [EnfermeriaCorral] ADD CONSTRAINT [PK_EnfermeriaCorral] 
	PRIMARY KEY CLUSTERED ([EnfermeriaCorralID])
; GO

ALTER TABLE [EntradaCondicion] ADD CONSTRAINT [PK_EntradaCondicion] 
	PRIMARY KEY CLUSTERED ([EntradaCondicionID])
; GO

ALTER TABLE [EntradaDetalle] ADD CONSTRAINT [PK_EntradaGanadoDetalle] 
	PRIMARY KEY CLUSTERED ([EntradaDetalleID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [PK_EntradaGanado] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoID])
; GO

ALTER TABLE [EntradaGanadoCalidad] ADD CONSTRAINT [PK_EntradaGanadoCalidad] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoCalidadID])
; GO

ALTER TABLE [EntradaGanadoCosteo] ADD CONSTRAINT [PK_EntradaGanadoCosteo] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoCosteoID])
; GO

ALTER TABLE [EntradaGanadoCosto] ADD CONSTRAINT [PK_EntradaGanadoCostoEntrada] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoCostoID])
; GO

ALTER TABLE [EntradaGanadoMuerte] ADD CONSTRAINT [PK_EntradaGanadoMuerte] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoMuerteID])
; GO

ALTER TABLE [EntradaGanadoSobrante] ADD CONSTRAINT [PK_EntradaGanadoSobrante] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoSobranteID])
; GO

ALTER TABLE [EntradaGanadoTransito] ADD CONSTRAINT [PK_EntradaGanadoTransito] 
	PRIMARY KEY CLUSTERED ([EntradaGanadoTransitoID])
; GO

ALTER TABLE [EntradaPremezcla] ADD CONSTRAINT [PK_AlmacenMovimientoSalida] 
	PRIMARY KEY CLUSTERED ([EntradaPremezclaID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [PK_EntradaProducto] 
	PRIMARY KEY CLUSTERED ([EntradaProductoID])
; GO

ALTER TABLE [EntradaProductoCosto] ADD CONSTRAINT [PK_EntradaProductoCosto] 
	PRIMARY KEY CLUSTERED ([EntradaProductoCostoID])
; GO

ALTER TABLE [EntradaProductoDetalle] ADD CONSTRAINT [PK_EntradaProductoDetalle] 
	PRIMARY KEY CLUSTERED ([EntradaProductoDetalleID])
; GO

ALTER TABLE [EntradaProductoMuestra] ADD CONSTRAINT [PK_EntradaProductoMuestra] 
	PRIMARY KEY CLUSTERED ([EntradaProductoMuestraID])
; GO

ALTER TABLE [EntradaProductoParcial] ADD CONSTRAINT [PK_EntradaProductoParcial] 
	PRIMARY KEY CLUSTERED ([EntradaProductoParcialID])
; GO

ALTER TABLE [EstadoComedero] ADD CONSTRAINT [PK_EstadoComedero] 
	PRIMARY KEY CLUSTERED ([EstadoComederoID])
; GO

ALTER TABLE [EstadoImplante] ADD CONSTRAINT [PK_EstadoImplante] 
	PRIMARY KEY CLUSTERED ([EstadoImplanteID])
; GO

ALTER TABLE [EstandarDescarga] ADD CONSTRAINT [PK_EstandarDescarga] 
	PRIMARY KEY CLUSTERED ([EstandarDescargaID])
; GO

ALTER TABLE [Estatus] ADD CONSTRAINT [PK_Estatus] 
	PRIMARY KEY CLUSTERED ([EstatusID])
; GO

ALTER TABLE [EvaluacionCorral] ADD CONSTRAINT [PK_EvaluacionCorral] 
	PRIMARY KEY CLUSTERED ([EvaluacionID])
; GO

ALTER TABLE [EvaluacionCorralDetalle] ADD CONSTRAINT [PK_EvaluacionCorralDetalle] 
	PRIMARY KEY CLUSTERED ([EvaluacionCorralDetalleID])
; GO

ALTER TABLE [Familia] ADD CONSTRAINT [PK_Familia] 
	PRIMARY KEY CLUSTERED ([FamiliaID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [PK_Flete] 
	PRIMARY KEY CLUSTERED ([FleteID])
; GO

ALTER TABLE [FleteDetalle] ADD CONSTRAINT [PK_FleteDetalle] 
	PRIMARY KEY CLUSTERED ([FleteDetalleID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [PK_FleteInterno] 
	PRIMARY KEY CLUSTERED ([FleteInternoID])
; GO

ALTER TABLE [FleteInternoCosto] ADD CONSTRAINT [PK_FleteInternoCosto] 
	PRIMARY KEY CLUSTERED ([FleteInternoCostoID])
; GO

ALTER TABLE [FleteInternoDetalle] ADD CONSTRAINT [PK_FleteInternoDetalle] 
	PRIMARY KEY CLUSTERED ([FleteInternoDetalleID])
; GO

ALTER TABLE [FleteMermaPermitida] ADD CONSTRAINT [PK_FleteMermaPermitida] 
	PRIMARY KEY CLUSTERED ([FleteMermaPermitidaID])
; GO

ALTER TABLE [Folio] ADD CONSTRAINT [PK_Folio] 
	PRIMARY KEY CLUSTERED ([FolioID])
; GO

ALTER TABLE [FolioAlmacen] ADD CONSTRAINT [PK_FolioAlmacen] 
	PRIMARY KEY CLUSTERED ([FolioAlmacenID])
; GO

ALTER TABLE [Formula] ADD CONSTRAINT [PK_Formula] 
	PRIMARY KEY CLUSTERED ([FormulaID])
; GO

ALTER TABLE [Formulario] ADD CONSTRAINT [PK_Formulario] 
	PRIMARY KEY CLUSTERED ([FormularioID])
; GO

ALTER TABLE [GastoCentroCosto] ADD CONSTRAINT [PK_GastoCentroCosto] 
	PRIMARY KEY CLUSTERED ([GastoCentroCostoID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [PK_GastoInventario] 
	PRIMARY KEY CLUSTERED ([GastoInventarioID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [PK_GastoMateriaPrima] 
	PRIMARY KEY CLUSTERED ([GastoMateriaPrimaID])
; GO

ALTER TABLE [Gelatinizacion] ADD CONSTRAINT [PK_Gelatinizacion] 
	PRIMARY KEY CLUSTERED ([GelatinizacionID])
; GO

ALTER TABLE [GelatinizacionDetalle] ADD CONSTRAINT [PK_GelatinizacionDetalle] 
	PRIMARY KEY CLUSTERED ([GelatinizacionDetalleID])
; GO

ALTER TABLE [Grado] ADD CONSTRAINT [PK_Grado] 
	PRIMARY KEY CLUSTERED ([GradoID])
; GO

ALTER TABLE [Grupo] ADD CONSTRAINT [PK_Grupo] 
	PRIMARY KEY CLUSTERED ([GrupoID])
; GO

ALTER TABLE [GrupoCorral] ADD CONSTRAINT [PK_GrupoCorral] 
	PRIMARY KEY CLUSTERED ([GrupoCorralID])
; GO

ALTER TABLE [Indicador] ADD CONSTRAINT [PK_Indicador] 
	PRIMARY KEY CLUSTERED ([IndicadorID])
; GO

ALTER TABLE [IndicadorObjetivo] ADD CONSTRAINT [PK_IndicadorObjetivo] 
	PRIMARY KEY CLUSTERED ([IndicadorObjetivoID])
; GO

ALTER TABLE [IndicadorProducto] ADD CONSTRAINT [PK_IndicadorProducto] 
	PRIMARY KEY CLUSTERED ([IndicadorProductoID])
; GO

ALTER TABLE [IndicadorProductoBoleta] ADD CONSTRAINT [PK_IndicadorProductoBoleta] 
	PRIMARY KEY CLUSTERED ([IndicadorProductoBoletaID])
; GO

ALTER TABLE [IndicadorProductoCalidad] ADD CONSTRAINT [PK_IndicadorProductoCalidad] 
	PRIMARY KEY CLUSTERED ([IndicadorProductoCalidadID])
; GO

ALTER TABLE [Ingrediente] ADD CONSTRAINT [PK_ConfiguracionFormulaIngrediente] 
	PRIMARY KEY CLUSTERED ([IngredienteID])
; GO

ALTER TABLE [InterfaceCosto] ADD CONSTRAINT [PK_InterfaceCosto] 
	PRIMARY KEY CLUSTERED ([CostoID], [CodigoRelacion], [TipoSistema])
; GO

ALTER TABLE [InterfaceOrganizacion] ADD CONSTRAINT [PK_InterfaceOrganizacion] 
	PRIMARY KEY CLUSTERED ([OrganizacionID], [CodigoRelacion], [TipoSistema])
; GO

ALTER TABLE [InterfaceSalida] ADD CONSTRAINT [PK_InterfaceSalida] 
	PRIMARY KEY CLUSTERED ([OrganizacionID], [SalidaID])
; GO

ALTER TABLE [InterfaceSalidaAnimal] ADD CONSTRAINT [PK_InterfaceSalidaAnimal] 
	PRIMARY KEY CLUSTERED ([OrganizacionID], [SalidaID], [Arete], [FechaCompra])
; GO

ALTER TABLE [InterfaceSalidaCosto] ADD CONSTRAINT [PK_InterfaceSalidaCosto] 
	PRIMARY KEY CLUSTERED ([OrganizacionID], [SalidaID], [Arete], [FechaCompra], [CostoID])
; GO

ALTER TABLE [InterfaceSalidaDetalle] ADD CONSTRAINT [PK_InterfaceSalidaDetalle] 
	PRIMARY KEY CLUSTERED ([OrganizacionID], [SalidaID], [TipoGanadoID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [PK_InterfaceSalidaTraspaso] 
	PRIMARY KEY CLUSTERED ([InterfaceSalidaTraspasoID])
; GO

ALTER TABLE [Iva] ADD CONSTRAINT [PK_Iva] 
	PRIMARY KEY CLUSTERED ([IvaID])
; GO

ALTER TABLE [Jaula] ADD CONSTRAINT [PK_Jaula] 
	PRIMARY KEY CLUSTERED ([JaulaID])
; GO

ALTER TABLE [LectorRegistro] ADD CONSTRAINT [PK_LectorRegistro] 
	PRIMARY KEY CLUSTERED ([LectorRegistroID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [PK_LectorRegistroDetalle] 
	PRIMARY KEY CLUSTERED ([LectorRegistroDetalleID])
; GO

ALTER TABLE [Liquidacion] ADD CONSTRAINT [PK_Liquidacion] 
	PRIMARY KEY CLUSTERED ([LiquidacionID])
; GO

ALTER TABLE [LiquidacionDetalle] ADD CONSTRAINT [PK_LiquidacionDetalle] 
	PRIMARY KEY CLUSTERED ([LiquidacionDetalleID])
; GO

ALTER TABLE [Lote] ADD CONSTRAINT [PK_Lote] 
	PRIMARY KEY CLUSTERED ([LoteID])
; GO

ALTER TABLE [LoteDistribucionAlimento] ADD CONSTRAINT [PK_LoteDistibucionAlimento] 
	PRIMARY KEY CLUSTERED ([LoteDistribucionAlimentoID])
; GO

ALTER TABLE [LoteProyeccion] ADD CONSTRAINT [PK_LoteProyeccion] 
	PRIMARY KEY CLUSTERED ([LoteID], [OrganizacionID])
; GO

ALTER TABLE [LoteReimplante] ADD CONSTRAINT [PK_LoteReimplante] 
	PRIMARY KEY CLUSTERED ([LoteReimplanteID], [NumeroReimplante])
; GO

ALTER TABLE [LoteSacrificio] ADD CONSTRAINT [PK_LoteSacrificio] 
	PRIMARY KEY CLUSTERED ([LoteSacrificioID])
; GO

ALTER TABLE [LoteSacrificioDetalle] ADD CONSTRAINT [PK_LoteSacrificioDetalle] 
	PRIMARY KEY CLUSTERED ([LoteSacrificioDetalleID])
; GO

ALTER TABLE [MermaSuperavit] ADD CONSTRAINT [PK_MermasSuperavit] 
	PRIMARY KEY CLUSTERED ([MermaSuperavitID])
; GO

ALTER TABLE [MetodoPago] ADD CONSTRAINT [PK_MetodoPago] 
	PRIMARY KEY CLUSTERED ([MetodoPagoID])
; GO

ALTER TABLE [Mezcladora] ADD CONSTRAINT [PK_Mezcladora] 
	PRIMARY KEY CLUSTERED ([MezcladoraID])
; GO

ALTER TABLE [Modulo] ADD CONSTRAINT [PK_OpcionMenu] 
	PRIMARY KEY CLUSTERED ([ModuloID])
; GO

ALTER TABLE [Moneda] ADD CONSTRAINT [PK_Moneda] 
	PRIMARY KEY CLUSTERED ([MonedaID])
; GO

ALTER TABLE [MonitoreoSilo] ADD CONSTRAINT [PK_MonitoreoSilo] 
	PRIMARY KEY CLUSTERED ([MonitoreoSiloID])
; GO

ALTER TABLE [MonitoreoSiloDetalle] ADD CONSTRAINT [PK_MonitoreoSiloDetalle] 
	PRIMARY KEY CLUSTERED ([MonitoreoSiloDetalleID])
; GO

ALTER TABLE [MonitoreoSiloIndicador] ADD CONSTRAINT [PK_MonitoreoSiloIndicador] 
	PRIMARY KEY CLUSTERED ([MonitoreoSiloIndicadorID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [PK_Muertes] 
	PRIMARY KEY CLUSTERED ([MuerteID])
; GO

ALTER TABLE [MuestreoFibraFormula] ADD CONSTRAINT [PK_MuestreoFibraFormula] 
	PRIMARY KEY CLUSTERED ([MuestreoFibraFormulaID])
; GO

ALTER TABLE [MuestreoFibraIngrediente] ADD CONSTRAINT [PK_MuestreoFibraIngrediente] 
	PRIMARY KEY CLUSTERED ([MuestreoFibraIngredienteID])
; GO

ALTER TABLE [Notificaciones] ADD CONSTRAINT [PK_Notificaciones] 
	PRIMARY KEY CLUSTERED ([NotificacionID])
; GO

ALTER TABLE [Observacion] ADD CONSTRAINT [PK_Observacion] 
	PRIMARY KEY CLUSTERED ([ObservacionID])
; GO

ALTER TABLE [Operador] ADD CONSTRAINT [PK_Operador] 
	PRIMARY KEY CLUSTERED ([OperadorID])
; GO

ALTER TABLE [OrdenSacrificio] ADD CONSTRAINT [PK_OrdenSacrificio] 
	PRIMARY KEY CLUSTERED ([OrdenSacrificioID])
; GO

ALTER TABLE [OrdenSacrificioDetalle] ADD CONSTRAINT [PK_OrdenSacrificioDetalle] 
	PRIMARY KEY CLUSTERED ([OrdenSacrificioDetalleID])
; GO

ALTER TABLE [Organizacion] ADD CONSTRAINT [PK_Organizacion] 
	PRIMARY KEY CLUSTERED ([OrganizacionID])
; GO

ALTER TABLE [Padre] ADD CONSTRAINT [PK_Padre] 
	PRIMARY KEY CLUSTERED ([PadreID])
; GO

ALTER TABLE [Parametro] ADD CONSTRAINT [PK_Parametro] 
	PRIMARY KEY CLUSTERED ([ParametroID])
; GO

ALTER TABLE [ParametroGeneral] ADD CONSTRAINT [PK_ParametroGeneral] 
	PRIMARY KEY CLUSTERED ([ParametroGeneralID])
; GO

ALTER TABLE [ParametroOrganizacion] ADD CONSTRAINT [PK_ParametroOrganizacion] 
	PRIMARY KEY CLUSTERED ([ParametroOrganizacionID])
; GO

ALTER TABLE [ParametroTrampa] ADD CONSTRAINT [PK_ParametroTrampa] 
	PRIMARY KEY CLUSTERED ([ParametroTrampaID])
; GO

ALTER TABLE [Pedido] ADD CONSTRAINT [PK_Pedido] 
	PRIMARY KEY CLUSTERED ([PedidoID])
; GO

ALTER TABLE [PedidoDetalle] ADD CONSTRAINT [PK_PedidoDetalle] 
	PRIMARY KEY CLUSTERED ([PedidoDetalleID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [PK_PesajeMateriaPrima] 
	PRIMARY KEY CLUSTERED ([PesajeMateriaPrimaID])
; GO

ALTER TABLE [Poliza] ADD CONSTRAINT [PK_Poliza] 
	PRIMARY KEY CLUSTERED ([PolizaID])
; GO

ALTER TABLE [PrecioGanado] ADD CONSTRAINT [PK_PrecioGanado] 
	PRIMARY KEY CLUSTERED ([PrecioGanadoID])
; GO

ALTER TABLE [PrecioPAC] ADD CONSTRAINT [PK_PrecioProductoAndelCaliente] 
	PRIMARY KEY CLUSTERED ([PrecioPACID])
; GO

ALTER TABLE [Pregunta] ADD CONSTRAINT [PK_Pregunta] 
	PRIMARY KEY CLUSTERED ([PreguntaID])
; GO

ALTER TABLE [Premezcla] ADD CONSTRAINT [PK_Premezcla] 
	PRIMARY KEY CLUSTERED ([PremezclaID])
; GO

ALTER TABLE [PremezclaDetalle] ADD CONSTRAINT [PK_PremezclaDetalle] 
	PRIMARY KEY CLUSTERED ([PremezclaDetalleID])
; GO

ALTER TABLE [PremezclaDistribucion] ADD CONSTRAINT [PK_PremezclaDistribucion] 
	PRIMARY KEY CLUSTERED ([PremezclaDistribucionID])
; GO

ALTER TABLE [PremezclaDistribucionDetalle] ADD CONSTRAINT [PK_PremezclaDistribucionDetalle] 
	PRIMARY KEY CLUSTERED ([PremezclaDistribucionDetalleID])
; GO

ALTER TABLE [Problema] ADD CONSTRAINT [PK_Problema] 
	PRIMARY KEY CLUSTERED ([ProblemaID])
; GO

ALTER TABLE [ProblemaSintoma] ADD CONSTRAINT [PK_ProblemaSintoma] 
	PRIMARY KEY CLUSTERED ([ProblemaSintomaID])
; GO

ALTER TABLE [ProblemaTratamiento] ADD CONSTRAINT [PK_ProblemaTratamiento] 
	PRIMARY KEY CLUSTERED ([ProblemaTratamientoID])
; GO

ALTER TABLE [ProduccionDiaria] ADD CONSTRAINT [PK_ProduccionDiaria] 
	PRIMARY KEY CLUSTERED ([ProduccionDiariaID])
; GO

ALTER TABLE [ProduccionDiariaDetalle] ADD CONSTRAINT [PK_ProduccionDiariaDetalle] 
	PRIMARY KEY CLUSTERED ([ProduccionDiariaDetalleID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [PK_ProduccionFormula] 
	PRIMARY KEY CLUSTERED ([ProduccionFormulaID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [PK_ProduccionFormulaBatch] 
	PRIMARY KEY CLUSTERED ([ProduccionFormulaBatchID])
; GO

ALTER TABLE [ProduccionFormulaDetalle] ADD CONSTRAINT [PK_ProduccionFormulaDetalle] 
	PRIMARY KEY CLUSTERED ([ProduccionFormulaDetalleID])
; GO

ALTER TABLE [Producto] ADD CONSTRAINT [PK_Producto] 
	PRIMARY KEY CLUSTERED ([ProductoID])
; GO

ALTER TABLE [ProductoCodigoParte] ADD CONSTRAINT [PK_ProductoCodigoParte] 
	PRIMARY KEY CLUSTERED ([ProductoCodigoParteID])
; GO

ALTER TABLE [ProgramacionCorte] ADD CONSTRAINT [PK_ProgramacionCorte] 
	PRIMARY KEY CLUSTERED ([FolioProgramacionID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [PK_ProgramacionMateriaPrima] 
	PRIMARY KEY CLUSTERED ([ProgramacionMateriaPrimaID])
; GO

ALTER TABLE [ProgramacionReimplante] ADD CONSTRAINT [PK_ProgramacionReimplante] 
	PRIMARY KEY CLUSTERED ([FolioProgramacionID])
; GO

ALTER TABLE [ProgramacionReimplanteDetalle] ADD CONSTRAINT [PK_ProgramacionReimplanteDetalle] 
	PRIMARY KEY CLUSTERED ([ProgramacionReimplanteDetalleID])
; GO

ALTER TABLE [Proveedor] ADD CONSTRAINT [PK_Proveedor] 
	PRIMARY KEY CLUSTERED ([ProveedorID])
; GO

ALTER TABLE [ProveedorAlmacen] ADD CONSTRAINT [PK_ProveedorAlmacen] 
	PRIMARY KEY CLUSTERED ([ProveedorAlmacenID])
; GO

ALTER TABLE [ProveedorChofer] ADD CONSTRAINT [PK_ProveedorChofer] 
	PRIMARY KEY CLUSTERED ([ProveedorChoferID])
; GO

ALTER TABLE [RecepcionProducto] ADD CONSTRAINT [PK_RecepcionProducto] 
	PRIMARY KEY CLUSTERED ([RecepcionProductoID])
; GO

ALTER TABLE [RecepcionProductoDetalle] ADD CONSTRAINT [PK_RecepcionProductoDetalle] 
	PRIMARY KEY CLUSTERED ([RecepcionProductoDetalleID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [PK_RegistroVigilancia] 
	PRIMARY KEY CLUSTERED ([RegistroVigilanciaID])
; GO

ALTER TABLE [Reparto] ADD CONSTRAINT [PK_Reparto] 
	PRIMARY KEY CLUSTERED ([RepartoID])
; GO

ALTER TABLE [RepartoAlimento] ADD CONSTRAINT [PK_RepartoAlimento] 
	PRIMARY KEY CLUSTERED ([RepartoAlimentoID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [PK_RepartoAlimentoDetalle] 
	PRIMARY KEY CLUSTERED ([RepartoAlimentoDetalleID])
; GO

ALTER TABLE [RepartoAvance] ADD CONSTRAINT [PK_RepartoAvance] 
	PRIMARY KEY CLUSTERED ([UsuarioID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [PK_RepartoDetalle] 
	PRIMARY KEY CLUSTERED ([RepartoDetalleID])
; GO

ALTER TABLE [Retencion] ADD CONSTRAINT [PK_Retencion] 
	PRIMARY KEY CLUSTERED ([RetencionID])
; GO

ALTER TABLE [RevisionImplante] ADD CONSTRAINT [PK_RevisionImplante] 
	PRIMARY KEY CLUSTERED ([RevisionImplanteID])
; GO

ALTER TABLE [Rol] ADD CONSTRAINT [PK_Rol] 
	PRIMARY KEY CLUSTERED ([RolID])
; GO

ALTER TABLE [Roladora] ADD CONSTRAINT [PK_Roladora] 
	PRIMARY KEY CLUSTERED ([RoladoraID])
; GO

ALTER TABLE [Rotomix] ADD CONSTRAINT [PK_Rotomix] 
	PRIMARY KEY CLUSTERED ([RotomixID])
; GO

ALTER TABLE [SalidaAnimal] ADD CONSTRAINT [PK_SalidaAnimal] 
	PRIMARY KEY CLUSTERED ([SalidaAnimalID])
; GO

ALTER TABLE [SalidaGanado] ADD CONSTRAINT [PK_SalidaGanado] 
	PRIMARY KEY CLUSTERED ([SalidaGanadoID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [PK_SalidaProducto] 
	PRIMARY KEY CLUSTERED ([SalidaProductoID])
; GO

ALTER TABLE [ServicioAlimento] ADD CONSTRAINT [PK_ServicioAlimento] 
	PRIMARY KEY CLUSTERED ([ServicioID])
; GO

ALTER TABLE [Silo] ADD CONSTRAINT [PK_Silo] 
	PRIMARY KEY CLUSTERED ([SiloID])
; GO

ALTER TABLE [Sintoma] ADD CONSTRAINT [PK_Sintoma] 
	PRIMARY KEY CLUSTERED ([SintomaID])
; GO

ALTER TABLE [SolicitudPremezcla] ADD CONSTRAINT [PK_SolicitudPremezcla] 
	PRIMARY KEY CLUSTERED ([SolicitudPremezclaID])
; GO

ALTER TABLE [SolicitudPremezclaDetalle] ADD CONSTRAINT [PK_SolicitudPremezclaDetalle] 
	PRIMARY KEY CLUSTERED ([SolicitudPremezclaDetalleID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [PK_SolicitudProducto] 
	PRIMARY KEY CLUSTERED ([SolicitudProductoID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [PK_SolicitudProductoDetalle] 
	PRIMARY KEY CLUSTERED ([SolicitudProductoDetalleID])
; GO

ALTER TABLE [SubFamilia] ADD CONSTRAINT [PK_SubFamilia] 
	PRIMARY KEY CLUSTERED ([SubFamiliaID])
; GO

ALTER TABLE [SupervisionDetectores] ADD CONSTRAINT [PK_SupervisionDetectores] 
	PRIMARY KEY CLUSTERED ([SupervisionDetectoresID])
; GO

ALTER TABLE [SupervisionDetectoresDetalle] ADD CONSTRAINT [PK_SupervisionDetectoresDetalle] 
	PRIMARY KEY CLUSTERED ([SupervisionDetectoresDetalleID])
; GO

ALTER TABLE [SupervisionGanado] ADD CONSTRAINT [PK_SupervisionGanado] 
	PRIMARY KEY CLUSTERED ([SupervisionGanadoID])
; GO

ALTER TABLE [SupervisorEnfermeria] ADD CONSTRAINT [PK_SupervisorEnfermeria] 
	PRIMARY KEY CLUSTERED ([SupervisorEnfermeriaID])
; GO

ALTER TABLE [TarifaSecado] ADD CONSTRAINT [PK_TarifaSecado] 
	PRIMARY KEY CLUSTERED ([TarifaSecadoID])
; GO

ALTER TABLE [TiempoMuerto] ADD CONSTRAINT [PK_TiempoMuerto] 
	PRIMARY KEY CLUSTERED ([TiempoMuertoID])
; GO

ALTER TABLE [TipoAlmacen] ADD CONSTRAINT [PK_TipoAlmacen] 
	PRIMARY KEY CLUSTERED ([TipoAlmacenID])
; GO

ALTER TABLE [TipoAutorizacion] ADD CONSTRAINT [PK_TipoAutorizacion] 
	PRIMARY KEY CLUSTERED ([TipoAutorizacionID])
; GO

ALTER TABLE [TipoCambio] ADD CONSTRAINT [PK_TipoCambio] 
	PRIMARY KEY CLUSTERED ([TipoCambioID])
; GO

ALTER TABLE [TipoCancelacion] ADD CONSTRAINT [PK_TipoCancelacion] 
	PRIMARY KEY CLUSTERED ([TipoCancelacionID])
; GO

ALTER TABLE [TipoContrato] ADD CONSTRAINT [PK_TipoContrato] 
	PRIMARY KEY CLUSTERED ([TipoContratoID])
; GO

ALTER TABLE [TipoCorral] ADD CONSTRAINT [PK_TipoCorral] 
	PRIMARY KEY CLUSTERED ([TipoCorralID])
; GO

ALTER TABLE [TipoCosto] ADD CONSTRAINT [PK_TipoCosto] 
	PRIMARY KEY CLUSTERED ([TipoCostoID])
; GO

ALTER TABLE [TipoCuenta] ADD CONSTRAINT [PK_TipoCuenta] 
	PRIMARY KEY CLUSTERED ([TipoCuentaID])
; GO

ALTER TABLE [TipoDeteccion] ADD CONSTRAINT [PK_TipoDeteccion] 
	PRIMARY KEY CLUSTERED ([TipoDeteccionID])
; GO

ALTER TABLE [TipoEmbarque] ADD CONSTRAINT [PK_TipoEmbarque] 
	PRIMARY KEY CLUSTERED ([TipoEmbarqueID])
; GO

ALTER TABLE [TipoEstatus] ADD CONSTRAINT [PK_TipoEstatus] 
	PRIMARY KEY CLUSTERED ([TipoEstatusID])
; GO

ALTER TABLE [TipoFlete] ADD CONSTRAINT [PK_TipoFlete] 
	PRIMARY KEY CLUSTERED ([TipoFleteID])
; GO

ALTER TABLE [TipoFolio] ADD CONSTRAINT [PK_TipoFolio] 
	PRIMARY KEY CLUSTERED ([TipoFolioID])
; GO

ALTER TABLE [TipoFormula] ADD CONSTRAINT [PK_TipoFormula] 
	PRIMARY KEY CLUSTERED ([TipoFormulaID])
; GO

ALTER TABLE [TipoGanado] ADD CONSTRAINT [PK_TipoGanado] 
	PRIMARY KEY CLUSTERED ([TipoGanadoID])
; GO

ALTER TABLE [TipoGanadoCorrales] ADD CONSTRAINT [PK_TipoGanadoCorrales] 
	PRIMARY KEY CLUSTERED ([TipoGanadoCorralID])
; GO

ALTER TABLE [TipoLugarMuestra] ADD CONSTRAINT [PK_TipoLugarMuestra] 
	PRIMARY KEY CLUSTERED ([TipoLugarMuestraID])
; GO

ALTER TABLE [TipoMovimiento] ADD CONSTRAINT [PK_TipoMovimiento] 
	PRIMARY KEY CLUSTERED ([TipoMovimientoID])
; GO

ALTER TABLE [TipoMuestra] ADD CONSTRAINT [PK_TipoAnalisisMuestra] 
	PRIMARY KEY CLUSTERED ([TipoMuestraID])
; GO

ALTER TABLE [TipoObjetivoCalidad] ADD CONSTRAINT [PK_TipoObjetivoCalidad] 
	PRIMARY KEY CLUSTERED ([TipoObjetivoCalidadID])
; GO

ALTER TABLE [TipoObservacion] ADD CONSTRAINT [PK_TipoObservacion] 
	PRIMARY KEY CLUSTERED ([TipoObservacionID])
; GO

ALTER TABLE [TipoOrganizacion] ADD CONSTRAINT [PK_TipoOrganizacion] 
	PRIMARY KEY CLUSTERED ([TipoOrganizacionID])
; GO

ALTER TABLE [TipoPAC] ADD CONSTRAINT [PK_TipoPAC] 
	PRIMARY KEY CLUSTERED ([TipoPACID])
; GO

ALTER TABLE [TipoParametro] ADD CONSTRAINT [PK_TipoParametro] 
	PRIMARY KEY CLUSTERED ([TipoParametroID])
; GO

ALTER TABLE [TipoPesaje] ADD CONSTRAINT [PK_TipoPesaje] 
	PRIMARY KEY CLUSTERED ([TipoPesajeID])
; GO

ALTER TABLE [TipoPoliza] ADD CONSTRAINT [PK_TipoPoliza] 
	PRIMARY KEY CLUSTERED ([TipoPolizaID])
; GO

ALTER TABLE [TipoPregunta] ADD CONSTRAINT [PK_TipoPregunta] 
	PRIMARY KEY CLUSTERED ([TipoPreguntaID])
; GO

ALTER TABLE [TipoProblema] ADD CONSTRAINT [PK_TipoProblema] 
	PRIMARY KEY CLUSTERED ([TipoProblemaID])
; GO

ALTER TABLE [TipoProceso] ADD CONSTRAINT [PK_TipoProceso] 
	PRIMARY KEY CLUSTERED ([TipoProcesoID])
; GO

ALTER TABLE [TipoProrrateo] ADD CONSTRAINT [PK_TipoProrrateo] 
	PRIMARY KEY CLUSTERED ([TipoProrrateoID])
; GO

ALTER TABLE [TipoProveedor] ADD CONSTRAINT [PK_TipoProveedor] 
	PRIMARY KEY CLUSTERED ([TipoProveedorID])
; GO

ALTER TABLE [TipoServicio] ADD CONSTRAINT [PK_TipoServicio] 
	PRIMARY KEY CLUSTERED ([TipoServicioID])
; GO

ALTER TABLE [TipoTarifa] ADD CONSTRAINT [PK_TipoTarifa] 
	PRIMARY KEY CLUSTERED ([TipoTarifaID])
; GO

ALTER TABLE [TipoTecnica] ADD CONSTRAINT [PK_TipoTecnicaAnalisis] 
	PRIMARY KEY CLUSTERED ([TipoTecnicaID])
; GO

ALTER TABLE [TipoTratamiento] ADD CONSTRAINT [PK_TipoTratamiento] 
	PRIMARY KEY CLUSTERED ([TipoTratamientoID])
; GO

ALTER TABLE [Trampa] ADD CONSTRAINT [PK_Trampa] 
	PRIMARY KEY CLUSTERED ([TrampaID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [PK_TraspasoMateriaPrima] 
	PRIMARY KEY CLUSTERED ([TraspasoMateriaPrimaID])
; GO

ALTER TABLE [Tratamiento] ADD CONSTRAINT [PK_Tratamiento] 
	PRIMARY KEY CLUSTERED ([TratamientoID])
; GO

ALTER TABLE [TratamientoProducto] ADD CONSTRAINT [PK_TratamientoProducto] 
	PRIMARY KEY CLUSTERED ([TratamientoProductoID])
; GO

ALTER TABLE [UnidadMedicion] ADD CONSTRAINT [PK_UnidadMedicion] 
	PRIMARY KEY CLUSTERED ([UnidadID])
; GO

ALTER TABLE [Usuario] ADD CONSTRAINT [PK_Usuario] 
	PRIMARY KEY CLUSTERED ([UsuarioID])
; GO

ALTER TABLE [UsuarioGrupo] ADD CONSTRAINT [PK_UsuarioGrupo] 
	PRIMARY KEY CLUSTERED ([UsuarioGrupoID])
; GO

ALTER TABLE [VentaGanado] ADD CONSTRAINT [PK_VentaGanado] 
	PRIMARY KEY CLUSTERED ([VentaGanadoID])
; GO

ALTER TABLE [VentaGanadoDetalle] ADD CONSTRAINT [PK_VentaGanadoDetalle] 
	PRIMARY KEY CLUSTERED ([VentaGanadoDetalleID])
; GO



ALTER TABLE [AccionesSIAP] ADD CONSTRAINT [FK_AccionesSIAP_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AccionesSIAP] ADD CONSTRAINT [FK_AccionesSIAP_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Almacen] ADD CONSTRAINT [FK_Almacen_TipoAlmacen] 
	FOREIGN KEY ([TipoAlmacenID]) REFERENCES [TipoAlmacen] ([TipoAlmacenID])
; GO

ALTER TABLE [Almacen] ADD CONSTRAINT [FK_Almacen_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Almacen] ADD CONSTRAINT [FK_Almacen_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenInventario] ADD CONSTRAINT [FK_AlmacenInventario_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [AlmacenInventario] ADD CONSTRAINT [FK_AlmacenInventario_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [AlmacenInventario] ADD CONSTRAINT [FK_AlmacenInventario_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenInventario] ADD CONSTRAINT [FK_AlmacenInventario_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenInventarioLote] ADD CONSTRAINT [FK_AlmacenInventarioLote_AlmacenInventario_02] 
	FOREIGN KEY ([AlmacenInventarioID]) REFERENCES [AlmacenInventario] ([AlmacenInventarioID])
; GO

ALTER TABLE [AlmacenInventarioLote] ADD CONSTRAINT [FK_AlmacenInventarioLote_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenInventarioLote] ADD CONSTRAINT [FK_AlmacenInventarioLote_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenMovimiento] ADD CONSTRAINT [FK_AlmacenMovimiento_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [AlmacenMovimiento] ADD CONSTRAINT [FK_AlmacenMovimiento_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [AlmacenMovimiento] ADD CONSTRAINT [FK_AlmacenMovimiento_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [AlmacenMovimiento] ADD CONSTRAINT [FK_AlmacenMovimiento_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenMovimiento] ADD CONSTRAINT [FK_AlmacenMovimiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [FK_AlmacenMovimientoCosto_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [FK_AlmacenMovimientoCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [FK_AlmacenMovimientoCosto_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [FK_AlmacenMovimientoCosto_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [FK_AlmacenMovimientoCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenMovimientoCosto] ADD CONSTRAINT [FK_AlmacenMovimientoCosto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [FK_AlmacenMovimientoDetalle_AlmacenInventarioLote] 
	FOREIGN KEY ([AlmacenInventarioLoteID]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [FK_AlmacenMovimientoDetalle_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [FK_AlmacenMovimientoDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [FK_AlmacenMovimientoDetalle_Tratamiento] 
	FOREIGN KEY ([TratamientoID]) REFERENCES [Tratamiento] ([TratamientoID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [FK_AlmacenMovimientoDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenMovimientoDetalle] ADD CONSTRAINT [FK_AlmacenMovimientoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenUsuario] ADD CONSTRAINT [FK_AlmacenUsuario_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [AlmacenUsuario] ADD CONSTRAINT [FK_AlmacenUsuario_UsuarioAlmacen] 
	FOREIGN KEY ([UsuarioID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenUsuario] ADD CONSTRAINT [FK_AlmacenUsuario_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmacenUsuario] ADD CONSTRAINT [FK_AlmacenUsuario_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonFactor] ADD CONSTRAINT [FK_AlmidonFactor_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [AlmidonFactor] ADD CONSTRAINT [FK_AlmidonFactor_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonFactor] ADD CONSTRAINT [FK_AlmidonFactor_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_Operador] 
	FOREIGN KEY ([OperdorIDCalidad]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_UsuarioIDLaboratorio] 
	FOREIGN KEY ([UsuarioIDLaboratorio]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonHeces] ADD CONSTRAINT [FK_AlmidonHeces_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonHecesDetalle] ADD CONSTRAINT [FK_AlmidonHecesDetalle_AlmidonHeces] 
	FOREIGN KEY ([AlmidonHecesID]) REFERENCES [AlmidonHeces] ([AlmidonHecesID])
; GO

ALTER TABLE [AlmidonHecesDetalle] ADD CONSTRAINT [FK_AlmidonHecesDetalle_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [AlmidonHecesDetalle] ADD CONSTRAINT [FK_AlmidonHecesDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AlmidonHecesDetalle] ADD CONSTRAINT [FK_AlmidonHecesDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnalisisGrasa] ADD CONSTRAINT [FK_AnalisisGrasa_EntradaProducto] 
	FOREIGN KEY ([EntradaProductoID]) REFERENCES [EntradaProducto] ([EntradaProductoID])
; GO

ALTER TABLE [AnalisisGrasa] ADD CONSTRAINT [FK_AnalisisGrasa_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [AnalisisGrasa] ADD CONSTRAINT [FK_AnalisisGrasa_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnalisisGrasa] ADD CONSTRAINT [FK_AnalisisGrasa_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [FK_Animal_CalidadGanado] 
	FOREIGN KEY ([CalidadGanadoID]) REFERENCES [CalidadGanado] ([CalidadGanadoID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [FK_Animal_CausaRechazo] 
	FOREIGN KEY ([CausaRechadoID]) REFERENCES [CausaRechazo] ([CausaRechazoID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [FK_Animal_ClasificacionGanado] 
	FOREIGN KEY ([ClasificacionGanadoID]) REFERENCES [ClasificacionGanado] ([ClasificacionGanadoID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [FK_Animal_TipoGanado] 
	FOREIGN KEY ([TipoGanadoID]) REFERENCES [TipoGanado] ([TipoGanadoID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [FK_Animal_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Animal] ADD CONSTRAINT [FK_Animal_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [FK_AnimalConsumo_Animal] 
	FOREIGN KEY ([AnimalID]) REFERENCES [Animal] ([AnimalID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [FK_AnimalConsumo_Formula] 
	FOREIGN KEY ([FormulaIDServida]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [FK_AnimalConsumo_Reparto] 
	FOREIGN KEY ([RepartoID]) REFERENCES [Reparto] ([RepartoID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [FK_AnimalConsumo_TipoServicio] 
	FOREIGN KEY ([TipoServicioID]) REFERENCES [TipoServicio] ([TipoServicioID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [FK_AnimalConsumo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalConsumo] ADD CONSTRAINT [FK_AnimalConsumo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalCosto] ADD CONSTRAINT [FK_AnimalCosto_Animal] 
	FOREIGN KEY ([AnimalID]) REFERENCES [Animal] ([AnimalID])
; GO

ALTER TABLE [AnimalCosto] ADD CONSTRAINT [FK_AnimalCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [AnimalCosto] ADD CONSTRAINT [FK_AnimalCosto_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalCosto] ADD CONSTRAINT [FK_AnimalCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_Animal] 
	FOREIGN KEY ([AnimalID]) REFERENCES [Animal] ([AnimalID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_LoteIDAnterior] 
	FOREIGN KEY ([LoteIDOrigen]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_Trampa] 
	FOREIGN KEY ([TrampaID]) REFERENCES [Trampa] ([TrampaID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalMovimiento] ADD CONSTRAINT [FK_AnimalMovimiento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_Animal] 
	FOREIGN KEY ([AnimalID]) REFERENCES [Animal] ([AnimalID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_Corral] 
	FOREIGN KEY ([CorraletaID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_OrdenSacrificio] 
	FOREIGN KEY ([OrdenSacrificioID]) REFERENCES [OrdenSacrificio] ([OrdenSacrificioID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AnimalSalida] ADD CONSTRAINT [FK_AnimalSalida_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AreaRevision] ADD CONSTRAINT [FK_AreaRevision_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AreaRevision] ADD CONSTRAINT [FK_AreaRevision_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_TipoAutorizacion] 
	FOREIGN KEY ([TipoAutorizacionID]) REFERENCES [TipoAutorizacion] ([TipoAutorizacionID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [AutorizacionMateriaPrima] ADD CONSTRAINT [FK_AutorizacionMateriaPrima_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [BitacoraErrores] ADD CONSTRAINT [FK_BitacoraErrores_AccionesSIAP] 
	FOREIGN KEY ([AccionesSiapID]) REFERENCES [AccionesSIAP] ([AccionesSiapID])
; GO

ALTER TABLE [BitacoraErrores] ADD CONSTRAINT [FK_BitacoraErrores_Usuario] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Calculo] ADD CONSTRAINT [FK_Calculo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Calculo] ADD CONSTRAINT [FK_Calculo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMateriaPrima] ADD CONSTRAINT [FK_CalidadMateriaPrima_ColorObjetivo] 
	FOREIGN KEY ([ColorObjetivoID]) REFERENCES [ColorObjetivo] ([ColorObjetivoID])
; GO

ALTER TABLE [CalidadMateriaPrima] ADD CONSTRAINT [FK_CalidadMateriaPrima_IndicadorObjetivo] 
	FOREIGN KEY ([IndicadorObjetivoID]) REFERENCES [IndicadorObjetivo] ([IndicadorObjetivoID])
; GO

ALTER TABLE [CalidadMateriaPrima] ADD CONSTRAINT [FK_CalidadMateriaPrima_PedidoDetalle] 
	FOREIGN KEY ([PedidoDetalleID]) REFERENCES [PedidoDetalle] ([PedidoDetalleID])
; GO

ALTER TABLE [CalidadMateriaPrima] ADD CONSTRAINT [FK_CalidadMateriaPrima_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMateriaPrima] ADD CONSTRAINT [FK_CalidadMateriaPrima_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_CamionReparto] 
	FOREIGN KEY ([CamionRepartoID]) REFERENCES [CamionReparto] ([CamionRepartoID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_Chofer] 
	FOREIGN KEY ([ChoferID]) REFERENCES [Chofer] ([ChoferID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_Mezcladora] 
	FOREIGN KEY ([MezcladoraID]) REFERENCES [Mezcladora] ([MezcladoraID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_OperadorIDAnalista] 
	FOREIGN KEY ([OperadorIDAnalista]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_OperadorIDMezclador] 
	FOREIGN KEY ([OperadorIDMezclador]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_TipoLugarMuestra] 
	FOREIGN KEY ([TipoLugarMuestraID]) REFERENCES [TipoLugarMuestra] ([TipoLugarMuestraID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_TipoTecnica] 
	FOREIGN KEY ([TipoTecnicaID]) REFERENCES [TipoTecnica] ([TipoTecnicaID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_UsuarioLaboratorio] 
	FOREIGN KEY ([UsuarioIDLaboratorio]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezclado] ADD CONSTRAINT [FK_CalidadMezclado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezcladoDetalle] ADD CONSTRAINT [FK_CalidadMezcladoDetalle_CalidadMezclado] 
	FOREIGN KEY ([CalidadMezcladoID]) REFERENCES [CalidadMezclado] ([CalidadMezcladoID])
; GO

ALTER TABLE [CalidadMezcladoDetalle] ADD CONSTRAINT [FK_CalidadMezcladoDetalle_TipoMuestra] 
	FOREIGN KEY ([TipoMuestraID]) REFERENCES [TipoMuestra] ([TipoMuestraID])
; GO

ALTER TABLE [CalidadMezcladoDetalle] ADD CONSTRAINT [FK_CalidadMezcladoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezcladoDetalle] ADD CONSTRAINT [FK_CalidadMezcladoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezcladoFactor] ADD CONSTRAINT [FK_CalidadMezcladoFactor_TipoMuestra] 
	FOREIGN KEY ([TipoMuestraID]) REFERENCES [TipoMuestra] ([TipoMuestraID])
; GO

ALTER TABLE [CalidadMezcladoFactor] ADD CONSTRAINT [FK_CalidadMezcladoFactor_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CalidadMezcladoFactor] ADD CONSTRAINT [FK_CalidadMezcladoFactor_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Camion] ADD CONSTRAINT [FK_Camion_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [Camion] ADD CONSTRAINT [FK_Camion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Camion] ADD CONSTRAINT [FK_Camion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CamionReparto] ADD CONSTRAINT [FK_CamionReparto_CentroCosto] 
	FOREIGN KEY ([CentroCostoID]) REFERENCES [CentroCosto] ([CentroCostoID])
; GO

ALTER TABLE [CamionReparto] ADD CONSTRAINT [FK_CamionReparto_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CamionReparto] ADD CONSTRAINT [FK_CamionReparto_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CamionReparto] ADD CONSTRAINT [FK_CamionReparto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CancelacionMovimiento] ADD CONSTRAINT [FK_CancelacionMovimiento_Pedido] 
	FOREIGN KEY ([PedidoID]) REFERENCES [Pedido] ([PedidoID])
; GO

ALTER TABLE [CancelacionMovimiento] ADD CONSTRAINT [FK_CancelacionMovimiento_TipoCancelacion] 
	FOREIGN KEY ([TipoCancelacionID]) REFERENCES [TipoCancelacion] ([TipoCancelacionID])
; GO

ALTER TABLE [CancelacionMovimiento] ADD CONSTRAINT [FK_CancelacionMovimiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CancelacionMovimiento] ADD CONSTRAINT [FK_CancelacionMovimiento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaPrecio] ADD CONSTRAINT [FK_CausaPrecio_CausaSalida] 
	FOREIGN KEY ([CausaSalidaID]) REFERENCES [CausaSalida] ([CausaSalidaID])
; GO

ALTER TABLE [CausaPrecio] ADD CONSTRAINT [FK_CausaPrecio_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CausaPrecio] ADD CONSTRAINT [FK_CausaPrecio_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaPrecio] ADD CONSTRAINT [FK_CausaPrecio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaRechazo] ADD CONSTRAINT [FK_CausaRechazo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaRechazo] ADD CONSTRAINT [FK_CausaRechazo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaSalida] ADD CONSTRAINT [FK_CausaSalida_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [CausaSalida] ADD CONSTRAINT [FK_CausaSalida_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaSalida] ADD CONSTRAINT [FK_CausaSalida_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaTiempoMuerto] ADD CONSTRAINT [FK_CausaTiempoMuerto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CausaTiempoMuerto] ADD CONSTRAINT [FK_CausaTiempoMuerto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroBeneficio] ADD CONSTRAINT [FK_CentroBeneficio_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroBeneficio] ADD CONSTRAINT [FK_CentroBeneficio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroCosto] ADD CONSTRAINT [FK_CentroCosto_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroCosto] ADD CONSTRAINT [FK_CentroCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroCostoUsuario] ADD CONSTRAINT [FK_CentroCostoUsuario_Usuario] 
	FOREIGN KEY ([UsuarioID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroCostoUsuario] ADD CONSTRAINT [FK_CentroCostoUsuario_UsuarioCreacionID] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroCostoUsuario] ADD CONSTRAINT [FK_CentroCostoUsuario_UsuarioModificacionID] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CentroCostoUsuario] ADD CONSTRAINT [FK_CentroCostoUsuario_CentroCosto] 
	FOREIGN KEY ([CentroCostoID]) REFERENCES [CentroCosto] ([CentroCostoID])
; GO

ALTER TABLE [CheckListCorral] ADD CONSTRAINT [FK_CheckListCorral_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [CheckListCorral] ADD CONSTRAINT [FK_CheckListCorral_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CheckListCorral] ADD CONSTRAINT [FK_CheckListCorral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListCorral] ADD CONSTRAINT [FK_CheckListCorral_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladora] ADD CONSTRAINT [FK_CheckListRoladora_CheckListRoladoraGeneral] 
	FOREIGN KEY ([CheckListRoladoraGeneralID]) REFERENCES [CheckListRoladoraGeneral] ([CheckListRoladoraGeneralID])
; GO

ALTER TABLE [CheckListRoladora] ADD CONSTRAINT [FK_CheckListRoladora_Roladora] 
	FOREIGN KEY ([RoladoraID]) REFERENCES [Roladora] ([RoladoraID])
; GO

ALTER TABLE [CheckListRoladora] ADD CONSTRAINT [FK_CheckListRoladora_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladora] ADD CONSTRAINT [FK_CheckListRoladora_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladora] ADD CONSTRAINT [FK_CheckListRoladora_UsuarioResponsable] 
	FOREIGN KEY ([UsuarioIDResponsable]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraAccion] ADD CONSTRAINT [FK_CheckListRoladoraAccion_CheckListRoladoraRango] 
	FOREIGN KEY ([CheckListRoladoraRangoID]) REFERENCES [CheckListRoladoraRango] ([CheckListRoladoraRangoID])
; GO

ALTER TABLE [CheckListRoladoraAccion] ADD CONSTRAINT [FK_CheckListRoladoraAccion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraAccion] ADD CONSTRAINT [FK_CheckListRoladoraAccion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraDetalle] ADD CONSTRAINT [FK_CheckListRoladoraDetalle_CheckListRoladora] 
	FOREIGN KEY ([CheckListRoladoraID]) REFERENCES [CheckListRoladora] ([CheckListRoladoraID])
; GO

ALTER TABLE [CheckListRoladoraDetalle] ADD CONSTRAINT [FK_CheckListRoladoraDetalle_CheckListRoladoraAccion] 
	FOREIGN KEY ([CheckListRoladoraAccionID]) REFERENCES [CheckListRoladoraAccion] ([CheckListRoladoraAccionID])
; GO

ALTER TABLE [CheckListRoladoraDetalle] ADD CONSTRAINT [FK_CheckListRoladoraDetalle_CheckListRoladoraRango] 
	FOREIGN KEY ([CheckListRoladoraRangoID]) REFERENCES [CheckListRoladoraRango] ([CheckListRoladoraRangoID])
; GO

ALTER TABLE [CheckListRoladoraDetalle] ADD CONSTRAINT [FK_CheckListRoladoraDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraDetalle] ADD CONSTRAINT [FK_CheckListRoladoraDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraGeneral] ADD CONSTRAINT [FK_CheckListRoladoraGeneral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraGeneral] ADD CONSTRAINT [FK_CheckListRoladoraGeneral_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraGeneral] ADD CONSTRAINT [FK_CheckListRoladoraGeneral_UsuarioSupervisor] 
	FOREIGN KEY ([UsuarioIDSupervisor]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraHorometro] ADD CONSTRAINT [FK_CheckListRoladoraHorometro_CheckListRoladoraGeneral] 
	FOREIGN KEY ([CheckListRoladoraGeneralID]) REFERENCES [CheckListRoladoraGeneral] ([CheckListRoladoraGeneralID])
; GO

ALTER TABLE [CheckListRoladoraHorometro] ADD CONSTRAINT [FK_CheckListRoladoraHorometro_Roladora] 
	FOREIGN KEY ([RoladoraID]) REFERENCES [Roladora] ([RoladoraID])
; GO

ALTER TABLE [CheckListRoladoraHorometro] ADD CONSTRAINT [FK_CheckListRoladoraHorometro_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraHorometro] ADD CONSTRAINT [FK_CheckListRoladoraHorometro_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraRango] ADD CONSTRAINT [FK_CheckListRoladoraRango_Pregunta] 
	FOREIGN KEY ([PreguntaID]) REFERENCES [Pregunta] ([PreguntaID])
; GO

ALTER TABLE [CheckListRoladoraRango] ADD CONSTRAINT [FK_CheckListRoladoraRango_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CheckListRoladoraRango] ADD CONSTRAINT [FK_CheckListRoladoraRango_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Chofer] ADD CONSTRAINT [FK_Chofer_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ClaseCostoProducto] ADD CONSTRAINT [FK_ClaseCostoProducto_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [ClaseCostoProducto] ADD CONSTRAINT [FK_ClaseCostoProducto_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [ClaseCostoProducto] ADD CONSTRAINT [FK_ClaseCostoProducto_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [ClaseCostoProducto] ADD CONSTRAINT [FK_ClaseCostoProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ClaseCostoProducto] ADD CONSTRAINT [FK_ClaseCostoProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ClasificacionGanado] ADD CONSTRAINT [FK_ClasificacionGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ClasificacionGanado] ADD CONSTRAINT [FK_ClasificacionGanado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Cliente] ADD CONSTRAINT [FK_Cliente_MetodoPago] 
	FOREIGN KEY ([MetodoPagoID]) REFERENCES [MetodoPago] ([MetodoPagoID])
; GO

ALTER TABLE [Cliente] ADD CONSTRAINT [FK_Cliente_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Cliente] ADD CONSTRAINT [FK_Cliente_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ColorObjetivo] ADD CONSTRAINT [FK_ColorObjetivo_TipoObjetivoCalidad] 
	FOREIGN KEY ([TipoObjetivoCalidadID]) REFERENCES [TipoObjetivoCalidad] ([TipoObjetivoCalidadID])
; GO

ALTER TABLE [ColorObjetivo] ADD CONSTRAINT [FK_ColorObjetivo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ColorObjetivo] ADD CONSTRAINT [FK_ColorObjetivo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConceptoDeteccion] ADD CONSTRAINT [FK_ConceptoDeteccion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConceptoDeteccion] ADD CONSTRAINT [FK_ConceptoDeteccion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Condicion] ADD CONSTRAINT [FK_Condicion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Condicion] ADD CONSTRAINT [FK_Condicion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CondicionJaula] ADD CONSTRAINT [FK_CondicionJaula_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CondicionJaula] ADD CONSTRAINT [FK_CondicionJaula_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionEmbarque] ADD CONSTRAINT [FK_ConfiguracionEmbarque_OrganizacionDestino] 
	FOREIGN KEY ([OrganizacionDestinoID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ConfiguracionEmbarque] ADD CONSTRAINT [FK_ConfiguracionEmbarque_OrganizacionOrigen] 
	FOREIGN KEY ([OrganizacionOrigenID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ConfiguracionEmbarque] ADD CONSTRAINT [FK_ConfiguracionEmbarque_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionEmbarque] ADD CONSTRAINT [FK_ConfiguracionEmbarque_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionFormula] ADD CONSTRAINT [FK_ConfiguracionFormula_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [ConfiguracionFormula] ADD CONSTRAINT [FK_ConfiguracionFormula_FormulaSiguiente] 
	FOREIGN KEY ([FormulaSiguienteID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [ConfiguracionFormula] ADD CONSTRAINT [FK_ConfiguracionFormula_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ConfiguracionFormula] ADD CONSTRAINT [FK_ConfiguracionFormula_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionFormula] ADD CONSTRAINT [FK_ConfiguracionFormula_UsuarioCrecion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionImplante] ADD CONSTRAINT [FK_ConfiguracionImplante_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionImplante] ADD CONSTRAINT [FK_ConfiguracionImplante_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionSemana] ADD CONSTRAINT [FK_ConfiguracionSemana_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ConfiguracionSemana] ADD CONSTRAINT [FK_ConfiguracionSemana_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionSemana] ADD CONSTRAINT [FK_ConfiguracionSemana_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionTraspasoAlmacen] ADD CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_TipoAlmacenOrigen] 
	FOREIGN KEY ([TipoAlmacenOrigenID]) REFERENCES [TipoAlmacen] ([TipoAlmacenID])
; GO

ALTER TABLE [ConfiguracionTraspasoAlmacen] ADD CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_TipoAlmacenDestino] 
	FOREIGN KEY ([TipoAlmacenDestinoID]) REFERENCES [TipoAlmacen] ([TipoAlmacenID])
; GO

ALTER TABLE [ConfiguracionTraspasoAlmacen] ADD CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ConfiguracionTraspasoAlmacen] ADD CONSTRAINT [FK_ConfiguracionTraspasoAlmacen_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Constantes] ADD CONSTRAINT [FK_Constantes_Calculo] 
	FOREIGN KEY ([CalculoID]) REFERENCES [Calculo] ([CalculoID])
; GO

ALTER TABLE [Constantes] ADD CONSTRAINT [FK_Constantes_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_TipoCambio] 
	FOREIGN KEY ([TipoCambioID]) REFERENCES [TipoCambio] ([TipoCambioID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_TipoContrato] 
	FOREIGN KEY ([TipoContratoID]) REFERENCES [TipoContrato] ([TipoContratoID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_TipoFlete] 
	FOREIGN KEY ([TipoFleteID]) REFERENCES [TipoFlete] ([TipoFleteID])
; GO

ALTER TABLE [Contrato] ADD CONSTRAINT [FK_Contrato_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ContratoDetalle] ADD CONSTRAINT [FK_ContratoDetalle_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [ContratoDetalle] ADD CONSTRAINT [FK_ContratoDetalle_Indicador] 
	FOREIGN KEY ([IndicadorID]) REFERENCES [Indicador] ([IndicadorID])
; GO

ALTER TABLE [ContratoDetalle] ADD CONSTRAINT [FK_ContratoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ContratoDetalle] ADD CONSTRAINT [FK_ContratoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ContratoHumedad] ADD CONSTRAINT [FK_ContratoHumedad_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [ContratoHumedad] ADD CONSTRAINT [FK_ContratoHumedad_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ContratoHumedad] ADD CONSTRAINT [FK_ContratoHumedad_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ContratoParcial] ADD CONSTRAINT [FK_ContratoParcial_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [ContratoParcial] ADD CONSTRAINT [FK_ContratoParcial_TipoCambio] 
	FOREIGN KEY ([TipoCambioID]) REFERENCES [TipoCambio] ([TipoCambioID])
; GO

ALTER TABLE [ContratoParcial] ADD CONSTRAINT [FK_ContratoParcial_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ContratoParcial] ADD CONSTRAINT [FK_ContratoParcial_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Corral] ADD CONSTRAINT [FK_Corral_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Corral] ADD CONSTRAINT [FK_Corral_TipoCorral] 
	FOREIGN KEY ([TipoCorralID]) REFERENCES [TipoCorral] ([TipoCorralID])
; GO

ALTER TABLE [Corral] ADD CONSTRAINT [FK_Corral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Corral] ADD CONSTRAINT [FK_Corral_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CorralDetector] ADD CONSTRAINT [FK_CorralDetector_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [CorralDetector] ADD CONSTRAINT [FK_CorralDetector_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [CorralDetector] ADD CONSTRAINT [FK_CorralDetector_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CorralLector] ADD CONSTRAINT [FK_CorralLector_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [CorralLector] ADD CONSTRAINT [FK_CorralLector_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [CorralLector] ADD CONSTRAINT [FK_CorralLector_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CorralLector] ADD CONSTRAINT [FK_CorralLector_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CorralRango] ADD CONSTRAINT [FK_CorralRango_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [CorralRango] ADD CONSTRAINT [FK_CorralRango_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CorralRango] ADD CONSTRAINT [FK_CorralRango_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CorralRango] ADD CONSTRAINT [FK_CorralRango_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Costo] ADD CONSTRAINT [FK_Costo_Retencion] 
	FOREIGN KEY ([RetencionID]) REFERENCES [Retencion] ([RetencionID])
; GO

ALTER TABLE [Costo] ADD CONSTRAINT [FK_Costo_TipoCosto] 
	FOREIGN KEY ([TipoCostoID]) REFERENCES [TipoCosto] ([TipoCostoID])
; GO

ALTER TABLE [Costo] ADD CONSTRAINT [FK_Costo_TipoProrrateo] 
	FOREIGN KEY ([TipoProrrateoID]) REFERENCES [TipoProrrateo] ([TipoProrrateoID])
; GO

ALTER TABLE [Costo] ADD CONSTRAINT [FK_Costo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Costo] ADD CONSTRAINT [FK_Costo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CostoEmbarqueDetalle] ADD CONSTRAINT [FK_CostoEmbarqueDetalle_EmbarqueDetalle] 
	FOREIGN KEY ([EmbarqueDetalleID]) REFERENCES [EmbarqueDetalle] ([EmbarqueDetalleID])
; GO

ALTER TABLE [CostoEmbarqueDetalle] ADD CONSTRAINT [FK_CostoEmbarqueDetalle_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [CostoEmbarqueDetalle] ADD CONSTRAINT [FK_CostoEmbarqueDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CostoEmbarqueDetalle] ADD CONSTRAINT [FK_CostoEmbarqueDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CostoOrganizacion] ADD CONSTRAINT [FK_CostoOrganizacion_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [CostoOrganizacion] ADD CONSTRAINT [FK_CostoOrganizacion_TipoOrganizacion] 
	FOREIGN KEY ([TipoOrganizacionID]) REFERENCES [TipoOrganizacion] ([TipoOrganizacionID])
; GO

ALTER TABLE [CostoOrganizacion] ADD CONSTRAINT [FK_CostoOrganizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CostoOrganizacion] ADD CONSTRAINT [FK_CostoOrganizacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CostoPromedio] ADD CONSTRAINT [FK_CostoPromedio_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [CostoPromedio] ADD CONSTRAINT [FK_CostoPromedio_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CostoPromedio] ADD CONSTRAINT [FK_CostoPromedio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CostoPromedio] ADD CONSTRAINT [FK_CostoPromedio_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CriterioSupervision] ADD CONSTRAINT [FK_CriterioSupervision_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CriterioSupervision] ADD CONSTRAINT [FK_CriterioSupervision_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Cuenta] ADD CONSTRAINT [FK_Cuenta_TipoCuenta] 
	FOREIGN KEY ([TipoCuentaID]) REFERENCES [TipoCuenta] ([TipoCuentaID])
; GO

ALTER TABLE [Cuenta] ADD CONSTRAINT [FK_Cuenta_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Cuenta] ADD CONSTRAINT [FK_Cuenta_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CuentaAlmacenSubFamilia] ADD CONSTRAINT [FK_CuentaAlmacenSubFamilia_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [CuentaAlmacenSubFamilia] ADD CONSTRAINT [FK_CuentaAlmacenSubFamilia_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [CuentaAlmacenSubFamilia] ADD CONSTRAINT [FK_CuentaAlmacenSubFamilia_SubFamilia] 
	FOREIGN KEY ([SubFamiliaID]) REFERENCES [SubFamilia] ([SubFamiliaID])
; GO

ALTER TABLE [CuentaAlmacenSubFamilia] ADD CONSTRAINT [FK_CuentaAlmacenSubFamilia_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CuentaAlmacenSubFamilia] ADD CONSTRAINT [FK_CuentaAlmacenSubFamilia_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CuentaSAP] ADD CONSTRAINT [FK_CuentaSAP_TipoCuenta] 
	FOREIGN KEY ([TipoCuentaID]) REFERENCES [TipoCuenta] ([TipoCuentaID])
; GO

ALTER TABLE [CuentaSAP] ADD CONSTRAINT [FK_CuentaSAP_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CuentaSAP] ADD CONSTRAINT [FK_CuentaSAP_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CuentaValor] ADD CONSTRAINT [FK_CuentaValor_Cuenta] 
	FOREIGN KEY ([CuentaID]) REFERENCES [Cuenta] ([CuentaID])
; GO

ALTER TABLE [CuentaValor] ADD CONSTRAINT [FK_CuentaValor_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [CuentaValor] ADD CONSTRAINT [FK_CuentaValor_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [CuentaValor] ADD CONSTRAINT [FK_CuentaValor_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DescripcionGanado] ADD CONSTRAINT [FK_DescripcionGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DescripcionGanado] ADD CONSTRAINT [FK_DescripcionGanado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_DescripcionGanado] 
	FOREIGN KEY ([DescripcionGanadoID]) REFERENCES [DescripcionGanado] ([DescripcionGanadoID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_Grado] 
	FOREIGN KEY ([GradoID]) REFERENCES [Grado] ([GradoID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_TipoDeteccion] 
	FOREIGN KEY ([TipoDeteccionID]) REFERENCES [TipoDeteccion] ([TipoDeteccionID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Deteccion] ADD CONSTRAINT [FK_Deteccion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_DescripcionGanado] 
	FOREIGN KEY ([DescripcionGanadoID]) REFERENCES [DescripcionGanado] ([DescripcionGanadoID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_Grado] 
	FOREIGN KEY ([GradoID]) REFERENCES [Grado] ([GradoID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_TipoDeteccion] 
	FOREIGN KEY ([TipoDeteccionID]) REFERENCES [TipoDeteccion] ([TipoDeteccionID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DeteccionAnimal] ADD CONSTRAINT [FK_DeteccionAnimal_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DeteccionSintoma] ADD CONSTRAINT [FK_DeteccionSintoma_Deteccion] 
	FOREIGN KEY ([DeteccionID]) REFERENCES [Deteccion] ([DeteccionID])
; GO

ALTER TABLE [DeteccionSintoma] ADD CONSTRAINT [FK_DeteccionSintoma_Sintoma] 
	FOREIGN KEY ([SintomaID]) REFERENCES [Sintoma] ([SintomaID])
; GO

ALTER TABLE [DeteccionSintoma] ADD CONSTRAINT [FK_DeteccionSintoma_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DeteccionSintoma] ADD CONSTRAINT [FK_DeteccionSintoma_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DeteccionSintomaAnimal] ADD CONSTRAINT [FK_DeteccionSintomaAnimal_DeteccionAnimal] 
	FOREIGN KEY ([DeteccionAnimalID]) REFERENCES [DeteccionAnimal] ([DeteccionAnimalID])
; GO

ALTER TABLE [DeteccionSintomaAnimal] ADD CONSTRAINT [FK_DeteccionSintomaAnimal_Sintoma] 
	FOREIGN KEY ([SintomaID]) REFERENCES [Sintoma] ([SintomaID])
; GO

ALTER TABLE [DeteccionSintomaAnimal] ADD CONSTRAINT [FK_DeteccionSintomaAnimal_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DeteccionSintomaAnimal] ADD CONSTRAINT [FK_DeteccionSintomaAnimal_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DiagnosticoAnalista] ADD CONSTRAINT [FK_DiagnosticoAnalista_DeteccionAnimal] 
	FOREIGN KEY ([DeteccionAnimalID]) REFERENCES [DeteccionAnimal] ([DeteccionAnimalID])
; GO

ALTER TABLE [DiagnosticoAnalista] ADD CONSTRAINT [FK_DiagnosticoAnalista_Grado] 
	FOREIGN KEY ([GradoID]) REFERENCES [Grado] ([GradoID])
; GO

ALTER TABLE [DiagnosticoAnalista] ADD CONSTRAINT [FK_DiagnosticoAnalista_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DiagnosticoAnalista] ADD CONSTRAINT [FK_DiagnosticoAnalista_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DiagnosticoAnalistaDetalle] ADD CONSTRAINT [FK_DiagnosticoAnalistaDetalle_DiagnosticoAnalista] 
	FOREIGN KEY ([DiagnosticoAnalistaID]) REFERENCES [DiagnosticoAnalista] ([DiagnosticoAnalistaID])
; GO

ALTER TABLE [DiagnosticoAnalistaDetalle] ADD CONSTRAINT [FK_DiagnosticoAnalistaDetalle_Problema] 
	FOREIGN KEY ([ProblemaID]) REFERENCES [Problema] ([ProblemaID])
; GO

ALTER TABLE [DiagnosticoAnalistaDetalle] ADD CONSTRAINT [FK_DiagnosticoAnalistaDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [DiagnosticoAnalistaDetalle] ADD CONSTRAINT [FK_DiagnosticoAnalistaDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Embarque] ADD CONSTRAINT [FK_Embarque_Estatus] 
	FOREIGN KEY ([Estatus]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [Embarque] ADD CONSTRAINT [FK_Embarque_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Embarque] ADD CONSTRAINT [FK_Embarque_TipoEmbarque] 
	FOREIGN KEY ([TipoEmbarqueID]) REFERENCES [TipoEmbarque] ([TipoEmbarqueID])
; GO

ALTER TABLE [Embarque] ADD CONSTRAINT [FK_Embarque_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Embarque] ADD CONSTRAINT [FK_Embarque_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_Camion] 
	FOREIGN KEY ([CamionID]) REFERENCES [Camion] ([CamionID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_Chofer] 
	FOREIGN KEY ([ChoferID]) REFERENCES [Chofer] ([ChoferID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_Jaula] 
	FOREIGN KEY ([JaulaID]) REFERENCES [Jaula] ([JaulaID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_OrganizacionDestino] 
	FOREIGN KEY ([OrganizacionDestinoID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_OrganizacionOrigen] 
	FOREIGN KEY ([OrganizacionOrigenID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_Embarque] 
	FOREIGN KEY ([EmbarqueID]) REFERENCES [Embarque] ([EmbarqueID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [EmbarqueDetalle] ADD CONSTRAINT [FK_EmbarqueDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Enfermeria] ADD CONSTRAINT [FK_Enfermeria_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Enfermeria] ADD CONSTRAINT [FK_Enfermeria_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Enfermeria] ADD CONSTRAINT [FK_Enfermeria_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EnfermeriaCorral] ADD CONSTRAINT [FK_EnfermeriaCorral_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [EnfermeriaCorral] ADD CONSTRAINT [FK_EnfermeriaCorral_Enfermeria] 
	FOREIGN KEY ([EnfermeriaID]) REFERENCES [Enfermeria] ([EnfermeriaID])
; GO

ALTER TABLE [EnfermeriaCorral] ADD CONSTRAINT [FK_EnfermeriaCorral_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EnfermeriaCorral] ADD CONSTRAINT [FK_EnfermeriaCorral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaCondicion] ADD CONSTRAINT [FK_EntradaCondicion_Condicion] 
	FOREIGN KEY ([CondicionID]) REFERENCES [Condicion] ([CondicionID])
; GO

ALTER TABLE [EntradaCondicion] ADD CONSTRAINT [FK_EntradaCondicion_EntradaGanado] 
	FOREIGN KEY ([EntradaGanadoID]) REFERENCES [EntradaGanado] ([EntradaGanadoID])
; GO

ALTER TABLE [EntradaCondicion] ADD CONSTRAINT [FK_EntradaCondicion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaCondicion] ADD CONSTRAINT [FK_EntradaCondicion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaDetalle] ADD CONSTRAINT [FK_EntradaDetalle_EntradaGanadoCosteo] 
	FOREIGN KEY ([EntradaGanadoCosteoID]) REFERENCES [EntradaGanadoCosteo] ([EntradaGanadoCosteoID])
; GO

ALTER TABLE [EntradaDetalle] ADD CONSTRAINT [FK_EntradaDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaDetalle] ADD CONSTRAINT [FK_EntradaGanadoDetalle_TipoGanado] 
	FOREIGN KEY ([TipoGanadoID]) REFERENCES [TipoGanado] ([TipoGanadoID])
; GO

ALTER TABLE [EntradaDetalle] ADD CONSTRAINT [FK_EntradaDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Camion] 
	FOREIGN KEY ([CamionID]) REFERENCES [Camion] ([CamionID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Chofer] 
	FOREIGN KEY ([ChoferID]) REFERENCES [Chofer] ([ChoferID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Embarque] 
	FOREIGN KEY ([EmbarqueID]) REFERENCES [Embarque] ([EmbarqueID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Jaula] 
	FOREIGN KEY ([JaulaID]) REFERENCES [Jaula] ([JaulaID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [EntradaGanado] ADD CONSTRAINT [FK_EntradaGanado_OrganizacionOrigen] 
	FOREIGN KEY ([OrganizacionOrigenID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EntradaGanadoCalidad] ADD CONSTRAINT [FK_EntradaGanadoCalidad_CalidadGanado] 
	FOREIGN KEY ([CalidadGanadoID]) REFERENCES [CalidadGanado] ([CalidadGanadoID])
; GO

ALTER TABLE [EntradaGanadoCalidad] ADD CONSTRAINT [FK_EntradaGanadoCalidad_EntradaGanado] 
	FOREIGN KEY ([EntradaGanadoID]) REFERENCES [EntradaGanado] ([EntradaGanadoID])
; GO

ALTER TABLE [EntradaGanadoCalidad] ADD CONSTRAINT [FK_EntradaGanadoCalidad_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoCalidad] ADD CONSTRAINT [FK_EntradaGanadoCalidad_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoCosteo] ADD CONSTRAINT [FK_EntradaGanadoCosteo_EntradaGanado] 
	FOREIGN KEY ([EntradaGanadoID]) REFERENCES [EntradaGanado] ([EntradaGanadoID])
; GO

ALTER TABLE [EntradaGanadoCosteo] ADD CONSTRAINT [FK_EntradaGanadoCosteo_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EntradaGanadoCosteo] ADD CONSTRAINT [FK_EntradaGanadoCosteo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoCosteo] ADD CONSTRAINT [FK_EntradaGanadoCosteo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoCosto] ADD CONSTRAINT [FK_EntradaGanadoCostoEntrada_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [EntradaGanadoCosto] ADD CONSTRAINT [FK_EntradaGanadoCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoCosto] ADD CONSTRAINT [FK_EntradaGanadoCosto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoCosto] ADD CONSTRAINT [FK_EntradaGanadoCosto_EntradaGanadoCosteo] 
	FOREIGN KEY ([EntradaGanadoCosteoID]) REFERENCES [EntradaGanadoCosteo] ([EntradaGanadoCosteoID])
; GO

ALTER TABLE [EntradaGanadoMuerte] ADD CONSTRAINT [FK_EntradaGanadoMuerte_EntradaGanado] 
	FOREIGN KEY ([EntradaGanadoID]) REFERENCES [EntradaGanado] ([EntradaGanadoID])
; GO

ALTER TABLE [EntradaGanadoMuerte] ADD CONSTRAINT [FK_EntradaGanadoMuerte_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoMuerte] ADD CONSTRAINT [FK_EntradaGanadoMuerte_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoSobrante] ADD CONSTRAINT [FK_EntradaGanadoSobrante_EntradaGanado] 
	FOREIGN KEY ([EntradaGanadoID]) REFERENCES [EntradaGanado] ([EntradaGanadoID])
; GO

ALTER TABLE [EntradaGanadoSobrante] ADD CONSTRAINT [FK_EntradaGanadoSobrante_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoSobrante] ADD CONSTRAINT [FK_EntradaGanadoSobrante_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoTransito] ADD CONSTRAINT [FK_EntradaGanadoTransito_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [EntradaGanadoTransito] ADD CONSTRAINT [FK_EntradaGanadoTransito_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaGanadoTransito] ADD CONSTRAINT [FK_EntradaGanadoTransito_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaPremezcla] ADD CONSTRAINT [FK_EntradaPremezcla_AlmacenMovimientoIDEntrada] 
	FOREIGN KEY ([AlmacenMovimientoIDEntrada]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [EntradaPremezcla] ADD CONSTRAINT [FK_EntradaPremezcla_AlmacenMovimientoIDSalida] 
	FOREIGN KEY ([AlmacenMovimientoIDSalida]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [EntradaPremezcla] ADD CONSTRAINT [FK_EntradaPremezcla_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaPremezcla] ADD CONSTRAINT [FK_EntradaPremezcla_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_AlmacenInventarioLote] 
	FOREIGN KEY ([AlmacenInventarioLoteID]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_AlmacenMovimientoSalida] 
	FOREIGN KEY ([AlmacenMovimientoSalidaID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_OperadorAlmacen] 
	FOREIGN KEY ([OperadorIDAlmacen]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_OperadorAnalista] 
	FOREIGN KEY ([OperadorIDAnalista]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_OperadorAutoriza] 
	FOREIGN KEY ([OperadorIDAutoriza]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_OperadorBascula] 
	FOREIGN KEY ([OperadorIDBascula]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_RegistroVigilancia] 
	FOREIGN KEY ([RegistroVigilanciaID]) REFERENCES [RegistroVigilancia] ([RegistroVigilanciaID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_TipoContrato] 
	FOREIGN KEY ([TipoContratoID]) REFERENCES [TipoContrato] ([TipoContratoID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProducto] ADD CONSTRAINT [FK_EntradaProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoCosto] ADD CONSTRAINT [FK_EntradaProductoCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [EntradaProductoCosto] ADD CONSTRAINT [FK_EntradaProductoCosto_EntradaProducto] 
	FOREIGN KEY ([EntradaProductoID]) REFERENCES [EntradaProducto] ([EntradaProductoID])
; GO

ALTER TABLE [EntradaProductoCosto] ADD CONSTRAINT [FK_EntradaProductoCosto_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [EntradaProductoCosto] ADD CONSTRAINT [FK_EntradaProductoCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoCosto] ADD CONSTRAINT [FK_EntradaProductoCosto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoDetalle] ADD CONSTRAINT [FK_EntradaProductoDetalle_EntradaProducto] 
	FOREIGN KEY ([EntradaProductoID]) REFERENCES [EntradaProducto] ([EntradaProductoID])
; GO

ALTER TABLE [EntradaProductoDetalle] ADD CONSTRAINT [FK_EntradaProductoDetalle_Indicador] 
	FOREIGN KEY ([IndicadorID]) REFERENCES [Indicador] ([IndicadorID])
; GO

ALTER TABLE [EntradaProductoDetalle] ADD CONSTRAINT [FK_EntradaProductoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoDetalle] ADD CONSTRAINT [FK_EntradaProductoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoMuestra] ADD CONSTRAINT [FK_EntradaProductoMuestra_EntradaProductoDetalle] 
	FOREIGN KEY ([EntradaProductoDetalleID]) REFERENCES [EntradaProductoDetalle] ([EntradaProductoDetalleID])
; GO

ALTER TABLE [EntradaProductoMuestra] ADD CONSTRAINT [FK_EntradaProductoMuestra_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoMuestra] ADD CONSTRAINT [FK_EntradaProductoMuestra_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoParcial] ADD CONSTRAINT [FK_EntradaProductoParcial_ContratoParcial] 
	FOREIGN KEY ([ContratoParcialID]) REFERENCES [ContratoParcial] ([ContratoParcialID])
; GO

ALTER TABLE [EntradaProductoParcial] ADD CONSTRAINT [FK_EntradaProductoParcial_EntradaProducto] 
	FOREIGN KEY ([EntradaProductoID]) REFERENCES [EntradaProducto] ([EntradaProductoID])
; GO

ALTER TABLE [EntradaProductoParcial] ADD CONSTRAINT [FK_EntradaProductoParcial_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EntradaProductoParcial] ADD CONSTRAINT [FK_EntradaProductoParcial_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EstadoComedero] ADD CONSTRAINT [FK_EstadoComedero_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EstadoComedero] ADD CONSTRAINT [FK_EstadoComedero_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EstadoImplante] ADD CONSTRAINT [FK_EstadoImplante_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EstadoImplante] ADD CONSTRAINT [FK_EstadoImplante_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EstandarDescarga] ADD CONSTRAINT [FK_EstandarDescarga_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EstandarDescarga] ADD CONSTRAINT [FK_EstandarDescarga_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [EstandarDescarga] ADD CONSTRAINT [FK_EstandarDescarga_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EstandarDescarga] ADD CONSTRAINT [FK_EstandarDescarga_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Estatus] ADD CONSTRAINT [FK_Estatus_TipoEstatus] 
	FOREIGN KEY ([TipoEstatus]) REFERENCES [TipoEstatus] ([TipoEstatusID])
; GO

ALTER TABLE [Estatus] ADD CONSTRAINT [FK_Estatus_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Estatus] ADD CONSTRAINT [FK_Estatus_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EvaluacionCorral] ADD CONSTRAINT [FK_EvaluacionCorral_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [EvaluacionCorral] ADD CONSTRAINT [FK_EvaluacionCorral_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [EvaluacionCorral] ADD CONSTRAINT [FK_EvaluacionCorral_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [EvaluacionCorral] ADD CONSTRAINT [FK_EvaluacionCorral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EvaluacionCorral] ADD CONSTRAINT [FK_EvaluacionCorral_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EvaluacionCorralDetalle] ADD CONSTRAINT [FK_EvaluacionCorralDetalle_EvaluacionCorral] 
	FOREIGN KEY ([EvaluacionCorralID]) REFERENCES [EvaluacionCorral] ([EvaluacionID])
; GO

ALTER TABLE [EvaluacionCorralDetalle] ADD CONSTRAINT [FK_EvaluacionCorralDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [EvaluacionCorralDetalle] ADD CONSTRAINT [FK_EvaluacionCorralDetalle_Pregunta] 
	FOREIGN KEY ([PreguntaID]) REFERENCES [Pregunta] ([PreguntaID])
; GO

ALTER TABLE [EvaluacionCorralDetalle] ADD CONSTRAINT [FK_EvaluacionCorralDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Familia] ADD CONSTRAINT [FK_Familia_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Familia] ADD CONSTRAINT [FK_Familia_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [FK_Flete_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [FK_Flete_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [FK_Flete_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [FK_Flete_TipoTarifa] 
	FOREIGN KEY ([TipoTarifaID]) REFERENCES [TipoTarifa] ([TipoTarifaID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [FK_Flete_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Flete] ADD CONSTRAINT [FK_Flete_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteDetalle] ADD CONSTRAINT [FK_FleteDetalle_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [FleteDetalle] ADD CONSTRAINT [FK_FleteDetalle_Flete] 
	FOREIGN KEY ([FleteID]) REFERENCES [Flete] ([FleteID])
; GO

ALTER TABLE [FleteDetalle] ADD CONSTRAINT [FK_FleteDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteDetalle] ADD CONSTRAINT [FK_FleteDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_AlmacenIDDestino] 
	FOREIGN KEY ([AlmacenIDDestino]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_AlmacenIDOrigen] 
	FOREIGN KEY ([AlmacenIDOrigen]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteInterno] ADD CONSTRAINT [FK_FleteInterno_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteInternoCosto] ADD CONSTRAINT [FK_FleteInternoCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [FleteInternoCosto] ADD CONSTRAINT [FK_FleteInternoCosto_FleteInternoDetalle] 
	FOREIGN KEY ([FleteInternoDetalleID]) REFERENCES [FleteInternoDetalle] ([FleteInternoDetalleID])
; GO

ALTER TABLE [FleteInternoCosto] ADD CONSTRAINT [FK_FleteInternoCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteInternoCosto] ADD CONSTRAINT [FK_FleteInternoCosto_UsuarioModificar] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteInternoDetalle] ADD CONSTRAINT [FK_FleteInternoDetalle_FleteInterno] 
	FOREIGN KEY ([FleteInternoID]) REFERENCES [FleteInterno] ([FleteInternoID])
; GO

ALTER TABLE [FleteInternoDetalle] ADD CONSTRAINT [FK_FleteInternoDetalle_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [FleteInternoDetalle] ADD CONSTRAINT [FK_FleteInternoDetalle_TipoTarifa] 
	FOREIGN KEY ([TipoTarifaID]) REFERENCES [TipoTarifa] ([TipoTarifaID])
; GO

ALTER TABLE [FleteInternoDetalle] ADD CONSTRAINT [FK_FleteInternoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteInternoDetalle] ADD CONSTRAINT [FK_FleteInternoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteMermaPermitida] ADD CONSTRAINT [FK_FleteMermaPermitida_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [FleteMermaPermitida] ADD CONSTRAINT [FK_FleteMermaPermitida_SubFamilia] 
	FOREIGN KEY ([SubFamiliaID]) REFERENCES [SubFamilia] ([SubFamiliaID])
; GO

ALTER TABLE [FleteMermaPermitida] ADD CONSTRAINT [FK_FleteMermaPermitida_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [FleteMermaPermitida] ADD CONSTRAINT [FK_FleteMermaPermitida_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Folio] ADD CONSTRAINT [FK_Folio_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Folio] ADD CONSTRAINT [FK_Folio_TipoFolio] 
	FOREIGN KEY ([TipoFolioID]) REFERENCES [TipoFolio] ([TipoFolioID])
; GO

ALTER TABLE [FolioAlmacen] ADD CONSTRAINT [FK_FolioAlmacen_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [FolioAlmacen] ADD CONSTRAINT [FK_FolioAlmacen_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [FolioLoteProducto] ADD CONSTRAINT [FK_FolioLoteProducto_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [FolioLoteProducto] ADD CONSTRAINT [FK_FolioLoteProducto_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Formula] ADD CONSTRAINT [FK_Formula_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Formula] ADD CONSTRAINT [FK_Formula_TipoFormula] 
	FOREIGN KEY ([TipoFormulaID]) REFERENCES [TipoFormula] ([TipoFormulaID])
; GO

ALTER TABLE [Formula] ADD CONSTRAINT [FK_Formula_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Formula] ADD CONSTRAINT [FK_Formula_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Formulario] ADD CONSTRAINT [FK_Formulario_Modulo] 
	FOREIGN KEY ([ModuloID]) REFERENCES [Modulo] ([ModuloID])
; GO

ALTER TABLE [Formulario] ADD CONSTRAINT [FK_Formulario_Padre] 
	FOREIGN KEY ([PadreID]) REFERENCES [Padre] ([PadreID])
; GO

ALTER TABLE [GastoCentroCosto] ADD CONSTRAINT [FK_GastoCentroCosto_CentroCosto] 
	FOREIGN KEY ([CentroCostoID]) REFERENCES [CentroCosto] ([CentroCostoID])
; GO

ALTER TABLE [GastoCentroCosto] ADD CONSTRAINT [FK_GastoCentroCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [GastoCentroCosto] ADD CONSTRAINT [FK_GastoCentroCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GastoCentroCosto] ADD CONSTRAINT [FK_GastoCentroCosto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [FK_GastoInventario_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [FK_GastoInventario_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [FK_GastoInventario_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [FK_GastoInventario_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [FK_GastoInventario_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GastoInventario] ADD CONSTRAINT [FK_GastoInventario_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_AlmacenInventarioLote] 
	FOREIGN KEY ([AlmacenInventarioLoteID]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GastoMateriaPrima] ADD CONSTRAINT [FK_GastoMateriaPrima_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Gelatinizacion] ADD CONSTRAINT [FK_Gelatinizacion_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Gelatinizacion] ADD CONSTRAINT [FK_Gelatinizacion_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Gelatinizacion] ADD CONSTRAINT [FK_Gelatinizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Gelatinizacion] ADD CONSTRAINT [FK_Gelatinizacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GelatinizacionDetalle] ADD CONSTRAINT [FK_GelatinizacionDetalle_Gelatinizacion] 
	FOREIGN KEY ([GelatinizacionID]) REFERENCES [Gelatinizacion] ([GelatinizacionID])
; GO

ALTER TABLE [GelatinizacionDetalle] ADD CONSTRAINT [FK_GelatinizacionDetalle_Roladora] 
	FOREIGN KEY ([RoladoraID]) REFERENCES [Roladora] ([RoladoraID])
; GO

ALTER TABLE [GelatinizacionDetalle] ADD CONSTRAINT [FK_GelatinizacionDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GelatinizacionDetalle] ADD CONSTRAINT [FK_GelatinizacionDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Grado] ADD CONSTRAINT [FK_Grado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Grado] ADD CONSTRAINT [FK_Grado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GrupoCorral] ADD CONSTRAINT [FK_GrupoCorral_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GrupoCorral] ADD CONSTRAINT [FK_GrupoCorral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [GrupoFormulario] ADD CONSTRAINT [FK_GrupoFormulario_Acceso] 
	FOREIGN KEY ([AccesoID]) REFERENCES [Acceso] ([AccesoID])
; GO

ALTER TABLE [GrupoFormulario] ADD CONSTRAINT [FK_GrupoFormulario_Formulario] 
	FOREIGN KEY ([FormularioID]) REFERENCES [Formulario] ([FormularioID])
; GO

ALTER TABLE [GrupoFormulario] ADD CONSTRAINT [FK_GrupoFormulario_Grupo] 
	FOREIGN KEY ([GrupoID]) REFERENCES [Grupo] ([GrupoID])
; GO

ALTER TABLE [Indicador] ADD CONSTRAINT [FK_Indicador_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Indicador] ADD CONSTRAINT [FK_Indicador_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorObjetivo] ADD CONSTRAINT [FK_IndicadorObjetivo_IndicadorProductoCalidad] 
	FOREIGN KEY ([IndicadorProductoCalidadID]) REFERENCES [IndicadorProductoCalidad] ([IndicadorProductoCalidadID])
; GO

ALTER TABLE [IndicadorObjetivo] ADD CONSTRAINT [FK_IndicadorObjetivo_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [IndicadorObjetivo] ADD CONSTRAINT [FK_IndicadorObjetivo_TipoObjetivoCalidad] 
	FOREIGN KEY ([TipoObjetivoCalidadID]) REFERENCES [TipoObjetivoCalidad] ([TipoObjetivoCalidadID])
; GO

ALTER TABLE [IndicadorObjetivo] ADD CONSTRAINT [FK_IndicadorObjetivo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorObjetivo] ADD CONSTRAINT [FK_IndicadorObjetivo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorProducto] ADD CONSTRAINT [FK_IndicadorProducto_Indicador] 
	FOREIGN KEY ([IndicadorID]) REFERENCES [Indicador] ([IndicadorID])
; GO

ALTER TABLE [IndicadorProducto] ADD CONSTRAINT [FK_IndicadorProducto_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [IndicadorProducto] ADD CONSTRAINT [FK_IndicadorProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorProducto] ADD CONSTRAINT [FK_IndicadorProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorProductoBoleta] ADD CONSTRAINT [FK_IndicadorProductoBoleta_IndicadorProducto] 
	FOREIGN KEY ([IndicadorProductoID]) REFERENCES [IndicadorProducto] ([IndicadorProductoID])
; GO

ALTER TABLE [IndicadorProductoBoleta] ADD CONSTRAINT [FK_IndicadorProductoBoleta_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [IndicadorProductoBoleta] ADD CONSTRAINT [FK_IndicadorProductoBoleta_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorProductoBoleta] ADD CONSTRAINT [FK_IndicadorProductoBoleta_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorProductoCalidad] ADD CONSTRAINT [FK_IndicadorProductoCalidad_Indicador] 
	FOREIGN KEY ([IndicadorID]) REFERENCES [Indicador] ([IndicadorID])
; GO

ALTER TABLE [IndicadorProductoCalidad] ADD CONSTRAINT [FK_IndicadorProductoCalidad_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [IndicadorProductoCalidad] ADD CONSTRAINT [FK_IndicadorProductoCalidad_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [IndicadorProductoCalidad] ADD CONSTRAINT [FK_IndicadorProductoCalidad_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Ingrediente] ADD CONSTRAINT [FK_Ingrediente_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [Ingrediente] ADD CONSTRAINT [FK_Ingrediente_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Ingrediente] ADD CONSTRAINT [FK_Ingrediente_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Ingrediente] ADD CONSTRAINT [FK_Ingrediente_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Ingrediente] ADD CONSTRAINT [FK_Ingrediente_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [InterfaceCosto] ADD CONSTRAINT [FK_InterfaceCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [InterfaceCosto] ADD CONSTRAINT [FK_InterfaceCosto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [InterfaceCosto] ADD CONSTRAINT [FK_InterfaceCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [InterfaceOrganizacion] ADD CONSTRAINT [FK_InterfaceOrganizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [InterfaceOrganizacion] ADD CONSTRAINT [FK_InterfaceOrganizacion_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceOrganizacion] ADD CONSTRAINT [FK_InterfaceOrganizacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [InterfaceSalida] ADD CONSTRAINT [FK_InterfaceSalida_Organizacion] 
	FOREIGN KEY ([OrganizacionDestinoID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceSalida] ADD CONSTRAINT [FK_InterfaceSalida_OrganizacionOrigen] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceSalidaAnimal] ADD CONSTRAINT [FK_InterfaceSalidaAnimal_InterfaceSalidaDetalle] 
	FOREIGN KEY ([OrganizacionID], [SalidaID], [TipoGanadoID]) REFERENCES [InterfaceSalidaDetalle] ([OrganizacionID], [SalidaID], [TipoGanadoID])
; GO

ALTER TABLE [InterfaceSalidaAnimal] ADD CONSTRAINT [FK_InterfaceSalidaAnimal_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceSalidaCosto] ADD CONSTRAINT [FK_InterfaceSalidaCosto_Costo] 
	FOREIGN KEY ([CostoID]) REFERENCES [Costo] ([CostoID])
; GO

ALTER TABLE [InterfaceSalidaCosto] ADD CONSTRAINT [FK_InterfaceSalidaCosto_InterfaceSalidaAnimal] 
	FOREIGN KEY ([OrganizacionID], [SalidaID], [Arete], [FechaCompra]) REFERENCES [InterfaceSalidaAnimal] ([OrganizacionID], [SalidaID], [Arete], [FechaCompra])
; GO

ALTER TABLE [InterfaceSalidaDetalle] ADD CONSTRAINT [FK_InterfaceSalidaDetalle_InterfaceSalida] 
	FOREIGN KEY ([OrganizacionID], [SalidaID]) REFERENCES [InterfaceSalida] ([OrganizacionID], [SalidaID])
; GO

ALTER TABLE [InterfaceSalidaDetalle] ADD CONSTRAINT [FK_InterfaceSalidaDetalle_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [FK_InterfaceSalidaTraspaso_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [FK_InterfaceSalidaTraspaso_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [FK_InterfaceSalidaTraspaso_OrganizacionIDDestino] 
	FOREIGN KEY ([OrganizacionIDDestino]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [FK_InterfaceSalidaTraspaso_TipoGanado] 
	FOREIGN KEY ([TipoGanadoID]) REFERENCES [TipoGanado] ([TipoGanadoID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [FK_InterfaceSalidaTraspaso_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [InterfaceSalidaTraspaso] ADD CONSTRAINT [FK_InterfaceSalidaTraspaso_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Iva] ADD CONSTRAINT [FK_Iva_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Jaula] ADD CONSTRAINT [FK_Jaula_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [Jaula] ADD CONSTRAINT [FK_Jaula_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Jaula] ADD CONSTRAINT [FK_Jaula_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LectorRegistro] ADD CONSTRAINT [FK_LectorRegistro_EstadoComedero] 
	FOREIGN KEY ([EstadoComederoID]) REFERENCES [EstadoComedero] ([EstadoComederoID])
; GO

ALTER TABLE [LectorRegistro] ADD CONSTRAINT [FK_LectorRegistro_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [LectorRegistro] ADD CONSTRAINT [FK_LectorRegistro_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [LectorRegistro] ADD CONSTRAINT [FK_LectorRegistro_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LectorRegistro] ADD CONSTRAINT [FK_LectorRegistro_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [FK_LectorRegistroDetalle_FormulaAnterior] 
	FOREIGN KEY ([FormulaIDAnterior]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [FK_LectorRegistroDetalle_FormulaProgramada] 
	FOREIGN KEY ([FormulaIDProgramada]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [FK_LectorRegistroDetalle_LectorRegistro] 
	FOREIGN KEY ([LectorRegistroID]) REFERENCES [LectorRegistro] ([LectorRegistroID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [FK_LectorRegistroDetalle_TipoServicio] 
	FOREIGN KEY ([TipoServicioID]) REFERENCES [TipoServicio] ([TipoServicioID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [FK_LectorRegistroDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LectorRegistroDetalle] ADD CONSTRAINT [FK_LectorRegistroDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Liquidacion] ADD CONSTRAINT [FK_Liquidacion_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [Liquidacion] ADD CONSTRAINT [FK_Liquidacion_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Liquidacion] ADD CONSTRAINT [FK_Liquidacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Liquidacion] ADD CONSTRAINT [FK_Liquidacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LiquidacionDetalle] ADD CONSTRAINT [FK_LiquidacionDetalle_EntradaProducto] 
	FOREIGN KEY ([EntradaProductoID]) REFERENCES [EntradaProducto] ([EntradaProductoID])
; GO

ALTER TABLE [LiquidacionDetalle] ADD CONSTRAINT [FK_LiquidacionDetalle_Liquidacion] 
	FOREIGN KEY ([LiquidacionID]) REFERENCES [Liquidacion] ([LiquidacionID])
; GO

ALTER TABLE [LiquidacionDetalle] ADD CONSTRAINT [FK_LiquidacionDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LiquidacionDetalle] ADD CONSTRAINT [FK_LiquidacionDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Lote] ADD CONSTRAINT [FK_Lote_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [Lote] ADD CONSTRAINT [FK_Lote_TipoCorral] 
	FOREIGN KEY ([TipoCorralID]) REFERENCES [TipoCorral] ([TipoCorralID])
; GO

ALTER TABLE [Lote] ADD CONSTRAINT [FK_Lote_TipoProceso] 
	FOREIGN KEY ([TipoProcesoID]) REFERENCES [TipoProceso] ([TipoProcesoID])
; GO

ALTER TABLE [LoteDistribucionAlimento] ADD CONSTRAINT [FK_LoteDistribucionAlimento_Estatus] 
	FOREIGN KEY ([EstatusDistribucionID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [LoteDistribucionAlimento] ADD CONSTRAINT [FK_LoteDistribucionAlimento_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [LoteDistribucionAlimento] ADD CONSTRAINT [FK_LoteDistribucionAlimento_TipoServicio] 
	FOREIGN KEY ([TipoServicioID]) REFERENCES [TipoServicio] ([TipoServicioID])
; GO

ALTER TABLE [LoteDistribucionAlimento] ADD CONSTRAINT [FK_LoteDistribucionAlimento_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteDistribucionAlimento] ADD CONSTRAINT [FK_LoteDistribucionAlimento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteProyeccion] ADD CONSTRAINT [FK_LoteProyeccion_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [LoteProyeccion] ADD CONSTRAINT [FK_LoteProyeccion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteProyeccion] ADD CONSTRAINT [FK_LoteProyeccion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteReimplante] ADD CONSTRAINT [FK_LoteReimplante_LoteProyeccion] 
	FOREIGN KEY ([LoteProyeccionID]) REFERENCES [LoteProyeccion] ([LoteProyeccionID])
; GO

ALTER TABLE [LoteReimplante] ADD CONSTRAINT [FK_LoteReimplante_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteReimplante] ADD CONSTRAINT [FK_LoteReimplante_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteSacrificio] ADD CONSTRAINT [FK_LoteSacrificio_Cliente] 
	FOREIGN KEY ([ClienteID]) REFERENCES [Cliente] ([ClienteID])
; GO

ALTER TABLE [LoteSacrificio] ADD CONSTRAINT [FK_LoteSacrificio_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [LoteSacrificio] ADD CONSTRAINT [FK_LoteSacrificio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteSacrificio] ADD CONSTRAINT [FK_LoteSacrificio_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [LoteSacrificioDetalle] ADD CONSTRAINT [FK_LoteSacrificioDetalle_LoteSacrificio] 
	FOREIGN KEY ([LoteSacrificioID]) REFERENCES [LoteSacrificio] ([LoteSacrificioID])
; GO

ALTER TABLE [MermaSuperavit] ADD CONSTRAINT [FK_MermasSuperavit_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [MermaSuperavit] ADD CONSTRAINT [FK_MermasSuperavit_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MermaSuperavit] ADD CONSTRAINT [FK_MermasSuperavit_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Mezcladora] ADD CONSTRAINT [FK_Mezcladora_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Mezcladora] ADD CONSTRAINT [FK_Mezcladora_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Mezcladora] ADD CONSTRAINT [FK_Mezcladora_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Moneda] ADD CONSTRAINT [FK_Moneda_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Moneda] ADD CONSTRAINT [FK_Moneda_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MonitoreoSilo] ADD CONSTRAINT [FK_MonitoreoSilo_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [MonitoreoSilo] ADD CONSTRAINT [FK_MonitoreoSilo_Silo] 
	FOREIGN KEY ([SiloID]) REFERENCES [Silo] ([SiloID])
; GO

ALTER TABLE [MonitoreoSilo] ADD CONSTRAINT [FK_MonitoreoSilo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MonitoreoSilo] ADD CONSTRAINT [FK_MonitoreoSilo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MonitoreoSiloDetalle] ADD CONSTRAINT [FK_MonitoreoSiloDetalle_MonitoreoSilo] 
	FOREIGN KEY ([MonitoreoSiloID]) REFERENCES [MonitoreoSilo] ([MonitoreoSiloID])
; GO

ALTER TABLE [MonitoreoSiloDetalle] ADD CONSTRAINT [FK_MonitoreoSiloDetalle_MonitoreoSiloIndicador] 
	FOREIGN KEY ([MonitoreoSiloIndicadorID]) REFERENCES [MonitoreoSiloIndicador] ([MonitoreoSiloIndicadorID])
; GO

ALTER TABLE [MonitoreoSiloDetalle] ADD CONSTRAINT [FK_MonitoreoSiloDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MonitoreoSiloDetalle] ADD CONSTRAINT [FK_MonitoreoSiloDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MonitoreoSiloIndicador] ADD CONSTRAINT [FK_MonitoreoSiloIndicador_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [MonitoreoSiloIndicador] ADD CONSTRAINT [FK_MonitoreoSiloIndicador_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MonitoreoSiloIndicador] ADD CONSTRAINT [FK_MonitoreoSiloIndicador_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_Operador] 
	FOREIGN KEY ([OperadorCancelacion]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_OperadorDeteccion] 
	FOREIGN KEY ([OperadorDeteccion]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_OperadorNecropsia] 
	FOREIGN KEY ([OperadorNecropsia]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_OperadorRecoleccion] 
	FOREIGN KEY ([OperadorRecoleccion]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_Problema] 
	FOREIGN KEY ([ProblemaID]) REFERENCES [Problema] ([ProblemaID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Muertes] ADD CONSTRAINT [FK_Muertes_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MuestreoFibraFormula] ADD CONSTRAINT [FK_MuestreoFibraFormula_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [MuestreoFibraFormula] ADD CONSTRAINT [FK_MuestreoFibraFormula_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [MuestreoFibraFormula] ADD CONSTRAINT [FK_MuestreoFibraFormula_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MuestreoFibraFormula] ADD CONSTRAINT [FK_MuestreoFibraFormula_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MuestreoFibraIngrediente] ADD CONSTRAINT [FK_MuestreoFibraIngrediente_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [MuestreoFibraIngrediente] ADD CONSTRAINT [FK_MuestreoFibraIngrediente_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [MuestreoFibraIngrediente] ADD CONSTRAINT [FK_MuestreoFibraIngrediente_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [MuestreoFibraIngrediente] ADD CONSTRAINT [FK_MuestreoFibraIngrediente_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Notificaciones] ADD CONSTRAINT [FK_Notificaciones_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Notificaciones] ADD CONSTRAINT [FK_Notificaciones_AccionesSIAP] 
	FOREIGN KEY ([AccionesSiapID]) REFERENCES [AccionesSIAP] ([AccionesSiapID])
; GO

ALTER TABLE [Notificaciones] ADD CONSTRAINT [FK_Notificaciones_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Observacion] ADD CONSTRAINT [FK_Observacion_TipoObservacion] 
	FOREIGN KEY ([TipoObservacionID]) REFERENCES [TipoObservacion] ([TipoObservacionID])
; GO

ALTER TABLE [Observacion] ADD CONSTRAINT [FK_Observacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Observacion] ADD CONSTRAINT [FK_Observacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Operador] ADD CONSTRAINT [FK_Operador_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Operador] ADD CONSTRAINT [FK_Operador_Rol] 
	FOREIGN KEY ([RolID]) REFERENCES [Rol] ([RolID])
; GO

ALTER TABLE [Operador] ADD CONSTRAINT [FK_Operador_Usuario] 
	FOREIGN KEY ([UsuarioID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Operador] ADD CONSTRAINT [FK_Operador_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Operador] ADD CONSTRAINT [FK_Operador_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [OrdenSacrificio] ADD CONSTRAINT [FK_OrdenSacrificio_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [OrdenSacrificio] ADD CONSTRAINT [FK_OrdenSacrificio_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [OrdenSacrificio] ADD CONSTRAINT [FK_OrdenSacrificio_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [OrdenSacrificio] ADD CONSTRAINT [FK_OrdenSacrificio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [OrdenSacrificioDetalle] ADD CONSTRAINT [FK_OrdenSacrificioDetalle_Corral] 
	FOREIGN KEY ([CorraletaID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [OrdenSacrificioDetalle] ADD CONSTRAINT [FK_OrdenSacrificioDetalle_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [OrdenSacrificioDetalle] ADD CONSTRAINT [FK_OrdenSacrificioDetalle_OrdenSacrificio] 
	FOREIGN KEY ([OrdenSacrificioID]) REFERENCES [OrdenSacrificio] ([OrdenSacrificioID])
; GO

ALTER TABLE [OrdenSacrificioDetalle] ADD CONSTRAINT [FK_OrdenSacrificioDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [OrdenSacrificioDetalle] ADD CONSTRAINT [FK_OrdenSacrificioDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Organizacion] ADD CONSTRAINT [FK_Organizacion_Iva] 
	FOREIGN KEY ([IvaID]) REFERENCES [Iva] ([IvaID])
; GO

ALTER TABLE [Organizacion] ADD CONSTRAINT [FK_Organizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Organizacion] ADD CONSTRAINT [FK_Organizacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Parametro] ADD CONSTRAINT [FK_Parametro_TipoParametro] 
	FOREIGN KEY ([TipoParametroID]) REFERENCES [TipoParametro] ([TipoParametroID])
; GO

ALTER TABLE [Parametro] ADD CONSTRAINT [FK_Parametro_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Parametro] ADD CONSTRAINT [FK_Parametro_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ParametroGeneral] ADD CONSTRAINT [FK_ParametroGeneral_Parametro] 
	FOREIGN KEY ([ParametroID]) REFERENCES [Parametro] ([ParametroID])
; GO

ALTER TABLE [ParametroGeneral] ADD CONSTRAINT [FK_ParametroGeneral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ParametroGeneral] ADD CONSTRAINT [FK_ParametroGeneral_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ParametroOrganizacion] ADD CONSTRAINT [FK_ParametroOrganizacion_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ParametroOrganizacion] ADD CONSTRAINT [FK_ParametroOrganizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ParametroOrganizacion] ADD CONSTRAINT [FK_ParametroOrganizacion_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ParametroOrganizacion] ADD CONSTRAINT [FK_ParametroOrganizacion_Parametro] 
	FOREIGN KEY ([ParametroID]) REFERENCES [Parametro] ([ParametroID])
; GO

ALTER TABLE [ParametroTrampa] ADD CONSTRAINT [FK_ParametroTrampa_Parametro] 
	FOREIGN KEY ([ParametroID]) REFERENCES [Parametro] ([ParametroID])
; GO

ALTER TABLE [ParametroTrampa] ADD CONSTRAINT [FK_ParametroTrampa_Trampa] 
	FOREIGN KEY ([TrampaID]) REFERENCES [Trampa] ([TrampaID])
; GO

ALTER TABLE [ParametroTrampa] ADD CONSTRAINT [FK_ParametroTrampa_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ParametroTrampa] ADD CONSTRAINT [FK_ParametroTrampa_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PedidoDetalle] ADD CONSTRAINT [FK_PedidoDetalle_AlmacenInventarioLote] 
	FOREIGN KEY ([InventarioLoteIDDestino]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [PedidoDetalle] ADD CONSTRAINT [FK_PedidoDetalle_Pedido] 
	FOREIGN KEY ([PedidoID]) REFERENCES [Pedido] ([PedidoID])
; GO

ALTER TABLE [PedidoDetalle] ADD CONSTRAINT [FK_PedidoDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [PedidoDetalle] ADD CONSTRAINT [FK_PedidoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PedidoDetalle] ADD CONSTRAINT [FK_PedidoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_AlmacenMovimientoDestino] 
	FOREIGN KEY ([AlmacenMovimientoDestinoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_AlmacenMovimientoOrigen] 
	FOREIGN KEY ([AlmacenMovimientoOrigenID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_Camion] 
	FOREIGN KEY ([CamionID]) REFERENCES [Camion] ([CamionID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_ProgramacionMateriaPrima] 
	FOREIGN KEY ([ProgramacionMateriaPrimaID]) REFERENCES [ProgramacionMateriaPrima] ([ProgramacionMateriaPrimaID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_ProveedorChofer] 
	FOREIGN KEY ([ProveedorChoferID]) REFERENCES [ProveedorChofer] ([ProveedorChoferID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_TipoPesaje] 
	FOREIGN KEY ([TipoPesajeID]) REFERENCES [TipoPesaje] ([TipoPesajeID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PesajeMateriaPrima] ADD CONSTRAINT [FK_PesajeMateriaPrima_UsuarioSurtido] 
	FOREIGN KEY ([UsuarioIDSurtido]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PrecioGanado] ADD CONSTRAINT [FK_PrecioGanado_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [PrecioGanado] ADD CONSTRAINT [FK_PrecioGanado_TipoGanado] 
	FOREIGN KEY ([TipoGanadoID]) REFERENCES [TipoGanado] ([TipoGanadoID])
; GO

ALTER TABLE [PrecioGanado] ADD CONSTRAINT [FK_PrecioGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PrecioGanado] ADD CONSTRAINT [FK_PrecioGanado_UsuarioModifficacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PrecioPAC] ADD CONSTRAINT [FK_PrecioPAC_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [PrecioPAC] ADD CONSTRAINT [FK_PrecioPAC_TipoPAC] 
	FOREIGN KEY ([TipoPACID]) REFERENCES [TipoPAC] ([TipoPACID])
; GO

ALTER TABLE [PrecioPAC] ADD CONSTRAINT [FK_PrecioPAC_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PrecioPAC] ADD CONSTRAINT [FK_PrecioPAC_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Pregunta] ADD CONSTRAINT [FK_Pregunta_TipoPregunta] 
	FOREIGN KEY ([TipoPreguntaID]) REFERENCES [TipoPregunta] ([TipoPreguntaID])
; GO

ALTER TABLE [Pregunta] ADD CONSTRAINT [FK_Pregunta_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Premezcla] ADD CONSTRAINT [FK_Premezcla_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Premezcla] ADD CONSTRAINT [FK_Premezcla_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Premezcla] ADD CONSTRAINT [FK_Premezcla_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Premezcla] ADD CONSTRAINT [FK_Premezcla_UsuarioModificar] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PremezclaDetalle] ADD CONSTRAINT [FK_PremezclaDetalle_Premezcla] 
	FOREIGN KEY ([PremezclaID]) REFERENCES [Premezcla] ([PremezclaID])
; GO

ALTER TABLE [PremezclaDetalle] ADD CONSTRAINT [FK_PremezclaDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [PremezclaDetalle] ADD CONSTRAINT [FK_PremezclaDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PremezclaDetalle] ADD CONSTRAINT [FK_PremezclaDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [PremezclaDistribucion] ADD CONSTRAINT [FK_PremezclaDistribucion_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [PremezclaDistribucion] ADD CONSTRAINT [FK_PremezclaDistribucion_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [Problema] ADD CONSTRAINT [FK_Problema_TipoProblema] 
	FOREIGN KEY ([TipoProblemaID]) REFERENCES [TipoProblema] ([TipoProblemaID])
; GO

ALTER TABLE [Problema] ADD CONSTRAINT [FK_Problema_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Problema] ADD CONSTRAINT [FK_Problema_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProblemaSintoma] ADD CONSTRAINT [FK_ProblemaSintoma_Problema] 
	FOREIGN KEY ([ProblemaID]) REFERENCES [Problema] ([ProblemaID])
; GO

ALTER TABLE [ProblemaSintoma] ADD CONSTRAINT [FK_ProblemaSintoma_Sintoma] 
	FOREIGN KEY ([SintomaID]) REFERENCES [Sintoma] ([SintomaID])
; GO

ALTER TABLE [ProblemaSintoma] ADD CONSTRAINT [FK_ProblemaSintoma_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProblemaTratamiento] ADD CONSTRAINT [FK_ProblemaTratamiento_Problema] 
	FOREIGN KEY ([ProblemaID]) REFERENCES [Problema] ([ProblemaID])
; GO

ALTER TABLE [ProblemaTratamiento] ADD CONSTRAINT [FK_ProblemaTratamiento_Tratamiento] 
	FOREIGN KEY ([TratamientoID]) REFERENCES [Tratamiento] ([TratamientoID])
; GO

ALTER TABLE [ProblemaTratamiento] ADD CONSTRAINT [FK_ProblemaTratamiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionDiaria] ADD CONSTRAINT [FK_ProduccionDiaria_UsuarioAutorizo] 
	FOREIGN KEY ([UsuarioIDAutorizo]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionDiaria] ADD CONSTRAINT [FK_ProduccionDiaria_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionDiaria] ADD CONSTRAINT [FK_ProduccionDiaria_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionDiariaDetalle] ADD CONSTRAINT [FK_ProduccionDiariaDetalle_PesajeMateriaPrima] 
	FOREIGN KEY ([PesajeMateriaPrimaID]) REFERENCES [PesajeMateriaPrima] ([PesajeMateriaPrimaID])
; GO

ALTER TABLE [ProduccionDiariaDetalle] ADD CONSTRAINT [FK_ProduccionDiariaDetalle_ProduccionDiaria] 
	FOREIGN KEY ([ProduccionDiariaID]) REFERENCES [ProduccionDiaria] ([ProduccionDiariaID])
; GO

ALTER TABLE [ProduccionDiariaDetalle] ADD CONSTRAINT [FK_ProduccionDiariaDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [ProduccionDiariaDetalle] ADD CONSTRAINT [FK_ProduccionDiariaDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionDiariaDetalle] ADD CONSTRAINT [FK_ProduccionDiariaDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [FK_ProduccionFormula_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [FK_ProduccionFormula_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [FK_ProduccionFormula_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [FK_ProduccionFormula_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [FK_ProduccionFormula_AlmacenMovimientoEntrada] 
	FOREIGN KEY ([AlmacenMovimientoEntradaID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [ProduccionFormula] ADD CONSTRAINT [FK_ProduccionFormula_AlmacenMovimientoSalida] 
	FOREIGN KEY ([AlmacenMovimientoSalidaID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_ProduccionFormula] 
	FOREIGN KEY ([ProduccionFormulaID]) REFERENCES [ProduccionFormula] ([ProduccionFormulaID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_Rotomix] 
	FOREIGN KEY ([RotomixID]) REFERENCES [Rotomix] ([RotomixID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormulaBatch] ADD CONSTRAINT [FK_ProduccionFormulaBatch_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormulaDetalle] ADD CONSTRAINT [FK_ProduccionFormulaDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormulaDetalle] ADD CONSTRAINT [FK_ProduccionFormulaDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProduccionFormulaDetalle] ADD CONSTRAINT [FK_ProduccionFormulaDetalle_ProgramacionFormula] 
	FOREIGN KEY ([ProduccionFormulaID]) REFERENCES [ProduccionFormula] ([ProduccionFormulaID])
; GO

ALTER TABLE [ProduccionFormulaDetalle] ADD CONSTRAINT [FK_ProduccionFormulaDetalle_Ingrediente] 
	FOREIGN KEY ([IngredienteID]) REFERENCES [Ingrediente] ([IngredienteID])
; GO

ALTER TABLE [ProduccionFormulaDetalle] ADD CONSTRAINT [FK_ProduccionFormulaDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_SubFamilia] 
	FOREIGN KEY ([SubFamiliaID]) REFERENCES [SubFamilia] ([SubFamiliaID])
; GO

ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_UnidadMedicion] 
	FOREIGN KEY ([UnidadID]) REFERENCES [UnidadMedicion] ([UnidadID])
; GO

ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProductoCodigoParte] ADD CONSTRAINT [FK_ProductoCodigoParte_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [ProductoCodigoParte] ADD CONSTRAINT [FK_ProductoCodigoParte_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProductoCodigoParte] ADD CONSTRAINT [FK_ProductoCodigoParte_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionCorte] ADD CONSTRAINT [FK_ProgramacionCorte_EntradaGanado] 
	FOREIGN KEY ([FolioEntradaID]) REFERENCES [EntradaGanado] ([EntradaGanadoID])
; GO

ALTER TABLE [ProgramacionCorte] ADD CONSTRAINT [FK_ProgramacionCorte_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ProgramacionCorte] ADD CONSTRAINT [FK_ProgramacionCorte_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_AutorizacionMateriaPrima] 
	FOREIGN KEY ([AutorizacionMateriaPrimaID]) REFERENCES [AutorizacionMateriaPrima] ([AutorizacionMateriaPrimaID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_AlmacenInventarioLote] 
	FOREIGN KEY ([InventarioLoteIDOrigen]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_PedidoDetalle] 
	FOREIGN KEY ([PedidoDetalleID]) REFERENCES [PedidoDetalle] ([PedidoDetalleID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionMateriaPrima] ADD CONSTRAINT [FK_ProgramacionMateriaPrima_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionReimplante] ADD CONSTRAINT [FK_ProgramacionReimplante_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [ProgramacionReimplante] ADD CONSTRAINT [FK_ProgramacionReimplante_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ProgramacionReimplante] ADD CONSTRAINT [FK_ProgramacionReimplante_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [ProgramacionReimplante] ADD CONSTRAINT [FK_ProgramacionReimplante_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionReimplante] ADD CONSTRAINT [FK_ProgramacionReimplante_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionReimplanteDetalle] ADD CONSTRAINT [FK_ProgramacionReimplanteDetalle_Corral] 
	FOREIGN KEY ([CorralDestinoID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [ProgramacionReimplanteDetalle] ADD CONSTRAINT [FK_ProgramacionReimplanteDetalle_ProgramacionReimplante] 
	FOREIGN KEY ([FolioProgramacionID]) REFERENCES [ProgramacionReimplante] ([FolioProgramacionID])
; GO

ALTER TABLE [ProgramacionReimplanteDetalle] ADD CONSTRAINT [FK_ProgramacionReimplanteDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProgramacionReimplanteDetalle] ADD CONSTRAINT [FK_ProgramacionReimplanteDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Proveedor] ADD CONSTRAINT [FK_Proveedor_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Proveedor] ADD CONSTRAINT [FK_Proveedor_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Proveedor] ADD CONSTRAINT [FK_TipoProveedorID] 
	FOREIGN KEY ([TipoProveedorID]) REFERENCES [TipoProveedor] ([TipoProveedorID])
; GO

ALTER TABLE [ProveedorAlmacen] ADD CONSTRAINT [FK_ProveedorAlmacen_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [ProveedorAlmacen] ADD CONSTRAINT [FK_ProveedorAlmacen_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [ProveedorAlmacen] ADD CONSTRAINT [FK_ProveedorAlmacen_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProveedorAlmacen] ADD CONSTRAINT [FK_ProveedorAlmacen_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ProveedorChofer] ADD CONSTRAINT [FK_ProveedorChofer_Chofer] 
	FOREIGN KEY ([ChoferID]) REFERENCES [Chofer] ([ChoferID])
; GO

ALTER TABLE [ProveedorChofer] ADD CONSTRAINT [FK_ProveedorChofer_Proveedor] 
	FOREIGN KEY ([ProveedorChoferID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [RecepcionProducto] ADD CONSTRAINT [FK_RecepcionProducto_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [RecepcionProducto] ADD CONSTRAINT [FK_RecepcionProducto_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [RecepcionProducto] ADD CONSTRAINT [FK_RecepcionProducto_Proveedor] 
	FOREIGN KEY ([ProveedorID]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [RecepcionProducto] ADD CONSTRAINT [FK_RecepcionProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RecepcionProducto] ADD CONSTRAINT [FK_RecepcionProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RecepcionProductoDetalle] ADD CONSTRAINT [FK_RecepcionProductoDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [RecepcionProductoDetalle] ADD CONSTRAINT [FK_RecepcionProductoDetalle_RecepcionProducto] 
	FOREIGN KEY ([RecepcionProductoID]) REFERENCES [RecepcionProducto] ([RecepcionProductoID])
; GO

ALTER TABLE [RecepcionProductoDetalle] ADD CONSTRAINT [FK_RecepcionProductoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RecepcionProductoDetalle] ADD CONSTRAINT [FK_RecepcionProductoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_Camion] 
	FOREIGN KEY ([CamionID]) REFERENCES [Camion] ([CamionID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_Contrato] 
	FOREIGN KEY ([ContratoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_Proveedor] 
	FOREIGN KEY ([ProveedorIDMateriasPrimas]) REFERENCES [Proveedor] ([ProveedorID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_ProveedorChofer] 
	FOREIGN KEY ([ProveedorChoferID]) REFERENCES [ProveedorChofer] ([ProveedorChoferID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RegistroVigilancia] ADD CONSTRAINT [FK_RegistroVigilancia_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Reparto] ADD CONSTRAINT [FK_Reparto_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [Reparto] ADD CONSTRAINT [FK_Reparto_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [Reparto] ADD CONSTRAINT [FK_Reparto_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Reparto] ADD CONSTRAINT [FK_Reparto_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Reparto] ADD CONSTRAINT [FK_Reparto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoAlimento] ADD CONSTRAINT [FK_RepartoAlimento_CamionReparto] 
	FOREIGN KEY ([CamionRepartoID]) REFERENCES [CamionReparto] ([CamionRepartoID])
; GO

ALTER TABLE [RepartoAlimento] ADD CONSTRAINT [FK_RepartoAlimento_TipoServicio] 
	FOREIGN KEY ([TipoServicioID]) REFERENCES [TipoServicio] ([TipoServicioID])
; GO

ALTER TABLE [RepartoAlimento] ADD CONSTRAINT [FK_RepartoAlimento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoAlimento] ADD CONSTRAINT [FK_RepartoAlimento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoAlimento] ADD CONSTRAINT [FK_RepartoAlimento_UsuarioReparto] 
	FOREIGN KEY ([UsuarioIDReparto]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [FK_RepartoAlimentoDetalle_CorralFinal] 
	FOREIGN KEY ([CorralIDFinal]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [FK_RepartoAlimentoDetalle_CorralInicio] 
	FOREIGN KEY ([CorralIDInicio]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [FK_RepartoAlimentoDetalle_Formula] 
	FOREIGN KEY ([FormulaIDRacion]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [FK_RepartoAlimentoDetalle_RepartoAlimento] 
	FOREIGN KEY ([RepartoAlimentoID]) REFERENCES [RepartoAlimento] ([RepartoAlimentoID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [FK_RepartoAlimentoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoAlimentoDetalle] ADD CONSTRAINT [FK_RepartoAlimentoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoAvance] ADD CONSTRAINT [FK_RepartoAvance_Usuario] 
	FOREIGN KEY ([UsuarioID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_CamionReparto] 
	FOREIGN KEY ([CamionRepartoID]) REFERENCES [CamionReparto] ([CamionRepartoID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_EstadoComedero] 
	FOREIGN KEY ([EstadoComederoID]) REFERENCES [EstadoComedero] ([EstadoComederoID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_FormulaProgramada] 
	FOREIGN KEY ([FormulaIDProgramada]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_FormulaServida] 
	FOREIGN KEY ([FormulaIDServida]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_Reparto] 
	FOREIGN KEY ([RepartoID]) REFERENCES [Reparto] ([RepartoID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_TipoServicio] 
	FOREIGN KEY ([TipoServicioID]) REFERENCES [TipoServicio] ([TipoServicioID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RepartoDetalle] ADD CONSTRAINT [FK_RepartoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Retencion] ADD CONSTRAINT [FK_Retencion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Retencion] ADD CONSTRAINT [FK_Retencion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RevisionImplante] ADD CONSTRAINT [FK_RevisionImplante_AreaRevision] 
	FOREIGN KEY ([AreaRevisionID]) REFERENCES [AreaRevision] ([AreaRevisionID])
; GO

ALTER TABLE [RevisionImplante] ADD CONSTRAINT [FK_RevisionImplante_EstadoImplante] 
	FOREIGN KEY ([EstadoImplanteID]) REFERENCES [EstadoImplante] ([EstadoImplanteID])
; GO

ALTER TABLE [RevisionImplante] ADD CONSTRAINT [FK_RevisionImplante_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [RevisionImplante] ADD CONSTRAINT [FK_RevisionImplante_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [RevisionImplante] ADD CONSTRAINT [FK_RevisionImplante_UsuarioModificcion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Rol] ADD CONSTRAINT [FK_Rol_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Rol] ADD CONSTRAINT [FK_Rol_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Roladora] ADD CONSTRAINT [FK_Roladora_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Roladora] ADD CONSTRAINT [FK_Roladora_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Roladora] ADD CONSTRAINT [FK_Roladora_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Rotomix] ADD CONSTRAINT [FK_Rotomix_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Rotomix] ADD CONSTRAINT [FK_Rotomix_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Rotomix] ADD CONSTRAINT [FK_Rotomix_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaAnimal] ADD CONSTRAINT [FK_SalidaAnimal_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [SalidaAnimal] ADD CONSTRAINT [FK_SalidaAnimal_SalidaGanado] 
	FOREIGN KEY ([SalidaGanadoID]) REFERENCES [SalidaGanado] ([SalidaGanadoID])
; GO

ALTER TABLE [SalidaAnimal] ADD CONSTRAINT [FK_SalidaAnimal_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaAnimal] ADD CONSTRAINT [FK_SalidaAnimal_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaGanado] ADD CONSTRAINT [FK_SalidaGanado_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [SalidaGanado] ADD CONSTRAINT [FK_SalidaGanado_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [SalidaGanado] ADD CONSTRAINT [FK_SalidaGanado_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaGanado] ADD CONSTRAINT [FK_SalidaGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaGanado] ADD CONSTRAINT [FK_SalidaGanado_VentaGanado] 
	FOREIGN KEY ([VentaGanadoID]) REFERENCES [VentaGanado] ([VentaGanadoID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_AutorizacionMateriaPrima] 
	FOREIGN KEY ([AutorizacionMateriaPrimaID]) REFERENCES [AutorizacionMateriaPrima] ([AutorizacionMateriaPrimaID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_Camion] 
	FOREIGN KEY ([CamionID]) REFERENCES [Camion] ([CamionID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_Chofer] 
	FOREIGN KEY ([ChoferID]) REFERENCES [Chofer] ([ChoferID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_Cliente] 
	FOREIGN KEY ([ClienteID]) REFERENCES [Cliente] ([ClienteID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_OrganizacionDestino] 
	FOREIGN KEY ([OrganizacionIDDestino ]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_TipoMovimiento] 
	FOREIGN KEY ([TipoMovimientoID]) REFERENCES [TipoMovimiento] ([TipoMovimientoID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [SalidaProducto] ADD CONSTRAINT [FK_SalidaProducto_AlmacenInventarioLote] 
	FOREIGN KEY ([AlmacenInventarioLoteID]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [ServicioAlimento] ADD CONSTRAINT [FK_ServicioAlimento_Corral] 
	FOREIGN KEY ([CorralID]) REFERENCES [Corral] ([CorralID])
; GO

ALTER TABLE [ServicioAlimento] ADD CONSTRAINT [FK_ServicioAlimento_Formula] 
	FOREIGN KEY ([FormulaID]) REFERENCES [Formula] ([FormulaID])
; GO

ALTER TABLE [ServicioAlimento] ADD CONSTRAINT [FK_ServicioAlimento_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [ServicioAlimento] ADD CONSTRAINT [FK_ServicioAlimento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [ServicioAlimento] ADD CONSTRAINT [FK_ServicioAlimento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Silo] ADD CONSTRAINT [FK_Silo_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Silo] ADD CONSTRAINT [FK_Silo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Silo] ADD CONSTRAINT [FK_Silo_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Sintoma] ADD CONSTRAINT [FK_Sintoma_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Sintoma] ADD CONSTRAINT [FK_Sintoma_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudPremezcla] ADD CONSTRAINT [FK_SolicitudPremezcla_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [SolicitudPremezcla] ADD CONSTRAINT [FK_SolicitudPremezcla_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudPremezcla] ADD CONSTRAINT [FK_SolicitudPremezcla_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudPremezclaDetalle] ADD CONSTRAINT [FK_SolicitudPremezclaDetalle_Premezcla] 
	FOREIGN KEY ([PremezclaID]) REFERENCES [Premezcla] ([PremezclaID])
; GO

ALTER TABLE [SolicitudPremezclaDetalle] ADD CONSTRAINT [FK_SolicitudPremezclaDetalle_SolicitudPremezcla] 
	FOREIGN KEY ([SolicitudPremezclaID]) REFERENCES [SolicitudPremezcla] ([SolicitudPremezclaID])
; GO

ALTER TABLE [SolicitudPremezclaDetalle] ADD CONSTRAINT [FK_SolicitudPremezclaDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudPremezclaDetalle] ADD CONSTRAINT [FK_SolicitudPremezclaDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_Almacen] 
	FOREIGN KEY ([AlmacenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_AlmacenMovimiento] 
	FOREIGN KEY ([AlmacenMovimientoID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_CentroCosto] 
	FOREIGN KEY ([CentroCostoID]) REFERENCES [CentroCosto] ([CentroCostoID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_UsuarioAutoriza] 
	FOREIGN KEY ([UsuarioIDAutoriza]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_UsuarioEntrega] 
	FOREIGN KEY ([UsuarioIDEntrega]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProducto] ADD CONSTRAINT [FK_SolicitudProducto_UsuarioSolicita] 
	FOREIGN KEY ([UsuarioIDSolicita]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [FK_SolicitudProductoDetalle_CamionReparto] 
	FOREIGN KEY ([CamionRepartoID]) REFERENCES [CamionReparto] ([CamionRepartoID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [FK_SolicitudProductoDetalle_Estatus] 
	FOREIGN KEY ([EstatusID]) REFERENCES [Estatus] ([EstatusID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [FK_SolicitudProductoDetalle_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [FK_SolicitudProductoDetalle_SolicitudProducto] 
	FOREIGN KEY ([SolicitudProductoID]) REFERENCES [SolicitudProducto] ([SolicitudProductoID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [FK_SolicitudProductoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SolicitudProductoDetalle] ADD CONSTRAINT [FK_SolicitudProductoDetalle_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SubFamilia] ADD CONSTRAINT [FK_SubFamilia_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [SubFamilia] ADD CONSTRAINT [FK_SubFamilia_Familia] 
	FOREIGN KEY ([FamiliaID]) REFERENCES [Familia] ([FamiliaID])
; GO

ALTER TABLE [SubFamilia] ADD CONSTRAINT [FK_SubFamilia_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SubFamilia] ADD CONSTRAINT [FK_SubFamilia_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisionDetectores] ADD CONSTRAINT [FK_SupervisionDetectores_CriterioSupervision] 
	FOREIGN KEY ([CriterioSupervisionID]) REFERENCES [CriterioSupervision] ([CriterioSupervisionID])
; GO

ALTER TABLE [SupervisionDetectores] ADD CONSTRAINT [FK_SupervisionDetectores_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [SupervisionDetectores] ADD CONSTRAINT [FK_SupervisionDetectores_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [SupervisionDetectores] ADD CONSTRAINT [FK_SupervisionDetectores_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisionDetectores] ADD CONSTRAINT [FK_SupervisionDetectores_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisionDetectoresDetalle] ADD CONSTRAINT [FK_SupervisionDetectoresDetalle_Pregunta] 
	FOREIGN KEY ([PreguntaID]) REFERENCES [Pregunta] ([PreguntaID])
; GO

ALTER TABLE [SupervisionDetectoresDetalle] ADD CONSTRAINT [FK_SupervisionDetectoresDetalle_SupervisionDetectores] 
	FOREIGN KEY ([SupervisionDetectoresID]) REFERENCES [SupervisionDetectores] ([SupervisionDetectoresID])
; GO

ALTER TABLE [SupervisionDetectoresDetalle] ADD CONSTRAINT [FK_SupervisionDetectoresDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisionDetectoresDetalle] ADD CONSTRAINT [FK_SupervisionDetectoresDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisionGanado] ADD CONSTRAINT [FK_SupervisionGanado_ConceptoDeteccion] 
	FOREIGN KEY ([ConceptoDeteccionID]) REFERENCES [ConceptoDeteccion] ([ConceptoDeteccionID])
; GO

ALTER TABLE [SupervisionGanado] ADD CONSTRAINT [FK_SupervisionGanado_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [SupervisionGanado] ADD CONSTRAINT [FK_SupervisionGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisionGanado] ADD CONSTRAINT [FK_SupervisionGanado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisorEnfermeria] ADD CONSTRAINT [FK_SupervisorEnfermeria_Enfermeria] 
	FOREIGN KEY ([EnfermeriaID]) REFERENCES [Enfermeria] ([EnfermeriaID])
; GO

ALTER TABLE [SupervisorEnfermeria] ADD CONSTRAINT [FK_SupervisorEnfermeria_Operador] 
	FOREIGN KEY ([OperadorID]) REFERENCES [Operador] ([OperadorID])
; GO

ALTER TABLE [SupervisorEnfermeria] ADD CONSTRAINT [FK_SupervisorEnfermeria_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [SupervisorEnfermeria] ADD CONSTRAINT [FK_SupervisorEnfermeria_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TarifaSecado] ADD CONSTRAINT [FK_TarifaSecado_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [TarifaSecado] ADD CONSTRAINT [FK_TarifaSecado_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [TarifaSecado] ADD CONSTRAINT [FK_TarifaSecado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TarifaSecado] ADD CONSTRAINT [FK_TarifaSecado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TiempoMuerto] ADD CONSTRAINT [FK_TiempoMuerto_CausaTiempoMuerto] 
	FOREIGN KEY ([CausaTiempoMuertoID]) REFERENCES [CausaTiempoMuerto] ([CausaTiempoMuertoID])
; GO

ALTER TABLE [TiempoMuerto] ADD CONSTRAINT [FK_TiempoMuerto_ProduccionDiaria] 
	FOREIGN KEY ([ProduccionDiariaID]) REFERENCES [ProduccionDiaria] ([ProduccionDiariaID])
; GO

ALTER TABLE [TiempoMuerto] ADD CONSTRAINT [FK_TiempoMuerto_RepartoAlimento] 
	FOREIGN KEY ([RepartoAlimentoID]) REFERENCES [RepartoAlimento] ([RepartoAlimentoID])
; GO

ALTER TABLE [TiempoMuerto] ADD CONSTRAINT [FK_TiempoMuerto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TiempoMuerto] ADD CONSTRAINT [FK_TiempoMuerto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoAlmacen] ADD CONSTRAINT [FK_TipoAlmacen_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoAlmacen] ADD CONSTRAINT [FK_TipoAlmacen_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoAutorizacion] ADD CONSTRAINT [FK_TipoAutorizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoAutorizacion] ADD CONSTRAINT [FK_TipoAutorizacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCambio] ADD CONSTRAINT [FK_TipoCambio_Moneda] 
	FOREIGN KEY ([MonedaID]) REFERENCES [Moneda] ([MonedaID])
; GO

ALTER TABLE [TipoCambio] ADD CONSTRAINT [FK_TipoCambio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCambio] ADD CONSTRAINT [FK_TipoCambio_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCancelacion] ADD CONSTRAINT [FK_TipoCancelacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCancelacion] ADD CONSTRAINT [FK_TipoCancelacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoContrato] ADD CONSTRAINT [FK_TipoContrato_Usuario] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCorral] ADD CONSTRAINT [FK_TipoCorral_GrupoCorral] 
	FOREIGN KEY ([GrupoCorralID]) REFERENCES [GrupoCorral] ([GrupoCorralID])
; GO

ALTER TABLE [TipoCorral] ADD CONSTRAINT [FK_TipoCorral_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCosto] ADD CONSTRAINT [FK_TipoCosto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCosto] ADD CONSTRAINT [FK_TipoCosto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCuenta] ADD CONSTRAINT [FK_TipoCuenta_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoCuenta] ADD CONSTRAINT [FK_TipoCuenta_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoDeteccion] ADD CONSTRAINT [FK_TipoDeteccion_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoDeteccion] ADD CONSTRAINT [FK_TipoDeteccion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoEmbarque] ADD CONSTRAINT [FK_TipoEmbarque_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoEmbarque] ADD CONSTRAINT [FK_TipoEmbarque_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoEstatus] ADD CONSTRAINT [FK_TipoEstatus_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoEstatus] ADD CONSTRAINT [FK_TipoEstatus_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoFlete] ADD CONSTRAINT [FK_TipoFlete_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoFlete] ADD CONSTRAINT [FK_TipoFlete_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoFormula] ADD CONSTRAINT [FK_TipoFormula_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoFormula] ADD CONSTRAINT [FK_TipoFormula_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoGanado] ADD CONSTRAINT [FK_TipoGanado_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoGanadoCorrales] ADD CONSTRAINT [FK_TipoGanadoCorrales_TipoGanado] 
	FOREIGN KEY ([TipoGanadoID]) REFERENCES [TipoGanado] ([TipoGanadoID])
; GO

ALTER TABLE [TipoGanadoCorrales] ADD CONSTRAINT [FK_TipoGanadoCorrales_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoGanadoCorrales] ADD CONSTRAINT [FK_TipoGanadoCorrales_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoLugarMuestra] ADD CONSTRAINT [FK_TipoLugarMuestra_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoLugarMuestra] ADD CONSTRAINT [FK_TipoLugarMuestra_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoMovimiento] ADD CONSTRAINT [FK_TipoMovimiento_TipoPoliza] 
	FOREIGN KEY ([TipoPolizaID]) REFERENCES [TipoPoliza] ([TipoPolizaID])
; GO

ALTER TABLE [TipoMovimiento] ADD CONSTRAINT [FK_TipoMovimiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoMovimiento] ADD CONSTRAINT [FK_TipoMovimiento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoMuestra] ADD CONSTRAINT [FK_TipoMuestra_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoMuestra] ADD CONSTRAINT [FK_TipoMuestra_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoObjetivoCalidad] ADD CONSTRAINT [FK_TipoObjetivoCalidad_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoObjetivoCalidad] ADD CONSTRAINT [FK_TipoObjetivoCalidad_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoObservacion] ADD CONSTRAINT [FK_TipoObservacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoObservacion] ADD CONSTRAINT [FK_TipoObservacion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoOrganizacion] ADD CONSTRAINT [FK_TipoOrganizacion_TipoProceso] 
	FOREIGN KEY ([TipoProcesoID]) REFERENCES [TipoProceso] ([TipoProcesoID])
; GO

ALTER TABLE [TipoOrganizacion] ADD CONSTRAINT [FK_TipoOrganizacion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoPAC] ADD CONSTRAINT [FK_TipoPAC_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoParametro] ADD CONSTRAINT [FK_TipoParametro_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoParametro] ADD CONSTRAINT [FK_TipoParametro_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoPesaje] ADD CONSTRAINT [FK_TipoPesaje_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoPesaje] ADD CONSTRAINT [FK_TipoPesaje_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoPoliza] ADD CONSTRAINT [FK_TipoPoliza_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoPoliza] ADD CONSTRAINT [FK_TipoPoliza_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoPregunta] ADD CONSTRAINT [FK_TipoPregunta_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoProblema] ADD CONSTRAINT [FK_TipoProblema_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoProceso] ADD CONSTRAINT [FK_TipoProceso_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoProrrateo] ADD CONSTRAINT [FK_TipoProrrateo_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoProveedor] ADD CONSTRAINT [FK_TipoProveedor_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoProveedor] ADD CONSTRAINT [FK_TipoProveedor_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoServicio] ADD CONSTRAINT [FK_TipoServicio_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoServicio] ADD CONSTRAINT [FK_TipoServicio_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoTarifa] ADD CONSTRAINT [FK_TipoTarifa_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoTarifa] ADD CONSTRAINT [FK_TipoTarifa_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoTecnica] ADD CONSTRAINT [FK_TipoTecnica_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoTecnica] ADD CONSTRAINT [FK_TipoTecnica_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoTratamiento] ADD CONSTRAINT [FK_TipoTratamiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TipoTratamiento] ADD CONSTRAINT [FK_TipoTratamiento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Trampa] ADD CONSTRAINT [FK_Trampa_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Trampa] ADD CONSTRAINT [FK_Trampa_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Trampa] ADD CONSTRAINT [FK_Trampa_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenInventarioLoteDestino] 
	FOREIGN KEY ([InventarioLoteDestinoID]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenInventarioLoteOrigen] 
	FOREIGN KEY ([InventarioLoteOrigenID]) REFERENCES [AlmacenInventarioLote] ([AlmacenInventarioLoteID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenMovimientoEntrada] 
	FOREIGN KEY ([AlmacenMovimientoEntradaID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenMovimientoSalida] 
	FOREIGN KEY ([AlmacenMovimientoSalidaID]) REFERENCES [AlmacenMovimiento] ([AlmacenMovimientoID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_CuentaSAP] 
	FOREIGN KEY ([CuentaSAPID]) REFERENCES [CuentaSAP] ([CuentaSAPID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_ContratoOrigen] 
	FOREIGN KEY ([ContratoOrigenID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_ContratoDestino] 
	FOREIGN KEY ([ContratoDestinoID]) REFERENCES [Contrato] ([ContratoID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenOrigen] 
	FOREIGN KEY ([AlmacenOrigenID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [TraspasoMateriaPrima] ADD CONSTRAINT [FK_TraspasoMateriaPrima_AlmacenDestino] 
	FOREIGN KEY ([AlmacenDestinoID]) REFERENCES [Almacen] ([AlmacenID])
; GO

ALTER TABLE [Tratamiento] ADD CONSTRAINT [FK_Tratamiento_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [Tratamiento] ADD CONSTRAINT [FK_Tratamiento_TipoTratamiento] 
	FOREIGN KEY ([TipoTratamientoID]) REFERENCES [TipoTratamiento] ([TipoTratamientoID])
; GO

ALTER TABLE [Tratamiento] ADD CONSTRAINT [FK_Tratamiento_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Tratamiento] ADD CONSTRAINT [FK_Tratamiento_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TratamientoProducto] ADD CONSTRAINT [FK_TratamientoProducto_Producto] 
	FOREIGN KEY ([ProductoID]) REFERENCES [Producto] ([ProductoID])
; GO

ALTER TABLE [TratamientoProducto] ADD CONSTRAINT [FK_TratamientoProducto_Tratamiento] 
	FOREIGN KEY ([TratamientoID]) REFERENCES [Tratamiento] ([TratamientoID])
; GO

ALTER TABLE [TratamientoProducto] ADD CONSTRAINT [FK_TratamientoProducto_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [TratamientoProducto] ADD CONSTRAINT [FK_TratamientoProducto_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [UnidadMedicion] ADD CONSTRAINT [FK_UnidadMedicion_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [UnidadMedicion] ADD CONSTRAINT [FK_UnidadMedicion_UsuarioModificacion] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [Usuario] ADD CONSTRAINT [FK_Usuario_Organizacion] 
	FOREIGN KEY ([OrganizacionID]) REFERENCES [Organizacion] ([OrganizacionID])
; GO

ALTER TABLE [UsuarioGrupo] ADD CONSTRAINT [FK_UsuarioGrupo_Grupo] 
	FOREIGN KEY ([GrupoID]) REFERENCES [Grupo] ([GrupoID])
; GO

ALTER TABLE [UsuarioGrupo] ADD CONSTRAINT [FK_UsuarioGrupo_Usuario] 
	FOREIGN KEY ([UsuarioID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [VentaGanado] ADD CONSTRAINT [FK_VentaGanado_Cliente] 
	FOREIGN KEY ([ClienteID]) REFERENCES [Cliente] ([ClienteID])
; GO

ALTER TABLE [VentaGanado] ADD CONSTRAINT [FK_VentaGanado_Lote] 
	FOREIGN KEY ([LoteID]) REFERENCES [Lote] ([LoteID])
; GO

ALTER TABLE [VentaGanado] ADD CONSTRAINT [FK_VentaGanado_Usuario] 
	FOREIGN KEY ([UsuarioModificacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [VentaGanado] ADD CONSTRAINT [FK_VentaGanado_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacionID]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [VentaGanadoDetalle] ADD CONSTRAINT [FK_VentaGanadoDetalle_CausaPrecio] 
	FOREIGN KEY ([CausaPrecioID]) REFERENCES [CausaPrecio] ([CausaPrecioID])
; GO

ALTER TABLE [VentaGanadoDetalle] ADD CONSTRAINT [FK_VentaGanadoDetalle_Usuario] 
	FOREIGN KEY ([UsuarioModificacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [VentaGanadoDetalle] ADD CONSTRAINT [FK_VentaGanadoDetalle_UsuarioCreacion] 
	FOREIGN KEY ([UsuarioCreacion]) REFERENCES [Usuario] ([UsuarioID])
; GO

ALTER TABLE [VentaGanadoDetalle] ADD CONSTRAINT [FK_VentaGanadoDetalle_VentaGanado] 
	FOREIGN KEY ([VentaGanadoID]) REFERENCES [VentaGanado] ([VentaGanadoID])
; GO
