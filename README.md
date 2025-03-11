# VinfoPOL

 Se sigue una arquitectura limpia y modular aplicando varios patrones de diseño, en visual studio 2019 y .Net Core 3.1:

Mediator Pattern: Centraliza la comunicación entre comandos/queries y sus handlers.

Command Pattern: Encapsula acciones como objetos (PostCommand, FollowCommand).

Query Pattern (CQRS): Separa las operaciones de lectura (DashboardQuery).

Repository Pattern: Abstrae el acceso a datos (IUserRepository, InMemoryUserRepository).

Factory Pattern: Crea instancias de comandos a partir de la entrada del usuario.

Domain Events: Permite que las entidades del dominio notifiquen cambios (PostCreatedEvent).


También se está usando inyección de dependencias de forma manual. En nuestra solución, las dependencias se pasan a través de los constructores de cada clase 

(por ejemplo, en los handlers se inyecta el repositorio), sin usar un contenedor DI externo. 

No instale la libreria Microsoft.Extensions.DependencyInjection, porque el ejercicio decia de no usar ninguna libreria externa.

En caso de haber usado DI, mi program estaria de la siguiente manera:

 1. Crear la colección de servicios
    
      var services = new ServiceCollection()

2. Registrar dependencias:

   Registrar el repositorio como Singleton (única instancia durante la vida de la aplicación)
     services.AddSingleton<IUserRepository, InMemoryUserRepository>();

   Registrar los handlers (como Transient, ya que se crean cada vez que se solicitan)
      services.AddTransient<PostCommandHandler>();
      services.AddTransient<FollowCommandHandler>();
      services.AddTransient<DashboardQueryHandler>();

    Registrar el Mediator como Singleton para centralizar el registro de handlers
      services.AddSingleton<IMediator, Mediator>();

3. Construir el proveedor de servicios (ServiceProvider)
      var serviceProvider = services.BuildServiceProvider();

4. Inicializar el repositorio agregando usuarios
      var userRepository = serviceProvider.GetService<IUserRepository>();
      userRepository.AddUser(new User("@Alfonso"));
      userRepository.AddUser(new User("@Ivan"));
      userRepository.AddUser(new User("@Alicia"));

5. Obtener las instancias del Mediator y de los handlers
      var mediator = serviceProvider.GetService<IMediator>();
      var postHandler = serviceProvider.GetService<PostCommandHandler>();
      var followHandler = serviceProvider.GetService<FollowCommandHandler>();
      var dashboardHandler = serviceProvider.GetService<DashboardQueryHandler>();

   Registrar los handlers en el Mediator
      mediator.RegisterHandler<PostCommand>(postHandler);
      mediator.RegisterHandler<FollowCommand>(followHandler);
      mediator.RegisterHandler<DashboardQuery>(dashboardHandler);


Para una solucion mas avanzada con una base de datos tambien usaria Unit of work para agrupar varias operaciones dentro de una única transacción, asegurando que todas se completen o ninguna se ejecute.


Por último se han implementado pruebas unitarias para validar el correcto funcionamiento de la aplicación:

PostCommand_CreaPostCorrectamente: Verifica que un usuario pueda publicar un post y que se almacene correctamente.

FollowCommand_SigueCorrectamente: Comprueba que un usuario pueda seguir a otro y que la relación se refleje en el sistema.

DashboardQuery_RetornaPostsCorrectamente: Asegura que un usuario pueda ver los posts de los usuarios que sigue en orden cronológico.

