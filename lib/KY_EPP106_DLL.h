
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the KY_EPP106_DLL_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// KY_EPP106_DLL_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef KY_EPP106_DLL_EXPORTS
#define KY_EPP106_DLL_API __declspec(dllexport)
#else
#define KY_EPP106_DLL_API __declspec(dllimport)
#endif

// This class is exported from the KY_EPP106_DLL.dll
class KY_EPP106_DLL_API CKY_EPP106_DLL {
public:
	CKY_EPP106_DLL(void);
	// TODO: add your methods here.
};

extern KY_EPP106_DLL_API int nKY_EPP106_DLL;

KY_EPP106_DLL_API int fnKY_EPP106_DLL(void);

KY_EPP106_DLL_API int _stdcall KY_EPP106_DLL_OPENPORT(int iPort, int iBaudrate);
KY_EPP106_DLL_API int _stdcall KY_EPP106_DLL_CLOSEPORT(void);
KY_EPP106_DLL_API int _stdcall KY_EPP106_DLL_COMMAND(unsigned char ucCmd, unsigned char *ucData, unsigned char ucDLen, unsigned char *ucResponse);
KY_EPP106_DLL_API void _stdcall KY_EPP106_DLL_GETCODE(unsigned char *ucCode);
KY_EPP106_DLL_API void _stdcall KY_EPP106_DLL_ASCTOBCD(unsigned char * ins,unsigned char * ous,int ilen);
KY_EPP106_DLL_API void _stdcall KY_EPP106_DLL_BCDTOASC(unsigned char * ins,unsigned char * ous,int ilen);

