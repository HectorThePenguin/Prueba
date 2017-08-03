set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go
IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[Genera_SP]'))
  DROP PROCEDURE [dbo].[Genera_SP]
go
-- =============================================
-- Author     : José Gilberto Quintero López
-- Create date: 31/10/2013
-- Description:  Ayuda para Generar los SP
-- Show '', 'U'
-- =============================================
Create proc [dbo].[Genera_SP] 
(
	@Table varchar(50)
)
as
begin
		
	declare @NombreSP		varchar(100)
	declare @PkId		varchar(100)	
	declare @Nombre		varchar(100)
	declare @Tipo		varchar(100)
	declare @Longitud	varchar(100)
	declare @Precision	varchar(100)
	declare @Scale		varchar(100)
	declare @Column_Id	int
	declare @ProcedureMtto varchar(8000)
	declare @ListaCampos varchar(8000)
	declare @ListaValues varchar(8000)
	declare @ListaCamposSP varchar(8000)
	declare @ListaUpdate varchar(8000)
	declare @MaxColumn int
	declare @SaltaRenglon varchar(20)
	declare @SaltaRenglonPorPagina varchar(20)
 	declare @Developer varchar(100)
	declare @ParametroID varchar(100)
	declare @CampoSP varchar(100)
	declare @LineaSeparadora varchar(100)
	
	SET NOCOUNT ON;
	
	set @LineaSeparadora = '--' + replicate('=',100)
	
	set @Developer = (Select [Nombre] FROM [dbo].[Developer] WHERE [Login] = replace(SUSER_SNAME(),'GVIZ\',''))
		
	--print 'set ANSI_NULLS ON'
	--print 'set QUOTED_IDENTIFIER ON'
	--print 'go'
		
	set @PkId = ''
	set @ProcedureMtto =''
	set @ProcedureMtto = @ProcedureMtto +'--=============================================' + char(13)
	set @ProcedureMtto = @ProcedureMtto + '-- Author     : ' + @Developer + char(13)
	set @ProcedureMtto = @ProcedureMtto + '-- Create date: ' + convert(varchar(15),getdate(),111)+ char(13)
	set @ProcedureMtto = @ProcedureMtto + '-- Description: ' + char(13)
	set @ProcedureMtto = @ProcedureMtto + '-- ' + char(13)
	set @ProcedureMtto = @ProcedureMtto + '--=============================================' + char(13)
	Set @ProcedureMtto = @ProcedureMtto + 'CREATE PROCEDURE ' --+ @Table 

	Set @ListaCamposSP = ''
	set @ListaCampos = '' 	
	set @ListaValues = ''
	set @ListaUpdate = ''
			
	declare @Campos as table
	(
		Nombre varchar(50)
		,Tipo varchar(50)
		,Longitud int
		,Precision varchar(50)
		,Scale varchar(50)
		,Column_Id int
	)
	
	insert @Campos(Nombre,Tipo,Longitud,Precision,Scale,Column_Id)
	select Nombre = c.name 
				,Tipo = upper(t.Name)
				,Longitud = c.max_length
				,Precision = c.Precision
				,Scale = c.Scale
				,c.Column_Id
				from sys.columns c
				inner join sys.types t on (t.system_type_id = c.system_type_id)
				inner join sys.sysobjects o on (o.Id = c.Object_Id)
				where o.Name = @Table
				Order by c.Column_Id
	
		set @MaxColumn = (select MAX(Column_Id) from @Campos)
		
		declare DataSource cursor for
				select * from @Campos

			open DataSource

			fetch next from DataSource
			into @Nombre,@Tipo,@Longitud,@Precision,@Scale,@Column_Id
			while @@fetch_status = 0
			begin
			
				set @SaltaRenglon =  case when @Column_Id = @MaxColumn then  char(9) else ',' + char(13) + char(9) end 
				set @SaltaRenglonPorPagina =  case when @Column_Id in(1,2,3) then  char(9) else ',' + char(13) + char(9) end 
				
				if @Column_Id = 1
				begin
					set @PkId = @Nombre
					--set @ListaCamposSP = @ListaCamposSP + char(9)
					--set @ListaCampos = @ListaCampos	  + char(9)
					--set @ListaValues = @ListaValues	  + char(9)
					--Set @ListaUpdate = @ListaUpdate     + char(9)
					set @ParametroID = +'@'+@Nombre+' '+@Tipo
				End
				else
				begin
					set @ListaCamposSP = @ListaCamposSP 	
					set @ListaCampos = @ListaCampos + char(9)
					set @ListaValues = @ListaValues + char(9)
					set @ListaUpdate = @ListaUpdate + char(9) 
				end

				if @Tipo in( 'numeric', 'decimal')
				begin
					set @CampoSP = '@' + @Nombre+' '+@Tipo+'('+@Precision + ',' + @Scale +')'	 
				end
				else  if @Tipo in( 'char' ,'varchar' )
				begin
					set @CampoSP = '@' + @Nombre+' '+@Tipo+'('+@Longitud+')'	
				end
				else 
				begin
				  set @CampoSP = '@' + @Nombre+' '+@Tipo
				end
								
				Set @ListaCamposSP = @ListaCamposSP + case when @Column_Id = 1 then '' else @CampoSP + @SaltaRenglon  End
				set @ListaCampos   = @ListaCampos   + case when @Column_Id = 1 then '' else @Nombre + @SaltaRenglon end
				set @ListaValues   = @ListaValues   + case when @Column_Id = 1 then '' else '@' +@Nombre + @SaltaRenglon end
				Set @ListaUpdate   = @ListaUpdate   + case when @Column_Id = 1 then '' else char(9) + @Nombre +' = @' + @Nombre + @SaltaRenglon end
		
			fetch next from DataSource
				into @Nombre,@Tipo,@Longitud,@Precision,@Scale,@Column_Id
			end

			set @NombreSP = '[dbo].['+ @Table +'_Crear' + ']'
			print 'IF EXISTS(SELECT *'
			print '      FROM   sys.objects'
			print '      WHERE  [object_id] = Object_id(N' +char(39) + @NombreSP +char(39) +'))'
			print '			DROP PROCEDURE ' + @NombreSP
			print 'GO'
		
			print @ProcedureMtto + @NombreSP			 
			print coalesce(@ListaCamposSP,'') 
			print 'AS'
			print 'BEGIN'
			print char(9)  +'SET NOCOUNT ON;'
			print char(9)  + 'INSERT '+@Table +'(' 
		 	print char(9)  + '' + rtrim(@ListaCampos)
		 	print char(9)  + ')' 
			print char(9)  + 'VALUES('
			print char(9)  + '' + rtrim(@ListaValues)
			print char(9)  + ')'
			print char(9)  +'SET NOCOUNT OFF;'
			print 'END' 
			print 'go'
			
			print @LineaSeparadora
			
			set @NombreSP = '[dbo].['+ @Table +'_Actualizar' + ']'
			
			print 'IF EXISTS(SELECT *'
			print '      FROM   sys.objects'
			print '      WHERE  [object_id] = Object_id(N' +char(39) + @NombreSP + char(39) +'))'
			print '			DROP PROCEDURE ' + @NombreSP
			print 'go'
			
			print @ProcedureMtto + @NombreSP
			print char(9) + coalesce(@ParametroID,'') + case when @ListaCamposSP = '' then '' else ','  + char(13) + char(9) + coalesce(@ListaCamposSP,'') end
			print 'AS'
			print 'BEGIN'
			print char(9)  +'SET NOCOUNT ON;'
			print char(9) + char(9) + 'UPDATE '+ @Table +' SET '
		 	print char(9) + @ListaUpdate 
		 	print char(9) + char(9) +  'WHERE ' + @PkId +' = @'+@PkId
		 	print char(9) +'SET NOCOUNT OFF;'
			print 'END' 
			print 'go'
			print @LineaSeparadora
			
			set @NombreSP = '[dbo].['+ @Table +'_ObtenerTodos' + ']'
			set @ListaCampos = char(9) + coalesce(@PkId,'') + case when @ListaCampos = '' then '' else ','  + char(13) + char(9) + coalesce(@ListaCampos,'') end
			
			print 'IF EXISTS(SELECT *'
			print '      FROM   sys.objects'
			print '      WHERE  [object_id] = Object_id(N' +char(39) + @NombreSP +char(39) +'))'
			print '			DROP PROCEDURE ' + @NombreSP
			print 'go' + char(13)
			
			print @ProcedureMtto + @NombreSP
			print '@Activo BIT = NULL'
			print 'AS'
			print 'BEGIN'
			print char(9)  + 'SET NOCOUNT ON;'
			print char(9)  +  'SELECT '
		 	print char(9)  +  rtrim(@ListaCampos) 
		 	print char(9)  +  'FROM '+ @Table
		 	print char(9)  +  'WHERE Activo = @Activo OR @Activo IS NULL'
		 	print char(9)  + 'SET NOCOUNT OFF;'
			print 'END' 
			print 'GO'
			
			print @LineaSeparadora
			
			set @NombreSP = '[dbo].['+ @Table +'_ObtenerPorID' + ']'
			print 'IF EXISTS(SELECT *'
			print '      FROM   sys.objects'
			print '      WHERE  [object_id] = Object_id(N' +char(39) + @NombreSP + char(39) +'))'
			print '			DROP PROCEDURE ' + @NombreSP
			print 'GO'
			
			print @ProcedureMtto + @NombreSP
			print coalesce(@ParametroID,'') 
			print 'AS'
			print 'BEGIN'
			print char(9) +'SET NOCOUNT ON;'
			print char(9) + 'SELECT '
		 	print char(9) + rtrim(@ListaCampos) 
		 	print char(9) + 'FROM '+ @Table
		 	print char(9) + 'WHERE ' + @PkId +' = @'+@PkId
		 	print char(9) + 'SET NOCOUNT OFF;'
			print 'END' 
			print 'GO'
			
			print @LineaSeparadora
			
			set @NombreSP = '[dbo].['+ @Table +'_ObtenerPorPagina' + ']'
			print 'IF EXISTS(SELECT *'
			print '      FROM   sys.objects'
			print '      WHERE  [object_id] = Object_id(N' +char(39) + @NombreSP + char(39) +'))'
			print '			DROP PROCEDURE ' + @NombreSP
			print 'GO' 
			
			print @ProcedureMtto + @NombreSP
			print coalesce(@ParametroID,'') + ','
			print '@Descripcion varchar(50),'
			print '@Activo BIT,'
			print '@Inicio INT,' 
			print '@Limite INT'
			print 'AS'
			print 'BEGIN'
			print char(9)  +'SET NOCOUNT ON;'
			print char(9)  +'SELECT' 
			print char(9)  +'	ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS RowNum,'
			print char(9) + char(9) + @PkId + ','
			print char(9) + char(9) +'Descripcion,'
			print char(9) + char(9) +'Activo'		
			print char(9)  + '	INTO #Datos'
			print char(9)  + 'FROM ' + @Table
			print char(9)  + 'WHERE (Descripcion like ' + char(39) + '%' + char(39) +' + @Descripcion + ' + char(39) +'%' + char(39) + ' OR @Descripcion = ' +  char(39) +  char(39) +')'
			print char(9)  + 'AND Activo = @Activo'
			print '	SELECT' 
			print char(9) + char(9) + @PkId + ','
			print '		Descripcion,'
			print '		Activo'
			print char(9) + 'FROM #Datos'	
			print char(9) + 'WHERE RowNum BETWEEN @Inicio AND @Limite'
			print char(13)
			print char(9)  + 'SELECT '
			print char(9)  + '	COUNT('+ @PkId +')AS TotalReg' 
			print char(9)  + 'From #Datos'
			print char(13)
			print char(9)  + 'DROP TABLE #Datos'
			print char(9)  + 'SET NOCOUNT OFF;'
			print 'END' 
			print 'GO'
			print char(13)
			
			-- cerramos el cursor
			close DataSource
			deallocate DataSource
	
End




