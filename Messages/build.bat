
protoc Protos.proto -I=. -I=../repos/protoactor-dotnet/src --csharp_out=. --csharp_opt=file_extension=.g.cs --grpc_out . --plugin=protoc-gen-grpc=%userprofile%\.nuget\packages\Grpc.Tools\1.8.3\tools\windows_x64\grpc_csharp_plugin.exe
dotnet ..\repos\protoactor-dotnet\protobuf\ProtoGrainGenerator\bin\Debug\netcoreapp2.0\protograin.dll Protos.proto Protos_protoactor.cs