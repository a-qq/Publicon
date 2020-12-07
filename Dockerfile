#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Publicon.Api/Publicon.Api.csproj", "Publicon.Api/"]
COPY ["Publicon.Infrastructure/Publicon.Infrastructure.csproj", "Publicon.Infrastructure/"]
COPY ["Publicon.Core/Publicon.Core.csproj", "Publicon.Core/"]
RUN dotnet restore "Publicon.Api/Publicon.Api.csproj"
COPY . .
WORKDIR "/src/Publicon.Api"
RUN dotnet build "Publicon.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Publicon.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV FrontendSettings__Url #type your own variable
ENV MailSettings__Email #type your own variable
ENV MailSettings__Password #type your own variable
ENV ConnectionStrings__SqlExpress #type your own variable
ENV JwtSettings__secretKey #type your own variable
ENV AdminEmail #type your own variable
ENV AdminPassword #type your own variable
ENV SecurityCodeExpiryTimeInMin 60
ENV AntySpamTimeSpanInMin 2
ENV ConnectionStrings__AzureStorage #type your own variable

ENTRYPOINT ["dotnet", "Publicon.Api.dll"]