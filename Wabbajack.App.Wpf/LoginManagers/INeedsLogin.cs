
using System.Windows.Input;
using System.Windows.Media;
using ReactiveUI;

namespace Wabbajack.LoginManagers;

public interface INeedsLogin
{
    string SiteName { get; }
    ICommand TriggerLogin { get; set; }
    ICommand ClearLogin { get; set; }
    ImageSource Icon { get; set; }
}