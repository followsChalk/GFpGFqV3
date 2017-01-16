using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GFqElem
{
	public Polynomial	f;
	
	public static Polynomial	_divisor							=new Polynomial(0);
	public static Polynomial	divisor								{	get{	if(_divisor!=new Polynomial(0))return _divisor;
																				int[] temp=new int[Const.d+1];
																				temp[Const.d]=1;
																				Polynomial f=new Polynomial(temp);
																	           
																				while(true)
																				{	if(Polynomial.IrreducibilityTest(f))break;
																					f=f.Next(Const.d+1);
																					if(f==new Polynomial(0))break;					}
																			
																				_divisor	=	f;
																				return	_divisor;												}	}
											
	
	public	GFqElem		(GFqElem a)									{	this.f	=	new Polynomial(a.f.vals);										}
	public 	GFqElem		(Polynomial	f)								{	this.f	=	new Polynomial(f.vals);											}
	public	GFqElem		(params int[] vals)							{	this.f	=	new Polynomial(vals);											}


	public 	static 	GFqElem		operator+(GFqElem a,GFqElem b)		{	return new GFqElem(a.f+b.f);												}	
	public	static	GFqElem		operator-(GFqElem a)				{	return new GFqElem(-a.f);													}
	public 	static 	GFqElem		operator-(GFqElem a,GFqElem b)		{	return new GFqElem(a.f-b.f);												}	
	public 	static 	GFqElem		operator*(GFqElem a,GFqElem b)		{	return new GFqElem(Polynomial.ResDiv(a.f*b.f,divisor)[1]);					}
	public	static	GFqElem		Pow(GFqElem a,int t)				{	return 	new GFqElem(Polynomial.Pow(a.f,t));									}
	
	//brute force za sad
	public			GFqElem		Inverse()							{	for(Polynomial f=new Polynomial(0,0);;)
																		{ 	if((this*new GFqElem(f)).f==new Polynomial(1))return new GFqElem(f);
																			f=f.Next();
																			if(f==new Polynomial(0)) break;										}
																		return new GFqElem(0);														}

	
	public	static 	GFqElem		operator/(GFqElem a,GFqElem b)		{	return a*b.Inverse();														}
	
	public	GFqElem				Next()								{	return new GFqElem(this.f.Next());											}
	
	public	static	bool		operator==(GFqElem a,GFqElem b)		{	return a.f==b.f;															}
	public	static	bool		operator!=(GFqElem a,GFqElem b)		{	return a.f!=b.f;															}
	public	override	bool	Equals(object b)					{	return this.f==((GFqElem)b).f;												}
	public	override	int		GetHashCode()						{	return this.f.GetHashCode();												}
	
	public	static 	implicit	operator GFqElem(int t)				{	return new GFqElem(t);														}
	public	static 	implicit 	operator string(GFqElem a)			{	return ""+ a.f;																}
	
	public	static 	implicit	operator	float(GFqElem a)		{	return 	(float)a.GetNumLabel();												}
	
	public	int 				GetNumLabel()						{	return this.f.GetNumLabel();												}
	
}




