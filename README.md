# FarmInfoOverlay

Um mod para **Stardew Valley** que exibe informações úteis sobre itens da fazenda em tempo real.

## 📋 Descrição

FarmInfoOverlay é um mod que adiciona uma sobreposição visual na tela mostrando informações detalhadas sobre os objetos da sua fazenda, facilitando o gerenciamento e otimização de plantações, máquinas e animais.

## ✨ Funcionalidades

- **Informações de Silos** - Visualize a quantidade de feno armazenado
- **Informações de Baús** - Veja o conteúdo e espaço disponível
- **Dias de Colheita** - Acompanhe quantos dias faltam para colher as plantações
- **Status de Máquinas** - Verifique o progresso de máquinas de processamento
- **Informações de Animais** - Veja status e detalhes dos seus animais

## 🎮 Como Usar

### Instalação

1. Instale o [SMAPI](https://smapi.io/) (Stardew Modding API)
2. Extraia os arquivos do mod em `Stardew Valley\Mods\FarmInfoOverlay`
3. Execute o jogo normalmente

### Controles

- **H** (padrão) - Ativa/desativa o overlay
  - Você pode alterar a tecla no arquivo de configuração

### Configuração

Edite o arquivo `config.json` na pasta do mod para personalizar:

```json
{
  "Enabled": true,
  "ToggleKey": "H",
  "ShowSiloInfo": true,
  "ShowChestInfo": true,
  "ShowCropDays": true,
  "ShowMachineStatus": true,
  "ShowAnimalInfo": true,
  "OverlayScale": 1.0,
  "OverlayOpacity": 0.85,
  "YOffset": -60
}
```

#### Opções Disponíveis:

- `Enabled` - Ativa ou desativa o mod
- `ToggleKey` - Tecla para ativar/desativar o overlay (padrão: H)
- `ShowSiloInfo` - Exibe informações de silos
- `ShowChestInfo` - Exibe informações de baús
- `ShowCropDays` - Exibe dias para colheita
- `ShowMachineStatus` - Exibe status de máquinas
- `ShowAnimalInfo` - Exibe informações de animais
- `OverlayScale` - Escala visual do overlay (0.5 a 2.0)
- `OverlayOpacity` - Transparência (0.0 a 1.0)
- `YOffset` - Posição vertical do overlay em relação ao objeto

## 🔧 Requisitos

- **Stardew Valley** 1.6+
- **SMAPI** (Stardew Modding API)
- **.NET 6.0** ou superior

### Dependências Opcionais:

- [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098) - Para menu de configuração gráfica

## 🛠️ Desenvolvimento

### Estrutura do Projeto

```
FarmInfoOverlay/
├── Handlers/              # Lógica para processar informações de objetos
│   ├── AnimalHandler.cs
│   ├── ChestHandler.cs
│   ├── CropHandler.cs
│   ├── MachineHandler.cs
│   └── SiloHandler.cs
├── Rendering/            # Sistema de renderização visual
│   ├── OverlayItem.cs
│   └── OverlayRenderer.cs
├── Interfaces/           # Interfaces do projeto
├── ModEntry.cs           # Ponto de entrada do mod
├── ModConfig.cs          # Configurações do mod
├── manifest.json         # Metadados do mod
└── FarmInfoOverlay.csproj # Arquivo de projeto C#
```

### Compilação

```bash
dotnet build
```

O mod será compilado em `bin/Debug/` e copiado automaticamente para:

```
C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley\Mods\FarmInfoOverlay\
```

## 📝 Exemplo de Uso

Ao ativar o mod com a tecla **H**, você verá sobreposições flutuantes acima dos objetos exibindo:

- Quantidade de itens em baús
- Dias restantes para colheita
- Status de processamento de máquinas
- Quantidade de feno nos silos
- Status de felicidade dos animais

## 🐛 Reportar Problemas

Se encontrar problemas ou bugs:

1. Verifique se o SMAPI está atualizado
2. Confirme que o arquivo `config.json` está válido
3. Verifique o arquivo `SMAPI-latest.txt` em `%appdata%\StardewValley\` para detalhes de erro

## 📄 Licença

Este projeto é fornecido como está para uso com Stardew Valley.

## 👨‍💻 Autor

**GlopMts**

## 🔗 Links

- [Nexus Mods](https://www.nexusmods.com/stardewvalley/mods/InfoPlantation)
- [SMAPI](https://smapi.io/)
- [Stardew Valley](https://www.stardewvalley.net/)

---

**Versão:** 1.0.0  
**Versão Mínima de API:** 4.0.0
