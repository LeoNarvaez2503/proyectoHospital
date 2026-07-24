∫
O/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. #
AddControllersWithViews (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddAuthentication "
(" #
$str# /
)/ 0
.		 
	AddCookie		 
(		 
$str		 
,		 
config		 #
=>		$ &
{

 
config 
. 
Cookie 
. 
Name 
= 
$str +
;+ ,
config 
. 
	LoginPath 
= 
$str *
;* +
config 
. 
AccessDeniedPath 
=  !
$str" 4
;4 5
} 
) 
; 
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
var 
	cadenaDAL 
= 
new 
	CadenaDAL 
( 
) 
;  
DatabaseInitializer 
. 

Initialize 
( 
	cadenaDAL (
.( )

cadenaDato) 3
)3 4
;4 5
if 
( 
! 
app 
. 	
Environment	 
. 
IsDevelopment "
(" #
)# $
)$ %
{ 
app 
. 
UseExceptionHandler 
( 
$str )
)) *
;* +
} 
app 
. 
UseAuthentication 
( 
) 
; 
app 
. 
UseAuthorization 
( 
) 
; 
app 
. 
UseStaticFiles 
( 
) 
; 
app 
. 

UseRouting 
( 
) 
; 
app 
. 
UseAuthorization 
( 
) 
; 
app!! 
.!! 
MapControllerRoute!! 
(!! 
name"" 
:"" 	
$str""
 
,"" 
pattern## 
:## 
$str## 7
)##7 8
;##8 9
app%% 
.%% 
Run%% 
(%% 
)%% 	
;%%	 
Å
V/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Models/Usuario.cs
	namespace 	
Login
 
. 
Models 
{ 
public 

class 
Usuario 
{ 
public 
int 
	idUsuario 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
correo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
clave 
{ 
get !
;! "
set# &
;& '
}( )
public		 
string		 
	confClave		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 
} ≥
]/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Models/ErrorViewModel.cs
	namespace 	
Login
 
