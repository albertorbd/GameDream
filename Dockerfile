FROM mcr.microsoft.com/dotnet/sdk:6.0 AS buildapp
WORKDIR /src
COPY . .
RUN dotnet publish "Presentation/Gamedream.Presentation.csproj" -c Release -o /Gamedream

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=buildapp /Gamedream ./
EXPOSE 7018
ENTRYPOINT ["dotnet", "Gamedream.Presentation.dll"]