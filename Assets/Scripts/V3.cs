using UnityEngine;
using System.Collections.Generic;

public partial class V3
{
	public V3Elem[,,]		vs;
	public V3Elem[,,]		subspaceVs;	
	public V3Elem[,]		lines;
	public V3Elem[,]		sublines;
	public V3Elem[,]		planes;
	

	public V3()															{	vs				=	new V3Elem[Const.q,Const.q,Const.q];
																			subspaceVs		=	new V3Elem[Const.q,Const.q,Const.q];
		
																			V3Elem v	=	new V3Elem(0);
																			for(int i=0;i<Const.v3Size;++i)
																			{	vs[Const.x(i),Const.y(i),Const.z(i)]	=	v;
																				v=v.Next();														}	
																				
																			for(int i=0;i<Const.p;++i)
																			for(int j=0;j<Const.p;++j)
																			for(int k=0;k<Const.p;++k)
																				subspaceVs[i,j,k]	=	
																					vs[i*Const.q/Const.p,j*Const.q/Const.p,k*Const.q/Const.p];
	
																			GenerateLines();
																			GenerateSublines();
																			GeneratePlanes();
																																																}
	
	public void GenerateLines()											{	List<V3Elem> points	=	new List<V3Elem>();
																			lines				=	new V3Elem[Const.q*Const.q+Const.q+1,Const.q];
																			GFq			field	=	new GFq();
																			
																			for(int i=0;i<Const.v3Size;++i)
																				points.Add(vs[Const.x(i),Const.y(i),Const.z(i)]);
																			
																			points.RemoveAll(x=>x==new V3Elem(0));
																			
																			for(int i=0;i<lines.GetLength(0);++i) 
																			{	for(int j=0;j<Const.q;++j)
																					lines[i,j]	=	field.xs[j]*points[0];
																				
																				for(int j=0;j<Const.q;++j)
																					points.RemoveAll(x => x==lines[i,j]);	}																	}
	
	public void GenerateSublines()										{	List<V3Elem> points	=	new List<V3Elem>();
																			sublines			=	new V3Elem[Const.p*Const.p+Const.p+1,Const.p];
																			GFq			field	=	new GFq();
																			
																			for(int i=0;i<Const.p;++i)
																			for(int j=0;j<Const.p;++j)
																			for(int k=0;k<Const.p;++k)
																				points.Add(subspaceVs[i,j,k]);
																			
																			points.RemoveAll(x=>x==new V3Elem(0));
																			
																			for(int i=0;i<sublines.GetLength(0);++i) 
																			{	for(int j=0;j<Const.p;++j)
																					sublines[i,j]	=	field.xs[j]*points[0];
																				
																				for(int j=0;j<Const.p;++j)
																					points.RemoveAll(x => x==sublines[i,j]);	}																}

	public void GeneratePlanes()											{	planes							=	new V3Elem[Const.q*Const.q+Const.q+1,Const.q*Const.q];
																				GFq				field			=	new GFq();
																				List<V3Elem[]>	paroviSmjerova	=	new List<V3Elem[]>();
																				List<List<V3Elem>>	planesTemp	=	new List<List<V3Elem>>();
																				
																				for(int i=0;i<lines.GetLength(0);++i) 	
																				for(int j=i+1;j<lines.GetLength(0);++j) 
																				{	V3Elem[] par=new V3Elem[2];
																					par[0]=lines[i,1];
																					par[1]=lines[j,1];
																					paroviSmjerova.Add(par);		}
																				
																				for(int a=0;a<planes.GetLength(0);++a)
																				{
																					planesTemp.Add(new List<V3Elem>());
																					for(int j=0;j<Const.q;++j)
																					for(int k=0;k<Const.q;++k)	
																					{	planesTemp[a].Add(field.xs[j]*paroviSmjerova[0][0] + field.xs[k]*paroviSmjerova[0][1]);
																						planes[a,j*Const.q+k]	=	planesTemp[a][j*Const.q+k];											}
																					
																					paroviSmjerova.RemoveAll(x=>planesTemp[a].Exists(y=> y==x[0]) && planesTemp[a].Exists(y=>y==x[1]) );					
																																															}	}	
	
	public	string 		Print()												{	string 	ret	=	"points\n";
																				for(int i=0;i<Const.q;++i)
																				{	for(int j=0;j<Const.q;++j)
																					{	for(int k=0;k<Const.q;++k)
																						{	ret+=vs[i,j,k]+" ";		}
																						ret+="\n";						}
																					ret+="\n\n";							}	
																				return ret;																										}
	
	public string 		PrintLines()										{	string ret="lines\n";
																				for(int i=0;i<lines.GetLength(0);++i)
																				{	for(int j=0;j<Const.q;++j)
																						ret+=lines[i,j]+" ";
																					ret+="\n";								}		
																				return ret;																										}
	
	public string 		PrintPlanes()										{	string ret="planes\n";
																				for(int i=0;i<planes.GetLength(0);++i)
																				{	for(int j=0;j<Const.q*Const.q;++j)
																						ret+=planes[i,j]+" ";
																					ret+="\n";								}		
																				return ret;																										}
	
}

