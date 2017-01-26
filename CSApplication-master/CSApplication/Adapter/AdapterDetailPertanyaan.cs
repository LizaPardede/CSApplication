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

        public AdapterDetailPertanyaan(Activity context, List<DetailPertanyaanModel> items, DBHelper dbHelper, string idPertanyaan)
        {
            this.mItems = items;
            this.mContext = context;
            this.dbHelper = dbHelper;
            this.IdPertanyaan = idPertanyaan;
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

            if (radioGroup == null)
            {
                Console.WriteLine("Auuuuuaaaaaaaaaaaaa");
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
            return row;
        }
    }
}