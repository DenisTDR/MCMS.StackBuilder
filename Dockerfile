FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
WORKDIR /app/

COPY ./MCMS.StackBuilder.sln /app/src/MCMS.StackBuilder.sln
COPY ./MCMS/MCMS/MCMS.csproj /app/src/MCMS/MCMS/MCMS.csproj
COPY ./MCMS/MCMS.Base/MCMS.Base.csproj /app/src/MCMS/MCMS.Base/MCMS.Base.csproj
COPY ./MCMS/MCMS.Auth/MCMS.Auth.csproj /app/src/MCMS/MCMS.Auth/MCMS.Auth.csproj
COPY ./MCMS/MCMS.Common/MCMS.Common.csproj /app/src/MCMS/MCMS.Common/MCMS.Common.csproj
COPY ./MCMS/MCMS.Files/MCMS.Files.csproj /app/src/MCMS/MCMS.Files/MCMS.Files.csproj
COPY ./MCMS.StackBuilder/MCMS.StackBuilder.csproj /app/src/MCMS.StackBuilder/MCMS.StackBuilder.csproj

WORKDIR /app/src/

RUN dotnet restore

COPY ./ /app/src

RUN dotnet publish --output "/app/bin" --configuration release 

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime-env
RUN apt-get update && apt-get install -y \
    sudo \
    net-tools \
 && rm -rf /var/lib/apt/lists/*

WORKDIR /app/bin
EXPOSE 6900
ENV NETCORE_USER_UID 69

COPY docker-entrypoint.sh /usr/bin/docker-entrypoint.sh
RUN chmod +x /usr/bin/docker-entrypoint.sh
CMD ["docker-entrypoint.sh"]

COPY --from=build-env /app/bin /app/bin

