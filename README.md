A simple library for creating shared memory.

Create and release memory.
```c#
var result = SharedMemory.Create(name, size, out handle);
var result = SharedMemory.Release(handle);
```

Write a string,
```c#
var subject = "Hello World!";
var buffer = Encoding.UTF8.GetBytes(subject);
var result = SharedMemory.Open(name, 0, size, out handle, out map);
var result = SharedMemory.Write(handle, map, buffer, 0, buffer.Length);
```

Read a string.
```c#
var buffer = new byte[size];
var result = SharedMemory.Open(name, 0, size, out handle, out map);
var result = SharedMemory.Read(handle, map, buffer, 0, size);
var subject = Encoding.UTF8.GetString(buffer);
```
