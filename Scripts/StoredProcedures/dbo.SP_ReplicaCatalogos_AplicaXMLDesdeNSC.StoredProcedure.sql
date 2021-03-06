USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SP_ReplicaCatalogos_AplicaXMLDesdeNSC]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SP_ReplicaCatalogos_AplicaXMLDesdeNSC]
GO
/****** Object:  StoredProcedure [dbo].[SP_ReplicaCatalogos_AplicaXMLDesdeNSC]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================================================                          
-- Author:  José Rico                          
-- Create date: 2 enero 2015             
-- Description: Aplica a la Base de Datos Local un XML que                          
--    recibe como parámetro como 2do paso de la                           
--    Replicación de Catálogos de NSC.                
--    Revisado para que cambie los valores (&chr39;) por apóstrofes                  
--    '&#x0A;' por salto de linea y '&#x0D;'                
--    por Retorno                          
-- =======================================================                          
Create Procedure [dbo].[SP_ReplicaCatalogos_AplicaXMLDesdeNSC]
	@Cadena  Varchar(Max)
As
Begin
	Set NoCount On;                          
                          
	Declare @Pos1			Int,                          
			@Pos2			Int,                          
			@Pos3			Int,                          
			@Pos4			Int,                          
			@Pos5			Int,                          
			@Pos6			Int,                          
			@Registro		Varchar(Max),                          
			@Registro2		Varchar(Max),                
			@Registro3		Varchar(Max),                
			@Tabla			VarChar(100),                
			@Tabla2			VarChar(100),                
			@Update			VarChar(max),                          
			@Insert			VarChar(8000),                          
			@Where			VarChar(max),                          
			@CamposLlave	VarChar(1000),                  
			@CamposLlave2	VarChar(1000),                        
			@CampoLlave		VarChar(100),                          
			@ValorLlave		VarChar(1000),                          
			@Tipo			VarChar(20),                          
			@Sql			VarChar(max),                          
			@Campo			VarChar(1000),                          
			@NombreCampo	VarChar(1000),                          
			@ValorCampo		VarChar(1000),                          
			@Seccion		VarChar(25),                          
			@Owner			VarChar(20),                          
			@Contador		SmallInt                
                             
	Begin Try                
               
		Set @Seccion = 'Inicialización'                                  
		--                          
		--- Reemplaza valores Mayor Que (>) y Menor que (<) que genera Coldfusion                          
		--              
		Set @Cadena = Replace(@Cadena,'&lt;','<')                          
		Set @Cadena = Replace(@Cadena,'&gt;','>')                
		Set @Cadena = Replace(@Cadena,'apostrofe','''''')                  
		Set @Cadena = Replace(@Cadena,'&#x0A;', CHAR(10) )                  
		Set @Cadena = Replace(@Cadena,'&#x0D;', CHAR(13) )     
		Set @Cadena = Replace(@Cadena,'&#xA;', CHAR(10) )          
		Set @Cadena = Replace(@Cadena,'&#xD;', CHAR(13) )          
		Set @Cadena = Replace(@Cadena,'&quot;','')               
              
		--              
		--- Tumba Root              
		--              
		If Substring(@Cadena,1,6) = '<root>'              
			Set @Cadena = Substring(@Cadena,7,Len(@Cadena))              
                 
		--                          
		--- Elimina Registro de Fecha en XML                          
		--              
		Set @Pos1  = CharIndex('/>',@Cadena)                          
		Set @Registro = Substring(@Cadena,1,@Pos1+1)                          
		Set @Pos2  = CharIndex(' ',@Cadena,2)                          
		Set @Tabla  = Substring(@Cadena,2,@Pos2-2)                
		If @Tabla  = 'tbFechaAct'                
			Set @Cadena = Substring(@Cadena,@Pos1+2,Len(@Cadena))                
              
		--                          
		--- Inicialización de Variables.                          
		--                
		Set @Seccion  = ''                
		Set @Tabla2   = ''                
		Set @CamposLlave2 = ''                
		Set @Sql   = ''                
		Set @Contador  = 0              
	End Try                
                 
	Begin Catch                
		Insert Into ReplicaCatalogos_Bitacora (RegistroXML, InstruccionSQL, ErrorSQL)                 
		Values (Substring(@Cadena,1,4000), 'Error al inicializar variables, se ignora archivo completo', Error_Message())                
		Return                
	End Catch              
               
	While Len(@Cadena) > 10              
	Begin                   
		Begin Try              
		
			Set @Pos1		= CharIndex('/>',@Cadena)                
			Set @Pos6		= @Pos1              
			Set @Registro	= Substring(@Cadena,1,@Pos1-1)                
			Set @Registro3  = @Registro                     
			Set @Pos1		= CharIndex(' ',@Cadena,2)                
			Set @Tabla		= Substring(@Cadena,2,@Pos1-2)                
			Set @Registro	= Substring(@Registro,@Pos1+1,Len(@Registro))                
			Set @Registro2  = @Registro
                  
			--                
			--- Tabla ADSUM_DatosDeProyecto Se ignora                
			--            
			If @Tabla = 'ADSUM_DatosDeProyecto'                
				Begin                
					Set @Cadena = Substring(@Cadena,@Pos6+2,Len(@Cadena))                
					Continue                
				End                
                            
			If @Tabla = 'DEM_RegistrosEliminados'                          
				Begin                
					Insert Into ReplicaCatalogos_Bitacora (RegistroXML, InstruccionSQL, ErrorSQL)                 
					Values (Substring(@Registro3,1,4000),                 
					'Stored no preparado para aplicar tabla DEM_RegistrosEliminados, se ignora registro',                 
					'Instrucción SQL no generada')                     
					Set @Cadena = Substring(@Cadena,@Pos6+2,Len(@Cadena))                
					Continue                          
				End                          
         
			--                
			--- Tablas que se inicializan primero hay que eliminar los registros          
			--                
			--If @Tabla In ('Adsum_Navegador','Adsum_Perfiles','Adsum_OperacionesSupervisadas_Remotas','Adsum_Usuarios_X_Perfil')                       
			--Begin                 
			--Set @Sql = 'Delete From ' + @Tabla          
			--Execute (@Sql)          
			--End                     
			--                
			--- Generación de Nulos en cadena de Update                
			--                
			--If Not Exists(Select cTabla From @CamposPorTabla Where cTabla = @Tabla)                          
			-- Begin                          
			--  Insert Into @CamposPorTabla                          
			--  Select @Tabla, Column_Name                           
			--    From Information_Schema.Columns                          
			--   Where Table_Name = @Tabla                          
			-- End                            
			--Else                          
			-- Begin                          
			--  Insert Into @CamposPorTabla Values ('1','2')                          
			-- End                          
                             
			--                
			--- Tablas que se inicializan no requieren armar Update
			--                
			If  @Tabla Not In ('Adsum_Navegador','Adsum_Perfiles','Adsum_OperacionesSupervisadas_Remotas', 'Adsum_Usuarios_X_Perfil')
				Begin                
					If @Tabla <> @Tabla2                
						Begin                
							Set @Seccion		= 'Identificar CamposLlave'                
							Set @CamposLlave	= ''                
							
							Select	@CamposLlave = @CamposLlave + Case	When Cam.Column_Name Is Null                           
																		Then ''                           
																		Else Cam.Column_Name + ', ' End                          
							  From	INFORMATION_SCHEMA.COLUMNS (NoLock) Cam                           
							 Inner	Join INFORMATION_SCHEMA.KEY_COLUMN_USAGE Lla (NoLock)                          
									 On Lla.Table_Name					= Cam.Table_Name                          
									And Lla.Column_Name					= Cam.Column_Name                          
									And Left(Lla.Constraint_Name, 2)	= 'PK'                          
									And Lla.Table_Name					= @Tabla                
                             
							If Len(@CamposLlave) = 0                        
								Begin                
									Insert Into ReplicaCatalogos_Bitacora (RegistroXML, InstruccionSQL, ErrorSQL)                 
									Values (Substring(@Registro3,1,4000),                 
									'Se encontró la tabla ' + @Tabla + ' sin llave primaria configurada, Llave ' + @CamposLlave + '.',                 
									'Instrucción SQL no generada')                     
									Set @Cadena = Substring(@Cadena,@Pos6+2,Len(@Cadena))                
									Continue                          
								End                          
							
							Set @Tabla2   = @Tabla                
							Set @CamposLlave2 = @CamposLlave                
						End                
					Else                
						Set @CamposLlave = @CamposLlave2                
                
					Set @Seccion = 'Armar Where'                
					Set @Where = 'Where '                          
					While Len(@CamposLlave) > 1                          
						Begin                
							Set @Pos2		= CharIndex(',',@CamposLlave,1)                
							Set @CampoLlave = Substring(@CamposLlave,1,@Pos2-1)                
							Set @Pos3		= CharIndex(@CampoLlave,@Registro,1)                
							Set @Pos4		= CharIndex('"',@Registro,@Pos2+@Pos3)                          
							Set @Pos5		= CharIndex('"',@Registro,@Pos4+1)                          
							Set @ValorLlave = Substring(@Registro,@Pos4+1,@Pos5-@Pos4-1)                          
                
							Select	@Tipo = Upper(Cam.Data_Type)                          
							  From	information_schema.columns Cam                          
							 Where	Cam.Table_Name = @Tabla                          
							   And	Cam.Column_Name = @CampoLlave                
                                 
							If @Where <> 'Where '                          
								Set @Where = @Where + ' And '                          
                                
							If @Tipo In ('TINYINT','SMALLINT','INT','BIGINT','SMALLMONEY','MONEY','NUMERIC', 'DECIMAL','FLOAT','REAL')                          
								Set @Where = @Where + @CampoLlave + ' = ' + @ValorLlave                          
							Else                          
								Set @Where = @Where + @CampoLlave + ' = ''' + @ValorLlave + ''''                          
                      
							Set @CamposLlave = Substring(@CamposLlave, @Pos2+2, Len(@CamposLlave))                          
                      
							If @Pos3 = 0                
								Set @Seccion = 'Campo ' + @CampoLlave + ' No en XML.'                 
                                 
							Set @Registro  = Substring(@Registro,1,@Pos3-1) + Substring(@Registro,@Pos5+2,Len(@Registro))                
						End                
                 
					--                
					--- Tablas que el 100% de sus campos son parte de su llave Principal                
					--                 
					If @Tabla Not In ('ADSUM_OperacionesSupervisadas_Supervisores','CTL_MonedasCanalesDistribucion','DEM_Acciones_X_Perfil_Demonio')
					And @Tabla Not In ('CTL_ProductosUsosProductos','ISK_ClientesFox','CTL_BonificacionSubfamilias')                
						Begin                
							Set @Seccion = 'Armar Update'                
							Set @Update  = ' Update ' + @Tabla + ' Set '                          
							While Len(@Registro) > 10              
								Begin                          
									Set @Pos2   = CharIndex('"',@Registro,1)                          
									Set @Pos3   = CharIndex('"',@Registro,@Pos2+1)                          
									Set @Campo   = Ltrim(Replace(Substring(@Registro,1,@Pos3),'"',''))                          
									Set @Pos4   = CharIndex('=',@Campo,1)                          
									Set @NombreCampo = Ltrim(rTrim(Substring(@Campo,1,@Pos4-1)))                          
									Set @ValorCampo  = Ltrim(rTrim(Substring(@Campo,@Pos4+1,Len(@Campo))))                    
									Set @Tipo=''             
						
									Select	@Tipo = Upper(Cam.Data_Type)                          
									  From	information_schema.columns Cam                          
									 Where	Cam.Table_Name = @Tabla                          
									   And	Cam.Column_Name = @NombreCampo                          
                           
									If @Tipo In ('TINYINT','SMALLINT','INT','BIGINT','SMALLMONEY','MONEY','NUMERIC', 'DECIMAL','FLOAT','REAL')                          
										Set @Update = @Update + @NombreCampo + ' = ' + @ValorCampo + ', '                          
									Else              
										Set @Update = @Update + @NombreCampo + ' = ''' + @ValorCampo + ''', '               

									Set @Registro = Substring(@Registro,@Pos3+1,Len(@Registro2))                         
								End                          
         
							Set @Seccion ='Ejecutar Update'
							Set @Update = Substring (rtrim(@Update),1,Len(rtrim(@Update))-1)     
							Set @Sql = @Update + ' ' + @Where      
							Execute (@Sql)                          
							Set @Contador = @@RowCount                          
						End -- Tablas con el 100% de sus campos como Llave Principal                
				End -- Tablas que se Inicializan                
			Else                          
				Set @Contador = 0                          
                             
			If @Contador = 0                          
				Begin
					Set @Seccion ='Armar Insert'          
					Set @Owner = ''                          
					Set @Owner = (	Select	Top 1 Schema_Name(ObjectProperty(Object_Id,'SchemaId'))                          
									  From	SYS.COLUMNS                          
									 Where	Object_Name(Object_Id) = @Tabla                          
									   And	is_identity = 1)                          
                         
					Set @Sql=''                             
					If Len(@Owner) > 0                          
						Set @Sql = ' Set Identity_Insert ' + @Owner + '.' + @Tabla + ' On'                          
					               
					Set @Registro = @Registro2                          
					Set @Insert = @sql + ' Insert Into ' + @Tabla + ' ('                          
					Set @Update = ') Values ('                          
					While Len(@Registro) > 10                        
						Begin                
							Set @Pos2  = CharIndex('"',@Registro,1)                
							Set @Pos3  = CharIndex('"',@Registro,@Pos2+1)                          
							Set @Campo  = Ltrim(Replace(Substring(@Registro,1,@Pos3),'"',''))                          
							Set @Pos4  = CharIndex('=',@Campo,1)                          
               
							Set @NombreCampo = Ltrim(rTrim(Substring(@Campo,1,@Pos4-1)))                        
							Set @ValorCampo  = Ltrim(rTrim(Substring(@Campo,@Pos4+1,Len(@Campo))))           
							Set @Insert = @Insert + @NombreCampo + ', '
							
							Set @Tipo=''                        
							Select	@Tipo = Upper(Cam.Data_Type)                    
							  From	information_schema.columns Cam                          
							 Where	Cam.Table_Name = @Tabla                          
							   And	Cam.Column_Name = @NombreCampo                          
                                
							If @Tipo In ('TINYINT','SMALLINT','INT','BIGINT','SMALLMONEY','MONEY','NUMERIC', 'DECIMAL','FLOAT','REAL')                          
								Set @Update = @Update + @ValorCampo + ', '                          
							Else                          
								Set @Update = @Update + '''' + @ValorCampo + ''', '                          
                          
							Set @Registro = Substring(@Registro,@Pos3+1,Len(@Registro))                          
						End    
      
					Set @Seccion ='Ejecutar Insert'                
					Set @Insert = Substring (@Insert,1,Len(@Insert)-1)                          
					Set @Update = Substring (rtrim(@Update),1,Len(rtrim(@Update))-1) + ')'     
					Set @Sql = @Insert + @Update  
                  
					If Len(@Owner) > 0                          
						Set @Sql = @Sql + 'Set Identity_Insert ' + @Owner + '.' + @Tabla + ' Off'
                       
					--                
					--- Tablas que el 100% de sus campos son parte de la Llave Principal                
					--                
					If @Tabla In ('ADSUM_OperacionesSupervisadas_Supervisores','CTL_MonedasCanalesDistribucion','DEM_Acciones_X_Perfil_Demonio')
					Or @Tabla In ('CTL_ProductosUsosProductos','ISK_ClientesFox', 'CTL_BonificacionSubfamilias')                
						Begin                
							Set @Sql = 'If Not Exists ( Select * From ' + @Tabla + ' ' + @Where + ') ' + @Sql                
							Execute (@Sql)                
						End                           
					Else                
						Execute (@Sql)                
                
			End --If @Contador = 0                          
                          
			Set @Cadena = Substring(@Cadena,@Pos6+2,Len(@Cadena))                
		End Try                           
                
		Begin Catch                
			If @Seccion = 'Armar Where'                
				Set @Sql = @Where + ' : ' + @Sql                
                   
			Insert Into ReplicaCatalogos_Bitacora (RegistroXML, InstruccionSQL, ErrorSQL)                
			Values (Substring(@Registro3,1,4000),                 
			' al ' + @Seccion + ': ' + Substring(@Sql,1,3950),            
			Substring(Error_Message(),1,1000))                
                      
			Set @Cadena = Substring(@Cadena,@Pos6+2,Len(@Cadena))                
		End Catch                         
                          
	End -- While Len(@Cadena) > 5                          
                           
	Return                         
End 


GO
