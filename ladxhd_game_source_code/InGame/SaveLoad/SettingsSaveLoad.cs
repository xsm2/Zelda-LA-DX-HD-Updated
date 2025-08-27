﻿﻿using Microsoft.Xna.Framework;
using ProjectZ.InGame.Controls;
using ProjectZ.InGame.Things;

namespace ProjectZ.InGame.SaveLoad
{
    class SettingsSaveLoad
    {
        private static readonly string SettingsFilePath = SaveManager.GetSettingsFile();

        public static void LoadSettings()
        {
            var saveManager = new SaveManager();

            // error loading file
            if (!saveManager.LoadFile(SettingsFilePath))
                return;

            Values.PathContentFolder = saveManager.GetString("ContentPath", Values.PathContentFolder);
            Values.PathSaveFolder = saveManager.GetString("SavePath", Values.PathSaveFolder);

            GameSettings.GameScale = saveManager.GetInt("GameScale", GameSettings.GameScale);
            GameSettings.GameScale = MathHelper.Clamp(GameSettings.GameScale, -1, 11);
            GameSettings.UiScale = saveManager.GetInt("UIScale", GameSettings.UiScale);
            GameSettings.UiScale = MathHelper.Clamp(GameSettings.UiScale, 0, 12);
            GameSettings.MusicVolume = saveManager.GetInt("MusicVolume", GameSettings.MusicVolume);
            GameSettings.EffectVolume = saveManager.GetInt("EffectVolume", GameSettings.EffectVolume);
            GameSettings.MuteInactive = saveManager.GetBool("MuteInactive", GameSettings.MuteInactive);
            GameSettings.EnableShadows = saveManager.GetBool("EnableShadows", GameSettings.EnableShadows);
            GameSettings.Autosave = saveManager.GetBool("Autosave", GameSettings.Autosave);
            GameSettings.HeartBeep = saveManager.GetBool("HeartBeep", GameSettings.HeartBeep);
            GameSettings.ScreenShake = saveManager.GetBool("ScreenShake", GameSettings.ScreenShake);
            GameSettings.SmoothCamera = saveManager.GetBool("SmoothCamera", GameSettings.SmoothCamera);
            GameSettings.IsFullscreen = saveManager.GetBool("IsFullscreen", GameSettings.IsFullscreen);
            GameSettings.LockFps = saveManager.GetBool("LockFPS", GameSettings.LockFps);
            GameSettings.ItemsOnRight = saveManager.GetBool("ItemsOnRight", GameSettings.ItemsOnRight);
            GameSettings.Controller = saveManager.GetString("Controller", GameSettings.Controller);

            Values.ControllerDeadzone = saveManager.GetFloat("ControllerDeadzone", Values.ControllerDeadzone);
            Game1.LanguageManager.CurrentLanguageIndex = saveManager.GetInt("CurrentLanguage", Game1.LanguageManager.CurrentLanguageIndex);

            ControlHandler.LoadButtonMap(saveManager);
            ControlHandler.SetControllerIndex();
        }

        public static void SaveSettings()
        {
            var saveManager = new SaveManager();

            saveManager.SetString("ContentPath", Values.PathContentFolder);
            saveManager.SetString("SavePath", Values.PathSaveFolder);

            saveManager.SetInt("Version", 1);
            saveManager.SetInt("GameScale", GameSettings.GameScale);
            saveManager.SetInt("UIScale", GameSettings.UiScale);
            saveManager.SetInt("MusicVolume", GameSettings.MusicVolume);
            saveManager.SetInt("EffectVolume", GameSettings.EffectVolume);
            saveManager.SetBool("MuteInactive", GameSettings.MuteInactive);
            saveManager.SetBool("EnableShadows", GameSettings.EnableShadows);
            saveManager.SetBool("Autosave", GameSettings.Autosave);
            saveManager.SetBool("HeartBeep", GameSettings.HeartBeep);
            saveManager.SetBool("ScreenShake", GameSettings.ScreenShake);
            saveManager.SetBool("SmoothCamera", GameSettings.SmoothCamera);
            saveManager.SetBool("IsFullscreen", GameSettings.IsFullscreen);
            saveManager.SetBool("LockFPS", GameSettings.LockFps);
            saveManager.SetBool("ItemsOnRight", GameSettings.ItemsOnRight);
            saveManager.SetString("Controller", GameSettings.Controller);

            saveManager.SetFloat("ControllerDeadzone", Values.ControllerDeadzone);
            saveManager.SetInt("CurrentLanguage", Game1.LanguageManager.CurrentLanguageIndex);

            ControlHandler.SaveButtonMaps(saveManager);

            saveManager.Save(SettingsFilePath, Values.SaveRetries);
        }
    }
}
