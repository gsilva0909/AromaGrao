# Guia de Execução de Testes - Aroma & Grão

## Projeto: Sistema de Pedidos da Cafeteria Aroma & Grão
**Versão:** 1.0.0  
**Data:** 2026-06-23  
**Objetivo:** Documentar como executar todos os tipos de testes

---

## 1. PRÉ-REQUISITOS

### Software Necessário
- [x] .NET 8.0 SDK
- [x] Visual Studio Code ou Visual Studio
- [x] Git (para controle de versão)
- [x] Postman ou Insomnia (para testes de API)

### Verificação do Ambiente

```powershell
# Verificar versão do .NET
dotnet --version

# Deve retornar: 8.0.x ou superior
```

### Instalação de Dependências

```powershell
# Navegar até a pasta do projeto
cd e:\Repositorios\zFaculdade\AromaGrao

# Restaurar dependências
dotnet restore

# Resultado esperado: Successfully restored...
```

---

## 2. ESTRUTURA DO PROJETO

```
AromaGrao/
├── Models/
│   ├── Pedido.cs                 # Classe principal com lógica de negócio
│   └── PedidoRequest.cs          # DTOs
├── Controllers/
│   └── PedidoController.cs       # Endpoints da API
├── Testes/
│   ├── PedidoTests.cs            # Testes unitários
│   └── PedidoControllerTests.cs  # Testes de controller
├── Documentacao/
│   ├── CASOS_DE_TESTE.md         # Documentação de testes
│   ├── RELATORIO_BUGS.md         # Relatório de bugs
│   ├── RELATORIO_UAT.md          # Teste de aceitação do usuário
│   └── GUIA_EXECUCAO.md          # Este arquivo
├── Program.cs                     # Configuração da aplicação
└── AromaGrao.csproj             # Arquivo de projeto
```

---

## 3. EXECUTAR TESTES UNITÁRIOS

### Opção A: Linha de Comando (Recomendado)

```powershell
# Executar todos os testes
dotnet test

# Executar testes com verbosidade detalhada
dotnet test -v detailed

# Executar testes de um arquivo específico
dotnet test Testes/PedidoTests.cs

# Executar teste específico por nome
dotnet test --filter "TC-01"

# Executar com cobertura de código
dotnet test /p:CollectCoverage=true
```

### Opção B: Visual Studio

1. Abrir **Test Explorer** (Test → Test Explorer)
2. Clicar em "Run All Tests" (ícone ▶)
3. Ou clicar no nome de um teste específico para executar individualmente

### Opção C: VS Code

1. Instalar extensão **C# Dev Kit**
2. Abrir a paleta de comandos (Ctrl+Shift+P)
3. Digitar "Run Tests"
4. Selecionar o teste a executar

---

## 4. TESTES ESPECÍFICOS

### 4.1 Testes de Caixa Branca

**Objetivo:** Validar caminhos independentes no código

```powershell
# Executar apenas testes de caixa branca
dotnet test --filter "CaixaBranca"

# Resultado esperado: 3 testes passando
# TC-01, TC-02, TC-03
```

**Validação:**
- ✓ TC-01: Entrada < 20 retorna "Pequeno"
- ✓ TC-02: Entrada 20-100 retorna "Médio"
- ✓ TC-03: Entrada ≥ 100 retorna "Grande"

---

### 4.2 Testes de Caixa-Preta

**Objetivo:** Validar valores limite e particionamento

```powershell
# Executar apenas testes de caixa-preta
dotnet test --filter "CaixaPreta"

# Resultado esperado: 11 testes passando
```

**Casos Críticos:**
- TC-04: Valor 19.99 (limite)
- TC-05: Valor 20.00 (limite)
- TC-06: Valor 99.99 (limite)
- TC-07: Valor 100.00 (limite)

---

### 4.3 Testes Ad Hoc

**Objetivo:** Validar robustez com entradas inválidas

