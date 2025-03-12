#include <wtypes.h>

#ifndef SHAREDMEMORYDEF
#define SHAREDMEMORYDEF(f) WINAPI f
#endif

HRESULT SHAREDMEMORYDEF(create_shared_memory)(const void* name, size_t capacity, HANDLE* handle);

HRESULT SHAREDMEMORYDEF(release_shared_memory)(HANDLE handle);