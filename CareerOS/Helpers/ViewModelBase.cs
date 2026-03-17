using CommunityToolkit.Mvvm.ComponentModel;

namespace CareerOS.Helpers;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string statusMessage = "Pronto";
}
