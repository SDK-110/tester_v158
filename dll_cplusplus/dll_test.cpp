
#include<afx.h>		
#include<afxwin.h>
#include<afxext.h>
#include<iostream>
using namespace std;

extern   "C"   __declspec(dllexport)  int  ShowDlg(WCHAR **ppStr, int len) {


	MessageBox(0, *ppStr, L"fsaf", 1);

	return 1;

};

extern   "C"   __declspec(dllexport)  int Str_Output(WCHAR *pStr)
{
	if (NULL == pStr)
	{
		return(-1);
	}

	ShellMessageBoxW(0, 0, pStr, NULL, 1);

	return(0);
}


static  WCHAR *g_StrReturn = (WCHAR *) L"Str_Return";
extern   "C"   __declspec(dllexport) WCHAR * Str_Return()
{
	wprintf(L"Str_Return \n");

	return(g_StrReturn);
}



extern   "C"   __declspec(dllexport) void __cdecl ShowDlg_QQ(int * i)
{

	
	*i = 130;
	
	

}

typedef void(__stdcall * CPPCallback)(int tick);

extern   "C"   __declspec(dllexport)  void __stdcall SetCallback(CPPCallback callback) {


	callback(130);

};

extern   "C"   __declspec(dllexport) void Str_ParameterOut(WCHAR **ppStr)
{
	if (NULL == ppStr)
	{
		return;
	}

	*ppStr = (WCHAR *)CoTaskMemAlloc(128 * sizeof(WCHAR));

	lstrcpynW(*ppStr, L"abc", 128);
	
}



struct CXTest
{
	LPBYTE pData;     // 一个指向byte数组的指针
	int nLen;         // 数组的长度
};

extern   "C"   __declspec(dllexport)  bool __stdcall XFunction121(const CXTest &inData_, CXTest &outData_) {





	return 0;

};

struct CXTest1
{
	WCHAR wzName[64];
	int nLen;
	byte byData[100];
};
extern   "C"   __declspec(dllexport) bool __stdcall  SetTest(const CXTest1 &stTest_) {

	return 0;
};


struct UIM_BOOK_STRUCT
{
	int UimIndex;
	char szName[15];
	char szPhone[21];
};

extern   "C"   __declspec(dllexport) int __stdcall  ReadUimAllBook(UIM_BOOK_STRUCT lpUimBookItem[], int nMaxArraySize) {

	return 0;
};












#define EXPORTDLL_API  extern   "C"   __declspec(dllexport) 

typedef struct _testStru1
{
	int		iVal;
	char	cVal;
	__int64 llVal;
}testStru1;

EXPORTDLL_API void Struct_Change(testStru1 *pStru)
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->iVal = 1;
	pStru->cVal = 'a';
	pStru->llVal = 2;

	wprintf(L"Struct_Change \n");
}



typedef struct _testStru5
{
	int		iVal;
}testStru5;
testStru5	g_stru5;
EXPORTDLL_API testStru5* Struct_Return()
{
	g_stru5.iVal = 5;
	wprintf(L"Struct_Return \n");
	return(&g_stru5);
}


typedef struct _testStru6
{
	int		iVal;
}testStru6;
EXPORTDLL_API void Struct_StruArr(testStru6 *pStru, int len)
{
	if (NULL == pStru)
	{
		return;
	}

	for (int ix = 0; ix < len; ix++)
	{
		pStru[ix].iVal = ix;
	}

	wprintf(L"Struct_StruArr \n");
}

typedef struct _testStru8
{
	int		m;
}testStru8;
EXPORTDLL_API void Struct_ParameterOut(testStru8 **ppStru)
{
	if (NULL == ppStru)
	{
		return;
	}

	*ppStru = (testStru8*)CoTaskMemAlloc(sizeof(testStru8));

	(*ppStru)->m = 8;
	wprintf(L"Struct_ParameterOut \n");
}


typedef struct _testStru3
{
	int		iValArrp[30];
	WCHAR	szChArr[30];
}testStru3;
EXPORTDLL_API void Struct_ChangeArr(testStru3 *pStru)
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->iValArrp[0] = 8;
	lstrcpynW(pStru->szChArr, L"as", 30);

	wprintf(L"Struct_ChangeArr \n");
}

typedef struct _testStru7
{
	int		m[5][5];
}testStru7;
EXPORTDLL_API void Struct_Change2DArr(testStru7 *pStru)
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->m[3][3] = 1;
	wprintf(L"Struct_Change2DArr \n");
}

typedef struct _testStru9
{
	WCHAR	*pWChArr;
	CHAR	*pChArr;
	bool	IsCbool;
	BOOL	IsBOOL;
}testStru9;
EXPORTDLL_API void Struct_ChangePtr(testStru9 *pStru)
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->IsBOOL = true;
	pStru->IsBOOL = TRUE;
	pStru->pWChArr = (WCHAR*)CoTaskMemAlloc(8 * sizeof(WCHAR));
	pStru->pChArr = (CHAR*)CoTaskMemAlloc(8 * sizeof(CHAR));

	lstrcpynW(pStru->pWChArr, L"ghj", 8);
	lstrcpynA(pStru->pChArr, "ghj", 8);

	wprintf(L"Struct_ChangePtr \n");
}


#pragma pack(push)
#pragma pack(1)
typedef struct _testStru2
{
	int		iVal;
	char	cVal;
	__int64 llVal;
}testStru2;
#pragma pack(pop)
EXPORTDLL_API void Struct_PackN(testStru2 *pStru)
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->iVal = 1;
	pStru->cVal = 'a';
	pStru->llVal = 2;

	wprintf(L"Struct_PackN \n");
}

typedef union _testStru4
{
	int		iValLower;
	int		iValUpper;
	struct
	{
		__int64 llLocation;
	};
}testStru4;
EXPORTDLL_API void Struct_Union(testStru4 *pStru)
{
	if (NULL == pStru)
	{
		return;
	}

	pStru->llLocation = 1024;
	wprintf(L"Struct_Union \n");
}


extern   "C"   __declspec(dllexport) int Str_Change(WCHAR *pStr, int len)
{
	if (NULL == pStr)
	{
		return(-1);
	}

	for (int ix = 0; ix < len - 1; ix++)
	{
		pStr[ix] = 'a' + (ix) % 26;
	}
	pStr[len - 1] = '\0\0';

	wprintf(L"Str_Change %s\n", pStr);
	return(0);
}






//int    ShowDlg(WCHAR **ppStr, int len)
//{
//	if (NULL == ppStr)
//	{
//		return(-1);
//	}
//
//	for (int ix = 0; ix < len; ix++)
//	{
//		if (NULL != ppStr[ix])
//		{
//			lstrcpyn(ppStr[ix], L"abc", 10);
//		}
//	}
//
//	return 0;
//}


