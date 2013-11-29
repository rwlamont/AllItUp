using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Caliburn.Micro;
using IndiaTango.Models;
using System.IO;

namespace IndiaTango.ViewModels
{
    class LoadInDataMetaViewModel : BaseViewModel
    {
         private IWindowManager _windowManager;
        private SimpleContainer _container;
        private String _metaFile;
        private String _dataFile;
        private String _lastPath;
        private String _fullMetaPath;
        private string _fullDataPath;

        public LoadInDataMetaViewModel(IWindowManager windowManager, SimpleContainer container)
        {
            _windowManager = windowManager;
            _container = container;
            _dataFile = "Please load a data file";
            _metaFile = "Please load a meta file";
            _lastPath = Environment.SpecialFolder.MyDocuments.ToString();
        }

        #region View Properties
        public string Title
        {
            get { return "Files To Import"; }
        }

        public string DataFile
        {
            get { return _dataFile; }
            set { _dataFile = value; NotifyOfPropertyChange(() => DataFile); }
        }

        public string MetaFile
        {
            get { return _metaFile; }
            set { _metaFile = value; NotifyOfPropertyChange(() => MetaFile); }
        }
        public string IntroTitle
        {
            get { return "Load Site in from Data and Meta"; }
            
        }

        #endregion

        #region Event Handlers
        public void btnImport()
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Common.UserExportRoot;
            dialog.Description = "Open The Date file you want";

            this.TryClose();
            
        }

        public void btnData()
        {
            var dialog = new OpenFileDialog{ Filter = @"All B3 Data Files|*.csv;*.txt;*.gln|CSV Files|*.csv|TSV Files|*.txt|GLEON files|*.gln" };
            dialog.Title = "Open the data file";
            dialog.InitialDirectory = _lastPath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _fullDataPath = dialog.FileName.ToString();
                DataFile = Path.GetFileName(dialog.FileName);
                _lastPath = Path.GetDirectoryName(dialog.FileName);
            }
        }

        public void btnMeta()
        {
            var dialog = new OpenFileDialog { Filter = @"All B3 Meta Files|*.txt;|Gleon Meta Files|*.txt"};
            dialog.Title = "Open the meta file";
            dialog.InitialDirectory = _lastPath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _fullMetaPath = dialog.FileName.ToString();
                MetaFile = Path.GetFileName(dialog.FileName);
                _lastPath = Path.GetDirectoryName(dialog.FileName);
            }
        }

        public void btnCancel()
        {
            this.TryClose();
        }

        #endregion
    
    }
}
