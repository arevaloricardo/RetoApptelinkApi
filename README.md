# RetoApptelink

## Elección de Tecnología

Para este proyecto, he decidido utilizar **.NET Core** para el desarrollo del **Web API**. Aunque al principio no tenía conocimientos profundos sobre .NET Core, me sumergí en la investigación y logré comprender cómo funciona el lenguaje y su flujo. Además, también exploré **SQL Server** y actualmente estoy probando la versión **SQL Express**. La parte del API está finalizada y desarrollada con **.NET Core 8**.

## Web API del Reto

He completado la implementación del **Web API** siguiendo los requerimientos establecidos. Los endpoints disponibles son los siguientes:

- `/api/Customer`
- `/api/Invoice`
- `/api/Product`
- `/api/InvoiceProduct`
- `/api/User`

## Funcionalidades del API

En cada uno de los endpoints, puedes realizar operaciones mágicas utilizando los métodos HTTP estándar: **GET**, **PUT**, **POST** y **DELETE**. Además, he agregado algunas funcionalidades adicionales:

### Filtrado de Datos

Los controladores pueden filtrar datos específicos utilizando los siguientes parámetros:

- `filterByProperty`: La columna por la cual aplicar el filtro.
- `filterValue`: El valor del filtro.

### Ordenamiento

Puedes ordenar los datos utilizando:

- `orderByProperty`: La columna por la cual ordenar.
- `sortOrder`: Ascendente (`asc`) o descendente (`desc`).

### Paginación

Para manejar grandes conjuntos de datos, hemos implementado paginación:

- `pageIndex`: Número de página.
- `pageSize`: Cantidad de datos por página.

## Autenticación y Seguridad

En el controlador de **User**, he creado un método llamado **Login** que permite a los usuarios acceder a la aplicación. Si algo sale mal durante el inicio de sesión, se les enviará un mensaje de error; si todo está bien, recibirán un "OK". Además, he implementado un bloqueo de usuario después de tres intentos fallidos de inicio de sesión.

## Pruebas y Validación

Actualmente, estoy probando la aplicación utilizando valores de prueba en **Swagger**, y hasta ahora todo funciona sin problemas.

¡Espero que esta descripción te ayude a tener un README completo y claro para tu repositorio! Si tienes alguna otra pregunta o necesitas más detalles.
