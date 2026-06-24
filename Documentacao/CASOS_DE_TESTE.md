# Documentação de Casos de Teste - Aroma & Grão

## Projeto: Sistema de Pedidos da Cafeteria Aroma & Grão
**Disciplina:** Engenharia de Software: Métricas, Qualidade e Testes  
**Instituição:** UNIARAXÁ  
**Data:** 2026-06-23

---

## 1. TESTES DE CAIXA BRANCA

### Objetivo
Validar todos os caminhos independentes do código, analisando a estrutura interna.

### Método Testado: `StatusPedido(double total)`

#### Fluxograma de Decisão
```
Entrada (total)
    ↓
[total < 20?]
    ├─ Sim → Retorna "Pequeno" ✓
    └─ Não ↓
        [total < 100?]
            ├─ Sim → Retorna "Médio" ✓
            └─ Não → Retorna "Grande" ✓
```

#### Caminhos Independentes Identificados
1. **Caminho 1:** total < 20 → "Pequeno"
2. **Caminho 2:** 20 ≤ total < 100 → "Médio"
3. **Caminho 3:** total ≥ 100 → "Grande"

#### Casos de Teste - Caixa Branca

| ID | Descrição | Entrada | Resultado Esperado | Status |
|:--:|-----------|---------|-------------------|--------|
| TC-01 | Entrada pequena (15) | 15 | "Pequeno" | ✓ PASSOU |
| TC-02 | Entrada média (50) | 50 | "Médio" | ✓ PASSOU |
| TC-03 | Entrada grande (150) | 150 | "Grande" | ✓ PASSOU |

---

## 2. TESTES DE CAIXA-PRETA

### Objetivo
Validar entradas e saídas sem conhecimento da estrutura interna usando particionamento de equivalência e análise de valor limite.

### Particionamento de Equivalência

| Classe | Intervalo | Resultado | Exemplos |
|--------|-----------|-----------|----------|
| Pequeno | total < 20 | "Pequeno" | 0, 10, 15, 19.99 |
| Médio | 20 ≤ total < 100 | "Médio" | 20, 50, 75, 99.99 |
| Grande | total ≥ 100 | "Grande" | 100, 150, 200, 500 |

### Análise de Valor Limite

#### Testes de Limite - StatusPedido

| ID | Descrição | Entrada | Resultado Esperado | Status |
|:--:|-----------|---------|-------------------|--------|
| TC-04 | Limite inferior (19.99) | 19.99 | "Pequeno" | ✓ PASSOU |
| TC-05 | Limite superior (20.00) | 20.00 | "Médio" | ✓ PASSOU |
| TC-06 | Limite intermediate (99.99) | 99.99 | "Médio" | ✓ PASSOU |
| TC-07 | Limite superior (100.00) | 100.00 | "Grande" | ✓ PASSOU |

#### Testes de Limite - AplicarDesconto

| ID | Descrição | Entrada | Resultado Esperado (Desconto 10%) | Status |
|:--:|-----------|---------|----------------------------------|--------|
| TC-08 | Valor baixo (50) | 50 | 50 (sem desconto) | ✓ PASSOU |
| TC-09 | Valor limite (99.99) | 99.99 | 99.99 (sem desconto) | ✓ PASSOU |
| TC-10 | Valor limite (100.00) | 100.00 | 90 (com desconto) | ✓ PASSOU |
| TC-11 | Valor alto (150) | 150 | 135 (com desconto) | ✓ PASSOU |

### Testes de Cálculo de Total

| ID | Descrição | Valor | Quantidade | Resultado Esperado | Status |
|:--:|-----------|-------|------------|-------------------|--------|
| TC-12 | Cálculo básico | 10 | 5 | 50 | ✓ PASSOU |
| TC-13 | Quantidade zero | 10 | 0 | 0 | ✓ PASSOU |
| TC-14 | Valor com decimais | 9.99 | 3 | 29.97 | ✓ PASSOU |

---

## 3. TESTES AD HOC

### Objetivo
Inserir valores inválidos, negativos e valores extremos para tentar quebrar o sistema.

### Casos de Teste Ad Hoc

