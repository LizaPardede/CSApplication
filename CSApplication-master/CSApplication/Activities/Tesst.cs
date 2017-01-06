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
using CSApplication.Fragments;

namespace CSApplication.Activities
{
    [Activity(Label = "Tesst")]
    public class Tesst : Activity
    {
        public string idDept; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView (Resource.Layout.test);
            idDept = Intent.GetStringExtra("idDepartemen") ?? "Data tidak tersedia";
            // Toast.MakeText(this, id, ToastLength.Long).Show();

            //TextView txt = FindViewById<TextView>(Resource.Id.textView1);
            //txt.Text = id;

            var pertanyaanFragment = new PertanyaanFragment();
            FragmentTransaction fTx = this.FragmentManager.BeginTransaction();
            fTx.Add(Resource.Id.fragment1, pertanyaanFragment);
            fTx.Commit();
        }
    }
}