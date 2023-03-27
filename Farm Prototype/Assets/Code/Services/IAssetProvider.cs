﻿using Code.Data.GardenBedData;
using Code.Data.ResourceData;
using Code.GameLogic.Gardens;
using Code.UI;
using Code.UI.Windows;
using Code.UI.Windows.SelectedAreaTab;
using Code.UI.Windows.ShopTab;

namespace Code.Services
{
    public interface IAssetProvider
    {
        GardenAreaVisual GardenAreaVisual { get; }
        Garden Garden { get; }
        GridSell GridSell { get; }
        GardenTypeHolder GardenTypeHolder { get; }
        ResourceHolder ResourceHolder { get; }
        SelectedGardenWindow SelectedGardenWindow { get; }
        ShopUI ShopUI { get; }
        HUD HUD { get; }
    }
}