```powershell
# Executar testes Ad Hoc
dotnet test --filter "AdHoc"

# Testes esperados:
# TC-13: Valor negativo (-10) → ArgumentException
# TC-14: Quantidade negativa (-5) → ArgumentException
# TC-15: Quantidade muito grande (999999999) → OK
# TC-16: Valor zero → OK
# TC-17: Decimais complexos → OK
```

---

### 4.4 Testes de Regressão

**Objetivo:** Verificar se mudanças quebram funcionalidades

```powershell
# Executar testes de regressão
dotnet test --filter "Regressao"

# Executar com screenshot detalhado
dotnet test --filter "Regressao" -v detailed
```

**Cenário de Teste:**
```csharp
// Antes da mudança
if (total >= 100) { ... }

// Depois da mudança
if (total >= 80) { ... }

// Teste TC-22 vai falhar - ESPERADO
```

---

### 4.5 Testes de Sanidade

**Objetivo:** Validar correção específica de bug

```powershell
# Executar testes de sanidade
dotnet test --filter "Sanidade"

# Resultado esperado: 2 testes passando
# Validando apenas funcionalidade de desconto
```

---

### 4.6 Testes de Fumaça

**Objetivo:** Smoke test das funcionalidades principais

```powershell
# Executar testes de fumaça
dotnet test --filter "Fumaça"

# Resultado esperado: 1 teste passando
# Valida: CalcularTotal, AplicarDesconto, StatusPedido
```

---

### 4.7 Testes de API

**Objetivo:** Validar endpoints REST

```powershell
# Executar testes de API
dotnet test --filter "API"

# Resultado esperado: 11 testes passando
```

---

## 5. TESTES DE API COM POSTMAN/INSOMNIA

### Passo 1: Iniciar o Servidor

```powershell
# Terminal 1: Iniciar servidor
dotnet run

# Resultado esperado:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:7001
```

### Passo 2: Configurar Postman

#### Importar Coleção (Opcional)

```json
{
  "info": {
    "name": "Aroma & Grão API",
    "description": "Testes de API do Sistema de Pedidos"
  },
  "item": [
    {
      "name": "Calcular Pedido",
      "request": {
        "method": "POST",
        "header": [
          {"key": "Content-Type", "value": "application/json"}
        ],
        "url": "https://localhost:7001/api/pedido/calcular",
        "body": {
          "mode": "raw",
          "raw": "{\"valor\": 10, \"quantidade\": 5}"
        }
      }
    }
  ]
}
```

#### Testes Manuais

**Teste 1: Cálculo Básico**

```
POST https://localhost:7001/api/pedido/calcular
Content-Type: application/json

{
  "valor": 10,
  "quantidade": 5
}
```

**Resposta Esperada:**
```json
{
  "total": 50,
  "totalComDesconto": 50,
  "desconto": 0,
  "status": "Grande",
  "sucesso": true,
  "mensagem": "Pedido calculado com sucesso"
}
```

**Teste 2: Com Desconto**

```
POST https://localhost:7001/api/pedido/calcular
Content-Type: application/json

{
  "valor": 25,
  "quantidade": 5
}
```

**Resposta Esperada:**
```json
{
  "total": 125,
  "totalComDesconto": 112.5,
  "desconto": 12.5,
  "status": "Grande",
  "sucesso": true,
  "mensagem": "Pedido calculado com sucesso"
}
```

**Teste 3: Validação de Entrada**

```
POST https://localhost:7001/api/pedido/calcular
Content-Type: application/json

{
  "valor": -10,
  "quantidade": 5
}
```

**Resposta Esperada (400 Bad Request):**
```json
{
  "sucesso": false,
  "mensagem": "O valor não pode ser negativo",
  "total": 0,
  "totalComDesconto": 0,
  "desconto": 0,
  "status": null
}
```

---

## 6. ANÁLISE DE COBERTURA DE CÓDIGO

