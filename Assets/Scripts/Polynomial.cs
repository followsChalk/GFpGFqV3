
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Polynomial
{
	public List<GFpElem>	vals;
	
	public	Polynomial		(List<GFpElem> vals)									{	this.vals	=	new List<GFpElem>(vals);		
																						Trim ();																			}		
		
	public 	Polynomial		(params GFpElem[] vals)									:this(vals.ToList()){}
	public	Polynomial		(params int[] vals)										{	this.vals	=	new List<GFpElem>();
																						for(int i=0;i<vals.Length;++i)
																							this.vals.Add(new GFpElem(vals[i]));
									
																						Trim ();																			}

	public void Trim()																{	for(int i=this.vals.Count-1;i>=0;--i)
																						if(this.vals[i]!=new GFpElem(0))return;
																						else this.vals.RemoveAt(i);															}


	public 	static 	Polynomial		operator+(Polynomial f,Polynomial g)			{	int len	=	Math.Max(f.vals.Count,g.vals.Count);
																						List<GFpElem> ret	=	new List<GFpElem>();
																						for(int i=0;i<len;++i) 
																						ret.Add	(	(i<f.vals.Count?f.vals[i]:0)	+
																									(i<g.vals.Count?g.vals[i]:0));
																						return new Polynomial(ret);															}
	
	public	static	Polynomial		operator-(Polynomial f)							{	List<GFpElem> ret	=	new List<GFpElem>();
																						for(int i=0;i<f.vals.Count;++i) ret.Add(-f.vals[i]);
																						return new Polynomial(ret);															}

		
	public 	static 	Polynomial		operator-(Polynomial f,Polynomial g)			{	return f + (-g);																	}	
	
	public 	static 	Polynomial		operator*(Polynomial f,Polynomial g)			{	int len	=	f.vals.Count	+	g.vals.Count;
																						List<GFpElem> ret	=	new List<GFpElem>();
																						for(int i=0;i<len;++i) 
																						{	GFpElem temp	=	new GFpElem(0);
																							for(int j=0;j<=i;++j)
																								temp	+=	(j<f.vals.Count?f.vals[j]:0)	*
																											(i-j<g.vals.Count?g.vals[i-j]:0);
																							ret.Add(temp);										}
																						return new Polynomial(ret);															}
	
	public	static	Polynomial		Pow(Polynomial f,int t)							{	return 	t<0?0:(t==0?1:f*Pow(f,t-1));												}
	
	public	static	Polynomial[]	ResDiv(Polynomial f,Polynomial g)				{	Polynomial[] ret		=	new Polynomial[2];
																						Polynomial 	quotient	=	new Polynomial(0);
																						Polynomial	divisor		=	new Polynomial(g.vals);
																						Polynomial	res			=	new Polynomial(f.vals);

																						ret[0]	=	quotient;
																						ret[1]	=	res;
																						if(f.vals.Count==0 || g.vals.Count==0)return ret;

																				        while(res.vals.Count>=divisor.vals.Count)
																						{	Polynomial temp = new Polynomial(0);
																				            for (int i = 0; i < res.vals.Count - divisor.vals.Count; ++i) 
																								temp.vals.Add(new GFpElem(0));
																				            temp.vals.Add(	(res.vals[res.vals.Count - 1] / 
			               																					divisor.vals[divisor.vals.Count - 1]).val);

																				         //   Debug.Log("res: " + (string)res);
																				           // Debug.Log("temp: "+(string) temp);

																				            quotient += temp;
																				            res = res - divisor * temp;											}
																						
																						ret[0]	=	quotient;
																						ret[1]	=	res;
																						return ret;																			}
	
	public 	static Polynomial		GCD(Polynomial f,Polynomial g)					{	if(g==new Polynomial(0))return f;
																							return GCD(g,ResDiv(f,g)[1]);													}
	
	public	static	bool			operator==(Polynomial f,Polynomial g)			{	if(f.vals.Count!=g.vals.Count)return false;
																							for(int i=0;i<f.vals.Count;++i)
																								if(f.vals[i]!=g.vals[i])return false;
																							return true;																	}
	
	public	static		bool		operator!=(Polynomial f,Polynomial g)			{	return !(f==g);																		}	
	public	override	bool		Equals(object b)								{	return this==b;																		}
	public	override	int 		GetHashCode()									{	return this.vals.GetHashCode();														}


	public			Polynomial		Next(int d=-1)									{	if(d==-1)d=Const.d;
		
																						for(int i=0;i<d;++i)
																						{	if(i>=vals.Count)vals.Add(0);
																							++vals[i];
																							if(vals[i]!=new GFpElem(0))break;		}	
																						return new Polynomial(vals);														}
	
	public	static 	implicit		operator Polynomial(int t)						{	return new Polynomial(t);															}
	
	public	static 	implicit 		operator string(Polynomial f)					{	string ret="";
																						int printType=0;
																						/*test*/
																					
																						if(printType==0)
																						{	for(int i=f.vals.Count-1;i>=0;--i)
																								ret+=f.vals[i];
																							for(int i=f.vals.Count;i<Const.d;++i)
																								ret=ret.Insert(0,"0");
																								
																							return ret;																}
																								
																						if(printType==1)
																						{	ret=f.GetNumLabel().ToString();
																							for(int i=ret.Length;i<Math.Pow(Const.p,Const.d).ToString().Length;++i)
																								ret=ret.Insert(0,"0");
																							
																							return ret;																}
																						
																						if(printType==2)
																						{
																							int max=	(Const.d>2?(Const.d-2)*4:0) + 5;
																							ret	=	f.vals.Count==0?"0":"";
																							
																							for(int i=0;i<f.vals.Count;++i)
																							{	if(f.vals[i]==0)continue;
																								if(i!=0 && f.vals[i-1]!=0)ret+="+";
																								ret	+=	(i!=0 && f.vals[i]==1?"":f.vals[i])	+
																										(i==0?"":"x"+(i==1?"":i.ToString())); 		}
																							
																							for(int i=ret.Length;i<max;++i)
																								ret+="_";
																								
																							return	ret.Substring(0,max);											}

																				        if (printType == 3)
																				        {	long[] niz =new long[8];
																				            for(int i=0;i<8;++i)
																				                niz[i]=(7-i>f.vals.Count-1)? 0 : f.vals[7-i].val;

																				            long part1 = 0, part2 = 0,acc=8;
																				            for(int i=0;i<4;++i)
																				            {  part1 += acc * niz[i];
																				                part2 += acc * niz[i + 4];
																				                acc /= 2;							}

																				            ret += part1.ToString("X")+part2.ToString("X");

																				            return ret;																}

																						return ret;																			}

	public	int 		GetNumLabel()												{	int bla=0;
																						for(int i=0;i<this.vals.Count;++i)
																							bla+=(int)this.vals[i].val*(int)Math.Pow(Const.p,i);
																						return bla;																			}
	//Rabin
	public static bool IrreducibilityTest(Polynomial f)								{	// Debug.Log("Irreducibility: " + (string)f);

																						List<int> ps	=	new List<int>();
																						List<int> ns	=	new List<int>();
																						int n			=	Const.d;
																						for(int i=2;i<=n;++i)
																						{	bool prime=true;
																							for(int j=2;j<=Math.Floor(Math.Sqrt(i));++j)
																							if(i%j==0){prime=false; break;}
																							
																							if(prime && n%i==0)
																							{	ps.Add(i);
																								ns.Add(n/i);	}								}
																						
																						for(int k=0;k<ps.Count;++k)
																						{	Polynomial temp	=	Polynomial.Pow(	new Polynomial(0,1),
			                                   																					(int)Math.Pow(Const.p,ns[k]))  -	
																												new Polynomial(0,1);
																				          
																				            Polynomial h	=	Polynomial.ResDiv(temp,f)[1];
																				            
																				            Polynomial g	=		Polynomial.GCD(h,f);
																							//Debug.Log("f:"+f+", g:"+g+", h:"+h);
																							if(g!=new Polynomial(1))return false;									}
																						
																				        
																						Polynomial temp2=	Polynomial.Pow(	new Polynomial(0,1),
		                                 																					(int)Math.Pow(Const.p,n))-	
																											new Polynomial(0,1);
																						Polynomial g2	=	Polynomial.ResDiv(temp2,f)[1];
																						if(g2==new Polynomial(0))return true;
																						return false;																		}
	
	
	public static string Print()													{	string ret	=	"";

																						for(Polynomial f=new Polynomial(0,0);;)
																						{ 	ret+=f+"\n";
																							
																							f=f.Next();
																							if(f==new Polynomial(0)) break;			}
																						
																						return ret;																			}		
	
	public static string PrintMultiTable()											{	string ret	=	"";
		
																						for(Polynomial f=new Polynomial(0,0);;)
																						{ 	for(Polynomial g=new Polynomial(0,0);;)
																							{ 	ret+=f*g+"\t";
																								
																								g=g.Next();
																								if(g==new Polynomial(0))break;			}
																							ret+="\n";
																							
																							f=f.Next();
																							if(f==new Polynomial(0)) break;						}
																						
																						return ret;																			}	


	public static string PrintResDivTable()											{	string ret	=	"";
		
																						for(Polynomial f=new Polynomial(0,0);;)
																						{ 	for(Polynomial g=new Polynomial(0,0);;)
																							{ 	ret+=ResDiv(f,g)[0] +", "+ResDiv(f,g)[1]+"\t";
																								
																								g=g.Next();
																								if(g==new Polynomial(0))break;						}
																							ret+="\n";
																							
																							f=f.Next();
																							if(f==new Polynomial(0)) break;								}
																						
																						return ret;																			}	
}





