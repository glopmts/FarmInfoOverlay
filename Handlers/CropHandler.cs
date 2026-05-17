using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using FarmInfoOverlay.Rendering;

namespace FarmInfoOverlay.Handlers
{
  public class CropHandler
  {
    // Só mostra se faltam até X dias — configurável
    private const int DaysThreshold = 3;

    public IEnumerable<OverlayItem> GetOverlays(GameLocation location)
    {
      foreach (var (tile, feature) in location.terrainFeatures.Pairs)
      {
        if (feature is not HoeDirt dirt || dirt.crop == null)
          continue;

        var crop = dirt.crop;

        // Pronto para colheita — sempre mostra
        if (crop.fullyGrown.Value)
        {
          yield return new OverlayItem
          {
            TilePosition = tile,
            Label = "✓",
            BorderColor = Color.LightGreen
          };
          continue;
        }

        // Calcula dias restantes
        int remaining = 0;
        int phase = crop.currentPhase.Value;
        int dayInPh = crop.dayOfCurrentPhase.Value;

        for (int i = phase; i < crop.phaseDays.Count - 1; i++)
        {
          remaining += crop.phaseDays[i];
          if (i == phase) remaining -= dayInPh;
        }

        // Só mostra se estiver perto da colheita
        if (remaining > DaysThreshold) continue;

        yield return new OverlayItem
        {
          TilePosition = tile,
          Label = $"{remaining}d",
          BorderColor = remaining <= 1 ? Color.Yellow : Color.White
        };
      }
    }
  }
}