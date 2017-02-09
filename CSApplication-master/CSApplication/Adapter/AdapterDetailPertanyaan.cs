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
using CSApplication.Activities;

namespace CSApplication.Adapter
{
    class AdapterDetailPertanyaan : BaseAdapter<DetailPertanyaanModel>
    {
        private List<DetailPertanyaanModel> mItems;
        private Activity mContext;
        List<UserResult> listResult = new List<UserResult>();
        private DBHelper dbHelper;
        private string IdPertanyaan;
        private List<UserResult> rslt;

        public AdapterDetailPertanyaan(Activity context, List<DetailPertanyaanModel> items, DBHelper dbHelper, string idPertanyaan)
        {
            this.mItems = items;
            this.mContext = context;
            this.dbHelper = dbHelper;
            this.IdPertanyaan = idPertanyaan;
        }

        public AdapterDetailPertanyaan(Activity context, List<DetailPertanyaanModel> items, DBHelper dbHelper, string idPertanyaan, List<UserResult> rslt)
        {
            this.mItems = items;
            this.mContext = context;
            this.dbHelper = dbHelper;
            this.IdPertanyaan = idPertanyaan;
            this.rslt = rslt;
        }


        public override DetailPertanyaanModel this[int position]
        {
            get
            {
                return mItems[position];
            }
        }

        public override int Count
        {
            get
            {
                return mItems.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.layout_detail_pertanyaan_adapter, parent, false);
            }

            TextView txtDetail = row.FindViewById<TextView>(Resource.Id.textDetailPertanyaan);
            txtDetail.Text = mItems[position].getDetailPertanyaan();
            RadioGroup radioGroup = row.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            RadioButton radioSetuju = row.FindViewById<RadioButton>(Resource.Id.radioButton1);
            RadioButton radioTidak = row.FindViewById<RadioButton>(Resource.Id.radioButton2);

            if (rslt != null)
            {
                foreach (UserResult rsl in rslt)
                {
                    if (rsl.pertanyaan_id.Equals(mItems[position].getIdPertanyaan()))
                    {
                        if (rsl.kategori_id.Equals("Setuju"))
                        {
                            radioGroup.Check(Resource.Id.radioButton1);
                        }
                        else if(rsl.kategori_id.Equals("Tidak Setuju"))
                        {
                            radioGroup.Check(Resource.Id.radioButton2);
                        }

                        radioTidak.Click += (object sender, EventArgs e) =>
                        {
                            RadioButton checkedRadioButton = row.FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);

                            UserResult userResult = new UserResult()
                            {
                                kategori_id = "2",
                                pertanyaan_id = IdPertanyaan,
                                detail_pertanyaan_id = mItems[position].getIdDetail(),
                                starttime = DateTime.Now,
                                user = "Customer"
                            };
                            dbHelper.InsertIntoTableResult(userResult);

                        };

                        radioSetuju.Click += (object sender, EventArgs e) =>
                        {
                            RadioButton checkedRadioButton = row.FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);

                            UserResult userResult = new UserResult()
                            {
                                kategori_id = "1",
                                pertanyaan_id = IdPertanyaan,
                                detail_pertanyaan_id = mItems[position].getIdDetail(),
                                starttime = DateTime.Now,
                                user = "Customer"
                            };
                            dbHelper.InsertIntoTableResult(userResult);

                        };

                    }
                }
            }
            else
            {
                //radioTidak.Click += (object sender, EventArgs e) =>
                //{
                //    RadioButton checkedRadioButton = row.FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);

                //    UserResult userResult = new UserResult()
                //    {
                //        kategori_id = "2",
                //        pertanyaan_id = IdPertanyaan,
                //        detail_pertanyaan_id = mItems[position].getIdDetail(),
                //        starttime = DateTime.Now,
                //        user = "Customer"
                //    };
                //    dbHelper.InsertIntoTableResult(userResult);

                //};

                //radioSetuju.Click += (object sender, EventArgs e) =>
                //{
                //    RadioButton checkedRadioButton = row.FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);

                //    UserResult userResult = new UserResult()
                //    {
                //        kategori_id = "1",
                //        pertanyaan_id = IdPertanyaan,
                //        detail_pertanyaan_id = mItems[position].getIdDetail(),
                //        starttime = DateTime.Now,
                //        user = "Customer"
                //    };
                //    dbHelper.InsertIntoTableResult(userResult);

                //};

                //Toast.MakeText(mContext, "Cannot be null", ToastLength.Short).Show();
                //TextView txtError = row.FindViewById<TextView>(Resource.Id.textError);
                //txtError.Visibility = ViewStates.Visible;

            }


            return row;
            }




    }
}