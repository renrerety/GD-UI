using UnityEngine;

namespace gdui.runtime
{
    public class ApplicationQuitter : MonoBehaviour
    {
        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}

