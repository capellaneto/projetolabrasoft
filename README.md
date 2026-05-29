# Sistema de Gestão de Bolsas

## 📌 Sobre o Projeto

O **Sistema de Gestão de Bolsas** foi desenvolvido como projeto prático do programa de formação **LabraSoft**, voltado à capacitação de estudantes de Análise e Desenvolvimento de Sistemas (ADS) para o ambiente corporativo.

A aplicação simula o fluxo acadêmico-financeiro de uma fundação responsável pela intermediação de bolsas e financiamentos de pesquisa em universidades públicas. 

O sistema é construído progressivamente ao longo de oito semanas. **Atualmente, o projeto concluiu a Semana 3 (Visualização e Organização)**, consolidando o relacionamento entre múltiplos domínios de dados (Mestre-Detalhe), consultas avançadas com LINQ e persistência centralizada em memória unificada.

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
- Recursos Avançados de LINQ (`Where`, `OrderBy`, `FirstOrDefault`, `Contains`)
- .NET Framework / ASP.NET WebForms (Componentes: `TextBox`, `DropDownList`, `ListBox`, `Button`, `Label`, `GridView`, `Panel`, `Repeater`)

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

### 🌐 Interatividade WebForms & Estado (Semana 2)
- **A Ponte entre Tela e Objeto (Aula 1):** Desenvolvimento do formulário interativo `CadastroBolsista.aspx`. Captura de entradas textuais e instanciação sob demanda ao disparar eventos de clique.
- **Memória Volátil Multi-Objeto (Aula 2):** Implementação de uma coleção fortemente tipada (`List<Bolsista>`) com escopo global persistido através do modificador `static`, mantendo o histórico de cadastros vivo entre os ciclos de vida de PostBack. Integração com o controle `GridView` para renderização em tempo real dos dados acumulados, acompanhado de rotinas automáticas de limpeza de formulário (`LimparCampos`) e regras básicas de validação/segurança contra reenvio de dados.

### 📊 Visualização, Organização e Modelagem Relacional (Semana 3)
- **Ordenação, Filtros e Visibilidade Dinâmica (Aula 1):** Introdução à manipulação de coleções em tempo de renderização utilizando LINQ. Adição de rotinas de ordenação alfabética (`OrderBy`) e isolamento por gênero (`Where`) em botões especializados. Implementação do contêiner `asp:Panel` com gerenciamento de visibilidade atrelado ao `Count` da lista para exibição inteligente de controles de tela.
- **Centralização de Dados com Repositorio.cs (Aula 2):** Criação da classe global de armazenamento estático `Repositorio.cs` na pasta `Models`. Para otimizar o fluxo de desenvolvimento e testes de usabilidade, as listas internas do repositório já iniciam carregadas com dados fictícios pré-preenchidos, garantindo que o sistema inicie com campos e tabelas populados.
- **Composição e Relações entre Objetos (Aula 2):** Atualização do modelo conceitual. Criação das entidades `Coordenador.cs` e `Projeto.cs`. Aplicação do princípio de composição POO, onde a classe `Projeto` agora encapsula uma instância completa de `Coordenador` e uma coleção fortemente tipada `List<Bolsista>` representando os alunos vinculados.
- **Novas Telas de Cadastro e Busca (Aula 2):** Implementação de formulários para inclusão e filtragem avançada de coordenadores (`CadastroCoordenador.aspx`) permitindo pesquisas simultâneas combinadas por texto e titulação via LINQ, com validação de chaves exclusivas (CPF único). Criação da interface de vínculos de projeto (`CadastroProjeto.aspx`) usando `DropDownList` para seleção do coordenador e `ListBox` para múltipla seleção de bolsistas cadastrados no sistema.
- **Experiência do Usuário Mestre-Detalhe (Aula 2):** Implementação de listagem compacta com sub-detalhamento dinâmico. Ao acionar o botão "🔍 Detalhes" dentro do `GridView` principal de projetos, um painel secundário é ativado para processar e renderizar de forma isolada, via componente `asp:Repeater`, a relação restrita de alunos alocados especificamente àquela pesquisa científica.

-----

## 📁 Estrutura Atual do Projeto

