public class GFp
{
	public GFpElem[]	xs;
	public GFpElem[] 	inverses;
	public GFpElem[,]	addTable;
	public GFpElem[,]	multiTable;
	
	public GFp()											{	xs			=	new GFpElem[Const.p];
																inverses	=	new GFpElem[Const.p];
																addTable	=	new GFpElem[Const.p,Const.p];
																multiTable	=	new GFpElem[Const.p,Const.p];
																
																for(int i=0;i<Const.p;++i)
																{	xs[i]		=	new GFpElem(i);
																	inverses[i]	=	1/xs[i];
																	
																	for(int j=0;j<Const.p;++j)
																	{	addTable[i,j]	=	new GFpElem(i+j);
																		multiTable[i,j]	=	new GFpElem(i*j);	}		}								}
															
	public	string 		PrintAddTable()						{	string 	ret	=	"";
																for(int i=0;	i<Const.p;		ret+="\n",					++i)
																for(int j=0;	j<Const.p;		ret+=addTable[i,j]+" ",		++j);	
																return ret;																				}
	
	public	string 		PrintMultiTable()					{	string 	ret	=	"";
																for(int i=0;	i<Const.p;		ret+="\n",					++i)
																for(int j=0;	j<Const.p;		ret+=multiTable[i,j]+" ",	++j);	
																return ret;																				}
	
	public	string		PrintMultiInverseTable()			{	string	ret1	=	"",	ret2	=	"";
																for(int i=0;i<Const.p;++i)	{	ret1+=i+" "; 	ret2+=inverses[i]+" ";	}
																return ret1+"\n"+ret2;																	}	
}
