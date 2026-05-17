using Microsoft.Xna.Framework;
using StardewValley;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class MachineHandler
  {
    private static readonly HashSet<string> MachineIds = new()
        {
            "Furnace", "Keg", "Preserves Jar", "Cheese Press",
            "Loom", "Oil Maker", "Recycling Machine", "Crystalarium",
            "Bee House", "Mushroom Box", "Slime Egg-Press"
        };

    // Só alerta máquinas com menos de X horas restantes
    private const int HoursThreshold = 2;

    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      foreach (var obj in location.objects.Values)
      {
        if (!MachineIds.Contains(obj.name)) continue;

        // Pronto — sempre mostra
        if (obj.readyForHarvest.Value)
        {
          yield return new OverlayItem
          {
            TilePosition = obj.TileLocation,
            Label = "✓",
            BorderColor = Color.LightGreen
          };
          continue;
        }

        // Vazio — não mostra (reduz ruído)
        if (obj.MinutesUntilReady <= 0) continue;

        // Só mostra se estiver quase pronto
        if (obj.MinutesUntilReady > HoursThreshold * 60) continue;

        int hours = obj.MinutesUntilReady / 60;
        int mins = obj.MinutesUntilReady % 60;
        string label = hours > 0 ? $"{hours}h{mins:D2}m" : $"{mins}m";

        yield return new OverlayItem
        {
          TilePosition = obj.TileLocation,
          Label = label,
          BorderColor = Color.Yellow
        };
      }
    }
  }
}