. 
Models 
{ 
public 

class 
ErrorViewModel 
{ 
public 
string 
? 
	RequestId  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
ShowRequestId !
=>" $
!% &
string& ,
., -
IsNullOrEmpty- :
(: ;
	RequestId; D
)D E
;E F
} 
}		 î>
`/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Data/DatabaseInitializer.cs
	namespace 	
Login
 
. 
Data 
{ 
public 

static 
class 
DatabaseInitializer +
{ 
public 
static 
void 

Initialize %
(% &
string& ,
connectionString- =
)= >
{ 	
var		 
builder		 
=		 
new		 &
SqlConnectionStringBuilder		 8
(		8 9
connectionString		9 I
)		I J
;		J K
string

 
dbName

 
=

 
builder

 #
.

# $
InitialCatalog

$ 2
;

2 3
builder 
. 
InitialCatalog "
=# $
$str% -
;- .
string 
serverConnStr  
=! "
builder# *
.* +
ConnectionString+ ;
;; <
WaitForSqlServer 
( 
serverConnStr *
)* +
;+ ,
using 
( 
var 
conn 
= 
new !
SqlConnection" /
(/ 0
serverConnStr0 =
)= >
)> ?
{ 
conn 
. 
Open 
( 
) 
; 
using 
( 
var 
cmd 
=  
conn! %
.% &
CreateCommand& 3
(3 4
)4 5
)5 6
{ 
cmd 
. 
CommandText #
=$ %
$@"& )
$str) Q
{Q R
dbNameR X
}X Y
$strY )
{) *
dbName* 0
}0 1
$str1 2
"2 3
;3 4
cmd 
. 
ExecuteNonQuery '
(' (
)( )
;) *
} 
} 
string 
initScriptPath !
=" #
Path$ (
.( )
Combine) 0
(0 1

AppContext1 ;
.; <
BaseDirectory< I
,I J
$strK U
)U V
;V W
if 
( 
! 
File 
. 
Exists 
( 
initScriptPath +
)+ ,
), -
{ 
Console 
. 
	WriteLine !
(! "
$str" k
)k l
;l m
return   
;   
}!! 
string## 
	dbConnStr## 
=## 
connectionString## /
;##/ 0
using$$ 
($$ 
var$$ 
conn$$ 
=$$ 
new$$ !
SqlConnection$$" /
($$/ 0
	dbConnStr$$0 9
)$$9 :
)$$: ;
{%% 
conn&& 
.&& 
Open&& 
(&& 
)&& 
;&& 
using'' 
('' 
var'' 
cmd'' 
=''  
conn''! %
.''% &
CreateCommand''& 3
(''3 4
)''4 5
)''5 6
{(( 
cmd)) 
.)) 
CommandText)) #
=))$ %
$str))& s
;))s t
int** 
count** 
=** 
(**  !
int**! $
)**$ %
cmd**% (
.**( )
ExecuteScalar**) 6
(**6 7
)**7 8
;**8 9
if++ 
(++ 
count++ 
>++ 
$num++  !
)++! "
{,, 
Console-- 
.--  
	WriteLine--  )
(--) *
$str--* n
)--n o
;--o p
return.. 
;.. 
}// 
}00 
}11 
Console33 
.33 
	WriteLine33 
(33 
$str33 L
)33L M
;33M N
string44 
script44 
=44 
File44  
.44  !
ReadAllText44! ,
(44, -
initScriptPath44- ;
)44; <
;44< =
string55 
[55 
]55 
batches55 
=55 
script55 %
.55% &
Split55& +
(55+ ,
new55, /
[55/ 0
]550 1
{552 3
$str554 @
,55@ A
$str55B J
,55J K
$str55L T
,55T U
$str55V Z
}55[ \
,55\ ]
StringSplitOptions55^ p
.55p q
RemoveEmptyEntries	55q É
)
55É Ñ
;
55Ñ Ö
using77 
(77 
var77 
conn77 
=77 
new77 !
SqlConnection77" /
(77/ 0
	dbConnStr770 9
)779 :
)77: ;
{88 
conn99 
.99 
Open99 
(99 
)99 
;99 
foreach:: 
(:: 
string:: 
batch::  %
in::& (
batches::) 0
)::0 1
{;; 
string<< 
trimmed<< "
=<<# $
batch<<% *
.<<* +
Trim<<+ /
(<</ 0
)<<0 1
;<<1 2
if== 
(== 
string== 
.== 
IsNullOrEmpty== ,
(==, -
trimmed==- 4
)==4 5
)==5 6
continue==7 ?
;==? @
using>> 
(>> 
var>> 
cmd>> "
=>># $
new>>% (

SqlCommand>>) 3
(>>3 4
trimmed>>4 ;
,>>; <
conn>>= A
)>>A B
)>>B C
{?? 
cmd@@ 
.@@ 
ExecuteNonQuery@@ +
(@@+ ,
)@@, -
;@@- .
}AA 
}BB 
}CC 
ConsoleEE 
.EE 
	WriteLineEE 
(EE 
$strEE _
)EE_ `
;EE` a
}FF 	
privateHH 
staticHH 
voidHH 
WaitForSqlServerHH ,
(HH, -
stringHH- 3
connectionStringHH4 D
,HHD E
intHHF I

maxRetriesHHJ T
=HHU V
$numHHW Y
,HHY Z
intHH[ ^
delaySecondsHH_ k
=HHl m
$numHHn o
)HHo p
{II 	
forJJ 
(JJ 
intJJ 
iJJ 
=JJ 
$numJJ 
;JJ 
iJJ 
<JJ 

maxRetriesJJ  *
;JJ* +
iJJ, -
++JJ- /
)JJ/ 0
{KK 
tryLL 
{MM 
usingNN 
varNN 
connNN "
=NN# $
newNN% (
SqlConnectionNN) 6
(NN6 7
connectionStringNN7 G
)NNG H
;NNH I
connOO 
.OO 
OpenOO 
(OO 
)OO 
;OO  
ConsolePP 
.PP 
	WriteLinePP %
(PP% &
$strPP& O
)PPO P
;PPP Q
returnQQ 
;QQ 
}RR 
catchSS 
(SS 
SqlExceptionSS #
)SS# $
{TT 
ConsoleUU 
.UU 
	WriteLineUU %
(UU% &
$"UU& (
$strUU( W
{UUW X
iUUX Y
+UUZ [
$numUU\ ]
}UU] ^
$strUU^ _
{UU_ `

