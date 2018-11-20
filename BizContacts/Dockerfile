FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["BizContacts/BizContacts.csproj", "BizContacts/"]
RUN dotnet restore "BizContacts/BizContacts.csproj"
COPY . .
WORKDIR "/src/BizContacts"
RUN dotnet build "BizContacts.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BizContacts.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BizContacts.dll"]