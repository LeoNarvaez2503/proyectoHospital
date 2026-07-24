import concurrent.futures
import time
import urllib.request
import urllib.parse
import http.cookiejar
import statistics

BASE_URL = "http://localhost:5076"
TOTAL_REQUESTS = 200  # 200 peticiones por escenario (1,200 inserciones en BD)
CONCURRENCY = 50      # 50 Usuarios Virtuales Concurrentes (Estrés Alto)

print("=" * 75)
print("🔥 INICIANDO PRUEBA DE ESTRÉS EXTREMO Y RESILIENCIA SQA (HIGH STRESS TEST)")
print(f"👥 Usuarios Virtuales Concurrentes (Virtual Users): {CONCURRENCY}")
print(f"📊 Peticiones por Módulo: {TOTAL_REQUESTS} | Total Inserciones: {TOTAL_REQUESTS * 6}")
print("=" * 75)

# 1. Autenticar usuario Admin para obtener Cookie de Sesión
print("🔑 Autenticando Administrador en el servidor Kestrel...")
cookie_jar = http.cookiejar.CookieJar()
opener = urllib.request.build_opener(urllib.request.HTTPCookieProcessor(cookie_jar))

login_data = urllib.parse.urlencode({
    "correo": "admin@hospital.com",
    "clave": "Admin123!"
}).encode('utf-8')

login_req = urllib.request.Request(
    f"{BASE_URL}/Acceso/Login",
    data=login_data,
    headers={"Content-Type": "application/x-www-form-urlencoded"},
    method="POST"
)

try:
    with opener.open(login_req) as resp:
        print(f"✅ Autenticación exitosa en servidor (Status: {resp.status})")
except Exception as e:
    print(f"⚠️ Error durante login inicial: {e}")

cookie_header = "; ".join([f"{cookie.name}={cookie.value}" for cookie in cookie_jar])
print(f"🍪 Cookie de Sesión activa: {cookie_header[:40]}...\n")

TEST_SCENARIOS = [
    {
        "name": "1. Módulo Pacientes (Creación Masiva en BD)",
        "url": f"{BASE_URL}/Pacientes/GuardarPaciente",
        "method": "POST",
        "data": lambda i: urllib.parse.urlencode({
            "Id": "0",
            "Nombre": f"PacienteEstres_{i}",
            "Apellido": "PruebaCargaSQA",
            "FechaNacimiento": "1995-08-20T00:00:00",
            "Telefono": f"099{i:07d}",
            "Email": f"estres_paciente_{i}@hospital.com",
            "Direccion": "Quito, Ecuador"
        }).encode('utf-8')
    },
    {
        "name": "2. Módulo Médicos (Creación Masiva en BD)",
        "url": f"{BASE_URL}/Medicos/GuardarMedico",
        "method": "POST",
        "data": lambda i: urllib.parse.urlencode({
            "Id": "0",
            "Nombre": f"DrEstres_{i}",
            "Apellido": "EspecialistaSQA",
            "EspecialidadId": "1",
            "Telefono": f"098{i:07d}",
            "Email": f"estres_medico_{i}@hospital.com"
        }).encode('utf-8')
    },
    {
        "name": "3. Módulo Citas Médicas (Creación Masiva en BD)",
        "url": f"{BASE_URL}/Citas/GuardarCita",
        "method": "POST",
        "data": lambda i: urllib.parse.urlencode({
            "idCita": "0",
            "idPaciente": "1",
            "idMedico": "1",
            "fecha": "2026-07-30T14:30:00",
            "estado": "Confirmada"
        }).encode('utf-8')
    },
    {
        "name": "4. Módulo Tratamientos (Creación Masiva en BD)",
        "url": f"{BASE_URL}/Tratamientos/GuardarTratamiento",
        "method": "POST",
        "data": lambda i: urllib.parse.urlencode({
            "Id": "0",
            "PacienteId": "1",
            "Descripcion": f"Tratamiento Estrés #{i} - Dosis Intensiva",
            "Fecha": "2026-07-30T15:00:00",
            "Costo": "125.00"
        }).encode('utf-8')
    },
    {
        "name": "5. Módulo Facturación (Creación Masiva en BD)",
        "url": f"{BASE_URL}/Facturacion/GuardarFacturacion",
        "method": "POST",
        "data": lambda i: urllib.parse.urlencode({
            "Id": "0",
            "PacienteId": "1",
            "Monto": "350.75",
            "MetodoPago": "Tarjeta",
            "FechaPago": "2026-07-30T16:00:00"
        }).encode('utf-8')
    },
    {
        "name": "6. Módulo Especialidades (Creación Masiva en BD)",
        "url": f"{BASE_URL}/Especialidades/GuardarEspecialidad",
        "method": "POST",
        "data": lambda i: urllib.parse.urlencode({
            "Id": "0",
            "Nombre": f"EspecialidadEstres_{i}"
        }).encode('utf-8')
    }
]

def execute_scenario(scenario):
    print("=" * 75)
    print(f"🔥 RÁFAGA DE ESTRÉS: {scenario['name']}")
    print(f"🎯 50 HILOS CONCURRENTES SIMULTÁNEOS -> {scenario['url']}")
    print("=" * 75)

    latencies = []
    statuses = []

    def make_request(request_id):
        start = time.perf_counter()
        try:
            body = scenario['data'](request_id)
            headers = {
                "User-Agent": "SQA-HighStressTester/2.0",
                "Content-Type": "application/x-www-form-urlencoded"
            }
            if cookie_header:
                headers["Cookie"] = cookie_header

            req = urllib.request.Request(
                scenario['url'],
                data=body,
                headers=headers,
                method=scenario['method']
            )
            with urllib.request.urlopen(req, timeout=15) as response:
                status = response.status
                response.read()
                elapsed = (time.perf_counter() - start) * 1000  # ms
                return status, elapsed
        except urllib.error.HTTPError as e:
            elapsed = (time.perf_counter() - start) * 1000
            return e.code, elapsed
        except Exception:
            elapsed = (time.perf_counter() - start) * 1000
            return 500, elapsed

    start_total = time.perf_counter()
    with concurrent.futures.ThreadPoolExecutor(max_workers=CONCURRENCY) as executor:
        futures = [executor.submit(make_request, i) for i in range(TOTAL_REQUESTS)]
        for future in concurrent.futures.as_completed(futures):
            status, elapsed = future.result()
            statuses.append(status)
            latencies.append(elapsed)

    total_duration = time.perf_counter() - start_total
    successful = len([s for s in statuses if s in (200, 302)])
    failed = TOTAL_REQUESTS - successful
    rps = TOTAL_REQUESTS / total_duration
    avg_latency = statistics.mean(latencies)
    p95_latency = sorted(latencies)[int(0.95 * len(latencies))]

    print(f"✅ Inserciones Exitosas en BD:        {successful} / {TOTAL_REQUESTS} ({successful/TOTAL_REQUESTS*100:.1f}%)")
    print(f"❌ Peticiones Fallidas:               {failed}")
    print(f"⏱️ Tiempo Total de Ráfaga:             {total_duration:.2f} segundos")
    print(f"⚡ Throughput (Peticiones/segundo):    {rps:.2f} req/sec")
    print(f"🚀 Latencia Promedio (Avg):           {avg_latency:.2f} ms")
    print(f"📊 Latencia Percentil 95 (P95):       {p95_latency:.2f} ms")
    print(f"📈 Latencia Máxima registrada:       {max(latencies):.2f} ms\n")

if __name__ == "__main__":
    for scenario in TEST_SCENARIOS:
        execute_scenario(scenario)
