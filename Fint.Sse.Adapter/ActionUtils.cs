using System;
using Fint.Event.Model;
using FINT.Model.Ressurser.Tilganger;

namespace Fint.Sse.Adapter
{
    public class ActionUtils
    {
        public static bool IsValidTilgangerAction(string eventAction)
        {
            if (Enum.TryParse(eventAction, true, out TilgangerActions action))
            {
                if (Enum.IsDefined(typeof(TilgangerActions), action))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidStatusAction(string eventAction)
        {
            if (Enum.TryParse(eventAction, true, out DefaultActions action))
            {
                if (Enum.IsDefined(typeof(DefaultActions), action))
                {
                    return true;
                }
            }
            return false;
        }
    }
}