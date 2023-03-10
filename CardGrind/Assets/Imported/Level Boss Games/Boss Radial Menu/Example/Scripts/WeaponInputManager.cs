using UnityEngine;
using System.Collections;
using System;

namespace LBG.UI.Radial
{
	public class WeaponInputManager : RadialMenuInputManager
	{

		/// <summary>
		/// Process a button based on that layers event name and the buttons event name
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="buttonEvent">Event name of the button</param>
		public override void ProcessButton(string layerEvent, string buttonEvent)
		{
			switch(layerEvent)
            {
                //This is a template layer case
                case "PUT_LAYER_EVENT_HERE":

                    switch(buttonEvent)
                    {
                        //This is a template button case 
                        case "CHANGE_TO_BUTTON_LAYER":
                            //ChangeLayer("NEXT LAYER NAME");
                            break;
                        case "CHANGE_TO_SLIDER_LAYER":
                            //ChangeLayer("NEXT LAYER NAME", FLOAT_VALUE);
                            break;
                        case "CHANGE_TO_SELECTION_LAYER":
                            //ChangeLayer("NEXT LAYER NAME", CURRENT_INDEX_OF_STRING_ARRAY_USED_FOR_SELECTION_VALUES, STRING_ARRAY_OF_SELECTION_VALUES);
                            break;

                        //Add more button cases here

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;
                
                //Add more layer cases here

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
                //This is a template layer case
                case "PUT_LAYER_EVENT_HERE":

                    switch (buttonEvent)
                    {
                        //This is a template button case 
                        case "PUT_BUTTON_EVENT_HERE":
                            break;

                        //Add more button cases here

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;

                //Add more layer cases here

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
                //This is a template layer case
                case "PUT_LAYER_EVENT_HERE":

                    break;
                //Add more layer cases here

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
                //This is a template layer case
                case "PUT_LAYER_EVENT_HERE":

                    switch (buttonEvent)
                    {
                        //This is a template toggle button case 
                        case "TOGGLE_BUTTON_EXAMPLE":
                        //    return GetToggleValue<T>(PUT_BOOL_VALUE_HERE);

                        //
                        case "SELECTION_BUTTON_EXAMPLE":
                        //    return GetSelectionValue<T>(PUT_STRING_VALUE_HERE_THAT_IS_USED_TO_REPRESENT_THE_SELECTION_VALUE);

                        case "SLIDER_BUTTON_EXAMPLE":
                         //   return GetSliderValue<T>(PUT_FLOAT_VALUE_HERE);

                        //Add more button cases here

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            return GetNullValue<T>();
                    }

                //Add more layer cases here

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
