using UnityEngine;
using System.Collections;


public partial class V3{
public static class Draw{

	static Vector3 diagonal												=new Vector3(0.5f,0.5f,0.5f);
	static Vector3[,] cube 												=new Vector3[,] 
																		{	{	new Vector3(0f,0f,0f),new Vector3(1f,0f,0f),new Vector3(1f,1f,0f),new Vector3(0f,1f,0f)	},
																			{	new Vector3(0f,0f,0f),new Vector3(0f,1f,0f),new Vector3(0f,1f,1f),new Vector3(0f,0f,1f)	},
																			{	new Vector3(0f,0f,0f),new Vector3(0f,0f,1f),new Vector3(1f,0f,1f),new Vector3(1f,0f,0f)	},
																			{	new Vector3(1f,1f,1f),new Vector3(1f,0f,1f),new Vector3(0f,0f,1f),new Vector3(0f,1f,1f)	},
																			{	new Vector3(1f,1f,1f),new Vector3(0f,1f,1f),new Vector3(0f,1f,0f),new Vector3(1f,1f,0f)	},
																			{	new Vector3(1f,1f,1f),new Vector3(1f,1f,0f),new Vector3(1f,0f,0f),new Vector3(1f,0f,1f)	}					};

	static Vector3[,] tetra												=new Vector3[,]
																		{	{ new Vector3(0f,0f,0f),new Vector3(0f,1f,0f),new Vector3(0f,0f,1f)},
																			{ new Vector3(0f,0f,0f),new Vector3(1f,0f,0f),new Vector3(0f,1f,0f)},
																			{ new Vector3(0f,0f,0f),new Vector3(0f,0f,1f),new Vector3(1f,0f,0f)},
																			{ new Vector3(0f,1f,0f),new Vector3(1f,0f,0f),new Vector3(0f,0f,1f)}											};
	
	static Vector3[,] point = cube;
	public static void Point(V3Elem v,Vector3 center,Color col,float w)	{	for(int i=0;i<point.GetLength(0);++i)
																			{	GL.Begin(point==tetra?GL.TRIANGLES:GL.QUADS);
																				GL.Color(col);					
																				for(int j=0;j<point.GetLength(1);++j)
																					GL.Vertex(center + v.ToVector3() + w*(-diagonal  + point[i,j]) );	                          										
																				GL.End();																								}	}

	
	
	public static void Grid(V3 v3,Vector3 center)						{	/*for(int i=0;i<v3.vs.GetLength(0);++i)
																			for(int j=0;j<v3.vs.GetLength(1);++j)
																			for(int k=0;k<v3.vs.GetLength(2);++k)
																				Point(v3.vs[i,j,k],center,new Color(1,1,1,1f),0.05f);
*/
																			Point(v3.vs[0,0,0],center + diagonal*(Const.q/2f),new Color(1,1,1,0.1f),Const.q+1);													
																																															}	
	 
	public static void Lines(	V3 v3,
	                         	V3Elem[,] lines,Vector3 center,
	                         	int breakPoint=-1)						{	if(breakPoint==-1)breakPoint=Const.q;
																			for(int i=0;i<lines.GetLength(0);++i)
																			{	Vector3 v=	center	+	Vector3.right*(i%breakPoint)*(Const.q+2)	+	
																							Vector3.back*(i/breakPoint)*(Const.q+2);
																				
																				Line(lines,i,v);
																				//Grid(v3,v);
																																												}
		
		
		
																																															}
	 
	
	public static void Line(V3Elem[,] lines,int i,Vector3 center)		{	Vector3 v,w=Vector3.zero;
																			for(int j=0;j<lines.GetLength(1);++j)
																			{	v	=	w;
																				w	=	lines[i,j].ToVector3() ;
																				
																				if(j>0)Segment(center+v,center+w,Color.black);														
 																				
																				Point(lines[i,j],center,Color.green,0.15f);																}	}
																																	
																																												
	public static void Planes(	V3 v3,
	                          	V3Elem[,] planes,Vector3 center,
                          		int breakPoint=-1)						{	if(breakPoint==-1)breakPoint=Const.q;
																			for(int i=0;i<planes.GetLength(0);++i)
																			{	Vector3 v=	center+Vector3.right*(i%breakPoint)*(Const.q+2)	+	
																							Vector3.back*(i/breakPoint)*(Const.q+2);
																				//Grid(v3,v);
																				Plane(planes,i,v);																						}	}
		
	public static void Plane(V3Elem[,] planes,int i,Vector3 center)		{	Vector3 v,w=new Vector3(planes[i,0].x,planes[i,0].y,planes[i,0].z);
																			
																			for(int j=0;j<planes.GetLength(1);++j)
																			{	if(j>0)
																				{	v	=	w;
																					w	=	new Vector3(planes[i,j].x,planes[i,j].y,planes[i,j].z);
																					GL.Begin(GL.LINES);
																					
																					GL.Vertex(center+v);
																					GL.Vertex(center+w);
																					GL.End();																	}
																				//PreviewPoint(center,v3.planes[i,j],Color.blue);		
																																														}	}

	public static void Segment(Vector3 v,Vector3 w,Color col)			{ 	GL.Begin(GL.LINES); 	GL.Color(col);	GL.Vertex(v);	GL.Vertex(w);	GL.End();								}
}}
