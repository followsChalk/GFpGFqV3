using UnityEngine;
using System.Collections.Generic;

public class Group
{
	public	string 		PrintGroupCycleTable()	
	{
		int col=0;
		string ret="";
		

		for(int j=2;	j<Const.q;					++j)
		if(j%Const.p!=0 )	   
		{
			ret+="<color=#555555ff>"+PrintInt(j)+"</color> ";
			
			List<List<int>> combinations=new List<List<int>>();
			List<int>		sizes		=	new List<int>();	
			List<int> used=new List<int>();
			List<int> partition=new List<int>();
			
			for(int i=1;	i<Const.q;			++i)
			{	
				if(used.Contains(i))continue;
				col=1-col;
				//if(col==0)	ret+="<color=#00aa00ff>";
				ret+="(";																				
				for(int k=i;;k=(j*k)%Const.q)
				{
					if(used.Contains(k))
					{
						if(partition.Count<=16)
						{
							combinations.Add(new List<int>(partition));
							sizes.Add(partition.Count);
						}
						partition.Clear();
						break;
					}
					partition.Add(k);
					used.Add(k);
					ret+=PrintInt(k)+" ";
				}
			
				ret+=")";
				//if(col==0)ret+="</color>";
						
			}
	

			
			used.Clear();
			ret+="\n";
			
			if(j==17 && Partitions(combinations))return ret;
		}
		return ret;
	}
	
	bool Partitions(List<List<int>> combinations)
	{
		for(int i=1;i<Mathf.Pow(2,combinations.Count);++i)
		{
			int sum=0;
			for(int k=0;k<combinations.Count && sum<=16;++k)
				if(((i>>k)&1)==1)sum+=combinations[k].Count;
			
			Debug.Log("sum "+sum);
			if(sum==16)
			if(TestDifferenceSet(i,combinations))return true;
		}
		return false;
	}
	
	public bool TestDifferenceSet(int indices,List<List<int>> combinations)
	{
		int[] ds = new int[Const.q];
		
		for(int i=0;i<combinations.Count;++i)
		for(int j=i;j<combinations.Count;++j)
		if(((indices>>i)&1)==1 && ((indices>>j)&1)==1) 
		{
			for(int s=0;s<combinations[i].Count;++s)
			for(int t=s+1;t<combinations[j].Count;++t)
			if(i!=j || i==j && s!=t)
			{
				++ds[(Const.q-(combinations[i][s]-combinations[j][t]))%Const.q];
				++ds[(Const.q-(combinations[j][t]-combinations[i][s]))%Const.q];	
			}
		}	
		
		string ret="";
		for(int i=1;i<Const.q;++i)
			ret+=ds[i]+" ";
			
		Debug.Log(ret);
		
		for(int i=1;i<Const.q;++i)
			if(ds[i]!=5)return false;
		return true;	
	}
	
	string PrintInt(int k)
	{
		string ret= k.ToString();
		for(int i=ret.Length;i<Const.q.ToString().Length;++i)
		ret=ret.Insert(0,"0"); 
		return ret;
	}
	
}