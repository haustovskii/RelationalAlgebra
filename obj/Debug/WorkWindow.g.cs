﻿#pragma checksum "..\..\WorkWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5E5FD3695F853283759EEFDC97DA2A9FEDAAFF4821BF886C52F925C2852DC525"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using RelationalAlgebra;
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


namespace RelationalAlgebra {
    
    
    /// <summary>
    /// WorkWindow
    /// </summary>
    public partial class WorkWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgPollUp;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgClose;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddTable;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnLoadTables;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSaveTables;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnOpenInfo;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxOperation;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxResult;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\WorkWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl MainTabControl;
        
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
            System.Uri resourceLocater = new System.Uri("/RelationalAlgebra;component/workwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WorkWindow.xaml"
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
            
            #line 22 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 23 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ImgPollUp = ((System.Windows.Controls.Image)(target));
            
            #line 38 "..\..\WorkWindow.xaml"
            this.ImgPollUp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgPollUp_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ImgClose = ((System.Windows.Controls.Image)(target));
            
            #line 45 "..\..\WorkWindow.xaml"
            this.ImgClose.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgClose_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnAddTable = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\WorkWindow.xaml"
            this.BtnAddTable.Click += new System.Windows.RoutedEventHandler(this.BtnAddTable_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnLoadTables = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\WorkWindow.xaml"
            this.BtnLoadTables.Click += new System.Windows.RoutedEventHandler(this.BtnLoadTables_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnSaveTables = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\WorkWindow.xaml"
            this.BtnSaveTables.Click += new System.Windows.RoutedEventHandler(this.BtnSaveTables_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnOpenInfo = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\WorkWindow.xaml"
            this.BtnOpenInfo.Click += new System.Windows.RoutedEventHandler(this.BtnOpenInfo_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TbxOperation = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.TbxResult = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            
            #line 153 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 159 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 165 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 171 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 177 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 183 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 189 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 202 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 208 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 214 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 220 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 226 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 232 "..\..\WorkWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OperationButton_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            this.MainTabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

