package md5e7b2058380b18dbfd63ea0ecbfc3b9c2;


public class PertanyaanActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_radioButton_OnClick:(Landroid/view/View;)V:__export__\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CSApplication.Activities.PertanyaanActivity, CSApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PertanyaanActivity.class, __md_methods);
	}


	public PertanyaanActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PertanyaanActivity.class)
			mono.android.TypeManager.Activate ("CSApplication.Activities.PertanyaanActivity, CSApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void radioButton_Click (android.view.View p0)
	{
		n_radioButton_OnClick (p0);
	}

	private native void n_radioButton_OnClick (android.view.View p0);


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
