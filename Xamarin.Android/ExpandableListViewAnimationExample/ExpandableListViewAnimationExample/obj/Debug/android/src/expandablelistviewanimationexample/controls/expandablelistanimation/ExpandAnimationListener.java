package expandablelistviewanimationexample.controls.expandablelistanimation;


public class ExpandAnimationListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.animation.Animation.AnimationListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationEnd:(Landroid/view/animation/Animation;)V:GetOnAnimationEnd_Landroid_view_animation_Animation_Handler:Android.Views.Animations.Animation/IAnimationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onAnimationRepeat:(Landroid/view/animation/Animation;)V:GetOnAnimationRepeat_Landroid_view_animation_Animation_Handler:Android.Views.Animations.Animation/IAnimationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onAnimationStart:(Landroid/view/animation/Animation;)V:GetOnAnimationStart_Landroid_view_animation_Animation_Handler:Android.Views.Animations.Animation/IAnimationListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.ExpandAnimationListener, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ExpandAnimationListener.class, __md_methods);
	}


	public ExpandAnimationListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ExpandAnimationListener.class)
			mono.android.TypeManager.Activate ("ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.ExpandAnimationListener, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ExpandAnimationListener (expandablelistviewanimationexample.controls.expandablelistanimation.AnimatedExpandableListAdapter p0, int p1, expandablelistviewanimationexample.controls.expandablelistanimation.DummyView p2) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ExpandAnimationListener.class)
			mono.android.TypeManager.Activate ("ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.ExpandAnimationListener, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.AnimatedExpandableListAdapter, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:ExpandableListViewAnimationExample.Controls.ExpandableListAnimation.DummyView, ExpandableListViewAnimationExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onAnimationEnd (android.view.animation.Animation p0)
	{
		n_onAnimationEnd (p0);
	}

	private native void n_onAnimationEnd (android.view.animation.Animation p0);


	public void onAnimationRepeat (android.view.animation.Animation p0)
	{
		n_onAnimationRepeat (p0);
	}

	private native void n_onAnimationRepeat (android.view.animation.Animation p0);


	public void onAnimationStart (android.view.animation.Animation p0)
	{
		n_onAnimationStart (p0);
	}

	private native void n_onAnimationStart (android.view.animation.Animation p0);

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
