˝
W/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/UsuarioBL.cs
	namespace 	
CapaNegocio
 
{ 
public 

class 
	UsuarioBL 
{ 
} 
} ≠
\/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/TratamientosBL.cs
	namespace		 	
CapaNegocio		
 
{

 
public 

class 
TratamientosBL 
{ 
public 
List 
< 
TratamientosCLS #
># $
ListarTratamientos% 7
(7 8
)8 9
{ 	
TratamientosDAL 
objTratamientosDAL .
=/ 0
new1 4
TratamientosDAL5 D
(D E
)E F
;F G
return 
objTratamientosDAL %
.% &
ListarTratamientos& 8
(8 9
)9 :
;: ;
} 	
public 
int 
GuardarTratamiento %
(% &
TratamientosCLS& 5
objTratamientoCLS6 G
)G H
{ 	
TratamientosDAL 
objTratamientosDAL .
=/ 0
new1 4
TratamientosDAL5 D
(D E
)E F
;F G
return 
objTratamientosDAL %
.% &
GuardarTratamiento& 8
(8 9
objTratamientoCLS9 J
)J K
;K L
} 	
public 
int 
EliminarTratamiento &
(& '
int' *
id+ -
)- .
{ 	
TratamientosDAL 
objTratamientosDAL .
=/ 0
new1 4
TratamientosDAL5 D
(D E
)E F
;F G
return 
objTratamientosDAL %
.% &
EliminarTratamiento& 9
(9 :
id: <
)< =
;= >
} 	
public 
TratamientosCLS  
RecuperarTratamiento 3
(3 4
int4 7
id8 :
): ;
{   	
TratamientosDAL!! 
objTratamientosDAL!! .
=!!/ 0
new!!1 4
TratamientosDAL!!5 D
(!!D E
)!!E F
;!!F G
return"" 
objTratamientosDAL"" %
.""% & 
RecuperarTratamiento""& :
("": ;
id""; =
)""= >
;""> ?
}## 	
public%% 
List%% 
<%% 
TratamientosCLS%% #
>%%# $
FiltrarTratamientos%%% 8
(%%8 9
TratamientosCLS%%9 H
filtro%%I O
)%%O P
{&& 	
TratamientosDAL'' 
objTratamientosDAL'' .
=''/ 0
new''1 4
TratamientosDAL''5 D
(''D E
)''E F
;''F G
return(( 
objTratamientosDAL(( %
.((% &
FiltrarTratamientos((& 9
(((9 :
filtro((: @
)((@ A
;((A B
})) 	
}** 
}++ ©
Y/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/PacientesBL.cs
	namespace		 	
CapaNegocio		
 
{

 
public 

class 
PacientesBL 
{ 
public 
List 
< 
PacienteCLS 
>  
ListarPacientes! 0
(0 1
)1 2
{ 	
PacienteDAL 
objPacientesDAL '
=( )
new* -
PacienteDAL. 9
(9 :
): ;
;; <
return 
objPacientesDAL "
." #
ListarPacientes# 2
(2 3
)3 4
;4 5
} 	
public 
int 
GuardarPaciente "
(" #
PacienteCLS# .
objPacienteCLS/ =
)= >
{ 	
PacienteDAL 
objPacientesDAL '
=( )
new* -
PacienteDAL. 9
(9 :
): ;
;; <
return 
objPacientesDAL "
." #
GuardarPaciente# 2
(2 3
objPacienteCLS3 A
)A B
;B C
} 	
public 
int 
EliminarPaciente #
(# $
int$ '
id( *
)* +
{ 	
PacienteDAL 
objPacientesDAL '
=( )
new* -
PacienteDAL. 9
(9 :
): ;
;; <
return 
objPacientesDAL "
." #
EliminarPaciente# 3
(3 4
id4 6
)6 7
;7 8
} 	
public 
PacienteCLS 
RecuperarPaciente ,
(, -
int- 0
id1 3
)3 4
{   	
PacienteDAL!! 
objPacientesDAL!! '
=!!( )
new!!* -
PacienteDAL!!. 9
(!!9 :
)!!: ;
;!!; <
return"" 
objPacientesDAL"" "
.""" #
RecuperarPaciente""# 4
(""4 5
id""5 7
)""7 8
;""8 9
}## 	
public%% 
List%% 
<%% 
PacienteCLS%% 
>%%  
FiltrarPacientes%%! 1
(%%1 2
PacienteCLS%%2 =
filtro%%> D
)%%D E
{&& 	
PacienteDAL'' 
objPacientesDAL'' '
=''( )
new''* -
PacienteDAL''. 9
(''9 :
)'': ;
;''; <
return(( 
objPacientesDAL(( "
.((" #
FiltrarPacientes((# 3
(((3 4
filtro((4 :
)((: ;
;((; <
})) 	
}** 
}++ Í
W/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/MedicosBL.cs
	namespace		 	
CapaNegocio		
 
{

 
public 

class 
	MedicosBL 
{ 
public 
List 
< 

MedicosCLS 
> 
ListarMedicos  -
(- .
). /
{ 	

MedicosDAL 
objMedicosDAL $
=% &
new' *

MedicosDAL+ 5
(5 6
)6 7
;7 8
return 
objMedicosDAL  
.  !
ListarMedicos! .
(. /
)/ 0
;0 1
} 	
public 
int 
GuardarMedico  
(  !

MedicosCLS! +
objMedicoCLS, 8
)8 9
{ 	

MedicosDAL 
objMedicosDAL $
=% &
new' *

MedicosDAL+ 5
(5 6
)6 7
;7 8
return 
objMedicosDAL  
.  !
GuardarMedico! .
(. /
objMedicoCLS/ ;
); <
;< =
} 	
public 
int 
EliminarMedico !
(! "
int" %
id& (
)( )
{ 	

MedicosDAL 
objMedicosDAL $
=% &
new' *

MedicosDAL+ 5
(5 6
)6 7
;7 8
return 
objMedicosDAL  
.  !
EliminarMedico! /
(/ 0
id0 2
)2 3
;3 4
} 	
public 

MedicosCLS 
RecuperarMedico )
() *
int* -
id. 0
)0 1
{   	

MedicosDAL!! 
objMedicosDAL!! $
=!!% &
new!!' *

MedicosDAL!!+ 5
(!!5 6
)!!6 7
;!!7 8
return"" 
objMedicosDAL""  
.""  !
RecuperarMedico""! 0
(""0 1
id""1 3
)""3 4
;""4 5
}## 	
public%% 
List%% 
<%% 

MedicosCLS%% 
>%% 
FiltrarMedicos%%  .
(%%. /

MedicosCLS%%/ 9
filtro%%: @
)%%@ A
{&& 	

MedicosDAL'' 
objMedicosDAL'' $
=''% &
new''' *

MedicosDAL''+ 5
(''5 6
)''6 7
;''7 8
return(( 
objMedicosDAL((  
.((  !
FiltrarMedicos((! /
(((/ 0
filtro((0 6
)((6 7
;((7 8
})) 	
}** 
}++ Å
W/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/GenericBL.cs
	namespace 	
CapaNegocio
 
{		 
public

 

class

 
	GenericBL

 
{ 
public 
List 
< 
int 
> 
obtenerClaves &
(& '
string' -
tabla. 3
)3 4
{ 	

GenericDAL 
objGenericDAL $
=% &
new' *

GenericDAL+ 5
(5 6
)6 7
;7 8
return 
objGenericDAL  
.  !
ObtenerClaves! .
(. /
tabla/ 4
)4 5
;5 6
} 	
} 
} ñ
[/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/FacturacionBL.cs
	namespace		 	
CapaNegocio		
 
{

 
public 

class 
FacturacionBL 
{ 
public 
List 
< 
FacturacionCLS "
>" #
ListarFacturaciones$ 7
(7 8
)8 9
{ 	
FacturacionDAL 
objFacturacionDAL ,
=- .
new/ 2
FacturacionDAL3 A
(A B
)B C
;C D
return 
objFacturacionDAL $
.$ %
ListarFacturaciones% 8
(8 9
)9 :
;: ;
} 	
public 
int 
GuardarFacturacion %
(% &
FacturacionCLS& 4
objFacturacionCLS5 F
)F G
{ 	
FacturacionDAL 
objFacturacionDAL ,
=- .
new/ 2
FacturacionDAL3 A
(A B
)B C
;C D
return 
objFacturacionDAL $
.$ %
GuardarFacturacion% 7
(7 8
objFacturacionCLS8 I
)I J
;J K
} 	
public 
int 
EliminarFacturacion &
(& '
int' *
id+ -
)- .
{ 	
FacturacionDAL 
objFacturacionDAL ,
=- .
new/ 2
FacturacionDAL3 A
(A B
)B C
;C D
return 
objFacturacionDAL $
.$ %
EliminarFacturacion% 8
(8 9
id9 ;
); <
;< =
} 	
public 
FacturacionCLS  
RecuperarFacturacion 2
(2 3
int3 6
id7 9
)9 :
{   	
FacturacionDAL!! 
objFacturacionDAL!! ,
=!!- .
new!!/ 2
FacturacionDAL!!3 A
(!!A B
)!!B C
;!!C D
return"" 
objFacturacionDAL"" $
.""$ % 
RecuperarFacturacion""% 9
(""9 :
id"": <
)""< =
;""= >
}## 	
public%% 
List%% 
<%% 
FacturacionCLS%% "
>%%" # 
FiltrarFacturaciones%%$ 8
(%%8 9
FacturacionCLS%%9 G
filtro%%H N
)%%N O
{&& 	
FacturacionDAL'' 
objFacturacionDAL'' ,
=''- .
new''/ 2
FacturacionDAL''3 A
(''A B
)''B C
;''C D
return(( 
objFacturacionDAL(( $
.(($ % 
FiltrarFacturaciones((% 9
(((9 :
filtro((: @
)((@ A
;((A B
})) 	
}** 
}++ Û
^/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/EspecialidadesBL.cs
	namespace		 	
CapaNegocio		
 
{

 
public 

class 
EspecialidadesBL !
{ 
public 
List 
< 
EspecialidadesCLS %
>% & 
ListarEspecialidades' ;
(; <
)< =
{ 	
EspecialidadesDAL  
objEspecialidadesDAL 2
=3 4
new5 8
EspecialidadesDAL9 J
(J K
)K L
;L M
return  
objEspecialidadesDAL '
.' ( 
ListarEspecialidades( <
(< =
)= >
;> ?
} 	
public 
int 
GuardarEspecialidad &
(& '
EspecialidadesCLS' 8
objEspecialidadCLS9 K
)K L
{ 	
EspecialidadesDAL  
objEspecialidadesDAL 2
=3 4
new5 8
EspecialidadesDAL9 J
(J K
)K L
;L M
return  
objEspecialidadesDAL '
.' (
GuardarEspecialidad( ;
(; <
objEspecialidadCLS< N
)N O
;O P
} 	
public 
int  
EliminarEspecialidad '
(' (
int( +
id, .
). /
{ 	
EspecialidadesDAL  
objEspecialidadesDAL 2
=3 4
new5 8
EspecialidadesDAL9 J
(J K
)K L
;L M
return  
objEspecialidadesDAL '
.' ( 
EliminarEspecialidad( <
(< =
id= ?
)? @
;@ A
} 	
public 
EspecialidadesCLS  !
RecuperarEspecialidad! 6
(6 7
int7 :
id; =
)= >
{   	
EspecialidadesDAL!!  
objEspecialidadesDAL!! 2
=!!3 4
new!!5 8
EspecialidadesDAL!!9 J
(!!J K
)!!K L
;!!L M
return""  
objEspecialidadesDAL"" '
.""' (!
RecuperarEspecialidad""( =
(""= >
id""> @
)""@ A
;""A B
}## 	
public%% 
List%% 
<%% 
EspecialidadesCLS%% %
>%%% &!
FiltrarEspecialidades%%' <
(%%< =
EspecialidadesCLS%%= N
filtro%%O U
)%%U V
{&& 	
EspecialidadesDAL''  
objEspecialidadesDAL'' 2
=''3 4
new''5 8
EspecialidadesDAL''9 J
(''J K
)''K L
;''L M
return((  
objEspecialidadesDAL(( '
.((' (!
FiltrarEspecialidades((( =
(((= >
filtro((> D
)((D E
;((E F
})) 	
}** 
}++ ¥
U/home/meatpuppets/Escritorio/University/proyectoHospital/Login/CapaNegocio/CitasBL.cs
	namespace		 	
CapaNegocio		
 
{

 
public 

class 
CitasBL 
{ 
public 
List 
< 
CitasCLS 
> 
ListarCitas )
() *
)* +
{ 	
CitasDAL 
objCitasDAL  
=! "
new# &
CitasDAL' /
(/ 0
)0 1
;1 2
return 
objCitasDAL 
. 
ListarCitas *
(* +
)+ ,
;, -
} 	
public 
int 
GuardarCita 
( 
CitasCLS '
objCitasCLS( 3
)3 4
{ 	
CitasDAL 
objCitasDAL  
=! "
new# &
CitasDAL' /
(/ 0
)0 1
;1 2
return 
objCitasDAL 
. 
GuardarCitas +
(+ ,
objCitasCLS, 7
)7 8
;8 9
} 	
public 
int 
EliminarCita 
(  
int  #
id$ &
)& '
{ 	
CitasDAL 
objCitasDAL  
=! "
new# &
CitasDAL' /
(/ 0
)0 1
;1 2
return 
objCitasDAL 
. 
EliminarCitas ,
(, -
id- /
)/ 0
;0 1
} 	
public 
CitasCLS 
RecuperarCitas &
(& '
int' *
idCita+ 1
)1 2
{ 	
CitasDAL 
objCitasDAL  
=! "
new# &
CitasDAL' /
(/ 0
)0 1
;1 2
return 
objCitasDAL 
. 
RecuperarCitas -
(- .
idCita. 4
)4 5
;5 6
}   	
public!! 
List!! 
<!! 
CitasCLS!! 
>!! 
FiltrarCitas!! *
(!!* +
CitasCLS!!+ 3
objCitasCLS!!4 ?
)!!? @
{"" 	
CitasDAL## 
objCitasDAL##  
=##! "
new### &
CitasDAL##' /
(##/ 0
)##0 1
;##1 2
return$$ 
objCitasDAL$$ 
.$$ 
FiltrarCitas$$ +
($$+ ,
objCitasCLS$$, 7
)$$7 8
;$$8 9
}%% 	
}&& 
}'' 