maxRetriesUU` j
}UUj k
$strUUk l
"UUl m
)UUm n
;UUn o
ThreadVV 
.VV 
SleepVV  
(VV  !
TimeSpanVV! )
.VV) *
FromSecondsVV* 5
(VV5 6
delaySecondsVV6 B
)VVB C
)VVC D
;VVD E
}WW 
}XX 
throwYY 
newYY 
TimeoutExceptionYY &
(YY& '
$strYY' R
)YYR S
;YYS T
}ZZ 	
}[[ 
}\\ –
j/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/TratamientosController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
]  
public

 

class

 "
TratamientosController

 '
:

( )

Controller

* 4
{ 
public 
IActionResult 
Index "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
List 
< 
TratamientosCLS #
># $
ListarTratamientos% 7
(7 8
)8 9
{ 	
TratamientosBL 
objTratamientosBL ,
=- .
new/ 2
TratamientosBL3 A
(A B
)B C
;C D
return 
objTratamientosBL $
.$ %
ListarTratamientos% 7
(7 8
)8 9
;9 :
} 	
public 
int 
GuardarTratamiento %
(% &
TratamientosCLS& 5
objTratamientoCLS6 G
)G H
{ 	
TratamientosBL 
objTratamientosBL ,
=- .
new/ 2
TratamientosBL3 A
(A B
)B C
;C D
return 
objTratamientosBL $
.$ %
GuardarTratamiento% 7
(7 8
objTratamientoCLS8 I
)I J
;J K
} 	
public 
int 
EliminarTratamiento &
(& '
int' *
id+ -
)- .
{ 	
TratamientosBL 
objTratamientosBL ,
=- .
new/ 2
TratamientosBL3 A
(A B
)B C
;C D
return 
objTratamientosBL $
.$ %
EliminarTratamiento% 8
(8 9
id9 ;
); <
;< =
}   	
public"" 
TratamientosCLS""  
RecuperarTratamiento"" 3
(""3 4
int""4 7
id""8 :
)"": ;
{## 	
TratamientosBL$$ 
objTratamientosBL$$ ,
=$$- .
new$$/ 2
TratamientosBL$$3 A
($$A B
)$$B C
;$$C D
return%% 
objTratamientosBL%% $
.%%$ % 
RecuperarTratamiento%%% 9
(%%9 :
id%%: <
)%%< =
;%%= >
}&& 	
public(( 
List(( 
<(( 
TratamientosCLS(( #
>((# $
FiltrarTratamientos((% 8
(((8 9
TratamientosCLS((9 H
filtro((I O
)((O P
{)) 	
TratamientosBL** 
objTratamientosBL** ,
=**- .
new**/ 2
TratamientosBL**3 A
(**A B
)**B C
;**C D
return++ 
objTratamientosBL++ $
.++$ %
FiltrarTratamientos++% 8
(++8 9
filtro++9 ?
)++? @
;++@ A
},, 	
}-- 
}.. ÷
g/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/PacientesController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
[ 
	Authorize 
( 
Roles 
= 
$str '
)' (
]( )
public

 

class

 
PacientesController

 $
:

% &

Controller

' 1
{ 
public 
IActionResult 
Index "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
List 
< 
PacienteCLS 
>  
ListarPacientes! 0
(0 1
)1 2
{ 	
PacientesBL 
objPacientesBL &
=' (
new) ,
PacientesBL- 8
(8 9
)9 :
;: ;
return 
objPacientesBL !
.! "
ListarPacientes" 1
(1 2
)2 3
;3 4
} 	
public 
int 
GuardarPaciente "
(" #
PacienteCLS# .
objPacienteCLS/ =
)= >
{ 	
PacientesBL 
objPacientesBL &
=' (
new) ,
PacientesBL- 8
(8 9
)9 :
;: ;
return 
objPacientesBL !
.! "
GuardarPaciente" 1
(1 2
objPacienteCLS2 @
)@ A
;A B
} 	
public 
int 
EliminarPaciente #
(# $
int$ '
id( *
)* +
{ 	
PacientesBL 
objPacientesBL &
=' (
new) ,
PacientesBL- 8
(8 9
)9 :
;: ;
return 
objPacientesBL !
.! "
EliminarPaciente" 2
(2 3
id3 5
)5 6
;6 7
}   	
public"" 
PacienteCLS"" 
RecuperarPaciente"" ,
("", -
int""- 0
id""1 3
)""3 4
{## 	
PacientesBL$$ 
objPacientesBL$$ &
=$$' (
new$$) ,
PacientesBL$$- 8
($$8 9
)$$9 :
;$$: ;
return%% 
objPacientesBL%% !
.%%! "
RecuperarPaciente%%" 3
(%%3 4
id%%4 6
)%%6 7
;%%7 8
}&& 	
public(( 
List(( 
<(( 
PacienteCLS(( 
>((  
FiltrarPacientes((! 1
(((1 2
PacienteCLS((2 =
filtro((> D
)((D E
{)) 	
PacientesBL** 
objPacientesBL** &
=**' (
new**) ,
PacientesBL**- 8
(**8 9
)**9 :
;**: ;
return++ 
objPacientesBL++ !
.++! "
FiltrarPacientes++" 2
(++2 3
filtro++3 9
)++9 :
;++: ;
},, 	
}-- 
}.. ç
e/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/MedicosController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
]  
public

 

class

 
MedicosController

 "
:

# $

Controller

% /
{ 
public 
IActionResult 
Index "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
List 
< 

MedicosCLS 
> 
ListarMedicos  -
(- .
). /
{ 	
	MedicosBL 
objMedicosBL "
=# $
new% (
	MedicosBL) 2
(2 3
)3 4
;4 5
return 
objMedicosBL 
.  
ListarMedicos  -
(- .
). /
;/ 0
} 	
public 
int 
GuardarMedico  
(  !

MedicosCLS! +
objMedicoCLS, 8
)8 9
{ 	
	MedicosBL 
objMedicosBL "
=# $
new% (
	MedicosBL) 2
(2 3
)3 4
;4 5
return 
objMedicosBL 
.  
GuardarMedico  -
(- .
objMedicoCLS. :
): ;
;; <
} 	
public 
int 
EliminarMedico !
(! "
int" %
id& (
)( )
{ 	
	MedicosBL 
objMedicosBL "
=# $
new% (
	MedicosBL) 2
(2 3
)3 4
;4 5
return 
objMedicosBL 
.  
EliminarMedico  .
(. /
id/ 1
)1 2
;2 3
}   	
public"" 

MedicosCLS"" 
RecuperarMedico"" )
("") *
int""* -
id"". 0
)""0 1
{## 	
	MedicosBL$$ 
objMedicosBL$$ "
=$$# $
new$$% (
	MedicosBL$$) 2
($$2 3
)$$3 4
;$$4 5
return%% 
objMedicosBL%% 
.%%  
RecuperarMedico%%  /
(%%/ 0
id%%0 2
)%%2 3
;%%3 4
}&& 	
public(( 
List(( 
<(( 

MedicosCLS(( 
>(( 
FiltrarMedicos((  .
(((. /

MedicosCLS((/ 9
filtro((: @
)((@ A
{)) 	
	MedicosBL** 
objMedicosBL** "
=**# $
new**% (
	MedicosBL**) 2
(**2 3
)**3 4
;**4 5
return++ 
objMedicosBL++ 
.++  
FiltrarMedicos++  .
(++. /
filtro++/ 5
)++5 6
;++6 7
},, 	
}-- 
}.. ™
b/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/HomeController.cs
	namespace 	
Login
 
. 
Controllers 
; 
public		 
class		 
HomeController		 
:		 

Controller		 (
{

 
private 
readonly 
ILogger 
< 
HomeController +
>+ ,
_logger- 4
;4 5
public 

HomeController 
( 
ILogger !
<! "
HomeController" 0
>0 1
logger2 8
)8 9
{ 
_logger 
= 
logger 
; 
} 
public 

IActionResult 
Index 
( 
)  
{ 
return 
View 
( 
) 
; 
} 
public 

IActionResult 
Privacy  
(  !
)! "
{ 
return 
View 
( 
) 
; 
} 
[ 
ResponseCache 
( 
Duration 
= 
$num 
,  
Location! )
=* +!
ResponseCacheLocation, A
.A B
NoneB F
,F G
NoStoreH O
=P Q
trueR V
)V W
]W X
public 

IActionResult 
Error 
( 
)  
{ 
return 
View 
( 
new 
ErrorViewModel &
{' (
	RequestId) 2
=3 4
Activity5 =
.= >
Current> E
?E F
.F G
IdG I
??J L
HttpContextM X
.X Y
TraceIdentifierY h
}i j
)j k
;k l
}   
public!! 

List!! 
<!! 
CitasCLS!! 
>!! 
ListarCitas!! %
(!!% &
)!!& '
{"" 
CitasDAL## 
objCitasDAL## 
=## 
new## "
CitasDAL### +
(##+ ,
)##, -
;##- .
return$$ 
objCitasDAL$$ 
.$$ 
ListarCitas$$ &
($$& '
)$$' (
;$$( )
}%% 
}&& ≠
e/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/GenericController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
public 

class 
GenericController "
:# $

Controller% /
{ 
public 
IActionResult 
Index "
(" #
)# $
{		 	
return

 
View

 
(

 
)

 
;

 
} 	
public 
List 
< 
int 
> 
obtenerClaves &
(& '
string' -
tabla. 3
)3 4
{ 	
	GenericBL 
objGenericBL "
=# $
new% (
	GenericBL) 2
(2 3
)3 4
;4 5
return 
objGenericBL 
.  
obtenerClaves  -
(- .
tabla. 3
)3 4
;4 5
} 	
} 
} π
i/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/FacturacionController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
[ 
	Authorize 
( 
Roles 
= 
$str '
)' (
]( )
public		 

class		 !
FacturacionController		 &
:		' (

Controller		) 3
{

 
public 
IActionResult 
Index "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
List 
< 
FacturacionCLS "
>" #
ListarFacturaciones$ 7
(7 8
)8 9
{ 	
FacturacionBL 
objFacturacionBL *
=+ ,
new- 0
FacturacionBL1 >
(> ?
)? @
;@ A
return 
objFacturacionBL #
.# $
ListarFacturaciones$ 7
(7 8
)8 9
;9 :
} 	
public 
int 
GuardarFacturacion %
(% &
FacturacionCLS& 4
objFacturacionCLS5 F
)F G
{ 	
FacturacionBL 
objFacturacionBL *
=+ ,
new- 0
FacturacionBL1 >
(> ?
)? @
;@ A
return 
objFacturacionBL #
.# $
GuardarFacturacion$ 6
(6 7
objFacturacionCLS7 H
)H I
;I J
} 	
public 
int 
EliminarFacturacion &
(& '
int' *
id+ -
)- .
{ 	
FacturacionBL 
objFacturacionBL *
=+ ,
new- 0
FacturacionBL1 >
(> ?
)? @
;@ A
return 
objFacturacionBL #
.# $
EliminarFacturacion$ 7
(7 8
id8 :
): ;
;; <
} 	
public!! 
FacturacionCLS!!  
RecuperarFacturacion!! 2
(!!2 3
int!!3 6
id!!7 9
)!!9 :
{"" 	
FacturacionBL## 
objFacturacionBL## *
=##+ ,
new##- 0
FacturacionBL##1 >
(##> ?
)##? @
;##@ A
return$$ 
objFacturacionBL$$ #
.$$# $ 
RecuperarFacturacion$$$ 8
($$8 9
id$$9 ;
)$$; <
;$$< =
}%% 	
public'' 
List'' 
<'' 
FacturacionCLS'' "
>''" # 
FiltrarFacturaciones''$ 8
(''8 9
FacturacionCLS''9 G
filtro''H N
)''N O
{(( 	
FacturacionBL)) 
objFacturacionBL)) *
=))+ ,
new))- 0
FacturacionBL))1 >
())> ?
)))? @
;))@ A
return** 
objFacturacionBL** #
.**# $ 
FiltrarFacturaciones**$ 8
(**8 9
filtro**9 ?
)**? @
;**@ A
}++ 	
},, 
}-- ñ
l/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/EspecialidadesController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
[ 
	Authorize 
( 
Roles 
= 
$str 
) 
]  
public		 

class		 $
EspecialidadesController		 )
:		* +

Controller		, 6
{

 
public 
IActionResult 
Index "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
List 
< 
EspecialidadesCLS %
>% & 
ListarEspecialidades' ;
(; <
)< =
{ 	
EspecialidadesBL 
objEspecialidadesBL 0
=1 2
new3 6
EspecialidadesBL7 G
(G H
)H I
;I J
return 
objEspecialidadesBL &
.& ' 
ListarEspecialidades' ;
(; <
)< =
;= >
} 	
public 
int 
GuardarEspecialidad &
(& '
EspecialidadesCLS' 8
objEspecialidadCLS9 K
)K L
{ 	
EspecialidadesBL 
objEspecialidadesBL 0
=1 2
new3 6
EspecialidadesBL7 G
(G H
)H I
;I J
return 
objEspecialidadesBL &
.& '
GuardarEspecialidad' :
(: ;
objEspecialidadCLS; M
)M N
;N O
} 	
public 
int  
EliminarEspecialidad '
(' (
int( +
id, .
). /
{ 	
EspecialidadesBL 
objEspecialidadesBL 0
=1 2
new3 6
EspecialidadesBL7 G
(G H
)H I
;I J
return 
objEspecialidadesBL &
.& ' 
EliminarEspecialidad' ;
(; <
id< >
)> ?
;? @
} 	
public!! 
EspecialidadesCLS!!  !
RecuperarEspecialidad!!! 6
(!!6 7
int!!7 :
id!!; =
)!!= >
{"" 	
EspecialidadesBL## 
objEspecialidadesBL## 0
=##1 2
new##3 6
EspecialidadesBL##7 G
(##G H
)##H I
;##I J
return$$ 
objEspecialidadesBL$$ &
.$$& '!
RecuperarEspecialidad$$' <
($$< =
id$$= ?
)$$? @
;$$@ A
}%% 	
public'' 
List'' 
<'' 
EspecialidadesCLS'' %
>''% &!
FiltrarEspecialidades''' <
(''< =
EspecialidadesCLS''= N
filtro''O U
)''U V
{(( 	
EspecialidadesBL)) 
objEspecialidadesBL)) 0
=))1 2
new))3 6
EspecialidadesBL))7 G
())G H
)))H I
;))I J
return** 
objEspecialidadesBL** &
.**& '!
FiltrarEspecialidades**' <
(**< =
filtro**= C
)**C D
;**D E
}++ 	
},, 
}-- Õ
c/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/CitasController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
[		 
	Authorize		 
(		 
Roles		 
=		 
$str		 '
)		' (
]		( )
public

 

class

 
CitasController

  
:

! "

Controller

# -
{ 
public 
IActionResult 
Citas "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
List 
< 
CitasCLS 
> 
ListarCitas )
() *
)* +
{ 	
CitasBL 

objCitasBL 
=  
new! $
CitasBL% ,
(, -
)- .
;. /
return 

objCitasBL 
. 
ListarCitas )
() *
)* +
;+ ,
} 	
public 
int 
GuardarCita 
( 
CitasCLS '
objCitasCLS( 3
)3 4
{ 	
CitasBL 

objCitasBL 
=  
new! $
CitasBL% ,
(, -
)- .
;. /
return 

objCitasBL 
. 
GuardarCita )
() *
objCitasCLS* 5
)5 6
;6 7
} 	
public 
int 
EliminarCita 
(  
int  #
id$ &
)& '
{ 	
CitasBL 

objCitasBL 
=  
new! $
CitasBL% ,
(, -
)- .
;. /
return 

objCitasBL 
. 
EliminarCita *
(* +
id+ -
)- .
;. /
} 	
public!! 
CitasCLS!! 
RecuperarCitas!! &
(!!& '
int!!' *
id!!+ -
)!!- .
{"" 	
CitasBL## 

objCitasBL## 
=##  
new##! $
CitasBL##% ,
(##, -
)##- .
;##. /
return$$ 

objCitasBL$$ 
.$$ 
RecuperarCitas$$ ,
($$, -
id$$- /
)$$/ 0
;$$0 1
}%% 	
public'' 
List'' 
<'' 
CitasCLS'' 
>'' 
FiltrarCitas'' *
(''* +
CitasCLS''+ 3
objCitasCLS''4 ?
)''? @
{(( 	
CitasBL)) 

objCitasBL)) 
=))  
new))! $
CitasBL))% ,
()), -
)))- .
;)). /
return** 

objCitasBL** 
.** 
FiltrarCitas** *
(*** +
objCitasCLS**+ 6
)**6 7
;**7 8
}++ 	
},, 
}-- Ù;
d/home/meatpuppets/Escritorio/University/proyectoHospital/Login/Login/Controllers/AccesoController.cs
	namespace 	
Login
 
. 
Controllers 
{ 
public 

class 
AccesoController !
:" #

Controller$ .
{ 
public 
IActionResult 
Login "
(" #
)# $
{ 	
return 
View 
( 
) 
; 
} 	
public 
IActionResult 
	Registrar &
(& '
)' (
{ 	
return 
View 
( 
) 
; 
} 	
public 
IActionResult 
Denegado %
(% &
)& '
{ 	
return 
View 
( 
) 
; 
} 	
[ 	
HttpPost	 
] 
public 
IActionResult 
	Registrar &
(& '

UsuarioCLS' 1
objUser2 9
)9 :
{   	
if!! 
(!! 
objUser!! 
.!! 
clave!! 
!=!!  
objUser!!! (
.!!( )
	confClave!!) 2
)!!2 3
{"" 
ViewData## 
[## 
$str## "
]##" #
=##$ %
$str##& D
;##D E
return$$ 
View$$ 
($$ 
)$$ 
;$$ 
}%% 
objUser&& 
.&& 
clave&& 
=&& 
	Encriptar&& %
(&&% &
objUser&&& -
.&&- .
clave&&. 3
)&&3 4
;&&4 5

UsuarioDAL(( 

objUserDAL(( !
=((" #
new(($ '

UsuarioDAL((( 2
(((2 3
)((3 4
;((4 5
bool)) 

registrado)) 
=)) 

objUserDAL)) (
.))( )
RegistrarUsuario))) 9
())9 :
objUser)): A
,))A B
out))C F
string))G M
mensaje))N U
)))U V
;))V W
if++ 
(++ 

registrado++ 
)++ 
{,, 
TempData-- 
[-- 
$str-- '
]--' (
=--) *
$str--+ Y
;--Y Z
return.. 
RedirectToAction.. '
(..' (
$str..( /
)../ 0
;..0 1
}// 
else00 
{11 
ViewData22 
[22 
$str22 "
]22" #
=22$ %
mensaje22& -
;22- .
return33 
View33 
(33 
$str33 #
)33# $
;33$ %
}44 
}66 	
private88 
string88 
	Encriptar88  
(88  !
string88! '
cadena88( .
)88. /
{99 	
StringBuilder:: 
builder:: !
=::" #
new::$ '
StringBuilder::( 5
(::5 6
)::6 7
;::7 8
using;; 
(;; 
SHA256;; 

sha256Hash;; $
=;;% &
SHA256;;' -
.;;- .
Create;;. 4
(;;4 5
);;5 6
);;6 7
{<< 
byte== 
[== 
]== 
result== 
=== 

sha256Hash==  *
.==* +
ComputeHash==+ 6
(==6 7
Encoding==7 ?
.==? @
UTF8==@ D
.==D E
GetBytes==E M
(==M N
cadena==N T
)==T U
)==U V
;==V W
foreach>> 
(>> 
byte>> 
b>> 
in>>  "
result>># )
)>>) *
builder?? 
.?? 
Append?? "
(??" #
b??# $
.??$ %
ToString??% -
(??- .
$str??. 2
)??2 3
)??3 4
;??4 5
}@@ 
returnAA 
builderAA 
.AA 
ToStringAA #
(AA# $
)AA$ %
;AA% &
}BB 	
[CC 	
HttpPostCC	 
]CC 
publicDD 
asyncDD 
TaskDD 
<DD 
IActionResultDD '
>DD' (
LoginDD) .
(DD. /

UsuarioCLSDD/ 9
objUserDD: A
)DDA B
{EE 	
objUserFF 
.FF 
claveFF 
=FF 
	EncriptarFF %
(FF% &
objUserFF& -
.FF- .
claveFF. 3
)FF3 4
;FF4 5
stringGG 
mensajeGG 
;GG 
intHH 
	idUsuarioHH 
;HH 
stringII 
rolII 
;II 

UsuarioDALJJ 

objUserDALJJ !
=JJ" #
newJJ$ '

UsuarioDALJJ( 2
(JJ2 3
)JJ3 4
;JJ4 5
boolKK 
exitoKK 
=KK 

objUserDALKK #
.KK# $
IniciarSesionKK$ 1
(KK1 2
objUserKK2 9
,KK9 :
outKK; >
mensajeKK? F
,KKF G
outKKG J
	idUsuarioKKK T
,KKT U
outKKV Y
rolKKZ ]
)KK] ^
;KK^ _
ifLL 
(LL 
exitoLL 
)LL 
{MM 
varNN 
claimsNN 
=NN 
newNN  
ListNN! %
<NN% &
ClaimNN& +
>NN+ ,
{OO 
newPP 
ClaimPP 
(PP 

ClaimTypesPP (
.PP( )
NamePP) -
,PP- .
objUserPP/ 6
.PP6 7
correoPP7 =
)PP= >
,PP> ?
newQQ 
ClaimQQ 
(QQ 

ClaimTypesQQ (
.QQ( )
RoleQQ) -
,QQ- .
rolQQ/ 2
)QQ2 3
}RR 
;RR 
varSS 
identitySS 
=SS 
newSS "
ClaimsIdentitySS# 1
(SS1 2
claimsSS2 8
,SS8 9
$strSS: F
)SSF G
;SSG H
varTT 
	principalTT 
=TT 
newTT  #
ClaimsPrincipalTT$ 3
(TT3 4
identityTT4 <
)TT< =
;TT= >
awaitUU 
HttpContextUU !
.UU! "
SignInAsyncUU" -
(UU- .
$strUU. :
,UU: ;
	principalUU< E
)UUE F
;UUF G
returnVV 
RedirectToActionVV '
(VV' (
$strVV( /
,VV/ 0
$strVV1 7
)VV7 8
;VV8 9
}WW 
elseXX 
{YY 
ViewDataZZ 
[ZZ 
$strZZ "
]ZZ" #
=ZZ$ %
mensajeZZ& -
;ZZ- .
return[[ 
View[[ 
([[ 
)[[ 
;[[ 
}\\ 
}]] 	
[^^ 	
HttpPost^^	 
]^^ 
public__ 
async__ 
Task__ 
<__ 
IActionResult__ '
>__' (
Logout__) /
(__/ 0
)__0 1
{`` 	
awaitbb 
HttpContextbb 
.bb 
SignOutAsyncbb *
(bb* +
$strbb+ 7
)bb7 8
;bb8 9
returnee 
RedirectToActionee #
(ee# $
$stree$ +
,ee+ ,
$stree- 5
)ee5 6
;ee6 7
}ff 	
publicgg 
boolgg 
RevisarPermisosgg #
(gg# $
)gg$ %
{hh 	
returnii 
Userii 
.ii 
IsInRoleii  
(ii  !
$strii! (
)ii( )
;ii) *
}jj 	
}kk 
}ll 