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
    class PoliModel
    {
        private string PoliId;
        private string PoliName;
        public string DepartemenId;

        public string getDepartemenId()
        {
            return DepartemenId;
        }

        public void setDepartemenId(string departemenId)
        {
            this.DepartemenId = departemenId;
        }


        public string getPoliId() {
            return PoliId;
        }

        public void setPoliId(string poliId) {
            this.PoliId = poliId;
        }

        public string getPoliName() {
            return PoliName;
        }

        public void setPoliName(string poliName) {
            this.PoliName = poliName;
        }
    }
}