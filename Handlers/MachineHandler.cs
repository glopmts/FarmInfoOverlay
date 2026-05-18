using Microsoft.Xna.Framework;
using StardewValley;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class MachineHandler
  {
    private readonly ModConfig Config;

    private static readonly HashSet<string> MachineIds = new()
        {
            "Furnace", "Keg", "Preserves Jar", "Cheese Press",
            "Loom", "Oil Maker", "Recycling Machine", "Crystalarium",
            "Bee House", "Mushroom Box", "Slime Egg-Press"
        };

    public MachineHandler(ModConfig config)
    {
      Config = config;
    }

    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      foreach (var obj in location.objects.Values)
      {
        if (!MachineIds.Contains(obj.name)) continue;

        // Pronto — não mostra nada, o jogo já exibe o brilho/animação padrão
        if (obj.readyForHarvest.Value) continue;

        // Vazio — não mostra
        if (obj.MinutesUntilReady <= 0) continue;

        // Fora do threshold — não mostra
        if (obj.MinutesUntilReady > Config.MachineHoursThreshold * 60) continue;

        // Dentro da última hora — mostra em minutos
        int mins = obj.MinutesUntilReady;
        string label;

        if (mins <= 10)
        {
          // Últimos 10 minutos — converte para segundos do jogo
          // 1 minuto in-game = 43 segundos reais (aprox)
          int segundos = mins * 43;
          label = $"{segundos}s";
        }
        else
        {
          int hours = mins / 60;
          int rest = mins % 60;
          label = hours > 0 ? $"{hours}h{rest:D2}m" : $"{mins}m";
        }

        yield return new OverlayItem
        {
          TilePosition = obj.TileLocation,
          Label = label,
          BorderColor = mins <= 10 ? Color.OrangeRed : Color.Yellow
        };
      }
    }
  }
}