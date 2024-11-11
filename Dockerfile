# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CaseClient.WebAPI/CaseClient.WebAPI.csproj", "src/CaseClient.WebAPI/"]
COPY ["src/CaseClient.Application/CaseClient.Application.csproj", "src/CaseClient.Application/"]
COPY ["src/CaseClient.Core/CaseClient.Core.csproj", "src/CaseClient.Core/"]
COPY ["src/CaseClient.Domain/CaseClient.Domain.csproj", "src/CaseClient.Domain/"]
COPY ["src/CaseClient.Data/CaseClient.Data.csproj", "src/CaseClient.Data/"]
RUN dotnet restore "./src/CaseClient.WebAPI/CaseClient.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/CaseClient.WebAPI"
RUN dotnet build "./CaseClient.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CaseClient.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CaseClient.WebAPI.dll"]