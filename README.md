
# 🏥 Hospital One - Sistema de Gestión Hospitalaria

> Proyecto Full Stack desarrollado para Coursera - Especialización en Desarrollo de Software

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat&logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%208.0-512BD4?style=flat)](https://docs.microsoft.com/en-us/ef/core/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## 📋 Descripción del Proyecto

**Hospital One** es una aplicación web full stack diseñada para modernizar y optimizar la gestión de citas médicas, consultorios y personal médico en instituciones hospitalarias. El sistema permite administrar de manera eficiente el flujo de pacientes, la asignación de doctores a consultorios, y el seguimiento completo del ciclo de vida de las citas médicas.

Este proyecto fue desarrollado como parte del programa de Coursera, aplicando las mejores prácticas de desarrollo de software, arquitectura limpia y patrones de diseño modernos.

## 🎯 Objetivos del Proyecto

- Demostrar competencia en desarrollo full stack con tecnologías .NET
- Implementar una arquitectura escalable y mantenible
- Aplicar principios SOLID y Clean Architecture
- Gestionar bases de datos relacionales complejas con Entity Framework Core
- Desarrollar APIs RESTful siguiendo estándares de la industria
- Implementar manejo de estados y validaciones robustas

## ✨ Características Principales

### 👥 Gestión de Pacientes (Clientes)
- ✅ Registro completo de pacientes con información demográfica
- ✅ Validación de documentos de identidad únicos
- ✅ Historial de citas por paciente
- ✅ Cálculo automático de edad
- ✅ Estados de activación/desactivación

### 👨‍⚕️ Gestión de Doctores
- ✅ Registro de médicos con especialidades
- ✅ Control de disponibilidad en tiempo real
- ✅ Estados: Disponible, No Disponible, En Consulta, En Ocio
- ✅ Asignación automática a consultorios
- ✅ Validación de licencias médicas únicas

### 🏢 Gestión de Consultorios
- ✅ Administración de salas por piso y edificio
- ✅ Estados: Disponible, No Disponible, Citas, Urgencias
- ✅ Asignación dinámica de doctores
- ✅ Seguimiento de ubicación y tipo de consultorio
- ✅ Control de disponibilidad en tiempo real

### 📅 Sistema de Citas
- ✅ Programación de citas regulares y de urgencia
- ✅ Estados completos: Programada, En Curso, Completada, Cancelada, No Asistió
- ✅ Cálculo automático de tiempos de espera
- ✅ Registro de diagnósticos y observaciones
- ✅ Duración real vs estimada de consultas
- ✅ Auditoría completa de cambios

### 🏥 Especialidades Médicas
- ✅ Catálogo de especialidades
- ✅ Tiempo promedio de consulta por especialidad
- ✅ Relación con doctores y citas

## 🏗️ Arquitectura del Sistema

El proyecto sigue los principios de **Clean Architecture** y está organizado en capas:

```
HospitalOne/
│
├── HospitalOne.Domain/              # Capa de Dominio
│   ├── Models/                      # Entidades del dominio
│   │   ├── Cliente.cs
│   │   ├── Doctor.cs
│   │   ├── Consultorio.cs
│   │   ├── Cita.cs
│   │   └── Especialidad.cs
│   ├── Enums/                       # Enumeraciones
│   │   └── Enums.cs
│   └── Interfaces/                  # Contratos del dominio
│
├── HospitalOne.Infrastructure/      # Capa de Infraestructura
│   ├── Data/                        # Contexto de base de datos
│   │   └── HospitalDbContext.cs
│   ├── Configurations/              # Configuraciones EF Core
│   │   └── EntityConfigurations.cs
│   ├── Repositories/                # Implementación de repositorios
│   └── Migrations/                  # Migraciones de base de datos
│
├── HospitalOne.Application/         # Capa de Aplicación
│   ├── DTOs/                        # Data Transfer Objects
│   ├── Services/                    # Lógica de negocio
│   ├── Interfaces/                  # Contratos de servicios
│   └── Validators/                  # Validaciones de negocio
│
├── HospitalOne.API/                 # Capa de Presentación (API)
│   ├── Controllers/                 # Controladores REST
│   ├── Middleware/                  # Middleware personalizado
│   ├── Filters/                     # Filtros de acción
│   └── Program.cs                   # Punto de entrada
│
└── HospitalOne.Tests/               # Pruebas
    ├── Unit/                        # Pruebas unitarias
    └── Integration/                 # Pruebas de integración
```

## 🛠️ Tecnologías Utilizadas

### Backend
- **Framework**: .NET 8.0 / ASP.NET Core
- **Lenguaje**: C# 12
- **ORM**: Entity Framework Core 8.0
- **Base de Datos**: SQL Server 2022
- **Arquitectura**: Clean Architecture, Repository Pattern, Unit of Work

### Patrones y Principios
- ✅ SOLID Principles
- ✅ Domain-Driven Design (DDD)
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ DTO Pattern
- ✅ Specification Pattern (opcional)

### Herramientas de Desarrollo
- Visual Studio 2022 / VS Code
- SQL Server Management Studio (SSMS)
- Postman / Swagger para testing de APIs
- Git & GitHub para control de versiones

## 📦 Instalación y Configuración

### Prerrequisitos

```bash
- .NET SDK 8.0 o superior
- SQL Server 2019 o superior
- Visual Studio 2022 o VS Code
- Git
```

### Paso 1: Clonar el Repositorio

```bash
git clone https://github.com/tu-usuario/hospital-one.git
cd hospital-one
```

### Paso 2: Restaurar Paquetes

```bash
dotnet restore
```

### Paso 3: Configurar Base de Datos

Edita `appsettings.json` en el proyecto API:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HospitalOne;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Paso 4: Crear Base de Datos

```bash
# Navega al proyecto Infrastructure
cd HospitalOne.Infrastructure

# Crea la migración inicial
dotnet ef migrations add InitialCreate --startup-project ../HospitalOne.API

# Aplica la migración
dotnet ef database update --startup-project ../HospitalOne.API
```

### Paso 5: Ejecutar el Proyecto

```bash
cd ../HospitalOne.API
dotnet run
```

La API estará disponible en: `https://localhost:5001` o `http://localhost:5000`

Swagger UI: `https://localhost:5001/swagger`

## 🔌 Endpoints de la API

### Clientes (Pacientes)
```
GET    /api/clientes              # Obtener todos los clientes
GET    /api/clientes/{id}         # Obtener cliente por ID
POST   /api/clientes              # Crear nuevo cliente
PUT    /api/clientes/{id}         # Actualizar cliente
DELETE /api/clientes/{id}         # Eliminar cliente (soft delete)
```

### Doctores
```
GET    /api/doctores              # Obtener todos los doctores
GET    /api/doctores/{id}         # Obtener doctor por ID
GET    /api/doctores/disponibles  # Obtener doctores disponibles
POST   /api/doctores              # Crear nuevo doctor
PUT    /api/doctores/{id}         # Actualizar doctor
PATCH  /api/doctores/{id}/estado  # Cambiar estado de disponibilidad
```

### Consultorios
```
GET    /api/consultorios              # Obtener todos los consultorios
GET    /api/consultorios/{id}         # Obtener consultorio por ID
GET    /api/consultorios/disponibles  # Obtener consultorios disponibles
POST   /api/consultorios              # Crear nuevo consultorio
PUT    /api/consultorios/{id}         # Actualizar consultorio
PATCH  /api/consultorios/{id}/asignar # Asignar doctor a consultorio
```

### Citas
```
GET    /api/citas                 # Obtener todas las citas
GET    /api/citas/{id}            # Obtener cita por ID
GET    /api/citas/pendientes      # Obtener citas pendientes
GET    /api/citas/hoy             # Obtener citas del día
POST   /api/citas                 # Programar nueva cita
PUT    /api/citas/{id}            # Actualizar cita
PATCH  /api/citas/{id}/iniciar    # Iniciar cita
PATCH  /api/citas/{id}/finalizar  # Finalizar cita
DELETE /api/citas/{id}            # Cancelar cita
```

### Especialidades
```
GET    /api/especialidades        # Obtener todas las especialidades
GET    /api/especialidades/{id}   # Obtener especialidad por ID
POST   /api/especialidades        # Crear nueva especialidad
PUT    /api/especialidades/{id}   # Actualizar especialidad
```

## 📊 Modelo de Datos

### Diagrama de Entidades Principales

```
┌─────────────────┐         ┌──────────────────┐         ┌─────────────────┐
│   Especialidad  │────────>│      Doctor      │────────>│   Consultorio   │
└─────────────────┘         └──────────────────┘         └─────────────────┘
                                     │                             │
                                     │                             │
                                     v                             v
                            ┌──────────────────┐         ┌─────────────────┐
                            │       Cita       │<────────│     Cliente     │
                            └──────────────────┘         └─────────────────┘
```

### Enumeraciones Clave

**EstadoDisponibilidad** (Doctor)
- Disponible
- NoDisponible
- EnConsulta
- EnOcio

**EstadoConsultorio**
- Disponible
- NoDisponible
- Citas
- Urgencias

**EstadoCita**
- Programada
- EnCurso
- Completada
- Cancelada
- NoAsistio

**TipoCita**
- Programada
- Urgencia

## 🧪 Testing

### Ejecutar Pruebas Unitarias

```bash
cd HospitalOne.Tests
dotnet test
```

### Ejecutar con Cobertura de Código

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## 📝 Buenas Prácticas Implementadas

### Código Limpio
- ✅ Nombres descriptivos y significativos
- ✅ Métodos pequeños y con responsabilidad única
- ✅ Comentarios XML para documentación
- ✅ Validaciones exhaustivas de entrada

### Arquitectura
- ✅ Separación de responsabilidades en capas
- ✅ Inyección de dependencias
- ✅ Inversión de control
- ✅ Bajo acoplamiento, alta cohesión

### Base de Datos
- ✅ Índices en columnas de búsqueda frecuente
- ✅ Restricciones de integridad referencial
- ✅ Validaciones a nivel de base de datos
- ✅ Campos de auditoría (fechas de creación, modificación)

### API
- ✅ Versionado de API
- ✅ Manejo centralizado de errores
- ✅ Validación de DTOs
- ✅ Documentación con Swagger/OpenAPI
- ✅ CORS configurado correctamente

## 🔐 Seguridad

- [ ] Autenticación JWT (implementación futura)
- [ ] Autorización basada en roles
- [ ] Encriptación de datos sensibles
- [ ] Validación de entrada contra SQL Injection
- [ ] Rate limiting para prevenir abuso
- [ ] HTTPS obligatorio en producción

## 🚀 Roadmap y Mejoras Futuras

### Fase 1 - Funcionalidad Core ✅
- [x] Modelo de dominio completo
- [x] Configuración de Entity Framework
- [x] Migraciones iniciales
- [x] Endpoints CRUD básicos

### Fase 2 - Lógica de Negocio (En Progreso)
- [ ] Servicios de aplicación
- [ ] Validaciones de negocio complejas
- [ ] Manejo de estados de citas
- [ ] Asignación automática de consultorios

### Fase 3 - Frontend
- [ ] Interfaz de usuario con React/Angular/Vue
- [ ] Dashboard de administración
- [ ] Panel para doctores
- [ ] Portal para pacientes

### Fase 4 - Funcionalidades Avanzadas
- [ ] Sistema de notificaciones (email/SMS)
- [ ] Recordatorios automáticos de citas
- [ ] Reportes y estadísticas
- [ ] Integración con servicios de pago
- [ ] Historia clínica electrónica
- [ ] Telemedicina

### Fase 5 - DevOps y Producción
- [ ] Containerización con Docker
- [ ] CI/CD con GitHub Actions
- [ ] Despliegue en Azure/AWS
- [ ] Monitoring y logging
- [ ] Backup automático de base de datos

## 📚 Recursos y Referencias

### Documentación Oficial
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core)
- [C# Language Reference](https://docs.microsoft.com/dotnet/csharp)

### Patrones y Arquitectura
- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/tags/domain%20driven%20design.html)
- [Repository Pattern](https://docs.microsoft.com/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

### Cursos Relacionados
- [Coursera - C# Programming](https://www.coursera.org/learn/csharp-programming)
- [Coursera - Web Development](https://www.coursera.org/specializations/full-stack-react)
- [Coursera - Database Design and Management](https://www.coursera.org/learn/database-design)

## 👥 Contribución

Este es un proyecto académico, pero las contribuciones son bienvenidas. Para contribuir:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👨‍💻 Autor

**Tu Nombre**
- GitHub: [@tu-usuario](https://github.com/RobertoRodriguezP)
- LinkedIn: [Tu Perfil](https://linkedin.com/in/robertorobertorodriguezp)
- Email: rroberto96@hotmail.com

## 🙏 Agradecimientos

- Coursera por proporcionar la plataforma de aprendizaje
- La comunidad de .NET por sus recursos y documentación
- Los instructores del curso por su guía y feedback
- Todos los que contribuyeron con ideas y sugerencias

---

⭐ **Si este proyecto te resultó útil, considera darle una estrella en GitHub!**

**Desarrollado con ❤️ como proyecto de Coursera**