| ID | Descrição | Entrada | Comportamento Esperado | Resultado Obtido | Status |
|:--:|-----------|---------|----------------------|-------------------|--------|
| TC-15 | Valor negativo (-10) | Valor: -10, Qtd: 5 | Lança ArgumentException | Exceção lançada ✓ | ✓ PASSOU |
| TC-16 | Quantidade negativa (-5) | Valor: 10, Qtd: -5 | Lança ArgumentException | Exceção lançada ✓ | ✓ PASSOU |
| TC-17 | Quantidade muito grande | Valor: 10, Qtd: 999999999 | Calcula corretamente | 9999999990 ✓ | ✓ PASSOU |
| TC-18 | Valor com muitas casas decimais | 9.99 | Calcula com precisão | Resultado preciso ✓ | ✓ PASSOU |
| TC-19 | Valor zero | 0 | Retorna 0 | 0 ✓ | ✓ PASSOU |
| TC-20 | Todos os valores extremos | Múltiplos | Trata sem crash | Sem erros ✓ | ✓ PASSOU |

---

## 4. TESTES DE REGRESSÃO

### Objetivo
Alterar uma regra (desconto de 100 para 80) e reexecutar testes para verificar o que quebrou.

### Casos de Teste de Regressão

#### Antes da Mudança
- Desconto aplicado quando: `total >= 100`

#### Mudança Proposta
- Desconto aplicado quando: `total >= 80`

| ID | Descrição | Entrada | Resultado (antes) | Resultado (depois) | Status |
|:--:|-----------|---------|------------------|------------------|--------|
| TC-21 | Teste com 50 | 50 | 50 (sem desc.) | 50 (sem desc.) | ✓ PASSOU |
| TC-22 | Teste com 80 | 80 | 80 (sem desc.) | 72 (com desc.) | ⚠ QUEBROU |
| TC-23 | Teste com 100 | 100 | 90 (com desc.) | 90 (com desc.) | ✓ PASSOU |
| TC-24 | Teste com 150 | 150 | 135 (com desc.) | 135 (com desc.) | ✓ PASSOU |

**Conclusão:** A alteração quebra o teste TC-22, que agora aplica desconto em valores >= 80.

---

## 5. TESTES DE SANIDADE

### Objetivo
Após corrigir um defeito, validar somente a funcionalidade corrigida.

#### Cenário de Defeito Corrigido
**Problema:** Desconto não era aplicado para valores >= 100.  
**Correção:** Implementar validação correta e aplicação de 10% de desconto.

### Casos de Teste de Sanidade

| ID | Descrição | Entrada | Resultado Esperado | Status |
|:--:|-----------|---------|-------------------|--------|
| TC-25 | Desconto aplicado em 100 | 100 | 90 | ✓ PASSOU |
| TC-26 | Desconto aplicado em 150 | 150 | 135 | ✓ PASSOU |
| TC-27 | Desconto correto (10%) | 200 | 180 | ✓ PASSOU |
| TC-28 | Sem desconto para < 100 | 99 | 99 | ✓ PASSOU |

---

## 6. TESTES DE FUMAÇA

### Objetivo
Executar rapidamente as funcionalidades principais e validar se o build pode ser aprovado.

### Checklist de Fumaça

| # | Funcionalidade | Entrada | Status |
|:--:|---|---|:--:|
| 1 | Abrir aplicação | - | ✓ OK |
| 2 | Calcular total | Valor: 25, Qtd: 5 | ✓ OK |
| 3 | Aplicar desconto | Total: 125 | ✓ OK |
| 4 | Gerar status | Total: 125 | ✓ OK |
| 5 | Gerar resumo completo | Valor: 25, Qtd: 5 | ✓ OK |

**Resultado:** ✓ BUILD APROVADA

---

## 7. TESTES DE ACEITAÇÃO DO USUÁRIO (UAT)

### Objetivo
Validar os requisitos de negócio com o cliente (outro grupo).

### Cenário 1: Desconto para Compras Acima de R$100

**Como cliente da cafeteria, quero receber desconto de 10% para compras acima de R$100.**

| ID | Critério | Entrada | Resultado Esperado | Resultado Obtido | Aprovação |
|:--:|----------|---------|-------------------|-------------------|-----------|
| UAT-01 | Pedido R$150 | Valor: 30, Qtd: 5 | Total: 135 | Total: 135 | ✓ APROVADO |
| UAT-02 | Pedido R$100 | Valor: 20, Qtd: 5 | Total: 90 | Total: 90 | ✓ APROVADO |
| UAT-03 | Pedido R$99 | Valor: 33, Qtd: 3 | Total: 99 (sem desc.) | Total: 99 | ✓ APROVADO |

**Status Geral:** ✓ APROVADO - Requisito atendido com sucesso

### Cenário 2: Classificação de Pedidos

**Como gerente, quero classificar pedidos por tamanho para melhor planejamento.**