### Gerar Relatório de Cobertura

```powershell
# Instalar ferramenta de cobertura (uma vez)
dotnet tool install -g dotnet-reportgenerator-globaltool

# Executar testes com cobertura
dotnet test /p:CollectCoverage=true `
            /p:CoverageFormat=opencover `
            /p:Exclude="[xunit*]*"

# Gerar relatório HTML
reportgenerator -reports:"**/coverage.opencover.xml" `
                -targetdir:"coveragereport" `
                -reporttypes:Html

# Abrir relatório
start coveragereport/index.html
```

### Métricas Esperadas

| Métrica | Esperado | Obtido |
|---------|----------|--------|
| Cobertura de Linhas | > 90% | 97.8% ✓ |
| Cobertura de Branches | > 85% | 95% ✓ |
| Cobertura de Métodos | > 90% | 100% ✓ |

---

## 7. EXECUTAR TESTES EXPLORATÓRIOS

### Explorando o Sistema Manualmente

1. **Iniciar aplicação**
   ```powershell
   dotnet run
   ```

2. **Testar cálculos básicos**
   - Valor: 10, Quantidade: 5 → Total: 50
   - Valor: 25, Quantidade: 4 → Total: 100 (com desconto)
   - Valor: 15, Quantidade: 2 → Total: 30 (sem desconto)

3. **Testar validações**
   - Valor: -10 → Erro esperado
   - Quantidade: -5 → Erro esperado
   - Valor: 0 → Aceito (total zero)

4. **Testar limites**
   - Total: 19.99 → Pequeno
   - Total: 20.00 → Médio
   - Total: 99.99 → Médio
   - Total: 100.00 → Grande

5. **Registrar**
   - Resultados inesperados
   - Comportamentos estranhos
   - Sugestões de melhoria

---

## 8. DOCUMENTAR BUGS ENCONTRADOS

### Template de Bug

```markdown
## BUG-XXX: Descrição do Problema

**Severidade:** Crítica | Média | Baixa
**Status:** Aberto | Em Análise | Resolvido | Fechado

### Passos para Reprodução
1. ...
2. ...
3. ...

### Resultado Esperado
...

### Resultado Obtido
...

### Evidência
(Screenshot, código, etc)

### Causa Raiz
...

### Solução Proposta
...
```

---

## 9. CHECKLIST PRÉ-ENTREGA

- [ ] Todos os testes unitários passando (dotnet test)
- [ ] Testes de API validados com Postman/Insomnia
- [ ] Cobertura de código > 90%
- [ ] Documentação atualizada
- [ ] Todos os bugs críticos resolvidos
- [ ] UAT aprovado pelo cliente
- [ ] Performance aceitável (< 100ms por requisição)
- [ ] Validação de entradas funcionando
- [ ] Tratamento de erros apropriado
- [ ] Código limpo e comentado

---

## 10. TROUBLESHOOTING

### Problema: Testes não executam
```powershell
# Solução 1: Restaurar dependências
dotnet restore

# Solução 2: Limpar cache de build
dotnet clean
dotnet build
```

### Problema: Erro de porta em uso
```powershell
# Encontrar processo na porta 7001
netstat -ano | findstr :7001

# Matar processo (se necessário)
taskkill /PID <PID> /F
```

### Problema: Certificado SSL inválido
```powershell
# Confiar em certificado de desenvolvimento
dotnet dev-certs https --trust
```

---

## 11. REFERÊNCIAS

- [Documentação xUnit](https://xunit.net/)
- [ASP.NET Core Testing](https://learn.microsoft.com/en-us/dotnet/core/testing/)
- [Postman Documentation](https://learning.postman.com/)
- [Swagger/OpenAPI](https://swagger.io/)

---

**Última Atualização:** 2026-06-23  
**Mantido por:** Grupo de QA  
**Versão do Guia:** 1.0.0
