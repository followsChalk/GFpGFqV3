using System;
using System.Collections;

public static class Const
{
	static int _p=7; 
	static int _d=1; 


	static int _q;
	static int _v3Size;
	static bool hasVarsBeenSet=false;
	static void SetVars()			{	_q	= 		(int)Math.Pow(_p,_d);												
										_v3Size	= 	(int)Math.Pow(_q,3);																
										hasVarsBeenSet=true;																}

	public static int p				{	get	{	return _p;		}															}
	public static int d				{	get	{	return _d;		}															}
	
	public static int q				{	get{	if(hasVarsBeenSet==false)SetVars();		return _q;		}					}
	public static int v3Size		{	get{	if(hasVarsBeenSet==false)SetVars();		return _v3Size;	}					}

	public static int x(int i)		{	return i/(_q*_q); 																	}
	public static int y(int i)		{	return (i/_q)%_q; 																	}	
	public static int z(int i)		{	return i%_q; 																		}	
}