| ID | Critério | Entrada | Resultado Esperado | Aprovação |
|:--:|----------|---------|-------------------|-----------|
| UAT-04 | Pedido pequeno | Total: 15 | "Pequeno" | ✓ APROVADO |
| UAT-05 | Pedido médio | Total: 50 | "Médio" | ✓ APROVADO |
| UAT-06 | Pedido grande | Total: 150 | "Grande" | ✓ APROVADO |

**Status Geral:** ✓ APROVADO - Requisito atendido com sucesso

---

## 8. TESTES DE API

### Objetivo
Validar a API REST usando Postman/Insomnia/Swagger.

### Endpoint: POST /api/pedido/calcular

#### Request Body
```json
{
  "valor": 10,
  "quantidade": 5
}
```

#### Expected Response
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

### Casos de Teste de API

| ID | Método | Endpoint | Entrada | Resultado Esperado | HTTP Status | Status |
|:--:|--------|----------|---------|-------------------|------------|--------|
| API-01 | POST | /api/pedido/calcular | {"valor":10, "qtd":5} | {"total":50, ...} | 200 | ✓ PASSOU |
| API-02 | POST | /api/pedido/calcular | {"valor":25, "qtd":5} | {"total":125, "desconto":12.5} | 200 | ✓ PASSOU |
| API-03 | POST | /api/pedido/calcular | {"valor":-10, "qtd":5} | {"sucesso":false, ...} | 400 | ✓ PASSOU |
| API-04 | POST | /api/pedido/calcular | {"valor":10, "qtd":-5} | {"sucesso":false, ...} | 400 | ✓ PASSOU |
| API-05 | POST | /api/pedido/resumo | {"valor":30, "qtd":4} | {"subtotal":120, "desconto":12} | 200 | ✓ PASSOU |
| API-06 | GET | /api/pedido/status/150 | - | {"status":"Grande"} | 200 | ✓ PASSOU |

---

## 9. TESTES EXPLORATÓRIOS

### Objetivo
Explorar livremente o sistema registrando falhas, comportamentos inesperados e sugestões.

### Relatório Exploratório

| Data | Testador | Funcionalidade | Resultado | Observação |
|:--:|----------|---|----------|-----------|
| 2026-06-23 | Grupo A | Cálculo Total | ✓ OK | Funciona corretamente com valores positivos |
| 2026-06-23 | Grupo A | Aplicar Desconto | ✓ OK | Desconto de 10% aplicado para valores >= 100 |
| 2026-06-23 | Grupo A | Status do Pedido | ✓ OK | Classificação funciona conforme esperado |
| 2026-06-23 | Grupo A | Validação de Entrada | ✓ OK | Rejeita valores negativos adequadamente |
| 2026-06-23 | Grupo A | Integração API | ✓ OK | Endpoints respondendo conforme esperado |

### Sugestões de Melhoria

1. **Adicionar persistência de dados** - Salvar pedidos em banco de dados
2. **Implementar autenticação** - Validar usuários antes de processar pedidos
3. **Adicionar histórico de pedidos** - Permitir consulta de pedidos anteriores
4. **Implementar diferentes faixas de desconto** - Descontos progressivos
5. **Interface gráfica** - Adicionar UI para melhor usabilidade

---

## 10. RESUMO EXECUTIVO

### Estatísticas de Testes

| Tipo | Total | Passou | Falhou | Taxa Sucesso |
|------|-------|--------|--------|--------------|
| Caixa Branca | 3 | 3 | 0 | 100% |
| Caixa-Preta | 11 | 11 | 0 | 100% |
| Ad Hoc | 6 | 6 | 0 | 100% |
| Regressão | 4 | 3 | 1 | 75% |
| Sanidade | 4 | 4 | 0 | 100% |
| Fumaça | 5 | 5 | 0 | 100% |
| API | 6 | 6 | 0 | 100% |
| UAT | 6 | 6 | 0 | 100% |
| **TOTAL** | **45** | **44** | **1** | **97.8%** |

### Conclusões

✓ **Sistema aprovado para produção** com ressalva:
- Implementar testes de regressão antes de alterar regras de negócio
- 97.8% de cobertura de testes alcançado
- Todos os requisitos de negócio validados e aprovados pelo cliente

### Próximos Passos

1. Corrigir quebra de regressão (se necessário)
2. Implementar sugestões de melhoria
3. Documentar API no Swagger
4. Realizar testes de carga e performance
5. Implementar testes de segurança

---

**Data de Conclusão:** 2026-06-23  
**Aprovado por:** Grupo de QA  
**Status Final:** ✓ APROVADO
