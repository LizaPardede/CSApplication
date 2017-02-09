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
    [Activity(Label = "PertanyaanRJ")]
    public class PertanyaanRJ : Activity
    {
        private List<PertanyaanModel> mRawatJalanList;
        private ListView mListView;

        //PAGINATION
        Button nextBtn, prevBtn, sendBtn;
        Paginator p;
        private int totalPages = Paginator.TOTAL_NUM_ITEMS / Paginator.ITEMS_PER_PAGE;
        private int currentPage = 0;
        private DBHelper dbHelper;

        //Collect temp data
        private static string pertanyaanSetuju = "";
        private static string pertanyaanTidakSetuju = "";
        private static string detailSetuju = "";
        private static string detailTidakSetuju = "";
        
        //Sum of data length
        int sizeOfList;

        private CSService.WebService1 mService = new CSService.WebService1();

        private string idDep;
        private string poliId;
        private string dokterId;

        public const int FINISH_QUESTION = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_pager);
            idDep = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";
            poliId = Intent.GetStringExtra("idPoli") ?? "Poli ID tidak tersedia";
            dokterId = Intent.GetStringExtra("idDokter") ?? "Dokter ID tidak tersedia";
            
            DataSet ds;
            dbHelper = new DBHelper();
            
            mRawatJalanList = new List<PertanyaanModel>();
            mListView = FindViewById<ListView>(Resource.Id.lvPertanyaan);
            
            mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

            ds = mService.GetPertanyaanByDepartmen(idDep);
            mRawatJalanList = getRawatJalanList(ds);
            
            p = new Paginator(mRawatJalanList);

            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage), dbHelper);
            
            sizeOfList = mRawatJalanList.Count();

            nextBtn = FindViewById<Button>(Resource.Id.idNext);
            prevBtn = FindViewById<Button>(Resource.Id.idPrev);
            sendBtn = FindViewById<Button>(Resource.Id.button1);
            
            nextBtn.Click += NextBtn_Click;
            prevBtn.Click += PrevBtn_Click;
            sendBtn.Click += SendBtn_Click;

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            List<UserResult> resultRawatJalan = new List<UserResult>();
            resultRawatJalan = dbHelper.selectTableResult();
            string tempSetuju = "";
            string tempTidakSetuju = "";
            
            foreach (UserResult result in resultRawatJalan)
            {
              
                if (result.kategori_id != null || result.pertanyaan_id != null || result.detail_pertanyaan_id != null || result.starttime != null || result.user != null)
                {
                    if (result.kategori_id == "1")
                    {

                        if (tempSetuju == result.pertanyaan_id)
                        {
                            if (detailSetuju == "")
                            {
                                detailSetuju += result.detail_pertanyaan_id;
                            }
                            else
                            {
                                detailSetuju += "," + result.detail_pertanyaan_id;
                            }
                        }
                        else
                        {
                            if (pertanyaanSetuju == "")
                            {
                                pertanyaanSetuju += result.pertanyaan_id;
                            }
                            else
                            {
                                pertanyaanSetuju += "," + result.pertanyaan_id;
                            }

                            if (detailSetuju == "")
                            {
                                detailSetuju += result.detail_pertanyaan_id;
                            }
                            else
                            {
                                detailSetuju += "," + result.detail_pertanyaan_id;
                            }
                        }
                        tempSetuju = result.pertanyaan_id;
                    }
                    else

                    if (tempTidakSetuju == result.pertanyaan_id)
                    {

                        if (detailTidakSetuju == "")
                        {
                            detailTidakSetuju += result.detail_pertanyaan_id;
                        }
                        else
                        {
                            detailTidakSetuju += "," + result.detail_pertanyaan_id;
                        }
                    }
                    else
                    {
                        {
                            if (pertanyaanTidakSetuju == "")
                            {
                                pertanyaanTidakSetuju += result.pertanyaan_id;
                            }
                            else
                            {
                                pertanyaanTidakSetuju += "," + result.pertanyaan_id;
                            }

                            if (detailTidakSetuju == "")
                            {
                                detailTidakSetuju += result.detail_pertanyaan_id;
                            }
                            else
                            {
                                detailTidakSetuju += "," + result.detail_pertanyaan_id;
                            }
                        }
                        tempTidakSetuju = result.pertanyaan_id;
                    }
                }
            }
            
            try
            {
               
                if (detailSetuju != "")
                {
                    
                    mService.InsertCustomerSatisfactionRawatJalan(poliId, dokterId, "1", pertanyaanSetuju, detailSetuju, DateTime.Now, "Customer");

                }
                if (detailTidakSetuju != "")
                {
                    mService.InsertCustomerSatisfactionRawatJalan(poliId, dokterId, "2", pertanyaanTidakSetuju, detailTidakSetuju, DateTime.Now, "Customer");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            dbHelper.deleteData(resultRawatJalan);
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
                currentPage -= 1;

            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage), dbHelper);
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (currentPage < sizeOfList - 1)
                currentPage += 1;

            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage), dbHelper);
            if (currentPage == sizeOfList - 1)
            {
                sendBtn.Visibility = ViewStates.Visible;
                nextBtn.Visibility = ViewStates.Gone;
                prevBtn.Visibility = ViewStates.Gone;
            }
        }


        private List<PertanyaanModel> getRawatJalanList(DataSet ds)
        {
            List<PertanyaanModel> tempPrj = new List<PertanyaanModel>();
            PertanyaanModel mPrj = null;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                mPrj = new PertanyaanModel();
                mPrj.setDepId(dr["Departemen_ID"].ToString());
                mPrj.setPertanyaanId(dr["Id_Pertanyaan"].ToString());
                mPrj.setPertanyaanNama(dr["Nama_Pertanyaan"].ToString());
                tempPrj.Add(mPrj);
            }
            return tempPrj;
        }


    }
}