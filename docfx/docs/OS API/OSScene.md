# Scenes Enumeration

## Definition
Namespace: OSLoader  
Assembly: OSloader.dll  
Source: Scenes.cs  

## Description
Enum containing all existing scenes in Obenseuer, ordered by their internal [Scene Index](https://docs.unity3d.com/ScriptReference/SceneManagement.Scene-buildIndex.html)

## Usage
[!code-csharp[](../Code Examples/OSScene.cs)]

## Properties

Property | Scene description
-- | -
`MainMenu` | First scene when the game is loaded
`OpenSewer_Intro` | Intro cutscene
`OpenSewer_Tenement` | OpenSewer exterior 
`Interior_Start` | Intro building
`Interior_PlayerTenement` | Player's building
`InteriorTenement_PlayerCanalSaunas` | Sauna behind the player's tenement
`InteriorTenement_B` | Interior of Tenement B (Above Speakeasy entryance)
`InteriorTenement_DeekulaA` | idem for Deekula A
`InteriorTenement_DeekulaB` | idem for Deekula B
`InteriorTenement_DeekulaC` | idem for Deekula C
`InteriorTenement_DeekulaMineEntrance`       | Entrance to the mines from the Deekula B and C blocks
`InteriorTenement_GeneralStore` | OneStopShop Interior
`InteriorTenement_OMarket` | O-Market interior
`InteriorTenement_Pharmacy` | Pharmacy interior
`InteriorTenement_Speakeasy` | Speakeasy (Entrance below Tenement A) interior
`InteriorTenement_Greenhouse` | Greenhouse (On roof of O-Market) interior 
`InteriorTenement_GreenhouseStorage` | Greenhouse storage house (behind greenhouse) interior
`InteriorTenement_KurahaaraHome` | Kurahaara brothers' house (next to greenhouse) interior
`InteriorTenement_Gatehouse` | Gatehouse with prison and Bazaar inspection border
`InteriorTenement_House3` | One of the houses' interior (will check later which)
`InteriorTenement_House4` | One of the houses' interior (will check later which)
`InteriorTenement_House5` | One of the houses' interior (will check later which)
`InteriorTenement_LotShack1` | One of the houses' interior (will check later which)
`InteriorTenement_LotShack2` | One of the houses' interior (will check later which)
`InteriorTenement_LotShack3` | One of the houses' interior (will check later which)
`InteriorTenement_RedemptionMilitia` | Redemption Militia (to the right of Bazaar inspection border)
`InteriorTenement_Bus` | Interior of bus (middle of the central map)
`InteriorTenement_Kolhola` | Kolhola A interior
`InteriorTenement_Caravan` | Caravan interior (between Deekula B and C entrances)
`UnderMap` | Level for when you glitch through the ground 
`TestMapEmpty` | (Inaccessible in normal gameplay) A test map
`TestMapCharacters` | (Inaccessible in normal gameplay) A test map
`OpenSewer_Mines` | Small mines with floodgate and 
`InteriorTenement_A` | (Inaccessible currently) Tenement A interior