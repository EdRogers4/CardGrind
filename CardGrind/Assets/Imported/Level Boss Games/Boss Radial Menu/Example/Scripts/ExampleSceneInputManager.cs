using UnityEngine;
using System.Collections.Generic;

namespace LBG.UI.Radial
{
	public class ExampleSceneInputManager : RadialMenuInputManager
	{
        /// <summary>
        /// Process a button based on that layers event name and the buttons event name
        /// </summary>
        /// <param name="layerEvent">Event name of the layer</param>
        /// <param name="buttonEvent">Event name of the button</param>
        /// 

        [SerializeField] private WeaponManager _scriptWeaponManager;

		public override void ProcessButton(string layerEvent, string buttonEvent)
		{
			switch(layerEvent)
            {
                case "Weapon":
                    
                    switch (buttonEvent)
                    {
                        case "Gun01":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 0;
                            _scriptWeaponManager.EquipWeapon(0);
                            break;
                        case "Gun02":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 1;
                            _scriptWeaponManager.EquipWeapon(1);
                            break;
                        case "Gun03":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 2;
                            _scriptWeaponManager.EquipWeapon(2);
                            break;
                        case "Gun04":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 3;
                            _scriptWeaponManager.EquipWeapon(3);
                            break;
                        case "Gun05":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 4;
                            _scriptWeaponManager.EquipWeapon(4);
                            break;
                        case "Gun06":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 5;
                            _scriptWeaponManager.EquipWeapon(5);
                            break;
                        case "Gun07":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 6;
                            _scriptWeaponManager.EquipWeapon(6);
                            break;
                        case "Gun08":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 7;
                            _scriptWeaponManager.EquipWeapon(7);
                            break;
                        case "Gun09":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 8;
                            _scriptWeaponManager.EquipWeapon(8);
                            break;
                        case "Gun10":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 9;
                            _scriptWeaponManager.EquipWeapon(9);
                            break;
                        case "Gun11":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 10;
                            _scriptWeaponManager.EquipWeapon(10);
                            break;
                        case "Gun12":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 11;
                            _scriptWeaponManager.EquipWeapon(11);
                            break;
                        case "Gun13":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 12;
                            _scriptWeaponManager.EquipWeapon(12);
                            break;
                        case "Gun14":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 13;
                            _scriptWeaponManager.EquipWeapon(13);
                            break;
                        case "Gun15":
                            _scriptWeaponManager.CurrentWeapon[_scriptWeaponManager.CurrentCharacterEquip] = 14;
                            _scriptWeaponManager.EquipWeapon(14);
                            break;
                    }
                    break;
                
                case "Main":

                    switch (buttonEvent)
                    {
                        case "Settings":
                            ChangeLayer("Settings");
                            break;

                        case "Useless Buttons":
                            ChangeLayer("Useless Buttons");
                            break;

                        default:
                            //Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;

                case "Settings":
                    switch (buttonEvent)
                    { 
                        case "Volume":
                            ChangeLayer("Volume", ExampleSettings.Volume);
                            break;

                        case "Mute":
                            ExampleSettings.ToggleMute();
                            break;

                        case "Background Colour":
                            ChangeLayer("Background Colour", ExampleSettings.CurrentBackgroundIndex, ExampleSettings.BackgroundColours);
                            break;

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;
             
                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    break;
            }
		}

		/// <summary>
		/// Process a selection layer based on it's layer event name
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="buttonEvent">Event name of the button</param>
		/// <param name="newIndex">The index that is returned from the selction layer</param>
		/// <param name="originalIndex">The original index that was set on the layers construct</param>
		public override void ProcessSelection(string layerEvent, string buttonEvent, int newIndex, int originalIndex)
		{
            switch (layerEvent)
            {
                case "Background Colour":

                    switch (buttonEvent)
                    {
                        case "Update":
                            ExampleSettings.ChangeBackgroundColour(newIndex);
                            break;

                        case "Confirm":
                            ExampleSettings.ChangeBackgroundColour(newIndex);
                            GoToPreviousLayer();
                            break;

                        case "Cancel":
                            ExampleSettings.ChangeBackgroundColour(originalIndex);
                            GoToPreviousLayer();
                            break;

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;
                    
                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    break;
            }
		}

		/// <summary>
		/// Process a slider based on it's layer event name
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="value">Value that is returned from the slider</param>
		/// <param name="saveValue">Should the value be saved</param>
		public override void ProcessSlider(string layerEvent, float value, bool saveValue)
		{
            switch (layerEvent)
            {
                case "Volume":
                    ExampleSettings.ChangeVolume(value);
                    break;

                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    break;
            }
		}
		
		/// <summary>
		/// Process to return values for a slider, selection or toggle button
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="buttonEvent">Event name of the button</param>
		/// <returns>Process to return values for a slider, selection or toggle button</returns>
		public override T ProcessReturnValues<T>(string layerEvent, string buttonEvent)
		{
            switch (layerEvent)
            {
                case "Settings":

                    switch (buttonEvent)
                    {
                        case "Mute":
                            return GetToggleValue<T>(ExampleSettings.Mute);
                            
                        case "Background Colour":
                            return GetSelectionValue<T>(ExampleSettings.BackgroundColours[ExampleSettings.CurrentBackgroundIndex]);

                        case "Volume":
                            return GetSliderValue<T>(ExampleSettings.Volume);
                            
                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            return GetNullValue<T>();
                    }
                    
                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    return GetNullValue<T>();
            }
		}

		/// <summary>
		/// What happens when you are on the top layer and cancel is pressed
		/// </summary>
		public override void LastCancel()
		{
			//Leave blank unless you want to do something unique on your top layer when using the cancel button for example, quit the application.
		}
	}
}
