#include "shared_memory.h"

HRESULT SHAREDMEMORYDEF(create_shared_memory)(const void* name, size_t capacity, HANDLE* handle) {
	*handle = CreateFileMapping(INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 0, capacity, name);
	if (*handle == NULL) {
		return E_FAIL;
	}
	return S_OK;
}

HRESULT SHAREDMEMORYDEF(release_shared_memory)(HANDLE handle) {
	if (!CloseHandle(handle)) {
		return E_FAIL;
	}
	return S_OK;
}