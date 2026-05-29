# Sistema de Gestão de Bolsas

## 📌 Sobre o Projeto

O **Sistema de Gestão de Bolsas** foi desenvolvido como projeto prático do programa de formação **LabraSoft**, voltado à capacitação de estudantes de Análise e Desenvolvimento de Sistemas (ADS) para o ambiente corporativo.

A aplicação simula o fluxo acadêmico-financeiro de uma fundação responsável pela intermediação de bolsas e financiamentos de pesquisa em universidades públicas. 

O sistema é construído progressivamente ao longo de oito semanas. **Atualmente, o projeto avançou para a Semana 2 (O Formulário Interativo)**, focada no desenvolvimento de interfaces interativas e na captura de dados inseridos pelo usuário.

-----

## 🎯 Objetivos do Projeto

- Aplicar conceitos de Programação Orientada a Objetos em C#;
- Desenvolver aplicações Web com ASP.NET WebForms;
- Implementar persistência de dados utilizando SQL Server;
- Trabalhar autenticação e segurança de aplicações;
- Utilizar padrões arquiteturais e boas práticas de desenvolvimento;
- Simular um ambiente corporativo real de desenvolvimento.

-----

## 🛠 Tecnologias Utilizadas (Estágio Atual)

### Frontend & Visual
- HTML5 / CSS3
- Bootstrap 5.3 (Integrado globalmente via CDN no `Site.Master`)

### Backend
- C#
- .NET Framework / ASP.NET WebForms (Controles de Servidor: `TextBox`, `DropDownList`, `Button`, `Label`)

### Ferramentas
- Visual Studio / VS Code
- Git / GitLab

> 🚀 **Nota de Evolução:** Ao longo das próximas semanas, o projeto integrará *SQL Server*, *ADO.NET*, *BCrypt.Net-Next*, *JWT (JSON Web Token)* e *Gmail API*.

-----

## 📚 Funcionalidades Implementadas

### 👨‍🏫 Gestão Acadêmica & POO (Semana 1)
- **A Planta Baixa (Classes e Propriedades):** Criação da classe base `Bolsista.cs` dentro da pasta `Models`, definindo a estrutura de dados (molde).
- **A Inteligência do Objeto (Métodos e Instância):** Implementação do método de lógica interna `CalcularIdade()` e instanciação dinâmica usando o operador `new` no evento `Page_Load`.
- **Interface Visual & Master Page:** Integração do layout acoplado à página mestre (`Site.Master`) utilizando Bootstrap 5.

### 🌐 Interatividade WebForms (Semana 2)
- **A Ponte entre Tela e Objeto (Aula 1):** Desenvolvimento de um formulário interativo em ambiente WebForms (`CadastroBolsista.aspx`). Captura das entradas textuais e seleções do usuário, vinculando-as dinamicamente a uma nova instância da classe `Bolsista` no evento de clique do botão, com retorno visual imediato em tela através de `Label`.

-----

## 📁 Estrutura Atual do Projeto

```

labrasoft/
│
├── WebApplication1/
│   ├── Models/
│   │   └── Bolsista.cs               # Definição, propriedades e métodos da classe Bolsista
│   │
│   ├── Properties/
│   │   └── AssemblyInfo.cs
│   │
│   ├── Web/
│   │   └── Formulario.html           # Interface web estática legada de testes estruturais
│   │
│   ├── BolsistaExemplo.aspx          # Página inicial de testes de instanciação
│   ├── BolsistaExemplo.aspx.cs       # Code-behind (C#) da página de testes
│   ├── BolsistaExemplo.aspx.designer.cs
│   │
│   ├── CadastroBolsista.aspx         # Formulário interativo de cadastro com controles ASP.NET
│   ├── CadastroBolsista.aspx.cs      # Lógica de captura de dados (Code-behind) da Semana 2
│   ├── CadastroBolsista.aspx.designer.cs
│   │
│   ├── Site.Master                   # Página Mestre global com a integração do Bootstrap 5
│   ├── Site.Master.cs
│   ├── Site.Master.designer.cs
│   │
│   ├── packages.config
│   ├── Web.config
│   ├── Web.Debug.config
│   └── Web.Release.config
│
├── WebApplication1.sln               # Arquivo de solução do Visual Studio
├── .gitignore                        # Filtro de arquivos locais e binários para o Git
└── README.md                         # Documentação do projeto

```

## 📈 Cronograma de Aprendizado

### 🗓 Semana 1: O Conceito de Objeto (POO)
* **Aula 1 (A Planta Baixa):** Criação do projeto, mapeamento da pasta `Models`, propriedades (`get; set;`) e tipagem básica.
* **Aula 2 (A Inteligência do Objeto):** Manipulação de escopo, retorno de métodos (`CalcularIdade`), ciclo de vida (`Page_Load`) e instanciação fixa controlada pelo código.

### 🗓 Semana 2: O Formulário Interativo
* **Aula 1 (A Ponte entre Tela e Objeto):** Substituição da entrada estática pela captura de dados dinâmica. Uso prático de web controls (`TextBox`, `DropDownList`, `Button`, `Label`). Instanciação sob demanda via manipulação de eventos de clique.
* **Estado Atual:** O sistema recebe dados reais do usuário, processa temporariamente na memória e renderiza uma resposta personalizada. (Nota: os dados são voláteis e duram apenas o ciclo de vida da requisição atual).

-----

## 🚀 Competências Desenvolvidas nesta Etapa

- Manipulação de Web Controls nativos do ASP.NET (`asp:TextBox`, `asp:Button`, etc.);
- Programação orientada a eventos em arquitetura Web;
- Leitura e conversão de dados de formulários baseados em texto para propriedades fortemente tipadas do C#;
- Exibição de retornos dinâmicos para o usuário com persistência em memória volátil de requisição.

-----

## ▶ Como Executar o Projeto Atual

### Pré-requisitos
- Visual Studio (com a carga de trabalho para desenvolvimento Web ASP.NET e .NET Framework instalada)

### Passos
1. Clone o repositório da formação:
   git clone [https://gitlab.com/labrasoft.ifba/labrasoft.git](https://gitlab.com/labrasoft.ifba/labrasoft.git)

2. Abra o arquivo `WebApplication1.sln` no seu Visual Studio;
3. No Gerenciador de Soluções, clique com o botão direito sobre a nova página `CadastroBolsista.aspx` e selecione **Definir como Página Inicial**;
4. Clique no botão de execução (IIS Express) na barra superior para iniciar o teste de interatividade.

-----

## 👨‍💻 Equipe

Projeto desenvolvido durante o programa de formação LabraSoft por estudantes do curso de Análise e Desenvolvimento de Sistemas (ADS).