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
    class PertanyaanModel
    {
        private string PertanyaanId;
        private string PertanyaanNama;
        private string DepartemenId;

        public string getPertanyaanId() {
            return PertanyaanId;
        }

        public void setPertanyaanId(string pertanyaanId) {
            this.PertanyaanId = pertanyaanId;
        }

        public string getPertanyaanNama() {
            return PertanyaanNama;
        }

        public void setPertanyaanNama(string pertanyaanNama) {
            this.PertanyaanNama = pertanyaanNama;
        }

        public string getDepId() {
            return DepartemenId;
        }

        public void setDepId(string depId) {
            this.DepartemenId = depId;
        }
    }
}