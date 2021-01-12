FROM mcr.microsoft.com/dotnet/aspnet:3.1

EXPOSE 8080

COPY  bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "KosalaiProj.dll"]