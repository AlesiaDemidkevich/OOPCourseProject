﻿#pragma checksum "..\..\Water.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2168A4DB14061CCA73D28F82A11B83AC2B2B0E77"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Dnevnik_Pitaniya;
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


namespace Dnevnik_Pitaniya {
    
    
    /// <summary>
    /// Water
    /// </summary>
    public partial class Water : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\Water.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Water_Table;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Water.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn P_date;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Water.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn P_water;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Water.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Date;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\Water.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WaterBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Dnevnik_Pitaniya;component/water.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Water.xaml"
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
            this.Water_Table = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.P_date = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 3:
            this.P_water = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 4:
            this.Date = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            
            #line 43 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Left_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 48 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Right_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 53 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_300);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 59 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_50);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 65 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close);
            
            #line default
            #line hidden
            return;
            case 10:
            this.WaterBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            
            #line 71 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_Water);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 72 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Water);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 73 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_100);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 79 "..\..\Water.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_200);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

