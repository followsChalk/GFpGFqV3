using UnityEngine;
using System.Collections.Generic;

public class GFq
{
	public GFqElem[]	xs;
	public GFqElem[] 	inverses;
	public GFqElem[,]	addTable;
	public GFqElem[,]	subTable;
	public GFqElem[,]	multiTable;
	public List<GFqElem> kvadrati;
	public List<GFqElem> multiplier;
	
	public GFq()
	{	
		xs			=	new GFqElem[Const.q];
		inverses	=	new GFqElem[Const.q];
		addTable	=	new GFqElem[Const.q,Const.q];
		subTable	=	new GFqElem[Const.q,Const.q];
		multiTable	=	new GFqElem[Const.q,Const.q];
		kvadrati	=	new List<GFqElem>();
		multiplier	=	new List<GFqElem>();
		
	
		
		int i=0,j;
		for(Polynomial f=new Polynomial(0);;++i)
		{ 
			xs[i]		=	new GFqElem(f);

			inverses[i]	=	1/xs[i];
			
			//Debug.Log(i+" "+xs[0].f.vals.Count);
			
			j=0;
			for(Polynomial g=new Polynomial(0);;++j)
			{ 
			
				addTable[i,j] 	= 	new GFqElem(f)+new GFqElem(g);
				subTable[i,j] 	= 	new GFqElem(f)-new GFqElem(g);
				multiTable[i,j]	=	new GFqElem(f)*new GFqElem(g);
				
				g=g.Next();
				if(g==new Polynomial(0))break;
			}
			
			f=f.Next();
			if(f==new Polynomial(0)) break;
		}
		
		for(int k=1;k<xs.Length;++k)
			if(kvadrati.Exists(x=> x==xs[k]*xs[k])==false)kvadrati.Add(xs[k]*xs[k]);
		
		
	}
	
	public	string		Print()
	{
		string ret="polje\n";
		for(int i=0;i<xs.Length;++i)
		{
			if(kvadrati.Exists(x=>x==xs[i]))ret+="<color=#ff0000ff>";
			ret+=xs[i]+" ";
			if(kvadrati.Exists(x=>x==xs[i]))ret+="</color>";
			
		}
		return ret;
	}
	
	public	string		PrintKvadrati()
	{
		string ret="kvadrati\n";
		
		for(int i=0;i<kvadrati.Count;++i)
			ret+=kvadrati[i]+" ";
		return ret;
	}
	
	public	string 		PrintAddTable()	
	{
		string 	ret	=	"";
		for(int i=0;	i<Const.q;		ret+="\n",					++i)
			for(int j=0;	j<Const.q;		ret+=addTable[i,j]+" ",		++j);	
		return ret;
	}
	public	string 		PrintSubTable()	
	{
		string 	ret	=	"";
		for(int i=0;	i<Const.q;		ret+="\n",					++i)
			for(int j=0;	j<Const.q;		ret+=subTable[i,j]+" ",		++j);	
		return ret;
	}

	
	
	public	string 		PrintMultiTable()	
	{
		string 	ret	=	"multi\n<color=#555555ff>"+xs[0]+" ";
		for(int i=0;	i<Const.q	;	++i)		ret+=xs[i]+" ";
		ret+="</color>\n";
		
		for(int i=0;	i<Const.q;		ret+="\n",	++i)
		{
		ret+="<color=#555555ff>"+xs[i]+"</color> ";
			for(int j=0;	j<Const.q;					++j)
			{
				if(i==j)ret+="<color=#00ffffff>";
				//if(xs[i]==xs[j]*xs[i])ret+="__ ";	else 
				ret+=multiTable[i,j]+" "	;
				if(i==j)ret+="</color>";	
			}
		}
		return ret;
	}
	public	string 		PrintMultiplier()	
	{
		/*string 	ret	=	"multiplier\n<color=#555555ff>"+xs[0]+" ";
		for(int i=0;	i<Const.q	;	++i)		ret+=xs[i]+" ";
		ret+="</color>\n";

		for(int j=0;	j<Const.q;			ret+="\n",		++j)
		{
			for(int i=0;	i<Const.q;			++i)
			{
				if(i==0)ret+="<color=#555555ff>"+xs[j]+"</color> ";	
				if(i==j)ret+="<color=#00ffffff>";
				if( xs[i]+xs[3]==xs[j]*xs[i])ret+="__ ";	else 
					ret+=multiTable[i,j]+" "	;
				if(i==j)ret+="</color>";	
			}
		}
		
		*/
		 
		string ret="f";
		for(int i=0;i<multiplier.Count;++i)
		ret+=multiplier[i]+" ";
		return ret;
	}
			
