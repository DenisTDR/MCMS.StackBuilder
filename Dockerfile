FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app/

COPY ./MCMS.StackBuilder.sln /app/src/MCMS.StackBuilder.sln
COPY ./MCMS.StackBuilder/MCMS.StackBuilder.csproj /app/src/MCMS.StackBuilder/MCMS.StackBuilder.csproj

WORKDIR /app/src/MCMS.StackBuilder/
                                                                         
ARG ENV_TYPE=CI_BUILD
COPY MCMS.StackBuilder/nuget.config /root/.nuget/NuGet/NuGet.Config
RUN dotnet restore

COPY ./ /app/src/

RUN dotnet publish --output "/app/bin" -c release 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime-env
RUN apt-get update && apt-get install -y \
    sudo \
    net-tools \
 && rm -rf /var/lib/apt/lists/*

WORKDIR /app/bin
ENV NETCORE_USER_UID 69

COPY docker-entrypoint.sh /usr/bin/docker-entrypoint.sh
RUN chmod +x /usr/bin/docker-entrypoint.sh
CMD ["docker-entrypoint.sh"]

COPY --from=build-env /app/bin /app/bin

