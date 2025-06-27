##### EM CONSTRUÇÃO

# Integrantes
- Erick da Silva Pereira Pinto - 06007298
- Felipe Braga Gomes - 06008527
- Gabriel Nunes Mazzaro Lopes - 06008527
- Lucas Cardia Quintan Valle - 06007104

# Apresentação
Link pro vídeo no YouTube: https://youtu.be/Oxq0TKhelkQ

# Aplicação de padrões de projeto
Utilizamos o Repository Pattern afim de adicionar uma camada a mais de abstração entre os serviços e os dados do banco.  Na nossa implementação, utilizamos um Repositório Genérico para métodos que fossem gerais a todas as entidades, enquanto que, para metodos especificos, este foram implementados nos repositórios que herdaram do genérico.


# Princípios SOLID em prática 
FALTA



# Convenções de nomenclatura claras 
Para este projeto, decidimos usar o inglês na maior parte do tempo, sempre respeitando as convençoes e demonstrando clareza. O trecho abaixo, presente entre as linhas ??-?? do arquivo ???? demonstra isso:
    print(f)
[inserir justificativas]
# ** Documentação mínima de código  **
Não optamos muito pela utilização de comentários, mas no nosso *program.cs* foi dividido de modo que cada comando semelhante fosse agrupado sob o comentário referente aonde tal grupo de linhas 

** Testes automatizados ** 
Infelizmente não tivemos tempo de planejar e realizar os testes :(

**Refatorações evidentes **
A refatoração mais siginificativa que fizemos foi na questão dos UseCases/Services. Já que na primeira entrega, recebemos o feedback que o uso de multiplos arquivos com casos de uso indiviudais não era a melhor abordagem, por isso criamos classes de service separadas por entidades.

8 - Tratamento de erros e exceções  
Uso consistente de blocos try/catch e telas de erro padronizadas, evidenciando a preocupação com confiabilidade e  **segurança**.  **Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação**.

9 - Exemplos de validação de entrada  
Métodos ou filtros que checam parâmetros e garantem que dados inválidos não sejam processados, ou parâmetros adicionados a queries, evitando vulnerabilidades como SQL Injection ou XSS.  **Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação**.

10 - Heurísticas de usabilidade no frontend  
Observação de CSS e componentes que seguem princípios de consistência e feedback visual, facilitando a experiência do usuário.  **Deve ser indicado no Readme.md o nome do código fonte e os números das linhas onde se encontra a implementação**.
