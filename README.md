# Gerenciador de eventos BLUE RIDGE

Projeto simulador de uma api para gerenciamento de estagiários.

### Tecnologias utilizadas
 
- .NET 5.0
- ASP.NET Core
- EF Core 5.0
- MySql 8.0

## Configurações iniciais

### Senha de usuário do banco de dados

Na camada de Host alterar o  arquivo *appsettings.json* -> Propriedade *"ConnectionStrings":*

### Dados cadastrados

**Administrador**

- Username: Admin
| Password: Gft@1234

**Usuários**

- Username: User
| Password: Gft@1234

## Funcionalidades

### Permissões

Usuários anônimos podem:

- Fazer login;
- Criar conta.

Usuários com a permissão de admin podem:

- Ver, alterar e deletar outras contas;
- Ver, Cadastrar, alterar e deletar categorias;
- Ver, Cadastrar, alterar e deletar starters;
- Fazer o upload de foto para um starter.

Usuários com a permissão de admin e user podem:

- Ver starter e sua foto;
- Listar starters por ordem crescente pelo nome;
- Listar starters por ordem decrescente pelo nome.

### Validações

- Conta de administrador não pode ser consultada, alterada ou deletada;
- Categorias não podem ser deletadas se houverem starters cadastrados;
- Categorias não podem ser desativadas se houverem starters ativos cadastrados;
- CPFs são validados;
- Abreviações de starters devem conter 4 letras;

### Email

Ao fazer login, é enviado um email para a conta confirmando o acesso. 

## Considerações

O projeto proporcionou o avanço dos conhecimentos na tecnologia ASP.NET, construção de APIs RESTful, arquitetura e *Design Patterns*.
