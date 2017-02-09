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
using CSApplication.Adapter;

namespace CSApplication.Activities
{
    [Activity(Label = "PoliActivity")]
    public class PoliActivity : Activity
    {
        private List<PoliModel> mPoliList;
        private ListView mListView;
        private string idDep;

        public const int FINISH_QUESTION = 0;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {  
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_poli);
            idDep = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";

            mListView = FindViewById<ListView>(Resource.Id.listPoli);
            mPoliList = new List<PoliModel>();

            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            DataSet ds = mService.GetDataPoly();
            mPoliList = getPoliName(ds);

            mListView.Adapter = new AdapterPoli(this, mPoliList);
            mListView.ItemClick += MListView_ItemClick;
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var mPoli = mPoliList[e.Position];
            var intent = new Intent(this, typeof(DokterActivity));
            intent.PutExtra("idPoli", mPoli.getPoliId());
            intent.PutExtra("idDepartemen", idDep);
            StartActivityForResult(intent, MainActivity.FINISH_QUESTION);
        }

        private List<PoliModel> getPoliName(DataSet ds)
        {
            List<PoliModel> tempPoli = new List<PoliModel>();
            PoliModel mPoli = null;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                mPoli = new PoliModel();
                mPoli.setPoliId(dr["Poly_ID"].ToString());
                mPoli.setPoliName(dr["Poly_Name"].ToString());
                tempPoli.Add(mPoli);
            }
            return tempPoli;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            var intent = new Intent(this, typeof(MainActivity))
                  .SetFlags(ActivityFlags.ReorderToFront);
            StartActivity(intent);
        }

    }
}