using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using FarmInfoOverlay.Handlers;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay
{
  public class ModEntry : Mod
  {
    private ModConfig Config = new();
    private OverlayRenderer Renderer = null!;

    public override void Entry(IModHelper helper)
    {
      Config = helper.ReadConfig<ModConfig>();
      Renderer = new OverlayRenderer(Monitor, Config);

      helper.Events.Display.RenderedWorld += OnRenderedWorld;
      helper.Events.Input.ButtonPressed += OnButtonPressed;

      // Registra comandos de console
      helper.ConsoleCommands.Add("fio",
          "Gerencia o FarmInfoOverlay.\n" +
          "Uso: fio <comando>\n\n" +
          "Comandos disponíveis:\n" +
          "  fio status          — mostra o que está ativo\n" +
          "  fio toggle          — ativa/desativa tudo\n" +
          "  fio toggle crops    — plantações\n" +
          "  fio toggle machines — máquinas\n" +
          "  fio toggle animals  — animais\n" +
          "  fio toggle silos    — silos\n" +
          "  fio toggle chests   — baús\n" +
          "  fio days <n>        — dias restantes para mostrar culturas (padrão: 3)\n" +
          "  fio hours <n>       — horas restantes para mostrar máquinas (padrão: 2)",
          OnCommand
      );

      Monitor.Log("FarmInfoOverlay carregado! Digite 'fio status' no console.", LogLevel.Info);
    }

    private void OnCommand(string command, string[] args)
    {
      if (args.Length == 0)
      {
        Monitor.Log("Use 'fio status' ou 'fio toggle <item>'.", LogLevel.Info);
        return;
      }

      switch (args[0].ToLower())
      {
        case "status":
          PrintStatus();
          break;

        case "toggle":
          if (args.Length < 2)
          {
            Config.Enabled = !Config.Enabled;
            Monitor.Log($"Overlay geral: {State(Config.Enabled)}", LogLevel.Info);
          }
          else
          {
            switch (args[1].ToLower())
            {
              case "machines":
                Config.ShowMachineStatus = !Config.ShowMachineStatus;
                Monitor.Log($"Máquinas: {State(Config.ShowMachineStatus)}", LogLevel.Info);
                break;
              case "silos":
                Config.ShowSiloInfo = !Config.ShowSiloInfo;
                Monitor.Log($"Silos: {State(Config.ShowSiloInfo)}", LogLevel.Info);
                break;
              case "chests":
                Config.ShowChestInfo = !Config.ShowChestInfo;
                Monitor.Log($"Baús: {State(Config.ShowChestInfo)}", LogLevel.Info);
                break;
              default:
                Monitor.Log($"Opções válidas: machines, silos, chests", LogLevel.Warn);
                break;
            }
          }
          Helper.WriteConfig(Config);
          break;

        case "hours":
          if (args.Length >= 2 && int.TryParse(args[1], out int hours) && hours >= 0)
          {
            Config.MachineHoursThreshold = hours;
            Helper.WriteConfig(Config);
            Monitor.Log($"Máquinas: mostrando quando faltam ≤ {hours}h.", LogLevel.Info);
          }
          else
            Monitor.Log("Use: fio hours <número>  ex: fio hours 1", LogLevel.Warn);
          break;

        case "debug":
          if (!Context.IsWorldReady)
          {
            Monitor.Log("Entre no jogo primeiro.", LogLevel.Warn);
            break;
          }
          Monitor.Log("=== Todos os objetos no local atual ===", LogLevel.Info);
          foreach (var obj in Game1.currentLocation.objects.Values)
          {
            Monitor.Log($"  type={obj.GetType().Name} name={obj.name} tile={obj.TileLocation} parentSheet={obj.ParentSheetIndex}", LogLevel.Info);
          }
          // Também lista interiores dos buildings
          if (Game1.currentLocation is Farm farm)
          {
            foreach (var building in farm.buildings)
            {
              var interior = building.GetIndoors();
              if (interior == null) continue;
              Monitor.Log($"--- Interior: {building.buildingType.Value} @ tile {building.tileX.Value},{building.tileY.Value} ---", LogLevel.Info);
              foreach (var obj in interior.objects.Values)
              {
                Monitor.Log($"  type={obj.GetType().Name} name={obj.name} tile={obj.TileLocation}", LogLevel.Info);
              }
            }
          }
          break;

        default:
          Monitor.Log($"Comando desconhecido: '{args[0]}'. Comandos: status, toggle, hours, debug", LogLevel.Warn);
          break;
      }
    }

    private void PrintStatus()
    {
      Monitor.Log("=== FarmInfoOverlay — Status ===", LogLevel.Info);
      Monitor.Log($"  Geral:      {State(Config.Enabled)}", LogLevel.Info);
      Monitor.Log($"  Máquinas:   {State(Config.ShowMachineStatus)} (threshold: ≤{Config.MachineHoursThreshold}h)", LogLevel.Info);
      Monitor.Log($"  Silos:      {State(Config.ShowSiloInfo)}", LogLevel.Info);
      Monitor.Log($"  Baús:       {State(Config.ShowChestInfo)}", LogLevel.Info);
    }

    private static string State(bool v) => v ? "✓ ATIVO" : "✗ INATIVO";

    private void OnRenderedWorld(object? sender, RenderedWorldEventArgs e)
    {
      if (!Context.IsWorldReady) return;

      var location = Game1.currentLocation;
      Renderer.Render(e.SpriteBatch, location);

      if (location is Farm farm)
      {
        foreach (var building in farm.buildings)
        {
          var interior = building.GetIndoors();
          if (interior != null)
            Renderer.Render(e.SpriteBatch, interior, building);
        }
      }
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
      if (e.Button == Config.ToggleKey && Context.IsWorldReady)
      {
        Config.Enabled = !Config.Enabled;
        Helper.WriteConfig(Config);
        Monitor.Log($"Overlay {(Config.Enabled ? "ativado" : "desativado")}", LogLevel.Info);
      }
    }
  }
}