using StardewModdingAPI;

namespace FarmInfoOverlay
{
  public class ModConfig
  {
    public bool Enabled { get; set; } = true;
    public SButton ToggleKey { get; set; } = SButton.U;

    public bool ShowSiloInfo { get; set; } = true;
    public bool ShowChestInfo { get; set; } = true;
    public bool ShowMachineStatus { get; set; } = true;
    public float OverlayScale { get; set; } = 1.0f;
    public float OverlayOpacity { get; set; } = 0.85f;
    public int YOffset { get; set; } = -60;

    // Thresholds ajustáveis via console
    public int CropDaysThreshold { get; set; } = 3;
    public int MachineHoursThreshold { get; set; } = 2;
  }
}