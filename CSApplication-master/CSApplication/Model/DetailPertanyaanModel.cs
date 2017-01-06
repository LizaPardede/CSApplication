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
    class DetailPertanyaanModel
    {
        private string IdDetailPertanyaan;
        private string IdPertanyaan;
        private string DetailPertanyaan;

        public string getIdDetail() {
            return IdDetailPertanyaan;
        }

        public void setIdDetailPertanyaan(string idDetailPertanyaan) {
            this.DetailPertanyaan = idDetailPertanyaan;
        }

        public string getIdPertanyaan() {
            return IdPertanyaan;
        }

        public void setIdPertanyaan(string idPertanyaan) {
            this.IdPertanyaan = idPertanyaan;
        }

        public string getDetailPertanyaan() {
            return DetailPertanyaan;
        }

        public void setDetailPertanyaan(string detailPertanyaan) {
            this.DetailPertanyaan = detailPertanyaan;
        }
    }
}