using StardewModdingAPI;

namespace FarmInfoOverlay
{
  public class ModConfig
  {
    public bool Enabled { get; set; } = true;
    public SButton ToggleKey { get; set; } = SButton.H;

    // Quais overlays aparecem
    public bool ShowSiloInfo { get; set; } = true;
    public bool ShowChestInfo { get; set; } = true;
    public bool ShowCropDays { get; set; } = true;
    public bool ShowMachineStatus { get; set; } = true;
    public bool ShowAnimalInfo { get; set; } = true;

    // Visual
    public float OverlayScale { get; set; } = 1.0f;
    public float OverlayOpacity { get; set; } = 0.85f;
    public int YOffset { get; set; } = -60; // pixels acima do objeto
  }
}