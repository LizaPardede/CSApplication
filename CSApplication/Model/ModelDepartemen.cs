using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CSApplication.Model
{
    class ModelDepartemen
    {
        public string DepartemenId;
        public string DepartemenName;

        public string getDepartemenId()
        {
            return DepartemenId;
        }

        public void setDepartemenId(string departemenId)
        {
            this.DepartemenId = departemenId;
        }

        public string getDepartemenName()
        {
            return DepartemenName;
        }

        public void setDepartemenName(string departemenName)
        {
            this.DepartemenName = departemenName;
        }
    }
}