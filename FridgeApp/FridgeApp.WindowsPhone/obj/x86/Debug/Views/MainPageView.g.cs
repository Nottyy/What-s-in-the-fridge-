﻿

#pragma checksum "D:\GithubRepository\What-s-in-the-fridge-\FridgeApp\FridgeApp.WindowsPhone\Views\MainPageView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D701115DE1AC01351C816DB8A8C3DE80"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FridgeApp.Views
{
    partial class MainPageView : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 14 "..\..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.NavigateToFridgeProductsPage;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 18 "..\..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.NavigateToExpiringProductsPage;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 23 "..\..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.NavigateToEatenProductsPage;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 27 "..\..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


