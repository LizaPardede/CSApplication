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
        //PAGINATION
        Button nextBtn, prevBtn, sendBtn;
        Paginator p;
        private int totalPages = Paginator.TOTAL_NUM_ITEMS / Paginator.ITEMS_PER_PAGE;
        private int currentPage = 0;
        private DBHelper dbHelper;

        private List<PertanyaanModel> mPertanyaanList;
        private ListView mListView;

        private static string pertanyaanSetuju = "";
        private static string pertanyaanTidakSetuju = "";
        private static string detailSetuju = "";
        private static string detailTidakSetuju = "";

        int sizeOfList;

        private CSService.WebService1 mService = new CSService.WebService1();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layout_pager);
            string id = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";

            //DataSet ds = mService.GetPertanyaanByDepartmen(id);
            //mPertanyaanList = getPertanyaanList(ds);
            DataSet ds;

            dbHelper = new DBHelper();

            if (id == "Dep1" || id == "Dep3")
            {
                mPertanyaanList = new List<PertanyaanModel>();
                mListView = FindViewById<ListView>(Resource.Id.lvPertanyaan);

                mService.Url = "http://10.160.1.123/CSService/WebService1.asmx";

                ds = mService.GetPertanyaanByDepartmen(id);
                mPertanyaanList = getPertanyaanList(ds);

                p = new Paginator(mPertanyaanList);

                mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage), dbHelper);

                sizeOfList = mPertanyaanList.Count();
                
                nextBtn = FindViewById<Button>(Resource.Id.idNext);
                prevBtn = FindViewById<Button>(Resource.Id.idPrev);
                sendBtn = FindViewById<Button>(Resource.Id.button1);

                nextBtn.Click += NextBtn_Click; 
                prevBtn.Click += PrevBtn_Click;
                sendBtn.Click += SendBtn_Click;
                
               
            }
            else if (id == "Dep2")
            {
                ds = mService.GetDataPoly();
                var intent = new Intent(this, typeof(PoliActivity));
                intent.PutExtra("idDepartemen", id);
                //StartActivity(intent);
                StartActivityForResult(intent, MainActivity.FINISH_QUESTION);


            }
        }

        

        private void SendBtn_Click(object sender, EventArgs e)
        {
            List<UserResult> userResult = new List<UserResult>();
            userResult = dbHelper.selectTableResult();
            string tempSetuju = "";
            string tempTidakSetuju = "";
            foreach (UserResult user in userResult)
            {
                if (user == null)
                {
                    Console.Write("Empty");
                }

                if (user.kategori_id != null || user.pertanyaan_id != null || user.detail_pertanyaan_id != null || user.starttime != null || user.user != null)
                {
                    if (user.kategori_id == "1")
                    {

                        if (tempSetuju == user.pertanyaan_id)
                        {
                            if (detailSetuju == "")
                            {
                                detailSetuju += user.detail_pertanyaan_id;
                            }
                            else
                            {
                                detailSetuju += "," + user.detail_pertanyaan_id;
                            }
                        }
                        else
                        {
                            if (pertanyaanSetuju == "")
                            {
                                pertanyaanSetuju += user.pertanyaan_id;
                            }
                            else
                            {
                                pertanyaanSetuju += "," + user.pertanyaan_id;
                            }

                            if (detailSetuju == "")
                            {
                                detailSetuju += user.detail_pertanyaan_id;
                            }
                            else
                            {
                                detailSetuju += "," + user.detail_pertanyaan_id;
                            }
                        }
                        tempSetuju = user.pertanyaan_id;
                    }
                    else

                    if (tempTidakSetuju == user.pertanyaan_id)
                    {

                        if (detailTidakSetuju == "")
                        {
                            detailTidakSetuju += user.detail_pertanyaan_id;
                        }
                        else
                        {
                            detailTidakSetuju += "," + user.detail_pertanyaan_id;
                        }
                    }
                    else
                    {
                        {
                            if (pertanyaanTidakSetuju == "")
                            {
                                pertanyaanTidakSetuju += user.pertanyaan_id;
                            }
                            else
                            {
                                pertanyaanTidakSetuju += "," + user.pertanyaan_id;
                            }

                            if (detailTidakSetuju == "")
                            {
                                detailTidakSetuju += user.detail_pertanyaan_id;
                            }
                            else
                            {
                                detailTidakSetuju += "," + user.detail_pertanyaan_id;
                            }
                        }
                        tempTidakSetuju = user.pertanyaan_id;
                    }
                }
            }

            try
            {
                if (detailSetuju != "")
                {
                    mService.InsertCustomerSatisfaction("1", pertanyaanSetuju, detailSetuju, DateTime.Now, "Customer");

                }
                if (detailTidakSetuju != "")
                {
                    mService.InsertCustomerSatisfaction("2", pertanyaanTidakSetuju, detailTidakSetuju, DateTime.Now, "Customer");
                }



                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alertDialog = builder.Create();
                alertDialog.SetIcon(Resource.Drawable.star);
                alertDialog.SetInverseBackgroundForced(true);
                alertDialog.SetMessage("Terimakasih atas partisipasi Anda ");
                

                alertDialog.SetButton("Ok", (s, ev) => {

                    Intent myIntent = new Intent();
                    SetResult(Result.Ok, myIntent);
                    Finish();
                });
                alertDialog.Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            dbHelper.deleteData(userResult);
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
            if (currentPage > 0)
                currentPage -= 1;

            mListView.Adapter = new AdapterPertanyaan(this, p.generatePage(currentPage), dbHelper);
            //toggleButton();
        }

        private void NextBtn_Click(object sender, System.EventArgs e)
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