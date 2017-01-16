using UnityEngine;
using System.Collections;
using System;

public class Main : MonoBehaviour 
	{

	V3 v3	=	new V3();


	// Use this for initialization
	void Start () 														{
																			Debug.Log(v3.PrintLines());	
																			
																		//	GFq bigField	=	new GFq();
																			//Debug.Log(bigField.PrintSubTable()+"");
																		//		Debug.Log(bigField.Print()+"");	
																				
																				
																		//		bigField.FindMultiplier();
																		//	Debug.Log(bigField.PrintMultiplier()+"");	
																		//	Debug.Log(bigField.PrintDiffSetDifferences());
																		//	Debug.Log(bigField.PrintGroupCycleTable()+"");
																		//		Debug.Log(bigField.PrintAddTable()+""); 
																		//		Debug.Log(bigField.PrintMultiTable()+"");
																				
																			//	Group g=new Group();
																		//		Debug.Log(g.PrintGroupCycleTable()+"");
																		//	Debug.Log(bigField.PrintMultiInverseTable());
																			//Debug.Log("divisor\n"+GFqElem.divisor+""); 
																			

																		//	Debug.Log(v3.Print());
																		/*	v3.GenerirajPravce();
																				v3.GenerirajPotpravce();
																			v3.Generirajplanes();
																		*/	
																			
																			//Debug.Log(v3.PrintLines());
																			//Debug.Log(v3.Printplanes());
																			//Debug.Log(new GFq().Print());		
																																												}
	
	
	void OnPostRender()													{	CreateLineMaterial();			
																			lineMaterial.SetPass(0);
																			V3.Draw.Lines(v3,v3.lines,Vector3.zero);			
																			V3.Draw.Lines(v3,v3.planes,Vector3.up*20);
																			//	PreviewSubLines(Vector3.down*30);	
																			//	Previewplanes(Vector3.up*30);	
																																												}
	#region Material
	
	static Material lineMaterial;
	static void CreateLineMaterial()									{	if (!lineMaterial)
																			{	//lineMaterial = Resources.Load("TetrisMat") as Material;
																				
																				// Unity has a built-in shader that is useful for drawing
																				// simple colored things.
																				var shader = Shader.Find("Hidden/Internal-Colored");
																				lineMaterial = new Material(shader);
                      															lineMaterial.hideFlags = HideFlags.HideAndDontSave;
																				// Turn on alpha blending
																				lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
																				lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
																				// Turn backface culling off
																				lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
																				// Turn off depth writes
																				lineMaterial.SetInt("_ZWrite", 1);
																				// lineMaterial.EnableKeyword("_ZTest");
																				//lineMaterial.SetInt("_ZTest",1);															
																																											}	}
		
	
	
	#endregion

}
