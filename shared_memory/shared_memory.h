#include <wtypes.h>

#ifndef SHAREDMEMORYDEF
#define SHAREDMEMORYDEF(f) WINAPI f
#endif


HRESULT SHAREDMEMORYDEF(create_shared_memory)(const void* name, size_t capacity, HANDLE* handle);

HRESULT SHAREDMEMORYDEF(open_shared_memory)(const void* name, size_t offset, size_t length, HANDLE* handle, LPVOID* map);

HRESULT SHAREDMEMORYDEF(close_shared_memory)(HANDLE handle, LPVOID map);

HRESULT SHAREDMEMORYDEF(read_shared_memory)(HANDLE handle, LPVOID map, const void* buffer, size_t offset, size_t length);

HRESULT SHAREDMEMORYDEF(write_shared_memory)(HANDLE handle, LPVOID map, const void* buffer, size_t offset, size_t length);

HRESULT SHAREDMEMORYDEF(release_shared_memory)(HANDLE handle);