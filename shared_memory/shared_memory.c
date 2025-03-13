#include "shared_memory.h"

HRESULT SHAREDMEMORYDEF(create_shared_memory)(const void* name, size_t capacity, HANDLE* handle) {
	*handle = CreateFileMapping(INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 0, capacity, name);
	if (*handle == NULL) {
		return E_FAIL;
	}
	return S_OK;
}

HRESULT SHAREDMEMORYDEF(open_shared_memory)(const void* name, size_t offset, size_t length, HANDLE* handle, LPVOID* map) {
	*handle = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, name);
	if (*handle == NULL) {
		return E_FAIL;
	}
	*map = MapViewOfFile(*handle, FILE_MAP_ALL_ACCESS, 0, offset, length);
	if (*map == NULL) {
		CloseHandle(*handle);
		return E_FAIL;
	}
	return S_OK;
}

HRESULT SHAREDMEMORYDEF(close_shared_memory)(HANDLE handle, LPVOID map) {
	if (!UnmapViewOfFile(map)) {
		return E_FAIL;
	}
	if (!CloseHandle(handle)) {
		return E_FAIL;
	}
	return S_OK;
}

HRESULT SHAREDMEMORYDEF(read_shared_memory)(HANDLE handle, LPVOID map, const void* buffer, size_t offset, size_t length) {
	CopyMemory((BYTE*)buffer + offset, map, length);
	return S_OK;
}

HRESULT SHAREDMEMORYDEF(write_shared_memory)(HANDLE handle, LPVOID map, const void* buffer, size_t offset, size_t length) {
	CopyMemory(map, (BYTE*)buffer + offset, length);
	return S_OK;
}

HRESULT SHAREDMEMORYDEF(release_shared_memory)(HANDLE handle) {
	if (!CloseHandle(handle)) {
		return E_FAIL;
	}
	return S_OK;
}