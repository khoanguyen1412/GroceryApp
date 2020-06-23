package mono.com.onesignal;


public class OSSessionManager_SessionListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.onesignal.OSSessionManager.SessionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSessionEnding:(Ljava/util/List;)V:GetOnSessionEnding_Ljava_util_List_Handler:Com.OneSignal.Android.OSSessionManager/ISessionListenerInvoker, OneSignal.Android.Binding\n" +
			"";
		mono.android.Runtime.register ("Com.OneSignal.Android.OSSessionManager+ISessionListenerImplementor, OneSignal.Android.Binding", OSSessionManager_SessionListenerImplementor.class, __md_methods);
	}


	public OSSessionManager_SessionListenerImplementor ()
	{
		super ();
		if (getClass () == OSSessionManager_SessionListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.OneSignal.Android.OSSessionManager+ISessionListenerImplementor, OneSignal.Android.Binding", "", this, new java.lang.Object[] {  });
	}


	public void onSessionEnding (java.util.List p0)
	{
		n_onSessionEnding (p0);
	}

	private native void n_onSessionEnding (java.util.List p0);

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
