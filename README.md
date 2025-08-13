# API de Gestión de Citas Médicas

Esta es una API REST desarrollada en .NET C# con arquitectura limpia para gestionar un sistema de citas médicas. 
Permite manejar la información de pacientes, médicos, citas y otros datos relacionados, todo conectado a una base de datos MySQL.


## Tecnologías utilizadas

- .NET 6 / .NET Core  
- C#  
- MySQL  
- Entity Framework Core
- JWT para autenticación y autorización  

## Instalación

1. Clona este repositorio:  
   ```bash
   git clone https://github.com/tuusuario/tu-repo.git
2. Entra al directorio del proyecto:
   ```bash
   cd tu-repo

4. Configura la cadena de conexión a MySQL en el archivo appsettings.json o donde tengas configurada la conexión:
   ```bash
   "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=tu_basedatos;user=usuario;password=contraseña;"
   }

5. Restaura las dependencias y compila el proyecto:
   ```bash
   dotnet restore
   dotnet build

6. Ejecuta la API:
   ```bash
   dotnet run

## Uso:

### 1. Registro:
Primero, debes autenticarte enviando tus credenciales para obtener un token JWT.  
#### Ejemplo usando curl:  
```bash
curl -X POST https://tuapi.com/Api/Usuario \
  -H "Content-Type: application/json" \
  -d{
      "Nombre": "Nombre",
      "Apellido": "Apellido",
      "Email": "TuCorreo@gmail.com",
      "Password": "Contraseña",
      "RolFK": Id del tipo de usuario
}
```
#### Respuesta exitosa (200 OK):
```bash
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cC..."
}
```
#### Notas:  

- El `token` devuelto debe guardarse y usarse en el encabezado Authorization en sguientes solicitudes protegidas.
- Es importante que el email sea válido, ya que puede ser utilizado posteriormente para verificación o recuperación de cuenta.
- El RolFK es el id del tipo del rol que tendra ese usuario.


