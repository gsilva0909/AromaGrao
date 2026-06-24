# Relatório de Bugs - Aroma & Grão

## Projeto: Sistema de Pedidos da Cafeteria Aroma & Grão
**Data:** 2026-06-23  
**Status Geral:** 1 Bug encontrado (1 Crítico durante testes de regressão)

---

## Bug #1: Regressão de Desconto com Mudança de Threshold

| Campo | Descrição |
|-------|-----------|
| **ID** | BUG-001 |
| **Título** | Desconto não se aplica corretamente quando threshold é alterado de 100 para 80 |
| **Severidade** | 🔴 CRÍTICA |
| **Status** | IDENTIFICADO |
| **Data de Reporte** | 2026-06-23 |
| **Reportado por** | QA Team |

### Descrição
Ao alterar a regra de desconto de `total >= 100` para `total >= 80`, o sistema quebra em valores entre 80 e 100, onde o desconto deveria ser aplicado mas não era antes.

### Passos para Reprodução

1. Abrir a classe `Pedido`
2. Localizar o método `AplicarDesconto()`
3. Alterar a condição de `if (total >= 100)` para `if (total >= 80)`
4. Executar testes de regressão
5. Observar falha em TC-22

### Entrada/Saída

**Entrada Utilizada:**
- Valor: 16
- Quantidade: 5
- Total Calculado: 80

**Resultado Esperado (com nova regra):**
- Desconto aplicado: Sim (80 * 10% = 8)
- Total com desconto: 72

**Resultado Obtido (teste antigo):**
- Teste TC-22 falhou
- Validação de regressão não foi realizada antes da mudança

### Evidência

```csharp
// Teste que quebrou:
[Fact(DisplayName = "TC-22: Teste com 80")]
public void AplicarDesconto_Total80_RetornaSemDesconto()
{
    // Arrange
    double total = 80;
    double esperado = 80; // Esperava sem desconto (regra antiga)

    // Act
    double resultado = _pedido.AplicarDesconto(total);

    // Assert
    // FALHOU: resultado foi 72 (desconto aplicado)
    Assert.Equal(esperado, resultado); // Erro!
}
```

### Causa Raiz
A mudança de regra de negócio não foi comunicada aos testes. Os testes foram escritos baseados na regra antiga (`>= 100`).

### Impacto
- **Clientes com pedidos entre R$80 e R$100:** Receberão desconto inesperado
- **Margem de lucro:** Será impactada negativamente
- **Consistência:** Quebra a consistência com testes antigos

### Solução Recomendada

**Opção 1: Atualizar os testes (recomendado)**
```csharp
// Atualizar TC-22 e dependentes
if (USAR_NOVA_REGRA) {
    total >= 80 ? aplicar desconto : não aplicar
}
```

**Opção 2: Manter regra antiga**
```csharp
// Manter condição original
if (total >= 100) {
    return total * 0.9;
}
```

### Status da Resolução
- [ ] Analisar impacto de negócio
- [ ] Decidir sobre regra correta
- [ ] Atualizar código e testes
- [ ] Reexecutar testes de regressão
- [ ] Validar com cliente (UAT)
- [ ] Fechar bug

---

## Bug #2: Sem Validação de Entrada Nula em Controller

| Campo | Descrição |
|-------|-----------|
| **ID** | BUG-002 |
| **Título** | Controller não valida corretamente request nula |
| **Severidade** | 🟡 MÉDIA |
| **Status** | RESOLVIDO |
| **Data de Reporte** | 2026-06-23 |
| **Reportado por** | QA Team - API Testing |

### Descrição
O endpoint POST /api/pedido/calcular deveria retornar uma resposta estruturada quando request é nula, mas pode lançar exceção inesperada.

### Passos para Reprodução

1. Usar Postman ou Insomnia
2. Fazer POST para `http://localhost:5000/api/pedido/calcular`
3. Enviar Body: `null`
4. Observar comportamento

### Resultado
**Obtido:** Resposta BadRequest com mensagem válida ✓  
**Status:** RESOLVIDO - Validação foi adicionada no controller

### Solução Implementada
```csharp
if (request == null)
    return BadRequest(new PedidoResponse 
    { 
        Sucesso = false, 
        Mensagem = "Requisição inválida" 
    });
```

---

## Resumo de Bugs

### Por Severidade
| Severidade | Quantidade | Status |
|-----------|-----------|--------|
| 🔴 Crítica | 1 | Identificada |
| 🟡 Média | 1 | Resolvida |
| 🟢 Baixa | 0 | - |

### Por Status
| Status | Quantidade |
|--------|-----------|
| Aberto | 0 |
| Em Análise | 1 |
| Resolvido | 1 |
| Fechado | 0 |

---

## Análise de Qualidade

### Cobertura de Testes
- **Unitários:** 24 testes ✓
- **Integração:** 2 testes ✓
- **API:** 11 testes ✓
- **Total:** 37 testes com 97.8% de sucesso

### Taxa de Defeitos por Tipo de Teste
| Tipo | Total | Bugs | Taxa |
|------|-------|------|------|
| Caixa Branca | 3 | 0 | 0% |
| Caixa-Preta | 11 | 0 | 0% |
| Ad Hoc | 6 | 0 | 0% |
| Regressão | 4 | 1 | 25% |
| API | 11 | 0 | 0% |

### Conclusão
O sistema apresenta **boa qualidade geral**, mas é **crítico implementar testes de regressão ANTES de qualquer mudança de regra de negócio**.

---

**Documento Atualizado:** 2026-06-23  
**Próxima Revisão:** Após resolução de BUG-001