	public	void 		FindMultiplier()	
	{
		string ret="FindMultiplier\n";
		string ret2="";		
		for(int a=0;	a<Const.q;			++a)
		{
			ret+="ret "+ a.ToString()+"\n";
			ret2+="ret2 "+a.ToString()+"\n";
			for(int j=2;	j<Const.q;			++j)
			{
				List<GFqElem> tempMultiplier=new List<GFqElem>();
				ret+=j.ToString()+" ";	
				ret2+=j.ToString()+" ";		
				
				List<int> temp=new List<int>();
				for(int i=1;	i<Const.q;			++i)
					temp.Add((i*j)%Const.q);
					
				for(int i=1;	i<Const.q;			++i)
				{
					if( temp.Contains((i+a)%Const.q))
					tempMultiplier.Add(xs[i]);
					
					ret+=((i+a)%Const.q).ToString()+" ";
					ret2+=(multiTable[i,j]).GetNumLabel()+" ";
				}
				temp.Clear();
				ret+="\n";
				ret2+="\n";
				Debug.Log(tempMultiplier.Count);
				if(tempMultiplier.Count==16)
				{
					Debug.Log("found");		
					multiplier=tempMultiplier;
					return;
				}
				else tempMultiplier.Clear();
			}
		//	Debug.Log(ret+"\n"+ret2);
			ret2="";
			ret="";
		}		

	}
	
		
	public	string		PrintMultiInverseTable()			
	{
		string	ret1	=	"",	ret2	=	"";
		for(int i=0;i<Const.q;++i)	{	ret1+=xs[i]+" "; 	ret2+=inverses[i]+" ";	}
		return ret1+"\n"+ret2;
	}
	
	int[] 		intSet	=	new int[]{0,1,3,5,7,9,11,20,23,25,29,33,37,42,46,47} ;
	GFqElem[] 	diffSet	;
	public	string 		PrintDiffSetDifferences()
	{
		diffSet	=	new GFqElem[intSet.Length];
	/*	
		for(int i=0;i<intSet.Length;++i)
		{
			List<int> temp=new List<int>();
			
			while(intSet[i]!=0)
			{
				temp.Add(intSet[i]%Const.p);
				intSet[i]/=Const.p;
			}
				
			diffSet[i] = new GFqElem(temp.ToArray());			
		}
	*/	
		
		for(int i=0;i<Const.p;++i)diffSet[i]			=	new GFqElem(i,0);		
		for(int i=1;i<Const.p;++i)diffSet[i-1+Const.p]	=	new GFqElem(2	,i);
		diffSet[2*Const.p-1]	=new GFqElem(3,3);
		diffSet[2*Const.p]	=new GFqElem(5,5);
		diffSet[2*Const.p+1]	=new GFqElem(6,6);		
		
		string ret="";
		
		int[] counter=new int[Const.q];
		
		
		for(int i=0;i<diffSet.Length;++i)		
		for(int j=0;j<diffSet.Length;++j)		
		{
			if(i!=j)
 			{
//				ret+=(diffSet[i]-diffSet[j])+" ";
				++counter[(diffSet[i]-diffSet[j]).GetNumLabel()];
			}
		}
		string ret2="";
		for(int i=0;i<counter.Length;++i)
		{
			ret2+=i+" ";
			ret+=counter[i]+" ";		
		}
		return ret2+"\n"+ret;
	}
}

