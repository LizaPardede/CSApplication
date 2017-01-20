using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CSApplication.Adapter;
using CSApplication.Model;
using System.Data;

namespace CSApplication.Activities
{
    [Activity(Label = "PertanyaanPager")]
    public class PertanyaanPager : Activity
    {
        
        Button nextBtn, prevBtn;
        Paginator p;
        private int totalPages = Paginator.TOTAL_NUM_ITEMS / Paginator.ITEMS_PER_PAGE;
        private int currentPage = 0;
        

        private List<PertanyaanModel> mPertanyaanList;
        private ListView mListView;

        int sizeOfList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layout_pager);
            string id = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";

            mPertanyaanList = new List<PertanyaanModel>();
            mListView = FindViewById<ListView>(Resource.Id.lvPertanyaan);
            
            
            CSService.WebService1 mService = new CSService.WebService1();
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            DataSet ds = mService.GetPertanyaanByDepartmen(id);
            mPertanyaanList = getPertanyaanList(ds);
            
            p = new Paginator(mPertanyaanList);

            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage));

            sizeOfList = mPertanyaanList.Count();
            Console.Write("Emak");
            Console.WriteLine(sizeOfList);

            nextBtn = FindViewById<Button>(Resource.Id.idNext);
            prevBtn = FindViewById<Button>(Resource.Id.idPrev);
            //sendBtn = FindViewById<Button>(Resource.Id.btnKirim);
            prevBtn.Enabled = false;

            nextBtn.Click += NextBtn_Click;
            prevBtn.Click += PrevBtn_Click;

        }

        private List<PertanyaanModel> getPertanyaanList(DataSet ds)
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

        private void PrevBtn_Click(object sender, System.EventArgs e)
        {
            currentPage -= 1;
            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage));
            toggleButton();
        }

        private void NextBtn_Click(object sender, System.EventArgs e)
        {
            currentPage +=1;
            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage));
            toggleButton();
        }

        private void toggleButton()
        {
            if (currentPage == totalPages)
            {
                nextBtn.Enabled = false;
                prevBtn.Enabled = true;

            }
            else if (currentPage == 0)
            {
                prevBtn.Enabled = false;
                nextBtn.Enabled = true;
            }
            else
            {
                prevBtn.Enabled = true;
                nextBtn.Enabled = true;
            }
        }
    }
}