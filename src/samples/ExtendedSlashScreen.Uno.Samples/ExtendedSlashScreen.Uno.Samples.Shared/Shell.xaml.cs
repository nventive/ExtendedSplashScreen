using Nventive.ExtendedSplashScreen;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ExtendedSlashScreen.Uno.Samples
{
	public sealed partial class Shell : UserControl
	{
		public static Shell Instance { get; private set; }

		public Shell(LaunchActivatedEventArgs e)
		{
			this.InitializeComponent();

			Instance = this;

			NavigationFrame.Navigate(typeof(MainPage), e.Arguments);
		}

		public IExtendedSplashScreen ExtendedSplashScreen => this.AppExtendedSplashScreen;

		public Frame NavigationFrame => this.RootNavigationFrame;
	}
}
