using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Happyhour.Control
{
    public class FBReturnObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propName));
            }
        }

        public FBReturnObject()
        {
            _id = null;

        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public static FBReturnObject FromJson(string JsonText)
        {
            FBReturnObject obj = JsonConvert.DeserializeObject<FBReturnObject>(JsonText);
            return obj;
        }
        private string _id;
    }
}
