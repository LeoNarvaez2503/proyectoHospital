# Guía de Ejecución de Análisis Estático de Código con SonarQube

Esta guía describe los pasos necesarios para ejecutar la auditoría de análisis estático de código sobre el proyecto **Sistema de Gestión Hospitalaria (SUT)** utilizando **SonarQube Community Edition** en Docker y la herramienta **dotnet-sonarscanner**.

---

## 📋 Requisitos Previos

1. **Docker Engine y Docker Compose** instalados y ejecutándose.
2. **.NET SDK** (versión 8.0 o 9.0) disponible en el sistema.
3. Herramienta global de escaneo `.NET`:
   ```bash
   dotnet tool install --global dotnet-sonarscanner
   ```
   *(Si ya está instalada, asegúrate de tener activas las variables de entorno `PATH` y `DOTNET_ROOT`).*

---

## 🚀 Pasos de Ejecución

### Paso 1: Levantar el contenedor de SonarQube
Si el contenedor de SonarQube no está activo, inícialo mediante Docker:
```bash
docker run -d --name sonarqube -p 9000:9000 sonarqube:community
# O si ya existe el contenedor:
docker start sonarqube
```

Espera a que el servidor de SonarQube esté en estado `UP`:
```bash
curl -s http://localhost:9000/api/system/status
```

---

### Paso 2: Generar Token de Autenticación (o usar credenciales default)
Para ejecutar el análisis desde la CLI de .NET, genera un token con la API de SonarQube:
```bash
curl -u admin:admin -X POST "http://localhost:9000/api/user_tokens/generate?name=sonarscanner_token"
```
*(Copia el valor del `token` generado en la respuesta JSON).*

---

### Paso 3: Ejecutar el Escaneo en 3 Etapas

Navega a la raíz del repositorio y ejecuta la secuencia estándar de `dotnet-sonarscanner`:

1. **Inicio del Escaneo (`begin`):**
   ```bash
   dotnet-sonarscanner begin \
     /k:"proyectoHospital" \
     /d:sonar.host.url="http://localhost:9000" \
     /d:sonar.token="TU_TOKEN_GENERADO"
   ```

2. **Compilación de la Solución (`build`):**
   ```bash
   dotnet build Login/Login.sln
   ```

3. **Finalización del Escaneo y Carga de Reportes (`end`):**
   ```bash
   dotnet-sonarscanner end /d:sonar.token="TU_TOKEN_GENERADO"
   ```

---

## 📊 Visualización de Resultados y Exportación

- **Dashboard Web de SonarQube:**  
  Abre tu navegador en: 👉 **http://localhost:9000/dashboard?id=proyectoHospital**  
  *(Usuario por defecto: `admin` / Contraseña: `admin`)*

- **Exportación de Incidencias a CSV:**  
  Puedes exportar la lista completa de hallazgos mediante la API REST de SonarQube a un archivo `.csv` ejecutando el siguiente comando en Python:
  ```bash
  python3 -c "
  import urllib.request, json, csv
  req = urllib.request.Request('http://localhost:9000/api/issues/search?componentKeys=proyectoHospital&ps=500')
  req.add_header('Authorization', 'Basic YWRtaW46YWRtaW4=')
  res = urllib.request.urlopen(req)
  data = json.loads(res.read().decode())
  with open('SQAP/sonarqube_report_inicial.csv', 'w', newline='', encoding='utf-8') as f:
      writer = csv.writer(f)
      writer.writerow(['Issue Key', 'Severity', 'Type', 'Component/File', 'Line', 'Rule', 'Message', 'Effort/Technical Debt'])
      for issue in data.get('issues', []):
          writer.writerow([issue.get('key'), issue.get('severity'), issue.get('type'), issue.get('component', '').split(':')[-1], issue.get('line', ''), issue.get('rule'), issue.get('message'), issue.get('effort', '')])
  "
  ```
