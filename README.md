# Descrição do projeto
O projeto consiste em uma agenda que possibilita o usuário adicionar e remover seus compromissos, podendo agrupá-los por categorias e visualizá-los em um calendário interativo.

# Para testar nossa Agenda
- **Ambiente de desenvolvimento:**
	-  rode o comando ``dotnet ef database update --project src/AgendaSerial3.Infrastructure --startup-project src/AgendaSerial3.WebAPI`` na raiz da solução
	-  no Visual Studio, foi configurado um perfil de inicialização compartilhável que roda o projeto da API e do Frontend ao mesmo tempo. Caso não consiga acessá-lo, abra dois terminais e rode os projetos (AgendaSerial3.WebAPI e AgendaSerial3.BlazorWasm) em cada instância
- **URL do projeto publicado no Azure:** https://happy-desert-060f7ec0f.2.azurestaticapps.net/

# Integrantes
- Erick da Silva Pereira Pinto - 06007298
- Felipe Braga Gomes - 06008527
- Gabriel Nunes Mazzaro Lopes - 06008527
- Lucas Cardia Quintan Valle - 06007104

# Apresentação
Link pro vídeo no YouTube: https://youtu.be/Oxq0TKhelkQ

# Aplicação de padrões de projeto
Utilizamos o Repository Pattern afim de adicionar uma camada a mais de abstração entre os serviços e os dados do banco.  Na nossa implementação, utilizamos um Repositório Genérico para métodos que fossem gerais a todas as entidades, enquanto que, para metodos especificos, este foram implementados nos repositórios que herdaram do genérico. A implementação se encontra inteiramente nos arquivos GenericRepository, *AppointmentRepository.cs, CategoryRepository.cs* e *AuthRepository.cs* (a implementar). 


# Princípios SOLID em prática 
**Open/Closed Principle**

1. GenericRepository.cs
- Arquivo: `src/AgendaSerial3.Infrastructure/Data/Repository/GenericRepository.cs`
- Linhas: 1–55

2. CategoryRepository.cs
- Arquivo: `src/AgendaSerial3.Infrastructure/Data/Repository/CategoryRepository.cs`
- Linhas: 1–30
 
O princípio Open/Closed é aplicado pelo repositório genérico `GenericRepository<T>`, que disponibiliza operações CRUD que podem ser utilizadas porqualquer entidade do aplicativo. Esta classe está **fechada para alterações**, pois não requer alterações para novos comportamentos, mas está **disponível para extensão**, já que outras classes, como `CategoryRepository`, podem herdá-la e implementar comportamentos específicos ou sobrescrever métodos conforme a necessidade. No exemplo, `CategoryRepository` amplia `GenericRepository<Category>`, alterando a funcionalidade de DeleteAsync atrelao à entidade em questão mas mantendo o comportamento padrão pra outras entidades.

# Convenções de nomenclatura claras 
Para este projeto, decidimos usar o inglês na maior parte do tempo, sempre respeitando as convençoes do C# e demonstrando clareza. O trecho abaixo, presente entre as linhas 9-19 do arquivo *AppointmentRepository* demonstra isso:
```csharp
public async Task<IEnumerable<Appointment>> GetAppointmentsAndCategoriesByUserIdAndDatesAsync(string userId, DateTime startDate, DateTime endDate)
    {
        var appointments = await _context.Appointments
            .Include(a => a.Category)
            .Where(a => a.UserId == userId &&
                       a.StartDateTime >= startDate &&
                       a.StartDateTime <= endDate)
            .ToListAsync();

        return appointments;
    }
```
Neste Trecho vemos que o nome do método está bem descritivo e que os outros itens seguem a conveção de Camel Case ou Pascal Case. 

# Documentação mínima de código 
Não optamos muito pela utilização de comentários, mas no nosso *program.cs* foi dividido de modo que cada comando semelhante fosse agrupado sob o comentário referente aonde tal grupo de linhas 
```csharp
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins("https://localhost:7001", "http://localhost:5001")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Repository e Services
builder.Services.AddScoped<AppointmentRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AppointmentService>();

var app = builder.Build();

// Roddar o swagger apenas no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

# Testes automatizados 
Infelizmente não tivemos tempo de planejar e realizar os testes :(

# Refatorações evidentes
A refatoração mais siginificativa que fizemos foi na questão dos UseCases/Services. Já que na primeira entrega, recebemos o feedback que o uso de multiplos arquivos com casos de uso indiviudais não era a melhor abordagem, por isso criamos classes de service separadas por entidades.

# Tratamento de erros e exceções  
No nosso projeto, os blocos try/catch foram bastante utilizados nos Controllers. Segue Exemplo das linhas 27-52 do arquivo *AppointmentController.cs*
```csharp
[HttpPost]
public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    if (appointmentDto.StartDateTime >= appointmentDto.EndDateTime)
        return BadRequest("Data/hora de início deve ser anterior à data/hora de fim");

    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userId))
        return Unauthorized();

    try
    {
        var appointment = await _appointmentService.CreateAppointmentAsync(appointmentDto, userId);
        return CreatedAtAction(nameof(GetAppointments), new { id = appointment.Id }, appointment);
    }
    catch (UnauthorizedAccessException ex)
    {
        return BadRequest(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return BadRequest(new { message = ex.Message });
    }
```

# Exemplos de validação de entrada  
Arquivo *LoginDto.cs*, linhas 5-23
```csharp
public class RegisterDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Sobrenome é obrigatório")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
    [Compare("Password", ErrorMessage = "Senhas não coincidem")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
```

# Heurísticas de usabilidade no frontend  
```csharp
<div class="relative h-16 border-b border-r border-gray-200 cursor-pointer transition-colors duration-200 @(isToday ? "bg-blue-50 hover:bg-blue-100" : "hover:bg-gray-100")"
                             @onclick="() => OnCellClick.InvokeAsync(cellDateTime)"
                             title="Adicionar compromisso às @hour:00">
```

**Feedback ao Usuário:** Ao passar o mouse sobre as células do calendário, elas mudam de cor (hover:bg-blue-100, hover:bg-gray-100), sinalizando que podem ser selecionadas. O @onclick="() => OnCellClick.InvokeAsync(cellDateTime)" indica uma resposta imediata visual e funcional.
