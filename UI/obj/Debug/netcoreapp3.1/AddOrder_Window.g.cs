﻿#pragma checksum "..\..\..\AddOrder_Window.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71330C644E0DCD74D8A9A037C5CAD826AEA075B6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using System.Windows.Controls.Ribbon;
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
using UI;


namespace UI {
    
    
    /// <summary>
    /// AddOrder_Window
    /// </summary>
    public partial class AddOrder_Window : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 15 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_datetime;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_order;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_state;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_total;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Submit;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Add_Order;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Cancel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_MainPage;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\AddOrder_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgOrderItem;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UI;component/addorder_window.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddOrder_Window.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.textbox_datetime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.textbox_order = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textbox_state = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textbox_total = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Btn_Submit = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\AddOrder_Window.xaml"
            this.Btn_Submit.Click += new System.Windows.RoutedEventHandler(this.Btn_Submit_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Btn_Add_Order = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\AddOrder_Window.xaml"
            this.Btn_Add_Order.Click += new System.Windows.RoutedEventHandler(this.Btn_Add_Order_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Btn_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\AddOrder_Window.xaml"
            this.Btn_Cancel.Click += new System.Windows.RoutedEventHandler(this.Btn_Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Btn_MainPage = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\AddOrder_Window.xaml"
            this.Btn_MainPage.Click += new System.Windows.RoutedEventHandler(this.Btn_MainPage_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dgOrderItem = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 10:
            
            #line 35 "..\..\..\AddOrder_Window.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnDelete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
