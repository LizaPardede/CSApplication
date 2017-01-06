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
    class DokterModel
    {
        private string DokterId;
        private string DokterName;
        public string DepartemenId;

        public string getDokterId()
        {
            return DokterId;
        }

        public void setDolterId(string dokterId)
        {
            this.DokterId = dokterId;
        }
        public string getDokterName() {
            return DokterName;
        }

        public void setDokterName(string dokterName) {
            this.DokterName = dokterName;
        }

        public string getDepartemenId()
        {
            return DepartemenId;
        }

        public void setDepartemenId(string departemenId)
        {
            this.DepartemenId = departemenId;
        }

    }
}