using System.Windows;

namespace UserManager.Extensions
{
    internal static class WindowExtensions
    {
        public static void SetInteractable(this Window window, bool state)
        {
            window.Opacity = state ? 1 : 0.4f;
            window.IsEnabled = state;
        }
    }
}
