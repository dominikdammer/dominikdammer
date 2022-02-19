/// <copyright> (c) Out of the box 2022 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using System;


//Observer Pattern Sender -> !Handler! -> Listener
public class GameEvents
{
    #region events

    public event Action<int> onTriggerOn;

    public event Action<int> onTriggerOff;

    public event Action<int> onDialogueTriggerOn;

    public event Action<int> onDialogueTriggerOff;



    #endregion events

    #region singleton

    private static GameEvents instance;

    public static GameEvents Instance
	{
        get
		{
            if (instance == null)
			{
                instance = new GameEvents ();
			}

            return instance;
		}
	}

    #endregion singleton

    #region event handling

    public void TriggerOn(int id)
    {
        onTriggerOn?.Invoke(id);
    }

    public void TriggerOff(int id)
    {
        onTriggerOff?.Invoke(id);
    }

    public void OnDialogueTriggerOn(int id)
    {
        onDialogueTriggerOn?.Invoke(id);
    }

    public void OnDialogueTriggerOff(int id)
    {
        onDialogueTriggerOff?.Invoke(id);
    }



    #endregion event handling
}