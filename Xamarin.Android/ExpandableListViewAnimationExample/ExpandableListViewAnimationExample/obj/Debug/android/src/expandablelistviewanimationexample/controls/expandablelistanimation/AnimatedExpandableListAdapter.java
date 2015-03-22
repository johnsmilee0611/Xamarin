package expandablelistviewanimationexample.controls.expandablelistanimation;


public abstract class AnimatedExpandableListAdapter
	extends android.widget.BaseExpandableListAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getChildType:(II)I:GetGetChildType_IIHandler\n" +
			"n_getChildTypeCount:()I:GetGetChildTypeCountHandler\n" +
			"n_getChildView:(IIZLandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetChildView_IIZLandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_getChildrenCount:(I)I:GetGetChildrenCount_IHandler\n" +
			"";
		mono.android.Runtime.register ("ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.AnimatedExpandableListAdapter, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AnimatedExpandableListAdapter.class, __md_methods);
	}


	public AnimatedExpandableListAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AnimatedExpandableListAdapter.class)
			mono.android.TypeManager.Activate ("ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.AnimatedExpandableListAdapter, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public int getChildType (int p0, int p1)
	{
		return n_getChildType (p0, p1);
	}

	private native int n_getChildType (int p0, int p1);


	public int getChildTypeCount ()
	{
		return n_getChildTypeCount ();
	}

	private native int n_getChildTypeCount ();


	public android.view.View getChildView (int p0, int p1, boolean p2, android.view.View p3, android.view.ViewGroup p4)
	{
		return n_getChildView (p0, p1, p2, p3, p4);
	}

	private native android.view.View n_getChildView (int p0, int p1, boolean p2, android.view.View p3, android.view.ViewGroup p4);


	public int getChildrenCount (int p0)
	{
		return n_getChildrenCount (p0);
	}

	private native int n_getChildrenCount (int p0);

	java.util.ArrayList refList;
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
