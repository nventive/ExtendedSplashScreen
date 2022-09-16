#if __WASM__
using System;
using System.Linq;
using System.Text;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Activation;

namespace Nventive.ExtendedSplashScreen.WinUI
{
	public partial class ExtendedSplashScreen
	{
		private FrameworkElement GetNativeSplashScreen(SplashScreen splashScreen)
		{
			// ExtendedSplashscreen is not implemented on WASM.
			return null;
		}
	}
}
#endif
