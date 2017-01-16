
public class GFpElem
{
	public long val;
	
	public 			GFpElem		(long val)							{	this.val=val%Const.p;	                    		}

	public			GFpElem		Zero								{	get{ return new GFpElem(0); 					}	}
	public 	static 	GFpElem		operator+(GFpElem a,GFpElem b)		{	return new GFpElem(a.val+b.val);	       	 		}
	public	static	GFpElem		operator-(GFpElem a)				{	return new GFpElem(Const.p-a.val);	        		}
	public 	static 	GFpElem		operator-(GFpElem a,GFpElem b)		{	return new GFpElem(Const.p+a.val-b.val);			}

	public			GFpElem		One									{	get{ return new GFpElem(1); 					}	}
	public 	static 	GFpElem		operator*(GFpElem a,GFpElem b)		{	return new GFpElem(a.val*b.val);	        		}	
	public	static	GFpElem		Pow(GFpElem a,int t)				{	return 	t<0?PowPos(1/a,-t):PowPos(a,t);     		}
    public  static  GFpElem     PowPos(GFpElem a,int t)             {   long ret=1;
                                                                        for(int i=0;i<t;ret=(ret*a.val)%Const.p, ++i);
                                                                        return new GFpElem(ret);							}
    public static 	GFpElem 	operator /(GFpElem a, GFpElem b) 	{ 	return a * Pow(b, Const.p - 2); 					}

    public	static	GFpElem		operator ++(GFpElem a)				{	return 1+a;											}
	public	static 	implicit	operator GFpElem(long t)			{	return new GFpElem(t);								}			
	public	static 	implicit 	operator string(GFpElem a)			{	return a.val.ToString();							}
	
	public	static		bool	operator==(GFpElem a,GFpElem b)		{	return a.val==b.val;								}
	public	static		bool	operator!=(GFpElem a,GFpElem b)		{	return a.val!=b.val;								}
	public 	override	bool	Equals(object a)					{	return this==a;										}
	public	override	int 	GetHashCode()						{	return (int)val;									}

}



