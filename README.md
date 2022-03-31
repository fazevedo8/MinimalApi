# MinimalApi
Estudo sobre minimal's api no .net 6.0

Minimals apis

- Forma alternativa
- não substitui a forma tradicional
- Não são indicadas para todos os tipos de projetos

- proposito de microsserviços com poucos recursos


	WebApplication.CreateBuilder(args); ==> Objetivo configurar a api de maneira simples
	https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication?view=aspnetcore-6.0
	
//Equivalente ao metodo configure services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

WebApplication --> unifica os recursos de diversas interfaces como: IHost etc...

Notem que nosso projeto não tem using, pois estamos utilizando o recurso de Golbal Using


Incluindo dependencias:
	- EF Core
	- EF Core Design


(dica: se for utilizar o ORM Entity Framework, precisara ter instalado em sua paquina
a ferra dotnet-ef: dotnet tool install --global dotnet-ef --version 3.0.0)


Recursos 	.net 6 novos: Top Level Statement 
      	Global Using
		Map[verb]

- Lambidas:
	Atributos decorados, parametros com atributos, tipo de retorno antes da lista de paramentros
	ter tipos delegate



EndPointRouteBuilderExtensions: MapGet, MapPost, MapDelete

