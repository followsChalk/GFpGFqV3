using System;
using UnityEngine;

public class V3Elem
{
	public GFqElem x;
	public GFqElem y;
	public GFqElem z;
	
	public	V3Elem(V3Elem v)									:this(v.x,v.y,v.z){}
	public	V3Elem(GFqElem x,GFqElem y,GFqElem z)				{	this.x=new GFqElem(x);		this.y=new GFqElem(y);		this.z=new GFqElem(z);	}
	public	V3Elem(int x)										:this(new GFqElem(x),new GFqElem(0),new GFqElem(0)){}

	public	static	V3Elem		operator+(V3Elem v,V3Elem u)	{	return new V3Elem(v.x+u.x,v.y+u.y,v.z+u.z);										}
	public	static	V3Elem		operator-(V3Elem v)				{	return new V3Elem(-v.x,-v.y,-v.z);												}
	public	static	V3Elem		operator-(V3Elem v,V3Elem u)	{	return v + (-u);																}
	public	static	V3Elem		operator*(GFqElem a,V3Elem v)	{	return new V3Elem(a*v.x,a*v.y,a*v.z);											}
	

	public	static	bool		operator ==(V3Elem v,V3Elem u)	{	return v.x==u.x && v.y==u.y && v.z==u.z;										}
	public	static	bool		operator !=(V3Elem v,V3Elem u)	{	return !(v==u);																	}
	public override	bool		Equals(object v)				{	return this==v;																	}
	public override	int 		GetHashCode()					{	return this.x.GetHashCode();													}
	
	public	V3Elem				Next()							{	V3Elem ret	=	new V3Elem(this);
																	ret.x	=	ret.x.Next();	if(ret.x!=new GFqElem(0))return ret;	
																	ret.y	=	ret.y.Next();	if(ret.y!=new GFqElem(0))return ret;
																	ret.z	=	ret.z.Next();	return ret;											}

	public Vector3	ToVector3()									{	return new Vector3(x,y,z);														}

	public	static	implicit	operator string(V3Elem v)		{	int printType=0;
																	if(printType==0)	return "("+v.x+","+v.y+","+v.z+")";			
																	if(printType==1)
																	{	string ret = 	(v.x.GetNumLabel()	+
																						v.y.GetNumLabel()*Const.q+
																						v.z.GetNumLabel()*Const.q*Const.q).ToString();
																		
																		for(int i=ret.Length;i<Math.Pow(Const.q,3).ToString().Length;++i)
																			ret=ret.Insert(0,"0");
																		return ret;																}
																	return "";																		}
}
