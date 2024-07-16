#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
#ARG TARGETARCH
#COPY . /source
#WORKDIR /source
#RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    #dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS development
#COPY . /source
#WORKDIR /source
#CMD dotnet run --no-launch-profile
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final
#WORKDIR /app
#COPY --from=build /app .
#ARG UID=10001
#RUN adduser \
    #--disabled-password \
    #--gecos "" \
    #--home "/nonexistent" \
    #--shell "/sbin/nologin" \
    #--no-create-home \
    #--uid "${UID}" \
    #appuser
#USER appuser
#ENTRYPOINT ["dotnet", "OnlineStore.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && apt-get install -y libfontconfig1
WORKDIR /app
#EXPOSE 5001
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnlineStore/OnlineStore.csproj", "OnlineStore/"]
RUN dotnet restore "./OnlineStore/OnlineStore.csproj"
COPY . .
WORKDIR "/src/OnlineStore"
RUN dotnet build "./OnlineStore.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnlineStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.dll"]