```

labrasoft/
│
├── WebApplication1/
│   ├── Models/
│   │   ├── Bolsista.cs               # Definição, propriedades e métodos da classe Bolsista
│   │   ├── Coordenador.cs            # Entidade Coordenador com propriedades cadastrais
│   │   ├── Projeto.cs                # Classe Projeto estruturada com regras de composição POO
│   │   └── Repositorio.cs            # Persistência em memória unificada static com dados pré-preenchidos
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
│   ├── CadastroBolsista.aspx         # Formulário de bolsistas com botões de filtro e GridView
│   ├── CadastroBolsista.aspx.cs      # Lógica de controle, validações e expressões LINQ
│   ├── CadastroBolsista.aspx.designer.cs
│   │
│   ├── CadastroCoordenador.aspx      # Interface de cadastro e busca dinâmica de coordenadores
│   ├── CadastroCoordenador.aspx.cs   # Lógica de busca combinada (.Where, .Contains) de docentes
│   ├── CadastroCoordenador.aspx.designer.cs
│   │
│   ├── CadastroProjeto.aspx          # Tela de amarração relacional de projetos com Coordenador e Alunos
│   ├── CadastroProjeto.aspx.cs       # Lógica de carga dinâmica e vinculação mestre-detalhe (FirstOrDefault)
│   ├── CadastroProjeto.aspx.designer.cs
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
* **Aula 1 (A Ponte entre Tela e Objeto):** Substituição da entrada estática pela captura de dados dinâmica. Uso prático de web controls (`TextBox`, `DropDownList`, `Button`, `Label`). Instanciação sob demanda via manuseio de eventos de clique.
* **Aula 2 (Memória Volátil - A Lista Estática):** Utilização de coleções genéricas (`List<T>`) qualificadas com a palavra-chave `static` para preservar o estado de dados concorrentes durante o PostBack. Alimentação de tabelas dinâmicas utilizando `DataSource` e `DataBind()` acoplados ao componente `GridView`.

### 🗓 Semana 3: Visualização e Organização
* **Aula 1 (Exibição com GridView):** Introdução à manipulação lógica de coleções através de expressões LINQ. Uso prático dos métodos extensionistas `Where()` para filtragens e `OrderBy()` para classificações alfabéticas antes do bind visual. Condicionamento da interface de usuário chaveando elementos estruturais (`asp:Panel`) baseados em lógica condicional de contagem.
* **Aula 2 (Replicação e Modelagem Relacional):** Criação e interconexão de múltiplas entidades utilizando Composição Orientada a Objetos. Centralização de dados arquiteturais na classe `Repositorio.cs` configurada com sementes de mock (dados pré-preenchidos) para viabilizar cargas automáticas em seletores (`DropDownList` e `ListBox`). Acoplamento de tabelas hierárquicas usando o padrão UX Mestre-Detalhe acionado por comandos em linha de GridView e renderizado via `asp:Repeater`.
* **Estado Atual:** O sistema gerencia o ciclo completo de um ecossistema acadêmico elementar. É possível cadastrar alunos, professores e projetos de forma independente, cruzar suas referências diretamente pela interface Web e detalhar os vínculos dinamicamente na tela sem dependência imediata de um banco de dados persistente.

-----

## 🚀 Competências Desenvolvidas nesta Etapa

- Domínio prático de relações estruturais entre objetos no ecossistema C# (Composição e Listas Agregadas);
- Centralização de estado e arquitetura Mock usando repositórios estáticos pré-populados;
- Integração avançada de controles de seleção múltipla (`asp:ListBox`) e repetidores estruturados de HTML (`asp:Repeater`);
- Utilização avançada de operadores LINQ de filtragem de texto, cruzamento de referências por id/chave e seleção de primeiro registro;
- Desenvolvimento de interfaces com padrões de navegação corporativa e detalhamento sob demanda (Master-Detail UX).

-----

## ▶ Como Executar o Projeto Atual

### Pré-requisitos
- Visual Studio (com a carga de trabalho para desenvolvimento Web ASP.NET e .NET Framework instalada)

### Passos
1. Clone o repositório da formação:
   git clone [https://gitlab.com/labrasoft.ifba/labrasoft.git](https://gitlab.com/labrasoft.ifba/labrasoft.git)

2. Abra o arquivo `WebApplication1.sln` no seu Visual Studio;
3. No Gerenciador de Soluções, clique com o botão direito sobre a nova página relacional `CadastroProjeto.aspx` (ou nas demais de cadastro) e selecione **Definir como Página Inicial**;
4. Clique no botão de execução (IIS Express) na barra superior para simular o ecossistema relacional completo alimentado pelas coleções pré-preenchidas.

-----

## 👨‍💻 Equipe

Projeto desenvolvido durante o programa de formação LabraSoft por estudantes do curso de Análise e Desenvolvimento de Sistemas (ADS).