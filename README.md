# FarmInfoOverlay

A mod for **Stardew Valley** that displays useful farm item information in real time.

## 📋 Description

FarmInfoOverlay adds an on-screen visual overlay showing detailed information about objects on your farm, making it easier to manage and optimize crops, machines, chests, and animals.

## ✨ Features

- **Silo Information** — View the amount of stored hay
- **Chest Information** — Check chest contents and available space
- **Crop Harvest Days** — Track how many days remain until crops are ready
- **Machine Status** — Monitor processing machine progress
- **Animal Information** — View status and details of your animals

## 🎮 How to Use

### Installation

1. Install [SMAPI](https://smapi.io/?utm_source=chatgpt.com) (Stardew Modding API)
2. Extract the mod files into:

```txt
Stardew Valley\Mods\FarmInfoOverlay
```

3. Launch the game normally

### Controls

- **H** (default) — Toggle the overlay on/off
  - You can change the key in the configuration file

### Configuration

Edit the `config.json` file inside the mod folder to customize settings:

```json
{
  "Enabled": true,
  "ToggleKey": "U",
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

#### Available Options

- `Enabled` — Enable or disable the mod
- `ToggleKey` — Key used to toggle the overlay (default: U)
- `ShowSiloInfo` — Display silo information
- `ShowChestInfo` — Display chest information
- `ShowCropDays` — Display remaining crop growth days
- `ShowMachineStatus` — Display machine processing status
- `ShowAnimalInfo` — Display animal information
- `OverlayScale` — Overlay visual scale (0.5 to 2.0)
- `OverlayOpacity` — Overlay transparency (0.0 to 1.0)
- `YOffset` — Vertical offset position relative to the object

## 🔧 Requirements

- **Stardew Valley** 1.6+
- [SMAPI](https://smapi.io/?utm_source=chatgpt.com) (Stardew Modding API)
- **.NET 6.0** or later

### Optional Dependencies

- [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098?utm_source=chatgpt.com) — Provides a graphical configuration menu

## 🛠️ Development

### Project Structure

```txt
FarmInfoOverlay/
├── Handlers/              # Logic for processing object information
│   ├── AnimalHandler.cs
│   ├── ChestHandler.cs
│   ├── CropHandler.cs
│   ├── MachineHandler.cs
│   └── SiloHandler.cs
├── Rendering/             # Visual rendering system
│   ├── OverlayItem.cs
│   └── OverlayRenderer.cs
├── Interfaces/            # Project interfaces
├── ModEntry.cs            # Mod entry point
├── ModConfig.cs           # Mod configuration
├── manifest.json          # Mod metadata
└── FarmInfoOverlay.csproj # C# project file
```

### Build

```bash
dotnet build
```

or

```bash
dotnet build FarmInfoOverlay.csproj
```

The mod will be compiled into:

```txt
bin/Debug/
```

and automatically copied to:

```txt
C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley\Mods\FarmInfoOverlay\
```

## 📝 Example Usage

When enabling the mod using the **H** key, floating overlays will appear above farm objects displaying:

- Item quantity inside chests
- Remaining crop growth days
- Machine processing status
- Amount of hay stored in silos
- Animal happiness and status

## 🐛 Reporting Issues

If you encounter problems or bugs:

1. Make sure SMAPI is up to date
2. Confirm the `config.json` file is valid
3. Check the `SMAPI-latest.txt` file located in:

```txt
%appdata%\StardewValley\
```

for error details

## 📄 License

This project is provided as-is for use with Stardew Valley.

## 👨‍💻 Author

**GlopMts**

## 🔗 Links

- [Nexus Mods](https://www.nexusmods.com/stardewvalley/mods/InfoPlantation?utm_source=chatgpt.com)
- [SMAPI](https://smapi.io/?utm_source=chatgpt.com)
- [Stardew Valley](https://www.stardewvalley.net/?utm_source=chatgpt.com)

---

**Version:** 1.0.5
**Minimum API Version:** 4.0.0
