﻿#pragma checksum "..\..\SuperUserWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D116F24C5BEF4E2E5D02F9FA1B7BD6A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PL {
    
    
    /// <summary>
    /// SuperUserWindow
    /// </summary>
    public partial class SuperUserWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu MainMenu;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CreateForum;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Set;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem CreateSub;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Del;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Exit;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid createForum;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newForumName;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newForumDescription;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newForumRules;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newForumPolicy;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\SuperUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newAdminUserName;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/superuserwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SuperUserWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainMenu = ((System.Windows.Controls.Menu)(target));
            return;
            case 2:
            
            #line 7 "..\..\SuperUserWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Actions);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CreateForum = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 4:
            this.Set = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 5:
            this.CreateSub = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 6:
            this.Del = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 7:
            this.Exit = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 8:
            
            #line 15 "..\..\SuperUserWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_View);
            
            #line default
            #line hidden
            return;
            case 9:
            this.createForum = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.newForumName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.newForumDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.newForumRules = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.newForumPolicy = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.newAdminUserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            
            #line 30 "..\..\SuperUserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_addCouponFromSocial);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
