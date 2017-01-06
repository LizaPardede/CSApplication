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
using CSApplication.Model;
using System.Data;

namespace CSApplication.Activities
{
    class Data
    {
        public Data() {
        }

        public static List<PertanyaanModel> listPertanyaan(string id) {

            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            List<PertanyaanModel> mListPertanyaan = new List<PertanyaanModel>();
            DataSet ds = mService.GetPertanyaanByDepartmen(id);
            mListPertanyaan = getPertanyaan(ds);
            return mListPertanyaan;
        }

        public static List<DetailPertanyaanModel> listDetailPertanyaan(string id)
        {
            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            List<DetailPertanyaanModel> mDetailPertanyaan = new List<DetailPertanyaanModel>();
            DataSet ds = mService.GetDetailPertanyaanByPertanyaan(id);
            mDetailPertanyaan = getDetailPertanyaan(ds);
            return mDetailPertanyaan;
        }

        private static List<DetailPertanyaanModel> getDetailPertanyaan(DataSet ds)
        {
            List<DetailPertanyaanModel> tempDetail = new List<DetailPertanyaanModel>();
            DetailPertanyaanModel mDetail = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mDetail = new DetailPertanyaanModel();
                mDetail.setIdDetailPertanyaan(dr["Id_Detail_Pertanyaan"].ToString());
                mDetail.setIdPertanyaan(dr["Id_Pertanyaan"].ToString());
                mDetail.setDetailPertanyaan(dr["detail_pertanyaan"].ToString());
                tempDetail.Add(mDetail);
            }
            return tempDetail;
        }
    

        private static List<PertanyaanModel> getPertanyaan(DataSet ds)
        {
            List<PertanyaanModel> tempDokter = new List<PertanyaanModel>();
            PertanyaanModel mPertanyaan = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mPertanyaan = new PertanyaanModel();
                mPertanyaan.setPertanyaanId(dr["Id_Pertanyaan"].ToString());
                mPertanyaan.setPertanyaanNama(dr["Nama_Pertanyaan"].ToString());
                tempDokter.Add(mPertanyaan);
            }
            return tempDokter;
        }
    }
}