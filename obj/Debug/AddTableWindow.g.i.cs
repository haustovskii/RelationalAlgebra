﻿#pragma checksum "..\..\AddTableWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "75EB6FADF351AA8DBB8605C92B5CB2A6D7E382788FD7099B910CBCB5DCA114F5"
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
    /// AddTableWindow
    /// </summary>
    public partial class AddTableWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal RelationalAlgebra.AddTableWindow WndAddTable;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgPollUp;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgClose;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxNameTable;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxCountColumns;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnOk;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\AddTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnClose;
        
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
            System.Uri resourceLocater = new System.Uri("/RelationalAlgebra;component/addtablewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddTableWindow.xaml"
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
            this.WndAddTable = ((RelationalAlgebra.AddTableWindow)(target));
            return;
            case 2:
            
            #line 27 "..\..\AddTableWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 28 "..\..\AddTableWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ImgPollUp = ((System.Windows.Controls.Image)(target));
            
            #line 44 "..\..\AddTableWindow.xaml"
            this.ImgPollUp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgPollUp_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ImgClose = ((System.Windows.Controls.Image)(target));
            
            #line 51 "..\..\AddTableWindow.xaml"
            this.ImgClose.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgClose_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TbxNameTable = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TbxCountColumns = ((System.Windows.Controls.TextBox)(target));
            
            #line 80 "..\..\AddTableWindow.xaml"
            this.TbxCountColumns.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbxCountColumns_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnOk = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\AddTableWindow.xaml"
            this.BtnOk.Click += new System.Windows.RoutedEventHandler(this.BtnOk_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtnClose = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\AddTableWindow.xaml"
            this.BtnClose.Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

